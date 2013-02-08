﻿#region License
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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PatientAccessEventFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.PatientAccessEvent">PatientAccessEvent</see>.
    /// </summary>
    public class PatientAccessEventFactory : IPatientAccessEventFactory
    {
        #region IPatientAccessEventFactory Members

        /// <summary>
        /// Creates the patient access event.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="patientAccessEventType">Type of the patient access event.</param>
        /// <param name="auditedContextDescription">The audited context description.</param>
        /// <param name="note">The note.</param>
        /// <returns>
        /// A PatientAccessEvent.
        /// </returns>
        public PatientAccessEvent CreatePatientAccessEvent (
            Patient patient,
            PatientAccessEventType patientAccessEventType,
            string auditedContextDescription,
            string note )
        {
            var patientAccessEvent = new PatientAccessEvent(patient, patientAccessEventType, auditedContextDescription, note);

            return patientAccessEvent;
        }

        #endregion
    }
}