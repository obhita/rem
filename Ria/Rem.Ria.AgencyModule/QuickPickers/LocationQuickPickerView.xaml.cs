using System.Windows.Controls;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.QuickPickers
{
    /// <summary>
    /// View for Class for picking location quick.
    /// </summary>
    public partial class LocationQuickPickerView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationQuickPickerView"/> class.
        /// </summary>
        public LocationQuickPickerView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationQuickPickerView"/> class.
        /// </summary>
        /// <param name="locationQuickPickerViewModel">The location quick picker view model.</param>
        [InjectionConstructor]
        public LocationQuickPickerView ( LocationQuickPickerViewModel locationQuickPickerViewModel )
            : this ()
        {
            DataContext = locationQuickPickerViewModel;
            var regionContext = RegionContext.GetObservableContext ( this );
            regionContext.PropertyChanged += ( source, args ) =>
                {
                    locationQuickPickerViewModel.SearchCommunicater =
                        ( QuickPickerCommunicator )regionContext.Value;
                };
        }

        #endregion
    }
}
