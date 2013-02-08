using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel.ClassBased;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// IndexConvention class.
    /// </summary>
    public class IndexConvention : IJoinedSubclassConvention,
                                   IPropertyConvention,
                                   IReferenceConvention,
                                   IReferenceConventionAcceptance,
                                   IComponentConvention
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
        /// Does nothing.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IJoinedSubclassInstance instance )
        {
            // We don't need to create an unique index for the base type key since this key is now treated as a primary key
        }

        /// <summary>
        /// Creates Foreign Key indexes for components.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var entityType = instance.EntityType;

            if ( !entityType.IsNHibernateComponent () )
            {
                var mapping = instance.ConvertToIComponentMapping ();

                CreateForeignKeyIndexes ( mapping, entityType );
            }
        }

        /// <summary>
        /// Applies Index based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            CreateUniqeIndexIfIndicated ( instance.Property, instance.Unique, instance.UniqueKey );

            var sourceName = instance.EntityType.Name;
            var propertyName = instance.Property.Name;

            if ( typeof( ILookup ).IsAssignableFrom ( instance.EntityType ) )
            {
                sourceName += "Lkp";
            }

            if ( typeof( ILookup ).IsAssignableFrom ( instance.Property.PropertyType ) )
            {
                propertyName += "Lkp";
            }

            var indexAttribute = instance.Property.MemberInfo.GetCustomAttributes (
                typeof( NaturalIndexAttribute ), false );

            if ( indexAttribute.Length > 0 )
            {
                var indexName = string.Format ( "{0}_{1}_IDX", sourceName, propertyName );
                instance.Index ( indexName );
            }
        }

        /// <summary>
        /// Applies Index based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IManyToOneInstance instance )
        {
            var isUniqeIndexCreated = CreateUniqeIndexIfIndicated ( instance.Property, instance.Unique, instance.UniqueKey );

            if ( !isUniqeIndexCreated )
            {
                var referenceName = instance.GetReferenceName ().Replace ( "`1", string.Empty );

                var indexKeyName = string.Format ( "{0}_FK_IDX", referenceName );

                instance.Index ( indexKeyName );
            }
        }

        #endregion

        #region Methods

        private static void CreateForeignKeyIndexes ( IComponentMapping componentMapping, Type entityType )
        {
            foreach ( var manyToOneMapping in componentMapping.References )
            {
                var foreignKeyName = manyToOneMapping.GetForeignKeyName ( componentMapping, entityType );
                const string ForeignKeyIndexNameSuffix = "_IDX";
                var indexName = string.Format ( "{0}{1}", foreignKeyName, ForeignKeyIndexNameSuffix );
                manyToOneMapping.Index ( indexName );
            }

            foreach ( var childComponentMapping in componentMapping.Components )
            {
                CreateForeignKeyIndexes ( childComponentMapping, entityType );
            }
        }

        private static bool CreateUniqeIndexIfIndicated ( Member member, Action actionForUnique, Action<string> actionForUniqueKey )
        {
            var isUniqueIndexCreated = false;
            var uniqueAttributes = member.MemberInfo.GetCustomAttributes (
                typeof( UniqueAttribute ), false );

            if ( uniqueAttributes.Length > 0 )
            {
                var uniqueAttribute = uniqueAttributes[0] as UniqueAttribute;
                if ( uniqueAttribute != null )
                {
                    if ( uniqueAttribute.GroupName == null )
                    {
                        actionForUnique ();
                    }
                    else
                    {
                        actionForUniqueKey ( uniqueAttribute.GroupName );
                    }

                    isUniqueIndexCreated = true;
                }
            }

            return isUniqueIndexCreated;
        }

        #endregion
    }
}
