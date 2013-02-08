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

using Microsoft.IdentityModel.Claims;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// This interface provides ability to manage permission claims.
    /// </summary>
    public interface IPermissionClaimsManager
    {
        #region Public Methods

        /// <summary>
        /// Exercises the emergency access.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <param name="systemAccount">
        /// The system account. 
        /// </param>
        void ExerciseEmergencyAccess ( IClaimsPrincipal claimsPrincipal, SystemAccount systemAccount );

        /// <summary>
        /// Issues the account key claims.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <param name="systemAccount">
        /// The system account. 
        /// </param>
        void IssueAccountKeyClaims ( IClaimsPrincipal claimsPrincipal, SystemAccount systemAccount );

        /// <summary>
        /// Issues the staff key claims.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <param name="staff">
        /// The staff. 
        /// </param>
        void IssueStaffKeyClaims ( IClaimsPrincipal claimsPrincipal, Staff staff );

        /// <summary>
        /// Issues the system permission claims.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <param name="systemAccount">
        /// The system account. 
        /// </param>
        void IssueSystemPermissionClaims ( IClaimsPrincipal claimsPrincipal, SystemAccount systemAccount );

        /// <summary>
        /// Issues the system permission claims for staff.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <param name="staff">
        /// The staff. 
        /// </param>
        void IssueSystemPermissionClaimsForStaff ( IClaimsPrincipal claimsPrincipal, Staff staff );

        /// <summary>
        /// Rollbacks the emergency access.
        /// </summary>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <param name="staff">
        /// The staff. 
        /// </param>
        void RollbackEmergencyAccess ( IClaimsPrincipal claimsPrincipal, Staff staff );

        #endregion
    }
}
