using System.Collections.Generic;

namespace Pillar.Domain.Primitives
{
    /// <summary>
    /// PagedList class.
    /// </summary>
    /// <typeparam name="T">The type in the list.</typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the item list.
        /// </summary>
        /// <value>The item list.</value>
        public IList<T> ItemList { get; set; }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        #endregion
    }
}
