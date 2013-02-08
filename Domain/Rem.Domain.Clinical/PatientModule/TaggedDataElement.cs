using System;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Defines the tagged data element identifier information.
    /// </summary>
    [Component]
    public class TaggedDataElement : IEquatable<TaggedDataElement>
    {
        private TaggedDataElement ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedDataElement"/> class.
        /// </summary>
        /// <param name="assigningAuthority">The assigning authority.</param>
        /// <param name="extension">The extension.</param>
        public TaggedDataElement(string assigningAuthority, string extension)
        {
            Check.IsNotNullOrWhitespace ( assigningAuthority, () => AssigningAuthorityName );

            AssigningAuthorityName = assigningAuthority;
            ExtensionValue = extension;
        }

        /// <summary>
        /// Gets the extension value.
        /// </summary>
        public virtual string ExtensionValue { get; private set; }

        /// <summary>
        /// Gets the assigning authority name.
        /// </summary>
        [NotNull]
        public virtual string AssigningAuthorityName { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( TaggedDataElement other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.AssigningAuthorityName, AssigningAuthorityName ) && Equals ( other.ExtensionValue, ExtensionValue );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof( TaggedDataElement ) )
            {
                return false;
            }
            return Equals ( ( TaggedDataElement )obj );
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode ()
        {
            unchecked
            {
                return ( ( AssigningAuthorityName != null ? AssigningAuthorityName.GetHashCode () : 0 ) * 397 ) ^ ( ExtensionValue != null ? ExtensionValue.GetHashCode () : 0 );
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == ( TaggedDataElement left, TaggedDataElement right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator != ( TaggedDataElement left, TaggedDataElement right )
        {
            return !Equals ( left, right );
        }
    }
}