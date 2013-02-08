using System;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Defines the author information of provenance.
    /// </summary>
    [Component]
    public class AssignedAuthor : IEquatable<AssignedAuthor>
    {
        private AssignedAuthor ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignedAuthor"/> class.
        /// </summary>
        /// <param name="providerDirectoryEntry">The provider directory entry.</param>
        /// <param name="name">The name.</param>
        public AssignedAuthor(string providerDirectoryEntry, PersonName name)
        {
            ProviderDirectoryEntryAddress = providerDirectoryEntry;
            Name = name;
        }

        /// <summary>
        /// Gets the provider directory entry address.
        /// </summary>
        public virtual string ProviderDirectoryEntryAddress { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual PersonName Name { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( AssignedAuthor other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.Name, Name ) && Equals ( other.ProviderDirectoryEntryAddress, ProviderDirectoryEntryAddress );
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
            if ( obj.GetType () != typeof( AssignedAuthor ) )
            {
                return false;
            }
            return Equals ( ( AssignedAuthor )obj );
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
                return ( ( Name != null ? Name.GetHashCode () : 0 ) * 397 ) ^ ( ProviderDirectoryEntryAddress != null ? ProviderDirectoryEntryAddress.GetHashCode () : 0 );
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
        public static bool operator == ( AssignedAuthor left, AssignedAuthor right )
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
        public static bool operator != ( AssignedAuthor left, AssignedAuthor right )
        {
            return !Equals ( left, right );
        }
    }
}