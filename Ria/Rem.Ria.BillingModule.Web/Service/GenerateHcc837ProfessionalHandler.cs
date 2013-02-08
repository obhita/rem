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

using Agatha.Common;
using Rem.Domain.Billing.ClaimModule;
using Rem.Infrastructure.Service;
using ClaimBatchStatus = Rem.WellKnownNames.ClaimModule.ClaimBatchStatus;

namespace Rem.Ria.BillingModule.Web.Service
{
    /// <summary>
    /// Class for handling generate HCC837 professional request.
    /// </summary>
    public class GenerateHcc837ProfessionalRequestHandler :
        NHibernateSessionRequestHandler<GenerateHcc837ProfessionalRequest, GenerateHcc837ProfessionalResponse>
    {
        #region Constants and Fields

        private readonly IClaimBatchRepository _claimBatchRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateHcc837ProfessionalRequestHandler"/> class.
        /// </summary>
        /// <param name="claimBatchRepository">The claim batch repository.</param>
        public GenerateHcc837ProfessionalRequestHandler ( IClaimBatchRepository claimBatchRepository )
        {
            _claimBatchRepository = claimBatchRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GenerateHcc837ProfessionalRequest request )
        {
            var claimBatch = _claimBatchRepository.GetByKey ( request.ClaimBatchKey );

            if ( claimBatch.ClaimBatchStatus.WellKnownName != ClaimBatchStatus.Hcc837PGenerated )
            {
                claimBatch.GenerateHealthCareClaim837Professional ();
            }

            var response = CreateTypedResponse ();
            return response;
        }

        #endregion
    }
}
