using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule
{
    /// <summary>
    /// View for MainAgencyQuickPickers class.
    /// </summary>
    public partial class MainAgencyQuickPickersView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainAgencyQuickPickersView"/> class.
        /// </summary>
        public MainAgencyQuickPickersView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainAgencyQuickPickersView"/> class.
        /// </summary>
        /// <param name="mainAgencyQuickPickersViewModel">The main agency quick pickers view model.</param>
        [InjectionConstructor]
        public MainAgencyQuickPickersView ( MainAgencyQuickPickersViewModel mainAgencyQuickPickersViewModel )
            : this ()
        {
            DataContext = mainAgencyQuickPickersViewModel;
        }

        #endregion
    }
}
