using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// ForeignKeyColumnNameConvention class.
    /// </summary>
    public class ForeignKeyColumnNameConvention : IReferenceConvention, IReferenceConventionAcceptance, IComponentConvention, IHasManyConvention
    {
        #region Public Methods

        /// <summary>
        /// Accepts the specified criteria if is component.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void Accept ( IAcceptanceCriteria<IManyToOneInspector> criteria )
        {
            criteria.Expect ( c => ! c.EntityType.IsNHibernateComponent () );
        }

        /// <summary>
        /// Applies Foriegn key column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IManyToOneInstance instance )
        {
            // name the key field
            var key = instance.Property.Name;

            if ( typeof( ILookup ).IsAssignableFrom ( instance.Property.PropertyType ) )
            {
                key = key + "Lkp";
            }

            instance.Column ( key + "Key" );
        }

        /// <summary>
        /// Applies Foriegn key column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            foreach ( var manyToOneInspector in instance.References )
            {
                var namingStrategy = instance.GetNamingStrategy ();
                var columnName = namingStrategy.GetColumnName ( instance.Property, manyToOneInspector.Property, false );
                if ( typeof( ILookup ).IsAssignableFrom ( manyToOneInspector.Property.PropertyType ) )
                {
                    columnName += "Lkp";
                }
                manyToOneInspector.Column ( columnName + "Key" );
            }
        }

        /// <summary>
        /// Applies Key based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply(IOneToManyCollectionInstance instance)
        {
            var entityType = instance.EntityType;

            // name the key field
            var key = entityType.Name;

            if (typeof(ILookup).IsAssignableFrom(entityType))
            {
                key = key + "Lkp";
            }

            instance.Key.Column(key + "Key");
            instance.Inverse();
        }

        #endregion
    }
}
