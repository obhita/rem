using System;
using System.Collections.Generic;

namespace Pillar.Common.Extension
{
    /// <summary>
    /// ListExtensions class.
    /// </summary>
    public static class ListExtensions
    {
        #region Public Methods

        /// <summary>
        /// Deletes the specified item from the item list.
        /// Throws exception if the item is not found in the list.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="itemList">The item list.</param>
        /// <param name="item">The item to be deleted.</param>
        public static void Delete<T> ( this IList<T> itemList, T item )
        {
            if ( !itemList.Remove ( item ) )
            {
                throw new Exception ( string.Format ( "{0} not found in the list.", item.GetType ().Name ) );
            }
        }

        #endregion
    }
}
