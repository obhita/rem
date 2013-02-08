using System;
using System.Text;

namespace Pillar.Domain.Primitives
{
    /// <summary>
    /// DateRange class.
    /// </summary>
    [Component]
    public class DateRange : IEquatable<DateRange>
    {
        #region Constants and Fields

        private readonly DateTime? _endDate;
        private readonly DateTime? _startDate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="expirationDate">The expiration date.</param>
        public DateRange ( DateTime? startDate, DateTime? expirationDate )
        {
            if ( startDate != null && expirationDate != null && startDate > expirationDate )
            {
                throw new ArgumentException ( "EndDate cannot be before StartDate" );
            }

            _startDate = startDate;
            _endDate = expirationDate;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DateRange"/> class from being created.
        /// </summary>
        private DateRange ()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the end date.
        /// </summary>
        public DateTime? EndDate
        {
            get { return _endDate; }
        }

        /// <summary>
        /// Gets the start date.
        /// </summary>
        public DateTime? StartDate
        {
            get { return _startDate; }
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
            return ( this as IEquatable<DateRange> ).Equals ( obj as DateRange );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode ()
        {
            var hash = 13;
            hash = ( hash * 7 ) + _startDate.GetHashCode ();
            hash = ( hash * 7 ) + _endDate.GetHashCode ();
            return hash;
        }

        /// <summary>
        /// Determines whether range includes the specified date.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>True if in range; otherwise false.</returns>
        public bool Includes ( DateTime date )
        {
            if ( date < StartDate )
            {
                return false;
            }

            if ( EndDate == null || date <= EndDate )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString ()
        {
            var sb = new StringBuilder ();
            sb.Append ( "Start Date: " );
            if ( _startDate != null )
            {
                sb.Append ( _startDate.Value.ToString () );
            }
            sb.Append ( "; " );
            sb.Append ( "End Date: " );
            if ( _endDate != null )
            {
                sb.Append ( _endDate.Value.ToString () );
            }

            return sb.ToString ();
        }

        #endregion

        #region Explicit Interface Methods

        bool IEquatable<DateRange>.Equals ( DateRange other )
        {
            if ( other == null )
            {
                return false;
            }

            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }

            if ( GetType () != other.GetType () )
            {
                return false;
            }

            if ( Equals ( _startDate, other._startDate ) && Equals ( _endDate, other._endDate ) )
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
