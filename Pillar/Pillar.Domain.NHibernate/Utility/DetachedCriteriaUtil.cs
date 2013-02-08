using System;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Utility
{
    /// <summary>
    /// DetachedCriteriaUtil class.
    /// </summary>
    public static class DetachedCriteriaUtil
    {
        #region Public Methods

        /// <summary>
        /// Creates the detached criteria for child.
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <typeparam name="TChild">The type of the child.</typeparam>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateDetachedCriteriaForChild<TRoot, TChild> (
            ICriterion rootCriterion, Expression<Func<TRoot, TChild>> associationPropertyExpression )
        {
            var criteria = DetachedCriteria.For<TRoot> ();
            criteria.Add ( rootCriterion );
            criteria.CreateCriteria ( associationPropertyExpression, JoinType.LeftOuterJoin );
            return criteria;
        }

        /// <summary>
        /// Creates the detached criteria for child.
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateDetachedCriteriaForChild<TRoot> (
            ICriterion rootCriterion, Expression<Func<TRoot, object>> associationPropertyExpression )
        {
            return CreateDetachedCriteriaForChild<TRoot, object> ( rootCriterion, associationPropertyExpression );
        }

        /// <summary>
        /// Creates the detached criteria for child.
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <typeparam name="TChild">The type of the child.</typeparam>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="fetchMode">The fetch mode.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateDetachedCriteriaForChild<TRoot, TChild> (
            ICriterion rootCriterion, Expression<Func<TRoot, TChild>> associationPropertyExpression, FetchMode fetchMode )
        {
            var criteria = DetachedCriteria.For<TRoot> ();
            criteria.Add ( rootCriterion );
            criteria.SetFetchMode ( associationPropertyExpression, fetchMode );
            return criteria;
        }

        /// <summary>
        /// Creates the detached criteria for child.
        /// </summary>
        /// <typeparam name="TRoot">The type of the root.</typeparam>
        /// <param name="rootCriterion">The root criterion.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="fetchMode">The fetch mode.</param>
        /// <returns>A <see cref="DetachedCriteria"/></returns>
        public static DetachedCriteria CreateDetachedCriteriaForChild<TRoot> (
            ICriterion rootCriterion, Expression<Func<TRoot, object>> associationPropertyExpression, FetchMode fetchMode )
        {
            return CreateDetachedCriteriaForChild<TRoot, object> ( rootCriterion, associationPropertyExpression, fetchMode );
        }

        #endregion
    }
}
