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
using Microsoft.IdentityModel.Web;
using NLog;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// The SignOffService implements a server user sign off service.
    /// </summary>
    /// <remarks>
    /// WSFederationAuthenticationModule (FAM): Enables browser-based federation, handling redirection to the appropriate STS for authentication and token issuance, and processing the resulting sign-in response to hydrate the issued security token into a ClaimsPrincipal to be used for authorization. This module also handles other important federation messages such as sign-out requests.
    /// </remarks>
    public class SignOffService : ISignOffService
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        private readonly WSFederationAuthenticationModule _federationAuthenticationModule;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SignOffService"/> class.
        /// </summary>
        /// <param name="federatedAuthenticationProvider">
        /// The federated authentication provider. 
        /// </param>
        public SignOffService ( IFederatedAuthenticationProvider federatedAuthenticationProvider )
        {
            _federationAuthenticationModule = federatedAuthenticationProvider.GetFederationAuthenticationModule ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Signs the user out using the Federation Authentication Module (FAM).
        /// </summary>
        /// <remarks>
        /// Federation Authentication Module SignOut calls Session Authentication Module (SAM) DeleteSessionTokenCookie.
        /// </remarks>
        public void SignOff ()
        {
            Logger.Debug (
                "Initiating: SignOff.  Calling the SignOff method of the WSFederationAuthenticationModule. DateTime Utc: " + DateTime.UtcNow );
            _federationAuthenticationModule.SignOut(false);
        }

        #endregion
    }
}
