using System.Windows.Controls;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.QuickPickers
{
    /// <summary>
    /// View for Class for picking staff quick.
    /// </summary>
    public partial class StaffQuickPickerView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffQuickPickerView"/> class.
        /// </summary>
        public StaffQuickPickerView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffQuickPickerView"/> class.
        /// </summary>
        /// <param name="staffQuickPickerViewModel">The staff quick picker view model.</param>
        public StaffQuickPickerView ( StaffQuickPickerViewModel staffQuickPickerViewModel )
            : this ()
        {
            DataContext = staffQuickPickerViewModel;
            var regionContext = RegionContext.GetObservableContext ( this );
            regionContext.PropertyChanged += ( source, args ) =>
                {
                    staffQuickPickerViewModel.SearchCommunicater =
                        ( QuickPickerCommunicator )regionContext.Value;
                };
        }

        #endregion
    }
}
