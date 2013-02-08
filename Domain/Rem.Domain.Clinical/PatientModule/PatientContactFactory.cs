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

using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PatientContactFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.PatientContact">PatientContact</see>.
    /// </summary>
    public class PatientContactFactory : IPatientContactFactory
    {
        private readonly IPatientContactRepository _patientContactRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContactFactory"/> class.
        /// </summary>
        /// <param name="patientContactRepository">The patient contact repository.</param>
        public PatientContactFactory ( IPatientContactRepository patientContactRepository )
        {
            _patientContactRepository = patientContactRepository;
        }

        #region IPatientContactFactory Members

        /// <summary>
        /// Creates the patient contact.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>
        /// A PatientContact.
        /// </returns>
        public PatientContact CreatePatientContact ( Patient patient, string firstName, string lastName )
        {
            var newPatientContact = new PatientContact ( patient, firstName, lastName );
            PatientContact createdPatientContact = null;

            DomainRuleEngine.CreateRuleEngine ( newPatientContact, "CreatePatientContactRuleSet" ).Execute (
                () =>
                    {
                        createdPatientContact = newPatientContact;

                        _patientContactRepository.MakePersistent ( newPatientContact );
                    } );

            return createdPatientContact;
        }

        /// <summary>
        /// Destroys the patient contact.
        /// </summary>
        /// <param name="patientContact">The patient contact.</param>
        public void DestroyPatientContact ( PatientContact patientContact )
        {
            Check.IsNotNull ( patientContact, "Patient contact is required." );
            _patientContactRepository.MakeTransient ( patientContact );
        }

        #endregion
    }
}