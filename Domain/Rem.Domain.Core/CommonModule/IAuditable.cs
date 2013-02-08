using System;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// IAuditable interface defines auditable attributes.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets the created timestamp.
        /// </summary>
        DateTimeOffset CreatedTimestamp { get; }

        /// <summary>
        /// Gets the created by system account.
        /// </summary>
        SystemAccount CreatedBySystemAccount { get; }

        /// <summary>
        /// Gets the updated timestamp.
        /// </summary>
        DateTimeOffset UpdatedTimestamp { get; }

        /// <summary>
        /// Gets the updated by system account.
        /// </summary>
        SystemAccount UpdatedBySystemAccount { get; }
    }
}
