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

using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web;

namespace Rem.Infrastructure.Mvc.Security
{
    /// <summary>
    /// Class for inspecting message.
    /// </summary>
    public class MessageInspector : IClientMessageInspector
    {
        #region Public Methods

        /// <summary>
        /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        /// <param name="reply">The message to be transformed into types and handed back to the client application.</param>
        /// <param name="correlationState">Correlation state data.</param>
        public void AfterReceiveReply ( ref Message reply, object correlationState )
        {
        }

        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        /// <param name="channel">The  client object channel.</param>
        /// <returns>The object that is returned as the <paramref name="correlationState "/>argument of the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)"/> method. This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid"/> to ensure that no two <paramref name="correlationState"/> objects are the same.</returns>
        public object BeforeSendRequest ( ref Message request, IClientChannel channel )
        {
            // The HTTP request object is made available in the outgoing message only
            // when the Visual Studio Debugger is attacched to the running process
            if ( !request.Properties.ContainsKey ( HttpRequestMessageProperty.Name ) )
            {
                request.Properties.Add (
                    HttpRequestMessageProperty.Name,
                    new HttpRequestMessageProperty () );
            }

            var httpRequest = ( HttpRequestMessageProperty )
                              request.Properties[HttpRequestMessageProperty.Name];
            var cookieStringBuilder = new StringBuilder ();
            foreach ( string cookieName in HttpContext.Current.Request.Cookies )
            {
                if ( !cookieName.StartsWith ( "FedAuth" ) )
                {
                    cookieStringBuilder.Append (
                        string.Format ( "{0}={1};", cookieName.Replace ( "Server", "" ), HttpContext.Current.Request.Cookies[cookieName].Value ) );
                }
            }
            httpRequest.Headers.Add ( HttpRequestHeader.Cookie, cookieStringBuilder.ToString () );
            httpRequest.Headers.Add ( HttpRequestHeader.ContentType, "application/soap+msbin1" );
            httpRequest.Headers.Add ( HttpRequestHeader.Connection, "Keep-Alive" );

            return null;
        }

        #endregion
    }
}
