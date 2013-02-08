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
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;
using Rem.Ria.PatientModule.Web.Service;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// View Model for AppointmentDetails class.
    /// </summary>
    public class AppointmentDetailsViewModel : PanelEditorViewModel<AppointmentDetailsDto>
    {
        #region Constants and Fields

        private const string PatientWorkspaceView = "PatientWorkspaceView";
        private const string WorkspacesRegion = "WorkspacesRegion";

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private ObservableCollection<TimeSlotDto> _availableTimeSlots;
        private ObservableCollection<StaffNameDto> _clinicians;
        private EditableDtoWrapper _editingWrapper;
        private bool _gotDto;
        private bool _isCreating;
        private bool _isEdit;
        private bool _isUpdating;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private TimeSlotDto _selectedTimeSlot;
        private ObservableCollection<VisitTemplateDto> _visitTemplates;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentDetailsViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="popupService">The popup service.</param>
        [InjectionConstructor]
        public AppointmentDetailsViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory,
            IPopupService popupService)
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _popupService = popupService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ViewProfileCommand = commandFactoryHelper.BuildDelegateCommand ( () => ViewProfileCommand, ExecuteViewProfileCommand );
            StatusChangedCommand = commandFactoryHelper.BuildDelegateCommand<LookupValueDto> (
                () => StatusChangedCommand, ExecuteStatusChangedCommand );

            _eventAggregator.GetEvent<VisitChangedEvent> ().Subscribe (
                VisitChangedEventHandler,
                ThreadOption.PublisherThread,
                true,
                FilterVisitChangedEvents );

            _eventAggregator.GetEvent<PatientChangedEvent> ().Subscribe (
                PatientChangedEventHandler,
                ThreadOption.PublisherThread,
                false,
                FilterPatientChangedEvents );

            CreateAppointmentCommand = NavigationCommandManager.BuildCommand ( () => CreateAppointmentCommand, NavigateToCreateAppointmentCommand );
            EditCommand = NavigationCommandManager.BuildCommand ( () => EditCommand, NavigateToEditCommand );

            EditingWrapper = new EditableDtoWrapper ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the available time slots.
        /// </summary>
        /// <value>The available time slots.</value>
        public ObservableCollection<TimeSlotDto> AvailableTimeSlots
        {
            get { return _availableTimeSlots; }
            set { ApplyPropertyChange ( ref _availableTimeSlots, () => AvailableTimeSlots, value ); }
        }

        /// <summary>
        /// Gets or sets the clinicians.
        /// </summary>
        /// <value>The clinicians.</value>
        public ObservableCollection<StaffNameDto> Clinicians
        {
            get { return _clinicians; }
            set { ApplyPropertyChange ( ref _clinicians, () => Clinicians, value ); }
        }

        /// <summary>
        /// Gets the create appointment command.
        /// </summary>
        public INavigationCommand CreateAppointmentCommand { get; private set; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        public INavigationCommand EditCommand { get; private set; }

        /// <summary>
        /// Gets or sets the editing wrapper.
        /// </summary>
        /// <value>The editing wrapper.</value>
        public EditableDtoWrapper EditingWrapper
        {
            get { return _editingWrapper; }
            set { ApplyPropertyChange ( ref _editingWrapper, () => EditingWrapper, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is create.
        /// </summary>
        public bool IsCreate
        {
            get { return EditingDto == null || EditingDto.Key == 0; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is edit.
        /// </summary>
        /// <value><c>true</c> if this instance is edit; otherwise, <c>false</c>.</value>
        public bool IsEdit
        {
            get { return _isEdit; }
            set { ApplyPropertyChange ( ref _isEdit, () => IsEdit, value ); }
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
        /// Gets or sets the selected time slot.
        /// </summary>
        /// <value>The selected time slot.</value>
        public TimeSlotDto SelectedTimeSlot
        {
            get { return _selectedTimeSlot; }
            set
            {
                if ( _selectedTimeSlot != value )
                {
                    _selectedTimeSlot = value;
                    RaisePropertyChanged ( () => SelectedTimeSlot );
                    if ( _selectedTimeSlot == null )
                    {
                        EditingDto.AppointmentEndDateTime = null;
                    }
                    else
                    {
                        EditingDto.AppointmentStartDateTime = value.StartTime;
                        EditingDto.AppointmentEndDateTime = value.EndTime;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the status changed command.
        /// </summary>
        public ICommand StatusChangedCommand { get; private set; }

        /// <summary>
        /// Gets the view profile command.
        /// </summary>
        public ICommand ViewProfileCommand { get; private set; }

        /// <summary>
        /// Gets or sets the visit templates.
        /// </summary>
        /// <value>The visit templates.</value>
        public ObservableCollection<VisitTemplateDto> VisitTemplates
        {
            get { return _visitTemplates; }
            set { ApplyPropertyChange ( ref _visitTemplates, () => VisitTemplates, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the patient changed events.
        /// </summary>
        /// <param name="patientChangedEventArgs">The <see cref="Rem.Ria.PatientModule.PatientChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterPatientChangedEvents ( PatientChangedEventArgs patientChangedEventArgs )
        {
            return EditingDto.PatientKey == patientChangedEventArgs.PatientKey && EditingDto.Key > 0;
        }

        /// <summary>
        /// Patients the changed event handler.
        /// </summary>
        /// <param name="patientChangedEventArgs">The <see cref="Rem.Ria.PatientModule.PatientChangedEventArgs"/> instance containing the event data.</param>
        public void PatientChangedEventHandler ( PatientChangedEventArgs patientChangedEventArgs )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<AppointmentDetailsDto> ( EditingDto.Key ) );
            requestDispatcher.ProcessRequests ( GetAppointmentDetailsRequestCompleted, HandleRequestDispatcherException );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can execute save command] the specified dto.
        /// </summary>
        /// <param name="dto">The dto to check.</param>
        /// <returns><c>true</c> if this instance [can execute save command] the specified dto; otherwise, <c>false</c>.</returns>
        protected override bool CanExecuteSaveCommand ( KeyedDataTransferObject dto )
        {
            return CanSave ();
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
        /// Executes the cancel command.
        /// </summary>
        /// <param name="dto">The dto to cancel edit changes.</param>
        protected override void ExecuteCancelCommand ( KeyedDataTransferObject dto )
        {
            if ( EditingDto.EditStatus == EditStatus.Create )
            {
                _popupService.ClosePopup ( "AppointmentDetailsView" );
            }
            else
            {
                base.ExecuteCancelCommand ( dto );
            }
        }

        /// <summary>
        /// Executes the save command.
        /// </summary>
        /// <param name="dto">The dto to save changes.</param>
        protected override void ExecuteSaveCommand ( KeyedDataTransferObject dto )
        {
            IsLoading = true;
            base.ExecuteSaveCommand ( dto );
            if ( EditingDto.EditStatus == EditStatus.Create )
            {
                _isCreating = true;
            }
            else if ( EditingDto.EditStatus == EditStatus.Update )
            {
                _isUpdating = true;
            }
            IsLoading = false;
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            if ( EditingDto != null )
            {
                EditingDto.PropertyChanged -= HandleDtoPropertyChanged;
            }

            base.RequestCompleted ( receivedResponses );

            if ( EditingDto != null )
            {
                EditingDto.PropertyChanged += HandleDtoPropertyChanged;
            }

            IsEdit = false;
            if ( _isCreating )
            {
                _isCreating = false;
                if ( EditingDto.ClinicianKey != null && EditingDto.AppointmentStartDateTime != null && EditingDto.AppointmentEndDateTime != null )
                {
                    var appointmentCreatedEvent = new AppointmentCreatedEventArgs
                        {
                            AppointmentKey = EditingDto.AppointmentKey,
                            PatientKey = EditingDto.PatientKey,
                            ClinicianKey = EditingDto.ClinicianKey.Value,
                            StartDateTime = EditingDto.AppointmentStartDateTime.Value,
                            EndDateTime = EditingDto.AppointmentEndDateTime.Value
                        };
                    _eventAggregator.GetEvent<AppointmentCreatedEvent> ().Publish ( appointmentCreatedEvent );
                }
            }
            else if ( _isUpdating )
            {
                if ( EditingDto.ClinicianKey != null )
                {
                    var appointmentUpdatedEvent = new AppointmentUpdatedEventArgs
                        {
                            ClinicianKey = EditingDto.ClinicianKey.Value
                        };
                    _eventAggregator.GetEvent<AppointmentUpdatedEvent> ().Publish ( appointmentUpdatedEvent );
                }
            }
            RaisePropertyChanged ( () => IsCreate );
            RaiseVisitChanged ();

            EditingWrapper.EditableDto = EditingDto;
        }

        private bool CanSave ()
        {
            var canSave = EditingDto != null &&
                           EditingDto.PatientKey > 0 &&
                           EditingDto.ClinicianKey > 0 &&
                           EditingDto.Location != null &&
                           ( EditingDto.AppointmentStartDateTime.HasValue && EditingDto.AppointmentStartDateTime.Value != DateTime.MinValue ) &&
                           ( EditingDto.AppointmentEndDateTime.HasValue
                             && EditingDto.AppointmentEndDateTime.Value > EditingDto.AppointmentStartDateTime.Value ) &&
                           ( EditingDto.Key > 0 || EditingDto.VisitTemplateKey > 0 );
            return canSave;
        }

        private void CheckAutoSave ()
        {
            if ( CanSave () )
            {
                IsEdit = false;
                SaveCommand.Execute ( EditingDto );
            }
        }

        private void ExecuteStatusChangedCommand ( LookupValueDto visitStatus )
        {
            var visitStatusUpdateDto = new VisitStatusUpdateDto
                {
                    VisitKey = EditingDto.Key,
                    VisitStatus = visitStatus,
                    UpdateDateTime = DateTime.Now
                };

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new UpdateVisitStatusRequest { VisitStatusUpdateDto = visitStatusUpdateDto } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleUpdateVisitStatusCompleted, HandleUpdateVisitStatusException );
        }

        private void ExecuteViewProfileCommand ()
        {
            _navigationService.Navigate (
                WorkspacesRegion,
                PatientWorkspaceView,
                "ViewPatient",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", EditingDto.PatientKey.ToString () ),
                        new KeyValuePair<string, string> ( "FullName", EditingDto.PatientFirstName + " " + EditingDto.PatientLastName ),
                        new KeyValuePair<string, string> ( "SubViewName", "PatientEditorView" ),
                    } );
            _popupService.ClosePopup ( "AppointmentDetailsView" );
        }

        private bool FilterVisitChangedEvents ( VisitChangedEventArgs visitChangedEventArgs )
        {
            return visitChangedEventArgs.Sender != this &&
                   EditingDto.Key == visitChangedEventArgs.VisitKey;
        }

        private void GetAppointmentDetailsRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<AppointmentDetailsDto>> ();
            if ( EditingDto != null )
            {
                EditingDto.PropertyChanged -= HandleDtoPropertyChanged;
            }
            EditingDto = response.DataTransferObject;
            EditingWrapper.EditableDto = EditingDto;
            if ( EditingDto != null )
            {
                if ( EditingDto.ClinicianKey.HasValue && EditingDto.AppointmentStartDateTime.HasValue )
                {
                    GetAvailableTimeSlotsAsync ( EditingDto.ClinicianKey.Value, EditingDto.AppointmentStartDateTime.Value, EditingDto.AppointmentKey );
                }

                EditingDto.PropertyChanged += HandleDtoPropertyChanged;
            }

            RaisePropertyChanged ( () => IsCreate );
        }

        private void GetAvailableTimeSlotsAsync ( long clinicalKey, DateTime scheduledStartDateTime, long? appointmentKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new GetAvailableTimeSlotsRequest
                    {
                        ClinicianKey = clinicalKey,
                        Date = scheduledStartDateTime,
                        AppointmentKey = appointmentKey
                    } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( GetAvailableTimeSlotsCompleted, HandleRequestDispatcherException );
        }

        private void GetAvailableTimeSlotsCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAvailableTimeSlotsResponse> ();
            var availableTimeSlots = new ObservableCollection<TimeSlotDto> ( response.TimeSlots );

            AvailableTimeSlots = availableTimeSlots;

            if ( EditingDto.AppointmentStartDateTime != null )
            {
                SelectedTimeSlot =
                    availableTimeSlots.FirstOrDefault ( ts => ts.StartTime.TimeOfDay == EditingDto.AppointmentStartDateTime.Value.TimeOfDay );
            }

            IsLoading = false;
        }

        private void GetCliniciansCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetCliniciansByLocationKeyResponse> ();
            Clinicians = new ObservableCollection<StaffNameDto> ( response.Clinicians );
        }

        private void GetLookupsCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void GetVisitTemplatesCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetVisitTemplatesByAgencyKeyResponse> ();
            VisitTemplates = new ObservableCollection<VisitTemplateDto> ( response.VisitTemplates );
        }


        private void  GetPatientSummaryDtoCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<PatientSummaryDto>> ();
            var patient = response.DataTransferObject;
            EditingDto.PatientFirstName = patient.FirstName;
            EditingDto.PatientLastName = patient.LastName;
            EditingDto.PatientDateOfBirth = patient.BirthDate;
            EditingDto.PatientGender = patient.Gender;
            EditingDto.PatientUniqueIdentifier = patient.UniqueIdentifier;
        }

        private void HandleDtoPropertyChanged ( object sender, PropertyChangedEventArgs e )
        {
            ( ( DelegateCommandBase )SaveCommand ).RaiseCanExecuteChanged ();
            var getAvailableSlots = false;
            if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => EditingDto.AppointmentStartDateTime )
                 && EditingDto.AppointmentStartDateTime.HasValue )
            {
                if ( AvailableTimeSlots != null && AvailableTimeSlots.Any ( p => p.StartTime.Date == EditingDto.AppointmentStartDateTime.Value.Date ) )
                {
                    var timeslot =
                        AvailableTimeSlots.FirstOrDefault ( ts => ts.StartTime.TimeOfDay == EditingDto.AppointmentStartDateTime.Value.TimeOfDay );
                    SelectedTimeSlot = timeslot;
                }
                else
                {
                    getAvailableSlots = true;
                }
            }
            if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => EditingDto.ClinicianKey ) )
            {
                getAvailableSlots = true;
            }
            if ( getAvailableSlots && EditingDto.ClinicianKey.HasValue && EditingDto.AppointmentStartDateTime.HasValue )
            {
                GetAvailableTimeSlotsAsync ( EditingDto.ClinicianKey.Value, EditingDto.AppointmentStartDateTime.Value, EditingDto.AppointmentKey );
            }
        }

        private void HandleRequest ( ReceivedResponses receivedResponses )
        {
            if ( _gotDto )
            {
                GetAppointmentDetailsRequestCompleted ( receivedResponses );
            }
            GetLookupsCompleted ( receivedResponses );
            GetCliniciansCompleted ( receivedResponses );
            GetVisitTemplatesCompleted ( receivedResponses );

            if (!_gotDto)
            {
                GetPatientSummaryDtoCompleted ( receivedResponses );
            }

            IsLoading = false;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Patient Current Status operation failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleUpdateVisitStatusCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<UpdateVisitStatusResponse> ();

            var visitStatusUpdateDto = response.VisitStatusUpdateDto;
            EditingDto.VisitStatus = visitStatusUpdateDto.VisitStatus;
            RaiseVisitChanged ();
            IsLoading = false;
        }

        private void HandleUpdateVisitStatusException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "UpdateVisitStatusCompleted: Could not update visit status", UserDialogServiceOptions.Ok );
        }

        private void NavigateToCreateAppointmentCommand ( KeyValuePair<string, string>[] parameters )
        {
            if ( EditingDto != null )
            {
                EditingDto.PropertyChanged -= HandleDtoPropertyChanged;
            }
            EditingDto = new AppointmentDetailsDto ();
            EditingDto.PropertyChanged += HandleDtoPropertyChanged;
            EditingDto.PatientKey = parameters.GetValue<long> ( "PatientKey" );

            EditingDto.VisitTemplateKey = parameters.GetValue<long> ( "VisitTemplateKey" );
            var startDateTime = parameters.GetValue<DateTime> ( "StartDateTime" );
            if ( startDateTime != default( DateTime ) )
            {
                EditingDto.AppointmentStartDateTime = startDateTime;
            }
            var endDateTime = parameters.GetValue<DateTime> ( "EndTime" );
            if ( endDateTime != default( DateTime ) )
            {
                EditingDto.AppointmentEndDateTime = endDateTime;
            }
            EditingDto.ClinicianKey = parameters.GetValue<long> ( "ClinicianKey" );
            EditingDto.Location = new LocationDisplayNameDto
                {
                    Key = CurrentUserContext.Location.Key,
                    DisplayName = CurrentUserContext.Location.DisplayName
                };
            EditingWrapper.EditableDto = EditingDto;
            SelectedTimeSlot = null;
            IsEdit = true;
            RaisePropertyChanged ( () => IsCreate );
            CheckAutoSave ();

            IsLoading = true;
            var requestDispatcher = SetupRequestDispatcher();
            requestDispatcher.Add ( new GetDtoRequest<PatientSummaryDto> {Key = EditingDto.PatientKey} );
            requestDispatcher.ProcessRequests ( HandleRequest, HandleRequestDispatcherException );
        }

        private void NavigateToEditCommand ( KeyValuePair<string, string>[] parameters )
        {
            var requestDispatcher = SetupRequestDispatcher ();

            var visitKey = parameters.GetValue<long> ( "VisitKey" );
            requestDispatcher.Add ( new GetDtoRequest<AppointmentDetailsDto> ( visitKey ) );
            _gotDto = true;
            IsEdit = false;

            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleRequest, HandleRequestDispatcherException );
        }

        private void RaiseVisitChanged ()
        {
            if ( EditingDto.ClinicianKey != null && EditingDto.AppointmentStartDateTime != null )
            {
                _eventAggregator.GetEvent<VisitChangedEvent> ().Publish (
                    new VisitChangedEventArgs
                        {
                            Sender = this,
                            ClinicianKey = EditingDto.ClinicianKey.Value,
                            VisitKey = EditingDto.Key,
                            VisitStartDateTime = EditingDto.AppointmentStartDateTime.Value,
                            PatientKey = EditingDto.PatientKey
                        } );
            }
        }

        private IAsyncRequestDispatcher SetupRequestDispatcher ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.AddLookupValuesRequest ( "VisitStatus" );
            requestDispatcher.Add ( new GetCliniciansByLocationKeyRequest { LocationKey = CurrentUserContext.Location.Key } );
            requestDispatcher.Add ( new GetVisitTemplatesByAgencyKeyRequest { AgencyKey = CurrentUserContext.Agency.Key } );
            _gotDto = false;
            return requestDispatcher;
        }

        private void VisitChangedEventHandler ( VisitChangedEventArgs obj )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<AppointmentDetailsDto> ( EditingDto.Key ) );
            requestDispatcher.ProcessRequests ( GetAppointmentDetailsRequestCompleted, HandleRequestDispatcherException );
        }

        #endregion
    }
}
