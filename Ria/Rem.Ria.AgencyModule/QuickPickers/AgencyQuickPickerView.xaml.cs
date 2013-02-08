using System.Windows.Controls;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.QuickPickers
{
    /// <summary>
    /// View for Class for picking agency quick.
    /// </summary>
    public partial class AgencyQuickPickerView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyQuickPickerView"/> class.
        /// </summary>
        public AgencyQuickPickerView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyQuickPickerView"/> class.
        /// </summary>
        /// <param name="agencyQuickPickerViewModel">The agency quick picker view model.</param>
        [InjectionConstructor]
        public AgencyQuickPickerView ( AgencyQuickPickerViewModel agencyQuickPickerViewModel )
            : this ()
        {
            DataContext = agencyQuickPickerViewModel;
            var regionContext = RegionContext.GetObservableContext ( this );
            regionContext.PropertyChanged += ( source, args ) =>
                {
                    agencyQuickPickerViewModel.SearchCommunicater =
                        ( QuickPickerCommunicator )regionContext.Value;
                };
        }

        #endregion
    }
}
