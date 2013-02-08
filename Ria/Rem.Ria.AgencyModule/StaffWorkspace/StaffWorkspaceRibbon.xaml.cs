using System.Windows.Controls;

namespace Rem.Ria.AgencyModule.StaffWorkspace
{
    /// <summary>
    /// StaffWorkspaceRibbon class.
    /// </summary>
    public partial class StaffWorkspaceRibbon : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffWorkspaceRibbon"/> class.
        /// </summary>
        public StaffWorkspaceRibbon ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffWorkspaceRibbon"/> class.
        /// </summary>
        /// <param name="staffWorkspaceViewModel">The staff workspace view model.</param>
        public StaffWorkspaceRibbon ( StaffWorkspaceViewModel staffWorkspaceViewModel )
            : this ()
        {
            DataContext = staffWorkspaceViewModel;
        }

        #endregion
    }
}
