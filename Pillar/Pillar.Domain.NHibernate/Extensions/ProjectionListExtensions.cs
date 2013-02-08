using System;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Pillar.Common.Utility;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// ProjectionListExtensions class.
    /// </summary>
    public static class ProjectionListExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds the specified projection list.
        /// </summary>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="projectionList">The projection list.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>A <see cref="ProjectionList"/></returns>
        public static ProjectionList Add<TTo, TProperty> (
            this ProjectionList projectionList,
            IProjection projection,
            Expression<Func<TTo, TProperty>> alias )
        {
            return projectionList.Add ( projection, PropertyUtil.ExtractPropertyName ( alias ) );
        }

        /// <summary>
        /// Adds the specified projection list.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="projectionList">The projection list.</param>
        /// <param name="projectionExpression">The projection expression.</param>
        /// <param name="aliasExpression">The alias expression.</param>
        /// <returns>A <see cref="ProjectionList"/></returns>
        public static ProjectionList Add<TFrom, TTo, TProperty> (
            this ProjectionList projectionList,
            Expression<Func<TFrom, object>> projectionExpression,
            Expression<Func<TTo, TProperty>> aliasExpression )
        {
            var projection = Projections.Property ( projectionExpression );
            return projectionList.Add ( projection, PropertyUtil.ExtractPropertyName ( aliasExpression ) );
        }

        /// <summary>
        /// Adds the specified projection list.
        /// </summary>
        /// <typeparam name="TFrom">The type of from.</typeparam>
        /// <typeparam name="TTo">The type of to.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="projectionList">The projection list.</param>
        /// <param name="projectionExpression">The projection expression.</param>
        /// <param name="aliasExpression">The alias expression.</param>
        /// <returns>A <see cref="ProjectionList"/></returns>
        public static ProjectionList Add<TFrom, TTo, TProperty> (
            this ProjectionList projectionList,
            Expression<Func<TFrom, TProperty>> projectionExpression,
            Expression<Func<TTo, TProperty>> aliasExpression )
        {
            var projection =
                Projections.Property ( ExpressionUtil.AddBox ( projectionExpression ) );
            return projectionList.Add ( projection, PropertyUtil.ExtractPropertyName ( aliasExpression ) );
        }

        #endregion
    }
}
