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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// ILocationRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Core.AgencyModule.Appointment">Appointment</see>.
    /// </summary>
    public interface ILocationRepository : IRepository<Location>
    {
        /// <summary>
        /// The GetLocationsByAgency method returns a list of Locations that fall under the jurisdiction of the Agency
        /// identified by the given Agency Key.
        /// </summary>
        /// <param name="agencyKey">The key for the referenced agency.</param>
        /// <returns>An IList of Locations</returns>
        IList<Location> GetLocationsByAgency ( long agencyKey );

        /// <summary>
        /// Finds the paged location list by keywords.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A PagedEntityList&lt;Location&gt;</returns>
        PagedEntityList<Location> FindPagedLocationListByKeywords(string searchCriteria, int pageIndex, int pageSize);
    }
}