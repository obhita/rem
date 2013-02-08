using System;
using System.Linq.Expressions;
using NHibernate;
using Pillar.Common.Utility;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// CriteriaExtensions class.
    /// </summary>
    public static class CriteriaExtensions
    {
        #region Public Methods

        /// <summary>
        /// Sets the fetch mode.
        /// </summary>
        /// <typeparam name="T">Type setting fetch mode for.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <param name="associationPropertyExpression">The association property expression.</param>
        /// <param name="mode">The mode to set.</param>
        /// <returns>A <see cref="ICriteria"/></returns>
        public static ICriteria SetFetchMode<T, TProperty> (
            this ICriteria criteria, Expression<Func<T, TProperty>> associationPropertyExpression, FetchMode mode )
        {
            var associationPath = PropertyUtil.ExtractPropertyName ( associationPropertyExpression );
            criteria.SetFetchMode ( associationPath, mode );
            return criteria;
        }

        #endregion
    }
}
