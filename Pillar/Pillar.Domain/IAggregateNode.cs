namespace Pillar.Domain
{
    /// <summary>
    /// IAggregateNode interface.
    /// </summary>
    [IgnoreMapping]
    public interface IAggregateNode
    {
        #region Public Properties

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        IAggregateRoot AggregateRoot { get; }

        #endregion
    }
}
