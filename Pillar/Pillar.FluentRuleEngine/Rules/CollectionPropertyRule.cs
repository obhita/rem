using System;
using System.Collections;
using System.Linq.Expressions;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// A collection property rule that manages creating/executing a sub Rule Collection on an IEnumerable property of a Subject.
    /// </summary>
    public class CollectionPropertyRule : PropertyRuleBase, ICollectionPropertyRule
    {
        private Action<IRuleEngineContext> _executeRules;
        private IRuleSelector _ruleSelector;

        private CollectionPropertyRule ( LambdaExpression propertyExpression, string name )
            : base ( propertyExpression, name )
        {
            WhenClause = p => true;
            AddThenClause ( ExecuteThenClause );
        }

        #region Methods

        internal static CollectionPropertyRule CreateCollectionPropertyRule<T, TProperty> (
            Expression<Func<T, TProperty>> propertyExpression, string name ) where TProperty : IEnumerable
        {
            var propertyRule = new CollectionPropertyRule ( propertyExpression, name ) { PropertyValueDelegate = propertyExpression.Compile () };
            return propertyRule;
        }

        #endregion

        #region ICollectionPropertyRule Members

        /// <inheritdoc/>
        public void WithRuleCollection<T> ( IRuleCollection<T> ruleCollection, IRuleSelector ruleSelector = null )
        {
            _ruleSelector = ruleSelector;
            var ruleEngine = new RuleEngine<T> ( ruleCollection );

            _executeRules = ctx => ruleEngine.ExecuteRules ( ctx );
        }

        #endregion

        private void ExecuteThenClause ( IRuleEngineContext ruleEngineContext )
        {
            var propertyValue = PropertyValueDelegate.DynamicInvoke ( ruleEngineContext.Subject ) as IEnumerable;
            if ( propertyValue != null )
            {
                foreach ( var childSubject in propertyValue )
                {
                    var subContext = new RuleEngineContext (
                        childSubject, ruleEngineContext.RuleViolationReporter, _ruleSelector, ruleEngineContext.NameProvider, ruleEngineContext );
                    _executeRules ( subContext );
                }
            }
        }
    }
}
