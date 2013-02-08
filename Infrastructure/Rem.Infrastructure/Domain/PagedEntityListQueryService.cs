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
using Pillar.Domain;
using Pillar.Domain.NHibernate.Extensions;

namespace Rem.Infrastructure.Domain
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.PagedEntityListQueryService">PagedEntityListQueryService </see> contains common service for paged entity list queries.
    /// </summary>
    public class PagedEntityListQueryService : IPagedEntityListQueryService
    {
        private readonly ISessionProvider _sessionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedEntityListQueryService"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public PagedEntityListQueryService(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        /// <summary>
        /// Gets the paged entity list.
        /// </summary>
        /// <typeparam name="TEnity">The type of the enity.</typeparam>
        /// <param name="entityCriterion">The entity criterion.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="eagerLoadingAssociations">The eager loading associations.</param>
        /// <returns>
        /// A PagedEntityList&lt;TEntity&gt;
        /// </returns>
        public PagedEntityList<TEnity> GetPagedEntityList<TEnity>(
            ICriterion entityCriterion,
            int pageIndex,
            int pageSize,
            IList<Order> orders = null,
            IList<Expression<Func<TEnity, IEnumerable>>> eagerLoadingAssociations = null)
            where TEnity : class, IEntity
        {
            var pagedEntityList = GetPagedEntityList(typeof(TEnity), entityCriterion, pageIndex, pageSize, orders, eagerLoadingAssociations);
            return pagedEntityList;
        }

        /// <summary>
        /// Gets the paged entity list.
        /// </summary>
        /// <typeparam name="TEnity">The type of the enity.</typeparam>
        /// <param name="enityType">Type of the enity.</param>
        /// <param name="entityCriterion">The entity criterion.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="eagerLoadingAssociations">The eager loading associations.</param>
        /// <returns>
        /// A PagedEntityList&lt;TEntity&gt;
        /// </returns>
        public PagedEntityList<TEnity> GetPagedEntityList<TEnity>(
            Type enityType,
            ICriterion entityCriterion,
            int pageIndex,
            int pageSize,
            IList<Order> orders = null,
            IList<Expression<Func<TEnity, IEnumerable>>> eagerLoadingAssociations = null)
            where TEnity : class, IEntity
        {
            if (enityType == null)
            {
                throw new ArgumentException(@"Enity Type should not be null.", "enityType");
            }

            if (!typeof(TEnity).IsAssignableFrom(enityType))
            {
                throw new ArgumentException(@"TEnity should be assignable from enityType.");
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException(@"Invalid page index.", "pageIndex");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException(@"Invalide page size.", "pageSize");
            }

            orders = orders ?? new List<Order>();

            var multiCriteria = _sessionProvider.GetSession().CreateMultiCriteria();

            var entitiesCriteria = DetachedCriteria.For(enityType);
            entitiesCriteria.Add(entityCriterion);
            foreach (var order in orders)
            {
                entitiesCriteria.AddOrder(order);
            }
            multiCriteria.Add(entitiesCriteria);

            var totalEntityCountCriteria = DetachedCriteria.For(enityType);
            totalEntityCountCriteria.SetProjection(Projections.RowCount());
            totalEntityCountCriteria.Add(entityCriterion);
            multiCriteria.Add(totalEntityCountCriteria);

            if (eagerLoadingAssociations != null)
            {
                foreach (var eagerLoadingAssociation in eagerLoadingAssociations)
                {
                    multiCriteria.AddDetachedCriteriaForChild(
                        entityCriterion,
                        eagerLoadingAssociation,
                        orders,
                        true);
                }
            }

            int skippedEntityCount;
            var totalEntityCount = 0;
            var entities = new List<TEnity>();

            // If has data, guarantee that the last page will be returned if no page for the inputted page index will be returned.
            do
            {
                if (totalEntityCount > 0)
                {
                    // Get the last page index
                    pageIndex = (int)Math.Ceiling(totalEntityCount / ((decimal)pageSize)) - 1;
                }

                skippedEntityCount = pageIndex * pageSize;

                entitiesCriteria.SetFirstResult(skippedEntityCount).SetMaxResults(pageSize);

                var multiResults = multiCriteria.List();

                // Entity list is the 1st result set
                var entityList = (IList)multiResults[0];
                entities.Clear();
                entities.AddRange((from object entity in entityList select entity as TEnity));

                // Total entity count is the 2nd result set
                var counts = (IList)multiResults[1];
                totalEntityCount = (int)counts[0];
            } 
            while (skippedEntityCount >= totalEntityCount && totalEntityCount > 0);

            if (totalEntityCount == 0)
            {
                pageIndex = 0;
            }

            var pagedEntityList = new PagedEntityList<TEnity> ( totalEntityCount, pageIndex, pageSize, entities );

            return pagedEntityList;
        }
    }
}
