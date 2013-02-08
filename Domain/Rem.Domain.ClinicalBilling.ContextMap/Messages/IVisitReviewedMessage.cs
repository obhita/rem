using NServiceBus;

namespace Rem.Domain.ClinicalBilling.ContextMap.Messages
{
    /// <summary>
    /// The interface defines a visit reviewed message.
    /// </summary>
    public interface IVisitReviewedMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the visit key.
        /// </summary>
        /// <value>
        /// The visit key.
        /// </value>
        long VisitKey { get; set; }
    }
}
