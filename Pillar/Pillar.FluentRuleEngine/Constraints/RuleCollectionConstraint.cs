using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires object to satisfy all rules in a Rule Collection.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class RuleCollectionConstraint<TProperty> : ConstraintBase, IHandleAddingRuleViolations
    {
        private readonly IRuleCollection<TProperty> _ruleCollection;
        private readonly IRuleSelector _ruleSelector;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleCollectionConstraint&lt;TProperty&gt;"/> class.
        /// </summary>
        /// <param name="ruleCollection">Rule Collection property must satisfy.</param>
        /// <param name="ruleSelector">Optional Rule selector for selecting rules in <paramref name="ruleCollection"/></param>
        public RuleCollectionConstraint ( IRuleCollection<TProperty> ruleCollection, IRuleSelector ruleSelector = null )
            : base ( null )
        {
            _ruleCollection = ruleCollection;
            _ruleSelector = ruleSelector;
        }

        /// <inheritdoc/>
        public override bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext )
        {
            var propertyRuleEngineContext = new RuleEngineContext<TProperty> (
                ( TProperty )propertyValue, ruleEngineContext.RuleViolationReporter, _ruleSelector, ruleEngineContext.NameProvider, ruleEngineContext );
            propertyRuleEngineContext.WorkingMemory.AddContextObject ( ruleEngineContext.Subject );
            var ruleEngine = new RuleEngine<TProperty> ( _ruleCollection );
            var result = ruleEngine.ExecuteRules ( propertyRuleEngineContext );
            return !result.HasRuleViolation;
        }
    }
}
