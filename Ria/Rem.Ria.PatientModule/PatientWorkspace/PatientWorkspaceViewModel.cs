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
using System.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.PatientDashboard;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;
using Rem.Ria.PatientModule.Web.PatientEditor;
using Rem.Ria.PatientModule.Web.PatientSearch;
using Rem.WellKnownNames;
using Rem.WellKnownNames.PatientModule;
using Telerik.Windows.Controls.DragDrop;

namespace Rem.Ria.PatientModule.PatientWorkspace
{
    /// <summary>
    /// View Model for PatientWorkspace class.
    /// </summary>
    public class PatientWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private const string ExternalPatientHistoryView = "ExternalPatientHistoryView";
        private const string PatientCellPhoneTypeWellKnownName = "C";
        private const string PatientDashboardView = "PatientDashboardView";
        private const string PatientEditorView = "PatientEditorView";
        private const string PatientHomeAddressTypeWellKnownName = "H";

        private readonly IAccessControlManager _accessControlManager;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly INotificationService _notificationService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private PatientDto _patientDto;
        private string _patientFullName;
        private bool _patientInitialized;
        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientWorkspaceViewModel (
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            INotificationService notificationService,
            IPopupService popupService,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _accessControlManager = accessControlManager;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _notificationService = notificationService;
            _popupService = popupService;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ViewExternalPatientHistoryCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => ViewExternalPatientHistoryCommand, ExecuteViewExternalPatientHistory );
            GoToDashboardCommand = commandFactoryHelper.BuildDelegateCommand ( () => GoToDashboardCommand, ExecuteGoToDashboard );
            EditPatientCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => EditPatientCommand, ExecuteEditPatient );
            DownloadC32Command = commandFactoryHelper.BuildDelegateCommand ( () => DownloadC32Command, ExecuteDownloadC32 );
            ViewC32Command = commandFactoryHelper.BuildDelegateCommand ( () => ViewC32Command, ExecuteViewC32 );
            SendC32Command = commandFactoryHelper.BuildDelegateCommand ( () => SendC32Command, ExecuteSendC32 );
            DownloadGreenC32Command = commandFactoryHelper.BuildDelegateCommand ( () => DownloadGreenC32Command, ExecuteDownloadGreenC32 );
            ExportC32ToPopHealthCommand = commandFactoryHelper.BuildDelegateCommand ( () => ExportC32ToPopHealthCommand, ExecuteExportC32ToPopHealth );
            GoToPopHealthCommand = commandFactoryHelper.BuildDelegateCommand ( () => GoToPopHealthCommand, ExecuteGoToPopHealthCommand );
            ShowAlertDetailsCommand = commandFactoryHelper.BuildDelegateCommand<PatientAlertDto> (
                () => ShowAlertDetailsCommand, ExecuteShowAlertDetailsCommand );

            _eventAggregator.GetEvent<PatientChangedEvent> ().Subscribe (
                PatientChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterPatientChangedEvents );

            CreateAppointmentCommand = NavigationCommandManager.BuildCommand (
                () => CreateAppointmentCommand, NavigateToCreateAppoitmentCommand, CanNavigateToDefaultCommand );
            SubViewPassThroughCommand = NavigationCommandManager.BuildCommand (
                () => SubViewPassThroughCommand, NavigateToSubViewPassThroughCommand, CanNavigateToDefaultCommand );

            DropQueryCommand = commandFactoryHelper.BuildDelegateCommand<DragDropQueryEventArgs>(() => DropQueryCommand, ExecuteDropQueryCommand);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the active allergies.
        /// </summary>
        public ObservableCollection<AllergyDto> ActiveAllergies
        {
            get
            {
                var activeAllergies = new ObservableCollection<AllergyDto> ();

                if (PatientDto != null && PatientDto.PatientAllergies.Allergies != null && PatientDto.PatientImportedAllergies.Allergies != null)
                {
                    var list =
                        PatientDto.PatientAllergies.Allergies.Where ( a => a.AllergyStatus.Name == "Active" ).Concat (
                            PatientDto.PatientImportedAllergies.Allergies.Where ( a => a.AllergyStatus.Name == "Active" ) );
                    activeAllergies = new ObservableCollection<AllergyDto> ( list );
                }

                return activeAllergies;
            }
        }

        /// <summary>
        /// Gets the create appointment command.
        /// </summary>
        public INavigationCommand CreateAppointmentCommand { get; private set; }

        /// <summary>
        /// Gets the download C32 command.
        /// </summary>
        public ICommand DownloadC32Command { get; private set; }

        /// <summary>
        /// Gets the download green C32 command.
        /// </summary>
        public ICommand DownloadGreenC32Command { get; private set; }

        /// <summary>
        /// Gets the edit patient command.
        /// </summary>
        public ICommand EditPatientCommand { get; private set; }

        /// <summary>
        /// Gets the export C32 to pop health command.
        /// </summary>
        public ICommand ExportC32ToPopHealthCommand { get; private set; }

        /// <summary>
        /// Gets the go to dashboard command.
        /// </summary>
        public ICommand GoToDashboardCommand { get; private set; }

        /// <summary>
        /// Gets the go to pop health command.
        /// </summary>
        public ICommand GoToPopHealthCommand { get; private set; }

        /// <summary>
        /// Gets the drop query command.
        /// </summary>
        public ICommand DropQueryCommand { get; private set; }

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get
            {
                if ( PatientDto != null )
                {
                    return PatientDto.DisplayName;
                }
                return _patientFullName;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is active flag.
        /// </summary>
        public bool IsActiveFlag
        {
            get
            {
                var isActive = ( PatientDto != null && PatientDto.PatientAllergies.Allergies != null
                                 && PatientDto.PatientImportedAllergies.Allergies != null &&
                                 ( PatientDto.PatientAllergies.Allergies.Any ( a => a.AllergyStatus.WellKnownName == AllergyStatus.Active )
                                   || PatientDto.PatientImportedAllergies.Allergies.Any ( a => a.AllergyStatus.WellKnownName == AllergyStatus.Active ) ) )
                               || ( PatientDto != null && PatientDto.Alerts != null && PatientDto.Alerts.Count > 0 );

                return isActive;
            }
        }

        /// <summary>
        /// Gets or sets the patient dto.
        /// </summary>
        /// <value>The patient dto.</value>
        public PatientDto PatientDto
        {
            get { return _patientDto; }
            set
            {
                _patientDto = value;
                RaisePropertyChanged ( () => PatientDto );
            }
        }

        /// <summary>
        /// Gets the patient home address.
        /// </summary>
        public PatientAddressDto PatientHomeAddress
        {
            get
            {
                if ( PatientDto == null )
                {
                    return null;
                }

                var address =
                    PatientDto.PatientAddresses.Addresses.FirstOrDefault (
                        p =>
                        p.PatientAddressType.WellKnownName == PatientHomeAddressTypeWellKnownName ) ?? 
                        PatientDto.PatientAddresses.Addresses.FirstOrDefault ();

                return address;
            }
        }

        /// <summary>
        /// Gets the patient phone.
        /// </summary>
        public PatientPhoneDto PatientPhone
        {
            get
            {
                if ( PatientDto == null )
                {
                    return null;
                }

                var phoneNumber =
                    PatientDto.PatientPhoneNumbers.PhoneNumbers.FirstOrDefault (
                        p =>
                        p.PatientPhoneType.WellKnownName == PatientCellPhoneTypeWellKnownName ) ??
                    PatientDto.PatientPhoneNumbers.PhoneNumbers.FirstOrDefault ();

                return phoneNumber;
            }
        }

        /// <summary>
        /// Gets the send C32 command.
        /// </summary>
        public ICommand SendC32Command { get; private set; }

        /// <summary>
        /// Gets the show alert details command.
        /// </summary>
        public ICommand ShowAlertDetailsCommand { get; private set; }

        /// <summary>
        /// Gets the sub view pass through command.
        /// </summary>
        public INavigationCommand SubViewPassThroughCommand { get; private set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.PatientRecord; }
        }

        /// <summary>
        /// Gets the view C32 command.
        /// </summary>
        public ICommand ViewC32Command { get; private set; }

        /// <summary>
        /// Gets the view external patient history command.
        /// </summary>
        public ICommand ViewExternalPatientHistoryCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the patient changed events.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.PatientChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterPatientChangedEvents ( PatientChangedEventArgs obj )
        {
            return PatientDto != null && PatientDto.Key == obj.PatientKey;
        }

        /// <summary>
        /// Patients the changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.PatientChangedEventArgs"/> instance containing the event data.</param>
        public void PatientChangedEventHandler ( PatientChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded ( () => LoadPatient ( PatientDto.Key ) );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Actives the changed.
        /// </summary>
        protected override void ActiveChanged ()
        {
            if ( IsActive )
            {
                _navigationService.Navigate ( "HeaderControlRegion", "MainPatientSearchView" );
            }
            base.ActiveChanged ();
        }

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "PatientKey" );
            var isTarget = _patientKey == key;

            return isTarget;
        }

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
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
            _patientKey = parameters.GetValue<long> ( "PatientKey" );
            _patientFullName = parameters.GetValue<string> ( "FullName" );
            var subViewName = parameters.GetValue<string> ( "SubViewName" );

            // Default to the Patient Dashboard view if a subview is not provided in navigation context.
            if ( string.IsNullOrWhiteSpace ( subViewName ) )
            {
                subViewName = PatientDashboardView;
            }

            // Get patient and cases.
            LoadPatient ( _patientKey );

            // Navigate to the Patient Editor sub view.
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                subViewName,
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ),
                        new KeyValuePair<string, string> ( "FullName", _patientFullName ?? string.Empty )
                    } );
        }

        private void ExecuteDownloadC32 ()
        {
            var relativePath = string.Format (
                "../{0}?{1}={2}&{3}={4}",
                HttpHandlerPaths.PatientModuleHttpHandlerPath,
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.RequestName ),
                HttpUtility.UrlEncode ( HttpHandlerRequestNames.DownloadC32Document ),
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.PatientKey ),
                _patientKey );
            var uri = new Uri ( Application.Current.Host.Source, relativePath );
            HtmlPage.Window.Navigate ( uri, "_blank" );
        }

        private void ExecuteDownloadGreenC32 ()
        {
            var relativePath = string.Format (
                "../{0}?{1}={2}&{3}={4}",
                HttpHandlerPaths.PatientModuleHttpHandlerPath,
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.RequestName ),
                HttpUtility.UrlEncode ( HttpHandlerRequestNames.DownloadGreenC32Document ),
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.PatientKey ),
                _patientKey );
            var uri = new Uri ( Application.Current.Host.Source, relativePath );
            HtmlPage.Window.Navigate ( uri, "_blank" );
        }

        private void ExecuteEditPatient ( object obj )
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                PatientEditorView,
                null,
                new[] { new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ) } );
        }

        private void ExecuteExportC32ToPopHealth ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new PostC32ToPopHealtheRequest { PatientKey = _patientKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( PostC32ToPopHealtheRequestRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void ExecuteGoToDashboard ()
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                PatientDashboardView,
                null,
                new[] { new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ) } );
        }

        private void ExecuteGoToPopHealthCommand ()
        {
            HtmlPage.Window.Navigate ( new Uri ( @"https://pophealth-demo.feisystems.com/" ), "__blank" );
        }

        private void ExecuteSendC32 ()
        {
            _popupService.ShowPopup (
                "SendC32View",
                "OpenSendC32View",
                "Send C32",
                new[] { new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ) } );
        }

        private void ExecuteShowAlertDetailsCommand ( PatientAlertDto patientAlertDto )
        {
            var uriQuery = new UriQuery
                {
                    { "Key", patientAlertDto.Key.ToString () },
                    { "Name", patientAlertDto.Name },
                    { "Note", patientAlertDto.Note },
                    { "CdsIdentifier", patientAlertDto.CdsIdentifier }
                };
            var uri = new Uri ( "CdsAlertView" + uriQuery, UriKind.Relative );
            _notificationService.ShowNotificationPopup ( uri );
        }

        private void ExecuteViewC32 ()
        {
            var relativePath = string.Format (
                "../{0}?{1}={2}&{3}={4}",
                HttpHandlerPaths.PatientModuleHttpHandlerPath,
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.RequestName ),
                HttpUtility.UrlEncode ( HttpHandlerRequestNames.ViewC32Document ),
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.PatientKey ),
                _patientKey );
            var uri = new Uri ( Application.Current.Host.Source, relativePath );
            HtmlPage.Window.Navigate ( uri, "_blank" );
        }

        private void ExecuteViewExternalPatientHistory ( object obj )
        {
            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                ExternalPatientHistoryView,
                null,
                new[] { new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ) } );
        }

        private void GetPatientRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPatientByKeyResponse> ();
            PatientDto = response.PatientDto;
            RaisePropertyChanged ( () => PatientHomeAddress );
            RaisePropertyChanged ( () => PatientPhone );
            RaisePropertyChanged ( () => IsActiveFlag );
            RaisePropertyChanged ( () => ActiveAllergies );
            RaisePropertyChanged ( () => HeaderContext );
            IsLoading = false;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Patient Workspace operation failed.", UserDialogServiceOptions.Ok );
        }

        private void LoadPatient ( long patientKey )
        {
            _patientInitialized = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetPatientByKeyRequest { Key = patientKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( GetPatientRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void NavigateToCreateAppoitmentCommand ( KeyValuePair<string, string>[] parameters )
        {
            //TODO: change how appointments are scheduled to use a service
            _navigationService.Navigate ( "WorkspacesRegion", "FrontDeskDashboardView", null, parameters );
        }

        private void NavigateToSubViewPassThroughCommand ( KeyValuePair<string, string>[] parameters )
        {
            if ( !_patientInitialized )
            {
                _patientKey = parameters.GetValue<long> ( "PatientKey" );
                LoadPatient ( _patientKey );
            }

            var subViewName = parameters.GetValue<string> ( "SubViewName" );

            // Default to the Patient Dashboard view if a subview is not provided in navigation context.
            if ( string.IsNullOrWhiteSpace ( subViewName ) )
            {
                subViewName = PatientDashboardView;
            }

            _navigationService.Navigate ( RegionManager, "WorkspaceContentScopedRegion", subViewName, null, parameters );
        }

        private void PostC32ToPopHealtheRequestRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<PostC32ToPopHealthResponse> ();
            _userDialogService.ShowDialog (
                string.Format ( "Message from PopHealth:\n{0}", response.Message ), "Export C32 to PopHealth", UserDialogServiceOptions.Ok );
        }

        private void ExecuteDropQueryCommand(DragDropQueryEventArgs obj)
        {
            var directMail = obj.Options.Payload as DirectMailDto;

            if (directMail == null)
            {
                return;
            }

            _navigationService.Navigate (
                RegionManager,
                "WorkspaceContentScopedRegion",
                ExternalPatientHistoryView,
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ),
                        new KeyValuePair<string, string> ( "MailId", directMail.Id.ToString () ),
                        new KeyValuePair<string, string> ( "MailFolderName", directMail.FolderName ),
                        new KeyValuePair<string, string> ("AttachmentName", directMail.AttachmentName), 
                        new KeyValuePair<string, string> ( "MailFromName", string.IsNullOrWhiteSpace ( directMail.FromName ) ? directMail.From : directMail.FromName ),
                    } );
        }

        #endregion
    }
}
