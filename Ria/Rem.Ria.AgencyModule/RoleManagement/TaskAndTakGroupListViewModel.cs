using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.RoleManagement;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View Model for TaskAndTakGroupList class.
    /// </summary>
    public class TaskAndTakGroupListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;

        private readonly PagedCollectionViewWrapper<SystemRoleDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAndTakGroupListViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public TaskAndTakGroupListViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _popupService = popupService;

            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<SystemRoleDto> ();

            InitializeGroupingDescriptions ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            CreateTaskOrTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => CreateTaskOrTaskGroupCommand, ExecuteCreateTaskOrTaskGroupCommand );
            EditTaskOrTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => EditTaskOrTaskGroupCommand, ExecuteEditTaskOrTaskGroupCommand );
            CloneTaskOrTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => CloneTaskOrTaskGroupCommand, ExecuteCloneTaskOrTaskGroupCommand );
            DeleteTaskOrTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => DeleteTaskOrTaskGroupCommand, ExecuteDeleteTaskOrTaskGroup );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the clone task or task group command.
        /// </summary>
        public ICommand CloneTaskOrTaskGroupCommand { get; private set; }

        /// <summary>
        /// Gets the create task or task group command.
        /// </summary>
        public ICommand CreateTaskOrTaskGroupCommand { get; private set; }

        /// <summary>
        /// Gets the delete task or task group command.
        /// </summary>
        public ICommand DeleteTaskOrTaskGroupCommand { get; private set; }

        /// <summary>
        /// Gets the edit task or task group command.
        /// </summary>
        public ICommand EditTaskOrTaskGroupCommand { get; private set; }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<SystemRoleDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
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
            GetAllTasksAndTaskGroups ();
        }

        private void DeleteSystemRole ( long systemRoleKey )
        {
            var requestDispatcher = SetupAsyncRequestDispatcher ();
            requestDispatcher.Add ( new DeleteTaskOrTaskGroupRequest { SystemRoleKey = systemRoleKey } );
            requestDispatcher.ProcessRequests ( HandleDeleteSystemRoleCompleted, HandleDeleteSystemRoleException );

            IsLoading = true;
        }

        private void ExecuteCloneTaskOrTaskGroupCommand ( SystemRoleDto systemRoleDto )
        {
            _popupService.ShowPopup (
                "EditTaskAndTaskGroupView",
                "Clone",
                "Task/Task Group",
                new[]
                    {
                        new KeyValuePair<string, string> ( "SystemRoleKey", systemRoleDto.Key.ToString () ),
                        new KeyValuePair<string, string> ( "SystemRoleType", systemRoleDto.SystemRoleType.ToString () )
                    },
                false,
                PopupClosed );
        }

        private void ExecuteCreateTaskOrTaskGroupCommand ( object isAddingNewTask )
        {
            var addingNewTesk = bool.Parse ( isAddingNewTask.ToString () );
            if ( addingNewTesk )
            {
                _popupService.ShowPopup (
                    "EditTaskAndTaskGroupView",
                    "CreateTask",
                    "Task/Task Group",
                    null,
                    false,
                    PopupClosed );
            }
            else
            {
                _popupService.ShowPopup (
                    "EditTaskAndTaskGroupView",
                    "CreateTaskGroup",
                    "Task Group",
                    null,
                    false,
                    PopupClosed );
            }
        }

        private void ExecuteDeleteTaskOrTaskGroup ( SystemRoleDto systemRoleDto )
        {
            var result = _userDialogService.ShowDialog (
                "Are you sure you want to delete?", "Confirmation", UserDialogServiceOptions.OkCancel );

            if ( result == UserDialogServiceResult.Ok )
            {
                DeleteSystemRole ( systemRoleDto.Key );
            }
        }

        private void ExecuteEditTaskOrTaskGroupCommand ( SystemRoleDto systemRoleDto )
        {
            _popupService.ShowPopup (
                "EditTaskAndTaskGroupView",
                "Edit",
                "Task/Task Group",
                new[]
                    {
                        new KeyValuePair<string, string> ( "SystemRoleKey", systemRoleDto.Key.ToString () ),
                        new KeyValuePair<string, string> ( "SystemRoleType", systemRoleDto.SystemRoleType.ToString () )
                    },
                false,
                PopupClosed );
        }

        private void GetAllTasksAndTaskGroups ()
        {
            var requestDispatcher = SetupAsyncRequestDispatcher ();
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetAllTasksAndTaskGroupsCompleted, HandleGetAllTasksAndTaskGroupsException );
        }

        private void HandleDeleteSystemRoleCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;

            var response = receivedResponses.Get<DeleteTaskOrTaskGroupResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            if ( validationErrors.Count () == 0 )
            {
                var taskAndTaskGroupSystemRoleList = response.TaskAndTaskGroupSystemRoles.SystemRoles;
                PagedCollectionViewWrapper.WrapInPagedCollectionView ( taskAndTaskGroupSystemRoleList, ( p => true ) );
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
            }
        }

        private void HandleDeleteSystemRoleException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not delete this role.", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAllTasksAndTaskGroupsCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;

            var response = receivedResponses.Get<DtoResponse<TaskAndTaskGroupSystemRolesDto>> ();

            var taskAndTaskGroupSystemRoleList = response.DataTransferObject.SystemRoles;
            PagedCollectionViewWrapper.WrapInPagedCollectionView ( taskAndTaskGroupSystemRoleList, ( p => true ) );
        }

        private void HandleGetAllTasksAndTaskGroupsException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not get all task and task groups.", UserDialogServiceOptions.Ok );
        }

        private void InitializeGroupingDescriptions ()
        {
            PagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<SystemRoleDto, object> ( p => p.SystemRoleType ), "Role Type" ) );
        }

        private void PopupClosed ()
        {
            GetAllTasksAndTaskGroups ();
        }

        private IAsyncRequestDispatcher SetupAsyncRequestDispatcher ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<TaskAndTaskGroupSystemRolesDto> () );
            return requestDispatcher;
        }

        #endregion
    }
}
