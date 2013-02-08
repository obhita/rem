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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Collections;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.ImportC32;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientEditor;

namespace Rem.Ria.PatientModule.ImportC32
{
    /// <summary>
    /// View Model for importing C32 document.
    /// </summary>
    public class ImportC32ViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private EditableDtoWrapper _editableWrapper;
        private bool _isEditing;
        private ProvenanceDto _provenance;
        private PagedCollectionView _medicationList;
        private PagedCollectionView _existingMedicationList;
        private PagedCollectionView _allergyList;
        private PagedCollectionView _problemList;
        private PagedCollectionView _immunizationList;
        private LabSpecimenDto _labSpecimen;
        private VitalSignDto _vitalSignDto;
        private long _patientDocumentKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportC32ViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public ImportC32ViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportC32ViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public ImportC32ViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _popupService = popupService;

            Wrapper = new EditableDtoWrapper ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ImportC32Command = commandFactoryHelper.BuildDelegateCommand ( () => ImportC32Command, ExecuteImportC32Command, CanExecuteImportC32Command );
            CancelCommand = commandFactoryHelper.BuildDelegateCommand ( () => CancelCommand, ExecuteCancelCommand );
            SelectionChangedCommand = commandFactoryHelper.BuildDelegateCommand ( () => SelectionChangedCommand, ExecuteSelectionChangedCommand );
        }
        #endregion

        #region Public Properties 
        
        /// <summary>
        /// Gets the import C32 command.
        /// </summary>
        public ICommand ImportC32Command { get; private set; }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; private set; }
  
        /// <summary>
        /// Gets or sets a value indicating whether this instance is editing.
        /// </summary>
        /// <value><c>true</c> if this instance is editing; otherwise, <c>false</c>.</value>
        public bool IsEditing
        {
            get { return _isEditing; }
            set { ApplyPropertyChange ( ref _isEditing, () => IsEditing, value ); }
        }

        /// <summary>
        /// Gets the selection changed command.
        /// </summary>
        public ICommand SelectionChangedCommand { get; private set; }

        /// <summary>
        /// Gets or sets the provenance.
        /// </summary>
        /// <value>
        /// The provenance.
        /// </value>
        public ProvenanceDto Provenance
        {
            get { return _provenance; }

            set
            {
                _provenance = value;
                RaisePropertyChanged(() => Provenance);
                Wrapper.EditableDto = Provenance;
            }
        }

        /// <summary>
        /// Gets the medication list.
        /// </summary>
        public PagedCollectionView MedicationList
        {
            get { return _medicationList; }
            private set { ApplyPropertyChange ( ref _medicationList, () => MedicationList, value ); }
        }

        /// <summary>
        /// Gets the existing medication list.
        /// </summary>
        public PagedCollectionView ExistingMedicationList
        {
            get { return _existingMedicationList; }
            private set { ApplyPropertyChange(ref _existingMedicationList, () => ExistingMedicationList, value); }
        }

        /// <summary>
        /// Gets the allergy list.
        /// </summary>
        public PagedCollectionView AllergyList
        {
            get { return _allergyList; }
            private set { ApplyPropertyChange(ref _allergyList, () => AllergyList, value); }
        }

        /// <summary>
        /// Gets the problem list.
        /// </summary>
        public PagedCollectionView ProblemList
        {
            get { return _problemList; }
            private set { ApplyPropertyChange ( ref _problemList, () => ProblemList, value ); }
        }

        /// <summary>
        /// Gets the immunization list.
        /// </summary>
        public PagedCollectionView ImmunizationList
        {
            get { return _immunizationList; }
            private set { ApplyPropertyChange ( ref _immunizationList, () => ImmunizationList, value ); }
        }

        /// <summary>
        /// Gets the lab specimen.
        /// </summary>
        public LabSpecimenDto LabSpecimen
        {
            get { return _labSpecimen; }
            private set { ApplyPropertyChange ( ref _labSpecimen, () => LabSpecimen, value ); }
        }

        /// <summary>
        /// Gets the vital sign.
        /// </summary>
        public VitalSignDto VitalSign
        {
            get { return _vitalSignDto; }
            private set { ApplyPropertyChange(ref _vitalSignDto, () => VitalSign, value); }
        }

        /// <summary>
        /// Gets or sets the wrapper.
        /// </summary>
        /// <value>The wrapper.</value>
        public EditableDtoWrapper Wrapper
        {
            get { return _editableWrapper; }

            set
            {
                _editableWrapper = value;
                RaisePropertyChanged ( () => Wrapper );
            }
        }

        #endregion

        #region Public Methods

        private void ExecuteSelectionChangedCommand()
        {
            (ImportC32Command as VirtualDelegateCommand).RaiseCanExecuteChanged ();
        }

        /// <summary>
        /// Determines whether this instance can close.
        /// </summary>
        /// <returns><c>true</c> if this instance can close; otherwise, <c>false</c>.</returns>
        public bool CanClose ()
        {
            var canClose = true;
            if ( Wrapper.IsDirty )
            {
                var result = _userDialogService.ShowDialog (
                    "You have unsaved changes. Are you sure you want to close?",
                    "Pending Changes:",
                    UserDialogServiceOptions.OkCancel );
                if ( result == UserDialogServiceResult.Cancel )
                {
                    canClose = false;
                }
            }
            return canClose;
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

        private void ExecuteCancelCommand ()
        {
            _popupService.ClosePopup ( "ImportC32View" );
        }
      
        private void ExecuteImportC32Command ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();

            var medicationsToBeImported = ( from medicationDto in _medicationList.SourceCollection.OfType<MedicationDto> ()
                                            where medicationDto.IsSelected
                                            select medicationDto ).ToList ();

            var allergiesToBeImported = ( from allergyDto in _allergyList.SourceCollection.OfType<AllergyDto> ()
                                          where allergyDto.IsSelected
                                          select allergyDto ).ToList ();

            var problemsToBeImported = (from problemDto in _problemList.SourceCollection.OfType<ProblemDto>()
                                         where problemDto.IsSelected
                                         select problemDto).ToList();

            var immunizationsToBeImported = (from immunizationDto in _immunizationList.SourceCollection.OfType<ImmunizationDto>()
                                        where immunizationDto.IsSelected
                                        select immunizationDto).ToList();

            var labSpecimenToBeImported = LabSpecimen;
            if (labSpecimenToBeImported != null)
            {
                labSpecimenToBeImported.LabResults = new SoftDeleteObservableCollection<LabResultDto> (
                    ( from labResultDto in LabSpecimen.LabResults
                      where labResultDto.IsSelected
                      select labResultDto ).ToList () );
            }

            var vitalSignToBeImported = VitalSign;
            if(vitalSignToBeImported != null )
            {
                vitalSignToBeImported.BloodPressures = new SoftDeleteObservableCollection<BloodPressureDto> (
                    ( from bpDto in VitalSign.BloodPressures where bpDto.IsSelected select bpDto ).ToList ());
                vitalSignToBeImported.HeartRates = new SoftDeleteObservableCollection<HeartRateDto> (
                    ( from hrDto in VitalSign.HeartRates where hrDto.IsSelected select hrDto ).ToList ());
            }

            if (vitalSignToBeImported.BloodPressures.Count == 0 && vitalSignToBeImported.HeartRates.Count == 0)
            {
                vitalSignToBeImported = null;
            }

            requestDispatcher.Add (
                    new ImportC32Request
                        {
                            PatientDocumentKey = _patientDocumentKey,
                            Provenance = Provenance,
                            Medications = medicationsToBeImported,
                            Allergies = allergiesToBeImported,
                            Problems = problemsToBeImported,
                            Immunizations = immunizationsToBeImported,
                            LabSpecimen = labSpecimenToBeImported,
                            VitalSign = vitalSignToBeImported
                        } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleImportC32Completed, HandleImportC32Exception );
        }

        private bool CanExecuteImportC32Command()
        {
            return (MedicationList != null && MedicationList.SourceCollection.OfType<MedicationDto> ().Any ( m => m.IsSelected )) || 
                (ProblemList != null && ProblemList.SourceCollection.OfType<ProblemDto> ().Any ( p => p.IsSelected )) ||
                (AllergyList != null && AllergyList.SourceCollection.OfType<AllergyDto> ().Any ( a => a.IsSelected )) ||
                (ImmunizationList != null && ImmunizationList.SourceCollection.OfType<ImmunizationDto> ().Any( i => i.IsSelected )) ||
                (LabSpecimen != null && LabSpecimen.LabResults != null && LabSpecimen.LabResults.Any( r => r.IsSelected ))||
                ( VitalSign != null && VitalSign.BloodPressures != null && VitalSign.BloodPressures.Any(bp => bp.IsSelected)) ||
                (VitalSign != null && VitalSign.HeartRates != null && VitalSign.HeartRates.Any(hr => hr.IsSelected));
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetDataFromC32Response>();
            Provenance = response.Provenance;
            MedicationList = new PagedCollectionView ( response.Medications );
            AllergyList = new PagedCollectionView ( response.Allergies );
            ProblemList = new PagedCollectionView ( response.Problems );
            ImmunizationList = new PagedCollectionView ( response.Immunizations );
            LabSpecimen = response.LabSpecimen;
            VitalSign = response.VitalSign;

            var responseExistingMedication = receivedResponses.Get<GetAllMedicationsByPatientResponse>();
            ExistingMedicationList = new PagedCollectionView ( responseExistingMedication.MedicationDtos);
            IsLoading = false;
        }



        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Getting data from C32 document failed", UserDialogServiceOptions.Ok );
        }

        private void HandleImportC32Completed ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<ImportC32Response> ();
            IsLoading = false;
            ExecuteCancelCommand ();
        }

        private void HandleImportC32Exception ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not import C32", UserDialogServiceOptions.Ok );
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand(KeyValuePair<string, string>[] parameters)
        {
            _patientDocumentKey = parameters.GetValue<long> ( "PatientDocumentKey" );
            var patientKey = parameters.GetValue<long> ( "PatientKey" );
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add ( new GetDataFromC32Request { PatientDocumentKey = _patientDocumentKey } );
            requestDispatcher.Add(new GetAllMedicationsByPatientRequest { PatientKey = patientKey });
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }
        #endregion
    }
}
