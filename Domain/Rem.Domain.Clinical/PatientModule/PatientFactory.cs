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
using Pillar.Domain;
using Pillar.Domain.Event;
using Rem.Domain.Clinical.PatientModule.Event;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PatientFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.Patient">Patient</see>.
    /// </summary>
    public class PatientFactory : IPatientFactory
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientUniqueIdentifierGenerator _patientUniqueIdentifierCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientFactory"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="patientUniqueIdentifierCalculator">The patient unique identifier calculator.</param>
        public PatientFactory (
            IPatientRepository patientRepository, IPatientUniqueIdentifierGenerator patientUniqueIdentifierCalculator )
        {
            _patientRepository = patientRepository;
            _patientUniqueIdentifierCalculator = patientUniqueIdentifierCalculator;
        }

        #region IPatientFactory Members

        /// <summary>
        /// Creates the patient.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="patientName">Name of the patient.</param>
        /// <param name="patientProfile">The patient profile.</param>
        /// <returns>
        /// A Patient.
        /// </returns>
        public Patient CreatePatient(Agency agency, PersonName patientName, PatientProfile patientProfile )
        {
            var newPatient = new Patient(agency, patientName, patientProfile);
            Patient createdPatient = null;

            DomainRuleEngine.CreateRuleEngine ( newPatient, "CreatePatientRuleSet" )
                .WithContext ( newPatient.Profile )
                .Execute(() =>
                    {
                        createdPatient = newPatient;

                        newPatient.UpdateUniqueIdentifier(_patientUniqueIdentifierCalculator.GenerateUniqueIdentifier(newPatient));

                        _patientRepository.MakePersistent ( newPatient );

                        DomainEvent.Raise ( new PatientCreatedEvent { Patient = newPatient } );
                });

            return createdPatient;
        }

        #endregion


        /// <summary>
        /// Destroys the patient.
        /// </summary>
        /// <param name="patient">The patient.</param>
        public void DestroyPatient(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
