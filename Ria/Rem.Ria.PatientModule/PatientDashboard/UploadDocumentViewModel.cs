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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
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
using Telerik.Windows.Controls;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for UploadDocument class.
    /// </summary>
    public class UploadDocumentViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private static readonly long MaxFileSize = 4000000;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<FileInfo> _selectedFiles;
        private readonly IUserDialogService _userDialogService;
        private ObservableCollection<LookupValueDto> _documentTypeDtos;
        private bool _isEncryptedDocument;
        private bool _isOtherTypeReadOnly;
        private PatientDocumentDto _patientDocumentDto;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadDocumentViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="commandFactory">The command factory.</param>
        public UploadDocumentViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _selectedFiles = new ObservableCollection<FileInfo> ();
            SelectedFiles = new ReadOnlyObservableCollection<FileInfo> ( _selectedFiles );
            IsOtherTypeReadOnly = true;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            UploadDocumentCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => UploadDocumentCommand, ExecuteUploadFileCommand );
            AddFilesCommand = commandFactoryHelper.BuildDelegateCommand<IEnumerable<FileInfo>> ( () => AddFilesCommand, ExecuteAddFilesCommand );
            DocumentTypeChangedCommand = commandFactoryHelper.BuildDelegateCommand<LookupValueDto> (
                () => DocumentTypeChangedCommand, ExecuteDocumentTypeChangedCommand );
            ClinicalStartDateChangedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => ClinicalStartDateChangedCommand, ExecuteClinicalStartDateChangedCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the add files command.
        /// </summary>
        public ICommand AddFilesCommand { get; private set; }

        /// <summary>
        /// Gets or sets the clinical start date changed command.
        /// </summary>
        /// <value>The clinical start date changed command.</value>
        public ICommand ClinicalStartDateChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the document type changed command.
        /// </summary>
        /// <value>The document type changed command.</value>
        public ICommand DocumentTypeChangedCommand { get; set; }

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
        /// Gets or sets a value indicating whether this instance is encrypted document.
        /// </summary>
        /// <value><c>true</c> if this instance is encrypted document; otherwise, <c>false</c>.</value>
        public bool IsEncryptedDocument
        {
            get { return _isEncryptedDocument; }
            set { ApplyPropertyChange ( ref _isEncryptedDocument, () => IsEncryptedDocument, value ); }
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
        /// Gets or sets the patient document dto.
        /// </summary>
        /// <value>The patient document dto.</value>
        public PatientDocumentDto PatientDocumentDto
        {
            get { return _patientDocumentDto; }
            set { ApplyPropertyChange ( ref _patientDocumentDto, () => PatientDocumentDto, value ); }
        }

        /// <summary>
        /// Gets the selected files.
        /// </summary>
        public ReadOnlyObservableCollection<FileInfo> SelectedFiles { get; private set; }

        /// <summary>
        /// Gets the upload document command.
        /// </summary>
        public ICommand UploadDocumentCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the clinical start date changed command.
        /// </summary>
        /// <param name="obj">The changed date.</param>
        public void ExecuteClinicalStartDateChangedCommand ( object obj )
        {
            var date = ( RadDateTimePicker )obj;
            if ( PatientDocumentDto.ClinicalEndDate == null || date.SelectedDate > PatientDocumentDto.ClinicalEndDate )
            {
                PatientDocumentDto.ClinicalEndDate = date.SelectedDate;
            }
        }

        /// <summary>
        /// Executes the document type changed command.
        /// </summary>
        /// <param name="lookupValueDto">The lookup value dto.</param>
        public void ExecuteDocumentTypeChangedCommand ( LookupValueDto lookupValueDto )
        {
            if ( lookupValueDto.WellKnownName.Equals ( WellKnownNames.PatientModule.PatientDocumentType.Other ) )
            {
                IsOtherTypeReadOnly = false;
            }
            else
            {
                IsOtherTypeReadOnly = true;
                PatientDocumentDto.OtherDocumentTypeName = string.Empty;
            }
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

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            PatientDocumentDto = new PatientDocumentDto ();
            PatientDocumentDto.PatientKey = parameters.GetValue<long> ( "PatientKey" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.AddLookupValuesRequest ( "PatientDocumentType" );
            requestDispatcher.ProcessRequests ( HandleGetPatientDocumentTypeCompleted, HandleGetPatientDocumentTypeException );
        }

        private void ExecuteAddFilesCommand ( IEnumerable<FileInfo> files )
        {
            if ( files.Count () > 1 )
            {
                _userDialogService.ShowDialog (
                    "You can only add one file.",
                    "Error adding file",
                    UserDialogServiceOptions.Ok );
            }
            else if ( files.Count () < 1 )
            {
                _userDialogService.ShowDialog (
                    "You must add at least one file.",
                    "Error adding file",
                    UserDialogServiceOptions.Ok );
            }
            else
            {
                var fileInfo = files.ElementAt ( 0 );
                if ( string.IsNullOrEmpty ( fileInfo.Extension ) )
                {
                    _userDialogService.ShowDialog (
                        "You cannot add a directory.",
                        "Error adding file",
                        UserDialogServiceOptions.Ok );
                }
                else
                {
                    _selectedFiles.Clear ();
                    _selectedFiles.Add ( fileInfo );
                }
            }
        }

        private void ExecuteUploadFileCommand ( object obj )
        {
            if ( SelectedFiles.Count () == 0 )
            {
                _userDialogService.ShowDialog ( "There are no files to upload. Please select a file.", "File Upload", UserDialogServiceOptions.Ok );
            }

            IsLoading = true;

            foreach ( var selectedFile in SelectedFiles )
            {
                if ( selectedFile.Length > MaxFileSize )
                {
                    _userDialogService.ShowDialog ( "The file greater than 4MB cannot be uploaded.", "File Upload", UserDialogServiceOptions.Ok );
                }
                else
                {
                    try
                    {
                        Stream stream = selectedFile.OpenRead ();
                        var buffer = new byte[stream.Length];
                        stream.Read ( buffer, 0, ( int )stream.Length );
                        stream.Dispose ();
                        stream.Close ();

                        PatientDocumentDto.Document = buffer;
                        PatientDocumentDto.FileName = selectedFile.Name;

                        if ( IsEncryptedDocument )
                        {
                            PatientDocumentDto.IsEncrypted = true;
                        }
                        SavePatientDocumentAsync ( PatientDocumentDto );
                    }
                    catch ( IOException e )
                    {
                        _userDialogService.ShowDialog ( e.Message, "Upload File", UserDialogServiceOptions.Ok );
                    }
                }
            }

            IsLoading = false;
        }

        private void HandleGetPatientDocumentTypeCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            DocumentTypeDtos = new ObservableCollection<LookupValueDto> ( lookupValueLists["PatientDocumentType"] );
        }

        private void HandleGetPatientDocumentTypeException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Getting Patient Document Type failed", UserDialogServiceOptions.Ok );
        }

        private void HandleSavePatientDocumenException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Saving Patient Document failed", UserDialogServiceOptions.Ok );
        }

        private void HandleSavePatientDocumentCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<DtoResponse<PatientDocumentDto>> ();
            var results = response.DataTransferObject;

            if ( results.HasErrors )
            {
                var errorMessageSb = new StringBuilder ();

                foreach ( var dataErrorInfo in results.DataErrorInfoCollection )
                {
                    errorMessageSb.AppendLine ( dataErrorInfo.ToString () );
                }

                var errorMessage = errorMessageSb.ToString ();

                if ( !string.IsNullOrEmpty ( errorMessage ) )
                {
                    _userDialogService.ShowDialog ( errorMessage, "Save Patient Document", UserDialogServiceOptions.Ok );
                }
            }
            else
            {
                var patientDocumentUploadedEvent = new PatientDocumentUploadedEventArgs
                    {
                        PatientKey = PatientDocumentDto.PatientKey
                    };
                RaiseViewClosing ();
                _eventAggregator.GetEvent<PatientDocumentUploadedEvent> ().Publish ( patientDocumentUploadedEvent );
            }
        }

        private void SavePatientDocumentAsync ( PatientDocumentDto dto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SaveDtoRequest<PatientDocumentDto> { DataTransferObject = dto } );
            requestDispatcher.ProcessRequests ( HandleSavePatientDocumentCompleted, HandleSavePatientDocumenException );
            IsLoading = true;
        }

        #endregion
    }
}
