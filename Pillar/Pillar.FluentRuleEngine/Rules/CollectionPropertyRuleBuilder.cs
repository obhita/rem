using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// A Builder for a <see cref="ICollectionPropertyRule">Collection Property Rule</see>.
    /// </summary>
    /// <typeparam name="TContext">The type of the <see cref="RuleEngineContext{TSubject}">RuleEngingContext</see> for the rule.</typeparam>
    /// <typeparam name="TSubject">The type of the subject for the rule.</typeparam>
    /// <typeparam name="TProperty">The type of the property of the <typeparamref name="TSubject">TSubject</typeparamref> for the rule</typeparam>
    public class CollectionPropertyRuleBuilder<TContext, TSubject, TProperty> : RuleBuilder<TContext, TSubject>,
                                                                                ICollectionPropertyRuleBuilder<TContext, TSubject, TProperty>
        where TContext : RuleEngineContext<TSubject>
    {
        private readonly CollectionPropertyRule _collectionPropertyRule;

        internal CollectionPropertyRuleBuilder ( CollectionPropertyRule collectionPropertyRule )
            : base ( collectionPropertyRule )
        {
            _collectionPropertyRule = collectionPropertyRule;
        }

        #region ICollectionPropertyRuleBuilder<TContext,TSubject,TProperty> Members

        /// <inheritdoc/>
        public ICollectionPropertyRuleBuilder<TContext, TSubject, TProperty> WithRuleCollection (
            IRuleCollection<TProperty> ruleCollection, IRuleSelector ruleSelector = null )
        {
            _collectionPropertyRule.WithRuleCollection ( ruleCollection, ruleSelector );
            return this;
        }

        #endregion
    }
}
