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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// IPatientAccessEventRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Clinical.PatientModule.PatientAccessEvent">PatientAccessEvent</see>.
    /// </summary>
    public interface IPatientAccessEventRepository
    {
        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="patientAccessEventKey">The patient access event key.</param>
        /// <returns>A PatientAccessEvent.</returns>
        PatientAccessEvent GetByKey ( long patientAccessEventKey );

        /// <summary>
        /// Determines whether [is patient read access audited today] [the specified patient access event].
        /// </summary>
        /// <param name="patientAccessEvent">The patient access event.</param>
        /// <param name="systemAcountKey">The system acount key.</param>
        /// <returns>
        ///   <c>true</c> if [is patient read access audited today] [the specified patient access event]; otherwise, <c>false</c>.
        /// </returns>
        bool IsPatientReadAccessAuditedToday(PatientAccessEvent patientAccessEvent, long systemAcountKey);

        /// <summary>
        /// Makes the persistent.
        /// </summary>
        /// <param name="patientAccessEvent">The patient access event.</param>
        /// <returns>A PatientAccessEvent.</returns>
        PatientAccessEvent MakePersistent ( PatientAccessEvent patientAccessEvent );

        /// <summary>
        /// Finds the patient access events by search criteria.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <param name="userKey">The user key.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="accessType">Type of the access.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortingMemberName">Name of the sorting member.</param>
        /// <param name="secondOrderSortBy">The second order sort by.</param>
        /// <param name="secondOrderSortDirection">The second order sort direction.</param>
        /// <returns>A Tuple&lt;int, int, List&lt;PatientAccessEvent&gt;&gt;.</returns>
        Tuple<int, int, List<PatientAccessEvent>> FindPatientAccessEventsBySearchCriteria ( long? patientKey,
                                                                                            long? userKey,
                                                                                            DateTime? startDate,
                                                                                            DateTime? endDate,
                                                                                            string accessType,
                                                                                            int pageIndex,
                                                                                            int pageSize,
                                                                                            string sortingMemberName,
                                                                                            string secondOrderSortBy,
                                                                                            string secondOrderSortDirection);
    }
}