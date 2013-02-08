#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System.Windows;
using System.Windows.Controls;
using NLog;
using Pillar.Common.InversionOfControl;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.View.Configuration;

namespace Rem.Ria.Shell
{
    /// <summary>
    /// Class for loading shell.
    /// </summary>
    public class ShellLoader
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        private readonly ICurrentUserPermissionService _currentUserPermissionService;
        private readonly IContainer _container;
        private bool _securityInitialized;
        private SplashScreen _splashScreen;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLoader"/> class.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        /// <param name="currentUserPermissionService">The current user permission service.</param>
        public ShellLoader (
            IContainer container,
            ICurrentUserPermissionService currentUserPermissionService )
        {
            _container = container;
            _currentUserPermissionService = currentUserPermissionService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The core grid the splash and main content are swapped in and out of
        /// </summary>
        /// <value>The layout root.</value>
        private Grid LayoutRoot { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initiates the loading.
        /// </summary>
        /// <returns>A <see cref="System.Windows.UIElement"/></returns>
        public UIElement InitiateLoading ()
        {
            Logger.Debug ( "Instantiating the Splash Screen." );
            _splashScreen = _container.Resolve<SplashScreen> ();

            LayoutRoot = new Grid
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

            var shell = _container.Resolve<Shell>();
            new ConfigurationBehaviorService ().Watch ( shell );
            LayoutRoot.Children.Add ( shell );
            LayoutRoot.Children.Add ( _splashScreen );

            Logger.Debug ( "Setting the Splash Screen as the Root Visual" );
            Application.Current.RootVisual = LayoutRoot;

            Logger.Debug ( "Initializing the client security manager." );
            _currentUserPermissionService.RegisterForPermissions ( OnUserPermissionsChanged, true );
            _currentUserPermissionService.InitializeUserSession ();

            return LayoutRoot;
        }

        /// <summary>
        /// Is called whenever the user permission service reports that permissions have changed.
        /// </summary>
        public void OnUserPermissionsChanged ()
        {
            _currentUserPermissionService.UnregisterForPermissions ( OnUserPermissionsChanged );
            if ( !_securityInitialized )
            {
                Logger.Debug ( "Removing splash screen." );
                LayoutRoot.Children.Remove ( _splashScreen );
                _securityInitialized = true;
            }
        }

        #endregion
    }
}
