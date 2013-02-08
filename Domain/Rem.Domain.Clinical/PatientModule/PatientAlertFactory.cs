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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PatientAlertFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.PatientAlert">PatientAlert</see>.
    /// </summary>
    public class PatientAlertFactory : IPatientAlertFactory
    {
        private readonly IPatientAlertRepository _patientAlertRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAlertFactory"/> class.
        /// </summary>
        /// <param name="patientAlertRepository">The patient alert repository.</param>
        public PatientAlertFactory(IPatientAlertRepository patientAlertRepository)
        {
            _patientAlertRepository = patientAlertRepository;
        }

        /// <summary>
        /// Creates the alert.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="name">The name.</param>
        /// <param name="note">The note.</param>
        /// <param name="cdsIdentifier">The CDS identifier.</param>
        /// <returns>
        /// A PatientAlert.
        /// </returns>
        public PatientAlert CreateAlert ( Patient patient, string name, string note, string cdsIdentifier )
        {
            var patientAlert = new PatientAlert(patient, name, note, cdsIdentifier);
            _patientAlertRepository.MakePersistent(patientAlert);

            return patientAlert;
        }

        /// <summary>
        /// Destroys the alert.
        /// </summary>
        /// <param name="alert">The alert.</param>
        public void DestroyAlert ( PatientAlert alert )
        {
            Check.IsNotNull(alert, "Patient alert is required.");
            _patientAlertRepository.MakeTransient(alert);
        }
    }
}