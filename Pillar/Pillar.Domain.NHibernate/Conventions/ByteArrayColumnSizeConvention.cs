using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// ByteArrayColumnSizeConvention class.
    /// </summary>
    public class ByteArrayColumnSizeConvention : IPropertyConvention, IPropertyConventionAcceptance
    {
        // Note: Using 2147483647 is a trick to get Fluent NHibernate to use varbinary(max)

        #region Constants and Fields

        /// <summary>
        /// Byte Array Default Length
        /// </summary>
        public static readonly int ByteArrayDefaultLength = 2147483647;

        #endregion

        #region Public Methods

        /// <summary>
        /// Accepts the specified criteria if property type is byte[]/>.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void Accept ( IAcceptanceCriteria<IPropertyInspector> criteria )
        {
            criteria.Expect ( p => p.Property.PropertyType == typeof( byte[] ) );
        }

        /// <summary>
        /// Applies length based on byte[].
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IPropertyInstance instance )
        {
            var lengthAttributes = instance.Property.MemberInfo.GetCustomAttributes (
                typeof( ColumnLengthAttribute ), false );

            if ( lengthAttributes.Length > 0 )
            {
                var columnLength = ( ColumnLengthAttribute )lengthAttributes[0];
                instance.Length ( columnLength.Length );
            }
            else
            {
                instance.Length ( ByteArrayDefaultLength );
            }
        }

        #endregion
    }
}
