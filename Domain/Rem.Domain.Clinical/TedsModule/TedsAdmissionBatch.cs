using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines the batch object for admission records.
    /// </summary>
    public class TedsAdmissionBatch : AuditableAggregateNodeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionBatch"/> class.
        /// </summary>
        protected internal TedsAdmissionBatch ()
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
