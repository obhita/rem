#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Payor subscriber cache contains the profile of a payor subscriber.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof( PropertyNameAsColumnNameNamingStrategy ) )]
    public class PayorSubscriberCache : IEquatable<PayorSubscriberCache>
    {
        private PayorSubscriberCache ()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PayorSubscriberCache"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="gender">The gender.</param>
        /// <param name="name">The name.</param>
        /// <param name="payorSubscriberRelationshipCacheType">Type of the payor subscriber relationship cache.</param>
        public PayorSubscriberCache (Address address,
            DateTime? birthDate,
            AdministrativeGender gender,
            PersonName name,
            PayorSubscriberRelationshipCacheType payorSubscriberRelationshipCacheType)
        {
            Address = address;
            BirthDate = birthDate;
            AdministrativeGender = gender;
            Name = name;
            PayorSubscriberRelationshipCacheType = payorSubscriberRelationshipCacheType;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public virtual Address Address { get; private set; }

        /// <summary>
        /// Gets the birth date.
        /// </summary>
        public virtual DateTime? BirthDate { get; private set; }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        public virtual AdministrativeGender AdministrativeGender { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual PersonName Name { get; private set; }


        /// <summary>
        /// Gets the type of the payor subscriber relationship cache.
        /// </summary>
        /// <value>
        /// The type of the payor subscriber relationship cache.
        /// </value>
        public virtual PayorSubscriberRelationshipCacheType PayorSubscriberRelationshipCacheType { get; private set; }


        /// <summary>
        /// Revises the address.
        /// </summary>
        /// <param name="address">The address.</param>
        public virtual void ReviseAddress (Address address)
        {
            Address = address;
        }

        /// <summary>
        /// Revises the birth date.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        public virtual void ReviseBirthDate(DateTime? birthDate)
        {
            BirthDate = birthDate;
        }

        /// <summary>
        /// Revises the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        public virtual void ReviseGender(AdministrativeGender gender)
        {
            AdministrativeGender = gender;
        }

        /// <summary>
        /// Revises the name.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void ReviseName(PersonName name)
        {
            Name = name;
        }

        /// <summary>
        /// Revises the type of the payor sub scriber relationship.
        /// </summary>
        /// <param name="payorSubscriberRelationshipCacheType">Type of the payor subscriber relationship cache.</param>
        public virtual void RevisePayorSubScriberRelatioshipType(PayorSubscriberRelationshipCacheType payorSubscriberRelationshipCacheType)
        {
            PayorSubscriberRelationshipCacheType = payorSubscriberRelationshipCacheType;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( PayorSubscriberCache other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.Address, Address ) && other.BirthDate.Equals ( BirthDate ) && Equals ( other.AdministrativeGender, AdministrativeGender ) && Equals ( other.Name, Name ) && Equals ( other.PayorSubscriberRelationshipCacheType, PayorSubscriberRelationshipCacheType );
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
            if ( obj.GetType () != typeof( PayorSubscriberCache ) )
            {
                return false;
            }
            return Equals ( ( PayorSubscriberCache )obj );
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
                int result = ( Address != null ? Address.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( BirthDate.HasValue ? BirthDate.Value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( AdministrativeGender != null ? AdministrativeGender.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( Name != null ? Name.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( PayorSubscriberRelationshipCacheType != null ? PayorSubscriberRelationshipCacheType.GetHashCode () : 0 );
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
        public static bool operator == ( PayorSubscriberCache left, PayorSubscriberCache right )
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
        public static bool operator != ( PayorSubscriberCache left, PayorSubscriberCache right )
        {
            return !Equals ( left, right );
        }
    }
}
