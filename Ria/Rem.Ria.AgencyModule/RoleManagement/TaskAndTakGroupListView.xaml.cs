using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View for TaskAndTakGroupList class.
    /// </summary>
    /// <content>
    /// TaskAndTakGroupListView code behind.
    /// </content>
    public partial class TaskAndTakGroupListView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAndTakGroupListView"/> class.
        /// </summary>
        public TaskAndTakGroupListView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAndTakGroupListView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        [InjectionConstructor]
        public TaskAndTakGroupListView ( TaskAndTakGroupListViewModel viewModel )
            : this ()
        {
            DataContext = viewModel;
        }

        #endregion
    }
}
