using System.Windows.Controls;
using Microsoft.Practices.Unity;
using Rem.Domain.Core.SecurityModule;
using Rem.Ria.AgencyModule.Web.Common;
using SelectionChangedEventArgs = Telerik.Windows.Controls.SelectionChangedEventArgs;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// Edit Task and Task Group View
    /// </summary>
    public partial class EditTaskAndTaskGroupView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTaskAndTaskGroupView"/> class.
        /// </summary>
        public EditTaskAndTaskGroupView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTaskAndTaskGroupView"/> class.
        /// </summary>
        /// <param name="editTaskAndTaskGroupViewModel">The edit task and task group view model.</param>
        [InjectionConstructor]
        public EditTaskAndTaskGroupView ( EditTaskAndTaskGroupViewModel editTaskAndTaskGroupViewModel )
            : this ()
        {
            DataContext = editTaskAndTaskGroupViewModel;
        }

        #endregion

        #region Methods

        private void PermissionListRadTreeViewPreviewSelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            e.Handled = true;
        }

        private void TaskGroupListRadTreeViewPreviewSelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if ( e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] != null )
            {
                if ( e.AddedItems[0] is SystemRoleDto )
                {
                    if ( ( e.AddedItems[0] as SystemRoleDto ).SystemRoleType == SystemRoleType.Task )
                    {
                        e.Handled = true;
                    }
                }
                else if ( e.AddedItems[0] is SystemPermissionDto )
                {
                    e.Handled = true;
                }
            }
        }

        private void TaskListRadTreeViewPreviewSelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if ( e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] != null )
            {
                if ( e.AddedItems[0] is SystemPermissionDto )
                {
                    e.Handled = true;
                }
            }
        }

        #endregion
    }
}
