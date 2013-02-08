using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.LocationDashboard
{
    /// <summary>
    /// View for ProgramOfferingList class.
    /// </summary>
    public partial class ProgramOfferingListView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingListView"/> class.
        /// </summary>
        public ProgramOfferingListView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingListView"/> class.
        /// </summary>
        /// <param name="programOfferingListViewModel">The program offering list view model.</param>
        [InjectionConstructor]
        public ProgramOfferingListView ( ProgramOfferingListViewModel programOfferingListViewModel )
            : this ()
        {
            DataContext = programOfferingListViewModel;
        }

        #endregion
    }
}
