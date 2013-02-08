using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// Interface that defines a service synchronization service.
    /// </summary>
    public interface IServiceSynchronizationService
    {
        /// <summary>
        /// Synchronizes the service.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        /// <param name="procedure">The procedure.</param>
        /// <param name="diagnosis">The diagnosis.</param>
        /// <returns>A service.</returns>
        Service SynchronizeService ( Encounter encounter, Procedure procedure, CodedConcept diagnosis );
    }
}
