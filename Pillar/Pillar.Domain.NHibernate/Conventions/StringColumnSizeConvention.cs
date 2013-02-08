using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using Pillar.Common.Extension;
using Pillar.Domain.NHibernate.Extensions;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// StringColumnSizeConvention class.
    /// </summary>
    public class StringColumnSizeConvention : IPropertyConvention, IComponentConvention
    {
        #region Constants and Fields

        /// <summary>
        /// Code Length
        /// </summary>
        public static readonly int CodeLength = 10;

        /// <summary>
        /// Description Length
        /// </summary>
        public static readonly int DescriptionLength = 500;

        /// <summary>
        /// Enum Length
        /// </summary>
        public static readonly int EnumLength = 50;

        /// <summary>
        /// Identifier Length
        /// </summary>
        public static readonly int IdentifierLength = 20;

        /// <summary>
        /// Name Length
        /// </summary>
        public static readonly int NameLength = 100;

        /// <summary>
        /// Note Length
        /// Note: Using 10000 is a trick to get Fluent NHibernate to use nvarchar(max)
        /// </summary>
        public static readonly int NoteLength = 10000;

        /// <summary>
        /// Number Length
        /// </summary>
        public static readonly int NumberLength = 20;

        /// <summary>
        /// Value Length
        /// </summary>
        public static readonly int ValueLength = 255;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the length of the property based on the Name.
        /// </summary>
        /// <param name="propertyInstance">The property instance.</param>
        /// <param name="columnName">Name of the column.</param>
        public static void Apply ( IPropertyInstance propertyInstance, string columnName )
        {
            var lengthAttributes = propertyInstance.Property.MemberInfo.GetCustomAttributes (
                typeof( ColumnLengthAttribute ), false );

            if ( lengthAttributes.Length > 0 )
            {
                var columnLength = ( ColumnLengthAttribute )lengthAttributes[0];
                propertyInstance.Length ( columnLength.Length );
            }
            else if ( columnName.EndsWith ( "Code" ) )
            {
                propertyInstance.Length ( CodeLength );
            }
            else if ( columnName.EndsWith ( "Number" ) )
            {
                propertyInstance.Length ( NumberLength );
            }
            else if ( columnName.EndsWith ( "Name" ) )
            {
                propertyInstance.Length ( NameLength );
            }
            else if ( columnName.EndsWith ( "Note" ) )
            {
                propertyInstance.Length ( NoteLength );
            }
            else if ( columnName.EndsWith ( "Description" ) )
            {
                propertyInstance.Length ( DescriptionLength );
            }
            else if ( columnName.EndsWith ( "Identifier" ) )
            {
                propertyInstance.Length ( IdentifierLength );
            }
            else if ( columnName.EndsWith ( "Value" ) )
            {
                propertyInstance.Length ( ValueLength );
            }
        }

        /// <summary>
        /// Accepts the specified criteria if is string or enum.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void Accept ( IAcceptanceCriteria<IPropertyInspector> criteria )
        {
            criteria.Expect (
                p => p.Property.PropertyType == typeof( string ) ||
                     p.Property.PropertyType.IsEnum ||
                     p.Property.PropertyType.IsNullableEnum ()
                );
        }

        /// <summary>
        /// Sets Length based on type and name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            if ( ! instance.Property.DeclaringType.IsNHibernateComponent () )
            {
                if ( instance.Property.PropertyType == typeof( string ) )
                {
                    Apply ( instance, instance.Name );
                }
                else if ( instance.Property.PropertyType.IsEnum ||
                          instance.Property.PropertyType.IsNullableEnum () )
                {
                    instance.Length ( EnumLength );
                }
            }
        }

        /// <summary>
        /// Sets length based on name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IComponentInstance instance )
        {
            var stringProperties = instance.Properties.Where ( p => p.Property.PropertyType == typeof( string ) );
            if ( stringProperties.Count () > 0 )
            {
                var namingStrategy = instance.GetNamingStrategy ();

                foreach ( var propertyInstance in stringProperties )
                {
                    var columnName = namingStrategy.GetColumnName ( instance.Property, propertyInstance.Property, false );
                    Apply ( propertyInstance, columnName );
                }
            }
        }

        #endregion
    }
}
