using System;
using Pillar.Domain;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// AuditableAggregateNodeBase provides a base implementation for aggregate nodes. The concept of an aggregate node defines
    /// the association of a child entity to to an aggregate root.
    /// </summary>
    public abstract class AuditableAggregateNodeBase : AbstractAggregateNode, IAuditable
    {
        private DateTimeOffset _createdTimestamp;
        private SystemAccount _createdBySystemAccount;
        private DateTimeOffset _updatedTimestamp;
        private SystemAccount _updatedBySystemAccount;

        #region Implementation of IAuditable

        /// <summary>
        /// Gets the created timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset CreatedTimestamp
        {
            get { return _createdTimestamp; }
            private set { _createdTimestamp = value; }
        }

        /// <summary>
        /// Gets or sets the created by system account.
        /// </summary>
        /// <value>
        /// The created by system account.
        /// </value>
        [NotNull]
        public virtual SystemAccount CreatedBySystemAccount
        {
            get { return _createdBySystemAccount; }
            protected set { _createdBySystemAccount = value; }
        }

        /// <summary>
        /// Gets the updated timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset UpdatedTimestamp
        {
            get { return _updatedTimestamp; }
            private set { _updatedTimestamp = value; }
        }

        /// <summary>
        /// Gets or sets the updated by system account.
        /// </summary>
        /// <value>
        /// The updated by system account.
        /// </value>
        [NotNull]
        public virtual SystemAccount UpdatedBySystemAccount
        {
            get { return _updatedBySystemAccount; }
            protected set { _updatedBySystemAccount = value; }
        }

        #endregion
    }
}
