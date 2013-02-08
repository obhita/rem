using System;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// The Phone defines a telephone number.
    /// </summary>
    [Component]
    public class Phone : IEquatable<Phone>
    {
        private Phone()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Phone"/> class.
        /// </summary>
        /// <param name="phoneNumber">
        /// The phone number.
        /// </param>
        /// <param name="phoneExtensionNumber">
        /// The phone extension number.
        /// </param>
        public Phone(string phoneNumber, string phoneExtensionNumber)
        {
            Check.IsNotNullOrWhitespace(phoneNumber, () => PhoneNumber);

            PhoneNumber = phoneNumber;
            PhoneExtensionNumber = string.IsNullOrWhiteSpace(phoneExtensionNumber) ? null : phoneExtensionNumber;
        }

        /// <summary>
        /// Gets PhoneNumber.
        /// </summary>
        [NotNull]
        public virtual string PhoneNumber { get; private set; }

        /// <summary>
        /// Gets PhoneExtensionNumber.
        /// </summary>
        public virtual string PhoneExtensionNumber { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Phone left, Phone right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Phone left, Phone right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// True if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals(Phone other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.PhoneNumber, PhoneNumber) && Equals(other.PhoneExtensionNumber, PhoneExtensionNumber);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(Phone))
            {
                return false;
            }

            return Equals((Phone)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((PhoneNumber != null ? PhoneNumber.GetHashCode() : 0) * 397) ^ (PhoneExtensionNumber != null ? PhoneExtensionNumber.GetHashCode() : 0);
            }
        }
    }
}