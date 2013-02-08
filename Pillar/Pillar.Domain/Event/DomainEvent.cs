using System;
using Pillar.Common.InversionOfControl;

namespace Pillar.Domain.Event
{
    /// <summary>
    /// Domain Event
    /// </summary>
    public class DomainEvent
    {
        #region Public Methods

        /// <summary>
        /// Raises the specified @event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="event">The @event.</param>
        public static void Raise<TEvent> ( TEvent @event ) where TEvent : IDomainEvent
        {
            if ( IoC.CurrentContainer != null )
            {
                IoC.CurrentContainer.Resolve<IDomainEventService>().Raise(@event);
            }
        }

        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="callback">The callback.</param>
        public static void Register<TEvent> ( Action<TEvent> callback ) where TEvent : IDomainEvent
        {
            if (IoC.CurrentContainer != null)
            {
                IoC.CurrentContainer.Resolve<IDomainEventService>().Register(callback);
            }
        }

        #endregion
    }
}
