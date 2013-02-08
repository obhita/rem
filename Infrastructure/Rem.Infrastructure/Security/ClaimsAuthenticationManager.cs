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

using System.Linq;
using System.Security;
using Microsoft.IdentityModel.Claims;
using NHibernate;
using NLog;
using Pillar.Common.InversionOfControl;
using Rem.Domain.Core.SecurityModule;
using uNhAddIns.SessionEasier;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// This class lets you perform claims transformation, including adding, modifying, and deleting claims extracted from an incoming token before they are received by your RP application. It provides a single place to authenticate claims. This is a common place between an ASP.NET and a WCF application where you ask the question: “Do I trust the issuer to make these claims?”
    /// </summary>
    public class ClaimsAuthenticationManager : Microsoft.IdentityModel.Claims.ClaimsAuthenticationManager
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticates a specified resource by its name.
        /// </summary>
        /// <param name="resourceName">
        /// Name of the resource. 
        /// </param>
        /// <param name="claimsPrincipal">
        /// The claims principal. 
        /// </param>
        /// <returns>
        /// Returns claims principal for given resource 
        /// </returns>
        public override IClaimsPrincipal Authenticate ( string resourceName, IClaimsPrincipal claimsPrincipal )
        {
            if ( claimsPrincipal.Identity.IsAuthenticated )
            {
                var sessionFactory = GetSessionFactory ();

                using ( var session = sessionFactory.GetCurrentSession () )
                using ( var trans = session.BeginTransaction () )
                {
                    var identity = claimsPrincipal.Identity as IClaimsIdentity;
                    if ( identity != null )
                    {
                        var nameIdentifier = identity.Claims.First ( c => c.ClaimType == Microsoft.IdentityModel.Claims.ClaimTypes.NameIdentifier ).Value;
                        Logger.Debug("Name identifier for authenticated principal ({0}): {1}.", identity.Name, nameIdentifier);

                        Logger.Debug("Resolving dependency on {0}.", typeof(ISystemAccountRepository).Name);
                        var systemAccountRepository = IoC.CurrentContainer.Resolve<ISystemAccountRepository>();
                        Logger.Debug("Resolved dependency on {0}.", typeof(ISystemAccountRepository).Name);

                        var systemAccount = systemAccountRepository.GetByIdentifier ( nameIdentifier );

                        if ( systemAccount != null )
                        {
                            Logger.Debug("Resolving dependency on {0}.", typeof(IPermissionClaimsManager).Name);
                            var permissionClaimsManager = IoC.CurrentContainer.Resolve<IPermissionClaimsManager>();
                            Logger.Debug("Resolved dependency on {0}.", typeof(IPermissionClaimsManager).Name);

                            Logger.Debug("Issue more claims for ({0} ({1}))", systemAccount.DisplayName, systemAccount.EmailAddress.Address);
                            permissionClaimsManager.IssueSystemPermissionClaims ( claimsPrincipal, systemAccount );
                            permissionClaimsManager.IssueAccountKeyClaims ( claimsPrincipal, systemAccount );
                        }
                        else
                        {
                            var errorMessage = string.Format (
                                "Authenticated principal ({0}) with identifier ({1}) does not have a system account in REM.", identity.Name, nameIdentifier );
                            Logger.Debug(errorMessage);
                        }
                    }

                    trans.Commit ();
                }
            }
            else
            {
                Logger.Debug("Resolving dependency on {0}.", typeof(IFederatedAuthenticationProvider).Name);
                var federatedAuthenticationProvider = IoC.CurrentContainer.Resolve<IFederatedAuthenticationProvider>();
                Logger.Debug("Resolved dependency on {0}.", typeof(IFederatedAuthenticationProvider).Name);

                var federationAuthenticationModule = federatedAuthenticationProvider.GetFederationAuthenticationModule ();
                Logger.Debug("Incoming IClaimsPrincipal was not authenticated. WIF will will redirect the request to the identity server {0}.", federationAuthenticationModule.Issuer);
            }

            return claimsPrincipal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the session factory.
        /// </summary>
        /// <returns>
        /// Returns session factory 
        /// </returns>
        private static ISessionFactory GetSessionFactory ()
        {
            var sessionFactoryProvider = IoC.CurrentContainer.Resolve<ISessionFactoryProvider> ();
            return sessionFactoryProvider.First ();
        }

        #endregion
    }
}
