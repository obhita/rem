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
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;
using LinqExtensionMethods = NHibernate.Linq.LinqExtensionMethods;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Core.AgencyModule.Staff">Staff</see>.
    /// </summary>
    public class StaffRepository : NHibernateRepositoryBase<Staff>, IStaffRepository
    {
        private readonly IKeywordsSearchService _keywordsSearchService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        /// <param name="keywordsSearchService">The keywords search service.</param>
        public StaffRepository(ISessionProvider sessionProvider, IKeywordsSearchService keywordsSearchService)
            : base(sessionProvider)
        {
            _keywordsSearchService = keywordsSearchService;
        }

        #region IStaffRepository Members

        /// <summary>
        /// Gets the Staff by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A Staff object.</returns>
        public Staff GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Saves a Staff.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A Staff object.</returns>
        public Staff MakePersistent(Staff entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Deletes a Staff.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient(Staff entity)
        {
            Helper.MakeTransient(entity);
        }

        
        /// <summary>
        /// Gets the name of all staff by location key and staff type well known.
        /// </summary>
        /// <param name="locationKey">The location key.</param>
        /// <param name="staffTypeWellKnownNames">The staff type well known names.</param>
        /// <returns>
        /// An IList&lt;Staff&gt;
        /// </returns>
        public IList<Staff> GetAllStaffByLocationKeyAndStaffTypeWellKnownName(long locationKey, params string[] staffTypeWellKnownNames)
        {
            //TODO: This query needs be revisited after the AgencyModule update is settled down
            // Probably it seems that there is no such thing of Staff search by location. Instead should search by Agency

            // TODO: research alternative Linq approaches that won't use a correlated sub-query
            var results = from staff in LinqExtensionMethods.Query<Staff>(Session)
                          where
                              staffTypeWellKnownNames.Contains(staff.StaffProfile.StaffType.WellKnownName)
                              && staff.Agency.Locations.Any(l => l.Key == locationKey)
                          select staff;

            return results.ToList();
        }

        /// <summary>
        /// Gets all staff by agency key.
        /// </summary>
        /// <param name="agencyKey">The agency key.</param>
        /// <returns>
        /// An IList&lt;Staff&gt;
        /// </returns>
        public IList<Staff> GetAllStaffByAgencyKey(long agencyKey)
        {
            var staffQuery = from staff in LinqExtensionMethods.Query<Staff>(Session) where staff.Agency.Key == agencyKey select staff;

            return staffQuery.ToList();
        }

        /// <summary>
        /// Finds the paged staff list by keywords.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A PagedEntityList&lt;Staff&gt;
        /// </returns>
        public PagedEntityList<Staff> FindPagedStaffListByKeywords(string searchCriteria, int pageIndex, int pageSize)
        {
            IList<Expression<Func<Staff, object>>> propertiesToSearch = new List<Expression<Func<Staff, object>>>
                {
                    p =>p.StaffProfile.StaffName.First,
                    p =>p.StaffProfile.StaffName.Middle,
                    p =>p.StaffProfile.StaffName.Last
                };

            IList<Order> orders = new List<Order>
                {
                    Order.Asc ( Projections.Property<Staff> ( p => p.StaffProfile.StaffName.First ) ),
                    Order.Asc ( Projections.Property<Staff> ( p => p.StaffProfile.StaffName.Last ) )
                };

            var pagedStaffList = _keywordsSearchService.FindPagedEntityListByKeywords(searchCriteria, propertiesToSearch, pageIndex, pageSize, orders);

            return pagedStaffList;
        }

        /// <summary>
        /// Finds the duplicate staff.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleInitial">The middle initial.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>
        /// A Staff object.
        /// </returns>
        public Staff FindDuplicateStaff(string firstName, string middleInitial, string lastName)
        {
            var result = from staff in LinqExtensionMethods.Query<Staff>(Session)
                         where
                             staff.StaffProfile.StaffName.First == firstName
                             && staff.StaffProfile.StaffName.Last == lastName
                         select staff;

            if (!string.IsNullOrEmpty(middleInitial))
            {
                result = result.Where(s => s.StaffProfile.StaffName.Middle.StartsWith(middleInitial));
            }

            return result.FirstOrDefault();
        }

        #endregion
    }
}
