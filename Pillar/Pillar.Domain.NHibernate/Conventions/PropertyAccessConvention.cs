using System;
using System.Reflection;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Pillar.Common.Extension;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// PropertyAccessConvention class.
    /// </summary>
    public class PropertyAccessConvention : IPropertyConvention, IHasManyConvention, IReferenceConvention, IComponentConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies access based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            var entityType = instance.EntityType;
            var propertyName = instance.Name;
            var propertyInfo = entityType.GetProperty ( propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );

            if ( propertyInfo.IsAutoProperty () )
            {
                instance.Access.BackField ();
            }
            else
            {
                instance.Access.CamelCaseField ( CamelCasePrefix.Underscore );
            }
        }

        /// <summary>
        /// Applies access based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IOneToManyCollectionInstance instance )
        {
            var entityType = instance.EntityType;
            var propertyName = instance.Member.Name;
            var propertyInfo = entityType.GetProperty ( propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );

            if ( propertyInfo.IsAutoProperty () )
            {
                instance.Access.BackField ();
            }
            else
            {
                instance.Access.CamelCaseField ( CamelCasePrefix.Underscore );
            }
        }

        /// <summary>
        /// Applies access based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IManyToOneInstance instance )
        {
            var entityType = instance.EntityType;
            var propertyName = instance.Name;
            var propertyInfo = entityType.GetProperty ( propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );

            if ( propertyInfo.IsAutoProperty () )
            {
                instance.Access.BackField ();
            }
            else
            {
                instance.Access.CamelCaseField ( CamelCasePrefix.Underscore );
            }
        }

        /// <summary>
        /// Applies access based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var entityType = instance.EntityType;
            var propertyName = instance.Name;
            var propertyInfo = entityType.GetProperty ( propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );

            if ( propertyInfo.IsAutoProperty () )
            {
                instance.Access.BackField ();
            }
            else
            {
                instance.Access.CamelCaseField ( CamelCasePrefix.Underscore );
            }
        }

        #endregion
    }
}
