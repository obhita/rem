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
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Infrastructure.Mvc.UserContext
{
    /// <summary>
    /// CurrentUserService class.
    /// </summary>
    public class CurrentUserService : ICurrentUserContextService
    {
        #region Constants and Fields

        private readonly object _currentUserContextSync = new object ();
        private readonly WeakDelegatesManager _currentUserContextWeakDelegatesManager = new WeakDelegatesManager ();
        private readonly IRequestDispatcherFactory _requestDispatcherFactory;
        private readonly IList<Permission> _userPermissions;
        private readonly object _userPermissionsSync = new object ();
        private bool _initialized;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        /// <param name="requestDispatcherFactory">The request dispatcher factory.</param>
        public CurrentUserService (
            IRequestDispatcherFactory requestDispatcherFactory )
        {
            _requestDispatcherFactory = requestDispatcherFactory;
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
            throw new NotImplementedException ();
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
        /// Initializes the user session.
        /// </summary>
        public void InitializeUserSession ()
        {
            var dispatcher = _requestDispatcherFactory.CreateRequestDispatcher ();
            dispatcher.Add ( new GetUserInformationRequest () );
            var userInfo = dispatcher.Get<GetUserInformationResponse> ();
            InitializeUserInformation ( userInfo.UserInformationDto );
            _initialized = true;
            ChangeContext ( CurrentUserContext );
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
                if ( _initialized )
                {
                    _currentUserContextWeakDelegatesManager.AddListener ( callback, keepReferenceAlive );
                    callback ( CurrentUserContext, true );
                }
            }
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

        #endregion

        #region Methods

        private void ChangeContext ( CurrentUserContext currentUserContext, bool isInit = false )
        {
            lock ( _currentUserContextSync )
            {
                CurrentUserContext = currentUserContext;

                //change context to have new location
                _currentUserContextWeakDelegatesManager.Raise ( CurrentUserContext, isInit );
            }
        }

        private void InitializeUserInformation ( UserInformationDto userInformationDto )
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
                        userInformationDto.DirectEmailAddress );
                    var account = new AccountContext (
                        userInformationDto.AccountKey,
                        userInformationDto.AccountIdentifier );
                    var currentUserContext = new CurrentUserContext (
                        agency,
                        location,
                        staff,
                        account );

                    CurrentUserContext = currentUserContext;
                }
            }
        }

        #endregion
    }
}
