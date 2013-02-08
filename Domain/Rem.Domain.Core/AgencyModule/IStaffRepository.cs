// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStaffRepository.cs" company="">
//   
// </copyright>
// <summary>
//   IStaffRepository interface defines basic repository services for the Staff.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// IStaffRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Core.AgencyModule.Staff">Staff</see>.
    /// </summary>
    public interface IStaffRepository : IRepository<Staff>
    {
        // TODO: We should not have a method that returns a list of staff for the agency.  this method will not perform
        // well when we deploy.  We should deprecate this method asap.

        /// <summary>
        /// Gets all staff by agency key.
        /// </summary>
        /// <param name="agencyKey">
        /// The agency key.
        /// </param>
        /// <returns>
        /// An IList&lt;Staff&gt;
        /// </returns>
        IList<Staff> GetAllStaffByAgencyKey ( long agencyKey );

        /// <summary>
        /// Gets the name of all staff by location key and staff type well known.
        /// </summary>
        /// <param name="locationKey">
        /// The location key.
        /// </param>
        /// <param name="staffTypeWellKnownNames">
        /// The staff type well known names.
        /// </param>
        /// <returns>
        /// An IList&lt;Staff&gt;
        /// </returns>
        IList<Staff> GetAllStaffByLocationKeyAndStaffTypeWellKnownName ( long locationKey, params string[] staffTypeWellKnownNames );


        /// <summary>
        /// Finds the paged staff list by keywords.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A PagedEntityList&lt;Staff&gt;</returns>
        PagedEntityList<Staff> FindPagedStaffListByKeywords(string searchCriteria, int pageIndex, int pageSize);

        /// <summary>
        /// Finds the duplicate staff.
        /// </summary>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="middleInitial">
        /// The middle initial.
        /// </param>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        /// <returns>
        /// A Staff.
        /// </returns>
        Staff FindDuplicateStaff ( string firstName, string middleInitial, string lastName );
    }
}
