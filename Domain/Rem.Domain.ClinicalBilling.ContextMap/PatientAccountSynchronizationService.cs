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
using System.Linq;
using Pillar.Common.Utility;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.PatientModule;
using IClinicalPayorCoverageRepository = Rem.Domain.Clinical.PatientModule.IPayorCoverageCacheRepository;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// The class defines a patient account service.
    /// </summary>
    public class PatientAccountSynchronizationService : IPatientAccountSynchronizationService
    {
        private readonly IBillingOfficeRepository _billingOfficeRepository;
        private readonly IPatientAccountRepository _patientAccountRepository;
        private readonly IPatientAccountFactory _patientAccountFactory;
        private readonly IPatientAccountPhoneTranslator _patientAccountPhoneTranslator;
        private readonly IClinicalPayorCoverageRepository _clinicalPayorCoverageRepository;
        private readonly IPayorCoverageTranslator _payorCoverageTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccountSynchronizationService"/> class.
        /// </summary>
        /// <param name="billingOfficeRepository">The billing office repository.</param>
        /// <param name="patientAccountRepository">The patient account repository.</param>
        /// <param name="patientAccountFactory">The patient account factory.</param>
        /// <param name="patientAccountPhoneTranslator">The patient phone to patient account phone translator.</param>
        /// <param name="payorCoverageRepository">The clinical payor coverage repository.</param>
        /// <param name="payorCoverageTranslator">The payor coverage translator.</param>
        public PatientAccountSynchronizationService (
            IBillingOfficeRepository billingOfficeRepository,
            IPatientAccountRepository patientAccountRepository,
            IPatientAccountFactory patientAccountFactory,
            IPatientAccountPhoneTranslator patientAccountPhoneTranslator,
            IClinicalPayorCoverageRepository payorCoverageRepository,
            IPayorCoverageTranslator payorCoverageTranslator)
        {
            _billingOfficeRepository = billingOfficeRepository;
            _patientAccountRepository = patientAccountRepository;
            _patientAccountFactory = patientAccountFactory;
            _patientAccountPhoneTranslator = patientAccountPhoneTranslator;
            _clinicalPayorCoverageRepository = payorCoverageRepository;
            _payorCoverageTranslator = payorCoverageTranslator;
        }

        /// <summary>
        /// Creates or Updates the patient account based on the given patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <returns>A <see cref="PatientAccount"/></returns>
        public PatientAccount SynchronizePatientAccount(Patient patient)
        {
            Check.IsNotNull ( patient, "Patient is required." );

            var agency = patient.Agency;
            var billingOffice = _billingOfficeRepository.GetByAgencyKey ( agency.Key );
            if (billingOffice == null)
            {
                throw new ArgumentException( string.Format ( 
                    "Agency {0} does not have a billing office.", 
                    agency.AgencyProfile.AgencyName.LegalName ));
            }

            var medicalRecordNumber = patient.Key;
            var patientAccount = _patientAccountRepository.GetByMedicalRecordNumber ( medicalRecordNumber );
            if ( patientAccount == null )
            {
                patientAccount = _patientAccountFactory.CreatePatientAccount(billingOffice, medicalRecordNumber, patient.Name, patient.Profile.BirthDate, patient.HomeAddress, patient.Profile.PatientGender.AdministrativeGender);
            }
            
            if (patient.Name != patientAccount.Name)
            {
                patientAccount.ReviseName ( patient.Name );
            }

            var patientPhones = patient.PhoneNumbers;
            var newPatientAccountPhones = _patientAccountPhoneTranslator.Translate ( patientPhones.ToList() );
            patientAccount.RevisePhones ( newPatientAccountPhones );

            if (patient.HomeAddress != patientAccount.HomeAddress)
            {
                patientAccount.ReviseHomeAddress ( patient.HomeAddress );
            }

            var clinicalPayorCoverages = _clinicalPayorCoverageRepository.GetPayorCoverageListByPatientKey ( patient.Key );
            var newBillingPayorCoverages = _payorCoverageTranslator.Translate ( clinicalPayorCoverages );

            patientAccount.RevisePayorCoverages(newBillingPayorCoverages.ToList());

            return patientAccount;
        }
    }
}