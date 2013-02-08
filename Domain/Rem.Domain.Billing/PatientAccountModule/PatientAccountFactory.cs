using System;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.PatientAccountModule
{
    /// <summary>
    /// Factory for creating <see cref="PatientAccount"/> instances.
    /// </summary>
    public class PatientAccountFactory : IPatientAccountFactory
    {
        private readonly IPatientAccountRepository _patientAccountRepository;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccountFactory"/> class.
        /// </summary>
        /// <param name="patientAccountRepository">The patient account repository.</param>
        public PatientAccountFactory(IPatientAccountRepository patientAccountRepository)
        {
            _patientAccountRepository = patientAccountRepository;
        }

        /// <summary>
        /// Creates the patient account.
        /// </summary>
        /// <param name="billingOffice">The billing office.</param>
        /// <param name="medicalRecordNumber">The medical record number.</param>
        /// <param name="name">The name.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="homeAddress">The home address.</param>
        /// <param name="administrativeGender">The administrative gender.</param>
        /// <returns>A patient account.</returns>
        public virtual PatientAccount CreatePatientAccount ( BillingOffice billingOffice, long medicalRecordNumber, PersonName name, DateTime? birthDate, Address homeAddress, AdministrativeGender administrativeGender)
        {
            var patientAccount = new PatientAccount ( billingOffice, medicalRecordNumber, name, birthDate, homeAddress, administrativeGender );
            _patientAccountRepository.MakePersistent ( patientAccount );
            return patientAccount;
        }
    }
}
