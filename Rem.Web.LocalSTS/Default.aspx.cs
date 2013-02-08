using System;
using System.Globalization;
using System.Threading;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Web;
using Rem.Web.LocalSTS.STS;

namespace Rem.Web.LocalSTS
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Performs WS-Federation Passive Protocol processing. 
        /// </summary>
        protected void Page_PreRender ( object sender, EventArgs e )
        {
            string action = Request.QueryString [ WSFederationConstants.Parameters.Action ];

            try
            {
                if ( action == WSFederationConstants.Actions.SignIn )
                {
                    // Process signin request.
                    SignInRequestMessage requestMessage = ( SignInRequestMessage ) WSFederationMessage.CreateFromUri ( Request.Url );
                    if ( User != null && User.Identity != null && User.Identity.IsAuthenticated )
                    {
                        SecurityTokenService sts = new CustomSecurityTokenService ( CustomSecurityTokenServiceConfiguration.Current );
                        SignInResponseMessage responseMessage = FederatedPassiveSecurityTokenServiceOperations.ProcessSignInRequest ( requestMessage, User, sts );
                        FederatedPassiveSecurityTokenServiceOperations.ProcessSignInResponse ( responseMessage, Response );
                    }
                    else
                    {
                        throw new UnauthorizedAccessException ();
                    }
                }
                else if ( action == WSFederationConstants.Actions.SignOut )
                {
                    // Process signout request.
                    SignOutRequestMessage requestMessage = ( SignOutRequestMessage ) WSFederationMessage.CreateFromUri ( Request.Url );
                    FederatedPassiveSecurityTokenServiceOperations.ProcessSignOutRequest ( requestMessage, User, requestMessage.Reply, Response );
                }
                else
                {
                    throw new InvalidOperationException (
                        String.Format ( CultureInfo.InvariantCulture,
                                       "The action '{0}' (Request.QueryString['{1}']) is unexpected. Expected actions are: '{2}' or '{3}'.",
                                       String.IsNullOrEmpty ( action ) ? "<EMPTY>" : action,
                                       WSFederationConstants.Parameters.Action,
                                       WSFederationConstants.Actions.SignIn,
                                       WSFederationConstants.Actions.SignOut ) );
                }
            }
            catch ( ThreadAbortException )
            {
            // Swallow exception 
            }
            catch ( Exception genericException )
            {
                throw new Exception ( "An unexpected error occurred when processing the request. See inner exception for details.", genericException );    
            }

        }
    }
}