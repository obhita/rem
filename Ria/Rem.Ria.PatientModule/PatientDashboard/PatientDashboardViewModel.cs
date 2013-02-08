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
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Service;
using Rem.Ria.PatientModule.Web.ClinicalCaseEditor;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for PatientDashboard class.
    /// </summary>
    public class PatientDashboardViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPatientAccessService _patientAccessService;
        private readonly IUserDialogService _userDialogService;
        private ObservableCollection<ClinicalCaseSummaryDto> _allClinicalCases;
        private long _lastSelectedClinicalCaseKey;
        private bool _newVisitSelected;
        private long _patientKey;
        private ClinicalCaseSummaryDto _selectedClinicalCase;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDashboardViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="patientAccessService">The patient access service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientDashboardViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPatientAccessService patientAccessService,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _patientAccessService = patientAccessService;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            CreateCaseCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => CreateCaseCommand, ExecuteCreateCase, CanExecuteCreateCase );
            SelectCaseCommand = commandFactoryHelper.BuildDelegateCommand ( () => SelectCaseCommand, ExecuteSelectCase );

            ApplyContextChanges = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets all clinical cases.
        /// </summary>
        public ObservableCollection<ClinicalCaseSummaryDto> AllClinicalCases
        {
            get { return _allClinicalCases; }
            private set { ApplyPropertyChange ( ref _allClinicalCases, () => AllClinicalCases, value ); }
        }

        /// <summary>
        /// Gets the create case command.
        /// </summary>
        public ICommand CreateCaseCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [new visit selected].
        /// </summary>
        /// <value><c>true</c> if [new visit selected]; otherwise, <c>false</c>.</value>
        public bool NewVisitSelected
        {
            get { return _newVisitSelected; }
            set { ApplyPropertyChange ( ref _newVisitSelected, () => NewVisitSelected, value ); }
        }

        /// <summary>
        /// Gets the select case command.
        /// </summary>
        public ICommand SelectCaseCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected clinical case.
        /// </summary>
        /// <value>The selected clinical case.</value>
        public ClinicalCaseSummaryDto SelectedClinicalCase
        {
            get { return _selectedClinicalCase; }
            set { ApplyPropertyChange ( ref _selectedClinicalCase, () => SelectedClinicalCase, value ); }
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
            return _patientKey == patientKey;
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

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDefaultClinicalCaseByPatientRequest { PatientKey = _patientKey } );
            requestDispatcher.Add ( new GetAllClinicalCasesByPatientRequest { PatientKey = _patientKey } );

            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );

            _patientAccessService.LogEventAccess(_patientKey, "Patient Dashboard", "The Patient Dashboard for Patient {0} was accessed");
        }

        private bool CanExecuteCreateCase ( object obj )
        {
            return _patientKey != 0;
        }

        private void CreateClinicalCase ()
        {
            var locationKey = CurrentUserContext.Location.Key;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new CreateNewClinicalCaseRequest
                    {
                        PatientKey = _patientKey,
                        LocationKey = locationKey
                    } );
            requestDispatcher.ProcessRequests (
                CreateNewClinicalCaseRequestDispatcherCompleted,
                HandleCreateNewClinicalCaseRequestDispatcherException );
        }

        private void CreateNewClinicalCaseRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<CreateNewClinicalCaseResponse> ();
            var clinicalCaseDto = response.ClinicalCaseDto;

            SelectedClinicalCase = new ClinicalCaseSummaryDto
                {
                    Key = clinicalCaseDto.Key,
                    ClinicalCaseNumber = clinicalCaseDto.ClinicalCaseProfile.ClinicalCaseNumber
                };

            if ( SelectedClinicalCase != null )
            {
                _lastSelectedClinicalCaseKey = SelectedClinicalCase.Key;
                NavigateToClinicalCase ( true );
            }
        }

        private void ExecuteCreateCase ( object obj )
        {
            CreateClinicalCase ();
        }

        private void ExecuteSelectCase ()
        {
            if ( SelectedClinicalCase != null && SelectedClinicalCase.Key != _lastSelectedClinicalCaseKey )
            {
                _lastSelectedClinicalCaseKey = SelectedClinicalCase.Key;
                RaiseClinicalCaseChangedEvent ( SelectedClinicalCase.Key );
            }
        }

        private void GetAllClinicalCasesByPatientRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAllClinicalCasesByPatientResponse> ();
            var caseSummaryDtos = response.ClinicalCases;

            AllClinicalCases = new ObservableCollection<ClinicalCaseSummaryDto> ( caseSummaryDtos );
            if ( SelectedClinicalCase != null )
            {
                SelectedClinicalCase = AllClinicalCases.SingleOrDefault ( x => x.Key == SelectedClinicalCase.Key );
            }
        }

        private void GetDefaultClinicalCaseByPatientRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetDefaultClinicalCaseByPatientResponse> ();
            var caseSummaryDto = response.CaseSummaryDto;

            if ( caseSummaryDto == null )
            {
                // Create new clinical case and navigate to clinical case editor.
                CreateClinicalCase ();
            }
            else
            {
                SelectedClinicalCase = new ClinicalCaseSummaryDto
                    {
                        Key = caseSummaryDto.Key,
                        ClinicalCaseNumber = caseSummaryDto.ClinicalCaseNumber,
                        ClinicalCaseStartDate = caseSummaryDto.ClinicalCaseStartDate,
                        ClinicalCaseCloseDate = caseSummaryDto.ClinicalCaseCloseDate
                    };

                RefreshDashboard ( caseSummaryDto.Key );
            }
        }

        private void HandleCreateNewClinicalCaseRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Clinical Case created failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetDefaultClinicalCaseByPatientRequestCompleted ( receivedResponses );
            GetAllClinicalCasesByPatientRequestCompleted ( receivedResponses );
            if (SelectedClinicalCase != null)
            {
                _lastSelectedClinicalCaseKey = SelectedClinicalCase.Key;
            }
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog (
                exceptionInfo.Message,
                "Could not retrieve the default clinical case / Trying to Get all Cases Failed",
                UserDialogServiceOptions.Ok );
        }

        private void NavigateToClinicalCase ( bool isCreateMode )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "PatientWorkspaceView",
                "SubViewPassThrough",
                new[]
                    {
                        new KeyValuePair<string, string> ( "SubViewName", "ClinicalCaseEditorView" ),
                        new KeyValuePair<string, string> ( "ClinicalCaseKey", SelectedClinicalCase.Key.ToString () ),
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ),
                        new KeyValuePair<string, string> ( "IsCreateMode", isCreateMode.ToString () )
                    } );
        }

        private void RaiseClinicalCaseChangedEvent ( long clinicalCaseKey )
        {
            _eventAggregator.GetEvent<ClinicalCaseChangedEvent> ().Publish (
                new ClinicalCaseChangedEventArgs { Sender = this, ClinicalCaseKey = clinicalCaseKey, PatientKey = _patientKey } );
        }

        private void RefreshDashboard ( long clinicalCaseKey )
        {
            var parameters = new[]
                {
                    new KeyValuePair<string, string> ( "ClinicalCaseKey", clinicalCaseKey.ToString () ),
                    new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () )
                };

            _navigationService.Navigate ( RegionManager, "ProgramEnrollmentRegion", "ProgramEnrollmentListView", null, parameters );
            _navigationService.Navigate ( RegionManager, "ActivitiesRegion", "CaseActivitiesView", null, parameters );
            _navigationService.Navigate ( RegionManager, "MedicationsAndProblemRegion", "MedicationListView", null, parameters );
            _navigationService.Navigate ( RegionManager, "MedicationsAndProblemRegion", "CaseProblemListView", null, parameters );
            _navigationService.Navigate ( RegionManager, "CaseSnapshotRegion", "CaseSnapshotView", null, parameters );
            _navigationService.Navigate ( RegionManager, "CaseAlertsRegion", "CaseAlertsView", null, parameters );
            _navigationService.Navigate ( RegionManager, "VisitListRegion", "VisitListView", null, parameters );
        }

        #endregion
    }
}
