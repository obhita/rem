using System.Text;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule.NamingStrategy;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// PersonName defines elements of persons name.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PersonNameTypeNamingStrategy))]
    public class PersonName
    {
        private PersonName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonName"/> class.
        /// </summary>
        /// <param name="prefixName">Name of the prefix.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="suffixName">Name of the suffix.</param>
        public PersonName(string prefixName, string firstName, string middleName, string lastName, string suffixName)
        {
            Check.IsNotNullOrWhitespace(firstName, () => First);
            Check.IsNotNullOrWhitespace(lastName, () => Last);

            Prefix = prefixName;
            First = firstName;
            Middle = middleName;
            Last = lastName;
            Suffix = suffixName;
        }

        /// <summary>
        /// Gets the name of the prefix.
        /// </summary>
        /// <value>
        /// The name of the prefix.
        /// </value>
        public virtual string Prefix { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        [NotNull]
        public virtual string First { get; private set; }

        /// <summary>
        /// Gets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public virtual string Middle { get; private set; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        [NotNull]
        public virtual string Last { get; private set; }

        /// <summary>
        /// Gets the name of the suffix.
        /// </summary>
        /// <value>
        /// The name of the suffix.
        /// </value>
        public virtual string Suffix { get; private set; }

        /// <summary>
        /// Gets the name of the complete.
        /// </summary>
        /// <value>
        /// The name of the complete.
        /// </value>
        [IgnoreMapping]
        public virtual string Complete
        {
            get
            {
                var nameBuilder = new StringBuilder();
                nameBuilder.Append(string.IsNullOrWhiteSpace(Prefix) ? string.Empty : Prefix.Trim() + " ");
                nameBuilder.Append(string.IsNullOrWhiteSpace(First) ? string.Empty : First.Trim() + " ");
                nameBuilder.Append(string.IsNullOrWhiteSpace(Middle) ? string.Empty : Middle.Trim() + " ");
                nameBuilder.Append(string.IsNullOrWhiteSpace(Last) ? string.Empty : Last.Trim());
                nameBuilder.Append(string.IsNullOrWhiteSpace(Suffix) ? string.Empty : " " + Suffix.Trim());
                return nameBuilder.ToString().Trim();
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
        public static bool operator ==(PersonName left, PersonName right)
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
        public static bool operator !=(PersonName left, PersonName right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(PersonName other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.Prefix, Prefix) && Equals(other.First, First) && Equals(other.Middle, Middle) && Equals(other.Last, Last) && Equals(other.Suffix, Suffix);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
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
            if (obj.GetType() != typeof(PersonName))
            {
                return false;
            }
            return Equals((PersonName)obj);
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
                int result = Prefix != null ? Prefix.GetHashCode() : 0;
                result = (result * 397) ^ (First != null ? First.GetHashCode() : 0);
                result = (result * 397) ^ (Middle != null ? Middle.GetHashCode() : 0);
                result = (result * 397) ^ (Last != null ? Last.GetHashCode() : 0);
                result = (result * 397) ^ (Suffix != null ? Suffix.GetHashCode() : 0);
                return result;
            }
        }
    }
}