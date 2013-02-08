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
using System.Linq;
using System.Security.Principal;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Web;
using NLog;
using Pillar.Common.Utility;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.SecurityModule;
using SystemRole = Rem.WellKnownNames.SecurityModule.SystemRole;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// This class provides ability to manage permission claims.
    /// </summary>
    public class PermissionClaimsManager : IPermissionClaimsManager
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();
        
        private readonly ISystemPermissionService _systemPermissionService;
        private readonly ISystemRoleRepository _systemRoleRepository;

        private readonly WSFederationAuthenticationModule _federationAuthenticationModule;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionClaimsManager"/> class.
        /// </summary>
        /// <param name="federatedAuthenticationProvider">The federated authentication provider.</param>
        /// <param name="systemRoleRepository">The system role repository.</param>
        /// <param name="systemPermissionService">The system permission service.</param>
        public PermissionClaimsManager (
            IFederatedAuthenticationProvider federatedAuthenticationProvider,
            ISystemRoleRepository systemRoleRepository,
            ISystemPermissionService systemPermissionService )
        {
            _systemRoleRepository = systemRoleRepository;
            _systemPermissionService = systemPermissionService;
            _federationAuthenticationModule = federatedAuthenticationProvider.GetFederationAuthenticationModule ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Exercises the emergency access.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="systemAccount">The system account.</param>
        public void ExerciseEmergencyAccess ( IClaimsPrincipal claimsPrincipal, SystemAccount systemAccount )
        {
            Check.IsNotNull ( claimsPrincipal, "ClaimsPrincipal is required." );
            Check.IsNotNull ( systemAccount, "SystemAccount is required." );

            var emergencyPermissions = FindEmergencyAccessPermissions ();

            IssueSystemPermissionClaims ( claimsPrincipal, emergencyPermissions, systemAccount );
        }

        /// <summary>
        /// Issues the account key claims.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="systemAccount">The system account.</param>
        public void IssueAccountKeyClaims ( IClaimsPrincipal claimsPrincipal, SystemAccount systemAccount )
        {
            Check.IsNotNull ( claimsPrincipal, "ClaimsPrincipal is required." );
            Check.IsNotNull ( systemAccount, "SystemAccount is required." );

            var realm = _federationAuthenticationModule.Realm;

            var claim = new Claim (
                ClaimTypes.AccountKeyClaimType, systemAccount.Key + string.Empty, realm );

            var identity = ( IClaimsIdentity )claimsPrincipal.Identity;
            identity.Claims.Add ( claim );

            Logger.Debug ( "Principal ({0}) is issued the following claim ({1}).", systemAccount.Identifier, claim.ToString () );
        }

        /// <summary>
        /// Issues the staff key claims.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="staff">The staff.</param>
        public void IssueStaffKeyClaims ( IClaimsPrincipal claimsPrincipal, Staff staff )
        {
            Check.IsNotNull ( claimsPrincipal, "ClaimsPrincipal is required." );
            Check.IsNotNull ( staff, "Staff is required." );

            var realm = _federationAuthenticationModule.Realm;

            var claim = new Claim ( ClaimTypes.StaffKeyClaimType, staff.Key + string.Empty, realm );

            var identity = ( IClaimsIdentity )claimsPrincipal.Identity;
            identity.Claims.Add ( claim );

            Logger.Debug ( "Principal ({0}) is issued the following claim ({1}).", staff.StaffProfile.StaffName.Complete, claim.ToString () );
        }

        /// <summary>
        /// Issues the system permission claims.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="systemAccount">The system account.</param>
        public void IssueSystemPermissionClaims ( IClaimsPrincipal claimsPrincipal, SystemAccount systemAccount )
        {
            Check.IsNotNull ( claimsPrincipal, "ClaimsPrincipal is required." );
            Check.IsNotNull ( systemAccount, "SystemAccount is required." );

            var grantedPermissions = systemAccount.FindGrantedPermissions ();
            IssueSystemPermissionClaims ( claimsPrincipal, grantedPermissions, systemAccount );
        }

        /// <summary>
        /// Issues the system permission claims for staff.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="staff">The staff.</param>
        public void IssueSystemPermissionClaimsForStaff ( IClaimsPrincipal claimsPrincipal, Staff staff )
        {
            Check.IsNotNull ( claimsPrincipal, "ClaimsPrincipal is required." );
            Check.IsNotNull ( staff, "Staff is required." );

            var grantedRoles = staff.SystemRoles.Select ( x => x.SystemRole );
            var grantedPermissions = _systemPermissionService.FindGrantedSystemPermissions ( grantedRoles );
            IssueSystemPermissionClaimsForStaff ( claimsPrincipal, grantedPermissions, staff );
        }

        /// <summary>
        /// Rollbacks the emergency access.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <param name="staff">The staff.</param>
        public void RollbackEmergencyAccess ( IClaimsPrincipal claimsPrincipal, Staff staff )
        {
            Check.IsNotNull ( claimsPrincipal, "ClaimsPrincipal is required." );
            Check.IsNotNull ( staff, "Staff is required." );

            var identity = ( IClaimsIdentity )claimsPrincipal.Identity;
            identity.Claims.Clear ();
            IssueSystemPermissionClaims ( claimsPrincipal, staff.SystemAccount );
            IssueAccountKeyClaims ( claimsPrincipal, staff.SystemAccount );
            IssueSystemPermissionClaimsForStaff ( claimsPrincipal, staff );
            IssueStaffKeyClaims ( claimsPrincipal, staff );
        }

        #endregion

        #region Methods

        private IEnumerable<SystemPermission> FindEmergencyAccessPermissions ()
        {
            var emergencyAccessRole = _systemRoleRepository.GetByWellKnownName (
                SystemRole.EmergencyAccessRole );

            var grantedPermissions = _systemPermissionService.FindGrantedSystemPermissions (
                new List<Rem.Domain.Core.SecurityModule.SystemRole> { emergencyAccessRole } );

            return grantedPermissions;
        }

        private IList<string> FindExistingSystemPermissionWellKnownNames ( IClaimsIdentity identity )
        {
            var claims = identity.Claims.FindAll ( x => x.ClaimType == ClaimTypes.PermissionClaimType );
            return claims.Select ( claim => claim.Value ).ToList ();
        }

        private void IssueSystemPermissionClaims (
            IPrincipal claimsPrincipal,
            IEnumerable<SystemPermission> grantedPermissions,
            SystemAccount systemAccount )
        {
            var identity = ( IClaimsIdentity )claimsPrincipal.Identity;
            var exsitingPermissions = FindExistingSystemPermissionWellKnownNames ( identity );

            var realm = _federationAuthenticationModule.Realm;

            foreach ( var grantedPermission in grantedPermissions )
            {
                if ( !exsitingPermissions.Any ( x => x == grantedPermission.WellKnownName ) )
                {
                    var claim = new Claim (
                        ClaimTypes.PermissionClaimType,
                        grantedPermission.WellKnownName,
                        realm );

                    identity.Claims.Add ( claim );

                    Logger.Debug ( "Principal ({0}) is issued the following claim ({1}).", systemAccount.Identifier, claim.ToString () );
                }
                else
                {
                    Logger.Debug ( "Claim for permission ({0}) has already existed.", grantedPermission.WellKnownName );
                }
            }
        }

        private void IssueSystemPermissionClaimsForStaff (
            IPrincipal claimsPrincipal,
            IEnumerable<SystemPermission> grantedPermissions,
            Staff staff )
        {
            var identity = ( IClaimsIdentity )claimsPrincipal.Identity;
            var exsitingPermissions = FindExistingSystemPermissionWellKnownNames ( identity );

            var realm = _federationAuthenticationModule.Realm;

            foreach ( var grantedPermission in grantedPermissions )
            {
                if ( !exsitingPermissions.Any ( x => x == grantedPermission.WellKnownName ) )
                {
                    var claim = new Claim (
                        ClaimTypes.PermissionClaimType,
                        grantedPermission.WellKnownName,
                        realm );

                    identity.Claims.Add ( claim );

                    Logger.Debug ( "Staff ({0}) is issued the following claim ({1}).", staff.Key, claim.ToString () );
                }
                else
                {
                    Logger.Debug ( "Claim for permission ({0}) has already existed.", grantedPermission.WellKnownName );
                }
            }
        }

        #endregion
    }
}
