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

using System.Collections.Generic;
using Pillar.Domain;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// IClinicalCaseRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Clinical.ClinicalCaseModule.ClinicalCase">ClinicalCase</see>.
    /// </summary>
    public interface IClinicalCaseRepository : IRepository<ClinicalCase>
    {
        /// <summary>
        /// Gets the most recent case number.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A long.</returns>
        long GetMostRecentCaseNumber ( long patientKey );

        /// <summary>
        /// Gets all clinical cases by patient key.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>An IEnumerable&lt;ClinicalCase&gt;</returns>
        IEnumerable<ClinicalCase> GetAllClinicalCasesByPatientKey ( long patientKey );

        /// <summary>
        /// Gets all associated problem by clinical case key.
        /// </summary>
        /// <param name="clinicalCaseKey">The clinical case key.</param>
        /// <returns>An IList&lt;Problem&gt;.</returns>
        IList<Problem> GetAllAssociatedProblemByClinicalCaseKey ( long clinicalCaseKey );

        /// <summary>
        /// Gets all not associated problems by clinical case key.
        /// </summary>
        /// <param name="clinicalCaseKey">The clinical case key.</param>
        /// <returns>An IList&lt;Problem&gt;.</returns>
        IList<Problem> GetAllNotAssociatedProblemsByClinicalCaseKey ( long clinicalCaseKey );

        /// <summary>
        /// Gets the active clinical case by patient.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A ClinicalCase.</returns>
        ClinicalCase GetActiveClinicalCaseByPatient ( long patientKey );

        /// <summary>
        /// Gets the most recent closed clinical case by patient.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A ClinicalCase.</returns>
        ClinicalCase GetMostRecentClosedClinicalCaseByPatient(long patientKey);
    }
}