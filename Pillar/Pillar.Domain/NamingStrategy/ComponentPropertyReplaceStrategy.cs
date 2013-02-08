using System;

namespace Pillar.Domain.NamingStrategy
{
    /// <summary>
    /// Strategy for component property replace naming.
    /// </summary>
    public class ComponentPropertyReplaceStrategy : IComponentNamingStrategy
    {
        #region Public Methods

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
        /// <returns>The column name.</returns>
        public string GetColumnName (
            Type entityType,
            Type entityPropertyType,
            string entityPropertyName,
            Type valueObjectType,
            Type valueObjectPropertyType,
            string valueObjectPropertyName,
            bool shortNameIndicator = false )
        {
            var name = entityPropertyName;
            if ( name.EndsWith ( valueObjectType.Name ) )
            {
                name = name.Substring ( 0, name.Length - valueObjectType.Name.Length );
            }

            name += valueObjectPropertyName;

            return name;
        }

        #endregion
    }
}
