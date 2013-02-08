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
using System.Windows;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Common;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.VisitModule;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for CaseActivities class.
    /// </summary>
    public class CaseActivitiesViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IActivityTypeToViewMapperService _activityTypeToViewMapper;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private ObservableCollection<ActivityDto> _activityList;
        private long _clinicalCaseKey;
        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseActivitiesViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="activityTypeToViewMapper">The activity type to view mapper.</param>
        [InjectionConstructor]
        public CaseActivitiesViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory,
            IPopupService popupService,
            IActivityTypeToViewMapperService activityTypeToViewMapper )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _popupService = popupService;
            _activityTypeToViewMapper = activityTypeToViewMapper;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            GoToVisitCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => GoToVisitCommand, ExecuteGoToVisitCommand );
            GoToActivityCommand = commandFactoryHelper.BuildDelegateCommand<ActivityDto> ( () => GoToActivityCommand, ExecuteGoToActivityCommand );

            _eventAggregator.GetEvent<ClinicalCaseChangedEvent> ().Subscribe (
                ClinicalCaseChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterClinicalCaseChangedEvents );

            _eventAggregator.GetEvent<ActivityChangedEvent> ().Subscribe (
                ActivityChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterActivityChangedEvents );

            _eventAggregator.GetEvent<VisitChangedEvent> ().Subscribe (
                VisitChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterVisitChangedEvents );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the activity list.
        /// </summary>
        /// <value>The activity list.</value>
        public ObservableCollection<ActivityDto> ActivityList
        {
            get { return _activityList; }
            set { ApplyPropertyChange ( ref _activityList, () => ActivityList, value ); }
        }

        /// <summary>
        /// Gets the go to activity command.
        /// </summary>
        public ICommand GoToActivityCommand { get; private set; }

        /// <summary>
        /// Gets the go to visit command.
        /// </summary>
        public ICommand GoToVisitCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activities the changed event handler.
        /// </summary>
        /// <param name="eventArgs">The <see cref="Rem.Ria.PatientModule.ActivityChangedEventArgs"/> instance containing the event data.</param>
        public void ActivityChangedEventHandler ( ActivityChangedEventArgs eventArgs )
        {
            Deployment.Current.InvokeIfNeeded ( () => GetAllActivitiesByClinicalCaseAsync ( _clinicalCaseKey ) );
        }

        /// <summary>
        /// Clinicals the case changed event handler.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        public void ClinicalCaseChangedEventHandler ( ClinicalCaseChangedEventArgs args )
        {
            GetAllActivitiesByClinicalCase ( args.ClinicalCaseKey );
        }

        /// <summary>
        /// Filters the activity changed events.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.ActivityChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterActivityChangedEvents ( ActivityChangedEventArgs args )
        {
            return args.ClinicalCaseKey == _clinicalCaseKey;
        }

        /// <summary>
        /// Filters the clinical case changed events.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterClinicalCaseChangedEvents ( ClinicalCaseChangedEventArgs args )
        {
            return args.PatientKey == _patientKey && args.ClinicalCaseKey != _clinicalCaseKey;
        }

        /// <summary>
        /// Filters the visit changed events.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterVisitChangedEvents ( VisitChangedEventArgs visitChangedEventArgs )
        {
            return _activityList != null && _activityList.Select ( a => a.VisitKey == visitChangedEventArgs.VisitKey ).Any ();
        }

        /// <summary>
        /// Visits the changed event handler.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        public void VisitChangedEventHandler ( VisitChangedEventArgs args )
        {
            GetAllActivitiesByClinicalCase ( _clinicalCaseKey );
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
            var key = parameters.GetValue<long> ( "ClinicalCaseKey" );
            _clinicalCaseKey = key;
            _patientKey = parameters.GetValue<long>("PatientKey");
            if ( _clinicalCaseKey > 0 )
            {
                GetAllActivitiesByClinicalCaseAsync ( key );
            }
        }

        private void ExecuteGoToActivityCommand ( ActivityDto activityDto )
        {
            if ( activityDto != null )
            {
                if (activityDto.ProvenanceKey == 0 && activityDto.VisitStatusWellKnownName != VisitStatus.CheckedIn )
                {
                    _userDialogService.ShowDialog (
                        "This activity is associated with a Visit that is not 'Checked-In' so the Activity will be in read-only mode.",
                        string.Empty,
                        UserDialogServiceOptions.Ok );
                }
                var viewName = _activityTypeToViewMapper.GetViewNameFromActivityType ( activityDto.ActivityType.WellKnownName );
                _popupService.ShowPopup (
                    viewName,
                    "EditActivity",
                    string.Empty,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "VisitStatusWellKnownName", activityDto.VisitStatusWellKnownName ),
                            new KeyValuePair<string, string> ( "IsCurrentActivity", "true" ),
                            new KeyValuePair<string, string> ( "ActivityKey", activityDto.Key.ToString () ),
                            new KeyValuePair<string, string> ( "PatientKey", activityDto.PatientKey.ToString () )
                        },
                    true );
            }
        }

        private void ExecuteGoToVisitCommand ( object obj )
        {
            var activityDto = obj as ActivityDto;

            if ( activityDto != null )
            {
                _navigationService.Navigate (
                    RegionManager,
                    "VisitRegion",
                    "VisitView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "VisitKey", activityDto.VisitKey.ToString () ),
                            new KeyValuePair<string, string> ( "PatientKey", activityDto.PatientKey.ToString () )
                        } );
            }
        }

        private void GetAllActivitiesByClinicalCase ( long clinicalCaseKey )
        {
            Deployment.Current.InvokeIfNeeded (
                () =>
                    {
                        _clinicalCaseKey = clinicalCaseKey;
                        GetAllActivitiesByClinicalCaseAsync ( _clinicalCaseKey );
                    } );
        }

        private void GetAllActivitiesByClinicalCaseAsync ( long clinicalCaseKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetAllActivitiesByClinicalCaseRequest { ClinicalCaseKey = clinicalCaseKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetAllActivitiesByClinicalCaseCompleted, HandleGetAllActivitiesByClinicalCaseException );
        }

        private void HandleGetAllActivitiesByClinicalCaseCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAllActivitiesByClinicalCaseResponse> ();
            var results = new ObservableCollection<ActivityDto> ( response.ActivityDtos );
            ActivityList = results;
            IsLoading = false;
        }

        private void HandleGetAllActivitiesByClinicalCaseException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get activities by clinical case failed.", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
