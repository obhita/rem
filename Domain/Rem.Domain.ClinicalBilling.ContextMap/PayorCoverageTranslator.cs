using System.Collections.Generic;
using System.Linq;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Billing.PayorModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// This class translates the specified patient phone to a patient account phone.
    /// </summary>
    public class PayorCoverageTranslator : IPayorCoverageTranslator
    {
        private readonly IPayorRepository _payorRepository;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverageTranslator"/> class.
        /// </summary>
        /// <param name="payorRepository">The payor repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public PayorCoverageTranslator (IPayorRepository  payorRepository, ILookupValueRepository lookupValueRepository )
        {
            _payorRepository = payorRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        /// <summary>
        /// Translates the specified clinical payor coverage.
        /// </summary>
        /// <param name="clinicalPayorCoverage">The clinical payor coverage.</param>
        /// <returns>A billing PayorCoverage.</returns>
        public PayorCoverage Translate(PayorCoverageCache clinicalPayorCoverage)
        {
            if (clinicalPayorCoverage == null)
            {
                return null;
            }

            var payor = _payorRepository.GetByKey ( clinicalPayorCoverage.PayorCache.Key );

            var clinicalPayorSubscriber = clinicalPayorCoverage.PayorSubscriberCache;
            var payorSubscriberRelationshipType =
                _lookupValueRepository.GetLookupByWellKnownName<PayorSubscriberRelationshipType> (
                    clinicalPayorSubscriber.PayorSubscriberRelationshipCacheType.WellKnownName );
            var payorSubscriber = new PayorSubscriber (
                clinicalPayorSubscriber.Address,
                clinicalPayorSubscriber.BirthDate,
                clinicalPayorSubscriber.AdministrativeGender,
                clinicalPayorSubscriber.Name,
                payorSubscriberRelationshipType );

            var payorCoverageType =
                _lookupValueRepository.GetLookupByWellKnownName<PayorCoverageType> ( clinicalPayorCoverage.PayorCoverageCacheType.WellKnownName );

            var billingPayorCoverage = new PayorCoverage (
                payor, payorSubscriber, clinicalPayorCoverage.MemberNumber, clinicalPayorCoverage.EffectiveDateRange, payorCoverageType);

            return billingPayorCoverage;
        }

        /// <summary>
        /// Translates the specified clinical payor coverages.
        /// </summary>
        /// <param name="clinicalPayorCoverages">The clinical payor coverages.</param>
        /// <returns>A IList&lt;PayorCoverage&gt;</returns>
        public IList<PayorCoverage> Translate(IList<PayorCoverageCache> clinicalPayorCoverages)
        {
            if (clinicalPayorCoverages == null)
            {
                return null;
            }

            return clinicalPayorCoverages.Select(Translate).ToList();
        }
    }
}