using Microsoft.Practices.Unity;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.AgencyModule.Login
{
    /// <summary>
    /// View for LoginInformation class.
    /// </summary>
    public partial class LoginInformationView : CollapsingContentControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInformationView"/> class.
        /// </summary>
        public LoginInformationView ()
        {
            InitializeComponent ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginInformationView"/> class.
        /// </summary>
        /// <param name="loginInformationViewModel">The login information view model.</param>
        [InjectionConstructor]
        public LoginInformationView ( LoginInformationViewModel loginInformationViewModel )
            : this ()
        {
            DataContext = loginInformationViewModel;
        }

        #endregion
    }
}
