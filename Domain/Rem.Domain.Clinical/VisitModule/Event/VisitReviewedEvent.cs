using Pillar.Domain.Event;

namespace Rem.Domain.Clinical.VisitModule.Event
{
    /// <summary>
    /// This is a domain event happened after a Visit is Reviewed by a coder.
    /// </summary>
    public class VisitReviewedEvent : IDomainEvent
    {
        /// <summary>
        /// Gets or sets the visit key.
        /// </summary>
        /// <value>
        /// The visit key.
        /// </value>
        public long VisitKey { get; set; }
    }
}
