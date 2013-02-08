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
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile;
using Rem.WellKnownNames.ProgramModule;

namespace Rem.Ria.PatientModule.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// View Model for DisenrollProgramEnrollment class.
    /// </summary>
    public class DisenrollProgramEnrollmentViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private static readonly string DisenrollReasonLookupName = "DisenrollReason";
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly PagedCollectionViewWrapper<ProgramEnrollmentDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private IList<LookupValueDto> _availableDisenrollReasons;

        private EditableDtoWrapper _editableWrapper;
        private ProgramEnrollmentDto _programEnrollment;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DisenrollProgramEnrollmentViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public DisenrollProgramEnrollmentViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisenrollProgramEnrollmentViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public DisenrollProgramEnrollmentViewModel (
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

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper (
                this, commandFactory );

            DisenrollProgramEnrollmentCommand =
                commandFactoryHelper.BuildDelegateCommand<ProgramEnrollmentDto> (
                    () => DisenrollProgramEnrollmentCommand, ExecuteDisenrollProgramEnrollment );
            DisenrollReasonSelectionChangedCommand =
                commandFactoryHelper.BuildDelegateCommand<LookupValueDto> (
                    () => DisenrollReasonSelectionChangedCommand, ExecuteChangeDisenrollReasonSelection );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the available disenroll reasons.
        /// </summary>
        /// <value>The available disenroll reasons.</value>
        public IList<LookupValueDto> AvailableDisenrollReasons
        {
            get { return _availableDisenrollReasons; }
            set { ApplyPropertyChange ( ref _availableDisenrollReasons, () => AvailableDisenrollReasons, value ); }
        }

        /// <summary>
        /// Gets the disenroll program enrollment command.
        /// </summary>
        public ICommand DisenrollProgramEnrollmentCommand { get; private set; }

        /// <summary>
        /// Gets the disenroll reason selection changed command.
        /// </summary>
        public ICommand DisenrollReasonSelectionChangedCommand { get; private set; }

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
            var programEnrollmentKey = parameters.GetValue<long> ( "ProgramEnrollmentKey" );

            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.AddLookupValuesRequest ( DisenrollReasonLookupName );
            dispatcher.Add ( new GetProgramEnrollmentByKeyRequest { ProgramEnrollmentKey = programEnrollmentKey } );

            IsLoading = true;
            dispatcher.ProcessRequests ( HandleRequestComplete, HandleRequestDispatcherException );
        }

        private void DisenrollProgramEnrollment ( ProgramEnrollmentDto dto )
        {
            var request = new DisenrollProgramEnrollmentRequest
                {
                    ProgramEnrollmentKey = dto.Key,
                    DisenrollmentDate = dto.DisenrollmentDate,
                    DisenrollReasonKey = dto.DisenrollReason != null ? dto.DisenrollReason.Key : 0,
                    DisenrollOtherReasonNote = dto.DisenrollOtherReasonNote
                };
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( request );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleDisenrollProgramEnrollmentCompleted, HandleDisenrollProgramEnrollmentException );
        }

        private void ExecuteChangeDisenrollReasonSelection ( LookupValueDto disenrollReason )
        {
            if ( disenrollReason != null )
            {
                if ( disenrollReason.WellKnownName != DisenrollReason.Other )
                {
                    ProgramEnrollment.DisenrollOtherReasonNote = null;
                }
            }
        }

        private void ExecuteDisenrollProgramEnrollment ( ProgramEnrollmentDto programEnrollmentDto )
        {
            DisenrollProgramEnrollment ( ProgramEnrollment );
        }

        private IDictionary<string, IList<LookupValueDto>> GetLookupValues ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            return lookupValueLists;
        }

        private void HandleDisenrollProgramEnrollmentCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<DisenrollProgramEnrollmentResponse> ();
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

                _popupService.ClosePopup ( "DisenrollProgramEnrollmentView" );
            }
        }

        private void HandleDisenrollProgramEnrollmentException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void HandleRequestComplete ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetProgramEnrollmentByKeyResponse> ();

            var programEnrollmentDto = response.DataTransferObject;

            var lookupValueLists = GetLookupValues ( receivedResponses );
            if ( lookupValueLists.ContainsKey ( DisenrollReasonLookupName ) )
            {
                AvailableDisenrollReasons = lookupValueLists[DisenrollReasonLookupName];

                ProgramEnrollment = programEnrollmentDto;
            }

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
