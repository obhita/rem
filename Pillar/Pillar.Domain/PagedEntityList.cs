using System.Collections.Generic;
using Pillar.Domain.Primitives;

namespace Pillar.Domain
{
    /// <summary>
    /// PagedEntityList class.
    /// </summary>
    /// <typeparam name="TEntity">The type of the enity.</typeparam>
    public class PagedEntityList<TEntity> : PagedList<TEntity>
        where TEntity : class, IEntity
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedEntityList&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="totalCount">The total count.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="itemList">The item list.</param>
        public PagedEntityList ( int totalCount, int pageIndex, int pageSize, IList<TEntity> itemList )
        {
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            ItemList = itemList;
        }

        #endregion
    }
}
