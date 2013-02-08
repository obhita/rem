using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.ProgramOfferingEditor
{
    /// <summary>
    /// View for Class for editing program offering.
    /// </summary>
    public partial class ProgramOfferingEditorView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingEditorView"/> class.
        /// </summary>
        public ProgramOfferingEditorView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingEditorView"/> class.
        /// </summary>
        /// <param name="programOfferingEditorViewModel">The program offering editor view model.</param>
        [InjectionConstructor]
        public ProgramOfferingEditorView ( ProgramOfferingEditorViewModel programOfferingEditorViewModel )
            : this ()
        {
            DataContext = programOfferingEditorViewModel;
        }

        #endregion
    }
}
