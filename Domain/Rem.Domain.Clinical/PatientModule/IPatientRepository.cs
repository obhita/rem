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
using System.Collections.Generic;
using Pillar.Domain;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// IPatientRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Clinical.PatientModule.Patient">Patient</see>.
    /// </summary>
    public interface IPatientRepository : IRepository<Patient>
    {
        /// <summary>
        /// Finds the patients by advanced search.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="genderWellKnownName">Name of the gender well known.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="motherMaidenName">Name of the mother maiden.</param>
        /// <param name="identifierTypeWellKnownName">Name of the identifier type well known.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="addressLineOne">The address line one.</param>
        /// <param name="city">The city.</param>
        /// <param name="stateWellKnownName">Name of the state well known.</param>
        /// <param name="suffixName">Name of the suffix.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="uniqueIdentifier">The unique identifier.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A Tuple&lt;int, int, List&lt;Patient&gt;&gt;.</returns>
        Tuple<int, int, List<Patient>> FindPatientsByAdvancedSearch (
            string firstName,
            string middleName,
            string lastName,
            string genderWellKnownName,
            DateTime? birthDate,
            string motherMaidenName,
            string identifierTypeWellKnownName,
            string identifier,
            string addressLineOne,
            string city,
            string stateWellKnownName,
            string suffixName,
            string zipCode,
            string uniqueIdentifier,
            int pageIndex,
            int pageSize );

        /// <summary>
        /// Gets all medications by patient key.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A IList&lt;Medication&gt;</returns>
        IList<Medication> GetAllMedicationsByPatientKey ( long patientKey );
    }
}
