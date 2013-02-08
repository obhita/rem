using System.Collections.Generic;
using System.Linq;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// This class translates the specified patient phone (or a list) to a patient account phone (or a list).
    /// </summary>
    public class PatientAccountPhoneTranslator : IPatientAccountPhoneTranslator
    {
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccountPhoneTranslator"/> class.
        /// </summary>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public PatientAccountPhoneTranslator (ILookupValueRepository lookupValueRepository )
        {
            _lookupValueRepository = lookupValueRepository;
        }

        /// <summary>
        /// Translates the specified patient phone.
        /// </summary>
        /// <param name="patientPhone">The patient phone.</param>
        /// <returns>A PatientAccountPhone.</returns>
        public PatientAccountPhone Translate(PatientPhone patientPhone)
        {
            if (patientPhone == null)
            {
                return null;
            }

            var patientAccountPhoneTypeWellKnownName = WellKnownNames.PatientAccountModule.PatientAccountPhoneType.Home;

            if (patientPhone.PatientPhoneType.WellKnownName == WellKnownNames.PatientModule.PatientPhoneType.Cell)
            {
                patientAccountPhoneTypeWellKnownName = WellKnownNames.PatientAccountModule.PatientAccountPhoneType.Cell;
            }

            var patientAccountPhoneType = _lookupValueRepository.GetLookupByWellKnownName<PatientAccountPhoneType>(patientAccountPhoneTypeWellKnownName);

            var patientAccountPhone = new PatientAccountPhone(patientAccountPhoneType, new Phone(patientPhone.PhoneNumber, patientPhone.PhoneExtensionNumber));

            return patientAccountPhone;
        }

        /// <summary>
        /// Translates the specified patient phones.
        /// </summary>
        /// <param name="patientPhones">The patient phones.</param>
        /// <returns>A IList&lt;PatientAccountPhone&gt;</returns>
        public IList<PatientAccountPhone> Translate(IList<PatientPhone> patientPhones)
        {
            if (patientPhones == null)
            {
                return null;
            }

            return patientPhones.Select ( Translate ).ToList ();
        }
    }
}
