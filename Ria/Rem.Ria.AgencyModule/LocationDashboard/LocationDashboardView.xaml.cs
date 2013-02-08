using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.LocationDashboard
{
    /// <summary>
    /// View for LocationDashboard class.
    /// </summary>
    public partial class LocationDashboardView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDashboardView"/> class.
        /// </summary>
        public LocationDashboardView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDashboardView"/> class.
        /// </summary>
        /// <param name="locationDashboardViewModel">The location dashboard view model.</param>
        [InjectionConstructor]
        public LocationDashboardView ( LocationDashboardViewModel locationDashboardViewModel )
            : this ()
        {
            DataContext = locationDashboardViewModel;
        }

        #endregion
    }
}
