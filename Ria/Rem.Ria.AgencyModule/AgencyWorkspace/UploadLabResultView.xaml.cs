using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.AgencyWorkspace
{
    /// <summary>
    /// View for UploadLabResult class.
    /// </summary>
    public partial class UploadLabResultView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadLabResultView"/> class.
        /// </summary>
        public UploadLabResultView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadLabResultView"/> class.
        /// </summary>
        /// <param name="uploadLabResultViewModel">The upload lab result view model.</param>
        [InjectionConstructor]
        public UploadLabResultView ( UploadLabResultViewModel uploadLabResultViewModel )
            : this ()
        {
            DataContext = uploadLabResultViewModel;
        }

        #endregion
    }
}
