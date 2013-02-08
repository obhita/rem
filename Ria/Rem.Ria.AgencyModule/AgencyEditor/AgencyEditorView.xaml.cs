using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.AgencyEditor
{
    /// <summary>
    /// View for Class for editing agency.
    /// </summary>
    public partial class AgencyEditorView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyEditorView"/> class.
        /// </summary>
        public AgencyEditorView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyEditorView"/> class.
        /// </summary>
        /// <param name="agencyEditorViewModel">The agency editor view model.</param>
        [InjectionConstructor]
        public AgencyEditorView ( AgencyEditorViewModel agencyEditorViewModel )
            : this ()
        {
            DataContext = agencyEditorViewModel;
        }

        #endregion
    }
}
