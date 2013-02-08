using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel.ClassBased;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// ForeignKeyConvention class.
    /// </summary>
    public class ForeignKeyConvention : IReferenceConvention, IReferenceConventionAcceptance, IJoinedSubclassConvention, IComponentConvention
    {
        #region Public Methods

        /// <summary>
        /// Accepts the specified criteria if is component.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void Accept ( IAcceptanceCriteria<IManyToOneInspector> criteria )
        {
            criteria.Expect ( c => !c.EntityType.IsNHibernateComponent () );
        }

        /// <summary>
        /// Applies Foriegn key based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IManyToOneInstance instance )
        {
            var referenceName = instance.GetReferenceName ().Replace ( "`1", string.Empty );

            // Name the foreign key constraint
            var foreignKeyName = string.Format ( "{0}_FK", referenceName );

            instance.ForeignKey ( foreignKeyName );
        }

        /// <summary>
        /// Applies Foriegn key based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IJoinedSubclassInstance instance )
        {
            var name = string.Format ( "{0}_{1}_FK", instance.EntityType.Name, instance.EntityType.BaseType.Name );
            instance.Key.ForeignKey ( name );
        }

        /// <summary>
        /// Applies Foriegn key based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var entityType = instance.EntityType;

            if ( !entityType.IsNHibernateComponent () )
            {
                var mapping = instance.ConvertToIComponentMapping ();

                CreateForeignKeys ( mapping, entityType );
            }
        }

        #endregion

        #region Methods

        private static void CreateForeignKeys ( IComponentMapping componentMapping, Type entityType )
        {
            foreach ( var manyToOneMapping in componentMapping.References )
            {
                var foreignKeyName = manyToOneMapping.GetForeignKeyName ( componentMapping, entityType );
                manyToOneMapping.ForeignKey ( foreignKeyName );
            }

            foreach ( var childComponentMapping in componentMapping.Components )
            {
                CreateForeignKeys ( childComponentMapping, entityType );
            }
        }

        #endregion
    }
}
