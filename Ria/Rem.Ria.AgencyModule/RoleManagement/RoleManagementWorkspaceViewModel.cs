using System.Collections.Generic;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View Model for RoleManagementWorkspace class.
    /// </summary>
    public class RoleManagementWorkspaceViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private readonly INavigationService _navigationService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleManagementWorkspaceViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public RoleManagementWorkspaceViewModel (
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _navigationService = navigationService;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get { return "Role Management"; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Administrative; }
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
            _navigationService.Navigate ( RegionManager, "RoleListRegion", "JobFunctionListView", null, parameters );
            _navigationService.Navigate ( RegionManager, "RoleListRegion", "TaskAndTakGroupListView", null, parameters );
        }

        #endregion
    }
}
