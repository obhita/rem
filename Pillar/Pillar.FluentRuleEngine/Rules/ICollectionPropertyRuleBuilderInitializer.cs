using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Interface for the buider initializer of a <see cref="ICollectionPropertyRule">Collection Property Rule</see>.
    /// </summary>
    /// <typeparam name="TContext">Type of the <see cref="RuleEngineContext{TSubject}">context</see> of the rule.</typeparam>
    /// <typeparam name="TSubject">Type of the subject of the rule.</typeparam>
    public interface ICollectionPropertyRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Sets the property expression for the rule.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property of the <typeparamref name="TSubject">subject</typeparamref>.</typeparam>
        /// <param name="propertyExpression">Property Expression to the property.</param>
        /// <returns>A <see cref="ICollectionPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        ICollectionPropertyRuleBuilder<TContext, TSubject, TProperty> WithProperty<TProperty> (
            Expression<Func<TSubject, IEnumerable<TProperty>>> propertyExpression );
    }
}
