using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// Interface that defines an encounter synchronization service.
    /// </summary>
    public interface IEncounterSynchronizationService
    {
        /// <summary>
        /// Synchronizes the encounter with the clinical visit.
        /// </summary>
        /// <param name="patientAccount">The patient account.</param>
        /// <param name="visit">The visit.</param>
        /// <returns>
        /// An <see cref="Encounter"/> instance.
        /// </returns>
        Encounter SynchronizeEncounter ( PatientAccount patientAccount, Visit visit );
    }
}
