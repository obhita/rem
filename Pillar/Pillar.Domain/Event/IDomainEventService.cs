using System;

namespace Pillar.Domain.Event
{
    /// <summary>
    /// Service for managing domain events.
    /// </summary>
    public interface IDomainEventService
    {
        #region Public Methods

        /// <summary>
        /// Raises the specified @event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="event">The @event.</param>
        void Raise<TEvent> ( TEvent @event ) where TEvent : IDomainEvent;

        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="callback">The callback.</param>
        void Register<TEvent> ( Action<TEvent> callback ) where TEvent : IDomainEvent;

        #endregion
    }
}
