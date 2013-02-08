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

using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The MedicationFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.Medication">Medication</see>.
    /// </summary>
    public class MedicationFactory : IMedicationFactory
    {
        private readonly IMedicationRepository _medicationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationFactory"/> class.
        /// </summary>
        /// <param name="medicationRepository">The medication repository.</param>
        public MedicationFactory ( IMedicationRepository medicationRepository )
        {
            _medicationRepository = medicationRepository;
        }

        #region IMedicationFactory Members

        /// <summary>
        /// Creates the medication.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="medicationCodeCodedConcept">The medication code coded concept.</param>
        /// <param name="rootMedicationCodedConcept">The root medication coded concept.</param>
        /// <returns>
        /// A Medication.
        /// </returns>
        public Medication CreateMedication(Patient patient, CodedConcept medicationCodeCodedConcept, CodedConcept rootMedicationCodedConcept )
        {
            var medication = new Medication ( patient, medicationCodeCodedConcept, rootMedicationCodedConcept );

            _medicationRepository.MakePersistent ( medication );

            return medication;
        }

        /// <summary>
        /// Creates the medication.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="medicationCodeCodedConcept">The medication code coded concept.</param>
        /// <param name="provenance">The provenance.</param>
        /// <returns>A Medication.</returns>
        public Medication CreateMedication(Patient patient, CodedConcept medicationCodeCodedConcept,  Provenance provenance)
        {
            var medication = new Medication(patient, medicationCodeCodedConcept, provenance);

            _medicationRepository.MakePersistent(medication);

            return medication;
        }

        /// <summary>
        /// Destroys the medication.
        /// </summary>
        /// <param name="medication">The medication.</param>
        public void DestroyMedication ( Medication medication )
        {
            _medicationRepository.MakeTransient ( medication );
        }

        #endregion
    }
}