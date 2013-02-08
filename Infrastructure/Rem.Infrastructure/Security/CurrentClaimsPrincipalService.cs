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

using System.Web;
using Microsoft.IdentityModel.Claims;
using NLog;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// This class serves the claims information for current user.
    /// </summary>
    public class CurrentClaimsPrincipalService : ICurrentClaimsPrincipalService
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the claims principle of current user.
        /// </summary>
        /// <returns>
        /// Returns the claims principle of current user. 
        /// </returns>
        public IClaimsPrincipal GetCurrentPrincipal ()
        {
            Logger.Info (
                "CurrentClaimsPrincipalService.GetCurrentPrincipal HttpContext.Current.User Name : " + HttpContext.Current.User.Identity.Name );
            Logger.Info (
                "CurrentClaimsPrincipalService.GetCurrentPrincipal HttpContext.Current.User IsAuthenticated : "
                + HttpContext.Current.User.Identity.IsAuthenticated );
            return ( IClaimsPrincipal )HttpContext.Current.User;
        }

        #endregion
    }
}
