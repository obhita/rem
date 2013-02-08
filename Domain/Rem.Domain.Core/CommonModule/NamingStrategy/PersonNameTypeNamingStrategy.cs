using System;
using Pillar.Domain;

namespace Rem.Domain.Core.CommonModule.NamingStrategy
{
    /// <summary>
    /// PersonNameTypeNamingStrategy implements the value object naming strategy.
    /// </summary>
    public class PersonNameTypeNamingStrategy : IComponentNamingStrategy
    {
        #region IComponentNamingStrategy Members

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityPropertyType">Type of the entity property.</param>
        /// <param name="entityPropertyName">Name of the entity property.</param>
        /// <param name="valueObjectType">Type of the value object.</param>
        /// <param name="valueObjectPropertyType">Type of the value object property.</param>
        /// <param name="valueObjectPropertyName">Name of the value object property.</param>
        /// <param name="shortNameIndicator">If set to <c>true</c> [short name indicator].</param>
        /// <returns>A string containing the column name.</returns>
        public string GetColumnName(Type entityType,
            Type entityPropertyType,
            string entityPropertyName,
            Type valueObjectType,
            Type valueObjectPropertyType,
            string valueObjectPropertyName,
            bool shortNameIndicator = false)
        {
            string columnName = valueObjectPropertyName + "Name";
            return columnName;
        }

        #endregion
    }
}
