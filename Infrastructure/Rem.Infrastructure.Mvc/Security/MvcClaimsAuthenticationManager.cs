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

using System;
using System.IO;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.Tokens.Saml2;
using Pillar.Common.Configuration;

namespace Rem.Infrastructure.Mvc.Security
{
    /// <summary>
    /// Class for managing MVC claims authentication.
    /// </summary>
    public class MvcClaimsAuthenticationManager : ClaimsAuthenticationManager
    {
        #region Public Methods

        /// <summary>
        /// Authenticates a specified resource by its name.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="claimsPrincipal">The claims principal.</param>
        /// <returns>Returns claims principal for given resource</returns>
        public override IClaimsPrincipal Authenticate ( string resourceName, IClaimsPrincipal claimsPrincipal )
        {
            if ( claimsPrincipal.Identity.IsAuthenticated )
            {
                var identity = claimsPrincipal.Identity as IClaimsIdentity;
                if ( identity != null )
                {
                    CheckAuthentication ( claimsPrincipal );
                }
            }
            else
            {
                //Logger.Error ( "Incoming IClaimsPrincipal was not authenticated" );
            }

            return claimsPrincipal;
        }

        /// <summary>
        /// Checks the authentication.
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal.</param>
        public void CheckAuthentication ( IClaimsPrincipal claimsPrincipal )
        {
            var bootStrapToken = ( from ident in claimsPrincipal.Identities
                                             where ident.BootstrapToken != null && ident.BootstrapToken is SamlSecurityToken
                                             select ident.BootstrapToken ).FirstOrDefault ();

            var responseSerializer = new WSTrust13ResponseSerializer ();
            var requestSecurityTokenResponse = new RequestSecurityTokenResponse
                {
                    AppliesTo = new EndpointAddress ( "https://localhost/Rem.Web" ),
                    Context = "passive",
                    Lifetime = new Lifetime ( bootStrapToken.ValidFrom, bootStrapToken.ValidTo ),
                    RequestedSecurityToken = new RequestedSecurityToken ( bootStrapToken ),
                    TokenType = Saml2SecurityTokenHandler.TokenProfile11ValueType,
                    RequestType = WSTrust13Constants.RequestTypes.Issue,
                    KeyType = WSTrust13Constants.KeyTypes.Bearer
                };

            string content;
            using ( var swriter = new StringWriter () )
            {
                using ( var xmlWriter = XmlWriter.Create ( swriter ) )
                {
                    responseSerializer.WriteXml (
                        requestSecurityTokenResponse,
                        xmlWriter,
                        new WSTrustSerializationContext () );
                    xmlWriter.Flush ();
                    swriter.Flush ();
                    var xmlString = swriter.ToString ().Replace ( "<?xml version=\"1.0\" encoding=\"utf-16\"?>", "" );
                    content = string.Format (
                        "wa=wsignin1.0&wresult={0}&wctx=rm%3D0%26id%3Dpassive%26ru%3D%252fRem.Web%252f",
                        HttpContext.Current.Server.UrlEncode ( xmlString ) );
                }
            }
            var currentHttpRequest = HttpContext.Current.Request;

            var cookieContainer = new CookieContainer ();
            //foreach (string cookieName in currentHttpRequest.Cookies)
            //{
            //    var httpcookie = currentHttpRequest.Cookies[cookieName];
            //    var cookie = new Cookie(cookieName, httpcookie.Value, httpcookie.Path, httpcookie.Domain ?? "localhost");
            //    cookieContainer.Add(cookie);
            //}

            var appSettingsConfiguration = new AppSettingsConfiguration ();
            var remWebApplicationAddress = appSettingsConfiguration.GetProperty ( "RemWebApplicationAddress" );
            var request = ( HttpWebRequest )WebRequest.CreateDefault ( new Uri ( remWebApplicationAddress ) );
            request.Method = currentHttpRequest.HttpMethod;
            request.CookieContainer = cookieContainer;
            request.ContentType = currentHttpRequest.ContentType;
            request.Accept = string.Empty;
            foreach ( var acceptType in currentHttpRequest.AcceptTypes )
            {
                request.Accept += acceptType;
                if ( acceptType != currentHttpRequest.AcceptTypes.Last () )
                {
                    request.Accept += ", ";
                }
            }
            request.ContentLength = content.Length;
            request.Referer = currentHttpRequest.UrlReferrer.AbsoluteUri.Replace ( "Rem.Mvc", "Rem.Web" );
            request.AllowAutoRedirect = false;
            using ( var streamWriter = new StreamWriter ( request.GetRequestStream () ) )
            {
                streamWriter.Write ( content );
            }
            try
            {
                var response = ( HttpWebResponse )request.GetResponse ();
                while ( response.StatusCode == HttpStatusCode.Found )
                {
                    response.Close ();
                    var location = response.Headers["Location"];
                    if ( !location.StartsWith ( "http" ) )
                    {
                        location = string.Format ( "{0}://{1}", request.RequestUri.Scheme, request.RequestUri.Host ) + location;
                    }
                    request = GetNewRequest ( location, cookieContainer );

                    response = ( HttpWebResponse )request.GetResponse ();
                }
                if ( response.StatusCode != HttpStatusCode.OK )
                {
                    throw new Exception ( "Failed authentication." );
                }
                var cookieStringBuilder = new StringBuilder ();
                foreach ( Cookie cookie in cookieContainer.GetCookies ( new Uri ( "https://localhost" ) ) )
                {
                    if ( cookie.Name.StartsWith ( "FedAuth" ) )
                    {
                        HttpContext.Current.Response.Cookies.Add ( new HttpCookie ( "Server" + cookie.Name, cookie.Value ) );
                        cookieStringBuilder.Append ( cookie.Value );
                    }
                    if ( cookie.Name == "ASP.NET_SessionId" )
                    {
                        HttpContext.Current.Response.Cookies.Add ( new HttpCookie ( cookie.Name, cookie.Value ) );
                    }
                }
            }
            catch ( Exception e )
            {
                throw;
            }
        }

        #endregion

        #region Methods

        private HttpWebRequest GetNewRequest ( string targetUrl, CookieContainer cookieContainer )
        {
            var request = ( HttpWebRequest )WebRequest.Create ( targetUrl );
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = false;
            return request;
        }

        #endregion
    }
}
