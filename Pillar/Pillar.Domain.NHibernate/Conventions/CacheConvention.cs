using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Pillar.Common.Configuration;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// CacheConvention class.
    /// </summary>
    public class CacheConvention : IClassConvention
    {
        /// <summary>
        /// Cache Attribute Region
        /// </summary>
        public static readonly string CacheAttributeRegion = "CacheAttributeRegion";

        /// <summary>
        /// Lookup Region
        /// </summary>
        /// NHibernate cache provider region names
        public static readonly string LookupRegion = "LookupRegion";

        #region Public Methods

        /// <summary>
        /// Applies caching for instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IClassInstance instance )
        {
            if ( IsCacheable ( instance ) )
            {
                instance.Cache.ReadOnly ();
                instance.Cache.IncludeAll ();

                var region = CacheAttributeRegion;
                if ( typeof( ILookup ).IsAssignableFrom ( instance.EntityType ) )
                {
                    region = LookupRegion;
                }
                instance.Cache.Region ( region );
            }
        }

        #endregion

        #region Methods

        private bool IsCacheable ( IClassInstance instance )
        {
            var isCacheable = false;

            if ( typeof( ILookup ).IsAssignableFrom ( instance.EntityType )
                 || instance.EntityType.IsDefined ( typeof( CacheAttribute ), true ) )
            {
                isCacheable = true;
            }

            return isCacheable;
        }

        #endregion
    }
}
