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
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.ProgramOfferingEditor;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile;

namespace Rem.Ria.PatientModule.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// View Model for CreateProgramEnrollment class.
    /// </summary>
    public class CreateProgramEnrollmentViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly PagedCollectionViewWrapper<ProgramEnrollmentDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private IList<StaffNameDto> _availableEnrollingStaffs;
        private IList<ProgramOfferingLocationDto> _availableProgramOfferingLocations;
        private IList<ProgramDisplayNameDto> _availablePrograms;

        private long _clinicalCaseKey;
        private EditableDtoWrapper _editableWrapper;
        private ProgramEnrollmentDto _programEnrollment;
        private StaffNameDto _selectedEnrollingStaff;
        private ProgramDisplayNameDto _selectedProgram;
        private ProgramOfferingLocationDto _selectedProgramOfferingLocation;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProgramEnrollmentViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public CreateProgramEnrollmentViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProgramEnrollmentViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public CreateProgramEnrollmentViewModel (
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
            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<ProgramEnrollmentDto> ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            CreateProgramEnrollmentCommand = commandFactoryHelper.BuildDelegateCommand<ProgramEnrollmentDto> (
                () => CreateProgramEnrollmentCommand, ExecuteCreateProgramEnrollment );

            ProgramSelectionChangedCommand = commandFactoryHelper.BuildDelegateCommand<ProgramDisplayNameDto> (
                () => ProgramSelectionChangedCommand, ExecuteProgramSelectionChanged );

            ProgramOfferingLocationSelectionChangedCommand =
                commandFactoryHelper.BuildDelegateCommand<ProgramOfferingLocationDto> (
                    () => ProgramOfferingLocationSelectionChangedCommand, ExecuteProgramOfferingLocationSelectionChanged );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the available enrolling staffs.
        /// </summary>
        /// <value>The available enrolling staffs.</value>
        public IList<StaffNameDto> AvailableEnrollingStaffs
        {
            get { return _availableEnrollingStaffs; }
            set { ApplyPropertyChange ( ref _availableEnrollingStaffs, () => AvailableEnrollingStaffs, value ); }
        }

        /// <summary>
        /// Gets or sets the available program offering locations.
        /// </summary>
        /// <value>The available program offering locations.</value>
        public IList<ProgramOfferingLocationDto> AvailableProgramOfferingLocations
        {
            get { return _availableProgramOfferingLocations; }
            set { ApplyPropertyChange ( ref _availableProgramOfferingLocations, () => AvailableProgramOfferingLocations, value ); }
        }

        /// <summary>
        /// Gets or sets the available programs.
        /// </summary>
        /// <value>The available programs.</value>
        public IList<ProgramDisplayNameDto> AvailablePrograms
        {
            get { return _availablePrograms; }
            set { ApplyPropertyChange ( ref _availablePrograms, () => AvailablePrograms, value ); }
        }

        /// <summary>
        /// Gets the create program enrollment command.
        /// </summary>
        public ICommand CreateProgramEnrollmentCommand { get; private set; }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<ProgramEnrollmentDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets or sets the program enrollment.
        /// </summary>
        /// <value>The program enrollment.</value>
        public ProgramEnrollmentDto ProgramEnrollment
        {
            get { return _programEnrollment; }

            set
            {
                _programEnrollment = value;
                RaisePropertyChanged ( () => ProgramEnrollment );
                Wrapper.EditableDto = ProgramEnrollment;
            }
        }

        /// <summary>
        /// Gets the program offering location selection changed command.
        /// </summary>
        public ICommand ProgramOfferingLocationSelectionChangedCommand { get; private set; }

        /// <summary>
        /// Gets the program selection changed command.
        /// </summary>
        public ICommand ProgramSelectionChangedCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected enrolling staff.
        /// </summary>
        /// <value>The selected enrolling staff.</value>
        public StaffNameDto SelectedEnrollingStaff
        {
            get { return _selectedEnrollingStaff; }
            set { ApplyPropertyChange ( ref _selectedEnrollingStaff, () => SelectedEnrollingStaff, value ); }
        }

        /// <summary>
        /// Gets or sets the selected program.
        /// </summary>
        /// <value>The selected program.</value>
        public ProgramDisplayNameDto SelectedProgram
        {
            get { return _selectedProgram; }
            set { ApplyPropertyChange ( ref _selectedProgram, () => SelectedProgram, value ); }
        }

        /// <summary>
        /// Gets or sets the selected program offering location.
        /// </summary>
        /// <value>The selected program offering location.</value>
        public ProgramOfferingLocationDto SelectedProgramOfferingLocation
        {
            get { return _selectedProgramOfferingLocation; }
            set { ApplyPropertyChange ( ref _selectedProgramOfferingLocation, () => SelectedProgramOfferingLocation, value ); }
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

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            return true;
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
            _clinicalCaseKey = parameters.GetValue<long> ( "ClinicalCaseKey" );
            var currentAgencyKey = CurrentUserContext.Agency.Key;
            var currentStaffKey = CurrentUserContext.Staff.Key;

            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.Add ( new GetDtoRequest<StaffSummaryDto> { Key = currentStaffKey } );
            dispatcher.Add ( new GetAvailableProgramsRequest { AgencyKey = currentAgencyKey, CurrentStaffKey = currentStaffKey } );

            IsLoading = true;
            dispatcher.ProcessRequests ( HandleRequestComplete, HandleRequestDispatcherException );
        }

        private void ChangeProgramOfferingLocationSelection ( ProgramOfferingLocationDto dto )
        {
            if ( dto != null )
            {
                var request = new GetAvailableEnrollingStaffsRequest { ProgramOfferingKey = dto.Key };

                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( request );
                requestDispatcher.ProcessRequests (
                    HandleGetAvailableEnrollingStaffsCompleted, HandleGetAvailableEnrollingStaffsException );
            }
            else
            {
                ProgramEnrollment.EnrollingStaff = null;
                AvailableEnrollingStaffs = new List<StaffNameDto> ();
            }
        }

        private void ChangeProgramSelection ( ProgramDisplayNameDto dto )
        {
            if ( dto != null )
            {
                var request = new GetAvailableProgramOfferingLocationsRequest { CurrentStaffKey = CurrentUserContext.Staff.Key, ProgramKey = dto.Key };

                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( request );
                requestDispatcher.ProcessRequests (
                    HandleGetAvailableProgramOfferingLocationsCompleted, HandleGetAvailableProgramOfferingLocationsException );
            }
            else
            {
                ProgramEnrollment.Location = null;
                AvailableProgramOfferingLocations = new List<ProgramOfferingLocationDto> ();
            }
        }

        private void CreateProgramEnrollment ( ProgramEnrollmentDto dto )
        {
            var request = new CreateProgramEnrollmentRequest
                {
                    ClinicalCaseKey = _clinicalCaseKey,
                    ProgramOfferingKey = SelectedProgramOfferingLocation != null ? SelectedProgramOfferingLocation.Key : 0,
                    EnrollingStaffKey = SelectedEnrollingStaff != null ? SelectedEnrollingStaff.Key : 0,
                    EnrollmentDate = dto.EnrollmentDate,
                    DaysOnWaitingListCount = dto.DaysOnWaitingListCount,
                    CommentsNote = dto.CommentsNote
                };
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( request );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleCreateProgramEnrollmentCompleted, HandleCreateProgramEnrollmentException );
        }

        private void ExecuteCreateProgramEnrollment ( ProgramEnrollmentDto programEnrollmentDto )
        {
            CreateProgramEnrollment ( ProgramEnrollment );
        }

        private void ExecuteProgramOfferingLocationSelectionChanged ( ProgramOfferingLocationDto obj )
        {
            ChangeProgramOfferingLocationSelection ( obj );
        }

        private void ExecuteProgramSelectionChanged ( ProgramDisplayNameDto obj )
        {
            ChangeProgramSelection ( obj );
        }

        private void HandleCreateProgramEnrollmentCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<CreateProgramEnrollmentResponse> ();
            var dto = response.DataTransferObject;
            IsLoading = false;
            if ( dto.HasErrors )
            {
                var message = string.Empty;
                foreach ( var dataErrorInfo in dto.DataErrorInfoCollection )
                {
                    message += dataErrorInfo.ErrorLevel + ": " + dataErrorInfo.Message + Environment.NewLine;
                }

                _userDialogService.ShowDialog ( message, "Could not save", UserDialogServiceOptions.Ok );
            }
            else
            {
                RaiseProgramEnrollmentChangedEvent ( dto );

                _popupService.ClosePopup ( "CreateProgramEnrollmentView" );
            }
        }

        private void HandleCreateProgramEnrollmentException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAvailableEnrollingStaffsCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAvailableEnrollingStaffsResponse> ();
            ProgramEnrollment.EnrollingStaff = null;
            AvailableEnrollingStaffs = response.StaffNames;
            SelectedEnrollingStaff = AvailableEnrollingStaffs.SingleOrDefault ( x => x.Key == CurrentUserContext.Staff.Key );
        }

        private void HandleGetAvailableEnrollingStaffsException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Could not load", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAvailableProgramOfferingLocationsCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAvailableProgramOfferingLocationsResponse> ();
            ProgramEnrollment.Location = null;
            AvailableProgramOfferingLocations = response.ProgramOfferingLocations;
        }

        private void HandleGetAvailableProgramOfferingLocationsException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Could not load", UserDialogServiceOptions.Ok );
        }

        private void HandleRequestComplete ( ReceivedResponses receivedResponses )
        {
            var staffDtoResponse = receivedResponses.Get<DtoResponse<StaffSummaryDto>> ();
            var availableProgramDtosResponse = receivedResponses.Get<GetAvailableProgramsResponse> ();

            ProgramEnrollment = new ProgramEnrollmentDto
                {
                    ClinicalCaseKey = _clinicalCaseKey,
                    EnrollmentDate = DateTime.Now,
                    EnrollingStaff = staffDtoResponse.DataTransferObject
                };

            AvailablePrograms = availableProgramDtosResponse.ProgramDisplayNames;
            AvailableProgramOfferingLocations = new List<ProgramOfferingLocationDto> ();
            AvailableEnrollingStaffs = new List<StaffNameDto> ();

            IsLoading = false;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Program Enrollment operation failed.", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void RaiseProgramEnrollmentChangedEvent ( ProgramEnrollmentDto dto )
        {
            _eventAggregator
                .GetEvent<ProgramEnrollmentChangedEvent> ()
                .Publish ( new ProgramEnrollmentChangedEventArgs { Sender = this, ProgramEnrollmentKey = dto.Key } );
        }

        #endregion
    }
}
