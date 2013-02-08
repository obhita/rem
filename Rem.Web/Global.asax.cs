using System;
using System.Diagnostics;
using System.Web;
using Microsoft.IdentityModel.Web;
using Microsoft.IdentityModel.Web.Controls;
using NLog;
using NServiceBus;
using Rem.Infrastructure.Web;

namespace Rem.Web
{
    public class Global : HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void OnStart ( HttpApplicationState appState )
        {
            // Uncomment to debug bootstrapping process.
            // Note: IIS 7 starts up far too fast for Visual Studio to attach.  If you need to 
            //       Debug the bootstrapping process then you need to uncomment this line of code,
            //       connect to Rem through the browser.  This statement will force a debugger to 
            //       attach to the worker process and enable you to debug.
            //System.Diagnostics.Debugger.Launch(); 
            new Bootstrapper().Run ( appState );
        }

        public static void OnEnd ()
        {
        }

        private static void OnFederatedSigningOut(object sender, SigningOutEventArgs e)
        {
            Debug.WriteLine("FederatedAuthenticationModule.SigningOut");
        }

        private static void OnFederatedSignOutError ( object sender, ErrorEventArgs e )
        {
            if ( e.Exception != null )
            {
                Debug.WriteLine ( e.Exception );
            }
        }

        protected void Application_Start ( object sender, EventArgs e )
        {
            Configure.WithWeb()
                .StructureMapBuilder()
                .XmlSerializer()
                .MsmqTransport()
                .IsTransactional(true)
                .DefineEndpointName("RemWebInputQueue")
                .UnicastBus()
                .CreateBus()
                .Start();

            OnStart ( Application );
            FederatedAuthentication.WSFederationAuthenticationModule.SignedIn += OnFederatedSignedIn;
            FederatedAuthentication.WSFederationAuthenticationModule.SigningOut += OnFederatedSigningOut;
            FederatedAuthentication.WSFederationAuthenticationModule.SignOutError += OnFederatedSignOutError;

            FederatedAuthentication.WSFederationAuthenticationModule.SessionSecurityTokenCreated +=
                WSFederationAuthenticationModule_SecurityTokenCreated;
            FederatedAuthentication.WSFederationAuthenticationModule.SecurityTokenReceived += WSFederationAuthenticationModule_SecurityTokenReceived;
            FederatedAuthentication.WSFederationAuthenticationModule.AuthorizationFailed += WSFederationAuthenticationModule_AuthorizationFailed;

            FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenCreated += SessionAuthenticationModule_SessionSecurityTokenCreated;
            FederatedAuthentication.SessionAuthenticationModule.SessionSecurityTokenReceived +=
                SessionAuthenticationModule_SessionSecurityTokenReceived;
        }

        private void WSFederationAuthenticationModule_SecurityTokenCreated ( object sender, SessionSecurityTokenCreatedEventArgs e )
        {
        }

        protected void WSFederationAuthenticationModule_SecurityTokenReceived ( object sender, SecurityTokenReceivedEventArgs e )
        {
        }

        protected void WSFederationAuthenticationModule_AuthorizationFailed ( object sender, AuthorizationFailedEventArgs e )
        {
            Logger.Debug("WSFederationAuthenticationModule AuthorizationFailed event occurred.");
        }

        protected void SessionAuthenticationModule_SessionSecurityTokenCreated ( object sender, SessionSecurityTokenCreatedEventArgs e )
        {
        }

        protected void SessionAuthenticationModule_SessionSecurityTokenReceived ( object sender, SessionSecurityTokenReceivedEventArgs e )
        {
            Logger.Debug(string.Format("SessionAuthenticationModule_SessionSecurityTokenReceived. Session security token has been read from a cookie. ValidTo: {0}. ValidFrom: {1} DateTime Utc: {2}", e.SessionToken.ValidTo, e.SessionToken.ValidFrom, DateTime.UtcNow));
            
            // The sliding expiration implementation will extend the token validity interval based on the current interval length when it is detected as expired.
            var utcNow = DateTime.UtcNow;
            var validFrom = e.SessionToken.ValidFrom;
            var validTo = e.SessionToken.ValidTo;

            if ( validTo > utcNow )
            {
                return;
            }

            var sessionAuthenticationModule = sender as SessionAuthenticationModule;

            if ( sessionAuthenticationModule == null )
            {
                return;
            }

            var slidingExpiration = validTo - validFrom;
            var newValidTo = utcNow + slidingExpiration;
            e.SessionToken = sessionAuthenticationModule.CreateSessionSecurityToken (
                e.SessionToken.ClaimsPrincipal, e.SessionToken.Context, utcNow, newValidTo, e.SessionToken.IsPersistent );
            e.ReissueCookie = true;

            Logger.Debug( string.Format( "Expired session token detected. ReissueCookie called to create new session token from SessionAuthenticationModule_SessionSecurityTokenReceived. ValidTo: {0}. ValidFrom: {1} DateTime Utc: {2}", e.SessionToken.ValidTo, e.SessionToken.ValidFrom, DateTime.UtcNow ) );
        }

        private void OnFederatedSignedIn ( object sender, EventArgs e )
        {
            throw new NotImplementedException ();
        }

        protected void Session_Start ( object sender, EventArgs e )
        {
            Debug.WriteLine ( "Session Start -IsNewSession {0}", Context.Session.IsNewSession );
        }

        protected void Application_BeginRequest ( object sender, EventArgs e )
        {
        }

        protected void Application_AuthenticateRequest ( object sender, EventArgs e )
        {
        }

        protected void Application_Error ( object sender, EventArgs e )
        {
        }

        protected void Session_End ( object sender, EventArgs e )
        {
            Debug.WriteLine ( "Session End -" );
        }

        protected void Application_End ( object sender, EventArgs e )
        {
            OnEnd ();
        }

        protected void WSFederationAuthenticationModule_SessionSecurityTokenCreated ( object sender, SessionSecurityTokenCreatedEventArgs e )
        {
            // IsSessionMode true has the effect of ensuring that the SessionSecurityToken remains in the cache for the whole 
            // duration of the session and generating a cookie which contains just a session identifier rather than the content of the session itself.
            // http://blogs.msdn.com/b/vbertocci/archive/2010/05/26/your-fedauth-cookies-on-a-diet-issessionmode-true.aspx
            FederatedAuthentication.SessionAuthenticationModule.IsSessionMode = true;
        }

        private void WSFederationAuthenticationModule_RedirectingToIdentityProvider ( object sender, RedirectingToIdentityProviderEventArgs e )
        {
            Debug.WriteLine ( "FederatedAuthenticationModule.RedirectingToIdentityProvider -" );
            Debug.WriteLine ( "FederatedAuthenticationModule.RedirectingToIdentityProvider - SigningInRequestMessage" );
            Debug.WriteLine ( e.SignInRequestMessage.WriteQueryString () );
            e.SignInRequestMessage.BaseUri = new Uri ( FederatedAuthentication.WSFederationAuthenticationModule.Issuer );

            // If there is a Home Real specified, then use it as the BaseUri (Go to said IP-STS to get authenticated
            if (string.IsNullOrWhiteSpace ( Request["whr"]) )
            {
                return;
            }
            var url = new Uri ( Request["whr"]);
            e.SignInRequestMessage.BaseUri = url;

            //if ( e.SignInRequestMessage.RequestUrl.Contains (
            //    "IP1RealmEntry.aspx" ) )
            //{
            //    e.SignInRequestMessage.BaseUri =
            //        new Uri ( "https://localhost/IP1/STS/Default.aspx" );
            //}

            //else if ( e.SignInRequestMessage.RequestUrl.Contains (
            //    "IP2RealmEntry.aspx" ) )
            //{
            //    e.SignInRequestMessage.BaseUri = new Uri (
            //        "https://localhost/IP2/STS/Default.aspx" );
            //}
        }
    }
}
