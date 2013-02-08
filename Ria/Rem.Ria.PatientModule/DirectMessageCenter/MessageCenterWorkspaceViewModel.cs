using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using System.Windows.Threading;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;
using Rem.WellKnownNames;
using Rem.WellKnownNames.PatientModule;
using Telerik.Windows.Controls.DragDrop;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// MessageCenter Workspace View Model
    /// </summary>
    public class MessageCenterWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _dispatcherFactory;
        private readonly IPopupService _popupService;
        private ObservableCollection<DirectMailDto> _incomingMail;
        private DispatcherTimer _retrieveInboxTimer;
        private DispatcherTimer _retrieveSentTimer;

        private DirectMailDto _selectedMail;
        private ObservableCollection<DirectMailDto> _sentMail;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageCenterWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="dispatcherFactory">The dispatcher factory.</param>
        public MessageCenterWorkspaceViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IPopupService popupService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory dispatcherFactory
            )
            : base ( accessControlManager, commandFactory )
        {
            _popupService = popupService;
            _dispatcherFactory = dispatcherFactory;
            IncomingMail = new ObservableCollection<DirectMailDto>();
            SentMail = new ObservableCollection<DirectMailDto>();

            CommandFactoryHelper<MessageCenterWorkspaceViewModel> commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );
            SendNewMailCommand = commandFactoryHelper.BuildDelegateCommand ( () => SendNewMailCommand, ExecuteSendNewMail );
            OpenSaveToExternalPatientHistoryCommand =
                commandFactoryHelper.BuildDelegateCommand<string> (
                    () => OpenSaveToExternalPatientHistoryCommand, ExecuteOpenSaveToExternalPatientHistory );
            DownloadMailAttachmentCommand = commandFactoryHelper.BuildDelegateCommand<string>(
                () => DownloadMailAttachmentCommand, DownloadMailAttachment);
            DragQueryCommand = commandFactoryHelper.BuildDelegateCommand<DragDropQueryEventArgs>(() => DragQueryCommand, ExecuteDragQuery);

            eventAggregator.GetEvent<MessageSentEvent> ().Subscribe ( HandleMessageSent );

            SetupImapFolderTimers ();
            RequestInboxContent ( null, null );
            RequestSentItemsContent ( null, null );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get { return "Direct Message Center"; }
        }
        
        /// <summary>
        /// Gets the incoming mail.
        /// </summary>
        public ObservableCollection<DirectMailDto> IncomingMail
        {
            get { return _incomingMail; }
            private set { ApplyPropertyChange ( ref _incomingMail, () => IncomingMail, value ); }
        }
        
        /// <summary>
        /// Gets the send new mail command.
        /// </summary>
        public ICommand SendNewMailCommand { get; private set; }

        /// <summary>
        /// Gets the open save to external patient history command.
        /// </summary>
        public ICommand OpenSaveToExternalPatientHistoryCommand { get; private set; }

        /// <summary>
        /// Gets the download mail attachment command.
        /// </summary>
        public ICommand DownloadMailAttachmentCommand { get; private set; }

        /// <summary>
        /// Gets the drag query command.
        /// </summary>
        public ICommand DragQueryCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected mail.
        /// </summary>
        /// <value>
        /// The selected mail.
        /// </value>
        public DirectMailDto SelectedMail
        {
            get { return _selectedMail; }
            set 
            { 
                ApplyPropertyChange ( ref _selectedMail, () => SelectedMail, value );
                if (_selectedMail != null && _selectedMail.HeadersOnly)
                {
                    IAsyncRequestDispatcher requestDispatcher = _dispatcherFactory.CreateAsyncRequestDispatcher();

                    requestDispatcher.Add(
                        new GetImapMailRequest
                        {
                            FolderName = _selectedMail.FolderName,
                            MailId = _selectedMail.Id
                        });

                    IsLoading = true;

                    requestDispatcher.ProcessRequests(HandleGetImapMailCompleted, HandleGetImapMailException);
                }
            }
        }
        
        /// <summary>
        /// Gets the sent mail.
        /// </summary>
        public ObservableCollection<DirectMailDto> SentMail
        {
            get { return _sentMail; }
            private set { ApplyPropertyChange ( ref _sentMail, () => SentMail, value ); }
        }
        
        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Administrative; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the message sent.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.DirectMessageCenter.MessageSentEventArgs"/> instance containing the event data.</param>
        public void HandleMessageSent ( MessageSentEventArgs args )
        {
            // SentMail.Add ( args.Mail );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>
        /// A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/>
        /// </returns>
        protected override ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory )
        {
            return CommandFactoryHelper.CreateHelper ( this, commandFactory );
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
        }

        private void ExecuteSendNewMail ()
        {
            _popupService.ShowPopup (
                "SendNewMailView",
                "OpenSendNewMailView",
                "New Mail" );
        }

        private void ExecuteOpenSaveToExternalPatientHistory(string mailAttachmentName)
        {
            _popupService.ShowPopup(
                "SaveMailAttachmentPatientDocumentView",
                null,
                null, 
                new[]
                    {
                        new KeyValuePair<string, string> ( "MailId", SelectedMail.Id.ToString( CultureInfo.InvariantCulture )  ),
                        new KeyValuePair<string, string> ( "MailFolderName", SelectedMail.FolderName  ),
                        new KeyValuePair<string, string> ( "MailFromName", string.IsNullOrWhiteSpace(SelectedMail.FromName)? SelectedMail.From : SelectedMail.FromName  ),
                        new KeyValuePair<string, string> ( "AttachmentName", mailAttachmentName  )
                    });
        }

        private void DownloadMailAttachment(string mailAttachmentName)
        {
            var relativePath = string.Format(
                "../{0}?{1}={2}&{3}={4}&{5}={6}&{7}={8}",
                HttpHandlerPaths.PatientModuleHttpHandlerPath,
                HttpUtility.UrlEncode(HttpHandlerQueryStrings.RequestName),
                HttpUtility.UrlEncode(HttpHandlerRequestNames.DownloadMailAttachment),
                HttpUtility.UrlEncode(HttpHandlerQueryStrings.MailId),
                SelectedMail.Id,
                HttpUtility.UrlEncode(HttpHandlerQueryStrings.MailFolderName),
                SelectedMail.FolderName,
                HttpUtility.UrlEncode(HttpHandlerQueryStrings.MailAttachmentName),
                mailAttachmentName);
            var uri = new Uri(Application.Current.Host.Source, relativePath);
            HtmlPage.Window.Navigate(uri, "_blank");
        }

        private void ExecuteDragQuery(DragDropQueryEventArgs obj)
        {
            var dragItem = obj.Options.Payload as DirectMailDto;
            if (dragItem != null)
            {
                obj.QueryResult = true;
            }
        }

        private void GetInboxItemsCompleted ( ReceivedResponses responses )
        {
            IsLoading = false;

            var inboxResponse = responses.Get<GetImapFolderItemsResponse> ( "INBOX" );

            foreach ( DirectMailDto messsage in inboxResponse.Messsages )
            {
                IncomingMail.Add ( messsage );
            }
            _retrieveInboxTimer.Start ();
        }

        private void GetSentItemsCompleted ( ReceivedResponses responses )
        {
            IsLoading = false;

            var sentItemsResponse = responses.Get<GetImapFolderItemsResponse> ( "Sent Items" );

            foreach (var messsage in sentItemsResponse.Messsages)
            {
                SentMail.Add ( messsage );
            }
            _retrieveSentTimer.Start ();
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
           IsLoading = false;
        }

        private void RequestInboxContent ( object sender, EventArgs e )
        {
            _retrieveInboxTimer.Stop ();
         
            Debug.WriteLine ( string.Format ( "{0} - Send request to retrieve Inbox", DateTime.Now.TimeOfDay.ToString () ) );

            IAsyncRequestDispatcher requestDispatcher = _dispatcherFactory.CreateAsyncRequestDispatcher ();

            requestDispatcher.Add (
                "INBOX",
                new GetImapFolderItemsRequest
                    {
                        FolderName = "INBOX", LastId = IncomingMail.Select ( im => im.Id ).OrderByDescending ( i => i ).FirstOrDefault ()
                    } );

            IsLoading = true;

            requestDispatcher.ProcessRequests ( GetInboxItemsCompleted, HandleRequestDispatcherException );
        }

        private void RequestSentItemsContent ( object sender, EventArgs e )
        {
            _retrieveSentTimer.Stop ();

            Debug.WriteLine(string.Format("{0} - Send request to retrieve Sent Items", DateTime.Now.TimeOfDay.ToString()));

            IAsyncRequestDispatcher requestDispatcher = _dispatcherFactory.CreateAsyncRequestDispatcher ();

            requestDispatcher.Add (
                "Sent Items",
                new GetImapFolderItemsRequest
                    {
                        FolderName = "Sent Items", LastId = SentMail.Select ( sm => sm.Id ).OrderByDescending ( i => i ).FirstOrDefault ()
                    } );

            IsLoading = true;
            requestDispatcher.ProcessRequests ( GetSentItemsCompleted, HandleRequestDispatcherException );
        }

        private void SetupImapFolderTimers ()
        {
            _retrieveInboxTimer = new DispatcherTimer { Interval = new TimeSpan ( 0, 0, 0, 300 ) };
            _retrieveInboxTimer.Tick += RequestInboxContent;

            _retrieveSentTimer = new DispatcherTimer { Interval = new TimeSpan ( 0, 0, 0, 300 ) };
            _retrieveSentTimer.Tick += RequestSentItemsContent;
        }

        private void HandleGetImapMailCompleted ( ReceivedResponses responses )
        {
            IsLoading = false;

            var mailResponse = responses.Get<GetImapMailResponse> ();

            var mail = mailResponse.Messsage;
            
            SelectedMail.HeadersOnly = false;
            SelectedMail.AttachmentName = mail.AttachmentName;
            SelectedMail.Message = mail.Message;
        }

        private void HandleGetImapMailException ( ExceptionInfo exceptionInfo )
        {
           IsLoading = false;
        }

        #endregion
    }
}
