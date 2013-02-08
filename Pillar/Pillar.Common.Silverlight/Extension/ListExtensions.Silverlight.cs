using System;
using System.Collections.Generic;
using System.Linq;

namespace Pillar.Common.Extension
{
    /// <summary>
    /// ListExtensions class.
    /// </summary>
    public static class ListExtensions
    {
        #region Public Methods

        /// <summary>
        /// Removes all.
        /// </summary>
        /// <typeparam name="T">The type in the list.</typeparam>
        /// <param name="list">The list to remove from.</param>
        /// <param name="match">The predicate match.</param>
        public static void RemoveAll<T> ( this List<T> list, Predicate<T> match )
        {
            foreach ( var item in list.Where ( l => match ( l ) ).ToList () )
            {
                list.Remove ( item );
            }
        }

        #endregion
    }
}
