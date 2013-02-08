using System.Collections.Generic;

namespace Pillar.Domain.Primitives
{
    /// <summary>
    /// IPagedList interface.
    /// </summary>
    /// <typeparam name="T">The type in the list.</typeparam>
    public interface IPagedList<T>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the item list.
        /// </summary>
        /// <value>The item list.</value>
        IList<T> ItemList { get; set; }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        int TotalCount { get; set; }

        #endregion
    }
}
