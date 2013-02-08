#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// View Model for Send New Mail view.
    /// </summary>
    public class SendNewMailViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        
        private CurrentUserContext _currentContext;

        private DirectMailDto _mail;

        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SendNewMailViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="currentUserContextService">The current user context service.</param>
        public SendNewMailViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IEventAggregator eventAggregator,
            IPopupService popupService,
            ICurrentUserContextService currentUserContextService )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _eventAggregator = eventAggregator;
            _popupService = popupService;

            currentUserContextService.RegisterForContext ( ( cuc, b ) => _currentContext = cuc );

            SelectedFiles = new ObservableCollection<FileInfo> ();

            // UI Commands 
            SendNewMailCommand = CommandFactoryHelper
                .CreateHelper ( this, commandFactory )
                .BuildDelegateCommand(() => SendNewMailCommand, ExecuteSendNewMail);

            OpenAddressBookCommand = CommandFactoryHelper
                .CreateHelper ( this, commandFactory )
                .BuildDelegateCommand ( () => OpenAddressBookCommand, ExecuteOpenAddressBook );

            AddFilesCommand = CommandFactoryHelper
                .CreateHelper(this, commandFactory).BuildDelegateCommand<IEnumerable<FileInfo>>(() => AddFilesCommand, ExecuteAddFiles);

            // Navigation Commands 
            OpenSendNewMailViewCommand = NavigationCommandManager.BuildCommand ( () => OpenSendNewMailViewCommand, NavigateToOpenSendNewMailViewCommand );

            _eventAggregator.GetEvent<DirectAddressRecipientSelectedEvent> ().Subscribe ( HandleDirectAddressRecipientSelectedEventHandler );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        /// <value>The mail.</value>
        public DirectMailDto Mail
        {
            get { return _mail; }
            set { ApplyPropertyChange ( ref _mail, () => Mail, value ); }
        }

        /// <summary>
        /// Gets the selected files.
        /// </summary>
        public ObservableCollection<FileInfo> SelectedFiles { get; private set; }

        /// <summary>
        /// Gets or sets the open address book command.
        /// </summary>
        /// <value>
        /// The open address book command.
        /// </value>
        public ICommand OpenAddressBookCommand { get; set; }

        /// <summary>
        /// Gets the send C32 command.
        /// </summary>
        public ICommand SendNewMailCommand { get; private set; }

        /// <summary>
        /// Gets the add files command.
        /// </summary>
        public ICommand AddFilesCommand { get; private set; }

        /// <summary>
        /// Gets the open send new mail view command.
        /// </summary>
        public INavigationCommand OpenSendNewMailViewCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the direct address recipient selected event handler.
        /// </summary>
        /// <param name="recipientSelected">The recipient selected.</param>
        public void HandleDirectAddressRecipientSelectedEventHandler ( DirectAddressRecipientSelectedArgs recipientSelected )
        {
            Mail.To = recipientSelected.Address;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
        protected override ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory )
        {
            return CommandFactoryHelper.CreateHelper ( this, commandFactory );
        }

        private void ExecuteOpenAddressBook ()
        {
            _popupService.ShowPopup ( "HealthProvidersDirectoryView", null, "HPD" );
        }

        private void ExecuteSendNewMail ()
        {
            if ( _patientKey > 0 )
            {
                //TODO: Handle This correctly

                IAsyncRequestDispatcher requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add (
                    new SendC32Request
                        {
                            StaffKey = _currentContext.Staff.Key,
                            PatientKey = _patientKey,
                            ToDirectEmail = Mail.To,
                            Subject = Mail.Subject,
                            Body = Mail.Message
                        } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( SendC32RequestDispatcherCompleted, HandleRequestDispatcherException );
            }
            else
            {
                byte[] attachmentData = null;
                string attachmentFileName = null;

                if (SelectedFiles.Count > 0)
                {
                    var selectedFile = SelectedFiles[0];
                    Stream stream = selectedFile.OpenRead();
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    stream.Dispose();
                    stream.Close();

                    attachmentData = buffer;
                    attachmentFileName = selectedFile.Name;
                }

                IAsyncRequestDispatcher requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add (
                    new SendDirectMessageRequest
                        {
                            StaffKey = _currentContext.Staff.Key,
                            ToDirectEmail = Mail.To,
                            Subject = Mail.Subject,
                            Body = Mail.Message,
                            AttachmentData = attachmentData,
                            AttachmentFileName = attachmentFileName
                        } );

                IsLoading = true;
                requestDispatcher.ProcessRequests ( SendC32RequestDispatcherCompleted, HandleRequestDispatcherException );
            }
        }

        private void ExecuteAddFiles(IEnumerable<FileInfo> files)
        {
            files = files.ToList ();

            if (files.Count() > 1)
            {
                _userDialogService.ShowDialog(
                    "You can only add one file.",
                    "Error adding file",
                    UserDialogServiceOptions.Ok);
            }
            else if (!files.Any())
            {
                _userDialogService.ShowDialog(
                    "You must add at least one file.",
                    "Error adding file",
                    UserDialogServiceOptions.Ok);
            }
            else
            {
                var fileInfo = files.ElementAt(0);
                if (string.IsNullOrEmpty(fileInfo.Extension))
                {
                    _userDialogService.ShowDialog(
                        "You cannot add a directory.",
                        "Error adding file",
                        UserDialogServiceOptions.Ok);
                }
                else
                {
                    SelectedFiles.Clear();
                    SelectedFiles.Add(fileInfo);
                }
            }
        }

        private void FireEventAndClose ()
        {
            _eventAggregator.GetEvent<MessageSentEvent> ().Publish ( new MessageSentEventArgs { Mail = Mail } );
            _popupService.ClosePopup ( "SendNewMailView" );
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Sending C32 failed.", UserDialogServiceOptions.Ok );
        }

        private void NavigateToOpenSendNewMailViewCommand ( KeyValuePair<string, string>[] parameters )
        {
            var mail = new DirectMailDto { From = _currentContext.Staff.DirectEmailAddress, FromName = _currentContext.Staff.FullName, Sent = DateTime.Now };


            _patientKey = parameters.GetValue<long> ( "PatientKey" );
            var patientName = parameters.GetValue<string> ( "PatientFullName" );
            if ( !string.IsNullOrEmpty ( patientName ) )
            {
                mail.AttachmentName = string.Format ( "{0}.xml, Preview.html", DateTime.Now.Ticks );
            }

            Mail = mail;
        }

        private void SendC32RequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            FireEventAndClose ();
        }

        #endregion
    }
}
