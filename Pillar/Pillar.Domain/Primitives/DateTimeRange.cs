using System;

namespace Pillar.Domain.Primitives
{
    /// <summary>
    /// DateTimeRange defines range of time between a start date and time and an end date and time.
    /// </summary>
    [Component]
    public class DateTimeRange : IEquatable<DateTimeRange>
    {
        #region Constants and Fields

        private readonly DateTime _endDateTime;
        private readonly DateTime _startDateTime;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> class.
        /// </summary>
        /// <param name="startDateTime">The start date time.</param>
        /// <param name="endDateTime">The end date time.</param>
        public DateTimeRange ( DateTime startDateTime, DateTime endDateTime )
        {
            if ( startDateTime > endDateTime )
            {
                throw new ArgumentException ( "EndDateTime cannot be before StartDateTime" );
            }

            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="DateTimeRange"/> class from being created.
        /// </summary>
        private DateTimeRange ()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the end date time.
        /// </summary>
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
        }

        /// <summary>
        /// Gets the start date time.
        /// </summary>
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals ( DateTimeRange other )
        {
            return ( ( IEquatable<DateTimeRange> )this ).Equals ( other );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode ()
        {
            var hash = 13;
            hash = ( hash * 7 ) + _startDateTime.GetHashCode ();
            hash = ( hash * 7 ) + _endDateTime.GetHashCode ();
            return hash;
        }

        /// <summary>
        /// Includeses the specified date time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool Includes ( DateTime dateTime )
        {
            return dateTime >= StartDateTime && dateTime <= EndDateTime;
        }

        /// <summary>
        /// Includeses the specified date time range.
        /// </summary>
        /// <param name="dateTimeRange">The date time range.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool Includes ( DateTimeRange dateTimeRange )
        {
            return dateTimeRange.StartDateTime >= StartDateTime && dateTimeRange.EndDateTime <= EndDateTime;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString ()
        {
            return string.Format ( "Start DateTime: {0}; End DateTime: {1}", _startDateTime, _endDateTime );
        }

        #endregion

        #region Explicit Interface Methods

        bool IEquatable<DateTimeRange>.Equals ( DateTimeRange other )
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

            if ( Equals ( _startDateTime, other._startDateTime ) && Equals ( _endDateTime, other._endDateTime ) )
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
