using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.MUObjectivesWorkspace
{
    /// <summary>
    /// View for MUObjectives class.
    /// </summary>
    public partial class MUObjectivesView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MUObjectivesView"/> class.
        /// </summary>
        public MUObjectivesView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MUObjectivesView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        [InjectionConstructor]
        public MUObjectivesView ( MUObjectivesViewModel viewModel )
            : this ()
        {
            DataContext = viewModel;
        }

        #endregion
    }
}
