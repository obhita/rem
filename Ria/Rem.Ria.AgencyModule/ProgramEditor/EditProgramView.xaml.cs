using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.ProgramEditor
{
    /// <summary>
    /// View for EditProgram class.
    /// </summary>
    public partial class EditProgramView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProgramView"/> class.
        /// </summary>
        public EditProgramView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProgramView"/> class.
        /// </summary>
        /// <param name="editProgramViewModel">The edit program view model.</param>
        [InjectionConstructor]
        public EditProgramView ( EditProgramViewModel editProgramViewModel )
            : this ()
        {
            DataContext = editProgramViewModel;
        }

        #endregion
    }
}
