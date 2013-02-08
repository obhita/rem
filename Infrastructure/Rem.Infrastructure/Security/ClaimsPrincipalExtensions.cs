#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using System.Linq;
using Microsoft.IdentityModel.Claims;
using NLog;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// Extenstion methods for <see cref="IClaimsPrincipal"/> .
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the current the account key.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <returns>
        /// Returns the current the account key. 
        /// </returns>
        public static long CurrentAccountKey ( this IClaimsPrincipal claimsPrincipal )
        {
            var claimsIdentity = ( IClaimsIdentity )claimsPrincipal.Identity;
            var accountKey = claimsIdentity.Claims
                .Where ( x => x.ClaimType == ClaimTypes.AccountKeyClaimType )
                .Select ( x => long.Parse ( x.Value ) )
                .FirstOrDefault ();

            return accountKey;
        }

        /// <summary>
        /// Currents the permissions.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <returns>
        /// Returns list of permissions. 
        /// </returns>
        public static List<string> CurrentPermissions ( this IClaimsPrincipal claimsPrincipal )
        {
            var claimsIdentity = ( IClaimsIdentity )claimsPrincipal.Identity;
            var permissions = ( from claim in claimsIdentity.Claims
                                where claim.ClaimType == ClaimTypes.PermissionClaimType
                                select claim.Value ).ToList ();

            if ( permissions.Count == 0 )
            {
                Logger.Info ( "ClaimsPrincipalExtensions.CurrentPermissions permissions count is zero." );
            }

            return permissions;
        }

        /// <summary>
        /// Gets the staff key.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <returns>
        /// Returns the staff key. 
        /// </returns>
        public static long CurrentStaffKey ( this IClaimsPrincipal claimsPrincipal )
        {
            var claimsIdentity = ( IClaimsIdentity )claimsPrincipal.Identity;
            var staffKey = claimsIdentity.Claims
                .Where ( x => x.ClaimType == ClaimTypes.StaffKeyClaimType )
                .Select ( x => long.Parse ( x.Value ) )
                .FirstOrDefault ();

            return staffKey;
        }

        #endregion
    }
}
