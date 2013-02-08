using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// View for editing Job Function.
    /// </summary>
    public partial class EditJobFunctionView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditJobFunctionView"/> class.
        /// </summary>
        public EditJobFunctionView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditJobFunctionView"/> class.
        /// </summary>
        /// <param name="editJobFunctionViewModel">The edit job function view model.</param>
        [InjectionConstructor]
        public EditJobFunctionView ( EditJobFunctionViewModel editJobFunctionViewModel )
            : this ()
        {
            DataContext = editJobFunctionViewModel;
        }

        #endregion
    }
}
