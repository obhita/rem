namespace Pillar.Domain
{
    /// <summary>
    /// Entity class.
    /// </summary>
    public abstract class Entity : IEntity
    {
        private long _key;
        private int _version;

        #region Public Properties

        /// <summary>
        /// Gets the key.
        /// </summary>
        public virtual long Key
        {
            get { return _key; }
            private set { _key = value; }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public virtual int Version
        {
            get { return _version; }
            protected set { _version = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Equalses the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public virtual bool Equals ( Entity entity )
        {
            if ( ReferenceEquals ( null, entity ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, entity ) )
            {
                return true;
            }
            if ( GetType () != entity.GetType () )
            {
                return false;
            }

            var otherIsTransient = Equals ( entity.Key, ( long )0 );
            var thisIsTransient = Equals ( Key, ( long )0 );

            if ( otherIsTransient && thisIsTransient )
            {
                return ReferenceEquals ( entity, this );
            }

            return entity.Key.Equals ( Key );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public virtual bool Equals ( IEntity other )
        {
            return Equals ( other as Entity );
        }

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
            if ( GetType () != obj.GetType () )
            {
                return false;
            }

            return Equals ( obj as Entity );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode ()
        {
            // Entities will always have an integer key.  
            // Entities that have not yet been saved to the database
            // will have key that equals 0 therefore we need a better 
            // algorithm in that case.
            return ( Key != 0 ? Key.GetHashCode () : string.Empty.GetHashCode () * 397 ) ^ GetType ().GetHashCode ();
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left entity.</param>
        /// <param name="right">The right entity.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator == ( Entity left, Entity right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left entity.</param>
        /// <param name="right">The right entity.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator != ( Entity left, Entity right )
        {
            return !Equals ( left, right );
        }

        #endregion
    }
}
