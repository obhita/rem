using FluentNHibernate;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// ComponentNamingStrategyExtensions class.
    /// </summary>
    public static class ComponentNamingStrategyExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <param name="componentNamingStrategy">The component naming strategy.</param>
        /// <param name="entityMember">The entity member.</param>
        /// <param name="componentMember">The component member.</param>
        /// <param name="shortNameIndicator">If set to <c>true</c> [short name indicator].</param>
        /// <returns>The column name.</returns>
        public static string GetColumnName (
            this IComponentNamingStrategy componentNamingStrategy, Member entityMember, Member componentMember, bool shortNameIndicator )
        {
            return componentNamingStrategy.GetColumnName (
                entityMember.DeclaringType,
                entityMember.PropertyType,
                entityMember.Name,
                componentMember.DeclaringType,
                componentMember.PropertyType,
                componentMember.Name,
                shortNameIndicator );
        }

        #endregion
    }
}
