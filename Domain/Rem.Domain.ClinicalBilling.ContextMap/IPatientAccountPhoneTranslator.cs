using System.Collections.Generic;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// This interface translates the specified patient phone to a patient account phone.
    /// </summary>
    public interface IPatientAccountPhoneTranslator
    {
        /// <summary>
        /// Translates the specified patient phone.
        /// </summary>
        /// <param name="patientPhone">The patient phone.</param>
        /// <returns>A PatientAccountPhone.</returns>
        PatientAccountPhone Translate ( PatientPhone patientPhone );

        /// <summary>
        /// Translates the specified patient phones.
        /// </summary>
        /// <param name="patientPhones">The patient phones.</param>
        /// <returns>A IList&lt;PatientAccountPhone&gt;</returns>
        IList<PatientAccountPhone> Translate ( IList<PatientPhone> patientPhones );
    }
}
