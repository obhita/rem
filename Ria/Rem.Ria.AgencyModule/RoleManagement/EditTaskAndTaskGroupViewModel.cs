using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.RoleManagement;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View Model for EditTaskAndTaskGroup class.
    /// </summary>
    public class EditTaskAndTaskGroupViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private bool _beginEditTaskFlag;
        private bool _beginEditTaskGroupFlag;

        private long _initialSystemRoleKeyToEdit;
        private bool _isAddingNewGroupTask;
        private bool _isAddingNewTask;
        private bool? _isBegunWithTaskWorkingMode;
        private bool _isTaskGroupWorkingModeInitialized;
        private bool _isTaskWorkingModeInitialized;
        private PagedCollectionView _permissionCollectionView;

        private int _permissionToBeAddedCount;
        private int _permissionToBeRemovedCount;
        private object _selectedItemInTaskGroupList;
        private object _selectedItemInTaskList;
        private PagedCollectionView _taskCollectionView;
        private PagedCollectionView _taskGroupCollectionView;
        private int _taskToBeAddedCount;
        private int _taskToBeRemovedCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTaskAndTaskGroupViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public EditTaskAndTaskGroupViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _popupService = popupService;

            _permissionCollectionView = new PagedCollectionView ( new List<SystemPermissionDto> () );
            _taskCollectionView = new PagedCollectionView ( new List<SystemRoleDto> () );
            _taskGroupCollectionView = new PagedCollectionView ( new List<SystemRoleDto> () );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            CloseCommand = commandFactoryHelper.BuildDelegateCommand ( () => CloseCommand, ExecuteCloseCommand );

            RoleWorkingModeChangedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => RoleWorkingModeChangedCommand, ExecuteRoleWorkingModeChangedCommand );
            PermissionToBeRemovedCheckedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => PermissionToBeRemovedCheckedCommand, ExecutePermissionToBeRemovedCheckedCommand );
            PermissionToBeAddedCheckedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => PermissionToBeAddedCheckedCommand, ExecutePermissionToBeAddedCheckedCommand );
            TaskToBeRemovedCheckedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => TaskToBeRemovedCheckedCommand, ExecuteTaskToBeRemovedCheckedCommand );
            TaskToBeAddedCheckedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => TaskToBeAddedCheckedCommand, ExecuteTaskToBeAddedCheckedCommand );

            AddToTaskCommand = commandFactoryHelper.BuildDelegateCommand (
                () => AddToTaskCommand, ExecuteAddToTaskCommand, CanExecuteAddToTaskCommand );
            CreateNewTaskCommand = commandFactoryHelper.BuildDelegateCommand (
                () => CreateNewTaskCommand, ExecuteCreateNewTaskCommand, CanExecuteCreateNewTaskCommand );
            RenameTaskCommand = commandFactoryHelper.BuildDelegateCommand (
                () => RenameTaskCommand, ExecuteRenameTaskCommand, CanExecuteRenameTaskCommand );
            SaveTaskNameCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => SaveTaskNameCommand, ExecuteSaveTaskNameCommand );
            CancelSaveTaskNameCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => CancelSaveTaskNameCommand, ExecuteCancelSaveTaskNameCommand );
            RemoveFromTaskCommand = commandFactoryHelper.BuildDelegateCommand (
                () => RemoveFromTaskCommand, ExecuteRemoveFromTaskCommand, CanExecuteRemoveFromTaskCommand );

            AddToTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand (
                () => AddToTaskGroupCommand, ExecuteAddToTaskGroupCommand, CanExecuteAddToTaskGroupCommand );
            CreateNewTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand (
                () => CreateNewTaskGroupCommand, ExecuteCreateNewTaskGroupCommand, CanExecuteCreateNewTaskGroupCommand );
            RenameTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand (
                () => RenameTaskGroupCommand, ExecuteRenameTaskGroupCommand, CanExecuteRenameTaskGroupCommand );
            SaveTaskGroupNameCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => SaveTaskGroupNameCommand, ExecuteSaveTaskGroupNameCommand );
            CancelSaveTaskGroupNameCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => CancelSaveTaskGroupNameCommand, ExecuteCancelSaveTaskGroupNameCommand );
            RemoveFromTaskGroupCommand = commandFactoryHelper.BuildDelegateCommand (
                () => RemoveFromTaskGroupCommand, ExecuteRemoveFromTaskGroupCommand, CanExecuteRemoveFromTaskGroupCommand );

            CreateTaskCommand = NavigationCommandManager.BuildCommand ( () => CreateTaskCommand, NavigateToCreateTaskCommand );
            CreateTaskGroupCommand = NavigationCommandManager.BuildCommand ( () => CreateTaskGroupCommand, NavigateToCreateTaskGroupCommand );
            EditCommand = NavigationCommandManager.BuildCommand ( () => EditCommand, NavigateToEditCommand );
            CloneCommand = NavigationCommandManager.BuildCommand ( () => CloneCommand, NavigateToCloneCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the add to task command.
        /// </summary>
        /// <value>The add to task command.</value>
        public ICommand AddToTaskCommand { get; set; }

        /// <summary>
        /// Gets or sets the add to task group command.
        /// </summary>
        /// <value>The add to task group command.</value>
        public ICommand AddToTaskGroupCommand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [begin edit task flag].
        /// </summary>
        /// <value><c>true</c> if [begin edit task flag]; otherwise, <c>false</c>.</value>
        public bool BeginEditTaskFlag
        {
            get { return _beginEditTaskFlag; }
            set { ApplyPropertyChange ( ref _beginEditTaskFlag, () => BeginEditTaskFlag, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [begin edit task group flag].
        /// </summary>
        /// <value><c>true</c> if [begin edit task group flag]; otherwise, <c>false</c>.</value>
        public bool BeginEditTaskGroupFlag
        {
            get { return _beginEditTaskGroupFlag; }
            set { ApplyPropertyChange ( ref _beginEditTaskGroupFlag, () => BeginEditTaskGroupFlag, value ); }
        }

        /// <summary>
        /// Gets or sets the cancel save task group name command.
        /// </summary>
        /// <value>The cancel save task group name command.</value>
        public ICommand CancelSaveTaskGroupNameCommand { get; set; }

        /// <summary>
        /// Gets or sets the cancel save task name command.
        /// </summary>
        /// <value>The cancel save task name command.</value>
        public ICommand CancelSaveTaskNameCommand { get; set; }

        /// <summary>
        /// Gets the clone command.
        /// </summary>
        public INavigationCommand CloneCommand { get; private set; }

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        /// <value>The close command.</value>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Gets or sets the create new task command.
        /// </summary>
        /// <value>The create new task command.</value>
        public ICommand CreateNewTaskCommand { get; set; }

        /// <summary>
        /// Gets or sets the create new task group command.
        /// </summary>
        /// <value>The create new task group command.</value>
        public ICommand CreateNewTaskGroupCommand { get; set; }

        /// <summary>
        /// Gets the create task command.
        /// </summary>
        public INavigationCommand CreateTaskCommand { get; private set; }

        /// <summary>
        /// Gets the create task group command.
        /// </summary>
        public INavigationCommand CreateTaskGroupCommand { get; private set; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        public INavigationCommand EditCommand { get; private set; }

        /// <summary>
        /// Gets the is begun with task working mode.
        /// </summary>
        public bool? IsBegunWithTaskWorkingMode
        {
            get { return _isBegunWithTaskWorkingMode; }
            private set { ApplyPropertyChange ( ref _isBegunWithTaskWorkingMode, () => IsBegunWithTaskWorkingMode, value ); }
        }

        /// <summary>
        /// Gets the permission collection view.
        /// </summary>
        public PagedCollectionView PermissionCollectionView
        {
            get { return _permissionCollectionView; }
            private set { ApplyPropertyChange ( ref _permissionCollectionView, () => PermissionCollectionView, value ); }
        }

        /// <summary>
        /// Gets or sets the permission to be added checked command.
        /// </summary>
        /// <value>The permission to be added checked command.</value>
        public ICommand PermissionToBeAddedCheckedCommand { get; set; }

        /// <summary>
        /// Gets or sets the permission to be removed checked command.
        /// </summary>
        /// <value>The permission to be removed checked command.</value>
        public ICommand PermissionToBeRemovedCheckedCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove from task command.
        /// </summary>
        /// <value>The remove from task command.</value>
        public ICommand RemoveFromTaskCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove from task group command.
        /// </summary>
        /// <value>The remove from task group command.</value>
        public ICommand RemoveFromTaskGroupCommand { get; set; }

        /// <summary>
        /// Gets or sets the rename task command.
        /// </summary>
        /// <value>The rename task command.</value>
        public ICommand RenameTaskCommand { get; set; }

        /// <summary>
        /// Gets or sets the rename task group command.
        /// </summary>
        /// <value>The rename task group command.</value>
        public ICommand RenameTaskGroupCommand { get; set; }

        /// <summary>
        /// Gets or sets the role working mode changed command.
        /// </summary>
        /// <value>The role working mode changed command.</value>
        public ICommand RoleWorkingModeChangedCommand { get; set; }

        /// <summary>
        /// Gets or sets the save task group name command.
        /// </summary>
        /// <value>The save task group name command.</value>
        public ICommand SaveTaskGroupNameCommand { get; set; }

        /// <summary>
        /// Gets or sets the save task name command.
        /// </summary>
        /// <value>The save task name command.</value>
        public ICommand SaveTaskNameCommand { get; set; }

        /// <summary>
        /// Gets or sets the selected item in task group list.
        /// </summary>
        /// <value>The selected item in task group list.</value>
        public object SelectedItemInTaskGroupList
        {
            get { return _selectedItemInTaskGroupList; }
            set
            {
                if ( value != null )
                {
                    if ( ( value as SystemRoleDto ) != null && ( value as SystemRoleDto ).SystemRoleType == SystemRoleType.TaskGroup )
                    {
                        var selectedTaskGroup = value as SystemRoleDto;

                        var taskList = TaskCollectionView.SourceCollection.OfType<SystemRoleDto> ();
                        foreach ( var task in taskList )
                        {
                            task.IsSelected = selectedTaskGroup.GrantedSystemRoles.Any ( p => p.Key == task.Key );
                            task.IsSelectable = !( _isBegunWithTaskWorkingMode ?? false ) && !task.IsSelected;

                            foreach ( var permission in task.GrantedSystemPermissions )
                            {
                                permission.IsSelected = true;
                                permission.IsSelectable = false;
                            }
                        }

                        // Refresh task list to only show selectable tasks after a task group is selected
                        TaskCollectionView.Filter = ( p => ( p as SystemRoleDto ).IsSelectable );
                        TaskCollectionView.Refresh ();

                        var taskGroupList = TaskGroupCollectionView.SourceCollection.OfType<SystemRoleDto> ();
                        foreach ( var taskGroup in taskGroupList )
                        {
                            if ( taskGroup.Key == selectedTaskGroup.Key )
                            {
                                foreach ( var task in taskGroup.GrantedSystemRoles )
                                {
                                    task.IsSelected = false;
                                    task.IsSelectable = true;
                                }
                            }
                            else
                            {
                                foreach ( var task in taskGroup.GrantedSystemRoles )
                                {
                                    task.IsSelected = false;
                                    task.IsSelectable = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // task or even permission under taskGroup treeview selected
                        // this should never happen since the event will be cancelled (handled) in PreviewSelectionChanged of treeview 
                        UpdateTaskCollectionViewAndTaskGroupCollectionView ();
                    }
                }
                else
                {
                    // no task group selected
                    UpdateTaskCollectionViewAndTaskGroupCollectionView ();
                }

                _taskToBeAddedCount = 0;
                _taskToBeRemovedCount = 0;
                ApplyPropertyChange ( ref _selectedItemInTaskGroupList, () => SelectedItemInTaskGroupList, value );
                RefreshCanExecuteCommandFlags ();
            }
        }

        /// <summary>
        /// Gets or sets the selected item in task list.
        /// </summary>
        /// <value>The selected item in task list.</value>
        public object SelectedItemInTaskList
        {
            get { return _selectedItemInTaskList; }
            set
            {
                if ( value != null )
                {
                    // task selected
                    if ( ( value as SystemRoleDto ) != null && ( value as SystemRoleDto ).SystemRoleType == SystemRoleType.Task )
                    {
                        var selectedTask = value as SystemRoleDto;
                        var permissionList = PermissionCollectionView.SourceCollection.OfType<SystemPermissionDto> ();
                        foreach ( var permission in permissionList )
                        {
                            permission.IsSelected = selectedTask.GrantedSystemPermissions.Any ( p => p.Key == permission.Key );
                            permission.IsSelectable = !permission.IsSelected;
                        }

                        // Refresh permission list to only show selectable permissions after a task is selected
                        PermissionCollectionView.Filter = ( p => ( p as SystemPermissionDto ).IsSelectable );
                        PermissionCollectionView.Refresh ();

                        var taskList = TaskCollectionView.SourceCollection.OfType<SystemRoleDto> ();
                        foreach ( var task in taskList )
                        {
                            if ( task.Key == selectedTask.Key )
                            {
                                foreach ( var permission in task.GrantedSystemPermissions )
                                {
                                    permission.IsSelected = false;
                                    permission.IsSelectable = IsBegunWithTaskWorkingMode ?? false;
                                }
                            }
                            else
                            {
                                foreach ( var permission in task.GrantedSystemPermissions )
                                {
                                    permission.IsSelected = false;
                                    permission.IsSelectable = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // if permission under task treeview selected, then make all permission not selectable
                        // this should never happen since the event will be cancelled (handled) in PreviewSelectionChanged of treeview 
                        UpdatePermissionCollectionViewAndTaskCollectionView ();
                    }
                }
                else
                {
                    // no task selected 
                    UpdatePermissionCollectionViewAndTaskCollectionView ();
                }

                _permissionToBeAddedCount = 0;
                _permissionToBeRemovedCount = 0;
                ApplyPropertyChange ( ref _selectedItemInTaskList, () => SelectedItemInTaskList, value );
                RefreshCanExecuteCommandFlags ();
            }
        }

        /// <summary>
        /// Gets or sets the system role checked command.
        /// </summary>
        /// <value>The system role checked command.</value>
        public ICommand SystemRoleCheckedCommand { get; set; }

        /// <summary>
        /// Gets the task collection view.
        /// </summary>
        public PagedCollectionView TaskCollectionView
        {
            get { return _taskCollectionView; }
            private set { ApplyPropertyChange ( ref _taskCollectionView, () => TaskCollectionView, value ); }
        }

        /// <summary>
        /// Gets the task group collection view.
        /// </summary>
        public PagedCollectionView TaskGroupCollectionView
        {
            get { return _taskGroupCollectionView; }
            private set { ApplyPropertyChange ( ref _taskGroupCollectionView, () => TaskGroupCollectionView, value ); }
        }

        /// <summary>
        /// Gets or sets the task to be added checked command.
        /// </summary>
        /// <value>The task to be added checked command.</value>
        public ICommand TaskToBeAddedCheckedCommand { get; set; }

        /// <summary>
        /// Gets or sets the task to be removed checked command.
        /// </summary>
        /// <value>The task to be removed checked command.</value>
        public ICommand TaskToBeRemovedCheckedCommand { get; set; }

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

        private bool CanExecuteAddToTaskCommand ()
        {
            return _permissionToBeAddedCount > 0;
        }

        private bool CanExecuteAddToTaskGroupCommand ()
        {
            return _taskToBeAddedCount > 0;
        }

        private bool CanExecuteCreateNewTaskCommand ()
        {
            return true;
        }

        private bool CanExecuteCreateNewTaskGroupCommand ()
        {
            return true;
        }

        private bool CanExecuteRemoveFromTaskCommand ()
        {
            return _permissionToBeRemovedCount > 0;
        }

        private bool CanExecuteRemoveFromTaskGroupCommand ()
        {
            return _taskToBeRemovedCount > 0;
        }

        private bool CanExecuteRenameTaskCommand ()
        {
            return ( _selectedItemInTaskList != null && ( _selectedItemInTaskList as SystemRoleDto ) != null
                     && ( _selectedItemInTaskList as SystemRoleDto ).SystemRoleType == SystemRoleType.Task );
        }

        private bool CanExecuteRenameTaskGroupCommand ()
        {
            return ( _selectedItemInTaskGroupList != null && ( _selectedItemInTaskGroupList as SystemRoleDto ) != null
                     && ( _selectedItemInTaskGroupList as SystemRoleDto ).SystemRoleType == SystemRoleType.TaskGroup );
        }

        private void ExecuteAddToTaskCommand ()
        {
            if ( PermissionCollectionView != null && _selectedItemInTaskList != null && ( _selectedItemInTaskList as SystemRoleDto ) != null )
            {
                var permissionList = PermissionCollectionView.SourceCollection.OfType<SystemPermissionDto> ();
                if ( permissionList.Any ( p => p.IsSelected && p.IsSelectable ) )
                {
                    // Execute the command
                    var permissionKeysToBeAdded =
                        ( from permission in _permissionCollectionView.SourceCollection.OfType<SystemPermissionDto> ()
                          where permission.IsSelectable && permission.IsSelected
                          select permission.Key ).ToList ();
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add (
                        new GrantSystemPermissionRequest
                            {
                                SystemRoleKey = ( _selectedItemInTaskList as SystemRoleDto ).Key,
                                SystemPermissionKeysToBeGranted = permissionKeysToBeAdded
                            } );
                    requestDispatcher.ProcessRequests ( HandleTaskCommandCompleted, HandleAddToTaskException );
                    IsLoading = true;
                }
            }
        }

        private void ExecuteAddToTaskGroupCommand ()
        {
            if ( TaskCollectionView != null && _selectedItemInTaskGroupList != null && ( _selectedItemInTaskGroupList as SystemRoleDto ) != null
                 && ( _selectedItemInTaskGroupList as SystemRoleDto ).SystemRoleType == SystemRoleType.TaskGroup )
            {
                var taskList = TaskCollectionView.SourceCollection.OfType<SystemRoleDto> ();
                if ( taskList.Any ( task => task.IsSelected && task.IsSelectable ) )
                {
                    // Execute the command
                    var taskKeysToBeAdded = ( from task in _taskCollectionView.SourceCollection.OfType<SystemRoleDto> ()
                                                     where task.IsSelectable && task.IsSelected
                                                     select task.Key ).ToList ();
                    if ( taskKeysToBeAdded.Count > 0 )
                    {
                        var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                        requestDispatcher.Add (
                            new GrantSystemRoleRequest
                                {
                                    SystemRoleKey = ( _selectedItemInTaskGroupList as SystemRoleDto ).Key,
                                    GrantedSystemRoleKeys = taskKeysToBeAdded
                                } );
                        requestDispatcher.ProcessRequests ( HandleTaskGroupCommandCompleted, HandleAddToTaskGroupException );
                        IsLoading = true;
                    }
                }
            }
        }

        private void ExecuteCancelSaveTaskGroupNameCommand ( object parameter )
        {
            var systemRole = parameter as SystemRoleDto;
            if ( systemRole != null )
            {
                if ( systemRole.Key > 0 )
                {
                    // Do nothing
                }
                else
                {
                    // Remove the newly added task
                    var _taskGroupList = _taskGroupCollectionView.SourceCollection as IList<SystemRoleDto>;
                    _taskGroupList.Remove ( systemRole );
                }

                BeginEditTaskFlag = false;
            }
        }

        private void ExecuteCancelSaveTaskNameCommand ( object parameter )
        {
            var systemRole = parameter as SystemRoleDto;
            if ( systemRole != null )
            {
                if ( systemRole.Key > 0 )
                {
                    // Do nothing
                }
                else
                {
                    // Remove the newly added task
                    var _taskList = _taskCollectionView.SourceCollection as IList<SystemRoleDto>;
                    _taskList.Remove ( systemRole );
                }

                BeginEditTaskFlag = false;
            }
        }

        private void ExecuteCloseCommand ()
        {
            _popupService.ClosePopup ( "EditTaskAndTaskGroupView" );
        }

        private void ExecuteCreateNewTaskCommand ()
        {
            // Execute the command
            var taskList = _taskCollectionView.SourceCollection as IList<SystemRoleDto>;
            var name = GetAvailableRoleName ( "New Task", taskList );
            var newTask = new SystemRoleDto
                {
                    Name = name,
                    SystemRoleType = SystemRoleType.Task,
                    GrantedSystemPermissions = new ObservableCollection<SystemPermissionDto> (),
                    GrantedSystemRoles = new ObservableCollection<SystemRoleDto> ()
                };
            taskList.Add ( newTask );
            SelectedItemInTaskList = newTask;

            BeginEditTaskFlag = false;
            BeginEditTaskFlag = true;
        }

        private void ExecuteCreateNewTaskGroupCommand ()
        {
            // Execute the command
            var taskGroupList = _taskGroupCollectionView.SourceCollection as IList<SystemRoleDto>;
            var name = GetAvailableRoleName ( "New Task Group", taskGroupList );
            var newTaskGroup = new SystemRoleDto
                {
                    Name = name,
                    SystemRoleType = SystemRoleType.TaskGroup,
                    GrantedSystemPermissions = new ObservableCollection<SystemPermissionDto> (),
                    GrantedSystemRoles = new ObservableCollection<SystemRoleDto> ()
                };
            taskGroupList.Add ( newTaskGroup );
            SelectedItemInTaskGroupList = newTaskGroup;

            BeginEditTaskGroupFlag = false;
            BeginEditTaskGroupFlag = true;
        }

        private void ExecutePermissionToBeAddedCheckedCommand ( object isChecked )
        {
            _permissionToBeAddedCount = bool.Parse ( isChecked == null ? "false" : isChecked.ToString () )
                                            ? ++_permissionToBeAddedCount
                                            : --_permissionToBeAddedCount;

            ( ( DelegateCommandBase )AddToTaskCommand ).RaiseCanExecuteChanged ();
        }

        private void ExecutePermissionToBeRemovedCheckedCommand ( object isChecked )
        {
            _permissionToBeRemovedCount = bool.Parse ( isChecked == null ? "false" : isChecked.ToString () )
                                              ? ++_permissionToBeRemovedCount
                                              : --_permissionToBeRemovedCount;

            ( ( DelegateCommandBase )RemoveFromTaskCommand ).RaiseCanExecuteChanged ();
        }

        private void ExecuteRemoveFromTaskCommand ()
        {
            if ( _selectedItemInTaskList != null && ( _selectedItemInTaskList as SystemRoleDto ) != null )
            {
                var task = _selectedItemInTaskList as SystemRoleDto;
                if ( task.GrantedSystemPermissions.Any ( p => p.IsSelected ) )
                {
                    // Execute the command
                    var permissionKeysToBeRevoked = ( from permission in ( _selectedItemInTaskList as SystemRoleDto ).GrantedSystemPermissions
                                                             where permission.IsSelected
                                                             select permission.Key ).ToList ();
                    if ( permissionKeysToBeRevoked.Count > 0 )
                    {
                        var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                        requestDispatcher.Add (
                            new RevokeSystemPermissionRequest
                                {
                                    SystemRoleKey = ( _selectedItemInTaskList as SystemRoleDto ).Key,
                                    SystemPermissionKeysToBeRevoked = permissionKeysToBeRevoked
                                } );
                        requestDispatcher.ProcessRequests ( HandleTaskCommandCompleted, HandleRemoveFromTaskException );
                        IsLoading = true;
                    }
                }
            }
        }

        private void ExecuteRemoveFromTaskGroupCommand ()
        {
            if ( _selectedItemInTaskGroupList != null && ( _selectedItemInTaskGroupList as SystemRoleDto ) != null
                 && ( _selectedItemInTaskGroupList as SystemRoleDto ).SystemRoleType == SystemRoleType.TaskGroup )
            {
                var taskGroup = _selectedItemInTaskGroupList as SystemRoleDto;
                if ( taskGroup.GrantedSystemRoles.Any ( task => task.IsSelected ) )
                {
                    // Execute the command
                    var taskKeysToBeRevoked = ( from task in ( _selectedItemInTaskGroupList as SystemRoleDto ).GrantedSystemRoles
                                                       where task.IsSelected
                                                       select task.Key ).ToList ();
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add (
                        new RevokeSystemRoleRequest
                            {
                                SystemRoleKey = ( _selectedItemInTaskGroupList as SystemRoleDto ).Key,
                                SystemRoleKeysToBeRevoked = taskKeysToBeRevoked
                            } );
                    requestDispatcher.ProcessRequests ( HandleTaskGroupCommandCompleted, HandleRemoveFromTaskGroupException );
                    IsLoading = true;
                }
            }
        }

        private void ExecuteRenameTaskCommand ()
        {
            if ( _selectedItemInTaskList != null && ( _selectedItemInTaskList as SystemRoleDto ) != null )
            {
                BeginEditTaskFlag = false;
                BeginEditTaskFlag = true;
            }
        }

        private void ExecuteRenameTaskGroupCommand ()
        {
            if ( _selectedItemInTaskGroupList != null && ( _selectedItemInTaskGroupList as SystemRoleDto ) != null
                 && ( _selectedItemInTaskGroupList as SystemRoleDto ).SystemRoleType == SystemRoleType.TaskGroup )
            {
                // Execute the command
                BeginEditTaskGroupFlag = false;
                BeginEditTaskGroupFlag = true;
            }
        }

        private void ExecuteRoleWorkingModeChangedCommand ( object isTaskWorkingMode )
        {
            if ( isTaskWorkingMode == null )
            {
                //create new one
                if ( !_isTaskWorkingModeInitialized )
                {
                    LoadPermissionsAndTasks ();
                }
                return;
            }

            var roleWorkingMode = bool.Parse ( isTaskWorkingMode.ToString () );
            IsBegunWithTaskWorkingMode = roleWorkingMode;

            if ( roleWorkingMode )
            {
                // Task working mode
                if ( !_isTaskWorkingModeInitialized )
                {
                    LoadPermissionsAndTasks ();
                }
                else
                {
                    UpdateSelectedItemInTreeView ( true );
                }
            }
            else
            {
                // Taskgroup working mode
                if ( !_isTaskGroupWorkingModeInitialized )
                {
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    if ( !_isTaskWorkingModeInitialized )
                    {
                        requestDispatcher.Add ( new GetDtoRequest<TaskSystemRolesDto> () );
                    }
                    requestDispatcher.Add ( new GetDtoRequest<TaskGroupSystemRolesDto> () );
                    requestDispatcher.ProcessRequests ( HandleInitializationForTaskGroupWorkingModeCompleted, HandleInitializationException );
                    IsLoading = true;
                }
                else
                {
                    UpdateSelectedItemInTreeView ( false );
                }
            }
        }

        private void ExecuteSaveTaskGroupNameCommand ( object parameter )
        {
            var systemRole = parameter as SystemRoleDto;
            if ( systemRole != null )
            {
                if ( systemRole.Key > 0 )
                {
                    var request = new RenameSystemRoleRequest
                        { SystemRoleKey = systemRole.Key, Name = systemRole.Name, Description = systemRole.Description };
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add ( request );
                    requestDispatcher.ProcessRequests ( HandleTaskGroupCommandCompleted, HandleRenameTaskGroupException );
                    IsLoading = true;
                }
                else
                {
                    var request = new CreateSystemRoleRequest
                        { Name = systemRole.Name, Description = systemRole.Description, SystemRoleType = systemRole.SystemRoleType };
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add ( request );
                    requestDispatcher.ProcessRequests ( HandleTaskGroupCommandCompleted, HandleCreateNewTaskGroupException );
                    IsLoading = true;
                }

                BeginEditTaskFlag = false;
            }
        }

        private void ExecuteSaveTaskNameCommand ( object parameter )
        {
            var systemRole = parameter as SystemRoleDto;
            if ( systemRole != null )
            {
                if ( systemRole.Key > 0 )
                {
                    var request = new RenameSystemRoleRequest
                        { SystemRoleKey = systemRole.Key, Name = systemRole.Name, Description = systemRole.Description };
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add ( request );
                    requestDispatcher.ProcessRequests ( HandleTaskCommandCompleted, HandleRenameTaskException );
                    IsLoading = true;
                }
                else
                {
                    var request = new CreateSystemRoleRequest
                        { Name = systemRole.Name, Description = systemRole.Description, SystemRoleType = systemRole.SystemRoleType };
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add ( request );
                    requestDispatcher.ProcessRequests ( HandleTaskCommandCompleted, HandleCreateNewTaskException );
                    IsLoading = true;
                }
                BeginEditTaskFlag = false;
                BeginEditTaskFlag = true;
            }
        }

        private void ExecuteTaskToBeAddedCheckedCommand ( object isChecked )
        {
            _taskToBeAddedCount = bool.Parse ( isChecked == null ? "false" : isChecked.ToString () ) ? ++_taskToBeAddedCount : --_taskToBeAddedCount;

            ( ( DelegateCommandBase )AddToTaskGroupCommand ).RaiseCanExecuteChanged ();
        }

        private void ExecuteTaskToBeRemovedCheckedCommand ( object isChecked )
        {
            _taskToBeRemovedCount = bool.Parse ( isChecked == null ? "false" : isChecked.ToString () )
                                        ? ++_taskToBeRemovedCount
                                        : --_taskToBeRemovedCount;

            ( ( DelegateCommandBase )RemoveFromTaskGroupCommand ).RaiseCanExecuteChanged ();
        }

        private string GetAvailableRoleName ( string suggestedName, IList<SystemRoleDto> roleDtos )
        {
            var i = 2;
            var name = suggestedName;
            while ( roleDtos.Any ( r => r.Name == name ) )
            {
                name = string.Format ( "{0} {1}", suggestedName, i );
                i++;
            }
            return name;
        }

        private void GetSystemPermissionListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<SystemPermissionsDto>> ();
            var systemPermissionList = response.DataTransferObject.SystemPermissions;
            PermissionCollectionView = new PagedCollectionView ( systemPermissionList );

            if ( _isTaskGroupWorkingModeInitialized )
            {
                UpdateSelectedItemInTreeView ( true );
            }
        }

        private void GetTaskGroupSystemRoleListCompleted ( ReceivedResponses receivedResponses )
        {
            _isTaskGroupWorkingModeInitialized = true;

            var response = receivedResponses.Get<DtoResponse<TaskGroupSystemRolesDto>> ();
            var taskGroupSystemRoleList = response.DataTransferObject.SystemRoles;
            taskGroupSystemRoleList = new ObservableCollection<SystemRoleDto> ( taskGroupSystemRoleList.OrderBy ( p => p.Name ) );
            foreach ( var taskGroup in taskGroupSystemRoleList )
            {
                taskGroup.GrantedSystemRoles = new ObservableCollection<SystemRoleDto> ( taskGroup.GrantedSystemRoles.OrderBy ( p => p.Name ) );
                foreach ( var task in taskGroup.GrantedSystemRoles )
                {
                    task.GrantedSystemPermissions =
                        new ObservableCollection<SystemPermissionDto> ( task.GrantedSystemPermissions.OrderBy ( p => p.DisplayName ) );
                }
            }

            TaskGroupCollectionView = new PagedCollectionView ( taskGroupSystemRoleList );

            var initialTaskGroupToEdit = taskGroupSystemRoleList.SingleOrDefault ( p => p.Key == _initialSystemRoleKeyToEdit );
            if ( initialTaskGroupToEdit != null )
            {
                SelectedItemInTaskGroupList = initialTaskGroupToEdit;
            }
            else if ( _isAddingNewGroupTask )
            {
                _isAddingNewGroupTask = false;
                ExecuteCreateNewTaskGroupCommand ();
                Thread.Sleep ( 500 );
            }
            else
            {
                UpdateSelectedItemInTreeView ( false );
            }
        }

        private void GetTaskSystemRoleListCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<TaskSystemRolesDto>> ();
            var taskSystemRoleList = response.DataTransferObject.SystemRoles;
            taskSystemRoleList = new ObservableCollection<SystemRoleDto> ( taskSystemRoleList.OrderBy ( p => p.Name ).ToList () );
            foreach ( var task in taskSystemRoleList )
            {
                task.GrantedSystemPermissions =
                    new ObservableCollection<SystemPermissionDto> ( task.GrantedSystemPermissions.OrderBy ( p => p.DisplayName ) );
            }

            TaskCollectionView = new PagedCollectionView ( taskSystemRoleList );

            if ( IsBegunWithTaskWorkingMode.HasValue && IsBegunWithTaskWorkingMode.Value )
            {
                var initialTaskToEdit = taskSystemRoleList.SingleOrDefault ( p => p.Key == _initialSystemRoleKeyToEdit );
                if ( initialTaskToEdit != null )
                {
                    SelectedItemInTaskList = initialTaskToEdit;
                }
                else if ( _isAddingNewTask )
                {
                    _isAddingNewTask = false;
                    ExecuteCreateNewTaskCommand ();
                    Thread.Sleep ( 500 );
                }
                else if ( !_isTaskGroupWorkingModeInitialized )
                {
                    UpdateSelectedItemInTreeView ( true );
                }
            }
        }

        private void HandleAddToTaskException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Add permissions to task failed", UserDialogServiceOptions.Ok );
        }

        private void HandleAddToTaskGroupException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Add task to task group failed", UserDialogServiceOptions.Ok );
        }

        private void HandleCloneRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            if ( validationErrors.Count () == 0 )
            {
                var clonedSystemRole = response.SystemRole;
                _initialSystemRoleKeyToEdit = clonedSystemRole.Key;

                switch ( clonedSystemRole.SystemRoleType )
                {
                    case SystemRoleType.Task:
                        ExecuteRoleWorkingModeChangedCommand ( true );
                        break;
                    case SystemRoleType.TaskGroup:
                        ExecuteRoleWorkingModeChangedCommand ( false );
                        break;
                }
            }
            else
            {
                ProcessValidationErrors ( validationErrors );
            }
            IsLoading = false;
        }

        private void HandleCloneRequestException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Clone failed", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleCreateNewTaskException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Create new task failed", UserDialogServiceOptions.Ok );
        }

        private void HandleCreateNewTaskGroupException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Create new task group failed", UserDialogServiceOptions.Ok );
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Initialization failed", UserDialogServiceOptions.Ok );
        }

        private void HandleInitializationForTaskGroupWorkingModeCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            if ( !_isTaskWorkingModeInitialized )
            {
                GetTaskSystemRoleListCompleted ( receivedResponses );
            }
            GetTaskGroupSystemRoleListCompleted ( receivedResponses );
        }

        private void HandleInitializationForTaskWorkingModeCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            GetSystemPermissionListCompleted ( receivedResponses );

            if ( !_isTaskGroupWorkingModeInitialized )
            {
                GetTaskSystemRoleListCompleted ( receivedResponses );
            }
            _isTaskWorkingModeInitialized = true;
        }

        private void HandleRemoveFromTaskException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Remove permissions from task failed", UserDialogServiceOptions.Ok );
        }

        private void HandleRemoveFromTaskGroupException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Remove tasks from task group failed", UserDialogServiceOptions.Ok );
        }

        private void HandleRenameTaskException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Rename task failed", UserDialogServiceOptions.Ok );
        }

        private void HandleRenameTaskGroupException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Rename task group failed", UserDialogServiceOptions.Ok );
        }

        private void HandleTaskCommandCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            if ( validationErrors.Count () == 0 )
            {
                var newlyChangedTask = response.SystemRole;

                RefreshForTaskChanges ( newlyChangedTask );
            }
            else
            {
                ProcessValidationErrors ( validationErrors );
            }
        }

        private void HandleTaskGroupCommandCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<SystemRoleCommandResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            if ( validationErrors.Count () == 0 )
            {
                var newlyChangedTaskGroup = response.SystemRole;

                RefreshForTaskGroupChanges ( newlyChangedTaskGroup );
            }
            else
            {
                ProcessValidationErrors ( validationErrors );
            }
        }

        private void LoadPermissionsAndTasks ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<SystemPermissionsDto> () );
            if ( !_isTaskGroupWorkingModeInitialized )
            {
                requestDispatcher.Add ( new GetDtoRequest<TaskSystemRolesDto> () );
            }
            requestDispatcher.ProcessRequests ( HandleInitializationForTaskWorkingModeCompleted, HandleInitializationException );
            IsLoading = true;
        }

        private void NavigateToCloneCommand ( KeyValuePair<string, string>[] parameters )
        {
            var systemRoleKeyToClone = parameters.GetValue<long> ( "SystemRoleKey" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new CloneSystemRoleRequest { SystemRoleKey = systemRoleKeyToClone } );
            requestDispatcher.ProcessRequests ( HandleCloneRequestCompleted, HandleCloneRequestException );
            IsLoading = true;
        }

        private void NavigateToCreateCommand ( KeyValuePair<string, string>[] parameters )
        {
            ExecuteRoleWorkingModeChangedCommand ( null );
        }

        private void NavigateToCreateTaskCommand ( KeyValuePair<string, string>[] parameters )
        {
            _isAddingNewTask = true;
            ExecuteRoleWorkingModeChangedCommand ( true );
        }

        private void NavigateToCreateTaskGroupCommand ( KeyValuePair<string, string>[] parameters )
        {
            _isAddingNewGroupTask = true;
            ExecuteRoleWorkingModeChangedCommand ( false );
        }

        private void NavigateToEditCommand ( KeyValuePair<string, string>[] parameters )
        {
            _initialSystemRoleKeyToEdit = parameters.GetValue<long> ( "SystemRoleKey" );
            var initialSystemRoleTypeToEdit =
                ( SystemRoleType )Enum.Parse ( typeof( SystemRoleType ), parameters.GetValue<string> ( "SystemRoleType" ), true );

            switch ( initialSystemRoleTypeToEdit )
            {
                case SystemRoleType.Task:
                    ExecuteRoleWorkingModeChangedCommand ( true );
                    break;
                case SystemRoleType.TaskGroup:
                    ExecuteRoleWorkingModeChangedCommand ( false );
                    break;
            }
        }

        private void ProcessValidationErrors ( IEnumerable<DataErrorInfo> validationErrors )
        {
            var errors = new StringBuilder ();
            errors.AppendLine ( "The following errors occurred:" );

            foreach ( var validationError in validationErrors )
            {
                errors.AppendLine ( validationError.Message );
            }

            _userDialogService.ShowDialog ( errors.ToString (), "Errors", UserDialogServiceOptions.Ok );
        }

        private void RefreshCanExecuteCommandFlags ()
        {
            ( ( DelegateCommandBase )AddToTaskCommand ).RaiseCanExecuteChanged ();
            ( ( DelegateCommandBase )CreateNewTaskCommand ).RaiseCanExecuteChanged ();
            ( ( DelegateCommandBase )RenameTaskCommand ).RaiseCanExecuteChanged ();
            ( ( DelegateCommandBase )RemoveFromTaskCommand ).RaiseCanExecuteChanged ();

            ( ( DelegateCommandBase )AddToTaskGroupCommand ).RaiseCanExecuteChanged ();
            ( ( DelegateCommandBase )CreateNewTaskGroupCommand ).RaiseCanExecuteChanged ();
            ( ( DelegateCommandBase )RenameTaskGroupCommand ).RaiseCanExecuteChanged ();
            ( ( DelegateCommandBase )RemoveFromTaskGroupCommand ).RaiseCanExecuteChanged ();
        }

        private void RefreshForTaskChanges ( SystemRoleDto newlyChangedTask )
        {
            // Update GrantedSystemPermissions for task
            foreach ( var permission in newlyChangedTask.GrantedSystemPermissions )
            {
                permission.IsSelectable = true;
                permission.IsSelected = false;
            }

            newlyChangedTask.GrantedSystemPermissions =
                new ObservableCollection<SystemPermissionDto> ( newlyChangedTask.GrantedSystemPermissions.OrderBy ( p => p.DisplayName ) );

            // Update task collection view
            var taskList = _taskCollectionView.SourceCollection as IList<SystemRoleDto>;

            var taskToBeRemoved = taskList.Where ( p => p.Key == 0 ).FirstOrDefault ();
            if ( taskToBeRemoved != null )
            {
                taskList.Remove ( taskToBeRemoved );
            }
            taskToBeRemoved = taskList.Where ( p => p.Key == newlyChangedTask.Key ).FirstOrDefault ();
            if ( taskToBeRemoved != null )
            {
                taskList.Remove ( taskToBeRemoved );
            }

            taskList.Add ( newlyChangedTask );

            _taskCollectionView.SortDescriptions.Add ( new SortDescription ( "Name", ListSortDirection.Ascending ) );
            _taskCollectionView.Refresh ();

            // Update the SelectedItemInTaskList
            SelectedItemInTaskList = newlyChangedTask;

            // Update task group collection view
            var taskGroupList = _taskGroupCollectionView.SourceCollection as IList<SystemRoleDto>;
            foreach ( var taskGroup in taskGroupList )
            {
                if ( taskGroup.GrantedSystemRoles.Any ( p => p.Key == newlyChangedTask.Key ) )
                {
                    var taskToRemovedFromTaskGroup = taskGroup.GrantedSystemRoles.First ( p => p.Key == newlyChangedTask.Key );
                    taskGroup.GrantedSystemRoles.Remove ( taskToRemovedFromTaskGroup );

                    taskGroup.GrantedSystemRoles.Add ( newlyChangedTask );
                }
            }
        }

        private void RefreshForTaskGroupChanges ( SystemRoleDto newlyChangedTaskGroup )
        {
            // Update GrantedSystemRoles and GrantedSystemPermissions for taskGroup
            foreach ( var task in newlyChangedTaskGroup.GrantedSystemRoles )
            {
                task.IsSelectable = true;
                task.IsSelected = false;

                task.GrantedSystemPermissions =
                    new ObservableCollection<SystemPermissionDto> ( task.GrantedSystemPermissions.OrderBy ( p => p.DisplayName ) );
            }

            newlyChangedTaskGroup.GrantedSystemRoles =
                new ObservableCollection<SystemRoleDto> ( newlyChangedTaskGroup.GrantedSystemRoles.OrderBy ( p => p.Name ) );

            // Update taskGroup collection view
            var taskGroupList = _taskGroupCollectionView.SourceCollection as IList<SystemRoleDto>;

            var taskGroupToBeRemoved = taskGroupList.Where ( p => p.Key == 0 ).FirstOrDefault ();
            if ( taskGroupToBeRemoved != null )
            {
                taskGroupList.Remove ( taskGroupToBeRemoved );
            }
            taskGroupToBeRemoved = taskGroupList.Where ( p => p.Key == newlyChangedTaskGroup.Key ).FirstOrDefault ();
            if ( taskGroupToBeRemoved != null )
            {
                taskGroupList.Remove ( taskGroupToBeRemoved );
            }

            taskGroupList.Add ( newlyChangedTaskGroup );

            _taskGroupCollectionView.SortDescriptions.Add ( new SortDescription ( "Name", ListSortDirection.Ascending ) );
            _taskGroupCollectionView.Refresh ();

            // Update the SelectedItemInTaskGroupList
            SelectedItemInTaskGroupList = newlyChangedTaskGroup;
        }

        private void UpdatePermissionCollectionViewAndTaskCollectionView ()
        {
            var permissionList = PermissionCollectionView.SourceCollection.OfType<SystemPermissionDto> ();
            foreach ( var permission in permissionList )
            {
                permission.IsSelected = false;
                permission.IsSelectable = false;
            }

            // Refresh permission list to only show selectable permissions after a task is selected
            PermissionCollectionView.Filter = ( p => true );
            PermissionCollectionView.Refresh ();

            var taskList = TaskCollectionView.SourceCollection.OfType<SystemRoleDto> ();
            foreach ( var task in taskList )
            {
                task.IsSelected = false;
                task.IsSelectable = false;

                //if (task.GrantedSystemPermissions == null)
                //{
                //    // when load first time
                //    continue;
                //}
                foreach ( var permission in task.GrantedSystemPermissions )
                {
                    permission.IsSelected = false;
                    permission.IsSelectable = false;
                }
            }
        }

        private void UpdateSelectedItemInTreeView ( bool roleWorkingMode )
        {
            if ( roleWorkingMode )
            {
                SelectedItemInTaskGroupList = null;
                SelectedItemInTaskList = null;
            }
            else
            {
                SelectedItemInTaskList = null;
                SelectedItemInTaskGroupList = null;
            }
        }

        private void UpdateTaskCollectionViewAndTaskGroupCollectionView ()
        {
            // no task group selected
            var taskList = TaskCollectionView.SourceCollection.OfType<SystemRoleDto> ();
            foreach ( var task in taskList )
            {
                task.IsSelected = false;
                task.IsSelectable = false;

                foreach ( var permission in task.GrantedSystemPermissions )
                {
                    permission.IsSelected = true;
                    permission.IsSelectable = false;
                }
            }

            TaskCollectionView.Filter = ( p => true );
            TaskCollectionView.Refresh ();

            var taskGroupList = TaskGroupCollectionView.SourceCollection.OfType<SystemRoleDto> ();
            foreach ( var taskGroup in taskGroupList )
            {
                //if (taskGroup.GrantedSystemRoles == null)
                //{
                //    continue;
                //}
                foreach ( var task in taskGroup.GrantedSystemRoles )
                {
                    task.IsSelected = false;
                    task.IsSelectable = false;
                }
            }
        }

        #endregion
    }
}
