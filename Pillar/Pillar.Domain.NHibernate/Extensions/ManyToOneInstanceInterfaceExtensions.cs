using System;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// ManyToOneInstanceInterfaceExtensions class.
    /// </summary>
    public static class ManyToOneInstanceInterfaceExtensions
    {
        #region Public Methods

        /// <summary>
        /// The first item in the Tuple is SourceName, the second item is TargetName.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The reference name.</returns>
        public static string GetReferenceName ( this IManyToOneInstance instance )
        {
            var sourceEntityType = instance.EntityType;
            var targetEntityType = instance.Class.GetUnderlyingSystemType ();
            var propertyName = instance.Property.Name;

            var sourceEntityName = sourceEntityType.Name;
            if ( typeof( ILookup ).IsAssignableFrom ( sourceEntityType ) )
            {
                sourceEntityName += "Lkp";
            }

            var targetEntityName = targetEntityType.Name;
            if ( typeof( ILookup ).IsAssignableFrom ( targetEntityType ) )
            {
                targetEntityName += "Lkp";
            }

            var referenceName = string.Format ( "{0}_{1}_{2}", sourceEntityName, targetEntityName, propertyName );
            if ( targetEntityName == propertyName || targetEntityName == propertyName + "Lkp" )
            {
                referenceName = string.Format ( "{0}_{1}", sourceEntityName, targetEntityName );
            }

            return referenceName;
        }

        #endregion
    }
}
