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

using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Security;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Permissions;

namespace Rem.Ria.Infrastructure.EmergencyAccess
{
    /// <summary>
    /// View Model for EmergencyAccess class.
    /// </summary>
    public class EmergencyAccessViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly ICurrentUserPermissionService _currentUserPermissionService;
        private readonly IEmergencyAccessService _emergencyAccessService;
        private readonly IUserDialogService _userDialogService;
        private bool _canExecuteExerciseEmergencyAccess;
        private bool _emergencyAccessEnabled;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmergencyAccessViewModel"/> class.
        /// </summary>
        /// <param name="currentUserPermissionService">The current user permission service.</param>
        /// <param name="emergencyAccessService">The emergency access service.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public EmergencyAccessViewModel (
            ICurrentUserPermissionService currentUserPermissionService,
            IEmergencyAccessService emergencyAccessService,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _currentUserPermissionService = currentUserPermissionService;
            _emergencyAccessService = emergencyAccessService;
            _userDialogService = userDialogService;
            _currentUserPermissionService.RegisterForPermissions ( OnUserPermissionsChanged );

            var commandfactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ExerciseEmergencyAccessCommand = commandfactoryHelper.BuildDelegateCommand (
                () => ExerciseEmergencyAccessCommand, ExecuteExerciseEmergencyAccess, CanExecuteExerciseEmergencyAccess );

            EvaluateEmergencyAccessClaims ();
        }

        #endregion

        // TODO: Evaluate implementation of a variation of SecureCommand that encapsulates collapse/show button instead of default disabled button.

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether [emergency access enabled].
        /// </summary>
        public bool EmergencyAccessEnabled
        {
            get { return _emergencyAccessEnabled; }
            private set { ApplyPropertyChange ( ref _emergencyAccessEnabled, () => EmergencyAccessEnabled, value ); }
        }

        /// <summary>
        /// Gets the exercise emergency access command.
        /// </summary>
        public ICommand ExerciseEmergencyAccessCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [user permissions changed].
        /// </summary>
        public void OnUserPermissionsChanged ()
        {
            // Evaluate user's permission claim to exercise Emergency Access. 
            EvaluateEmergencyAccessClaims ();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
        protected override ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory )
        {
            return CommandFactoryHelper.CreateHelper ( this, commandFactory );
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
        }

        private bool CanExecuteExerciseEmergencyAccess ()
        {
            return _canExecuteExerciseEmergencyAccess;
        }

        private void EvaluateEmergencyAccessClaims ()
        {
            _canExecuteExerciseEmergencyAccess = AccessControlManager.CanAccess ( new ResourceRequest { GetType ().FullName } );
            ( ExerciseEmergencyAccessCommand as VirtualDelegateCommand ).RaiseCanExecuteChanged ();

            EmergencyAccessEnabled = _currentUserPermissionService.DoesUserHavePermission ( InfrastructurePermission.EmergencyAccessPermission );
        }

        private void ExecuteExerciseEmergencyAccess ()
        {
            if ( EmergencyAccessEnabled )
            {
                _emergencyAccessService.RollbackEmergencyAccess ();
            }
            else
            {
                var result = _userDialogService.ShowDialog (
                    "Are you sure that you want to exercise an Emergency Access request.",
                    "Emergency Access Request",
                    UserDialogServiceOptions.OkCancel );

                if ( result == UserDialogServiceResult.Ok )
                {
                    _emergencyAccessService.ExerciseEmergencyAccess ();
                }
            }
        }

        #endregion
    }
}
