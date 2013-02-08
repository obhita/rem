namespace Pillar.Common.Collections
{
    /// <summary>
    /// An interface that provides an event handler that fires when an item is 'soft deleted' from a list.
    /// </summary>
    public interface ISoftDeleted
    {
        #region Public Events

        /// <summary>
        /// Occurs when an item is soft deleted from a list.
        /// </summary>
        event SoftDeletedEventHandler SoftDeleted;

        #endregion
    }
}
