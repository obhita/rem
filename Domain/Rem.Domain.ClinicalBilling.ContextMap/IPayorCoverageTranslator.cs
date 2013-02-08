using System.Collections.Generic;
using BillingPayorCoverage = Rem.Domain.Billing.PatientAccountModule.PayorCoverage;
using ClinicalPayorCoverage = Rem.Domain.Clinical.PatientModule.PayorCoverageCache;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// This interface translates the specified patient phone to a patient account phone.
    /// </summary>
    public interface IPayorCoverageTranslator
    {
        /// <summary>
        /// Translates the specified clinical payor coverage.
        /// </summary>
        /// <param name="clinicalPayorCoverage">The clinical payor coverage.</param>
        /// <returns>A billing PayorCoverage.</returns>
        BillingPayorCoverage Translate(ClinicalPayorCoverage clinicalPayorCoverage);

        /// <summary>
        /// Translates the specified clinical payor coverages.
        /// </summary>
        /// <param name="clinicalPayorCoverages">The clinical payor coverages.</param>
        /// <returns>A IList&lt;BillingPayorCoverage&gt;</returns>
        IList<BillingPayorCoverage> Translate(IList<ClinicalPayorCoverage> clinicalPayorCoverages);
    }
}