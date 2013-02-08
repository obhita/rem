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
using System.Text;
using System.Web;
using System.Web.SessionState;
using NLog;
using Pillar.Common.Configuration;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Rem.WellKnownNames.NewCropModule;

namespace Rem.Ria.NewCropModule.Web
{
    /// <summary>
    /// Class for handling new crop HTTP.
    /// </summary>
    public class NewCropHttpHandler : IHttpHandler, IReadOnlySessionState
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest ( HttpContext context )
        {
            var requestName = context.Request.QueryString[NewCropHttpHandlerQueryString.RequestName];

            switch ( requestName )
            {
                case NewCropHttpHandlerRequestName.ERxCompose:
                    ProcessERxComposeRequest ( context );
                    break;

                default:
                    context.Response.Write ( string.Format ( "No {0} request is allowed.", requestName ) );
                    break;
            }
        }

        #endregion

        #region Methods

        private string GenerateErrorPage ( string errorMessage )
        {
            var errorPage = new StringBuilder ();
            var html =
                @"<html>
                    <body style='background-color:#08667F'>
                        <p style='font-color:white'>An error has occurred while processing the NewCrop Request. {0} </p>
                    </body>
                </html>";

            errorPage.AppendFormat ( html, errorMessage );

            return errorPage.ToString ();
        }

        private string GeneratePage ( string ncscriptXml, string endpointUrl )
        {
            var url = new Uri ( endpointUrl );

            var page =
                @"<!doctype html>
                    <head>
                    <script type='text/javascript'>
                        function myfunc () 
                        {
                            var frm = document.getElementById('NewCrop');
                            frm.submit();
                        }

                        window.onload = myfunc;
                    </script>
                    </head>
                    <body>
                    <form id='NewCrop' action='" +
                url +
                @"' method='post'>
                    <!-- <input value='Go' type='submit' style/><br /><br> -->
                    <textarea id='RxInput' name='RxInput' rows='33' cols='79' style='visibility:hidden'>";

            page += ncscriptXml;
            page += @"</textarea></form></body></html>";

            return page;
        }

        private void ProcessERxComposeRequest ( HttpContext context )
        {
            var request = context.Request;
            var response = context.Response;

            var configProvider = IoC.CurrentContainer.Resolve<IConfigurationPropertiesProvider> ();
            var nscriptBuilder = IoC.CurrentContainer.Resolve<INcsCriptBuilder>();

            #region validate parameters

            var patientKeyParameter = request.QueryString[NewCropHttpHandlerQueryString.PatientKey];
            var staffKeyParameter = request.QueryString[NewCropHttpHandlerQueryString.StaffKey];
            var locationKeyParameter = request.QueryString[NewCropHttpHandlerQueryString.LocationKey];
            var agencyKeyParameter = request.QueryString[NewCropHttpHandlerQueryString.AgencyKey];

            var format = "The Request does not contain a parameter named {0}";

            try
            {
                Check.IsNotNullOrWhitespace (
                    patientKeyParameter,
                    string.Format ( format, NewCropHttpHandlerQueryString.PatientKey ) );

                Check.IsNotNullOrWhitespace (
                    staffKeyParameter,
                    string.Format ( format, NewCropHttpHandlerQueryString.StaffKey ) );

                Check.IsNotNullOrWhitespace (
                    locationKeyParameter,
                    string.Format ( format, NewCropHttpHandlerQueryString.LocationKey ) );

                Check.IsNotNullOrWhitespace (
                    agencyKeyParameter,
                    string.Format ( format, NewCropHttpHandlerQueryString.AgencyKey ) );

                long patientKey, staffKey, locationKey, agencyKey;

                long.TryParse ( patientKeyParameter, out patientKey );
                long.TryParse ( staffKeyParameter, out staffKey );
                long.TryParse ( locationKeyParameter, out locationKey );
                long.TryParse ( agencyKeyParameter, out agencyKey );

                #endregion

                Logger.Debug (
                    "Processing NewCrop Session Transfer for Patient Key{0}, StaffKey {1}, AgencyKey {2}, LocationKey {3}",
                    patientKey,
                    staffKey,
                    agencyKey,
                    locationKey );

                var xml = nscriptBuilder.BuildToXml ( patientKey, staffKey, agencyKey, locationKey );
                var url = configProvider.GetProperty ( NewCropConfigurationStoreProperty.EndPointPropertyName );
                var constructedPage = GeneratePage ( xml, url );

                Logger.Debug ( "Transfering Session to NewCrop at endpoint {0}, with the following message {1}", url, xml );

                response.OutputStream.Write (
                    Encoding.UTF8.GetBytes ( constructedPage ),
                    0,
                    Encoding.UTF8.GetBytes ( constructedPage ).Length );
            }
            catch ( ArgumentException argumentException )
            {
                var htmlOutput =
                    GenerateErrorPage (
                        string.Format (
                            "Please contact your System Administrator or check the Application Log for more information. The error details are {0}",
                            argumentException.Message ) );

                Logger.Debug (
                    "An Error occurred when retrieving the NewCrop Configuration Parameters. {0}",
                    argumentException.Message );

                response.OutputStream.Write (
                    Encoding.UTF8.GetBytes ( htmlOutput ),
                    0,
                    Encoding.UTF8.GetBytes ( htmlOutput ).Length );
            }
            catch ( ApplicationException applicationException )
            {
                var htmlOutput = GenerateErrorPage ( applicationException.Message );
                Logger.Debug (
                    "An Error occurred when generating a NewCrop NCScript XML Message. {0}",
                    applicationException.Message );

                response.OutputStream.Write (
                    Encoding.UTF8.GetBytes ( htmlOutput ),
                    0,
                    Encoding.UTF8.GetBytes ( htmlOutput ).Length );
            }
        }

        #endregion
    }
}
