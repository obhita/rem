using System;
using Pillar.Common.Extension;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiInterviewSectionAggregateNodeBase provides a base aggregate node implementation.
    /// </summary>
    public abstract class DensAsiInterviewSectionAggregateNodeBase : IAggregateNode, IAuditable
    {
        private DateTimeOffset _createdTimestamp;
        private SystemAccount _createdBySystemAccount;
        private DateTimeOffset _updatedTimestamp;
        private SystemAccount _updatedBySystemAccount;

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiInterviewSectionAggregateNodeBase"/> class.
        /// </summary>
        protected DensAsiInterviewSectionAggregateNodeBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiInterviewSectionAggregateNodeBase"/> class.
        /// </summary>
        /// <param name="densAsiInterview">The dens asi interview.</param>
        protected DensAsiInterviewSectionAggregateNodeBase( DensAsiInterview densAsiInterview )
        {
            DensAsiInterview = densAsiInterview;
        }

        /// <summary>
        /// Gets or sets the dens asi interview.
        /// </summary>
        /// <value>
        /// The dens asi interview.
        /// </value>
        [NotNull]
        public virtual DensAsiInterview DensAsiInterview { get; protected internal set; }

        #region Implementation of IAuditable

        /// <summary>
        /// Gets the created timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset CreatedTimestamp
        {
            get
            {
                return _createdTimestamp;
            }

            private set
            {
                _createdTimestamp = value;
            }
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
            get
            {
                return _createdBySystemAccount;
            }

            protected set
            {
                _createdBySystemAccount = value;
            }
        }

        /// <summary>
        /// Gets the updated timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset UpdatedTimestamp
        {
            get
            {
                return _updatedTimestamp;
            }

            private set
            {
                _updatedTimestamp = value;
            }
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
            get
            {
                return _updatedBySystemAccount;
            }

            protected set
            {
                _updatedBySystemAccount = value;
            }
        }

        #endregion

        /// <summary>
        /// Gets the aggregate node's aggregate root.
        /// </summary>
        public virtual IAggregateRoot AggregateRoot
        {
            get
            {
                return DensAsiInterview;
            }
        }

        /// <summary>
        /// Gets the aggregate node's key.
        /// </summary>
        [IgnoreMapping]
        public virtual long Key
        {
            get
            {
                return DensAsiInterview.Key;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return GetType().Name.SeparatePascalCaseWords ();
        }
    }
}