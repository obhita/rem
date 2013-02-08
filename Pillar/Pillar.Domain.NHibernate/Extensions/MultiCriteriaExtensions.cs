using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Pillar.Domain.NHibernate.Utility;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// MultiCriteriaExtensions class.
    /// </summary>
    public static class MultiCriteriaExtensions
    {
        #region Public Methods

        /// <summary>
        /// To use multi criteria to eagerly load multiple child collection, if the first detached criteria has child collection to query,
        /// then must use .SetResultTransformer(new DistinctRootEntityResultTransformer() for the first detached criteria
        /// </summary>
        /// <param name="multiCriteria">The multi criteria.</param>
        /// <param name="detachedCriteria">The detached criteria.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="returnDistinctRoot">If set to <c>true</c> [return distinct root].</param>
        /// <returns>A <see cref="IMultiCriteria"/></returns>
        public static IMultiCriteria AddDetachedCriteria (
            this IMultiCriteria multiCriteria, DetachedCriteria detachedCriteria, IEnumerable<Order> orders = null, bool returnDistinctRoot = false )
        {
            if ( orders != null && orders.Count () > 0 )
            {
                detachedCriteria.AddOrders ( orders );
            }

            if ( returnDistinctRoot )
            {
                detachedCriteria = detachedCriteria.SetResultTransformer ( new DistinctRootEntityResultTransformer () );
            }

            multiCriteria.Add ( detachedCriteria );
            return multiCriteria;
        }

        /// <summary>
        /// To use multi criteria to eagerly load multiple child collection, if the first detached criteria has child collection to query,
        /// then must use .SetResultTransformer(new DistinctRootEntityResultTransformer() for the first detached criteria
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <typeparam name="TChild">The type of the child.</typeparam>
        /// <param name="multiCriteria">The multi criteria.</param>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="returnDistinctRoot">If set to <c>true</c> [return distinct root].</param>
        /// <returns>A <see cref="IMultiCriteria"/></returns>
        public static IMultiCriteria AddDetachedCriteriaForChild<TRoot, TChild> (
            this IMultiCriteria multiCriteria,
            ICriterion rootCriterion,
            Expression<Func<TRoot, TChild>> associationPropertyExpression,
            IEnumerable<Order> orders = null,
            bool returnDistinctRoot = false )
        {
            var detachedCriteria = DetachedCriteriaUtil.CreateDetachedCriteriaForChild (
                rootCriterion,
                associationPropertyExpression );
            return multiCriteria.AddDetachedCriteria ( detachedCriteria, orders, returnDistinctRoot );
        }

        /// <summary>
        /// To use multi criteria to eagerly load multiple child collection, if the first detached criteria has child collection to query,
        /// then must use .SetResultTransformer(new DistinctRootEntityResultTransformer() for the first detached criteria
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <param name="multiCriteria">The multi criteria.</param>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="returnDistinctRoot">If set to <c>true</c> [return distinct root].</param>
        /// <returns>A <see cref="IMultiCriteria"/></returns>
        public static IMultiCriteria AddDetachedCriteriaForChild<TRoot> (
            this IMultiCriteria multiCriteria,
            ICriterion rootCriterion,
            Expression<Func<TRoot, object>> associationPropertyExpression,
            IEnumerable<Order> orders = null,
            bool returnDistinctRoot = false )
        {
            var detachedCriteria = DetachedCriteriaUtil.CreateDetachedCriteriaForChild (
                rootCriterion,
                associationPropertyExpression );
            return multiCriteria.AddDetachedCriteria ( detachedCriteria, orders, returnDistinctRoot );
        }

        /// <summary>
        /// To use multi criteria to eagerly load multiple child collection, if the first detached criteria has child collection to query,
        /// then must use .SetResultTransformer(new DistinctRootEntityResultTransformer() for the first detached criteria
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <typeparam name="TChild">The type of the child.</typeparam>
        /// <param name="multiCriteria">The multi criteria.</param>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="fetchMode">The fetch mode.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="returnDistinctRoot">If set to <c>true</c> [return distinct root].</param>
        /// <returns>A <see cref="IMultiCriteria"/></returns>
        public static IMultiCriteria AddDetachedCriteriaForChild<TRoot, TChild> (
            this IMultiCriteria multiCriteria,
            ICriterion rootCriterion,
            Expression<Func<TRoot, TChild>> associationPropertyExpression,
            FetchMode fetchMode,
            IEnumerable<Order> orders = null,
            bool returnDistinctRoot = false )
        {
            var detachedCriteria = DetachedCriteriaUtil.CreateDetachedCriteriaForChild (
                rootCriterion,
                associationPropertyExpression,
                fetchMode );
            return multiCriteria.AddDetachedCriteria ( detachedCriteria, orders, returnDistinctRoot );
        }

        /// <summary>
        /// To use multi criteria to eagerly load multiple child collection, if the first detached criteria has child collection to query,
        /// then must use .SetResultTransformer(new DistinctRootEntityResultTransformer() for the first detached criteria
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <param name="multiCriteria">The multi criteria.</param>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="fetchMode">The fetch mode.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="returnDistinctRoot">If set to <c>true</c> [return distinct root].</param>
        /// <returns>A <see cref="IMultiCriteria"/></returns>
        public static IMultiCriteria AddDetachedCriteriaForChild<TRoot> (
            this IMultiCriteria multiCriteria,
            ICriterion rootCriterion,
            Expression<Func<TRoot, object>> associationPropertyExpression,
            FetchMode fetchMode,
            IEnumerable<Order> orders = null,
            bool returnDistinctRoot = false )
        {
            var detachedCriteria = DetachedCriteriaUtil.CreateDetachedCriteriaForChild (
                rootCriterion,
                associationPropertyExpression,
                fetchMode );
            return multiCriteria.AddDetachedCriteria ( detachedCriteria, orders, returnDistinctRoot );
        }

        #endregion
    }
}
