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

using System.Linq;
using Microsoft.IdentityModel.Claims;
using Pillar.Security.AccessControl;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// This class serves the permission information for current user.
    /// </summary>
    public class CurrentUserPermissionService : ICurrentUserPermissionService
    {
        #region Constants and Fields

        private readonly ICurrentClaimsPrincipalService _currentClaimsPrincipalService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserPermissionService"/> class.
        /// </summary>
        /// <param name="currentClaimsPrincipalService">The current claims principal service.</param>
        public CurrentUserPermissionService ( ICurrentClaimsPrincipalService currentClaimsPrincipalService )
        {
            _currentClaimsPrincipalService = currentClaimsPrincipalService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether the current user has the specified <see cref="Permission"/>.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        ///   <c>true</c> if the user has been granted the specified <see cref="Permission"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool DoesUserHavePermission ( Permission permission )
        {
            var claimsPrincipal = _currentClaimsPrincipalService.GetCurrentPrincipal ();
            var claimsIdentity = ( IClaimsIdentity )claimsPrincipal.Identity;
            var hasClaim = claimsIdentity.Claims.Any (
                c =>
                c.ClaimType == ClaimTypes.PermissionClaimType && c.Value == permission.Name );

            return hasClaim;
        }

        #endregion
    }
}
