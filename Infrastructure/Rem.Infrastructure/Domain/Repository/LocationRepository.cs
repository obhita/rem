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
using NHibernate.Linq;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Core.AgencyModule.Location">Location</see>.
    /// </summary>
    public class LocationRepository : NHibernateRepositoryBase<Location>, ILocationRepository
    {
        private readonly IKeywordsSearchService _keywordsSearchService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        /// <param name="keywordsSearchService">The keywords search service.</param>
        public LocationRepository(ISessionProvider sessionProvider, IKeywordsSearchService keywordsSearchService)
            : base(sessionProvider)
        {
            _keywordsSearchService = keywordsSearchService;
        }

        /// <summary>
        /// Gets a Location by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A Location object.</returns>
        public Location GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Saves a Location.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A Location object.</returns>
        public Location MakePersistent ( Location entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes a Location.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( Location entity )
        {
            Helper.MakeTransient ( entity );
        }

        /// <summary>
        /// The GetLocationsByAgency method returns a list of Locations that fall under the jurisdiction of the Agency
        /// identified by the given Agency Key.
        /// </summary>
        /// <param name="agencyKey">The key for the referenced agency.</param>
        /// <returns>
        /// An IList of Locations
        /// </returns>
        public IList<Location> GetLocationsByAgency ( long agencyKey )
        {
            var locationList =
                from location in Session.Query<Location> ()
                where location.Agency.Key == agencyKey
                select location;

            return locationList.ToList ();
        }

        /// <summary>
        /// Finds the paged location list by keywords.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A PagedEntityList&lt;Location&gt;
        /// </returns>
        public PagedEntityList<Location> FindPagedLocationListByKeywords(string searchCriteria, int pageIndex, int pageSize)
        {
            IList<Expression<Func<Location, object>>> propertiesToSearch = new List<Expression<Func<Location, object>>>
                {
                    p =>p.LocationProfile.LocationName.Name,
                    p =>p.LocationProfile.LocationName.DisplayName
                };

            IList<Order> orders = new List<Order>
                {
                    Order.Asc ( Projections.Property<Location> ( p => p.LocationProfile.LocationName.Name ) )
                };

            var pagedLocationList = _keywordsSearchService.FindPagedEntityListByKeywords(searchCriteria, propertiesToSearch, pageIndex, pageSize, orders);

            return pagedLocationList;
        }
    }
}