using System;
using System.Text;

namespace Pillar.Domain.Primitives
{
    /// <summary>
    /// TimeRange class.
    /// </summary>
    [Component]
    public class TimeRange : IEquatable<TimeRange>
    {
        #region Constants and Fields

        private readonly TimeSpan? _endTime;
        private readonly TimeSpan? _startTime;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeRange"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="expirationTime">The expiration time.</param>
        public TimeRange ( TimeSpan? startTime, TimeSpan? expirationTime )
        {
            if ( startTime != null && expirationTime != null && startTime > expirationTime )
            {
                throw new ArgumentException ( "EndTime cannot be before StartTime" );
            }

            _startTime = startTime;
            _endTime = expirationTime;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="TimeRange"/> class from being created.
        /// </summary>
        private TimeRange ()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the end time.
        /// </summary>
        public TimeSpan? EndTime
        {
            get { return _endTime; }
        }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        public TimeSpan? StartTime
        {
            get { return _startTime; }
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
            return ( this as IEquatable<TimeRange> ).Equals ( obj as TimeRange );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode ()
        {
            var hash = 13;
            hash = ( hash * 7 ) + _startTime.GetHashCode ();
            hash = ( hash * 7 ) + _endTime.GetHashCode ();
            return hash;
        }

        /// <summary>
        /// Includeses the specified time.
        /// </summary>
        /// <param name="time">The time to check.</param>
        /// <returns>True if time range includes time; otherwise false.</returns>
        public bool Includes ( TimeSpan time )
        {
            if ( !StartTime.HasValue && !EndTime.HasValue )
            {
                return false;
            }
            if ( StartTime.HasValue && time < StartTime.Value )
            {
                return false;
            }

            if ( EndTime == null || time <= EndTime.Value )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Overlapses the specified time range.
        /// </summary>
        /// <param name="timeRange">The time range.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool Overlaps ( TimeRange timeRange )
        {
            if ( !timeRange.StartTime.HasValue || !timeRange.EndTime.HasValue )
            {
                return false;
            }
            if ( !StartTime.HasValue || !EndTime.HasValue )
            {
                return false;
            }
            if ( ( StartTime <= timeRange.EndTime ) && ( EndTime >= timeRange.StartTime ) )
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
            sb.Append ( "Start Time: " );
            if ( _startTime != null )
            {
                sb.Append ( _startTime.Value.ToString () );
            }
            sb.Append ( "; " );
            sb.Append ( "End Time: " );
            if ( _endTime != null )
            {
                sb.Append ( _endTime.Value.ToString () );
            }

            return sb.ToString ();
        }

        #endregion

        #region Explicit Interface Methods

        bool IEquatable<TimeRange>.Equals ( TimeRange other )
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

            if ( Equals ( _startTime, other._startTime ) && Equals ( _endTime, other._endTime ) )
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
