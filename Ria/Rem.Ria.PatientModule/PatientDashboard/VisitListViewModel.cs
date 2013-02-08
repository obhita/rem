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
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.VisitModule;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for VisitList class.
    /// </summary>
    public class VisitListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IActivityTypeToViewMapperService _activityTypeToViewMapper;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _clinicalCaseKey;
        private long _patientKey;
        private int _lastRecentVisitCount;
        private int _lastScheduledVisitCount;
        private ObservableCollection<VisitActivityDto> _recentActivities;
        private int _recentVisitCount;
        private ObservableCollection<VisitActivityDto> _scheduledActivities;
        private int _scheduledVisitCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitListViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="activityTypeToViewMapper">The activity type to view mapper.</param>
        /// <param name="popupService">The popup service.</param>
        [InjectionConstructor]
        public VisitListViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory,
            IActivityTypeToViewMapperService activityTypeToViewMapper,
            IPopupService popupService )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _activityTypeToViewMapper = activityTypeToViewMapper;
            _popupService = popupService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            GoToVisitCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => GoToVisitCommand, ExecuteGoToVisitCommand );
            GoToActivityCommand = commandFactoryHelper.BuildDelegateCommand<VisitActivityDto> (
                () => GoToActivityCommand, ExecuteGoToActivityCommand );
            RecentVisitCountChangedCommand = commandFactoryHelper.BuildDelegateCommand (
                () => RecentVisitCountChangedCommand, ExecuteRecentVisitCountChangedCommand );
            ScheduledVisitCountChangedCommand = commandFactoryHelper.BuildDelegateCommand (
                () => ScheduledVisitCountChangedCommand, ExecuteScheduledVisitCountChangedCommand );

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

            RecentVisitCount = ScheduledVisitCount = _lastRecentVisitCount = _lastScheduledVisitCount = 2;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the go to activity command.
        /// </summary>
        public ICommand GoToActivityCommand { get; private set; }

        /// <summary>
        /// Gets or sets the go to visit command.
        /// </summary>
        /// <value>The go to visit command.</value>
        public ICommand GoToVisitCommand { get; set; }

        /// <summary>
        /// Gets or sets the recent activities.
        /// </summary>
        /// <value>The recent activities.</value>
        public ObservableCollection<VisitActivityDto> RecentActivities
        {
            get { return _recentActivities; }
            set { ApplyPropertyChange ( ref _recentActivities, () => RecentActivities, value ); }
        }

        /// <summary>
        /// Gets or sets the recent visit count.
        /// </summary>
        /// <value>The recent visit count.</value>
        public int RecentVisitCount
        {
            get { return _recentVisitCount; }
            set { ApplyPropertyChange ( ref _recentVisitCount, () => RecentVisitCount, value ); }
        }

        /// <summary>
        /// Gets the recent visit count changed command.
        /// </summary>
        public ICommand RecentVisitCountChangedCommand { get; private set; }

        /// <summary>
        /// Gets or sets the scheduled activities.
        /// </summary>
        /// <value>The scheduled activities.</value>
        public ObservableCollection<VisitActivityDto> ScheduledActivities
        {
            get { return _scheduledActivities; }
            set { ApplyPropertyChange ( ref _scheduledActivities, () => ScheduledActivities, value ); }
        }

        /// <summary>
        /// Gets or sets the scheduled visit count.
        /// </summary>
        /// <value>The scheduled visit count.</value>
        public int ScheduledVisitCount
        {
            get { return _scheduledVisitCount; }
            set { ApplyPropertyChange ( ref _scheduledVisitCount, () => ScheduledVisitCount, value ); }
        }

        /// <summary>
        /// Gets the scheduled visit count changed command.
        /// </summary>
        public ICommand ScheduledVisitCountChangedCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activities the changed event handler.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.ActivityChangedEventArgs"/> instance containing the event data.</param>
        public void ActivityChangedEventHandler ( ActivityChangedEventArgs args )
        {
            GetRecentActivitiesByClinicalCase ( args.ClinicalCaseKey );
            GetScheduledActivitiesByClinicalCase ( args.ClinicalCaseKey );
        }

        /// <summary>
        /// Clinicals the case changed event handler.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        public void ClinicalCaseChangedEventHandler ( ClinicalCaseChangedEventArgs args )
        {
            _clinicalCaseKey = args.ClinicalCaseKey;
            GetRecentActivitiesByClinicalCase ( args.ClinicalCaseKey );
            GetScheduledActivitiesByClinicalCase ( args.ClinicalCaseKey );
        }

        /// <summary>
        /// Filters the activity changed events.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.ActivityChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterActivityChangedEvents ( ActivityChangedEventArgs visitChangedEventArgs )
        {
            return visitChangedEventArgs.ClinicalCaseKey == _clinicalCaseKey;
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
            return ( _scheduledActivities != null && _scheduledActivities.Select ( a => a.VisitKey == visitChangedEventArgs.VisitKey ).Any () ) ||
                   ( _recentActivities != null && _recentActivities.Select ( a => a.VisitKey == visitChangedEventArgs.VisitKey ).Any () );
        }

        /// <summary>
        /// Visits the changed event handler.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        public void VisitChangedEventHandler ( VisitChangedEventArgs args )
        {
            GetScheduledActivitiesByClinicalCase ( _clinicalCaseKey );
            GetRecentActivitiesByClinicalCase ( _clinicalCaseKey );
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
            _patientKey = parameters.GetValue<long>("PatientKey");
            var key = parameters.GetValue<long> ( "ClinicalCaseKey" );
            _clinicalCaseKey = key;

            if ( _clinicalCaseKey > 0 )
            {
                GetRecentActivitiesByClinicalCaseAsync ( key );
                GetScheduledActivitiesByClinicalCaseAsync ( key );
            }
        }

        private void ExecuteGoToActivityCommand ( VisitActivityDto visitActivityDto )
        {
            if ( visitActivityDto != null && visitActivityDto.ActivityTypeWellKnownName != null )
            {
                if ( visitActivityDto.VisitStatusWellKnownName != VisitStatus.CheckedIn )
                {
                    _userDialogService.ShowDialog (
                        "This activity is associated with a Visit that is not 'Checked-In' so the Activity will be in read-only mode.",
                        string.Empty,
                        UserDialogServiceOptions.Ok );
                }
                var viewName = _activityTypeToViewMapper.GetViewNameFromActivityType ( visitActivityDto.ActivityTypeWellKnownName );
                _popupService.ShowPopup (
                    viewName,
                    "EditActivity",
                    string.Empty,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "VisitStatusWellKnownName", visitActivityDto.VisitStatusWellKnownName ),
                            new KeyValuePair<string, string> ( "IsCurrentActivity", "true" ),
                            new KeyValuePair<string, string> ( "ActivityKey", visitActivityDto.ActivityKey.ToString () ),
                            new KeyValuePair<string, string> ( "PatientKey", visitActivityDto.PatientKey.ToString () )
                        },
                    true );
            }
        }

        private void ExecuteGoToVisitCommand ( object obj )
        {
            var visitActivityDto = obj as VisitActivityDto;

            if ( visitActivityDto != null )
            {
                _navigationService.Navigate (
                    RegionManager,
                    "VisitRegion",
                    "VisitView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "VisitKey", visitActivityDto.VisitKey.ToString () ),
                            new KeyValuePair<string, string> ( "PatientKey", visitActivityDto.PatientKey.ToString () ),
                            new KeyValuePair<string, string> ( "ClinicalCaseKey", _clinicalCaseKey.ToString () )
                        } );
            }
        }

        private void ExecuteRecentVisitCountChangedCommand ()
        {
            if ( RecentVisitCount != _lastRecentVisitCount )
            {
                _lastRecentVisitCount = RecentVisitCount;
                GetRecentActivitiesByClinicalCase ( _clinicalCaseKey );
            }
        }

        private void ExecuteScheduledVisitCountChangedCommand ()
        {
            if ( ScheduledVisitCount != _lastScheduledVisitCount )
            {
                _lastScheduledVisitCount = ScheduledVisitCount;
                GetScheduledActivitiesByClinicalCase ( _clinicalCaseKey );
            }
        }

        private void GetRecentActivitiesByClinicalCase ( long clinicalCaseKey )
        {
            Deployment.Current.InvokeIfNeeded (
                () =>
                    {
                        _clinicalCaseKey = clinicalCaseKey;
                        GetRecentActivitiesByClinicalCaseAsync ( _clinicalCaseKey );
                    } );
        }

        private void GetRecentActivitiesByClinicalCaseAsync ( long clinicalCaseKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new GetRecentVisitActivitiesByClinicalCaseRequest { ClinicalCaseKey = clinicalCaseKey, VisitCount = RecentVisitCount } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetRecentActivitiesByClinicalCaseCompleted, HandleGetRecentActivitiesByClinicalCaseException );
        }

        private void GetScheduledActivitiesByClinicalCase ( long clinicalCaseKey )
        {
            Deployment.Current.InvokeIfNeeded (
                () =>
                    {
                        _clinicalCaseKey = clinicalCaseKey;
                        GetScheduledActivitiesByClinicalCaseAsync ( _clinicalCaseKey );
                    } );
        }

        private void GetScheduledActivitiesByClinicalCaseAsync ( long clinicalCaseKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new GetScheduledVisitActivitiesByClinicalCaseRequest { ClinicalCaseKey = clinicalCaseKey, VisitCount = ScheduledVisitCount } );
            IsLoading = true;
            requestDispatcher.ProcessRequests (
                HandleGetScheduledActivitiesByClinicalCaseCompleted, HandleGetScheduledActivitiesByClinicalCaseException );
        }

        private void HandleGetRecentActivitiesByClinicalCaseCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetRecentVisitActivitiesByClinicalCaseResponse> ();
            var results = new ObservableCollection<VisitActivityDto> ( response.RecentActivityDtos );
            RecentActivities = results;
            IsLoading = false;
        }

        private void HandleGetRecentActivitiesByClinicalCaseException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Getting recent activities by clinical case failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleGetScheduledActivitiesByClinicalCaseCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetScheduledVisitActivitiesByClinicalCaseResponse> ();
            var results = new ObservableCollection<VisitActivityDto> ( response.ScheduledActivityDtos );
            ScheduledActivities = results;
            IsLoading = false;
        }

        private void HandleGetScheduledActivitiesByClinicalCaseException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get scheduled activities by clinical case failed.", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
