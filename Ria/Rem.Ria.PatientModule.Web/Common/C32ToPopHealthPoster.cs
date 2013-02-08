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
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using C32Gen;
using Pillar.Common.Configuration;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Class for posting C32 to pop health.
    /// </summary>
    public class C32ToPopHealthPoster : IC32ToPopHealthPoster
    {
        #region Constants and Fields

        private const string PopHealthServiceAddress = "PopHealthServiceAddress";

        private readonly IC32Builder _c32Builder;
        private readonly string _requestUriString;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="C32ToPopHealthPoster"/> class.
        /// </summary>
        /// <param name="c32Builder">The C32 builder.</param>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        public C32ToPopHealthPoster ( IC32Builder c32Builder, IConfigurationPropertiesProvider configurationPropertiesProvider )
        {
            _c32Builder = c32Builder;
            _requestUriString = configurationPropertiesProvider.GetProperty ( PopHealthServiceAddress );
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Posts the C32 to pop health poster.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public string PostC32ToPopHealthPoster ( long patientKey )
        {
            var c32Xml = _c32Builder.BuildC32Xml ( patientKey, true );

            ServicePointManager.ServerCertificateValidationCallback = CertificateCallback;

            var webRequest = WebRequest.Create ( _requestUriString );
            webRequest.Credentials = new NetworkCredential ( "pophealth", "pophealth" );
            webRequest.Method = "POST";
            webRequest.ContentType = "application/xml";

            var byteArray = Encoding.ASCII.GetBytes ( c32Xml );

            var postStream = webRequest.GetRequestStream ();
            postStream.Write ( byteArray, 0, byteArray.Length );
            postStream.Close ();

            var popHealthResponse = ( HttpWebResponse )webRequest.GetResponse ();

            var receiveStream = popHealthResponse.GetResponseStream ();

            var encode = Encoding.GetEncoding ( "utf-8" );

            var streamReader = new StreamReader ( receiveStream, encode );
            var read = new char[256];
            var count = streamReader.Read ( read, 0, 256 );
            var stringBuilder = new StringBuilder ();
            while ( count > 0 )
            {
                stringBuilder.Append ( new string ( read, 0, count ) );
                count = streamReader.Read ( read, 0, 256 );
            }

            streamReader.Close ();
            receiveStream.Close ();
            popHealthResponse.Close ();

            ServicePointManager.ServerCertificateValidationCallback = null;

            return stringBuilder.ToString ();
        }

        #endregion

        #region Methods

        private bool CertificateCallback ( object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors )
        {
            var request = sender as HttpWebRequest;
            if ( request != null && request.Address.ToString () == _requestUriString )
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
