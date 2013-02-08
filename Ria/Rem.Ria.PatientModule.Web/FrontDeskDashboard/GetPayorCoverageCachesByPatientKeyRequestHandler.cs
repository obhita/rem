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
using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using AutoMapper;
using NHibernate.Criterion;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Class for handling get payor coverages by patient key request.
    /// </summary>
    public class GetPayorCoverageCachesByPatientKeyRequestHandler :
        NHibernateSessionRequestHandler<GetPayorCoverageCachesByPatientKeyRequest, GetPayorCoverageCachesByPatientKeyResponse>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPayorCoverageCachesByPatientKeyRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public GetPayorCoverageCachesByPatientKeyRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetPayorCoverageCachesByPatientKeyRequest request )
        {
            var response = CreateTypedResponse ();

            var searchCriterion = Restrictions.Where<PayorCoverageCache> ( pc => pc.Patient.Key == request.PatientKey );
            var order = new Order ( "EffectiveDateRange.StartDate", false );

            var rowCount = Session.CreateCriteria<PayorCoverageCache> ()
                .Add (
                    Restrictions.And ( searchCriterion, Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate < DateTime.Today ) ) )
                .SetProjection ( Projections.RowCount () ).FutureValue<int> ();

            var results = Session.CreateCriteria<PayorCoverageCache> ()
                .Add (
                    Restrictions.And ( searchCriterion, Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate < DateTime.Today ) ) )
                .AddOrder ( order )
                .SetFirstResult ( request.PageIndex * request.PageSize )
                .SetMaxResults ( request.PageSize )
                .Future<PayorCoverageCache> ();

            var primaryPayorCoverageCacheTypeKey =
                _mappingHelper.MapLookupField<PayorCoverageCacheType> ( WellKnownNames.PatientModule.PayorCoverageCacheType.Primary ).Key;
            var secondaryPayorCoverageCacheTypeKey =
                _mappingHelper.MapLookupField<PayorCoverageCacheType> ( WellKnownNames.PatientModule.PayorCoverageCacheType.Secondary ).Key;
            var tertiaryPayorCoverageCacheTypeKey =
                _mappingHelper.MapLookupField<PayorCoverageCacheType> ( WellKnownNames.PatientModule.PayorCoverageCacheType.Tertiary ).Key;

            var currentPrimaryList = Session.CreateCriteria<PayorCoverageCache> ()
                .Add (
                    Restrictions.And (
                        Restrictions.And (
                            searchCriterion, Restrictions.Where<PayorCoverageCache> ( pc => pc.PayorCoverageCacheType.Key == primaryPayorCoverageCacheTypeKey ) ),
                        Restrictions.Or (
                            Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate == null ),
                            Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate >= DateTime.Today ) ) ) )
                .AddOrder ( order )
                .Future<PayorCoverageCache> ();

            var currentSecondaryList = Session.CreateCriteria<PayorCoverageCache> ()
                .Add (
                    Restrictions.And (
                        Restrictions.And (
                            searchCriterion, Restrictions.Where<PayorCoverageCache> ( pc => pc.PayorCoverageCacheType.Key == secondaryPayorCoverageCacheTypeKey ) ),
                        Restrictions.Or (
                            Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate == null ),
                            Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate >= DateTime.Today ) ) ) )
                .AddOrder ( order )
                .Future<PayorCoverageCache> ();

            var currentTertiaryList = Session.CreateCriteria<PayorCoverageCache> ()
                .Add (
                    Restrictions.And (
                        Restrictions.And (
                            searchCriterion, Restrictions.Where<PayorCoverageCache> ( pc => pc.PayorCoverageCacheType.Key == tertiaryPayorCoverageCacheTypeKey ) ),
                        Restrictions.Or (
                            Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate == null ),
                            Restrictions.Where<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate >= DateTime.Today ) ) ) )
                .AddOrder ( order )
                .Future<PayorCoverageCache> ();

            var totalCount = rowCount.Value;

            response.PagedHistory = new PagedPayorCoverageCaches
                {
                    PagedList = Mapper.Map<IList<PayorCoverageCache>, IList<PayorCoverageCacheDto>> ( results.ToList () ),
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    TotalCount = totalCount
                };

            response.CurrentPrimaryList = Mapper.Map<IEnumerable<PayorCoverageCache>, IEnumerable<PayorCoverageCacheDto>> ( currentPrimaryList );
            response.CurrentSecondaryList = Mapper.Map<IEnumerable<PayorCoverageCache>, IEnumerable<PayorCoverageCacheDto>> ( currentSecondaryList );
            response.CurrentTertiaryList = Mapper.Map<IEnumerable<PayorCoverageCache>, IEnumerable<PayorCoverageCacheDto>> ( currentTertiaryList );

            return response;
        }

        #endregion
    }
}
