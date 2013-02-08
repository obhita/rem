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

using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using AutoMapper;
using NHibernate.Linq;
using Rem.Infrastructure.Service;
using Rem.WellKnownNames.ClaimModule;

namespace Rem.Ria.BillingModule.Web.BillingAdministrationDashboard
{
    /// <summary>
    /// Class for handling get open claim summary list by business office key request.
    /// </summary>
    public class GetOpenClaimSummaryListByBusinessOfficeKeyRequestHandler :
        NHibernateSessionRequestHandler<GetOpenClaimSummaryListByBillingOfficeKeyRequest, DtoResponse<PagedClaimSummaryListDto>>
    {
        #region Public Methods

        /// <summary>
        /// Using billing office key returns a paged list of ClaimSummaryDtos.
        /// </summary>
        /// <param name="request">The request containing the Billing Office Key.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/> containing a paged list of <see cref="ClaimSummaryDto"/></returns>
        public override Response Handle ( GetOpenClaimSummaryListByBillingOfficeKeyRequest request )
        {
            var response = CreateTypedResponse ();

            var claims = Session.Query<Domain.Billing.ClaimModule.Claim> ()
                .Where (
                    c => c.ClaimBatch.ClaimBatchStatus.WellKnownName != ClaimBatchStatus.Closed
                         && c.Payor.BillingOffice.Key == request.BillingOfficeKey )
                .Skip ( request.PageIndex * request.PageSize )
                .Take ( request.PageSize )
                .ToFuture ();

            var totalCount = Session.Query<Domain.Billing.ClaimModule.Claim> ()
                .Where (
                    c => c.ClaimBatch.ClaimBatchStatus.WellKnownName != ClaimBatchStatus.Closed
                         && c.Payor.BillingOffice.Key == request.BillingOfficeKey )
                .Count ();

            response.DataTransferObject = new PagedClaimSummaryListDto
                {
                    ClaimSummaryList =
                        Mapper.Map<IEnumerable<Domain.Billing.ClaimModule.Claim>, IEnumerable<ClaimSummaryDto>> ( claims.ToList () ).ToList (),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalCount = totalCount
                };

            return response;
        }

        #endregion
    }
}
