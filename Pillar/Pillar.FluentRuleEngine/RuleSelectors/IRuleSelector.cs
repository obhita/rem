using System.Collections.Generic;

namespace Pillar.FluentRuleEngine.RuleSelectors
{
    /// <summary>
    /// Interface for a Rule Selector.
    /// Used by <see cref="IRuleEngine{TSubject}">rule engine</see> to get list of rules to run.
    /// </summary>
    public interface IRuleSelector
    {
        /// <summary>
        /// Gets the list of <see cref="IRule">Rules</see> to run.
        /// </summary>
        /// <typeparam name="TSubject">Type of subject for <paramref name="ruleCollection">rule collection</paramref></typeparam>
        /// <param name="ruleCollection">The rule collection.</param>
        /// <param name="context">The context.</param>
        /// <returns>List of rules to run.</returns>
        IEnumerable<IRule> SelectRules<TSubject> ( IRuleCollection<TSubject> ruleCollection, IRuleEngineContext context  );
    }
}
