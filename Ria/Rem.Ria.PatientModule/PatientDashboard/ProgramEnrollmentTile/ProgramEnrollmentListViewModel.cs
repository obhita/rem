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
using System.Windows;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard.ProgramEnrollmentTile;
using Telerik.Windows.Controls.DragDrop;

namespace Rem.Ria.PatientModule.PatientDashboard.ProgramEnrollmentTile
{
    /// <summary>
    /// View Model for ProgramEnrollmentList class.
    /// </summary>
    public class ProgramEnrollmentListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;

        private readonly Predicate<object> _filter;
        private readonly PagedCollectionViewWrapper<ProgramEnrollmentDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _clinicalCaseKey;
        private long _patientKey;
        private IList<ProgramEnrollmentDto> _programEnrollmentList;
        private ProgramEnrollmentDto _selectedProgramEnrollment;
        private ShowOption _showOption;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramEnrollmentListViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public ProgramEnrollmentListViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramEnrollmentListViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public ProgramEnrollmentListViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _popupService = popupService;
            _showOption = ShowOption.ShowActive;
            _filter = FilterByActiveStatus;

            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<ProgramEnrollmentDto> ();

            InitializeGroupingDescriptions ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ShowAllCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowAllCommand, ExecuteShowAll );
            ShowActiveOnlyCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowActiveOnlyCommand, ExecuteShowActiveOnly );

            CreateProgramEnrollmentCommand = commandFactoryHelper.BuildDelegateCommand (
                () => CreateProgramEnrollmentCommand, ExecuteCreateProgramEnrollmentCommand );
            EditProgramEnrollmentCommand = commandFactoryHelper.BuildDelegateCommand<ProgramEnrollmentDto> (
                () => EditProgramEnrollmentCommand, ExecuteEditProgramEnrollmentCommand );
            DisenrollProgramEnrollmentCommand =
                commandFactoryHelper.BuildDelegateCommand<ProgramEnrollmentDto> (
                    () => DisenrollProgramEnrollmentCommand, ExecuteDisenrollProgramEnrollmentCommand );
            DeleteProgramEnrollmentCommand = commandFactoryHelper.BuildDelegateCommand<ProgramEnrollmentDto> (
                () => DeleteProgramEnrollmentCommand, ExecuteDeleteProgramEnrollment, CanExecuteDeleteProgramEnrollment );
            DragQueryCommand = commandFactoryHelper.BuildDelegateCommand<DragDropQueryEventArgs> ( () => DragQueryCommand, ExecuteDragQueryCommand );

            _eventAggregator.GetEvent<ClinicalCaseChangedEvent> ()
                .Subscribe ( ClinicalCaseChangedEventHandler, ThreadOption.BackgroundThread, false, FilterClinicalCaseChangedEvents );

            _eventAggregator.GetEvent<ProgramEnrollmentChangedEvent> ()
                .Subscribe ( ProgramEnrollmentChangedEventHandler, ThreadOption.BackgroundThread, false, FilterProgramEnrollmentChangedEvents );
        }

        #endregion

        #region Enums

        /// <summary>
        /// ShowOption enum.
        /// </summary>
        private enum ShowOption
        {
            /// <summary>
            /// Show all items.
            /// </summary>
            ShowAll,

            /// <summary>
            /// Show active items only.
            /// </summary>
            ShowActive
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the create program enrollment command.
        /// </summary>
        public ICommand CreateProgramEnrollmentCommand { get; private set; }

        /// <summary>
        /// Gets the delete program enrollment command.
        /// </summary>
        public ICommand DeleteProgramEnrollmentCommand { get; private set; }

        /// <summary>
        /// Gets the disenroll program enrollment command.
        /// </summary>
        public ICommand DisenrollProgramEnrollmentCommand { get; private set; }

        /// <summary>
        /// Gets the drag query command.
        /// </summary>
        public ICommand DragQueryCommand { get; private set; }

        /// <summary>
        /// Gets the edit program enrollment command.
        /// </summary>
        public ICommand EditProgramEnrollmentCommand { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has selected program enrollment.
        /// </summary>
        public bool HasSelectedProgramEnrollment
        {
            get { return SelectedProgramEnrollmentDto != null; }
        }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<ProgramEnrollmentDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets or sets the selected program enrollment.
        /// </summary>
        /// <value>The selected program enrollment.</value>
        public ProgramEnrollmentDto SelectedProgramEnrollment
        {
            get { return _selectedProgramEnrollment; }

            set
            {
                ApplyPropertyChange ( ref _selectedProgramEnrollment, () => SelectedProgramEnrollment, value );

                RaisePropertyChanged ( () => HasSelectedProgramEnrollment );
            }
        }

        /// <summary>
        /// Gets or sets the selected program enrollment dto.
        /// </summary>
        /// <value>The selected program enrollment dto.</value>
        public ProgramEnrollmentDto SelectedProgramEnrollmentDto { get; set; }

        /// <summary>
        /// Gets the show active only command.
        /// </summary>
        public ICommand ShowActiveOnlyCommand { get; private set; }

        /// <summary>
        /// Gets the show all command.
        /// </summary>
        public ICommand ShowAllCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clinicals the case changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        public void ClinicalCaseChangedEventHandler ( ClinicalCaseChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded (
                () =>
                    {
                        _clinicalCaseKey = obj.ClinicalCaseKey;
                        GetAllProgramEnrollmentsByClinicalCase ( _clinicalCaseKey );
                    } );
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
        /// Filters the program enrollment changed events.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.PatientDashboard.ProgramEnrollmentTile.ProgramEnrollmentChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterProgramEnrollmentChangedEvents ( ProgramEnrollmentChangedEventArgs args )
        {
            return true;
        }

        /// <summary>
        /// Programs the enrollment changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.PatientDashboard.ProgramEnrollmentTile.ProgramEnrollmentChangedEventArgs"/> instance containing the event data.</param>
        public void ProgramEnrollmentChangedEventHandler ( ProgramEnrollmentChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded ( () => GetAllProgramEnrollmentsByClinicalCase ( _clinicalCaseKey ) );
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
            _clinicalCaseKey = parameters.GetValue<long> ( "ClinicalCaseKey" );
            _patientKey = parameters.GetValue<long>("PatientKey");
            GetAllProgramEnrollmentsByClinicalCase ( _clinicalCaseKey );
        }

        private bool CanExecuteDeleteProgramEnrollment ( ProgramEnrollmentDto arg )
        {
            // TODO: may need some business rule check in the future
            return true;
        }

        private void DeleteProgramEnrollment ( long programEnrollmentKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new DeleteProgramEnrollmentRequest { ProgramEnrollmentKey = programEnrollmentKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleDeleteProgramEnrollmentCompleted, HandleDeleteProgramEnrollmentException );
        }

        private void ExecuteCreateProgramEnrollmentCommand ()
        {
            _popupService.ShowPopup (
                "CreateProgramEnrollmentView",
                "Create",
                "Program Enrollment",
                new[] { new KeyValuePair<string, string> ( "ClinicalCaseKey", _clinicalCaseKey.ToString () ) },
                false,
                PopupClosed );
        }

        private void ExecuteDeleteProgramEnrollment ( ProgramEnrollmentDto programEnrollmentDto )
        {
            var result = _userDialogService.ShowDialog (
                "Are you sure you want to delete?", "Confirmation", UserDialogServiceOptions.OkCancel );

            if ( result == UserDialogServiceResult.Ok )
            {
                DeleteProgramEnrollment ( programEnrollmentDto.Key );
            }
        }

        private void ExecuteDisenrollProgramEnrollmentCommand ( ProgramEnrollmentDto programEnrollmentDto )
        {
            _popupService.ShowPopup (
                "DisenrollProgramEnrollmentView",
                "Disenroll",
                "Program Disenrollment",
                new[] { new KeyValuePair<string, string> ( "ProgramEnrollmentKey", programEnrollmentDto.Key.ToString () ) },
                false,
                PopupClosed );
        }

        private void ExecuteDragQueryCommand ( DragDropQueryEventArgs obj )
        {
            var dragItem = obj.Options.Payload as ProgramEnrollmentDto;
            if ( dragItem != null )
            {
                obj.QueryResult = dragItem.IsActive;
            }
        }

        private void ExecuteEditProgramEnrollmentCommand ( ProgramEnrollmentDto programEnrollmentDto )
        {
            _popupService.ShowPopup (
                "EditProgramEnrollmentView",
                "Edit",
                "Program Enrollment",
                new[] { new KeyValuePair<string, string> ( "ProgramEnrollmentKey", programEnrollmentDto.Key.ToString () ) },
                false,
                PopupClosed );
        }

        private void ExecuteShowActiveOnly ()
        {
            if ( _showOption != ShowOption.ShowActive )
            {
                _showOption = ShowOption.ShowActive;
                PagedCollectionViewWrapper.SetFilter ( _filter );
            }
        }

        private void ExecuteShowAll ()
        {
            if ( _showOption != ShowOption.ShowAll )
            {
                _showOption = ShowOption.ShowAll;
                PagedCollectionViewWrapper.SetFilter ( _filter );
            }
        }

        private bool FilterByActiveStatus ( object obj )
        {
            var returnValue = true;

            var programEnrollmentDto = obj as ProgramEnrollmentDto;

            if ( programEnrollmentDto != null && _showOption == ShowOption.ShowActive )
            {
                if ( !programEnrollmentDto.IsActive )
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        private void GetAllProgramEnrollmentsByClinicalCase ( long clinicalCaseKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetAllProgramEnrollmentsByClinicalCaseRequest { ClinicalCaseKey = clinicalCaseKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests (
                HandleGetAllProgramEnrollmentsByClinicalCaseCompleted, HandleGetAllProgramEnrollmentsByClinicalCaseException );
        }

        private void HandleDeleteProgramEnrollmentCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<DeleteProgramEnrollmentResponse> ();
            _programEnrollmentList = new List<ProgramEnrollmentDto> ( response.ProgramEnrollmentDtos );
            PagedCollectionViewWrapper.WrapInPagedCollectionView ( _programEnrollmentList, _filter );
            IsLoading = false;
        }

        private void HandleDeleteProgramEnrollmentException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not delete", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAllProgramEnrollmentsByClinicalCaseCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAllProgramEnrollmentsByClinicalCaseResponse> ();

            _programEnrollmentList = new List<ProgramEnrollmentDto> ( response.ProgramEnrollmentDtos );
            PagedCollectionViewWrapper.WrapInPagedCollectionView ( _programEnrollmentList, _filter );
            IsLoading = false;
        }

        private void HandleGetAllProgramEnrollmentsByClinicalCaseException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not get all program enrollments by clinical case key", UserDialogServiceOptions.Ok );
        }

        private void InitializeGroupingDescriptions ()
        {
            PagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<ProgramEnrollmentDto, object> ( p => p.ProgramName ), "Program" ) );
        }

        private void PopupClosed ()
        {
            GetAllProgramEnrollmentsByClinicalCase ( _clinicalCaseKey );
        }

        #endregion
    }
}
