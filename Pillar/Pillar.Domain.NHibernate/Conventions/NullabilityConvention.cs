using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel.ClassBased;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// NullabilityConvention class.
    /// </summary>
    public class NullabilityConvention : IPropertyConvention, IReferenceConvention, IComponentConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies nullablity based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            var entityType = instance.EntityType;

            if ( !entityType.IsNHibernateComponent () )
            {
                var member = instance.Property;

                if ( !member.IsDbNullable () )
                {
                    instance.Not.Nullable ();
                }
            }
        }

        /// <summary>
        /// Applies nullablity based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IManyToOneInstance instance )
        {
            var member = instance.Property;
            var entityType = instance.EntityType;

            if (!entityType.IsNHibernateComponent())
            {
                if ( !member.IsDbNullable () )
                {
                    instance.Not.Nullable ();
                }
            }
        }

        /// <summary>
        /// Applies nullablity based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var entityType = instance.EntityType;

            if ( !entityType.IsNHibernateComponent () )
            {
                var rootComponentMember = instance.Property;
                if ( !rootComponentMember.IsDbNullable () )
                {
                    var mapping = instance.ConvertToIComponentMapping ();
                    ProcessSettingNotNullable ( mapping );
                }
            }
        }

        #endregion

        #region Methods

        private static void ProcessSettingNotNullable ( IComponentMapping componentMapping )
        {
            foreach ( var propertyMapping in componentMapping.Properties )
            {
                var member = propertyMapping.Member;

                if ( !member.IsDbNullable () )
                {
                    var column = propertyMapping.Columns.Single ();
                    column.NotNull = true;
                }
            }

            foreach ( var manyToOneMapping in componentMapping.References )
            {
                var member = manyToOneMapping.Member;
                if ( !member.IsDbNullable () )
                {
                    foreach ( var column in manyToOneMapping.Columns )
                    {
                        column.NotNull = true;
                    }
                }
            }

            foreach ( var childComponentMapping in componentMapping.Components )
            {
                ProcessSettingNotNullable ( childComponentMapping );
            }
        }

        #endregion
    }
}
