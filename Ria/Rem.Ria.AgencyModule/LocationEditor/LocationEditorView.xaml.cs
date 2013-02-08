using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.LocationEditor
{
    /// <summary>
    /// View for Class for editing location.
    /// </summary>
    public partial class LocationEditorView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationEditorView"/> class.
        /// </summary>
        public LocationEditorView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationEditorView"/> class.
        /// </summary>
        /// <param name="locationEditorViewModel">The location editor view model.</param>
        [InjectionConstructor]
        public LocationEditorView ( LocationEditorViewModel locationEditorViewModel )
            : this ()
        {
            DataContext = locationEditorViewModel;
        }

        #endregion
    }
}
