using System.Collections.Generic;

namespace Pillar.FluentRuleEngine.RuleSelectors
{
    /// <summary>
    /// Rule Selector that selects all rules.
    /// </summary>
    public class SelectAllRuleSelector : IRuleSelector
    {
        #region IRuleSelector Members

        /// <inheritdoc/>
        public IEnumerable<IRule> SelectRules<TSubject> ( IRuleCollection<TSubject> ruleCollection, IRuleEngineContext context  )
        {
            return ruleCollection;
        }

        #endregion
    }
}
