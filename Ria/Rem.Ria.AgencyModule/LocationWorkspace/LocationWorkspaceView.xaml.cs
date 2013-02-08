using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.LocationWorkspace
{
    /// <summary>
    /// View for LocationWorkspace class.
    /// </summary>
    public partial class LocationWorkspaceView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationWorkspaceView"/> class.
        /// </summary>
        public LocationWorkspaceView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationWorkspaceView"/> class.
        /// </summary>
        /// <param name="locationWorkspaceViewModel">The location workspace view model.</param>
        [InjectionConstructor]
        public LocationWorkspaceView ( LocationWorkspaceViewModel locationWorkspaceViewModel )
            : this ()
        {
            DataContext = locationWorkspaceViewModel;
        }

        #endregion
    }
}
