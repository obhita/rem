using System;
using System.Collections.Generic;
using Pillar.Common.InversionOfControl;

namespace Pillar.Domain.Event
{
    /// <summary>
    /// Service for managing Domain Events
    /// </summary>
    public class DomainEventService : IDomainEventService
    {
        #region Constants and Fields

        private List<Delegate> _actions;

        #endregion

        #region Public Methods

        /// <summary>
        /// Raises the specified @event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="event">The @event.</param>
        public void Raise<TEvent> ( TEvent @event ) where TEvent : IDomainEvent
        {
            if (IoC.CurrentContainer != null)
            {
                foreach (var handler in IoC.CurrentContainer.ResolveAll<IDomainEventHandler<TEvent>>())
                {
                    handler.Handle ( @event );
                }
            }

            if ( _actions != null )
            {
                foreach ( var action in _actions )
                {
                    if ( action is Action<TEvent> )
                    {
                        ( ( Action<TEvent> )action ) ( @event );
                    }
                }
            }
        }

        /// <summary>
        /// Registers the specified callback.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="callback">The callback.</param>
        public void Register<TEvent> ( Action<TEvent> callback ) where TEvent : IDomainEvent
        {
            if ( _actions == null )
            {
                _actions = new List<Delegate> ();
            }
            _actions.Add ( callback );
        }

        #endregion
    }
}
