using System;
using System.Collections;

namespace Pillar.Common.Collections
{
    /// <summary>
    /// The event arguments for the <see cref="ISoftDeleted.SoftDeleted"/> event.
    /// </summary>
    public class SoftDeletedEventArgs : EventArgs
    {
        #region Constants and Fields

        private readonly IList _softDeletedItems;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeletedEventArgs"/> class.
        /// </summary>
        /// <param name="softDeletedItems">The soft deleted items.</param>
        public SoftDeletedEventArgs ( IList softDeletedItems )
        {
            _softDeletedItems = softDeletedItems;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the soft deleted items.
        /// </summary>
        public IList SoftDeletedItems
        {
            get { return _softDeletedItems; }
        }

        #endregion
    }
}
