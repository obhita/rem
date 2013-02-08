using System.Collections.Generic;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for the Collection of Rules.
    /// </summary>
    /// <typeparam name="TSubject">Type of the subject of the Rule Collection.</typeparam>
    public interface IRuleCollection<TSubject> : IEnumerable<IRule>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        bool IsInitialized { get; set; }
    }
}
