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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Pillar.Common.Commands;
using Pillar.Common.Extension;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.CdsAlerts;
using Rem.Ria.PatientModule.Common;
using Rem.Ria.PatientModule.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.Service;
using Rem.WellKnownNames.VisitModule;
using Telerik.Windows.Controls.DragDrop;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for Visit class.
    /// </summary>
    public class VisitViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private const string VisitStatusLookupName = "VisitStatus";
        private readonly IActivityTypeToViewMapperService _activityTypeToViewMapperService;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly ICdsAlertService _cdsAlertService;
        private readonly object _checkStatusSync = new object ();
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPatientAccessService _patientAccessService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _clinicalCaseKey;
        private long _currentActivityKey;
        private long _patientKey;
        private ObservableCollection<ProblemDto> _problemList;
        private IList<ActivityTypeDto> _schedulableActivityTypes;
        private ActivityTypeDto _selectedActivityType;
        private ObservableCollection<StaffSummaryDto> _staffList;
        private IList<LookupValueDto> _statusList;
        private bool _statusQuestionAsked;
        private VisitDto _visit;
        private long _visitKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="cdsAlertService">The CDS alert service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="patientAccessService">The patient access service.</param>
        /// <param name="activityTypeToViewMapperService">The activity type to view mapper service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        public VisitViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            ICdsAlertService cdsAlertService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPatientAccessService patientAccessService,
            IActivityTypeToViewMapperService activityTypeToViewMapperService,
            INavigationService navigationService,
            ICommandFactory commandFactory,
            IPopupService popupService )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _cdsAlertService = cdsAlertService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _patientAccessService = patientAccessService;
            _activityTypeToViewMapperService = activityTypeToViewMapperService;
            _navigationService = navigationService;
            _popupService = popupService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SelectAddActivityCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => SelectAddActivityCommand, ExecuteSelectAddActivity );
            SaveVisitCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => SaveVisitCommand, ExecuteSaveVisit );
            DetachProblemCommand = commandFactoryHelper.BuildDelegateCommand<ProblemDto> ( () => DetachProblemCommand, ExecuteDetachProblem );
            StatusUpdatedCommand = commandFactoryHelper.BuildDelegateCommand<LookupValueDto> (
                () => StatusUpdatedCommand, ExecuteStatusUpdatedCommand, CanExecuteStatusUpdatedCommand );
            DropQueryCommand = commandFactoryHelper.BuildDelegateCommand<DragDropQueryEventArgs> ( () => DropQueryCommand, ExecuteDropQueryCommand );
            ShowGrowthChartCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowGrowthChartCommand, ExecuteShowGrowthChartCommand );
            CloseViewCommand = commandFactoryHelper.BuildDelegateCommand ( () => CloseViewCommand, ExecuteCloseViewCommand );

            ProblemList = new ObservableCollection<ProblemDto> ();

            _eventAggregator.GetEvent<VisitChangedEvent> ().Subscribe (
                VisitChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterVisitChangedEvents );

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
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the detach problem command.
        /// </summary>
        public ICommand DetachProblemCommand { get; private set; }

        /// <summary>
        /// Gets the drop query command.
        /// </summary>
        public ICommand DropQueryCommand { get; private set; }

        /// <summary>
        /// Gets or sets the problem list.
        /// </summary>
        /// <value>The problem list.</value>
        public ObservableCollection<ProblemDto> ProblemList
        {
            get { return _problemList; }
            set
            {
                _problemList = value ?? new ObservableCollection<ProblemDto> ();

                RaisePropertyChanged ( () => ProblemList );

                ProblemList.CollectionChanged -= ProblemList_CollectionChanged;
                ProblemList.CollectionChanged += ProblemList_CollectionChanged;
            }
        }

        /// <summary>
        /// Gets the save visit command.
        /// </summary>
        public ICommand SaveVisitCommand { get; private set; }

        /// <summary>
        /// Gets the schedulable activity types.
        /// </summary>
        public IList<ActivityTypeDto> SchedulableActivityTypes
        {
            get { return _schedulableActivityTypes; }
            private set { ApplyPropertyChange ( ref _schedulableActivityTypes, () => SchedulableActivityTypes, value ); }
        }

        /// <summary>
        /// Gets the select add activity command.
        /// </summary>
        public ICommand SelectAddActivityCommand { get; private set; }

        /// <summary>
        /// Gets or sets the type of the selected activity.
        /// </summary>
        /// <value>The type of the selected activity.</value>
        public ActivityTypeDto SelectedActivityType
        {
            get { return _selectedActivityType; }
            set { ApplyPropertyChange ( ref _selectedActivityType, () => SelectedActivityType, value ); }
        }

        /// <summary>
        /// Gets the show growth chart command.
        /// </summary>
        public ICommand ShowGrowthChartCommand { get; private set; }

        /// <summary>
        /// Gets or sets the staff list.
        /// </summary>
        /// <value>The staff list.</value>
        public ObservableCollection<StaffSummaryDto> StaffList
        {
            get { return _staffList; }
            set { ApplyPropertyChange ( ref _staffList, () => StaffList, value ); }
        }

        /// <summary>
        /// Gets or sets the status list.
        /// </summary>
        /// <value>The status list.</value>
        public IList<LookupValueDto> StatusList
        {
            get { return _statusList; }
            set { ApplyPropertyChange ( ref _statusList, () => StatusList, value ); }
        }

        /// <summary>
        /// Gets the status updated command.
        /// </summary>
        public ICommand StatusUpdatedCommand { get; private set; }

        /// <summary>
        /// Gets or sets the visit.
        /// </summary>
        /// <value>The visit.</value>
        public VisitDto Visit
        {
            get { return _visit; }
            set
            {
                _visit = value;
                RaisePropertyChanged ( () => Visit );
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Activities the changed event handler.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.ActivityChangedEventArgs"/> instance containing the event data.</param>
        public void ActivityChangedEventHandler ( ActivityChangedEventArgs visitChangedEventArgs )
        {
            Deployment.Current.InvokeIfNeeded ( () => GetVisitByVisitKeyAsync ( visitChangedEventArgs.VisitKey ) );
        }

        /// <summary>
        /// Clinicals the case changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        public void ClinicalCaseChangedEventHandler ( ClinicalCaseChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded (
                () =>
                    {
                        ClearVisitInfo ();
                        if ( CloseViewCommand.CanExecute ( null ) )
                        {
                            CloseViewCommand.Execute ( null );
                        }
                    } );
        }

        /// <summary>
        /// Filters the activity changed events.
        /// </summary>
        /// <param name="activityChangedEventArgs">The <see cref="Rem.Ria.PatientModule.ActivityChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterActivityChangedEvents ( ActivityChangedEventArgs activityChangedEventArgs )
        {
            if ( activityChangedEventArgs.Sender == this )
            {
                return false;
            }
            return _visit != null && activityChangedEventArgs.VisitKey == _visit.Key;
        }

        /// <summary>
        /// Filters the clinical case changed events.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterClinicalCaseChangedEvents ( ClinicalCaseChangedEventArgs args )
        {
            return true;
        }

        /// <summary>
        /// Filters the visit changed events.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterVisitChangedEvents ( VisitChangedEventArgs visitChangedEventArgs )
        {
            var senderType = visitChangedEventArgs.Sender.GetType ();
            if ( senderType.BaseType != null )
            {
                if ( senderType.BaseType.IsGenericType && senderType.BaseType.GetGenericTypeDefinition () == typeof( ActivityViewModelBase<> ) )
                {
                    return false;
                }
            }
            if ( visitChangedEventArgs.Sender == this )
            {
                return false;
            }
            return _visit != null && visitChangedEventArgs.VisitKey == _visit.Key;
        }

        /// <summary>
        /// Gets the visit by visit key async.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        public void GetVisitByVisitKeyAsync ( long visitKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<VisitDto> { Key = visitKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetVisitByKeyCompleted, HandleGetVisitByKeyException );
        }

        /// <summary>
        /// Visits the changed event handler.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        public void VisitChangedEventHandler ( VisitChangedEventArgs visitChangedEventArgs )
        {
            Deployment.Current.InvokeIfNeeded ( () => GetVisitByVisitKeyAsync ( visitChangedEventArgs.VisitKey ) );
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
        /// Executes the close view command.
        /// </summary>
        protected override void ExecuteCloseViewCommand ()
        {
            ClearVisitInfo ();
            RaiseViewClosing ();
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            _visitKey = parameters.GetValue<long> ( "VisitKey" );
            _currentActivityKey = parameters.GetValue<long> ( "CurrentActivityKey" );
            _patientKey = parameters.GetValue<long> ( "PatientKey" );
            _clinicalCaseKey = parameters.GetValue<long> ( "ClinicalCaseKey" );

            if ( Visit == null || _visitKey != Visit.Key )
            {
                _statusQuestionAsked = false;
            }

            ClearVisitInfo ();

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add ( new GetDtoRequest<VisitDto> { Key = _visitKey } );
            requestDispatcher.Add ( new GetStaffsByAgencyRequest { AgencyKey = CurrentUserContext.Agency.Key } );
            requestDispatcher.AddLookupValuesRequest ( VisitStatusLookupName );
            requestDispatcher.Add ( new GetAllSchedulableActivityTypesRequest () );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );

            _patientAccessService.LogEventAccess(_patientKey, "Visit Editor", "The Visit Editor for Patient {0} was accessed");
            _cdsAlertService.CheckRules ( _patientKey );
        }

        private void CheckStatus ()
        {
            lock ( _checkStatusSync )
            {
                if ( Visit != null && StatusList != null && StatusList.Count > 0 && !_statusQuestionAsked )
                {
                    if ( Visit.VisitStatus.WellKnownName == VisitStatus.Scheduled && Visit.AppointmentStartDateTime.Date <= DateTime.Now )
                    {
                        _statusQuestionAsked = true;
                        var result =
                            _userDialogService.ShowDialog (
                                "This Visit is not checked in. Would you like to check it in?", "Visit Status", UserDialogServiceOptions.OkCancel );
                        if ( result == UserDialogServiceResult.Ok )
                        {
                            ExecuteStatusUpdatedCommand ( StatusList.First ( s => s.WellKnownName == VisitStatus.CheckedIn ) );
                        }
                    }
                }
            }
        }

        private void ClearVisitInfo ()
        {
            if (RegionManager.Regions.ContainsRegionWithName("VisitRegion"))
            {
                var activitiesRegion = RegionManager.Regions["VisitRegion"];

                // Retain a reference to the Context before removing the View from the Region.
                // This is necessary because Prism sets the Region Context to null when a view is removed from it.
                var regionContext = activitiesRegion.Context;

                foreach ( var view in activitiesRegion.Views )
                {
                    if ( view is FrameworkElement && ( view as FrameworkElement ).DataContext is IActivityViewModel )
                    {
                        activitiesRegion.Remove ( view );
                    }
                }

                activitiesRegion.Context = regionContext;
            }

            Visit = null;
            ProblemList = null;
            _statusQuestionAsked = false;
        }

        private void ExecuteDetachProblem ( ProblemDto problemDto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new DetachProblemFromVisitRequest { ProblemDto = problemDto, VisitKey = Visit.Key } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleDetachProblemFromVisitCompleted, HandleDetachProblemFromVisitException );
        }

        private void ExecuteDropQueryCommand ( DragDropQueryEventArgs obj )
        {
            var result = false;
            if ( obj.Options.Payload is ProblemDto )
            {
                var problemDto = obj.Options.Payload as ProblemDto;
                var probQuery =
                    ProblemList.FirstOrDefault (
                        d => d.ProblemCodeCodedConcept.CodedConceptCode == problemDto.ProblemCodeCodedConcept.CodedConceptCode );
                result = probQuery == null;
            }
            obj.QueryResult = result;
        }

        private void ExecuteSaveVisit ( object obj )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SaveDtoRequest<VisitDto> { DataTransferObject = Visit } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleSaveVisitDtoCompleted, HandleSaveVisitDtoException );
        }

        private void ExecuteSelectAddActivity ( object obj )
        {
            if ( _selectedActivityType != null )
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

                requestDispatcher.Add ( new ScheduleActivityRequest { ActivityType = _selectedActivityType, VisitKey = Visit.Key } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleSaveScheduledActivityCompleted, HandleSaveScheduledActivityDtoException );
                SelectedActivityType = null;
            }
        }

        private void ExecuteShowGrowthChartCommand ()
        {
            _popupService.ShowPopup (
                "GrowthRateView",
                null,
                "Growth Chart",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ),
                    } );
        }

        private void ExecuteStatusUpdatedCommand ( LookupValueDto visitStatus )
        {
            if ( visitStatus != null && visitStatus.WellKnownName != _visit.VisitStatus.WellKnownName )
            {
                var visitStatusUpdateDto = new VisitStatusUpdateDto
                    {
                        VisitKey = Visit.Key,
                        VisitStatus = visitStatus,
                        UpdateDateTime = DateTime.Now
                    };

                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( new UpdateVisitStatusRequest { VisitStatusUpdateDto = visitStatusUpdateDto } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleUpdateVisitStatusCompleted, HandleUpdateVisitStatusException );
            }
        }

        private bool CanExecuteStatusUpdatedCommand ( LookupValueDto visitStatus )
        {
            return _visit != null;
        }

        private void GetAllSchedulableActivityTypesCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAllSchedulableActivityTypesResponse> ();
            SchedulableActivityTypes = response.ActivityTypes.OrderBy ( at => at.Name ).ToList ();
        }

        private void GetLookupValueListCompleted ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            StatusList = new ObservableCollection<LookupValueDto> ( lookupValueLists[VisitStatusLookupName] );

            CheckStatus ();
        }

        private void GetStaffsByAgencyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetStaffsByAgencyResponse> ();
            var staffSummaryDtos = new ObservableCollection<StaffSummaryDto> ( response.Staffs );
            StaffList = staffSummaryDtos;
        }

        private void HandleAssociateProblemsWithVisitCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<AssociateProblemsWithVisitResponse> ();
            var problemDtos = new ObservableCollection<ProblemDto> ( response.ProblemDtos );

            ProblemList = problemDtos;
            VisitProblemChanged ();
            IsLoading = false;
        }

        private void HandleAssociateProblemsWithVisitException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not Associate problems.", UserDialogServiceOptions.Ok );
        }

        private void HandleDetachProblemFromVisitCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DetachProblemFromVisitResponse> ();
            var problemDtos = new ObservableCollection<ProblemDto> ( response.ProblemDtos );

            ProblemList = problemDtos;
            VisitProblemChanged ();
            IsLoading = false;
        }

        private void HandleDetachProblemFromVisitException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not detach problem.", UserDialogServiceOptions.Ok );
        }

        private void HandleGetVisitByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<VisitDto>> ();

            var activities = response.DataTransferObject.Activities.OrderBy ( a => a.ActivityType.Key );

            // Load the views inside Activity Region based on the list of activities for the current visit.
            foreach ( var activity in activities )
            {
                var activityViewName = _activityTypeToViewMapperService.GetViewNameFromActivityType ( activity.ActivityType.WellKnownName );

                // Current activity denotes one that caused navigation to the this view model. 
                // For example, via a Dashboard double click on a grid row denoting the activity and subsequent ExecuteGoToVisitCommand execution. 
                var isCurrentActivity = activity.Key == _currentActivityKey;

                if ( !string.IsNullOrWhiteSpace ( activityViewName ) )
                {
                    _navigationService.Navigate (
                        RegionManager,
                        "VisitRegion",
                        activityViewName,
                        "EditActivity",
                        new[]
                            {
                                new KeyValuePair<string, string> ( "IsCurrentActivity", isCurrentActivity.ToString () ),
                                new KeyValuePair<string, string> ( "ActivityKey", activity.Key.ToString () ),
                                new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () )
                            } );
                }
            }

            Visit = response.DataTransferObject;
            ProblemList = Visit.Problems;
            IsLoading = false;

            CheckStatus ();
        }

        private void HandleGetVisitByKeyException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get visit by key failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetStaffsByAgencyCompleted ( receivedResponses );
            GetLookupValueListCompleted ( receivedResponses );
            GetAllSchedulableActivityTypesCompleted ( receivedResponses );
            HandleGetVisitByKeyCompleted ( receivedResponses );
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Visit initialization failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleSaveScheduledActivityCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<ScheduleActivityResponse> ();
            var activityDto = response.Activity;

            if ( activityDto == null )
            {
                return;
            }

            if ( activityDto.DataErrorInfoCollection.Count () > 0 )
            {
                var errors = new StringBuilder ();
                activityDto.DataErrorInfoCollection.ForEach ( error => errors.AppendLine ( error.Message ) );
                _userDialogService.ShowDialog ( errors.ToString (), "Schedule Activity", UserDialogServiceOptions.Ok );
            }
            else
            {
                // Add the new Activity view tile.
                var activityViewName = _activityTypeToViewMapperService.GetViewNameFromActivityType ( activityDto.ActivityType.WellKnownName );

                if ( !string.IsNullOrWhiteSpace ( activityViewName ) )
                {
                    _navigationService.Navigate (
                        RegionManager,
                        "VisitRegion",
                        activityViewName,
                        "EditActivity",
                        new[]
                            {
                                new KeyValuePair<string, string> ( "VisitStatusWellKnownName", Visit.VisitStatus.WellKnownName ),
                                new KeyValuePair<string, string> ( "ActivityKey", activityDto.Key.ToString () ),
                                new KeyValuePair<string, string> ( "PatientKey", activityDto.PatientKey.ToString () )
                            } );

                    _eventAggregator.GetEvent<ActivityChangedEvent> ().Publish (
                        new ActivityChangedEventArgs
                            {
                                Sender = this,
                                VisitKey = activityDto.VisitKey,
                                ClinicalCaseKey = _clinicalCaseKey,
                                ClinicianKey = activityDto.ClinicianKey,
                                PatientKey = activityDto.PatientKey
                            } );
                }
            }
        }

        private void HandleSaveScheduledActivityDtoException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Scheduling Activity failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleSaveVisitDtoCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;

            var response = receivedResponses.Get<DtoResponse<VisitDto>> ();
            var currentVisit = response.DataTransferObject;

            Visit = currentVisit;
            RaiseVisitChanged ();
        }

        private void HandleSaveVisitDtoException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Visit save failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleUpdateVisitStatusCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<UpdateVisitStatusResponse> ();
            var visitStatusUpdateDto = response.VisitStatusUpdateDto;

            Visit.VisitStatus = visitStatusUpdateDto.VisitStatus;
            RaiseVisitChanged ();
            IsLoading = false;
        }

        private void HandleUpdateVisitStatusException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not update visit status.", UserDialogServiceOptions.Ok );
        }

        private void ProblemList_CollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.NewItems != null && e.NewItems.Count > 0 )
            {
                IList<ProblemDto> problems = new List<ProblemDto> ();

                foreach ( var item in e.NewItems )
                {
                    var problemDto = item as ProblemDto;
                    if ( problemDto != null )
                    {
                        problems.Add ( problemDto );
                    }
                }

                if ( problems.Count > 0 )
                {
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add ( new AssociateProblemsWithVisitRequest { ProblemDtos = problems, VisitKey = Visit.Key } );
                    IsLoading = true;
                    requestDispatcher.ProcessRequests ( HandleAssociateProblemsWithVisitCompleted, HandleAssociateProblemsWithVisitException );
                }
            }
        }

        private void RaiseVisitChanged ()
        {
            var visitChangedEventArgs = new VisitChangedEventArgs
                {
                    Sender = this,
                    ClinicianKey = Visit.Staff.Key,
                    VisitKey = Visit.Key,
                    VisitStartDateTime = Visit.AppointmentStartDateTime,
                    PatientKey = _patientKey,
                    VisitStatus = Visit.VisitStatus
                };

            _eventAggregator.GetEvent<VisitChangedEvent> ().Publish ( visitChangedEventArgs );
        }

        private void VisitProblemChanged ()
        {
            _navigationService.Navigate (
                RegionManager,
                "MedicationsAndProblemRegion",
                "CaseProblemListView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ),
                        new KeyValuePair<string, string> ( "ClinicalCaseKey", _visit.ClinicalCaseKey.ToString () )
                    } );
        }

        #endregion
    }
}
