using System;
using System.Text.RegularExpressions;
using Pillar.Common.Utility;

namespace Pillar.Domain.Primitives
{
    /// <summary>
    /// EmailAddress class.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof( EmailAddressTypeNamingStrategy ) )]
    public class EmailAddress : IEquatable<EmailAddress>
    {
        #region Constants and Fields

        /// <summary>
        /// Validation Expression
        /// </summary>
        public static readonly string ValidationExpression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        public EmailAddress ( string emailAddress )
        {
            Check.IsNotNullOrWhitespace ( emailAddress, "Email address is required" );
            Validate ( emailAddress );
            Address = emailAddress;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="EmailAddress"/> class from being created.
        /// </summary>
        private EmailAddress ()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the domain.
        /// </summary>
        [IgnoreMapping]
        public string Domain
        {
            get { return ExtractDomain ( Address ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
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
            if ( obj.GetType () != typeof( EmailAddress ) )
            {
                return false;
            }
            return Equals ( ( EmailAddress )obj );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( EmailAddress other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.Address, Address );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode ()
        {
            return ( Address != null ? Address.GetHashCode () : 0 );
        }

        /// <summary>
        /// Determines whether [is in domain] [the specified domain].
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns><c>true</c> if [is in domain] [the specified domain]; otherwise, <c>false</c>.</returns>
        public bool IsInDomain ( string domain )
        {
            return string.Compare ( Domain, domain, true ) == 0;
        }

        #endregion

        #region Methods

        private static string ExtractDomain ( string emailAddress )
        {
            return emailAddress.Substring ( 0, emailAddress.IndexOf ( "@" ) );
        }

        private static void Validate ( string emailAddress )
        {
            var regex = new Regex ( ValidationExpression );
            if ( !regex.IsMatch ( emailAddress ) )
            {
                throw new ArgumentException ( string.Format ( "{0} is not a valid email address", emailAddress ) );
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left email address.</param>
        /// <param name="right">The right email address.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator == ( EmailAddress left, EmailAddress right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left email address.</param>
        /// <param name="right">The right email address.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator != ( EmailAddress left, EmailAddress right )
        {
            return !Equals ( left, right );
        }

        #endregion
    }
}
