using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.AgencyWorkspace
{
    /// <summary>
    /// View for AgencyWorkspace class.
    /// </summary>
    public partial class AgencyWorkspaceView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyWorkspaceView"/> class.
        /// </summary>
        public AgencyWorkspaceView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyWorkspaceView"/> class.
        /// </summary>
        /// <param name="agencyWorkspaceViewModel">The agency workspace view model.</param>
        [InjectionConstructor]
        public AgencyWorkspaceView ( AgencyWorkspaceViewModel agencyWorkspaceViewModel )
            : this ()
        {
            DataContext = agencyWorkspaceViewModel;
        }

        #endregion
    }
}
