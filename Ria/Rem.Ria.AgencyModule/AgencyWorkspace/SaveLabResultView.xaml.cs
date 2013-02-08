using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.AgencyWorkspace
{
    /// <summary>
    /// View for SaveLabResult class.
    /// </summary>
    public partial class SaveLabResultView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLabResultView"/> class.
        /// </summary>
        public SaveLabResultView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLabResultView"/> class.
        /// </summary>
        /// <param name="saveLabResultViewModel">The save lab result view model.</param>
        [InjectionConstructor]
        public SaveLabResultView ( SaveLabResultViewModel saveLabResultViewModel )
            : this ()
        {
            DataContext = saveLabResultViewModel;
        }

        #endregion
    }
}
