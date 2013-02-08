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
using System.Web.Configuration;
using System.Web.SessionState;

namespace Rem.Infrastructure.Web
{
    /// <summary>
    /// Http handler to provide appropriate federation metadata xml file based on the environment under which the application is running. E.g.: dev, qa.
    /// </summary>
    internal class FederationMetadataXmlHandler : IHttpHandler, IReadOnlySessionState
    {
        #region Constants and Fields

        private const string AppsettingsEnvironment = "Environment";

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        /// <returns> true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false. </returns>
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">
        /// An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. 
        /// </param>
        public void ProcessRequest ( HttpContext context )
        {
            string environment = WebConfigurationManager.AppSettings[AppsettingsEnvironment];

            environment = string.IsNullOrWhiteSpace ( environment ) ? "dev" : environment; // Default to dev if the value is not present.
            string filename = string.Format ( "{0}.FederationMetadata.xml", environment );

            HttpResponse response = context.Response;
            response.WriteFile ( filename );
        }

        #endregion
    }
}
