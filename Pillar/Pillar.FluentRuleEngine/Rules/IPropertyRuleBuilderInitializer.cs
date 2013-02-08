using System;
using System.Linq.Expressions;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Interface for the builder initializer of a <see cref="IPropertyRule">Property Rule</see>.
    /// </summary>
    /// <typeparam name="TContext">Type of <see cref="RuleEngineContext{TSubject}">context</see> for the rule.</typeparam>
    /// <typeparam name="TSubject">Type of subject for the rule.</typeparam>
    public interface IPropertyRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Sets the property the rule is for.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property on the <typeparamref name="TSubject"/>subject.</typeparam>
        /// <param name="propertyExpression">The expression for the property.</param>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> WithProperty<TProperty> ( Expression<Func<TSubject, TProperty>> propertyExpression );
    }
}
