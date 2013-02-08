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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using NHibernate.Criterion;
using NHibernate.Transform;
using Rem.Domain.Billing.ClaimModule;
using Rem.Infrastructure.Service;
using ClaimBatchStatus = Rem.WellKnownNames.ClaimModule.ClaimBatchStatus;

namespace Rem.Ria.BillingModule.Web.BillingAdministrationDashboard
{
    /// <summary>
    /// Class for handling get claim batch list summary request.
    /// </summary>
    public class GetClaimBatchListSummaryRequestHandler :
        NHibernateSessionRequestHandler<GetClaimBatchListSummaryRequest, DtoResponse<ClaimBatchListSummaryDto>>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetClaimBatchListSummaryRequest request )
        {
            var response = CreateTypedResponse ();

            var groupedClaimsCounts = Session.CreateCriteria<Domain.Billing.ClaimModule.Claim> ()
                .CreateAlias ( "ClaimBatch", "cb" )
                .CreateAlias ( "cb.ClaimBatchStatus", "cbs" )
                .CreateAlias ( "cb.PayorType", "pt" )
                .CreateAlias ( "pt.BillingOffice", "bo" )
                .Add ( Restrictions.Eq ( "bo.Key", request.BillingOfficeKey ) )
                .Add ( Restrictions.Not ( Restrictions.Eq ( "cbs.WellKnownName", ClaimBatchStatus.Closed ) ) )
                .SetProjection ( Projections.RowCount (), Projections.GroupProperty ( "pt.Name" ) )
                .SetResultTransformer ( new ClaimDictionaryResultTransformer () ).List ();
            var groupedClaimBatchCharges = Session.CreateCriteria<ClaimBatch> ()
                .CreateAlias ( "ClaimBatchStatus", "cbs" )
                .CreateAlias ( "PayorType", "pt" )
                .CreateAlias ( "pt.BillingOffice", "bo" )
                .Add ( Restrictions.Eq ( "bo.Key", request.BillingOfficeKey ) )
                .Add ( Restrictions.Not ( Restrictions.Eq ( "cbs.WellKnownName", ClaimBatchStatus.Closed ) ) )
                .SetProjection ( Projections.Sum ( "ChargeAmount.Amount" ), Projections.GroupProperty ( "pt.Name" ) )
                .SetResultTransformer ( new ClaimDictionaryResultTransformer () ).List ();

            var claimBatchListSummaryDto = new ClaimBatchListSummaryDto
                {
                    TotalClaims = groupedClaimsCounts.OfType<KeyValuePair<string, object>> ().Select ( kvp => ( int )kvp.Value ).Sum (),
                    TotalCharges = groupedClaimBatchCharges.OfType<KeyValuePair<string, object>> ().Select ( kvp => ( decimal )kvp.Value ).Sum (),
                    ClaimChargesByPayorTypeList =
                        groupedClaimBatchCharges.OfType<KeyValuePair<string, object>>().Select(
                            kvp => string.Format("{0}: {1}", kvp.Key, ((decimal)kvp.Value).ToString("c0"))).ToList(),
                    ClaimCountByPayorTypeList =
                        groupedClaimsCounts.OfType<KeyValuePair<string, object>>().Select(kvp => string.Format("{0}: {1}", kvp.Key, kvp.Value)).ToList()
                };

            response.DataTransferObject = claimBatchListSummaryDto;

            return response;
        }

        #endregion

        /// <summary>
        /// Class for transforming claim dictionary result.
        /// </summary>
        [Serializable]
        private class ClaimDictionaryResultTransformer : IResultTransformer
        {
            #region Public Methods

            /// <summary>
            /// Transforms the list.
            /// </summary>
            /// <param name="collection">The collection.</param>
            /// <returns>A <see cref="System.Collections.IList"/></returns>
            public IList TransformList ( IList collection )
            {
                return collection;
            }

            /// <summary>
            /// Transforms the tuple.
            /// </summary>
            /// <param name="tuple">The tuple.</param>
            /// <param name="aliases">The aliases.</param>
            /// <returns>A <see cref="System.Object"/></returns>
            public object TransformTuple ( object[] tuple, string[] aliases )
            {
                var result = new KeyValuePair<string, object> ( tuple.ElementAt ( 1 ).ToString (), tuple.ElementAt ( 0 ) );
                return result;
            }

            #endregion
        }
    }
}
