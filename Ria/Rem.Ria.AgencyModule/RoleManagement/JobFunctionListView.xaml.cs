using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View for listing all job functions.
    /// </summary>
    public partial class JobFunctionListView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JobFunctionListView"/> class.
        /// </summary>
        public JobFunctionListView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobFunctionListView"/> class.
        /// </summary>
        /// <param name="jobFunctionListViewModel">The job function list view model.</param>
        [InjectionConstructor]
        public JobFunctionListView ( JobFunctionListViewModel jobFunctionListViewModel )
            : this ()
        {
            DataContext = jobFunctionListViewModel;
        }

        #endregion
    }
}
