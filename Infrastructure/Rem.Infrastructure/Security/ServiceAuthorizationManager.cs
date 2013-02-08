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
using System.ServiceModel;
using System.Web.Hosting;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Pillar.Common.InversionOfControl;
using Pillar.Security.AccessControl;

namespace Rem.Infrastructure.Security
{
    /// <summary>
    /// ServiceAuthorizationManager derives from <see cref="IdentityModelServiceAuthorizationManager">IdentityModelServiceAuthorizationManager</see> which is a custom <see cref="ServiceAuthorizationManager">ServiceAuthorizationManager</see> implementation. This class substitues the WCF generated IAuthorizationPolicies with AuthorizationPolicy. These policies do not participate in the EvaluationContext and hence will render an empty WCF AuthorizationConext. Once this AuthorizationManager is substitued to a ServiceHost, only IClaimsPrincipal will be available for Authorization decisions.
    /// </summary>
    public class ServiceAuthorizationManager : IdentityModelServiceAuthorizationManager
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        #endregion

        #region Methods

        /// <summary>
        /// Checks authorization for the given operation context based on policy evaluation.
        /// </summary>
        /// <param name="operationContext">
        /// The operation context. 
        /// </param>
        /// <returns>
        /// true if authorized, false otherwise 
        /// </returns>
        protected override bool CheckAccessCore ( OperationContext operationContext )
        {
            var canAccess = false;

            var applicationVirtualRoot = HostingEnvironment.ApplicationVirtualPath;

            if ( applicationVirtualRoot == null )
            {
                throw new ArgumentException ( "The application virtual root could not be found for the current environment." );
            }

            // Remove the deployment environment specific application virtual root from the front of the resource request identifier.
            var path = applicationVirtualRoot.Equals ( @"/" )
                           ? operationContext.EndpointDispatcher.EndpointAddress.Uri.LocalPath
                           : operationContext.EndpointDispatcher.EndpointAddress.Uri.LocalPath.Remove ( 0, applicationVirtualRoot.Length );

            var currentClaimsPrincipalService = IoC.CurrentContainer.Resolve<ICurrentClaimsPrincipalService> ();
            var principal = currentClaimsPrincipalService.GetCurrentPrincipal ();

            if ( principal.Identity.IsAuthenticated )
            {
                var accessControlManager = IoC.CurrentContainer.Resolve<IAccessControlManager> ();
                canAccess = accessControlManager.CanAccess ( new ResourceRequest { path } );
            }
            else
            {
                Logger.Debug ( string.Format ( "Access to service '{0}' is denied because the principal is not authenticated.", path ) );
            }

            return canAccess;
        }

        #endregion
    }
}
