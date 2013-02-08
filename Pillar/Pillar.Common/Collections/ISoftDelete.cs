using System.Collections;
using System.Collections.Generic;

namespace Pillar.Common.Collections
{
    /// <summary>
    /// An interface for keeping track of deleted items.
    /// </summary>
    public interface ISoftDelete : ISoftDeleted
    {
        #region Public Properties

        /// <summary>
        /// Gets the current items.
        /// </summary>
        IList CurrentItems { get; }

        /// <summary>
        /// Gets the removed items.
        /// </summary>
        IList RemovedItems { get; }

        #endregion
    }

    /// <summary>
    /// An interface for keeping track of deleted items.
    /// </summary>
    /// <typeparam name="T">The type of object stored in the list.</typeparam>
    public interface ISoftDelete<T> : ISoftDeleted
    {
        #region Public Properties

        /// <summary>
        /// Gets the current items.
        /// </summary>
        IList<T> CurrentItems { get; }

        /// <summary>
        /// Gets the removed items.
        /// </summary>
        IList<T> RemovedItems { get; }

        #endregion
    }
}
