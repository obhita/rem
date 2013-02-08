using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.RoleManagement;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View Model for EditJobFunction class.
    /// </summary>
    public class EditJobFunctionViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private EditableDtoWrapper _editableWrapper;
        private bool _isEditing;
        private SystemRoleDto _systemRoleDto;
        private PagedCollectionView _taskGroupSystemRoleLookupList;
        private PagedCollectionView _taskSystemRoleLookupList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditJobFunctionViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public EditJobFunctionViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditJobFunctionViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public EditJobFunctionViewModel (
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

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SaveJobFunctionCommand = commandFactoryHelper.BuildDelegateCommand ( () => SaveJobFunctionCommand, ExecuteSaveJobFunctionCommand );
            GrantSystemRoleCommand = commandFactoryHelper.BuildDelegateCommand ( () => GrantSystemRoleCommand, ExecuteGrantSystemRoleCommand );
            RevokeSystemRoleCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => RevokeSystemRoleCommand, ExecuteRevokeSystemRoleCommand );
            CancelCommand = commandFactoryHelper.BuildDelegateCommand ( () => CancelCommand, ExecuteCancelCommand );

            CreateCommand = NavigationCommandManager.BuildCommand ( () => CreateCommand, NavigateToCreateCommand );
            EditCommand = NavigationCommandManager.BuildCommand ( () => EditCommand, NavigateToEditCommand );
            CloneCommand = NavigationCommandManager.BuildCommand ( () => CloneCommand, NavigateToCloneCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        /// <summary>
        /// Gets the clone command.
        /// </summary>
        public INavigationCommand CloneCommand { get; private set; }

        /// <summary>
        /// Gets the create command.
        /// </summary>
        public INavigationCommand CreateCommand { get; private set; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        public INavigationCommand EditCommand { get; private set; }

        /// <summary>
        /// Gets the grant system role command.
        /// </summary>
        public ICommand GrantSystemRoleCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is editing.
        /// </summary>
        /// <value><c>true</c> if this instance is editing; otherwise, <c>false</c>.</value>
        public bool IsEditing
        {
            get { return _isEditing; }
            set { ApplyPropertyChange ( ref _isEditing, () => IsEditing, value ); }
        }

        /// <summary>
        /// Gets the revoke system role command.
        /// </summary>
        public ICommand RevokeSystemRoleCommand { get; private set; }

        /// <summary>
        /// Gets the save job function command.
        /// </summary>
        public ICommand SaveJobFunctionCommand { get; private set; }

        /// <summary>
        /// Gets or sets the system role dto.
        /// </summary>
        /// <value>The system role dto.</value>
        public SystemRoleDto SystemRoleDto
        {
            get { return _systemRoleDto; }

            set
            {
                _systemRoleDto = value;
                RaisePropertyChanged ( () => SystemRoleDto );
                IsEditing = _systemRoleDto.Key != 0;
                Wrapper.EditableDto = SystemRoleDto;
            }
        }

        /// <summary>
        /// Gets the task group system role lookup list.
        /// </summary>
        public PagedCollectionView TaskGroupSystemRoleLookupList
        {
            get { return _taskGroupSystemRoleLookupList; }
            private set { ApplyPropertyChange ( ref _taskGroupSystemRoleLookupList, () => TaskGroupSystemRoleLookupList, value ); }
        }

        /// <summary>
        /// Gets the task system role lookup list.
        /// </summary>
        public PagedCollectionView TaskSystemRoleLookupList
        {
            get { return _taskSystemRoleLookupList; }
            private set { ApplyPropertyChange ( ref _taskSystemRoleLookupList, () => TaskSystemRoleLookupList, value ); }
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

        #region Public Methods

        /// <summary>
        /// Determines whether this instance can close.
        /// </summary>
        /// <returns><c>true</c> if this instance can close; otherwise, <c>false</c>.</returns>
        public bool CanClose ()
        {
            var canClose = true;
            if ( Wrapper.IsDirty )
            {
                var result = _userDialogService.ShowDialog (
                    "You have unsaved changes. Are you sure you want to close?",
                    "Pending Changes:",
                    UserDialogServiceOptions.OkCancel );
                if ( result == UserDialogServiceResult.Cancel )
                {
                    canClose = false;
                }
            }
            return canClose;
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

        private void ExecuteAddJobFunctionCommand ()
        {
            var request = new CreateSystemRoleRequest
                {
                    Name = SystemRoleDto.Name,
                    Description = SystemRoleDto.Description,
                    SystemRoleType = SystemRoleType.JobFunction
                };
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( request );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleAddJobFunctionCompleted, HandleAddJobFunctionException );
        }

        private void ExecuteCancelCommand ()
        {
            _popupService.ClosePopup ( "EditJobFunctionView" );
        }

        private void ExecuteGrantSystemRoleCommand ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

            //only handle adding, not deleting, which will be handled in the delete command
            var sytemRoleKeysToBeGranted = ( from systemRoleDto in _taskSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> ()
                                                        .Concat ( _taskGroupSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> () )
                                                    where systemRoleDto.IsSelectable && systemRoleDto.IsSelected
                                                    select systemRoleDto.Key ).ToList ();

            if ( sytemRoleKeysToBeGranted.Count > 0 )
            {
                requestDispatcher.Add (
                    new GrantSystemRoleRequest { SystemRoleKey = SystemRoleDto.Key, GrantedSystemRoleKeys = sytemRoleKeysToBeGranted } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleGrantSystemRoleCompleted, HandleGrantSystemRoleException );
            }
        }

        private void ExecuteRevokeSystemRoleCommand ( SystemRoleDto obj )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new RevokeSystemRoleRequest { SystemRoleKey = SystemRoleDto.Key, SystemRoleKeysToBeRevoked = new List<long> { obj.Key } } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleRevokeSystemRoleCompleted, HandleRevokeSystemRoleException );
        }

        private void ExecuteSaveJobFunctionCommand ()
        {
            if ( SystemRoleDto.Key == 0 )
            {
                ExecuteAddJobFunctionCommand ();
            }
            else
            {
                var request = new RenameSystemRoleRequest
                    {
                        Name = SystemRoleDto.Name,
                        Description = SystemRoleDto.Description,
                        SystemRoleKey = SystemRoleDto.Key
                    };
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( request );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleSaveJobFunctionCompleted, HandleSaveJobFunctionException );
            }
        }

        private void GetTaskGroupSystemRoleListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<TaskGroupSystemRolesDto>> ();
            TaskGroupSystemRoleLookupList = new PagedCollectionView ( response.DataTransferObject.SystemRoles );
        }

        private void GetTaskSystemRoleListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<TaskSystemRolesDto>> ();
            TaskSystemRoleLookupList = new PagedCollectionView ( response.DataTransferObject.SystemRoles );
        }

        private void HandleAddJobFunctionCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.SystemRole, validationErrors );
        }

        private void HandleAddJobFunctionException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void HandleCloneRequestComplete ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.SystemRole, validationErrors, true );
        }

        private void HandleCloneRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Clone job function failed.", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleEditRequestComplete ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<SystemRoleDto>> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.DataTransferObject, validationErrors, true );
        }

        private void HandleEditRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Load job function failed.", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleGrantSystemRoleCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.SystemRole, validationErrors );
        }

        private void HandleGrantSystemRoleException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetTaskSystemRoleListCompleted ( receivedResponses );
            GetTaskGroupSystemRoleListCompleted ( receivedResponses );
            RefreshSystemRoleLists ();
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Job Function editor initialization failed", UserDialogServiceOptions.Ok );
        }

        private void HandleRevokeSystemRoleCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.SystemRole, validationErrors );
        }

        private void HandleRevokeSystemRoleException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void HandleSaveJobFunctionCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.SystemRole, validationErrors );
        }

        private void HandleSaveJobFunctionException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void InitializeTaskAndTaskGroupList ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<TaskSystemRolesDto> () );
            requestDispatcher.Add ( new GetDtoRequest<TaskGroupSystemRolesDto> () );

            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }

        private void NavigateToCloneCommand ( KeyValuePair<string, string>[] parameters )
        {
            var systemRoleKey = parameters.GetValue<long> ( "SystemRoleKey" );
            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.Add ( new CloneSystemRoleRequest { SystemRoleKey = systemRoleKey } );
            IsLoading = true;
            dispatcher.ProcessRequests ( HandleCloneRequestComplete, HandleCloneRequestDispatcherException );
        }

        private void NavigateToCreateCommand ( KeyValuePair<string, string>[] parameters )
        {
            SystemRoleDto = new SystemRoleDto ();
            InitializeTaskAndTaskGroupList ();
        }

        private void NavigateToEditCommand ( KeyValuePair<string, string>[] parameters )
        {
            var systemRoleKey = parameters.GetValue<long> ( "SystemRoleKey" );
            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.Add ( new GetDtoRequest<SystemRoleDto> { Key = systemRoleKey } );
            IsLoading = true;
            dispatcher.ProcessRequests ( HandleEditRequestComplete, HandleEditRequestDispatcherException );
        }

        private void ProcessSystemRoleResponses ( SystemRoleDto systemRoleDto, List<DataErrorInfo> validationErrors, bool isInitializing = false )
        {
            IsLoading = false;

            if ( validationErrors.Count () == 0 )
            {
                SystemRoleDto = systemRoleDto;
                if ( isInitializing )
                {
                    InitializeTaskAndTaskGroupList ();
                }
                else
                {
                    RefreshSystemRoleLists ();
                }
            }
            else
            {
                var errors = new StringBuilder ();
                errors.AppendLine ( "The following errors occurred:" );

                foreach ( var validationError in validationErrors )
                {
                    errors.AppendLine ( validationError.Message );
                }

                _userDialogService.ShowDialog ( errors.ToString (), "Errors", UserDialogServiceOptions.Ok );

                //Wrapper.EditableDto.AddDataErrorInfo(validationError);
            }
        }

        private void RefreshSystemRoleLists ()
        {
            RefreshTaskGroupSystemRoleLookupList ();
            RefreshTaskSystemRoleLookupList ();
        }

        private void RefreshTaskGroupSystemRoleLookupList ()
        {
            foreach ( var systemRoleDto in TaskGroupSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> () )
            {
                systemRoleDto.IsSelectable = true;
                systemRoleDto.IsSelected = false;
                if ( SystemRoleDto.GrantedSystemRoles != null
                     && SystemRoleDto.GrantedSystemRoles.Any ( grantedSystemRoleDto => systemRoleDto.Key == grantedSystemRoleDto.Key ) )
                {
                    systemRoleDto.IsSelectable = false;
                    systemRoleDto.IsSelected = true;
                }
            }

            TaskGroupSystemRoleLookupList.Filter = ( p => ( p as SystemRoleDto ).IsSelectable );
            TaskGroupSystemRoleLookupList.Refresh ();
        }

        private void RefreshTaskSystemRoleLookupList ()
        {
            foreach ( var systemRoleDto in TaskSystemRoleLookupList.SourceCollection.OfType<SystemRoleDto> () )
            {
                systemRoleDto.IsSelectable = true;
                systemRoleDto.IsSelected = false;

                if ( SystemRoleDto.GrantedSystemRoles != null
                     && SystemRoleDto.GrantedSystemRoles.Any ( grantedSystemRoleDto => systemRoleDto.Key == grantedSystemRoleDto.Key ) )
                {
                    systemRoleDto.IsSelectable = false;
                    systemRoleDto.IsSelected = true;
                }
            }

            TaskSystemRoleLookupList.Filter = ( p => ( p as SystemRoleDto ).IsSelectable );
            TaskSystemRoleLookupList.Refresh ();
        }

        #endregion
    }
}
