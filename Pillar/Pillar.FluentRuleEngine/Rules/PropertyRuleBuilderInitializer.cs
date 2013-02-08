using System;
using System.Linq.Expressions;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Builder that initializes a <see cref="IPropertyRule">Property Rule</see>.
    /// </summary>
    /// <typeparam name="TContext">Type of the <see cref="RuleEngineContext{TSubject}">Rule Engine Context</see> for rule.</typeparam>
    /// <typeparam name="TSubject">Type of the subject for the rule.</typeparam>
    public class PropertyRuleBuilderInitializer<TContext, TSubject> : IPropertyRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        #region Constants and Fields

        private readonly Action<PropertyRule> _addRuleCallBack;

        private readonly string _name;

        #endregion

        #region Constructors and Destructors

        internal PropertyRuleBuilderInitializer ( string name, Action<PropertyRule> addRuleCallBack )
        {
            _name = name;
            _addRuleCallBack = addRuleCallBack;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public IPropertyRuleBuilder<TContext, TSubject, TProperty> WithProperty<TProperty> (
            Expression<Func<TSubject, TProperty>> propertyExpression )
        {
            var propertyRule = PropertyRule.CreatePropertyRule ( propertyExpression, _name );
            _addRuleCallBack ( propertyRule );
            return new PropertyRuleBuilder<TContext, TSubject, TProperty> ( propertyRule );
        }

        #endregion
    }
}
