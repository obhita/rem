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

using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Agatha.Common;
using Agatha.ServiceLayer;

namespace Rem.Ria.PatientModule.Web.DroolsTest
{
    /// <summary>
    /// Class for handling send drools test request.
    /// </summary>
    public class SendDroolsTestRequestHandler : RequestHandler<SendDroolsTestRequest, DroolsTestResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( SendDroolsTestRequest request )
        {
            var response = CreateTypedResponse ();
            
                var outString =
                    string.Format (
                        "<batch-execution lookup=\"ksession1\">" +
                          "<insert out-identifier=\"xacmlresponse\">" +
                             "<com.sample.model.XACMLResponse>{0}</com.sample.model.XACMLResponse>" +
                          "</insert>" +
                          "<insert out-identifier=\"acsutil\">" +
                            "<com.sample.model.ACSUtil>" +
                              "<xmlString>{1}</xmlString>" +
                            "</com.sample.model.ACSUtil>" +
                          "</insert>" +
                          "<insert out-identifier=\"condition\">" +
                            "<com.sample.model.Condition></com.sample.model.Condition>" +
                          "</insert>" +
                          "<insert out-identifier=\"medication\">" +
                            "<com.sample.model.Medications>" +
                              "<code>307789</code>" +
                            "</com.sample.model.Medications>" +
                          "</insert>" +
                          "<insert out-identifier=\"result\">" +
                             "<com.sample.model.Result></com.sample.model.Result>" +
                          "</insert>" +
                          "<start-process processId=\"acsRuleFlow\"/>" +
                          "<fire-all-rules/>" +
                        "</batch-execution>",
                        request.XacmlText,
                        request.C32Text ).Replace ( "\n", string.Empty );
                var regex = new Regex(@">\s*<");
                outString = regex.Replace ( outString, "><" );
            var httprequest = ( HttpWebRequest )WebRequest.Create ( request.DroolsServerAddress );
            httprequest.Method = "POST";
            httprequest.ContentType = "text/plain";
            httprequest.ContentLength = outString.Length;
            var requestStream = httprequest.GetRequestStream ();
            using ( var writer = new StreamWriter ( requestStream, Encoding.ASCII ) )
            {
                writer.Write ( outString );
                writer.Flush();
            }

            var httpResponse = httprequest.GetResponse();
            using (var reader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseString = reader.ReadToEnd();
                const string FindString = "xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
                var startIndex = responseString.IndexOf ( FindString ) + FindString.Length;
                var c32String = responseString.Substring ( startIndex, responseString.IndexOf ( "</xmlString>", startIndex ) - startIndex );
                var xmlDoc = new XmlDocument ();
                xmlDoc.LoadXml ( HttpUtility.HtmlDecode ( c32String ) );
                var c32Builder = new StringBuilder ();
                using (var stringWriter = new XmlTextWriter(new StringWriter(c32Builder)))
                {
                    stringWriter.Formatting = Formatting.Indented;
                    xmlDoc.Save ( stringWriter );
                    response.C32Reponse = c32Builder.ToString ().Replace ( "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n", string.Empty);
                }
            }

            return response;
        }

        #endregion
    }
}
