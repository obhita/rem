using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.AgencyDashboard
{
    /// <summary>
    /// View for AgencyDashboard class.
    /// </summary>
    public partial class AgencyDashboardView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyDashboardView"/> class.
        /// </summary>
        public AgencyDashboardView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyDashboardView"/> class.
        /// </summary>
        /// <param name="agencyDashboardViewModel">The agency dashboard view model.</param>
        [InjectionConstructor]
        public AgencyDashboardView ( AgencyDashboardViewModel agencyDashboardViewModel )
            : this ()
        {
            DataContext = agencyDashboardViewModel;
        }

        #endregion
    }
}
