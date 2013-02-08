using Pillar.Common.Commands;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Info object for executing rules on commands.
    /// </summary>
    public class RuleCommandInfo : FrameworkCommandInfo, IRuleCommandInfo
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCommandInfo"/> class.
        /// </summary>
        /// <param name="owner">The owner of the command.</param>
        /// <param name="name">The name of the command.</param>
        /// <param name="ruleSelector">The rule selector.</param>
        public RuleCommandInfo ( object owner, string name, IRuleSelector ruleSelector )
            : base ( owner, name )
        {
            RuleSelector = ruleSelector;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the rule selector.
        /// </summary>
        public IRuleSelector RuleSelector { get; private set; }

        #endregion
    }
}
