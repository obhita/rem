using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Pillar.Common.Utility;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// DetachedCriteriaExtensions class.
    /// </summary>
    public static class DetachedCriteriaExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds the orders.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="orders">The orders.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria AddOrders ( this DetachedCriteria criteria, IEnumerable<Order> orders )
        {
            foreach ( var order in orders )
            {
                criteria.AddOrder ( order );
            }

            return criteria;
        }

        /// <summary>
        /// Creates the criteria.
        /// </summary>
        /// <typeparam name="T">Type creating criteria for.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="joinType">Type of the join.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateCriteria<T, TProperty> (
            this DetachedCriteria criteria, Expression<Func<T, TProperty>> associationPropertyExpression, JoinType joinType )
        {
            var associationPath = PropertyUtil.ExtractPropertyName ( associationPropertyExpression );
            criteria.CreateCriteria ( associationPath, joinType );
            return criteria;
        }

        /// <summary>
        /// Creates the criteria.
        /// </summary>
        /// <typeparam name="T">Type creating criteria for.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="joinType">Type of the join.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateCriteria<T> (
            this DetachedCriteria criteria, Expression<Func<T, object>> associationPropertyExpression, JoinType joinType )
        {
            return CreateCriteria<T, object> ( criteria, associationPropertyExpression, joinType );
        }

        /// <summary>
        /// Sets the fetch mode.
        /// </summary>
        /// <typeparam name="T">Type setting fetch mode for.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="mode">The mode to set.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria SetFetchMode<T, TProperty> (
            this DetachedCriteria criteria, Expression<Func<T, TProperty>> associationPropertyExpression, FetchMode mode )
        {
            var associationPath = PropertyUtil.ExtractPropertyName ( associationPropertyExpression );
            criteria.SetFetchMode ( associationPath, mode );
            return criteria;
        }

        /// <summary>
        /// Sets the fetch mode.
        /// </summary>
        /// <typeparam name="T">Type setting fetch mode for.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="mode">The mode to set.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria SetFetchMode<T> (
            this DetachedCriteria criteria, Expression<Func<T, object>> associationPropertyExpression, FetchMode mode )
        {
            return SetFetchMode<T, object> ( criteria, associationPropertyExpression, mode );
        }

        #endregion
    }
}
