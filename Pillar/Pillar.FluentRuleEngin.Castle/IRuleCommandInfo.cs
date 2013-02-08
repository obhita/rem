using Pillar.Common.Commands;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for rule specific command information.
    /// </summary>
    public interface IRuleCommandInfo : IFrameworkCommandInfo
    {
        /// <summary>
        /// Gets the rule selector.
        /// </summary>
        IRuleSelector RuleSelector { get; }
    }
}
