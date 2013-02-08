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

using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Agatha.Common;
using Microsoft.Practices.Unity;
using NLog;
using Rem.Ria.Infrastructure.LogOutWarning;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.Shell
{
    /// <summary>
    /// Shell class.
    /// </summary>
    public partial class Shell : UserControl
    {
        #region Constants and Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger ();
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly DispatcherTimer _logoutTimer;
        private readonly IRedirectService _redirectService;
        private readonly IUserDialogService _userDialogService;
        private readonly DispatcherTimer _warningLogoutTimer;
        private ChildWindow LogOutWarning;
        private GetTimeOutIntervalResponse response;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell ()
        {
            InitializeComponent ();

            _logoutTimer = new DispatcherTimer ();
            _warningLogoutTimer = new DispatcherTimer ();

            // Register events that extend the logout expiration time.
            AddHandler ( KeyDownEvent, new KeyEventHandler ( UserControlKeyDown ), true );
            MouseMove += UserControlMouseMove;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        /// <param name="shellViewModel">The shell view model.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="redirectService">The redirect service.</param>
        [InjectionConstructor]
        public Shell (
            ShellViewModel shellViewModel,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IRedirectService redirectService )
            : this ()
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _redirectService = redirectService;
            DataContext = shellViewModel;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetTimeOutIntervalRequest () );
            requestDispatcher.ProcessRequests ( HandleGetTimeOutIntervalCompleted, HandleGetTimeOutIntervalException );
        }

        #endregion

        #region Methods

        private void HandleGetTimeOutIntervalCompleted ( ReceivedResponses receivedResponses )
        {
            response = receivedResponses.Get<GetTimeOutIntervalResponse> ();

            _logoutTimer.Interval = TimeSpan.FromMinutes ( response.AutomaticTimeOutIntervalMinutes );
            _logoutTimer.Tick += LogoutTimerTick;
            _logoutTimer.Start ();
        }

        private void HandleGetTimeOutIntervalException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog (
                "Logout timeout interval configuration service request could not be completed. Error: " + ex.Message,
                "An error has occurred",
                UserDialogServiceOptions.Ok );
        }

        private void HandleSignOffCompleted ( ReceivedResponses receivedResponses )
        {
            logger.Info ( "HandleSignOffCompleted: RedirectService.RedirectTo - Redirecting to Logout.aspx. DateTime Utc: " + DateTime.UtcNow );

            _redirectService.RedirectTo ( new Uri ( "Logout.aspx", UriKind.Relative ) );
        }

        private void HandleSignOffException ( ExceptionInfo ex )
        {
            logger.Error ( "HandleSignOffException: Exception occurred. DateTime Utc: " + DateTime.UtcNow );

            _userDialogService.ShowDialog (
                "The sign out of the relying party application process could not be completed. Error: " + ex.Message,
                "An error has occurred",
                UserDialogServiceOptions.Ok );
        }

        private void LogOutWarningClosed ( object sender, EventArgs e )
        {
            var logoutWarningDialog = ( LogOutWarningView )sender;
            var logOutOfApplication = ( logoutWarningDialog.DialogResult != null ) && Convert.ToBoolean ( LogOutWarning.DialogResult );

            if ( logOutOfApplication )
            {
                logger.Info ( "LogOutWarningClosed: Automated logout will be processed." );

                _warningLogoutTimer.Stop ();
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( new SignOffRequest () );
                requestDispatcher.ProcessRequests ( HandleSignOffCompleted, HandleSignOffException );
            }
            else
            {
                logger.Info ( "LogOutWarningClosed: User elected to stay logged into the application." );

                // User elected to stay logged in. Reset the main and warning timers.
                _logoutTimer.Start ();
                _warningLogoutTimer.Stop ();
            }
        }

        private void LogoutTimerTick ( object sender, EventArgs e )
        {
            ( ( DispatcherTimer )sender ).Stop ();

            // The main logout time has expired. Show the log out warning prompt.
            LogOutWarning = new LogOutWarningView ( response.WarningTimeOutIntervalSeconds );
            LogOutWarning.Closed += LogOutWarningClosed;
            LogOutWarning.Show ();

            // Start the warning timer.
            _warningLogoutTimer.Interval = TimeSpan.FromSeconds ( response.WarningTimeOutIntervalSeconds );
            _warningLogoutTimer.Tick += WarningLogoutTimerTick;
            _warningLogoutTimer.Start ();
        }

        private void UserControlKeyDown ( object sender, KeyEventArgs e )
        {
            _logoutTimer.Start ();
        }

        private void UserControlMouseMove ( object sender, MouseEventArgs e )
        {
            _logoutTimer.Start ();
        }

        private void WarningLogoutTimerTick ( object sender, EventArgs e )
        {
            ( ( DispatcherTimer )sender ).Stop ();

            logger.Info ( "WarningLogoutTimerTick: The automatic warning time has elapsed." );

            LogOutWarning.Closed -= LogOutWarningClosed;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SignOffRequest () );
            requestDispatcher.ProcessRequests ( HandleSignOffCompleted, HandleSignOffException );
        }

        #endregion
    }
}
