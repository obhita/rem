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
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
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
    /// View Model for EditProgramEnrollment class.
    /// </summary>
    public class EditProgramEnrollmentViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly PagedCollectionViewWrapper<ProgramEnrollmentDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private IList<StaffNameDto> _availableEnrollingStaffs;

        private EditableDtoWrapper _editableWrapper;
        private bool _isInitializing;
        private ProgramEnrollmentDto _programEnrollment;
        private StaffNameDto _selectedEnrollingStaff;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProgramEnrollmentViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public EditProgramEnrollmentViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProgramEnrollmentViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public EditProgramEnrollmentViewModel (
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

            EditProgramEnrollmentCommand = commandFactoryHelper.BuildDelegateCommand<ProgramEnrollmentDto> (
                () => EditProgramEnrollmentCommand, ExecuteEditProgramEnrollment );
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
        /// Gets the edit program enrollment command.
        /// </summary>
        public ICommand EditProgramEnrollmentCommand { get; private set; }

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
        /// Gets or sets the selected enrolling staff.
        /// </summary>
        /// <value>The selected enrolling staff.</value>
        public StaffNameDto SelectedEnrollingStaff
        {
            get { return _selectedEnrollingStaff; }

            set
            {
                ApplyPropertyChange ( ref _selectedEnrollingStaff, () => SelectedEnrollingStaff, value );
                if ( !_isInitializing )
                {
                    ProgramEnrollment.EnrollingStaff = null; // just mark as dirty
                }
            }
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
            _isInitializing = true;
            var programEnrollmentKey = parameters.GetValue<long> ( "ProgramEnrollmentKey" );

            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.Add ( new GetProgramEnrollmentByKeyRequest { ProgramEnrollmentKey = programEnrollmentKey } );

            IsLoading = true;
            dispatcher.ProcessRequests ( HandleRequestComplete, HandleRequestDispatcherException );
        }

        private void EditProgramEnrollment ( ProgramEnrollmentDto dto )
        {
            var request = new ReviseProgramEnrollmentRequest
                {
                    ProgramEnrollmentKey = dto.Key,
                    EnrollingStaffKey = SelectedEnrollingStaff != null ? SelectedEnrollingStaff.Key : 0,
                    EnrollmentDate = dto.EnrollmentDate,
                    DaysOnWaitingListCount = dto.DaysOnWaitingListCount,
                    CommentsNote = dto.CommentsNote
                };
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( request );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleReviseProgramEnrollmentCompleted, HandleReviseProgramEnrollmentException );
        }

        private void ExecuteEditProgramEnrollment ( ProgramEnrollmentDto programEnrollmentDto )
        {
            EditProgramEnrollment ( ProgramEnrollment );
        }

        private void HandleInitializeAvailableSelectionListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAvailableEnrollingStaffsResponse> ();
            AvailableEnrollingStaffs = response.StaffNames;
            SelectedEnrollingStaff = AvailableEnrollingStaffs.SingleOrDefault ( x => x.Key == ProgramEnrollment.EnrollingStaff.Key );

            _isInitializing = false;
            IsLoading = false;
        }

        private void HandleInitializeAvailableSelectionListException ( ExceptionInfo ex )
        {
            _isInitializing = false;
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not load", UserDialogServiceOptions.Ok );
        }

        private void HandleRequestComplete ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetProgramEnrollmentByKeyResponse> ();

            ProgramEnrollment = response.DataTransferObject;

            InitializeAvailableSelectionList ( ProgramEnrollment );

            IsLoading = false;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Program Enrollment operation failed.", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleReviseProgramEnrollmentCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<ReviseProgramEnrollmentResponse> ();
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

                _popupService.ClosePopup ( "EditProgramEnrollmentView" );
            }
        }

        private void HandleReviseProgramEnrollmentException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void InitializeAvailableSelectionList ( ProgramEnrollmentDto dto )
        {
            if ( dto != null )
            {
                var request = new GetAvailableEnrollingStaffsRequest { ProgramOfferingKey = dto.ProgramOfferingKey };

                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( request );

                IsLoading = true;
                requestDispatcher.ProcessRequests (
                    HandleInitializeAvailableSelectionListCompleted, HandleInitializeAvailableSelectionListException );
            }
            else
            {
                AvailableEnrollingStaffs = new List<StaffNameDto> ();
            }
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
