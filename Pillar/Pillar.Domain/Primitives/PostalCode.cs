using System;
using System.Text.RegularExpressions;
using Pillar.Common.Utility;

namespace Pillar.Domain.Primitives
{
    /// <summary>
    /// PostalCode class.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof( PostalCodeTypeNamingStrategy ) )]
    public class PostalCode : IEquatable<PostalCode>
    {
        //Note: This is U.S.A zip code expression
        // We could have a IPostalCodeValidator interface as a Property for this class.
        // The IPostalCodeValidator implementation is in another dll.
        // The implementation could be picked up by DI container at application start-up time, or by e.g. MEF at run time

        #region Constants and Fields

        /// <summary>
        /// Validation Expression
        /// </summary>
        public static readonly string ValidationExpression = @"^\d{5}(-\d{4})?$";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PostalCode"/> class.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        public PostalCode ( string postalCode )
        {
            Check.IsNotNullOrWhitespace ( postalCode, "Postal code is required" );
            Validate ( postalCode );
            Code = postalCode;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PostalCode"/> class from being created.
        /// </summary>
        private PostalCode ()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; private set; }

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
            if ( obj.GetType () != typeof( PostalCode ) )
            {
                return false;
            }
            return Equals ( ( PostalCode )obj );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( PostalCode other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.Code, Code );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode ()
        {
            return ( Code != null ? Code.GetHashCode () : 0 );
        }

        #endregion

        #region Methods

        private static void Validate ( string postalCode )
        {
            var regex = new Regex ( ValidationExpression );
            if ( !regex.IsMatch ( postalCode ) )
            {
                throw new ArgumentException ( string.Format ( "{0} is not a valid united state zip code.", postalCode ) );
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left postal code.</param>
        /// <param name="right">The right postal code.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator == ( PostalCode left, PostalCode right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left postal code.</param>
        /// <param name="right">The right postal code.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator != ( PostalCode left, PostalCode right )
        {
            return !Equals ( left, right );
        }

        #endregion
    }
}
