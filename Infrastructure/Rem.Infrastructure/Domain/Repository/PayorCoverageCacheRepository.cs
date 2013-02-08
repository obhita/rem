#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
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
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.PatientModule.PayorCoverageCache">PayorCoverageCache</see>.
    /// </summary>
    public class PayorCoverageCacheRepository : NHibernateRepositoryBase<PayorCoverageCache>, IPayorCoverageCacheRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverageCacheRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public PayorCoverageCacheRepository(ISessionProvider sessionProvider)
            : base ( sessionProvider )
        {
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A PayorCoverage.</returns>
        public PayorCoverageCache GetByKey(long key)
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Makes the persistent.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A PayorCoverage.</returns>
        public PayorCoverageCache MakePersistent(PayorCoverageCache entity)
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Makes the transient.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MakeTransient(PayorCoverageCache entity)
        {
            Helper.MakeTransient ( entity );
        }

        /// <summary>
        /// Gets the payor coverage list by patient key.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>
        /// A IList&lt;PayorCoverage&gt;
        /// </returns>
        public System.Collections.Generic.IList<PayorCoverageCache> GetPayorCoverageListByPatientKey(long patientKey)
        {
            var payorCoverageCaches = Session.QueryOver<PayorCoverageCache>()
                .Where(c => c.Patient.Key == patientKey)
                .List();
            return payorCoverageCaches;
        }

        /// <summary>
        /// Determines whether that there are any payor coverages with specified type whose effective date range intersect with specified date range.
        /// </summary>
        /// <param name="payorCoverageCacheKey">The payor coverage cache key.</param>
        /// <param name="patientKey">The patient key.</param>
        /// <param name="payorCoverageCacheType">Type of the payor coverage cache.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <returns>True if there exists a match; otherwise false</returns>
        public bool AnyPayorCoverageTypeInEffectiveDateRange( long payorCoverageCacheKey, long patientKey, PayorCoverageCacheType payorCoverageCacheType, DateRange effectiveDateRange)
        {
            var startDate = effectiveDateRange.StartDate.Value;
            var endDate = effectiveDateRange.EndDate;
            var query = Session.CreateCriteria<PayorCoverageCache> ().Add (
                Restrictions.And ( Restrictions.Eq ( Projections.Property<PayorCoverageCache> ( pc => pc.Patient.Key ), patientKey ),
                Restrictions.And (
                Restrictions.Not ( Restrictions.IdEq ( payorCoverageCacheKey ) ),
                Restrictions.And (
                    Restrictions.Eq ( Projections.Property<PayorCoverageCache> ( pc => pc.PayorCoverageCacheType ), payorCoverageCacheType.Key ),
                    Restrictions.Or (
                        Restrictions.Eq ( Projections.Property<PayorCoverageCache> ( pc => pc.EffectiveDateRange.StartDate ), startDate ),
                        Restrictions.Or (
                            Restrictions.And (
                                Restrictions.Lt ( Projections.Property<PayorCoverageCache> ( pc => pc.EffectiveDateRange.StartDate ), startDate ),
                                Restrictions.Or (
                                    Restrictions.IsNull ( Projections.Property<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate ) ),
                                    Restrictions.Ge ( Projections.Property<PayorCoverageCache> ( pc => pc.EffectiveDateRange.EndDate ), startDate ) ) ),
                            Restrictions.And (
                                Restrictions.Gt ( Projections.Property<PayorCoverageCache> ( pc => pc.EffectiveDateRange.StartDate ), startDate ),
                                Restrictions.Or (
                                    Restrictions.IsNull ( Projections.Constant ( endDate, NHibernateUtil.GuessType ( typeof(DateTime?) ) ) ),
                                    Restrictions.Le ( Projections.Property<PayorCoverageCache> ( pc => pc.EffectiveDateRange.StartDate ), endDate ) ) ) )
                        ) ) ) )
                )
                .SetProjection ( Projections.RowCount () )
                .FutureValue<int> ();
            return query.Value > 0;
        }
    }
}
