using System;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule.NamingStrategy
{
    /// <summary>
    /// DensAsiNonResponseTypeNamingStrategy implements the value object naming strategy.
    /// </summary>
    public class DensAsiNonResponseTypeNamingStrategy : IComponentNamingStrategy
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
        /// <returns>A string with the column name.</returns>
        public string GetColumnName(Type entityType, Type entityPropertyType, string entityPropertyName, Type valueObjectType, Type valueObjectPropertyType, string valueObjectPropertyName, bool shortNameIndicator = false)
        {
            string name = entityPropertyName;

            // TODO: find a way to not use string property name here
            if ( valueObjectPropertyName != "Value" && !shortNameIndicator)
            {
                name += valueObjectPropertyName;
            }
            return name;
        }

        #endregion
    }
}