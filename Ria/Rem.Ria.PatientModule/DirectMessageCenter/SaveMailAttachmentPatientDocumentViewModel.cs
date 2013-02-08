#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.PatientDashboard;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// View Model for saving mail attachment patient document.
    /// </summary>
    public class SaveMailAttachmentPatientDocumentViewModel : PanelEditorViewModel<MailAttachmentPatientDocumentDto>, IPopupTitleProvider
    {
        #region Constants and Fields

        private const string WorkspacesRegion = "WorkspacesRegion";
        private const string PatientWorkspaceView = "PatientWorkspaceView";
        private const string ExternalPatientHistoryView = "ExternalPatientHistoryView";
        private const string PatientEditorView = "PatientEditorView";

        private readonly INavigationService _navigationService;

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;

        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private PatientSearchResultDto _patient;
        private string _mailAttachmentName;
        private int _mailId;
        private string _mailFolderName;
        private string _mailFromName;
        private string _popupTitle;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveMailAttachmentPatientDocumentViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="popupService">The popup service.</param>
        public SaveMailAttachmentPatientDocumentViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IEventAggregator eventAggregator,
            INavigationService navigationService,
            IPopupService popupService )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _popupService = popupService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            CreatePatientImportDocumentCommand = commandFactoryHelper.BuildDelegateCommand (
                () => CreatePatientImportDocumentCommand, ExecuteCreatePatientImportDocumentCommand );

            var ruleExecutor = new NotifyPropertyChangedRuleExecutor<SaveMailAttachmentPatientDocumentViewModel, IDataTransferObject> ();
            ruleExecutor.AddRunAllRulesProperty ( vm => vm.EditingDto );
            ruleExecutor.WatchSubject ( this );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets the name of the mail attachment.
        /// </summary>
        /// <value>
        /// The name of the mail attachment.
        /// </value>
        public string MailAttachmentName
        {
            get { return _mailAttachmentName; }
            private set { ApplyPropertyChange ( ref _mailAttachmentName, () => MailAttachmentName, value ); }
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        public PatientSearchResultDto Patient
        {
            get { return _patient; }
            private set { ApplyPropertyChange ( ref _patient, () => Patient, value ); }
        }

        /// <summary>
        /// Gets the create patient import document command.
        /// </summary>
        public ICommand CreatePatientImportDocumentCommand { get; private set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string PopupTitle
        {
            get { return _popupTitle; }
            private set { ApplyPropertyChange ( ref _popupTitle, () => PopupTitle, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">
        /// The command factory.
        /// </param>
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
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            _mailId = parameters.GetValue<int> ( "MailId" );
            _mailFolderName = parameters.GetValue<string> ( "MailFolderName" );
            _mailFromName = parameters.GetValue<string> ( "MailFromName" );
            MailAttachmentName = parameters.GetValue<string> ( "AttachmentName" );

            EditingDto = GetNewMailAttachmentPatientDocumentDto ();

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new QueryPatientByDocumentRequest { MailAttachmentPatientDocument = EditingDto } );
            requestDispatcher.AddLookupValuesRequest ( "PatientGender" );
            requestDispatcher.AddLookupValuesRequest ( "StateProvince" );
            requestDispatcher.AddLookupValuesRequest ( "PatientPhoneType" );
            requestDispatcher.AddLookupValuesRequest ( "PatientAddressType" );
            requestDispatcher.AddLookupValuesRequest ( "PatientDocumentType" );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }

        /// <summary>
        /// Executes the save command.
        /// </summary>
        /// <param name="dto">The dto.</param>
        protected override void ExecuteSaveCommand ( KeyedDataTransferObject dto )
        {
            if ( EditingDto.Key == 0 )
            {
                // Save the Patient for future usage
                Patient = EditingDto.Patient;
            }

            base.ExecuteSaveCommand ( dto );
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;

            base.RequestCompleted ( receivedResponses );

            if ( EditingDto.HasErrors )
            {
                var errors = EditingDto.GetErrors ( null );

                var errorMessageSb = new StringBuilder ();

                foreach ( DataErrorInfo dataErrorInfo in errors )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                errors =
                    EditingDto.GetErrors ( PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object> ( p => p.PatientDocumentType ) );
                foreach ( DataErrorInfo dataErrorInfo in errors )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                errors =
                    EditingDto.GetErrors (
                        PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object> ( p => p.OtherDocumentTypeName ) );
                foreach ( DataErrorInfo dataErrorInfo in errors )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                errors = EditingDto.GetErrors ( PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object> ( p => p.Description ) );
                foreach ( DataErrorInfo dataErrorInfo in errors )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                errors =
                    EditingDto.GetErrors ( PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object> ( p => p.ClinicalStartDate ) );
                foreach ( DataErrorInfo dataErrorInfo in errors )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                errors = EditingDto.GetErrors (
                    PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object> ( p => p.ClinicalEndDate ) );
                foreach ( DataErrorInfo dataErrorInfo in errors )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                errors =
                    EditingDto.GetErrors (
                        PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object> ( p => p.DocumentProviderName ) );
                foreach ( DataErrorInfo dataErrorInfo in errors )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                var errorMessage = errorMessageSb.ToString ();

                if ( !string.IsNullOrWhiteSpace ( errorMessage ) )
                {
                    _userDialogService.ShowDialog ( errorMessage, "Save Mail Attachment to External Patient History", UserDialogServiceOptions.Ok );
                }
            }
            else
            {
                var patientDocumentUploadedEvent = new PatientDocumentUploadedEventArgs
                    {
                        PatientKey = EditingDto.PatientKey
                    };
                _eventAggregator.GetEvent<PatientDocumentUploadedEvent> ().Publish ( patientDocumentUploadedEvent );

                _navigationService.Navigate (
                    WorkspacesRegion,
                    PatientWorkspaceView,
                    "SubViewPassThrough",
                    new[]
                        {
                            new KeyValuePair<string, string> ( "PatientKey", Patient.Key.ToString () ),
                            new KeyValuePair<string, string> ( "FullName", Patient.FullName ),
                            new KeyValuePair<string, string> ( "SubViewName", ExternalPatientHistoryView )
                        } );

                CloseViewCommand.Execute ( null );
            }
        }

        /// <summary>
        /// Executes the close view command.
        /// </summary>
        protected override void ExecuteCloseViewCommand ()
        {
            base.ExecuteCloseViewCommand ();
            _popupService.ClosePopup ( "SaveMailAttachmentPatientDocumentView" );
        }

        private void ExecuteCreatePatientImportDocumentCommand ()
        {
            if ( !EditingDto.HasErrors ||
                 ( EditingDto.HasErrors
                   && EditingDto.DataErrorInfoCollection.Where ( p => p.DataErrorInfoType == DataErrorInfoType.PropertyLevel ).Count () == 0 ) )
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add (
                    new CreatePatientImportDocumentRequest
                        {
                            MailAttachmentPatientDocument = EditingDto,
                            PatientSearchResult = EditingDto.Patient,
                            AgencyKey = CurrentUserContext.Agency.Key
                        } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleSavePatientImportDocumentCompleted, HandleSavePatientImportDocumentException );
            }
            else
            {
                var errors =
                    EditingDto.DataErrorInfoCollection.Where ( p => p.ErrorLevel == ErrorLevel.Error ).Select ( p => p.ToString () ).Aggregate (
                        ( i, j ) => i + Environment.NewLine + j );
                _userDialogService.ShowDialog (
                    string.Format ( "Please fix the following errors before trying to save:{0}{1}", Environment.NewLine, errors ),
                    "Errors",
                    UserDialogServiceOptions.Ok );
            }
        }

        private MailAttachmentPatientDocumentDto GetNewMailAttachmentPatientDocumentDto ()
        {
            var dto = new MailAttachmentPatientDocumentDto { MailId = _mailId, MailFolderName = _mailFolderName };
            dto.ClinicalStartDate = DateTime.Today;
            dto.DocumentProviderName = _mailFromName;

            return dto;
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                            where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                            select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            LookupValueLists = lookupValueLists;
        }

        private void GetPatientSearchResultDtoCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<QueryPatientByDocumentResponse> ();

            EditingDto.Patient = response.PatientSearchResult;

            if ( EditingDto.Patient.Key > 0 )
            {
                PopupTitle = "Existing Patient - Save Attachment to External Patient History";
            }
            else
            {
                PopupTitle = "New Patient - Save Attachment to External Patient History";
            }
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            GetLookupValuesCompleted ( receivedResponses );
            SetDocumentType ();
            GetPatientSearchResultDtoCompleted ( receivedResponses );
        }

        private void SetDocumentType ()
        {
            if ( MailAttachmentName.ToLower ().EndsWith ( ".zip" ) )
            {
                EditingDto.PatientDocumentType =
                    LookupValueLists["PatientDocumentType"].Single ( p => p.WellKnownName == WellKnownNames.PatientModule.PatientDocumentType.XDM );
            }
            else if ( MailAttachmentName.ToLower ().EndsWith ( ".xml" ) )
            {
                EditingDto.PatientDocumentType =
                    LookupValueLists["PatientDocumentType"].Single ( p => p.WellKnownName == WellKnownNames.PatientModule.PatientDocumentType.C32 );
            }
            else
            {
                EditingDto.PatientDocumentType =
                    LookupValueLists["PatientDocumentType"].Single ( p => p.WellKnownName == WellKnownNames.PatientModule.PatientDocumentType.Other );
            }
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog (
                exceptionInfo.Message, "Save mail attachment patient document view initialization failed", UserDialogServiceOptions.Ok );
        }

        private void HandleSavePatientImportDocumentCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;

            var response = receivedResponses.Get<CreatePatientImportDocumentResponse> ();
            var patientProfile = response.PatientProfile;
            _navigationService.Navigate (
                WorkspacesRegion,
                PatientWorkspaceView,
                "SubViewPassThrough",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", patientProfile.Key.ToString () ),
                        new KeyValuePair<string, string> ( "FullName", patientProfile.FullName ),
                        new KeyValuePair<string, string> ( "SubViewName", PatientEditorView )
                    } );

            ExecuteCloseViewCommand ();
        }

        private void HandleSavePatientImportDocumentException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;

            _userDialogService.ShowDialog ( exceptionInfo.Message, "Save patient and import document failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
