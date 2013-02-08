using System;

namespace Pillar.Common.Commands
{
    /// <summary>
    /// Object for holding info about command.
    /// </summary>
    public class FrameworkCommandInfo : IFrameworkCommandInfo, IEquatable<FrameworkCommandInfo>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkCommandInfo"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="name">The name of the command.</param>
        public FrameworkCommandInfo ( object owner, string name )
        {
            Owner = owner;
            Name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the owner.
        /// </summary>
        public object Owner { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( FrameworkCommandInfo other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.Owner, Owner ) && Equals ( other.Name, Name );
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
            if ( obj.GetType () != typeof( FrameworkCommandInfo ) )
            {
                return false;
            }
            return Equals ( ( FrameworkCommandInfo )obj );
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
                return ( ( Owner != null ? Owner.GetHashCode () : 0 ) * 397 ) ^ ( Name != null ? Name.GetHashCode () : 0 );
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == ( FrameworkCommandInfo left, FrameworkCommandInfo right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator != ( FrameworkCommandInfo left, FrameworkCommandInfo right )
        {
            return !Equals ( left, right );
        }

        #endregion
    }
}
