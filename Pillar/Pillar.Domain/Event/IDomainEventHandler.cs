namespace Pillar.Domain.Event
{
    /// <summary>
    /// Interface for handling a <see cref="IDomainEvent"/>.
    /// </summary>
    /// <typeparam name="T">Type of Domain Event.</typeparam>
    public interface IDomainEventHandler<in T>
        where T : IDomainEvent
    {
        #region Public Methods

        /// <summary>
        /// Handles the <see cref="IDomainEvent"/>.
        /// </summary>
        /// <param name="args">The args for the <see cref="IDomainEvent"/>.</param>
        void Handle ( T args );

        #endregion
    }
}
