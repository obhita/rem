using System;
using Pillar.Domain.Primitives;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientProfileEmailAddressNamingStrategy implements the value object naming strategy.
    /// </summary>
    public class PatientProfileEmailAddressNamingStrategy : EmailAddressTypeNamingStrategy
    {
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
        public override string GetColumnName(Type entityType, Type entityPropertyType, string entityPropertyName, Type valueObjectType, Type valueObjectPropertyType, string valueObjectPropertyName, bool shortNameIndicator = false)
        {
            string columnName = base.GetColumnName(entityType, 
                entityPropertyType, 
                entityPropertyName, 
                valueObjectType,
                valueObjectPropertyType, 
                valueObjectPropertyName, 
                shortNameIndicator);
            return columnName;
        }
    }
}