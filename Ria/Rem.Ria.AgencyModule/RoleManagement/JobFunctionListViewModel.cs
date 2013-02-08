using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.RoleManagement;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View Model for JobFunctionList class.
    /// </summary>
    public class JobFunctionListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly PagedCollectionViewWrapper<SystemRoleDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private SystemRoleDto _selectedJobFunction;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobFunctionListViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public JobFunctionListViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobFunctionListViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public JobFunctionListViewModel (
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

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            CreateJobFunctionCommand = commandFactoryHelper.BuildDelegateCommand ( () => CreateJobFunctionCommand, ExecuteCreateJobFunctionCommand );
            EditJobFunctionCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => EditJobFunctionCommand, ExecuteEditJobFunctionCommand );
            CloneJobFunctionCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => CloneJobFunctionCommand, ExecuteCloneJobFunctionCommand );
            DeleteJobFunctionCommand = commandFactoryHelper.BuildDelegateCommand<SystemRoleDto> (
                () => DeleteJobFunctionCommand, ExecuteDeleteJobFunction );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the clone job function command.
        /// </summary>
        public ICommand CloneJobFunctionCommand { get; private set; }

        /// <summary>
        /// Gets the create job function command.
        /// </summary>
        public ICommand CreateJobFunctionCommand { get; private set; }

        /// <summary>
        /// Gets the delete job function command.
        /// </summary>
        public ICommand DeleteJobFunctionCommand { get; private set; }

        /// <summary>
        /// Gets the edit job function command.
        /// </summary>
        public ICommand EditJobFunctionCommand { get; private set; }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<SystemRoleDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets or sets the selected job function.
        /// </summary>
        /// <value>The selected job function.</value>
        public SystemRoleDto SelectedJobFunction
        {
            get { return _selectedJobFunction; }
            set { ApplyPropertyChange ( ref _selectedJobFunction, () => SelectedJobFunction, value ); }
        }

        /// <summary>
        /// Gets or sets the selected job function dto.
        /// </summary>
        /// <value>The selected job function dto.</value>
        public SystemRoleDto SelectedJobFunctionDto { get; set; }

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
            GetAllJobFunctions ();
        }

        private void DeleteJobFunction ( long systemRoleKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new DeleteJobFunctionSystemRoleRequest { SystemRoleKey = systemRoleKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleDeleteJobFunctionSystemRoleCompleted, HandleDeletePJobFunctionException );
        }

        private void ExecuteCloneJobFunctionCommand ( SystemRoleDto systemRoleDto )
        {
            _popupService.ShowPopup (
                "EditJobFunctionView",
                "Clone",
                "Job Function",
                new[] { new KeyValuePair<string, string> ( "SystemRoleKey", systemRoleDto.Key.ToString () ) },
                false,
                PopupClosed );
        }

        private void ExecuteCreateJobFunctionCommand ()
        {
            _popupService.ShowPopup (
                "EditJobFunctionView",
                "Create",
                "Job Function",
                null,
                false,
                PopupClosed );
        }

        private void ExecuteDeleteJobFunction ( SystemRoleDto systemRoleDto )
        {
            var result = _userDialogService.ShowDialog (
                "Are you sure you want to delete?", "Confirmation", UserDialogServiceOptions.OkCancel );

            if ( result == UserDialogServiceResult.Ok )
            {
                DeleteJobFunction ( systemRoleDto.Key );
            }
        }

        private void ExecuteEditJobFunctionCommand ( SystemRoleDto systemRoleDto )
        {
            _popupService.ShowPopup (
                "EditJobFunctionView",
                "Edit",
                "Job Function",
                new[] { new KeyValuePair<string, string> ( "SystemRoleKey", systemRoleDto.Key.ToString () ) },
                false,
                PopupClosed );
        }

        private void GetAllJobFunctions ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<JobFunctionSystemRolesDto> () );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetAllJobFunctionsCompleted, HandleGetAllJobFunctionsException );
        }

        private void HandleDeleteJobFunctionSystemRoleCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DeleteJobFunctionSystemRoleResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.JobFunctionSystemRoles.SystemRoles, validationErrors );
        }

        private void HandleDeletePJobFunctionException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not delete", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAllJobFunctionsCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<JobFunctionSystemRolesDto>> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            ProcessSystemRoleResponses ( response.DataTransferObject.SystemRoles, validationErrors );
        }

        private void HandleGetAllJobFunctionsException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not get all job functions.", UserDialogServiceOptions.Ok );
        }

        private void PopupClosed ()
        {
            GetAllJobFunctions ();
        }

        private void ProcessSystemRoleResponses ( IEnumerable<SystemRoleDto> systemRoleDtos, List<DataErrorInfo> validationErrors )
        {
            IsLoading = false;

            if ( validationErrors.Count () == 0 )
            {
                var jobFunctionList = new List<SystemRoleDto> ( systemRoleDtos );
                PagedCollectionViewWrapper.WrapInPagedCollectionView ( jobFunctionList );
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

        #endregion
    }
}
