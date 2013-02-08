using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.StaffEditor
{
    /// <summary>
    /// View for Class for editing staff.
    /// </summary>
    public partial class StaffEditorView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffEditorView"/> class.
        /// </summary>
        public StaffEditorView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffEditorView"/> class.
        /// </summary>
        /// <param name="staffEditorViewModel">The staff editor view model.</param>
        [InjectionConstructor]
        public StaffEditorView ( StaffEditorViewModel staffEditorViewModel )
            : this ()
        {
            DataContext = staffEditorViewModel;
        }

        #endregion
    }
}
