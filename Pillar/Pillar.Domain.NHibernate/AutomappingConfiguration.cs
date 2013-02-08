using System;
using FluentNHibernate;
using FluentNHibernate.Automapping;

namespace Pillar.Domain.NHibernate
{
    /// <summary>
    /// AutomappingConfiguration class.
    /// </summary>
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        #region Public Methods

        /// <summary>
        /// Checks to see if class is layer supertype.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if is supertype; otherwise false.</returns>
        public override bool AbstractClassIsLayerSupertype ( Type type )
        {
            var attributes = type.GetCustomAttributes (
                typeof(NotLayerSupertypeAttribute), false);

            return attributes.Length <= 0 && base.AbstractClassIsLayerSupertype ( type );
        }

        /// <summary>
        /// Determines whether the specified type is component.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if the specified type is component; otherwise, <c>false</c>.</returns>
        public override bool IsComponent ( Type type )
        {
            return type.IsNHibernateComponent ();
        }

        /// <summary>
        /// Determines whether the specified member is id.
        /// </summary>
        /// <param name="member">The member to check.</param>
        /// <returns><c>true</c> if the specified member is id; otherwise, <c>false</c>.</returns>
        public override bool IsId ( Member member )
        {
            var declaringType = member.MemberInfo.DeclaringType;
            if (typeof(Entity).IsAssignableFrom(declaringType))
            {
                return member.Name == "Key";
            }

            return false;
        }

        /// <summary>
        /// Determins whether should map type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if should map type; otherwise false.</returns>
        public override bool ShouldMap ( Type type )
        {
            var baseShouldMap = base.ShouldMap ( type );
            var isEntity = typeof( IEntity ).IsAssignableFrom ( type );
            var isAggregateNode = typeof( IAggregateNode ).IsAssignableFrom ( type );

            var ignoreMappingTypeAttributes = type.GetCustomAttributes ( typeof( IgnoreMappingAttribute ), false );

            var result = baseShouldMap &&
                          ( isEntity || isAggregateNode ) &&
                          !type.IsInterface &&
                          ignoreMappingTypeAttributes.Length == 0;

            return result;
        }

        /// <summary>
        /// Determins whether should map member.
        /// </summary>
        /// <param name="member">The member to check.</param>
        /// <returns>True if should map member; otherwise false.</returns>
        public override bool ShouldMap ( Member member )
        {
            var ignoreMappingMemberAttributes = member.MemberInfo.GetCustomAttributes (
                typeof( IgnoreMappingAttribute ), false );
            var ignoreMappingMemberTypeAttributes = member.PropertyType.GetCustomAttributes (
                typeof( IgnoreMappingAttribute ), false );

            var shouldMap = base.ShouldMap ( member ) &&
                             ignoreMappingMemberTypeAttributes.Length == 0 &&
                             ignoreMappingMemberAttributes.Length == 0;

            return shouldMap;
        }

        #endregion
    }
}
