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
using Pillar.Domain.Primitives;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientProfile defines general patient information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class PatientProfile : IEquatable<PatientProfile>
    {
        private PatientProfile ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientProfile"/> class.
        /// </summary>
        /// <param name="patientGender">The patient gender.</param>
        /// <param name="birthDate">The birth date.</param>
        /// <param name="deathDate">The death date.</param>
        /// <param name="contactPreference">The contact preference.</param>
        /// <param name="emailAddress">The email address.</param>
        public PatientProfile (
            PatientGender patientGender,
            DateTime? birthDate,
            DateTime? deathDate,
            ContactPreference contactPreference,
            EmailAddress emailAddress )
        {
            PatientGender = patientGender;
            BirthDate = birthDate;
            DeathDate = deathDate;
            ContactPreference = contactPreference;
            EmailAddress = emailAddress;
        }

        /// <summary>
        /// Gets the patient gender.
        /// </summary>
        public virtual PatientGender PatientGender { get; private set; }

        /// <summary>
        /// Gets the birth date.
        /// </summary>
        public virtual DateTime? BirthDate { get; private set; }

        /// <summary>
        /// Gets the death date.
        /// </summary>
        public virtual DateTime? DeathDate { get; private set; }

        /// <summary>
        /// Gets the contact preference.
        /// </summary>
        public virtual ContactPreference ContactPreference { get; private set; }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public virtual EmailAddress EmailAddress { get; private set; }

        /// <summary>
        /// Gets the age.
        /// </summary>
        [IgnoreMapping]
        public virtual int Age
        {
            get
            {
                var age = BirthDate != null ? (int)((DateTime.UtcNow - BirthDate).Value.TotalDays / 365.255) : 0;
                return age;
            }
        }


        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="obj">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>             
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
            if ( obj.GetType () != typeof ( PatientProfile ) )
            {
                return false;
            }
            return Equals ( ( PatientProfile ) obj );
        }


        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="other">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>              
        public bool Equals ( PatientProfile other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }

            return
                Equals ( other.PatientGender, PatientGender ) &&
                other.BirthDate.Equals ( BirthDate ) &&
                other.DeathDate.Equals ( DeathDate ) &&
                Equals ( other.ContactPreference, ContactPreference ) &&
                Equals ( other.EmailAddress, EmailAddress );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode ()
        {
            unchecked
            {
                var result = ( PatientGender != null ? PatientGender.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( BirthDate.HasValue ? BirthDate.Value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( DeathDate.HasValue ? DeathDate.Value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( ContactPreference != null ? ContactPreference.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( EmailAddress != null ? EmailAddress.GetHashCode () : 0 );
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
        public static bool operator == ( PatientProfile left, PatientProfile right )
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
        public static bool operator != ( PatientProfile left, PatientProfile right )
        {
            return !Equals ( left, right );
        }
    }
}