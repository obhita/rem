using Pillar.FluentRuleEngine.RuleSelectors;
using Pillar.FluentRuleEngine.Rules;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for a <see cref="CollectionPropertyRule">CollectionPropertyRule</see>
    /// This is a rule that tells the engine which <see cref="IRuleCollection{TSubject}">rule collection</see> to use when this rule is run.
    /// </summary>
    public interface ICollectionPropertyRule : IRule
    {
        /// <summary>
        /// Sets the <see cref="IRuleCollection{TSubject}">Rule Collection</see> and optionally the <see cref="IRuleSelector">RuleSelector</see> to use.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property of TSubject.</typeparam>
        /// <param name="ruleCollection"><see cref="IRuleCollection{TSubject}">Rule Collection</see> to run.</param>
        /// <param name="ruleSelector">Optional <see cref="IRuleSelector">Rule Selector</see> for the <paramref name="ruleCollection">Rule Collection</paramref></param>
        void WithRuleCollection<TProperty> ( IRuleCollection<TProperty> ruleCollection, IRuleSelector ruleSelector = null );
    }
}
