using System.Windows.Controls;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.StaffSearch
{
    /// <summary>
    /// View for StaffSearch class.
    /// </summary>
    public partial class StaffSearchView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffSearchView"/> class.
        /// </summary>
        public StaffSearchView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffSearchView"/> class.
        /// </summary>
        /// <param name="staffSearchViewModel">The staff search view model.</param>
        [InjectionConstructor]
        public StaffSearchView ( StaffSearchViewModel staffSearchViewModel )
            : this ()
        {
            DataContext = staffSearchViewModel;
            var regionContext = RegionContext.GetObservableContext ( this );
            regionContext.PropertyChanged += ( source, args ) =>
                {
                    staffSearchViewModel.SearchCommunicater =
                        ( SearchCommunicater )regionContext.Value;
                };
        }

        #endregion
    }
}
