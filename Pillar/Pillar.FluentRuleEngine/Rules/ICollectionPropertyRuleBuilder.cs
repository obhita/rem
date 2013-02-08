using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Interface for the Builder of a <see cref="ICollectionPropertyRule">Collection Property Rule</see>.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public interface ICollectionPropertyRuleBuilder<TContext, TSubject, TProperty> : IRuleBuilder<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Sets the <see cref="IRuleCollection{TSubject}">Rule Collection</see> that will be used to run rules for each item of the collection.
        /// </summary>
        /// <param name="ruleCollection">An <see cref="IRuleCollection{TSubject}">IRuleCollection</see></param>
        /// <param name="ruleSelector">Optional <see cref="IRuleSelector">Rule selector</see> for the <paramref name="ruleCollection">ruleCollection</paramref>.</param>
        /// <returns>An <see cref="ICollectionPropertyRuleBuilder{TContext,TSubject,TProperty}">ICollectionPropertyRuleBuilder</see>.</returns>
        ICollectionPropertyRuleBuilder<TContext, TSubject, TProperty> WithRuleCollection (
            IRuleCollection<TProperty> ruleCollection, IRuleSelector ruleSelector = null );
    }
}
