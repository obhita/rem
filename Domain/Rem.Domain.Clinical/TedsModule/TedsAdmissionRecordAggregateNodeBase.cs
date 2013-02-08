using System;
using Pillar.Common.Extension;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// TedsAdmissionRecordAggregateNodeBase provides a base aggregate node implementation.
    /// </summary>
    public abstract class TedsAdmissionRecordAggregateNodeBase : AuditableAggregateNodeBase
    {
        /// <summary>
        /// Gets or sets the teds admission record.
        /// </summary>
        /// <value>
        /// The teds admission record.
        /// </value>
        [NotNull]
        public virtual TedsAdmissionRecord TedsAdmissionRecord { get; protected internal set; }

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return TedsAdmissionRecord; }
        }
    }
}