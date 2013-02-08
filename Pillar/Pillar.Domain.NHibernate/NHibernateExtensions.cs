using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;

namespace Pillar.Domain.NHibernate
{
    /// <summary>
    /// NHibernateExtensions class.
    /// </summary>
    public static class NHibernateExtensions
    {
        #region Public Methods

        /// <summary>
        /// Determines whether the specified type is a Nhibernate component.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if the specified type is a Nhibernate component; otherwise, <c>false</c>.</returns>
        public static bool IsNHibernateComponent ( this Type type )
        {
            var nhibernateComponentAttributes = type.GetCustomAttributes ( typeof( ComponentAttribute ), false );
            var result = nhibernateComponentAttributes.Length == 1;
            return result;
        }

        /// <summary>
        /// Gets paged results.
        /// </summary>
        /// <typeparam name="T">The type of entity to get.</typeparam>
        /// <param name="session">The session.</param>
        /// <param name="searchCriterion">The search criterion.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="order">The order.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>The list of entities in the page.</returns>
        public static IList<T> GetPagedResults<T>(this ISession session, ICriterion searchCriterion, int pageIndex, int pageSize, Order order, out int totalCount)
            where T : IAggregateRoot
        {
            var rowCount = session.CreateCriteria(typeof(T))
            .Add(searchCriterion)
            .SetProjection(Projections.RowCount()).FutureValue<int>();

            var criteria = session.CreateCriteria(typeof(T))
                .Add(searchCriterion);

            if(order != null)
            {
                criteria
                    .AddOrder ( order );
            }

            var results = criteria
                .SetFirstResult(pageIndex * pageSize)
                .SetMaxResults(pageSize)
                .Future<T>();

            totalCount = rowCount.Value;
            return results.ToList();
        }

        #endregion
    }
}
