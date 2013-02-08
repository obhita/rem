using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// DateTimeColumnTypeConvention class.
    /// </summary>
    public class DateTimeColumnTypeConvention : IPropertyConvention, IComponentConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies CustomSqlType for Date and time columns.
        /// </summary>
        /// <param name="propertyInstance">The property instance.</param>
        /// <param name="columnName">Name of the column.</param>
        public static void Apply ( IPropertyInstance propertyInstance, string columnName )
        {
            columnName = columnName.ToLower ();

            if ( columnName.EndsWith ( "timestamp" ) )
            {
                propertyInstance.CustomSqlType ( "datetimeoffset" );
            }
            else if ( columnName.EndsWith ( "datetime" ) )
            {
                //TODO: Property ending with DateTime will use datetimeoffset in future. 
                //     Do not remove this condition or it'll pick Time as its type from the next condition
                //instance.CustomSqlType ( "datetimeoffset" );
            }
            else if ( columnName.EndsWith ( "date" ) )
            {
                propertyInstance.CustomSqlType ( "date" );
            }
            else if ( columnName.EndsWith ( "time" ) )
            {
                propertyInstance.CustomSqlType ( "time" );
            }
        }

        /// <summary>
        /// Applies Column name based on naming strategy for property.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            var columnName = instance.Property.Name;

            Apply ( instance, columnName );
        }

        /// <summary>
        /// Applies Column name based on naming strategy for all properties.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var namingStrategy = instance.GetNamingStrategy ();

            foreach ( var propertyInstance in instance.Properties )
            {
                var columnName = namingStrategy.GetColumnName ( instance.Property, propertyInstance.Property, false );
                Apply ( propertyInstance, columnName );
            }
        }

        #endregion
    }
}
