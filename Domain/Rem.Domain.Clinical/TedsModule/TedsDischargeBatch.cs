using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines the batch object for discharge records.
    /// </summary>
    public class TedsDischargeBatch : AuditableAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeBatch"/> class.
        /// </summary>
        protected internal TedsDischargeBatch()
        {
        }

        /// <summary>
        /// Gets the teds batch.
        /// </summary>
        public virtual TedsBatch TedsBatch { get; private set; }

        /// <summary>
        /// Gets the submission document.
        /// </summary>
        public virtual byte[] SubmissionDocument { get; private set; }

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return TedsBatch; }
        }
    }
}
