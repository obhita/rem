using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using Pillar.Domain.Event;
using Rem.Domain.Clinical.VisitModule.Event;
using Rem.Domain.ClinicalBilling.ContextMap.Messages;

namespace Rem.Domain.ClinicalBilling.ContextMap.DomainEventHandler
{
    /// <summary>
    /// This class defines a handler for <see cref="VisitReviewedEvent"/>.
    /// </summary>
    public class VisitReviewedEventHandler : IDomainEventHandler<VisitReviewedEvent>
    {
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitReviewedEventHandler"/> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        public VisitReviewedEventHandler (IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Handles the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        public void Handle(VisitReviewedEvent args)
        {
            var message = _bus.CreateInstance<IVisitReviewedMessage> ();
            message.VisitKey = args.VisitKey;
            _bus.Send ( message );
        }
    }
}
