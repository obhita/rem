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
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Security;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Factory for user information dto.
    /// </summary>
    public class UserInformationDtoFactory : IUserInformationDtoFactory
    {
        #region Constants and Fields

        private readonly ICurrentClaimsPrincipalService _currentClaimsPrincipalService;
        private readonly ISessionProvider _sessionProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInformationDtoFactory"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        /// <param name="currentClaimsPrincipalService">The current claims principal service.</param>
        public UserInformationDtoFactory (
            ISessionProvider sessionProvider,
            ICurrentClaimsPrincipalService currentClaimsPrincipalService )
        {
            _sessionProvider = sessionProvider;
            _currentClaimsPrincipalService = currentClaimsPrincipalService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the user information dto.
        /// </summary>
        /// <returns>A <see cref="UserInformationDto"/></returns>
        public UserInformationDto CreateUserInformationDto ()
        {
            var claimsPrincipal = _currentClaimsPrincipalService.GetCurrentPrincipal ();
            var permissions = claimsPrincipal.CurrentPermissions ();
            var accountKey = claimsPrincipal.CurrentAccountKey ();
            var staffKey = claimsPrincipal.CurrentStaffKey ();

            UserInformationDto userInformationDto = null;

            var session = _sessionProvider.GetSession ();

            var account = session.Get<SystemAccount> ( accountKey );
            var staff = account.StaffMembers.First ( x => x.Key == staffKey );
            var agency = staff.Agency;
            var location = staff.PrimaryLocation;

            userInformationDto = new UserInformationDto
                {
                    AccountKey = account.Key,
                    AccountIdentifier = account.Identifier,
                    AgencyKey = agency.Key,
                    AgencyDisplayName = agency.AgencyProfile.AgencyName.DisplayName,
                    LocationKey = location == null ? 0 : location.Key,
                    LocationDisplayName = location == null ? string.Empty : location.LocationProfile.LocationName.DisplayName,
                    StaffKey = staff.Key,
                    StaffFirstName = staff.StaffProfile.StaffName.First,
                    StaffMiddleName = staff.StaffProfile.StaffName.Middle,
                    StaffLastName = staff.StaffProfile.StaffName.Last,
                    DirectEmailAddress = staff.DirectAddressCredential == null ? null : (staff.DirectAddressCredential.DirectAddress == null? null : staff.DirectAddressCredential.DirectAddress.Address),
                    GrantedPermissions = permissions
                };

            return userInformationDto;
        }

        #endregion
    }
}
