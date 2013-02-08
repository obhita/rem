using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.RateModule
{
    /// <summary>
    /// This service finds the rate based on medical procedure.
    /// </summary>
    public interface IRateLookupService
    {
        /// <summary>
        /// Locates the rate.
        /// </summary>
        /// <param name="medicalProcedure">The medical procedure.</param>
        /// <returns>A Money instance.</returns>
        Money LocateRate ( MedicalProcedure medicalProcedure );
    }
}
