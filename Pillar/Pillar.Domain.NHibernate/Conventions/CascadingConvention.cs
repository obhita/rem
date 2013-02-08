using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// CascadingConvention class.
    /// </summary>
    public class CascadingConvention : IHasManyConvention, IReferenceConvention, IHasOneConvention
    {
        #region Public Methods

        /// <summary>
        /// Sets cascade mode to AllDeleteOrphan for all types except Aggregate roots.
        /// Aggregate roots cascade mode is None.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IOneToManyCollectionInstance instance )
        {
            var rootSubclass =
                typeof( AbstractAggregateRoot ).IsAssignableFrom (
                    instance.Relationship.Class.GetUnderlyingSystemType () );

            if ( rootSubclass )
            {
                instance.Cascade.None ();
            }
            else
            {
                instance.Cascade.AllDeleteOrphan ();
            }
        }

        /// <summary>
        /// Sets cascade mode to AllDeleteOrphan for all types except Aggregate roots and instances with <see cref="NoneCascadingAttribute"/>.
        /// Aggregate roots cascade mode is None.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IManyToOneInstance instance )
        {
            var rootSubClass = typeof( AbstractAggregateRoot ).IsAssignableFrom ( instance.Property.PropertyType );

            if ( rootSubClass )
            {
                instance.Cascade.None ();
            }
            else
            {
                var noneCascadingAttributeAttributes = instance.Property.MemberInfo.GetCustomAttributes (
                    typeof( NoneCascadingAttribute ), false );
                if ( noneCascadingAttributeAttributes.Length == 1 )
                {
                    instance.Cascade.None ();
                }
                else
                {
                    instance.Cascade.SaveUpdate ();
                }
            }
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IOneToOneInstance instance )
        {
        }

        #endregion
    }
}
