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
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.PatientModule;
using TerminologyService.WebService;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for EditMedication class.
    /// </summary>
    public class EditMedicationViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly string _codeSystemIdentifier = "2.16.840.1.113883.6.88";
        private readonly string _codeSystemName = "RxNorm";
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserDialogService _userDialogService;
        private MedicationDtsInfoDto _dtsInfo;
        private CodedConceptDto _dtsMedicationCode;
        private EditableDtoWrapper _editableWrapper;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private MedicationDto _medication;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditMedicationViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public EditMedicationViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;

            Wrapper = new EditableDtoWrapper ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SaveCommand = commandFactoryHelper.BuildDelegateCommand ( () => SaveCommand, ExecuteSaveCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Gets or sets the DTS info.
        /// </summary>
        /// <value>The DTS info.</value>
        public MedicationDtsInfoDto DTSInfo
        {
            get { return _dtsInfo; }
            set
            {
                if ( DTSInfo != value )
                {
                    if ( _dtsInfo != null )
                    {
                        _dtsInfo.PropertyChanged -= DtsInfo_PropertyChanged;
                    }
                    _dtsInfo = value;
                    RaisePropertyChanged ( () => DTSInfo );
                    if ( _dtsInfo != null )
                    {
                        _dtsInfo.PropertyChanged += DtsInfo_PropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the DTS medication code.
        /// </summary>
        /// <value>The DTS medication code.</value>
        public CodedConceptDto DtsMedicationCode
        {
            get { return _dtsMedicationCode; }
            set
            {
                if ( !Equals ( _dtsMedicationCode, value ) )
                {
                    _dtsMedicationCode = value;
                    RaisePropertyChanged ( () => DtsMedicationCode );
                    UpdateDtsInfo ();
                    UpdateMedicationCode ();
                }
            }
        }

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets or sets the medication.
        /// </summary>
        /// <value>The medication.</value>
        public MedicationDto Medication
        {
            get { return _medication; }
            set
            {
                _medication = value;
                RaisePropertyChanged ( () => Medication );
                Wrapper.EditableDto = Medication;
            }
        }

        /// <summary>
        /// Gets or sets the save command.
        /// </summary>
        /// <value>The save command.</value>
        public ICommand SaveCommand { get; set; }

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

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "MedicationKey" );
            return _medication == null || key == _medication.Key;
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
            var key = parameters.GetValue<long> ( "MedicationKey" );
            var patientkey = parameters.GetValue<long> ( "PatientKey" );
            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            if ( key == 0 )
            {
                Medication = new MedicationDto ();
                Medication.PatientKey = patientkey;
            }
            else
            {
                dispatcher.Add ( new GetMedicationByKeyRequest { Key = key } );
                IsLoading = true;
            }
            dispatcher.AddLookupValuesRequest ( "MedicationStatus" );
            dispatcher.AddLookupValuesRequest ( "DiscontinuedReason" );
            dispatcher.ProcessRequests ( HandleRequestComplete, HandleRequestDispatcherException );
            IsLoading = true;

            StartRuleWatch ();
        }

        private void DtsInfo_PropertyChanged ( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => DTSInfo.SelectedForm ) ||
                 e.PropertyName == PropertyUtil.ExtractPropertyName ( () => DTSInfo.SelectedStrength ) )
            {
                UpdateMedicationCode ();
            }
        }

        private void ExecuteSaveCommand ()
        {
            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.Add ( new SaveDtoRequest<MedicationDto> { DataTransferObject = Medication } );
            dispatcher.ProcessRequests ( HandleSaveRequestComplete, HandleRequestDispatcherException );
            IsLoading = true;
        }

        private CodedConceptDto GetCodedConceptDto ( TerminologyConcept terminologyConcept )
        {
            return new CodedConceptDto
                {
                    CodedConceptCode = terminologyConcept.Code,
                    CodeSystemIdentifier = _codeSystemIdentifier,
                    CodeSystemName = _codeSystemName,
                    DisplayName = terminologyConcept.DisplayName
                };
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            var lookupValueLists =
                responses.Cast<GetLookupValuesResponse> ().ToDictionary ( response => response.Name, response => response.LookupValues );

            LookupValueLists = lookupValueLists;
            if ( Medication != null && Medication.Key == 0 )
            {
                Medication.MedicationStatus =
                    lookupValueLists["MedicationStatus"].FirstOrDefault (
                        m => m.WellKnownName == MedicationStatus.Active );
            }
            IsLoading = false;
        }

        private void GetMedicationByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            if ( receivedResponses.HasResponse<GetMedicationByKeyResponse> () )
            {
                IsLoading = false;
                var response = receivedResponses.Get<GetMedicationByKeyResponse> ();
                Medication = response.Medication;
                DtsMedicationCode = Medication.MedicationCodeCodedConcept;
            }
        }

        private void HandleRequestComplete ( ReceivedResponses receivedResponses )
        {
            GetLookupValuesCompleted ( receivedResponses );
            GetMedicationByKeyCompleted ( receivedResponses );
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Medication operation failed.", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleSaveRequestComplete ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<MedicationDto>> ();
            Medication = response.DataTransferObject;
            if ( !Medication.HasErrors )
            {
                _eventAggregator.GetEvent<MedicationChangedEvent> ().Publish (
                    new MedicationChangedEventArgs { Sender = this, MedicationKey = Medication.Key, PatientKey = Medication.PatientKey } );
                CloseViewCommand.Execute ( null );
            }
            IsLoading = false;
        }

        private void MedicationFormStrengthRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<MedicationFormStrengthResponse> ();
            DTSInfo = response.DTSInfo;
            _dtsMedicationCode = GetCodedConceptDto ( response.MainCode );
            RaisePropertyChanged ( () => DtsMedicationCode );
            if ( Medication != null && Medication.MedicationCodeCodedConcept != null )
            {
                var origDrugConcept =
                    response.DTSInfo.Drugs.FirstOrDefault ( d => d.Code == Medication.MedicationCodeCodedConcept.CodedConceptCode );
                if ( origDrugConcept == null )
                {
                    response.DTSInfo.SelectedStrength =
                        response.DTSInfo.Strengths.FirstOrDefault ( s => s.Code == Medication.MedicationCodeCodedConcept.CodedConceptCode );
                    response.DTSInfo.SelectedForm =
                        response.DTSInfo.Forms.FirstOrDefault ( f => f.Code == Medication.MedicationCodeCodedConcept.CodedConceptCode );
                }
                else
                {
                    response.DTSInfo.SelectedStrength =
                        response.DTSInfo.Strengths.FirstOrDefault (
                            s => origDrugConcept.Associations.SelectMany ( a => a.Value ).Contains ( s.Code ) );
                    response.DTSInfo.SelectedForm =
                        response.DTSInfo.Forms.FirstOrDefault ( f => origDrugConcept.Associations.SelectMany ( a => a.Value ).Contains ( f.Code ) );
                }
                Medication.RootMedicationCodedConcept = GetCodedConceptDto ( response.MainCode );
            }
            else if ( Medication != null )
            {
                Medication.MedicationCodeCodedConcept = GetCodedConceptDto ( response.MainCode );
                Medication.RootMedicationCodedConcept = GetCodedConceptDto ( response.MainCode );
            }
            IsLoading = false;
        }

        private void StartRuleWatch ()
        {
            var ruleExecutor = new NotifyPropertyChangedRuleExecutor<EditMedicationViewModel, IDataTransferObject> ();
            ruleExecutor.AddRunAllRulesProperty ( vm => vm.Medication );
            ruleExecutor.WatchSubject ( this );
        }

        private void UpdateDtsInfo ()
        {
            if ( DtsMedicationCode == null )
            {
                DTSInfo = null;
            }
            else
            {
                var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                dispatcher.Add ( new MedicationFormStrengthRequest { SelectedConcept = DtsMedicationCode } );
                dispatcher.ProcessRequests ( MedicationFormStrengthRequestCompleted, HandleRequestDispatcherException );
                IsLoading = true;
            }
        }

        private void UpdateMedicationCode ()
        {
            if ( DTSInfo != null )
            {
                if ( DTSInfo.SelectedForm != null && DTSInfo.SelectedStrength != null )
                {
                    var drug =
                        DTSInfo.Drugs.FirstOrDefault (
                            d => d.Associations.SelectMany ( a => a.Value ).Where ( v => DTSInfo.SelectedForm.Code == v ).Count () > 0 &&
                                 d.Associations.SelectMany ( a => a.Value ).Where ( v => DTSInfo.SelectedStrength.Code == v ).Count () > 0 );
                    Medication.MedicationCodeCodedConcept = GetCodedConceptDto ( drug );
                }
                else if ( DTSInfo.SelectedForm != null )
                {
                    Medication.MedicationCodeCodedConcept = GetCodedConceptDto ( DTSInfo.SelectedForm );
                }
                else if ( DTSInfo.SelectedStrength != null )
                {
                    Medication.MedicationCodeCodedConcept = GetCodedConceptDto ( DTSInfo.SelectedStrength );
                }
                else
                {
                    Medication.MedicationCodeCodedConcept = DtsMedicationCode;
                }
            }
        }

        #endregion
    }
}
