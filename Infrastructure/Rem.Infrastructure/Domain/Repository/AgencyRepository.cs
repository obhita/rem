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
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Defines repository services for the <see cref="T:Rem.Domain.Core.AgencyModule.Agency">Agency</see>.
    /// </summary>
    public class AgencyRepository : NHibernateRepositoryBase<Agency>, IAgencyRepository
    {
        private readonly IKeywordsSearchService _keywordsSearchService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        /// <param name="keywordsSearchService">The keywords search service.</param>
        public AgencyRepository(ISessionProvider sessionProvider, IKeywordsSearchService keywordsSearchService)
            : base(sessionProvider)
        {
            _keywordsSearchService = keywordsSearchService;
        }

        #region IRepository<Agency> Members

        /// <summary>
        /// Gets an agency the by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>An agency object.</returns>
        public Agency GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        /// Saves an agency.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>An agency object.</returns>
        public Agency MakePersistent ( Agency entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        /// Deletes the agency.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient ( Agency entity )
        {
            Helper.MakeTransient ( entity );
        }

        /// <summary>
        /// Gets all agencies.
        /// </summary>
        /// <returns>
        /// An IEnumerable&lt;Agency&gt;
        /// </returns>
        public IEnumerable<Agency> GetAllAgencies()
        {
            var agencies = Session.Query<Agency> ();
            return agencies.ToList ();
        }

        /// <summary>
        /// Gets an agency by the legal name.
        /// </summary>
        /// <param name="legalName">The legal name.</param>
        /// <returns>
        /// An Agency object.
        /// </returns>
        public Agency GetAgencyByLegalName ( string legalName )
        {
            var agencys = Session.CreateCriteria<Agency> ()
                .Add ( Restrictions.Like ( "AgencyProfile.AgencyName.LegalName", legalName ) )
                .SetMaxResults ( 1 )
                .List<Agency> ();

            if ( agencys == null || agencys.Count == 0 )
            {
                return null;
            }

            Agency agency = agencys.First ();

            return agency;
        }

        /// <summary>
        /// Gets the locations by agency.
        /// </summary>
        /// <param name="agencyKey">The agency key.</param>
        /// <returns>An agency object.</returns>
        public IList<Agency> GetLocationsByAgency ( long agencyKey )
        {
            string query = "from org in Agency where org.ParentAgency.Key = " + agencyKey;
            IQuery organziationQuery = Session.CreateQuery ( query );

            IList<Agency> agencyList = organziationQuery.List<Agency> ();

            return agencyList;
        }

        /// <summary>
        /// Gets the locations by patient key.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>An IList&lt;Agency&gt;.</returns>
        public IList<Agency> GetLocationsByPatientKey ( long patientKey )
        {
            string sql =
                @"from Agency org
                    where org.ParentAgency.Key = 
                    (
                    select p.Agency.Key
                    from Patient p	
                    where p.Key = " +
                patientKey + " )";

            IQuery organziationQuery = Session.CreateQuery ( sql );

            IList<Agency> agencyList = organziationQuery.List<Agency> ();

            return agencyList;
        }

        /// <summary>
        /// Finds the paged agencies by keywords.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A PagedEntityList&lt;Agency&gt;
        /// </returns>
        public PagedEntityList<Agency> FindPagedAgencyListByKeywords(string searchCriteria, int pageIndex, int pageSize)
        {
            IList<Expression<Func<Agency, object>>> propertiesToSearch = new List<Expression<Func<Agency, object>>>
                {
                    p =>p.AgencyProfile.AgencyName.LegalName,
                    p =>p.AgencyProfile.AgencyName.DisplayName
                };

            IList<Order> orders = new List<Order>
                {
                    Order.Asc ( Projections.Property<Agency> ( p => p.AgencyProfile.AgencyName.LegalName ) )
                };

            var pagedAgencyList = _keywordsSearchService.FindPagedEntityListByKeywords(searchCriteria, propertiesToSearch, pageIndex, pageSize, orders);

            return pagedAgencyList;
        }

        #endregion
    }
}