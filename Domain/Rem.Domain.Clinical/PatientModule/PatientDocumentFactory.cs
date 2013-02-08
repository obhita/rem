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

using Pillar.Common.Cryptography;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PatientDocumentFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.PatientDocument">PatientDocument</see>.
    /// </summary>
    public class PatientDocumentFactory : IPatientDocumentFactory
    {
        private readonly IPatientDocumentRepository _patientDocumentRepository;
        private readonly IHashingUtility _hashingUtility;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDocumentFactory"/> class.
        /// </summary>
        /// <param name="patientDocumentRepository">The patient document repository.</param>
        /// <param name="hashingUtility">The hashing utility.</param>
        public PatientDocumentFactory ( IPatientDocumentRepository patientDocumentRepository, IHashingUtility hashingUtility )
        {
            _patientDocumentRepository = patientDocumentRepository;
            _hashingUtility = hashingUtility;
        }

        #region IPatientDocumentFactory Members

        /// <summary>
        /// Creates the patient document.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="patientDocumentType">Type of the patient document.</param>
        /// <param name="document">The document.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// A PatientDocument.
        /// </returns>
        public PatientDocument CreatePatientDocument (
            Patient patient,
            PatientDocumentType patientDocumentType,
            byte[] document,
            string fileName )
        {
            var hash = _hashingUtility.ComputeHash ( document );

            var patientDocument = new PatientDocument(patient, patientDocumentType, document, fileName, hash);

            _patientDocumentRepository.MakePersistent ( patientDocument );

            return patientDocument;
        }

        /// <summary>
        /// Destroys the patient document.
        /// </summary>
        /// <param name="patientDocument">The patient document.</param>
        public void DestroyPatientDocument ( PatientDocument patientDocument )
        {
            _patientDocumentRepository.MakeTransient ( patientDocument );
        }

        #endregion
    }
}