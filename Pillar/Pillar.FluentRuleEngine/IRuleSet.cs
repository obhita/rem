using System.Collections.Generic;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for a set of rules.
    /// </summary>
    public interface IRuleSet : IEnumerable<IRule>
    {
        /// <summary>
        /// Name of the rule set.
        /// </summary>
        string Name { get; }
    }
}
