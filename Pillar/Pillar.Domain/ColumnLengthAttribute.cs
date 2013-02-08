using System;

namespace Pillar.Domain
{
    /// <summary>
    /// ColumnLengthAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Property )]
    public class ColumnLengthAttribute : Attribute
    {
        #region Constants and Fields

        private readonly int _length;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnLengthAttribute"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public ColumnLengthAttribute ( int length )
        {
            if ( length < 1 )
            {
                throw new ArgumentException ( "Length must be greater than or equal to 1." );
            }
            _length = length;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length
        {
            get { return _length; }
        }

        #endregion
    }
}
