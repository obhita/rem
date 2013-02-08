using System;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Interface for the builder that initializes a <see cref="IRule">rule</see>.
    /// </summary>
    /// <typeparam name="TContext">Type of the context for the rule.</typeparam>
    /// <typeparam name="TSubject">Type of subject for the rule.</typeparam>
    public interface IRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Called when [context object].
        /// </summary>
        /// <typeparam name="TContextObject">The type of the context object.</typeparam>
        /// <param name="contextObjectName">Name of the context object.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> OnContextObject<TContextObject> ( string contextObjectName = null );

        /// <summary>
        /// Adds a when clause to the rule that only takes the subject as a parameter.
        /// </summary>
        /// <param name="whenClause"><see cref="Predicate{TSubject}">Predicate</see> that takes the subject as a parameter.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder</see></returns>
        IRuleBuilder<TContext, TSubject> When ( Predicate<TSubject> whenClause );

        /// <summary>
        /// Adds a when clause to the rule that takes both the subject and the context as parameters.
        /// </summary>
        /// <param name="whenClause"><see cref="Func{TSubject,TContext,TResult}">Func</see> that takes the subject as a parameter.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder</see></returns>
        IRuleBuilder<TContext, TSubject> When(Func<TSubject, TContext, bool> whenClause);

        /// <summary>
        /// Adds the attribute.
        /// </summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>An <see cref="IRuleBuilderInitializer{TContext,TSubject}"/></returns>
        IRuleBuilderInitializer<TContext, TSubject> AddAttribute(string key, object value);
    }
}
