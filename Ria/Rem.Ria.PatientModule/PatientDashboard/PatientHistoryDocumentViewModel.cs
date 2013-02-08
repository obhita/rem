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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames;
using Rem.WellKnownNames.PatientModule;
using Telerik.Windows.Controls;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for PatientHistoryDocument class.
    /// </summary>
    public class PatientHistoryDocumentViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserDialogService _userDialogService;
        private readonly IPopupService _popupService;
        private readonly INavigationService _navigationService;
        private ObservableCollection<LookupValueDto> _documentTypeDtos;
        private bool _isOtherTypeReadOnly;
        private PatientDocumentDto _selectedDocument;
        private ObservableCollection<PatientDocumentDto> _patientDocumentDtos;
        private long _patientKey;
        private MailAttachmentPatientDocumentDto _mailAttachmentPatientDocumentDto;

        #endregion

        #region Constructors and Destructors


        /// <summary>
        /// Initializes a new instance of the <see cref="PatientHistoryDocumentViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientHistoryDocumentViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IPopupService popupService,
            INavigationService navigationService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _popupService = popupService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<PatientDocumentUploadedEvent> ().Subscribe (
                PatientDocumentUploadedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                PatientDocumentUploadedEvents );
            IsOtherTypeReadOnly = true;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            OpenDocumentCommand = commandFactoryHelper.BuildDelegateCommand<PatientDocumentDto> (
                () => OpenDocumentCommand, ExecuteOpenDocumentCommand );
            OpenEncryptedDocumentCommand = commandFactoryHelper.BuildDelegateCommand<PatientDocumentDto> (
                () => OpenEncryptedDocumentCommand, ExecuteOpenEncryptedDocumentCommand );

            SaveNewDocumentCommand = commandFactoryHelper.BuildDelegateCommand<PatientDocumentDto>(
                () => SaveNewDocumentCommand, ExecuteSaveNewDocument);
            CancelNewDocumentCommand = commandFactoryHelper.BuildDelegateCommand<PatientDocumentDto>(
                () => CancelNewDocumentCommand, ExecuteCancelNewDocument);

            UpdateDocumentCommand = commandFactoryHelper.BuildDelegateCommand<PatientDocumentDto> (
                () => UpdateDocumentCommand, ExecuteUpdateDocument );
            DeleteDocumentCommand = commandFactoryHelper.BuildDelegateCommand<PatientDocumentDto> (
                () => DeleteDocumentCommand, ExecuteDeleteDocumentCommand, CanExecuteDeleteDocumentCommand );

            DocumentTypeChangedCommand = commandFactoryHelper.BuildDelegateCommand<LookupValueDto> (
                () => DocumentTypeChangedCommand, ExecuteDocumentTypeChangedCommand );
            ClinicalStartDateChangedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => ClinicalStartDateChangedCommand, ExecuteClinicalStartDateChangedCommand );

            ImportC32DocumentCommand = commandFactoryHelper.BuildDelegateCommand<PatientDocumentDto>(
               () => ImportC32DocumentCommand, ExecuteImportC32DocumentCommand, CanExecuteImportC32DocumnetCommand);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the clinical start date changed command.
        /// </summary>
        /// <value>The clinical start date changed command.</value>
        public ICommand ClinicalStartDateChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the delete document command.
        /// </summary>
        /// <value>The delete document command.</value>
        public DelegateCommand<PatientDocumentDto> DeleteDocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the document type changed command.
        /// </summary>
        /// <value>The document type changed command.</value>
        public ICommand DocumentTypeChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the import C32 document command.
        /// </summary>
        /// <value>
        /// The import C32 document command.
        /// </value>
        public DelegateCommand<PatientDocumentDto> ImportC32DocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the document type dtos.
        /// </summary>
        /// <value>The document type dtos.</value>
        public ObservableCollection<LookupValueDto> DocumentTypeDtos
        {
            get { return _documentTypeDtos; }
            set { ApplyPropertyChange ( ref _documentTypeDtos, () => DocumentTypeDtos, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is other type read only.
        /// </summary>
        /// <value><c>true</c> if this instance is other type read only; otherwise, <c>false</c>.</value>
        public bool IsOtherTypeReadOnly
        {
            get { return _isOtherTypeReadOnly; }
            set { ApplyPropertyChange ( ref _isOtherTypeReadOnly, () => IsOtherTypeReadOnly, value ); }
        }

        /// <summary>
        /// Gets or sets the open document command.
        /// </summary>
        /// <value>The open document command.</value>
        public ICommand OpenDocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the open encrypted document command.
        /// </summary>
        /// <value>The open encrypted document command.</value>
        public ICommand OpenEncryptedDocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the patient document dtos.
        /// </summary>
        /// <value>The patient document dtos.</value>
        public ObservableCollection<PatientDocumentDto> PatientDocumentDtos
        {
            get { return _patientDocumentDtos; }
            set 
            {
                if (_patientDocumentDtos != null)
                {
                    _patientDocumentDtos.CollectionChanged -= PatientDocumentDtosCollectionChanged;
                    _patientDocumentDtos.CollectionChanged += PatientDocumentDtosCollectionChanged;
                }

                ApplyPropertyChange ( ref _patientDocumentDtos, () => PatientDocumentDtos, value );

                if (_patientDocumentDtos != null)
                {
                    _patientDocumentDtos.CollectionChanged -= PatientDocumentDtosCollectionChanged;
                    _patientDocumentDtos.CollectionChanged += PatientDocumentDtosCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Gets or sets the save document edit command.
        /// </summary>
        /// <value>The save document edit command.</value>
        public ICommand UpdateDocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the save new document command.
        /// </summary>
        /// <value>
        /// The save new document command.
        /// </value>
        public ICommand SaveNewDocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the cancel new document command.
        /// </summary>
        /// <value>
        /// The cancel new document command.
        /// </value>
        public ICommand CancelNewDocumentCommand { get; set; }

        /// <summary>
        /// Gets or sets the selected document.
        /// </summary>
        /// <value>The selected document.</value>
        public PatientDocumentDto SelectedDocument
        {
            get { return _selectedDocument; }

            set { ApplyPropertyChange(ref _selectedDocument, () => SelectedDocument, value); }
        }

        /// <summary>
        /// Gets the can cancel beginning edit.
        /// </summary>
        public Func<bool> CanCancelBeginningEdit
        {
            get { return ( () => CanCancel () ); }
        }

        /// <summary>
        /// Gets the can cancel selection changing.
        /// </summary>
        public Func<bool> CanCancelSelectionChanging
        {
            get { return ( () => CanCancel () ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the clinical start date changed command.
        /// </summary>
        /// <param name="obj">The date that changed.</param>
        public void ExecuteClinicalStartDateChangedCommand ( object obj )
        {
            var date = ( RadDateTimePicker )obj;
            if ( SelectedDocument != null && (SelectedDocument.ClinicalEndDate == null || date.SelectedDate > SelectedDocument.ClinicalEndDate ))
            {
                SelectedDocument.ClinicalEndDate = date.SelectedDate;
            }
        }

        /// <summary>
        /// Executes the document type changed command.
        /// </summary>
        /// <param name="lookupValueDto">The lookup value dto.</param>
        public void ExecuteDocumentTypeChangedCommand ( LookupValueDto lookupValueDto )
        {
            if (lookupValueDto != null && lookupValueDto.WellKnownName == PatientDocumentType.Other)
            {
                IsOtherTypeReadOnly = false;
            }
            else
            {
                IsOtherTypeReadOnly = true;
                if (SelectedDocument != null)
                {
                    SelectedDocument.OtherDocumentTypeName = string.Empty;
                }
            }
        }

        /// <summary>
        /// Patients the document uploaded event handler.
        /// </summary>
        /// <param name="patientDocumentUploadedEventArgs">The <see cref="Rem.Ria.PatientModule.PatientDashboard.PatientDocumentUploadedEventArgs"/> instance containing the event data.</param>
        public void PatientDocumentUploadedEventHandler (
            PatientDocumentUploadedEventArgs patientDocumentUploadedEventArgs )
        {
            Deployment.Current.InvokeIfNeeded ( () => GetPatientDocumentsByPatientKeyAsync ( patientDocumentUploadedEventArgs.PatientKey ) );
        }

        /// <summary>
        /// Patients the document uploaded events.
        /// </summary>
        /// <param name="patientDocumentUploadedEventArgs">The <see cref="Rem.Ria.PatientModule.PatientDashboard.PatientDocumentUploadedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool PatientDocumentUploadedEvents ( PatientDocumentUploadedEventArgs patientDocumentUploadedEventArgs )
        {
            return _patientKey == patientDocumentUploadedEventArgs.PatientKey;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var patientKey = parameters.GetValue<long> ( "PatientKey" );
            return _patientKey == 0 || _patientKey == patientKey;
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
        protected override void NavigateToDefaultCommand(KeyValuePair<string, string>[] parameters)
        {
            _patientKey = parameters.GetValue<long>("PatientKey");
            var mailId = parameters.GetValue<int>("MailId");
            if (mailId != 0)
            {
                var mailFolderName = parameters.GetValue<string>("MailFolderName");
                var mailFromName = parameters.GetValue<string>("MailFromName");
                var attachmentName = parameters.GetValue<string> ( "AttachmentName" );

                _mailAttachmentPatientDocumentDto = new MailAttachmentPatientDocumentDto
                {
                    MailId = mailId,
                    MailFolderName = mailFolderName,
                    DocumentProviderName = mailFromName,
                    ClinicalStartDate = DateTime.Today,
                    PatientKey = _patientKey,
                    CreatedDate = DateTime.Today,
                    FileName = attachmentName,
                };
            }
            else
            {
                _mailAttachmentPatientDocumentDto = null;
            }

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetPatientDocumentsByPatientRequest { PatientKey = _patientKey });
            requestDispatcher.AddLookupValuesRequest("PatientDocumentType");
            IsLoading = true;
            requestDispatcher.ProcessRequests(HandleInitializationCompleted, HandleInitializationException);
        }

        private void ExecuteDeleteDocumentCommand ( PatientDocumentDto patientDocumentDto )
        {
            ActAfterCheckIfHavingUnsavedNewDocument (
                () =>
                    {
                        var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                        IsLoading = true;
                        requestDispatcher.Add ( new DeletePatientDocumentRequest { PatientDocumentKey = patientDocumentDto.Key } );
                        requestDispatcher.ProcessRequests ( HandleDeletePatientDocumentCompleted, HandleDeletePatientDocumentException );
                    }
                );
        }

        private bool CanExecuteDeleteDocumentCommand(PatientDocumentDto arg)
        {
            bool result = arg != null 
                          && !(arg.C32ImportedIndicator.HasValue && arg.C32ImportedIndicator.Value)
                          && _patientDocumentDtos.All(p => p.Key > 0);

            return result;
        }

        private void ExecuteImportC32DocumentCommand(PatientDocumentDto patientDocumentDto)
        {
            ActAfterCheckIfHavingUnsavedNewDocument (
                () => _popupService.ShowPopup (
                    "ImportC32View",
                    "Default",
                    "Import C32 Document",
                    new[]
                        {
                            new KeyValuePair<string, string> ( "PatientDocumentKey", patientDocumentDto.Key.ToString ( CultureInfo.InvariantCulture ) ),
                            new KeyValuePair<string, string> ( "PatientKey", patientDocumentDto.PatientKey.ToString ( CultureInfo.InvariantCulture ) )
                        },
                    true,
                    () => ExecuteGoToDashboard ( patientDocumentDto.PatientKey ) ) );
        }

        private void ExecuteGoToDashboard(long patientKey )
        {
            _navigationService.Navigate(
                   "WorkspacesRegion",
                   "PatientWorkspaceView",
                   "ViewPatient",
                   new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", patientKey.ToString () ),
                        new KeyValuePair<string, string> ( "SubViewName", "PatientDashboardView" ),
                    });
        }


        private bool CanExecuteImportC32DocumnetCommand(PatientDocumentDto arg)
        {
            bool result = arg != null
                          &&
                          ( arg.PatientDocumentType.WellKnownName == PatientDocumentType.C32
                            || arg.PatientDocumentType.WellKnownName == PatientDocumentType.XDM )
                          && !( arg.C32ImportedIndicator.HasValue && arg.C32ImportedIndicator.Value )
                          && _patientDocumentDtos.All ( p => p.Key > 0 );

            return result;
        }

        private void ExecuteOpenDocumentCommand ( PatientDocumentDto patientDocumentDto )
        {
            var relativePath = string.Format (
                "../{0}?{1}={2}&{3}={4}",
                HttpHandlerPaths.PatientModuleHttpHandlerPath,
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.RequestName ),
                HttpUtility.UrlEncode ( HttpHandlerRequestNames.DownloadPatientDocument ),
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.PatientDocumentKey ),
                patientDocumentDto.Key );
            var uri = new Uri ( Application.Current.Host.Source, relativePath );
            HtmlPage.Window.Navigate ( uri, "_blank" );
        }

        private void ExecuteOpenEncryptedDocumentCommand ( PatientDocumentDto patientDocumentDto )
        {
            var u = new Uri ( Application.Current.Host.Source, "../DownloadEncryptedDocument.ashx?DocumentKey=" + patientDocumentDto.Key );
            HtmlPage.Window.Navigate ( u, "_blank" );
        }

        private void ExecuteUpdateDocument ( PatientDocumentDto patientDocumentDto )
        {
            ActAfterCheckIfHavingUnsavedNewDocument (
                () =>
                    {
                        if ( patientDocumentDto.Key > 0 )
                        {
                            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

                            requestDispatcher.Add ( new SaveDtoRequest<PatientDocumentDto> { DataTransferObject = patientDocumentDto } );
                            IsLoading = true;
                            requestDispatcher.ProcessRequests ( HandleSavePatientDocumentCompleted, HandleSavePatientDocumentException );
                        }
                    }
                );
        }

        private void ExecuteSaveNewDocument(PatientDocumentDto patientDocumentDto)
        {
            if (patientDocumentDto.Key == 0)
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();

                if (patientDocumentDto is MailAttachmentPatientDocumentDto)
                {
                    requestDispatcher.Add(
                        new SaveDtoRequest<MailAttachmentPatientDocumentDto> { DataTransferObject = patientDocumentDto as MailAttachmentPatientDocumentDto });
                    IsLoading = true;
                    requestDispatcher.ProcessRequests(HandleSaveMailAttachmentPatientDocumentCompleted, HandleSavePatientDocumentException);
                }
                else
                {
                    requestDispatcher.Add(new SaveDtoRequest<PatientDocumentDto> { DataTransferObject = patientDocumentDto });
                    IsLoading = true;
                    requestDispatcher.ProcessRequests(HandleSavePatientDocumentCompleted, HandleSavePatientDocumentException);
                }
            }
        }

        private void ExecuteCancelNewDocument(PatientDocumentDto patientDocumentDto)
        {
            PatientDocumentDtos.Remove ( patientDocumentDto );
        }

        private void PatientDocumentDtosCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ImportC32DocumentCommand.RaiseCanExecuteChanged();
            DeleteDocumentCommand.RaiseCanExecuteChanged();
        }

        private void ActAfterCheckIfHavingUnsavedNewDocument(Action action)
        {
            if (_patientDocumentDtos.All(p => p.Key != 0))
            {
                action ();
            }
            else
            {
                _userDialogService.ShowDialog ( "You have unsaved new document. Please save or cancel it.", "Alert", UserDialogServiceOptions.Ok );
            }
        }

        private void GetPatientDocumentsByPatientKeyAsync(long patientKey)
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetPatientDocumentsByPatientRequest { PatientKey = patientKey });
            IsLoading = true;
            requestDispatcher.ProcessRequests(HandleGetPatientDocumentsByPatientKeyCompleted, HandleGetPatientDocumentsByPatientKeyException);
        }

        private bool CanCancel()
        {
            return (_patientDocumentDtos != null && _patientDocumentDtos.Any(p => p.Key == 0));
        }

        private void HandleDeletePatientDocumentCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<DeletePatientDocumentResponse> ();

            PatientDocumentDtos = new ObservableCollection<PatientDocumentDto> ( response.PatientDocumentDtos );
        }

        private void HandleDeletePatientDocumentException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Deleting Patient Document failed", UserDialogServiceOptions.Ok );
        }

        private void HandleInitializationCompleted(ReceivedResponses receivedResponses)
        {
            IsLoading = false;
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>>();

            var responses = from response in receivedResponses.Responses
                            where typeof(GetLookupValuesResponse).IsAssignableFrom(response.GetType())
                            select response;

            foreach (GetLookupValuesResponse response in responses)
            {
                lookupValueLists.Add(response.Name, response.LookupValues);
            }
            DocumentTypeDtos = new ObservableCollection<LookupValueDto>(lookupValueLists["PatientDocumentType"]);

            var patientDocumentResponse = receivedResponses.Get<GetPatientDocumentsByPatientResponse>();
            var results = new ObservableCollection<PatientDocumentDto>(patientDocumentResponse.PatientDocumentDtos);
            PatientDocumentDtos = results;
            if (_mailAttachmentPatientDocumentDto != null)
            {
                if ( _mailAttachmentPatientDocumentDto.FileName.ToLower ().EndsWith ( ".xml" ) )
                {
                    _mailAttachmentPatientDocumentDto.PatientDocumentType = _documentTypeDtos.First ( p => p.WellKnownName == PatientDocumentType.C32 );
                }
                else if ( _mailAttachmentPatientDocumentDto.FileName.ToLower ().EndsWith ( ".zip" ) )
                {
                    _mailAttachmentPatientDocumentDto.PatientDocumentType = _documentTypeDtos.First ( p => p.WellKnownName == PatientDocumentType.XDM );
                }
                else
                {
                    _mailAttachmentPatientDocumentDto.PatientDocumentType = _documentTypeDtos.First ( p => p.WellKnownName == PatientDocumentType.Other );
                }
                PatientDocumentDtos.Add ( _mailAttachmentPatientDocumentDto );
            }
        }

        private void HandleInitializationException(ExceptionInfo ex)
        {
            IsLoading = false;
            _userDialogService.ShowDialog(ex.Message, "Initializing patient document failed", UserDialogServiceOptions.Ok);
        }

        private void HandleGetPatientDocumentsByPatientKeyCompleted(ReceivedResponses receivedResponses)
        {
            IsLoading = false;
            var response = receivedResponses.Get<GetPatientDocumentsByPatientResponse>();
            var results = new ObservableCollection<PatientDocumentDto>(response.PatientDocumentDtos);

            PatientDocumentDtos = results;
        }

        private void HandleGetPatientDocumentsByPatientKeyException(ExceptionInfo ex)
        {
            IsLoading = false;
            _userDialogService.ShowDialog(ex.Message, "Getting Patient Documents by Patient Key failed", UserDialogServiceOptions.Ok);
        }

        private void HandleSavePatientDocumentCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<DtoResponse<PatientDocumentDto>> ();

            var dto = response.DataTransferObject;

            if (_patientDocumentDtos.SingleOrDefault(p => p.Key == 0) != null)
            {
                _patientDocumentDtos.Remove ( _patientDocumentDtos.Single ( p => p.Key == 0 ) );
                _patientDocumentDtos.Add(dto);
            }
            SelectedDocument = dto;
            HandleErrors(dto);
        }

        private void HandleSaveMailAttachmentPatientDocumentCompleted(ReceivedResponses receivedResponses)
        {
            IsLoading = false;
            var response = receivedResponses.Get<DtoResponse<MailAttachmentPatientDocumentDto>>();

            var dto = response.DataTransferObject;

            if (_patientDocumentDtos.SingleOrDefault(p => p.Key == 0) != null)
            {
                _patientDocumentDtos.Remove(_patientDocumentDtos.Single(p => p.Key == 0));
                _patientDocumentDtos.Add(dto);
            }
            SelectedDocument = dto;
            HandleErrors(dto);
        }

        private void HandleErrors(PatientDocumentDto patientDocumentDto)
        {
            if (patientDocumentDto.HasErrors)
            {
                var errors = patientDocumentDto.GetErrors(null);

                var errorMessageSb = new StringBuilder();

                foreach (DataErrorInfo dataErrorInfo in errors)
                {
                    errorMessageSb.AppendLine(dataErrorInfo.ToString());
                }

                errors = patientDocumentDto.GetErrors("ClinicalStartDate");

                foreach (DataErrorInfo dataErrorInfo in errors)
                {
                    errorMessageSb.AppendLine(dataErrorInfo.ToString());
                }

                errors = patientDocumentDto.GetErrors("DocumentProviderName");

                foreach (DataErrorInfo dataErrorInfo in errors)
                {
                    errorMessageSb.AppendLine(dataErrorInfo.ToString());
                }

                errors = patientDocumentDto.GetErrors("OtherDocumentTypeName");

                foreach (DataErrorInfo dataErrorInfo in errors)
                {
                    errorMessageSb.AppendLine(dataErrorInfo.ToString());
                }

                var errorMessage = errorMessageSb.ToString();

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    _userDialogService.ShowDialog(errorMessage, "Save Patient Document", UserDialogServiceOptions.Ok);
                }
            }
        }

        private void HandleSavePatientDocumentException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Saving Patient Document failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
