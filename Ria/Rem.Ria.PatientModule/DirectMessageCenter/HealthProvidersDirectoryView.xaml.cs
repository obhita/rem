using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// Interaction logic for HealthProvidersDirectoryView.xaml
    /// </summary>
    public partial class HealthProvidersDirectoryView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthProvidersDirectoryView"/> class.
        /// </summary>
        public HealthProvidersDirectoryView()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="HealthProvidersDirectoryView"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        [InjectionConstructor]
        public HealthProvidersDirectoryView(HealthProvidersDirectoryViewModel viewModel)
            : this ()
        {
            DataContext = viewModel;
        }
    }
}
