using System;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Defines organization information of provenance.
    /// </summary>
    [Component]
    public class RepresentedOrganization : IEquatable<RepresentedOrganization>
    {
        private RepresentedOrganization ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepresentedOrganization"/> class.
        /// </summary>
        /// <param name="organizationTaggedDataElement">The organization tagged data element.</param>
        /// <param name="organizationName">Name of the organization.</param>
        /// <param name="phone">The phone.</param>
        public RepresentedOrganization(TaggedDataElement organizationTaggedDataElement, string organizationName, Phone phone)
        {
            OrganizationTaggedDataElement = organizationTaggedDataElement;
            OrganizationName = organizationName;
            Phone = phone;
        }

        /// <summary>
        /// Gets the organization tagged data element.
        /// </summary>
        public virtual TaggedDataElement OrganizationTaggedDataElement { get; private set; }

        /// <summary>
        /// Gets the name of the organization.
        /// </summary>
        /// <value>
        /// The name of the organization.
        /// </value>
        public virtual string OrganizationName { get; private set; }

        /// <summary>
        /// Gets the phone.
        /// </summary>
        public virtual Phone Phone { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( RepresentedOrganization other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.Phone, Phone ) && Equals ( other.OrganizationName, OrganizationName ) && Equals ( other.OrganizationTaggedDataElement, OrganizationTaggedDataElement );
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
            if ( obj.GetType () != typeof( RepresentedOrganization ) )
            {
                return false;
            }
            return Equals ( ( RepresentedOrganization )obj );
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
                int result = ( Phone != null ? Phone.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( OrganizationName != null ? OrganizationName.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( OrganizationTaggedDataElement != null ? OrganizationTaggedDataElement.GetHashCode () : 0 );
                return result;
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
        public static bool operator == ( RepresentedOrganization left, RepresentedOrganization right )
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
        public static bool operator != ( RepresentedOrganization left, RepresentedOrganization right )
        {
            return !Equals ( left, right );
        }
    }
}