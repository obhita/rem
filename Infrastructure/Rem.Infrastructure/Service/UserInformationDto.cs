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

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Data transfer object for UserInformation class.
    /// </summary>
    public class UserInformationDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>The account identifier.</value>
        public string AccountIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the account key.
        /// </summary>
        /// <value>The account key.</value>
        public long AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the display name of the agency.
        /// </summary>
        /// <value>The display name of the agency.</value>
        public string AgencyDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the agency key.
        /// </summary>
        /// <value>The agency key.</value>
        public long AgencyKey { get; set; }

        /// <summary>
        /// Gets or sets the granted permissions.
        /// </summary>
        /// <value>The granted permissions.</value>
        public List<string> GrantedPermissions { get; set; }

        /// <summary>
        /// Gets or sets the display name of the location.
        /// </summary>
        /// <value>The display name of the location.</value>
        public string LocationDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the location key.
        /// </summary>
        /// <value>The location key.</value>
        public long LocationKey { get; set; }

        /// <summary>
        /// Gets or sets the first name of the staff.
        /// </summary>
        /// <value>The first name of the staff.</value>
        public string StaffFirstName { get; set; }

        /// <summary>
        /// Gets or sets the staff key.
        /// </summary>
        /// <value>The staff key.</value>
        public long StaffKey { get; set; }

        /// <summary>
        /// Gets or sets the last name of the staff.
        /// </summary>
        /// <value>The last name of the staff.</value>
        public string StaffLastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the staff middle.
        /// </summary>
        /// <value>The name of the staff middle.</value>
        public string StaffMiddleName { get; set; }

        /// <summary>
        /// Gets or sets the direct email address.
        /// </summary>
        /// <value>
        /// The direct email address.
        /// </value>
        public string DirectEmailAddress { get; set; }

        #endregion
    }
}
