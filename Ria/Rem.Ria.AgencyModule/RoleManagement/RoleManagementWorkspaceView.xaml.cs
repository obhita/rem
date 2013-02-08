using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View for RoleManagementWorkspace class.
    /// </summary>
    /// <content>
    /// RoleManagementWorkspaceView code behind.
    /// </content>
    public partial class RoleManagementWorkspaceView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleManagementWorkspaceView"/> class.
        /// </summary>
        public RoleManagementWorkspaceView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleManagementWorkspaceView"/> class.
        /// </summary>
        /// <param name="roleManagementWorkspaceViewModel">The role management workspace view model.</param>
        [InjectionConstructor]
        public RoleManagementWorkspaceView ( RoleManagementWorkspaceViewModel roleManagementWorkspaceViewModel )
            : this ()
        {
            DataContext = roleManagementWorkspaceViewModel;
        }

        #endregion
    }
}
