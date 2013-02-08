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

using System.Globalization;
using System.Web;
using System.Web.SessionState;
using NHibernate;
using Pillar.Common.InversionOfControl;
using Rem.Domain.Billing.ClaimModule;
using Rem.Infrastructure.Domain;
using Rem.WellKnownNames.BillingModule;
using ClaimBatchStatus = Rem.WellKnownNames.ClaimModule.ClaimBatchStatus;

namespace Rem.Ria.BillingModule.Web
{
    /// <summary>
    /// Class for handling HTTP.
    /// </summary>
    public class HttpHandler : IHttpHandler, IReadOnlySessionState
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns> true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false. </returns>
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
            var request = context.Request;
            var response = context.Response;

            var requestName = request.QueryString[HttpHandlerQueryStrings.RequestName];

            if ( requestName == HttpHandlerRequestNames.DownloadHealthCareClaim837ProfessionalDocument )
            {
                long claimBatchKey;

                if ( long.TryParse ( request.QueryString[HttpHandlerQueryStrings.ClaimBatchKey], out claimBatchKey ) )
                {
                    ProcessDownloadHealthCareClaim837ProfessionalDocumentRequest ( claimBatchKey, response );
                }
                else
                {
                    response.Write ( "Claim Batch Key is in incorrect format." );
                }
            }
        }

        #endregion

        #region Methods

        private void ProcessDownloadHealthCareClaim837ProfessionalDocumentRequest ( long claimBatchKey, HttpResponse response )
        {
            HealthCareClaim837Professional healthCareClaim837Professional;

            var sessionProvider = IoC.CurrentContainer.Resolve<ISessionProvider> ();

            using ( var session = sessionProvider.GetSession () )
            {
                using ( var tran = session.BeginTransaction () )
                {
                    var healthCareClaim837ProfessionalRepository = IoC.CurrentContainer.Resolve<IHealthCareClaim837ProfessionalRepository> ();
                    healthCareClaim837Professional = healthCareClaim837ProfessionalRepository.GetByClaimBatchKey ( claimBatchKey );

                    if ( healthCareClaim837Professional == null || healthCareClaim837Professional.Document == null )
                    {
                        var claimBatchRepository = IoC.CurrentContainer.Resolve<IClaimBatchRepository> ();
                        var claimBatch = claimBatchRepository.GetByKey ( claimBatchKey );

                        if ( claimBatch != null )
                        {
                            if ( claimBatch.ClaimBatchStatus.WellKnownName == ClaimBatchStatus.Active )
                            {
                                healthCareClaim837Professional = claimBatch.GenerateHealthCareClaim837Professional ();
                            }
                            else
                            {
                                response.Write (
                                    string.Format (
                                        "Cannot create a Health care claim 837P as the claim batch status is {0}.", claimBatch.ClaimBatchStatus.Name ) );
                            }
                        }
                        else
                        {
                            response.Write ( "No Claim Batch found." );
                        }
                    }
                    tran.Commit ();
                }
            }

            if ( healthCareClaim837Professional != null && healthCareClaim837Professional.Document != null )
            {
                response.Clear ();
                response.AddHeader (
                    "Content-Disposition", "attachment; filename=HealthCareClaim837Professional_" + healthCareClaim837Professional.Key );
                response.AddHeader ( "Content-Length", healthCareClaim837Professional.Document.Length.ToString ( CultureInfo.InvariantCulture ) );
                response.OutputStream.Write ( healthCareClaim837Professional.Document, 0, healthCareClaim837Professional.Document.Length );
                response.End ();
            }
            else
            {
                response.Write ( "Could not find/generate Health Care Claim 837 Professional document." );
            }
        }

        #endregion
    }
}
