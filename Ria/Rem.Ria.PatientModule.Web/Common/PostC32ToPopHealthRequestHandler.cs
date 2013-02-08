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

using System.Linq;
using System.Text;
using Agatha.Common;
using NHibernate.Linq;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Class for handling post C32 to pop health request.
    /// </summary>
    public class PostC32ToPopHealthRequestHandler : NHibernateSessionRequestHandler<PostC32ToPopHealtheRequest, PostC32ToPopHealthResponse>
    {
        #region Constants and Fields

        private readonly IC32ToPopHealthPoster _c32ToPopHealthPoster;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostC32ToPopHealthRequestHandler"/> class.
        /// </summary>
        /// <param name="c32ToPopHealthPoster">The C32 to pop health poster.</param>
        public PostC32ToPopHealthRequestHandler ( IC32ToPopHealthPoster c32ToPopHealthPoster )
        {
            _c32ToPopHealthPoster = c32ToPopHealthPoster;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( PostC32ToPopHealtheRequest request )
        {
            var response = CreateTypedResponse ();

            var patientKey = request.PatientKey;

            if ( patientKey == null )
            {
                var resultList = from p in Session.Query<Patient> ()
                                                 select p; // new { PatientKey = p.Key };

                var stringBuilder = new StringBuilder ();
                foreach ( var patient in resultList )
                {
                    var message = _c32ToPopHealthPoster.PostC32ToPopHealthPoster ( patient.Key );
                    stringBuilder.AppendLine ( message + " (Patient Name: " + patient.Name.Complete + ")" );
                }

                var popHealthMessage = stringBuilder.ToString ();
                response.Message = popHealthMessage.Remove ( popHealthMessage.Length - 1 );
            }
            else
            {
                // Only send the C32 document for this patient 
                response.Message = _c32ToPopHealthPoster.PostC32ToPopHealthPoster ( patientKey.Value );
            }

            return response;
        }

        #endregion
    }
}
