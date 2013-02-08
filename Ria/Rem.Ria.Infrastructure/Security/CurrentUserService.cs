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
using System.Collections.Generic;
using Agatha.Common;
using NLog;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.Infrastructure.Security
{
    /// <summary>
    /// CurrentUserService class.
    /// </summary>
    public class CurrentUserService : ICurrentUserPermissionService, ICurrentUserContextService, IEmergencyAccessService
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly object _currentUserContextSync = new object();
        private readonly WeakDelegatesManager _currentUserContextWeakDelegatesManager = new WeakDelegatesManager();
        private readonly IUserDialogService _userDialogService;
        private readonly IList<Permission> _userPermissions;
        private readonly object _userPermissionsSync = new object();
        private readonly WeakDelegatesManager _userPermissionsWeakDelegatesManager = new WeakDelegatesManager();
        private bool _permissionsInitialized;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        public CurrentUserService (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userPermissions = new List<Permission> ();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current user context.
        /// </summary>
        /// <value>The current user context.</value>
        private CurrentUserContext CurrentUserContext { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the context.
        /// </summary>
        /// <param name="locationContext">The location context.</param>
        public void ChangeContext ( LocationContext locationContext )
        {
            if ( locationContext.Key != CurrentUserContext.Location.Key )
            {
                ChangeContext (
                    new CurrentUserContext ( CurrentUserContext.Agency, locationContext, CurrentUserContext.Staff, CurrentUserContext.Account ) );
            }
        }

        /// <summary>
        /// Determines whether the current system user has the specified <see cref="Permission"/>.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns><c>true</c> if the user has been granted the specified <see cref="Permission"/>; otherwise, <c>false</c>.</returns>
        public bool DoesUserHavePermission ( Permission permission )
        {
            return _userPermissions.Contains ( permission );
        }

        /// <summary>
        /// Exercises the emergency access.
        /// </summary>
        public void ExerciseEmergencyAccess ()
        {
            Logger.Info ( "ExerciseEmergencyAccess" );
            _permissionsInitialized = false;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new ExerciseEmergencyAccessRequest () );
            requestDispatcher.ProcessRequests ( OnExerciseEmergencyAccessCompleted, HandleRequestDispatcherException );
        }

        /// <summary>
        /// Initializes the user session.
        /// </summary>
        public void InitializeUserSession ()
        {
            Logger.Info ( "InitializeUserSession" );
            _permissionsInitialized = false;
            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            dispatcher.Add ( new GetUserInformationRequest () );
            dispatcher.ProcessRequests ( OnInitializeUserSessionCompleted, HandleRequestDispatcherException );
        }

        /// <summary>
        /// Registers for context.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="keepReferenceAlive">If set to <c>true</c> [keep reference alive].</param>
        public void RegisterForContext ( Action<CurrentUserContext, bool> callback, bool keepReferenceAlive = false )
        {
            lock ( _currentUserContextSync )
            {
                _currentUserContextWeakDelegatesManager.AddListener ( callback, keepReferenceAlive );
                callback ( CurrentUserContext, true );
            }
        }

        /// <summary>
        /// Registers a callback that is called whenever the current users' permissions change.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="keepReferenceAlive">If set to <c>true</c> keeps callback references alive.</param>
        public void RegisterForPermissions ( Action callback, bool keepReferenceAlive = false )
        {
            lock ( _userPermissionsSync )
            {
                _userPermissionsWeakDelegatesManager.AddListener ( callback, keepReferenceAlive );
                if ( _permissionsInitialized )
                {
                    callback ();
                }
            }
        }

        /// <summary>
        /// Rollbacks the emergency access.
        /// </summary>
        public void RollbackEmergencyAccess ()
        {
            Logger.Info ( "RollbackEmergencyAccess" );
            _permissionsInitialized = false;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new RollbackEmergencyAccessRequest () );
            requestDispatcher.ProcessRequests ( OnRollbackEmergencyAccessCompleted, HandleRequestDispatcherException );
        }

        /// <summary>
        /// Unregisters for context.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void UnregisterForContext ( Action<CurrentUserContext, bool> callback )
        {
            lock ( _currentUserContextSync )
            {
                _currentUserContextWeakDelegatesManager.RemoveListener ( callback );
            }
        }

        /// <summary>
        /// Unregisters the callback.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void UnregisterForPermissions ( Action callback )
        {
            lock ( _userPermissionsSync )
            {
                _userPermissionsWeakDelegatesManager.RemoveListener ( callback );
            }
        }

        #endregion

        #region Methods

        private void ChangeContext(CurrentUserContext currentUserContext)
        {
            lock ( _currentUserContextSync )
            {
                CurrentUserContext = currentUserContext;

                //change context to have new location
                _currentUserContextWeakDelegatesManager.Raise ( CurrentUserContext, false );
            }
        }

        private void HandleRequestDispatcherException(ExceptionInfo exceptionInfo)
        {
            Logger.Info ( "Request Dispatcher Exception {0}", exceptionInfo );
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Security Access Failed", UserDialogServiceOptions.Ok );
        }

        private void InitializeUserInformation(UserInformationDto userInformationDto)
        {
            lock ( _userPermissionsSync )
            {
                lock ( _currentUserContextSync )
                {
                    _userPermissions.Clear ();

                    foreach ( var grantedPermission in userInformationDto.GrantedPermissions )
                    {
                        var permission = new Permission { Name = grantedPermission };
                        _userPermissions.Add ( permission );
                    }

                    var agency = new AgencyContext (
                        userInformationDto.AgencyKey,
                        userInformationDto.AgencyDisplayName );
                    var location = new LocationContext (
                        userInformationDto.LocationKey,
                        userInformationDto.LocationDisplayName );
                    var staff = new StaffContext (
                        userInformationDto.StaffKey,
                        userInformationDto.StaffFirstName,
                        userInformationDto.StaffMiddleName,
                        userInformationDto.StaffLastName,
                        userInformationDto.DirectEmailAddress);
                    var account = new AccountContext (
                        userInformationDto.AccountKey,
                        userInformationDto.AccountIdentifier );
                    var currentUserContext = new CurrentUserContext (
                        agency,
                        location,
                        staff,
                        account );

                    ChangeContext ( currentUserContext );

                    _permissionsInitialized = true;

                    _userPermissionsWeakDelegatesManager.Raise ();
                }
            }
        }

        private void OnExerciseEmergencyAccessCompleted(ReceivedResponses receivedResponses)
        {
            var exerciseEmergencyAccessResponse = receivedResponses.Get<ExerciseEmergencyAccessResponse> ();
            InitializeUserInformation ( exerciseEmergencyAccessResponse.UserInformationDto );
            Logger.Debug ( "OnExerciseEmergencyAccessCompleted" );
        }

        private void OnInitializeUserSessionCompleted(ReceivedResponses receivedResponses)
        {
            var userInformationResponse = receivedResponses.Get<GetUserInformationResponse> ();
            InitializeUserInformation ( userInformationResponse.UserInformationDto );
            Logger.Debug ( "OnInitializeUserSessionCompleted" );
        }

        private void OnRollbackEmergencyAccessCompleted(ReceivedResponses receivedResponses)
        {
            var rollbackEmergencyAccessResponse = receivedResponses.Get<RollbackEmergencyAccessResponse> ();
            InitializeUserInformation ( rollbackEmergencyAccessResponse.UserInformationDto );
            Logger.Debug ( "OnRollbackEmergencyAccessCompleted" );
        }

        #endregion
    }
}
