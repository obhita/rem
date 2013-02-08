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
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;
using Rem.Ria.PatientModule.Web.Service;
using Rem.WellKnownNames.VisitModule;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// View Model for ClinicianScheduleTile class.
    /// </summary>
    public class ClinicianScheduleTileViewModel : NavigationViewModel, IViewRefreshable
    {
        #region Constants and Fields

        private static readonly DateTime DailyScheduleEndTime = new DateTime ( 2000, 1, 1, 17, 0, 0 );
        private static readonly DateTime DailyScheduleStartTime = new DateTime ( 2000, 1, 1, 8, 0, 0 );
        private static readonly int TimeSlotSizeInMinutes = 60;

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;
        private readonly INotificationService _notificationService;
        private readonly IUserDialogService _userDialogService;
        private long? _clinicianKey;

        private ClinicianScheduleDto _clinicianSchedule;
        private long _previousClinicanKey;
        private DateTime _selectedDate;
        private DateRange _selectedRange;
        private bool _sendDateChangeUpdates = true;
        private IList<LookupValueDto> _statusList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicianScheduleTileViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="notificationService">The notification service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        [InjectionConstructor]
        public ClinicianScheduleTileViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            INotificationService notificationService,
            INavigationService navigationService,
            ICommandFactory commandFactory,
            IPopupService popupService)
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _notificationService = notificationService;
            _navigationService = navigationService;
            _popupService = popupService;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AppointmentCreatedEvent> ().Subscribe (
                AppointmentCreatedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterAppointmentCreatedEvents );

            _eventAggregator.GetEvent<VisitChangedEvent> ().Subscribe (
                VisitChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterVisitChangedEvents );

            _eventAggregator.GetEvent<AppointmentUpdatedEvent> ().Subscribe (
                AppointmentUpdatedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterAppointmentUpdatedEvents );

            _eventAggregator.GetEvent<PatientChangedEvent> ().Subscribe (
                FilterPatientChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterPatientChangedEvents );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            StatusUpdatedCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                () => StatusUpdatedCommand, ExecuteStatusUpdatedCommand );
            AppointmentUpdatedCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                () => AppointmentUpdatedCommand,
                ExecuteAppointmentUpdatedCommand,
                CanExecuteAppointmentUpdatedCommand );
            ShowAppointmentDetailsCommand =
                commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                    () => ShowAppointmentDetailsCommand, ExecuteDoubleClickAppointmentCommand, CanExecuteDoubleClickAppointmentCommand );
            RangeUpdatedCommand = commandFactoryHelper.BuildDelegateCommand<DateRange> (
                () => RangeUpdatedCommand,
                ExecuteRangeUpdatedCommand,
                CanExecuteRangeUpdatedCommand );
            AppointmentAddedCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                () => AppointmentAddedCommand, ExecuteAppointmentAddedCommand, CanExecuteAppointmentAddedCommand );
            AppointmentDeletedCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                () => AppointmentDeletedCommand, ExecuteAppointmentDeletedCommand, CanExecuteAppointmentDeletedCommand );
            ShowAlertDetailsCommand = commandFactoryHelper.BuildDelegateCommand<PatientAlertDto> (
                () => ShowAlertDetailsCommand, ExecuteShowAlertDetailsCommand );
            GoToPatientDashboardCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                () => GoToPatientDashboardCommand, ExecuteGoToPatientDashboardCommand );
            GoToPatientProfileCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                () => GoToPatientProfileCommand, ExecuteGoToPatientProfileCommand);
            GoToPaymentCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianAppointmentDto> (
                () => GoToPaymentCommand, ExecuteGoToPaymentCommand );

            ClinicianSchedule = new ClinicianScheduleDto ();

            _eventAggregator.GetEvent<FrontDeskDashboardDateChangedEvent> ().Subscribe (
                OnFrontDeskDashboardDateChanged, ThreadOption.PublisherThread, false, FilterFrontDeskDashboardDateChanged );
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [refresh view].
        /// </summary>
        public event EventHandler<EventArgs> RefreshView = ( s, e ) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the appointment added command.
        /// </summary>
        public ICommand AppointmentAddedCommand { get; private set; }

        /// <summary>
        /// Gets the appointment deleted command.
        /// </summary>
        public ICommand AppointmentDeletedCommand { get; private set; }

        /// <summary>
        /// Gets the appointment updated command.
        /// </summary>
        public ICommand AppointmentUpdatedCommand { get; private set; }

        /// <summary>
        /// Gets the go to payment command.
        /// </summary>
        public ICommand GoToPaymentCommand { get; private set; }

        /// <summary>
        /// Gets or sets the clinician schedule.
        /// </summary>
        /// <value>The clinician schedule.</value>
        public ClinicianScheduleDto ClinicianSchedule
        {
            get { return _clinicianSchedule; }
            set
            {
                _clinicianSchedule = value;
                RaisePropertyChanged ( () => ClinicianSchedule );
            }
        }

        /// <summary>
        /// Gets the go to patient dashboard command.
        /// </summary>
        public ICommand GoToPatientDashboardCommand { get; private set; }

        /// <summary>
        /// Gets the go to patient profile command.
        /// </summary>
        public ICommand GoToPatientProfileCommand { get; private set; }

        /// <summary>
        /// Gets the range updated command.
        /// </summary>
        public ICommand RangeUpdatedCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        /// <value>The selected date.</value>
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { ApplyPropertyChange ( ref _selectedDate, () => SelectedDate, value ); }
        }

        /// <summary>
        /// Gets the show alert details command.
        /// </summary>
        public ICommand ShowAlertDetailsCommand { get; private set; }

        /// <summary>
        /// Gets the show appointment details command.
        /// </summary>
        public ICommand ShowAppointmentDetailsCommand { get; private set; }

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
        /// Sets a value indicating whether [stop listening to selected date changed events].
        /// </summary>
        /// <value><c>true</c> if [stop listening to selected date changed events]; otherwise, <c>false</c>.</value>
        public bool StopListeningToSelectedDateChangedEvents
        {
            set
            {
                _sendDateChangeUpdates = false;
                _eventAggregator.GetEvent<FrontDeskDashboardDateChangedEvent> ().Unsubscribe ( OnFrontDeskDashboardDateChanged );
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appointments the created event handler.
        /// </summary>
        /// <param name="appointmentCreatedEventArgs">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.AppointmentCreatedEventArgs"/> instance containing the event data.</param>
        public void AppointmentCreatedEventHandler ( AppointmentCreatedEventArgs appointmentCreatedEventArgs )
        {
            Deployment.Current.InvokeIfNeeded ( () => UpdateSchedule ( true ) );
        }

        /// <summary>
        /// Appointments the updated event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.AppointmentUpdatedEventArgs"/> instance containing the event data.</param>
        public void AppointmentUpdatedEventHandler ( AppointmentUpdatedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded ( () => UpdateSchedule ( true ) );
        }

        /// <summary>
        /// Determines whether this instance [can execute appointment added command] the specified clinician appointment dto.
        /// </summary>
        /// <param name="clinicianAppointmentDto">The clinician appointment dto.</param>
        /// <returns><c>true</c> if this instance [can execute appointment added command] the specified clinician appointment dto; otherwise, <c>false</c>.</returns>
        public bool CanExecuteAppointmentAddedCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            if ( clinicianAppointmentDto == null )
            {
                return false;
            }

            return IsScheduled ( clinicianAppointmentDto );
        }

        /// <summary>
        /// Determines whether this instance [can execute appointment deleted command] the specified clinician appointment dto.
        /// </summary>
        /// <param name="clinicianAppointmentDto">The clinician appointment dto.</param>
        /// <returns><c>true</c> if this instance [can execute appointment deleted command] the specified clinician appointment dto; otherwise, <c>false</c>.</returns>
        public bool CanExecuteAppointmentDeletedCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            if ( clinicianAppointmentDto == null )
            {
                return false;
            }

            return IsScheduled ( clinicianAppointmentDto );
        }

        /// <summary>
        /// Determines whether this instance [can execute appointment updated command] the specified clinician appointment dto.
        /// </summary>
        /// <param name="clinicianAppointmentDto">The clinician appointment dto.</param>
        /// <returns><c>true</c> if this instance [can execute appointment updated command] the specified clinician appointment dto; otherwise, <c>false</c>.</returns>
        public bool CanExecuteAppointmentUpdatedCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            if ( clinicianAppointmentDto == null )
            {
                return false;
            }

            return IsScheduled ( clinicianAppointmentDto ) && clinicianAppointmentDto.ClinicianKey == ClinicianSchedule.ClinicianKey;
        }

        /// <summary>
        /// Determines whether this instance [can execute double click appointment command] the specified clinician appointment dto.
        /// </summary>
        /// <param name="clinicianAppointmentDto">The clinician appointment dto.</param>
        /// <returns><c>true</c> if this instance [can execute double click appointment command] the specified clinician appointment dto; otherwise, <c>false</c>.</returns>
        public bool CanExecuteDoubleClickAppointmentCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            return ( clinicianAppointmentDto != null );
        }

        /// <summary>
        /// Executes the appointment updated command.
        /// </summary>
        /// <param name="clinicianAppointmentDto">The clinician appointment dto.</param>
        public void ExecuteAppointmentUpdatedCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            if ( clinicianAppointmentDto != null )
            {
                UpdateClinicianAppointmentAsync ( ClinicianSchedule.ClinicianKey, clinicianAppointmentDto );
            }
            else
            {
                _userDialogService.ShowDialog (
                    "No Appointment was sent, couldn't update appointment.",
                    "Updating Appointment Failed",
                    UserDialogServiceOptions.Ok );
            }
        }

        /// <summary>
        /// Filters the appointment created events.
        /// </summary>
        /// <param name="appointmentCreatedEventArgs">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.AppointmentCreatedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterAppointmentCreatedEvents ( AppointmentCreatedEventArgs appointmentCreatedEventArgs )
        {
            return
                appointmentCreatedEventArgs.ClinicianKey == ClinicianSchedule.ClinicianKey;
        }

        /// <summary>
        /// Filters the appointment updated events.
        /// </summary>
        /// <param name="appointmentUpdatedEventArgs">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.AppointmentUpdatedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterAppointmentUpdatedEvents ( AppointmentUpdatedEventArgs appointmentUpdatedEventArgs )
        {
            return appointmentUpdatedEventArgs.ClinicianKey == ClinicianSchedule.ClinicianKey;
        }

        /// <summary>
        /// Filters the front desk dashboard date changed.
        /// </summary>
        /// <param name="frontDeskDashboardDateChangedEventArgs">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.FrontDeskDashboardDateChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterFrontDeskDashboardDateChanged ( FrontDeskDashboardDateChangedEventArgs frontDeskDashboardDateChangedEventArgs )
        {
            return frontDeskDashboardDateChangedEventArgs.Source != this
                   && CurrentUserContext.Location.Key == frontDeskDashboardDateChangedEventArgs.LocationKey;
        }

        /// <summary>
        /// Filters the patient changed event handler.
        /// </summary>
        /// <param name="patientChangedEventArgs">The <see cref="Rem.Ria.PatientModule.PatientChangedEventArgs"/> instance containing the event data.</param>
        public void FilterPatientChangedEventHandler ( PatientChangedEventArgs patientChangedEventArgs )
        {
            Deployment.Current.InvokeIfNeeded ( () => UpdateSchedule ( true ) );
        }

        /// <summary>
        /// Filters the patient changed events.
        /// </summary>
        /// <param name="patientChangedEventArgs">The <see cref="Rem.Ria.PatientModule.PatientChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterPatientChangedEvents ( PatientChangedEventArgs patientChangedEventArgs )
        {
            return ClinicianSchedule.ClinicianAppointments.Where ( ca => ca.PatientKey == patientChangedEventArgs.PatientKey ).Any ();
        }

        /// <summary>
        /// Filters the visit changed events.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterVisitChangedEvents ( VisitChangedEventArgs visitChangedEventArgs )
        {
            if ( visitChangedEventArgs.Sender == this )
            {
                return false;
            }
            var isVisitChanged = visitChangedEventArgs.ClinicianKey == ClinicianSchedule.ClinicianKey ||
                                  ClinicianSchedule.ClinicianAppointments.Any (
                                      a => a.Key == visitChangedEventArgs.VisitKey );
            return isVisitChanged &&
                   visitChangedEventArgs.VisitStartDateTime.Date == SelectedDate;
        }

        /// <summary>
        /// Raises the <see cref="E:FrontDeskDashboardDateChanged"/> event.
        /// </summary>
        /// <param name="frontDeskDashboardDateChangedEventArgs">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.FrontDeskDashboardDateChangedEventArgs"/> instance containing the event data.</param>
        public void OnFrontDeskDashboardDateChanged ( FrontDeskDashboardDateChangedEventArgs frontDeskDashboardDateChangedEventArgs )
        {
            SelectedDate = frontDeskDashboardDateChangedEventArgs.Date.Date;
            if ( _selectedRange.StartDate.Date != _selectedRange.EndDate.Date &&
                 ( _selectedRange.StartDate.Date > frontDeskDashboardDateChangedEventArgs.Date.Date ||
                   _selectedRange.EndDate.Date < frontDeskDashboardDateChangedEventArgs.Date.Date ) )
            {
                _selectedRange.StartDate =
                    frontDeskDashboardDateChangedEventArgs.Date.GetFirstDayOfWeek ();
                _selectedRange.EndDate =
                    _selectedRange.StartDate.AddDays ( 6 );
            }
            UpdateSchedule ( true );
        }

        /// <summary>
        /// Visits the changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        public void VisitChangedEventHandler ( VisitChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded ( () => UpdateSchedule ( true ) );
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
            var clinicianKey = parameters.GetValue<long> ( "ClinicianKey" );
            return _clinicianKey == clinicianKey;
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
            var clinicianKey = parameters.GetValue<long> ( "ClinicianKey" );
            var initialDate = parameters.GetValue<DateTime> ( "SelectedDate" );
            _clinicianKey = clinicianKey == 0 ? CurrentUserContext.Staff.Key : clinicianKey;

            SelectedDate = initialDate.Date;
            _selectedRange = new DateRange
                {
                    StartDate = SelectedDate,
                    EndDate = SelectedDate
                };
            UpdateSchedule ( true );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.AddLookupValuesRequest ( "VisitStatus" );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetLookupValuesCompleted, HandleGetLookupValuesException );
        }

        private bool CanExecuteRangeUpdatedCommand ( DateRange dateRange )
        {
            return dateRange != null;
        }

        private void ChangeDate ( DateTime newDate )
        {
            if ( _sendDateChangeUpdates )
            {
                _eventAggregator.GetEvent<FrontDeskDashboardDateChangedEvent> ().Publish (
                    new FrontDeskDashboardDateChangedEventArgs
                        {
                            Source = this,
                            Date = newDate,
                            LocationKey = CurrentUserContext.Location.Key
                        } );
            }
        }

        private void DeleteClinicianAppointmentAsync ( long clinicianKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new DeleteClinicianAppointmentRequest { ClinicianKey = clinicianKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleDeleteClinicianAppointmentCompleted, HandleDeleteClinicianAppointmentException );
        }

        private void ExecuteAppointmentAddedCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            if ( clinicianAppointmentDto.Key != 0 )
            {
                _previousClinicanKey = clinicianAppointmentDto.ClinicianKey;
                UpdateClinicianAppointmentAsync ( ClinicianSchedule.ClinicianKey, clinicianAppointmentDto );

                _eventAggregator.GetEvent<AppointmentCreatedEvent> ().Publish (
                    new AppointmentCreatedEventArgs
                        {
                            AppointmentKey =
                                clinicianAppointmentDto.Key,
                            ClinicianKey =
                                ClinicianSchedule.ClinicianKey,
                            EndDateTime =
                                clinicianAppointmentDto.AppointmentEndDateTime,
                            PatientKey =
                                clinicianAppointmentDto.PatientKey,
                            StartDateTime =
                                clinicianAppointmentDto.AppointmentStartDateTime
                        } );
            }
            else
            {
                //TODO: fix this to somehow be based off of how timeslots are setup
                var startTime = new DateTime (
                    clinicianAppointmentDto.Start.Year,
                    clinicianAppointmentDto.Start.Month,
                    clinicianAppointmentDto.Start.Day,
                    clinicianAppointmentDto.AppointmentStartDateTime.Hour,
                    0,
                    0 );
                var parameters = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string> ( "PatientKey", clinicianAppointmentDto.PatientKey.ToString () ),
                        new KeyValuePair<string, string> ( "PatientFirstName", clinicianAppointmentDto.PatientFirstName ),
                        new KeyValuePair<string, string> ( "PatientLastName", clinicianAppointmentDto.PatientLastName ),
                        new KeyValuePair<string, string> ( "StartDateTime", startTime.ToString () ),
                        new KeyValuePair<string, string> ( "EndTime", startTime.AddMinutes ( 59 ).ToString () ),
                        new KeyValuePair<string, string> ( "ClinicianKey", ClinicianSchedule.ClinicianKey.ToString () ),
                        new KeyValuePair<string, string> ( "ShouldReload", false.ToString () )
                    };
                if ( clinicianAppointmentDto.VisitTemplateKey.HasValue )
                {
                    parameters.Add (
                        new KeyValuePair<string, string> ( "VisitTemplateKey", clinicianAppointmentDto.VisitTemplateKey.Value.ToString () ) );
                }
                _navigationService.NavigateToActiveView ( "WorkspacesRegion", "CreateAppointment", parameters.ToArray () );
                ClinicianSchedule.ClinicianAppointments.Remove ( clinicianAppointmentDto );
            }
        }

        private void ExecuteAppointmentDeletedCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            if ( clinicianAppointmentDto != null )
            { 
                _previousClinicanKey = clinicianAppointmentDto.ClinicianKey;
                DeleteClinicianAppointmentAsync ( clinicianAppointmentDto.Key );
            }
            else
            {
                _userDialogService.ShowDialog (
                    "No Appointment was sent, couldn't delete appointment.",
                    "Deleting Appointment Failed",
                    UserDialogServiceOptions.Ok );
            }
        }

        private void ExecuteDoubleClickAppointmentCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            _popupService.ShowPopup (
                "AppointmentDetailsView",
                "Edit",
                "Appointment Details",
                new[] { new KeyValuePair<string, string> ( "VisitKey", clinicianAppointmentDto.Key.ToString () ) } );
        }

        private void ExecuteGoToPatientDashboardCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "PatientWorkspaceView",
                "ViewPatient",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", clinicianAppointmentDto.PatientKey.ToString () ),
                        new KeyValuePair<string, string> (
                            "FullName", clinicianAppointmentDto.PatientFirstName + " " + clinicianAppointmentDto.PatientLastName ),
                        new KeyValuePair<string, string> ( "SubViewName", "PatientDashboardView" ),
                    } );
        }

        private void ExecuteGoToPatientProfileCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "PatientWorkspaceView",
                "ViewPatient",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", clinicianAppointmentDto.PatientKey.ToString () ),
                        new KeyValuePair<string, string> (
                            "FullName", clinicianAppointmentDto.PatientFirstName + " " + clinicianAppointmentDto.PatientLastName ),
                        new KeyValuePair<string, string> ( "SubViewName", "PatientEditorView" ),
                    } );
        }

        private void ExecuteGoToPaymentCommand(ClinicianAppointmentDto clinicianAppointmentDto)
        {
            _navigationService.Navigate (
                RegionManager,
                "FrontDeskMainRegion",
                "BillingView",
                "MakePayment",
                new KeyValuePair<string, string> ( "PatientKey", clinicianAppointmentDto.PatientKey.ToString () ),
                new KeyValuePair<string, string> (
                    "PatientFullName", string.Format ( "{0} {1}", clinicianAppointmentDto.PatientFirstName, clinicianAppointmentDto.PatientLastName ) ) );
        }

        private void ExecuteRangeUpdatedCommand ( DateRange dateRange )
        {
            _selectedRange = dateRange;
            UpdateSchedule ( false );
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

        private void ExecuteStatusUpdatedCommand ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            var visitStatusUpdateDto = new VisitStatusUpdateDto
                {
                    VisitKey = clinicianAppointmentDto.Key,
                    VisitStatus = clinicianAppointmentDto.VisitStatus,
                    UpdateDateTime = DateTime.Now
                };

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new UpdateVisitStatusRequest { VisitStatusUpdateDto = visitStatusUpdateDto } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleUpdateVisitStatusCompleted, HandleUpdateVisitStatusException );

            RefreshView ( this, null );
        }

        private void HandleDeleteClinicianAppointmentCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DeleteClinicianAppointmentResponse> ();

            var clinicianAppointmentDto = response.ClinicianAppointmentDto;
            if ( clinicianAppointmentDto != null )
            {
                RaiseVisitChanged ( clinicianAppointmentDto );
            }
            ClinicianSchedule.AvailableAppointments =
                ClinicianSchedule.TotalAppointments - ClinicianSchedule.ClinicianAppointments.Count;

            if ( _previousClinicanKey != 0 )
            {
                var appointmentUpdatedEventArgs = new AppointmentUpdatedEventArgs
                    {
                        ClinicianKey = _previousClinicanKey
                    };
                _eventAggregator.GetEvent<AppointmentUpdatedEvent> ().Publish ( appointmentUpdatedEventArgs );
            }
            IsLoading = false;
        }

        private void HandleDeleteClinicianAppointmentException ( ExceptionInfo ex )
        {
            UpdateSchedule ( true );
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "DeleteClinicianAppointmentCompleted", UserDialogServiceOptions.Ok );

            if (_previousClinicanKey != 0)
            {
                var appointmentUpdatedEventArgs = new AppointmentUpdatedEventArgs
                    {
                        ClinicianKey = _previousClinicanKey
                    };
                _eventAggregator.GetEvent<AppointmentUpdatedEvent>().Publish(appointmentUpdatedEventArgs);
            }
        }

        private void HandleGetClinicianScheduleByClinicianKeyAndDateRangeCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetClinicianScheduleByClinicianKeyAndDateRangeResponse> ();

            ClinicianSchedule = response.ClinicianScheduleDto;
            IsLoading = false;
        }

        private void HandleGetClinicianScheduleByClinicianKeyAndDateRangeException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog (
                ex.Message,
                "GetClinicianScheduleByClinicianKeyAndDateRangeCompleted",
                UserDialogServiceOptions.Ok );
        }

        private void HandleGetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            StatusList = new ObservableCollection<LookupValueDto> ( lookupValueLists["VisitStatus"] );
            IsLoading = false;
        }

        private void HandleGetLookupValuesException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get lookup values failed", UserDialogServiceOptions.Ok );
        }

        private void HandleUpdateClinicianAppointmentCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<UpdateClinicianAppointmentResponse> ();

            var clinicianAppointmentDto = response.ClinicianAppointmentDto;
            RaiseVisitChanged ( clinicianAppointmentDto );
            ClinicianSchedule.AvailableAppointments =
                ClinicianSchedule.TotalAppointments - ClinicianSchedule.ClinicianAppointments.Count;

            if ( _previousClinicanKey != 0 )
            {
                var appointmentUpdatedEventArgs = new AppointmentUpdatedEventArgs
                    {
                        ClinicianKey = _previousClinicanKey
                    };
                _eventAggregator.GetEvent<AppointmentUpdatedEvent> ().Publish ( appointmentUpdatedEventArgs );
            }
            IsLoading = false;
        }

        private void HandleUpdateClinicianAppointmentException ( ExceptionInfo ex )
        {
            UpdateSchedule ( true );
            _userDialogService.ShowDialog ( ex.Message, "UpdateClinicianAppointmentCompleted", UserDialogServiceOptions.Ok );

            if ( _previousClinicanKey != 0 )
            {
                var appointmentUpdatedEventArgs = new AppointmentUpdatedEventArgs
                    {
                        ClinicianKey = _previousClinicanKey
                    };
                _eventAggregator.GetEvent<AppointmentUpdatedEvent> ().Publish ( appointmentUpdatedEventArgs );
            }
        }

        private void HandleUpdateVisitStatusCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<UpdateVisitStatusResponse> ();
            var visitStatusUpdateDto = response.VisitStatusUpdateDto;

            var clinicianAppointmentDto =
                ClinicianSchedule.ClinicianAppointments.FirstOrDefault ( a => a.Key == visitStatusUpdateDto.VisitKey );
            if ( clinicianAppointmentDto != null )
            {
                clinicianAppointmentDto.VisitStatus = visitStatusUpdateDto.VisitStatus;
                RaiseVisitChanged ( clinicianAppointmentDto );
            }
            IsLoading = false;
        }

        private void HandleUpdateVisitStatusException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "UpdateVisitStatusCompleted:Could not update visit status", UserDialogServiceOptions.Ok );
        }

        private bool IsScheduled ( ClinicianAppointmentDto dto )
        {
            return dto != null
                   &&
                   ( dto.VisitStatus == null
                     || dto.VisitStatus.WellKnownName.Equals ( VisitStatus.Scheduled, StringComparison.InvariantCultureIgnoreCase ) );
        }

        private void RaiseVisitChanged ( ClinicianAppointmentDto clinicianAppointmentDto )
        {
            _eventAggregator.GetEvent<VisitChangedEvent> ().Publish (
                new VisitChangedEventArgs
                    {
                        Sender = this,
                        ClinicianKey = ClinicianSchedule.ClinicianKey,
                        VisitKey = clinicianAppointmentDto.Key,
                        VisitStartDateTime = clinicianAppointmentDto.AppointmentStartDateTime,
                        PatientKey = clinicianAppointmentDto.PatientKey
                    } );
        }

        private void UpdateClinicianAppointmentAsync ( long clinicianKey, ClinicianAppointmentDto clinicianAppointmentDto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new UpdateClinicianAppointmentRequest { ClinicianKey = clinicianKey, ClinicianAppointmentDto = clinicianAppointmentDto } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleUpdateClinicianAppointmentCompleted, HandleUpdateClinicianAppointmentException );
        }

        private void UpdateSchedule ( bool forceRefresh )
        {
            if ( _clinicianKey.HasValue )
            {
                if ( ClinicianSchedule != null && !forceRefresh )
                {
                    if ( _selectedRange.StartDate.Date == _selectedRange.EndDate.Date )
                    {
                        ChangeDate ( _selectedRange.StartDate );
                    }
                    else if ( _selectedRange.StartDate.Date > SelectedDate ||
                              _selectedRange.EndDate.Date < SelectedDate )
                    {
                        _selectedRange.StartDate = SelectedDate.GetFirstDayOfWeek ();
                        _selectedRange.EndDate = _selectedRange.StartDate.AddDays ( 6 );
                    }
                }
                else if ( forceRefresh && _selectedRange.StartDate.Date == _selectedRange.EndDate.Date )
                {
                    _selectedRange = new DateRange
                        {
                            StartDate = SelectedDate,
                            EndDate = SelectedDate
                        };
                }

                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add (
                    new GetClinicianScheduleByClinicianKeyAndDateRangeRequest
                        {
                            ClinicianKey = _clinicianKey.Value,
                            StartDate = _selectedRange.StartDate,
                            EndDate = _selectedRange.EndDate,
                            SlotSizeInMinutes = TimeSlotSizeInMinutes,
                            BeginTime = DailyScheduleStartTime,
                            EndTime = DailyScheduleEndTime
                        } );
                IsLoading = true;
                requestDispatcher.ProcessRequests (
                    HandleGetClinicianScheduleByClinicianKeyAndDateRangeCompleted, HandleGetClinicianScheduleByClinicianKeyAndDateRangeException );
            }
        }

        #endregion
    }
}
