using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.AgencyModule.StaffEditor
{
    /// <summary>
    /// View for UploadPhoto class.
    /// </summary>
    public partial class UploadPhotoView : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadPhotoView"/> class.
        /// </summary>
        public UploadPhotoView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadPhotoView"/> class.
        /// </summary>
        /// <param name="uploadPhotoViewModel">The upload photo view model.</param>
        [InjectionConstructor]
        public UploadPhotoView ( UploadPhotoViewModel uploadPhotoViewModel )
            : this ()
        {
            DataContext = uploadPhotoViewModel;
        }

        #endregion
    }
}
