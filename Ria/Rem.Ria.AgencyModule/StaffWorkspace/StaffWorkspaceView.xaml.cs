using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.StaffWorkspace
{
    /// <summary>
    /// View for StaffWorkspace class.
    /// </summary>
    public partial class StaffWorkspaceView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffWorkspaceView"/> class.
        /// </summary>
        public StaffWorkspaceView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffWorkspaceView"/> class.
        /// </summary>
        /// <param name="staffWorkspaceViewModel">The staff workspace view model.</param>
        [InjectionConstructor]
        public StaffWorkspaceView ( StaffWorkspaceViewModel staffWorkspaceViewModel )
            : this ()
        {
            DataContext = staffWorkspaceViewModel;
        }

        #endregion
    }
}
