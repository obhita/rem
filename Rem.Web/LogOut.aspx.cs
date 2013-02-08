using System;
using System.Web.UI;
using Microsoft.IdentityModel.Web;
using NLog;

namespace Rem.Web
{
    /// <summary>
    /// Provides federated logout based on the WS-Federation protocol.
    /// </summary>
    public partial class LogOut : Page
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender ( object sender, EventArgs e )
        {
            // Initiate a federated sign out to the Issuing Party (IP). After the Issuing Party (IP) completes its sign out action, it will issue a request which will be 
            // unauthenticated, to navigate back to this Relying Party (RP) application. The unauthenticated request will force a Secure Token Request (STR) which will then 
            // force navigation to the Issuing Party (IP) hosted login page in order to provide credentials.
 
            // TODO: Figure out how to notify the right Identity Provider about the sign out. 
            // The code below notifies the configuration defined "issuer" of the signout. The issuer defined in configuration is not necessarily the actual issuer.
            // This can be the case when multiple issuers exist. 
            logger.Debug("Initiating: SignOff. Calling the FederatedSignOut method of the WSFederationAuthenticationModule DateTime Utc: " + DateTime.UtcNow );
            var authModule = FederatedAuthentication.WSFederationAuthenticationModule;
            WSFederationAuthenticationModule.FederatedSignOut ( new Uri ( authModule.Issuer ), new Uri ( authModule.Realm ) );
        }
    }
}
