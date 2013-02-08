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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Pillar.Common.Extension;
using Pillar.Domain;
using Pillar.Domain.NHibernate.Utility;

namespace Rem.Infrastructure.Domain
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.KeywordsSearchService">KeywordsSearchService </see> continas common utilties for key word search.
    /// </summary>
    public class KeywordsSearchService : IKeywordsSearchService
    {
        private readonly IPagedEntityListQueryService _pagedEntitiesQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordsSearchService"/> class.
        /// </summary>
        /// <param name="pagedEntitiesQueryService">The paged entities query service.</param>
        public KeywordsSearchService (IPagedEntityListQueryService pagedEntitiesQueryService)
        {
            _pagedEntitiesQueryService = pagedEntitiesQueryService;
        }

        /// <summary>
        /// Finds the paged entity list by keywords.
        /// </summary>
        /// <typeparam name="TEnity">The type of the enity.</typeparam>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="propertiesToSearch">The properties to search.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="eagerLoadingAssociations">The eager loading associations.</param>
        /// <returns>
        /// A PagedEntityList&lt;TEntity&gt;
        /// </returns>
        public PagedEntityList<TEnity> FindPagedEntityListByKeywords<TEnity>(
            string searchCriteria,
            IList<Expression<Func<TEnity, object>>> propertiesToSearch,
            int pageIndex, 
            int pageSize,
            IList<Order> orders = null,
            IList<Expression<Func<TEnity, IEnumerable>>> eagerLoadingAssociations = null) 
            where TEnity : class, IEntity
        {
            var pagedEntityList = FindPagedEntityListByKeywords (
                typeof(TEnity), searchCriteria, propertiesToSearch, pageIndex, pageSize, orders, eagerLoadingAssociations);

            return pagedEntityList;
        }

        /// <summary>
        /// Finds the paged entity list by keywords.
        /// </summary>
        /// <typeparam name="TEnity">The type of the enity.</typeparam>
        /// <param name="enityType">Type of the enity.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="propertiesToSearch">The properties to search.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="eagerLoadingAssociations">The eager loading associations.</param>
        /// <returns>
        /// A PagedEntityList&lt;TEntity&gt;
        /// </returns>
        public PagedEntityList<TEnity> FindPagedEntityListByKeywords<TEnity>(
            Type enityType, 
            string searchCriteria,
            IList<Expression<Func<TEnity, object>>> propertiesToSearch,
            int pageIndex, 
            int pageSize,
            IList<Order> orders = null,
            IList<Expression<Func<TEnity, IEnumerable>>> eagerLoadingAssociations = null) 
            where TEnity : class, IEntity
        {
            if (propertiesToSearch == null || propertiesToSearch.Count == 0)
            {
                throw new ArgumentException(@"Properties to search should be specified.", "propertiesToSearch");
            }

            searchCriteria = (searchCriteria ?? string.Empty).Trim();

            var entitySearchCriterion = GetCriterion(propertiesToSearch, searchCriteria);

            var pagedEntityList = _pagedEntitiesQueryService.GetPagedEntityList (
                enityType, entitySearchCriterion, pageIndex, pageSize, orders, eagerLoadingAssociations );
            return pagedEntityList;
        }

        private static ICriterion GetCriterion<TEnity>(IList<Expression<Func<TEnity, object>>> propertiesToSearch, string searchCriteria)
        {
            var keywords = searchCriteria.SplitIntoDistinctWords().ToList();

            if (keywords.Count() == 0)
            {
                return RestrictionsUtil.AlwaysTrueCriterion();
            }

            var disjunction = Restrictions.Disjunction();

            foreach (var keyword in keywords)
            {
                var likeKeyword = keyword + "%";
                foreach (var property in propertiesToSearch)
                {
                    disjunction.Add(Restrictions.InsensitiveLike(Projections.Property(property ), likeKeyword));
                }
            }

            return disjunction;
        }
    }
}
