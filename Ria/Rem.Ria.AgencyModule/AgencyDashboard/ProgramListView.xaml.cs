using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.AgencyDashboard
{
    /// <summary>
    /// View for ProgramList class.
    /// </summary>
    public partial class ProgramListView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramListView"/> class.
        /// </summary>
        public ProgramListView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramListView"/> class.
        /// </summary>
        /// <param name="programListViewModel">The program list view model.</param>
        [InjectionConstructor]
        public ProgramListView ( ProgramListViewModel programListViewModel )
            : this ()
        {
            DataContext = programListViewModel;
        }

        #endregion
    }
}
