using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Pillar.Common.Extension;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// ColumnNameConvention class.
    /// </summary>
    public class ColumnNameConvention : IComponentConvention, IPropertyConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies the column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var namingStrategy = instance.GetNamingStrategy ();

            foreach ( var propertyInstance in instance.Properties )
            {
                var columnName = namingStrategy.GetColumnName ( instance.Property, propertyInstance.Property, false );

                if ( propertyInstance.Property.PropertyType.IsEnum ||
                     propertyInstance.Property.PropertyType.IsNullableEnum () )
                {
                    columnName = GetColumnNameForEnum ( columnName );
                }

                propertyInstance.Column ( columnName );
            }
        }

        /// <summary>
        /// Applies the column name based on type.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            if ( !instance.Property.DeclaringType.IsNHibernateComponent () )
            {
                if ( instance.Property.PropertyType.IsEnum ||
                     instance.Property.PropertyType.IsNullableEnum () )
                {
                    var columnName = GetColumnNameForEnum ( instance.Property.Name );
                    instance.Column ( columnName );
                }
            }
        }

        #endregion

        #region Methods

        private static string GetColumnNameForEnum ( string defaultColumnName )
        {
            return defaultColumnName + "Enum";
        }

        #endregion
    }
}
