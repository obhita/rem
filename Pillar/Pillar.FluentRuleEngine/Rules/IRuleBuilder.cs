using System;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Interface for a class that builds up a <see cref="IRule">Rule.</see>
    /// </summary>
    /// <typeparam name="TContext">Type of the context for the rule.</typeparam>
    /// <typeparam name="TSubject">Type of the subject for the rule.</typeparam>
    public interface IRuleBuilder<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Gets the <see cref="IRule">Rule</see> the builder is building.
        /// </summary>
        IRule Rule { get; }

        #region Public Methods

        /// <summary>
        /// Adds an elsethen <see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to the <see cref="IRule">Rule</see>.
        /// </summary>
        /// <param name="elseThenClause"><see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to add to <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder&lt;TContext,TSubject&gt;</see>.</returns>
        IRuleBuilder<TContext, TSubject> ElseThen ( Action<TSubject> elseThenClause );

        /// <summary>
        /// Adds a then <see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to the <see cref="IRule">Rule</see>.
        /// </summary>
        /// <param name="thenClause"><see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to add to <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder&lt;TContext,TSubject&gt;</see>.</returns>
        IRuleBuilder<TContext, TSubject> Then ( Action<TSubject> thenClause );

        /// <summary>
        /// Adds an elsethen <see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to the <see cref="IRule">Rule</see>.
        /// </summary>
        /// <param name="elseThenClause"><see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to add to <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder&lt;TContext,TSubject&gt;</see>.</returns>
        IRuleBuilder<TContext, TSubject> ElseThen ( Action<TSubject, TContext> elseThenClause );

        /// <summary>
        /// Adds a then <see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to the <see cref="IRule">Rule</see>.
        /// </summary>
        /// <param name="thenClause"><see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to add to <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder&lt;TContext,TSubject&gt;</see>.</returns>
        IRuleBuilder<TContext, TSubject> Then ( Action<TSubject, TContext> thenClause );

        /// <summary>
        /// Configures the Rule to not be run if a <see cref="RuleViolation">Rule Violation</see> has occured.
        /// </summary>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder&lt;TContext,TSubject&gt;</see>.</returns>
        IRuleBuilder<TContext, TSubject> DoNotRunIfHasRuleViolation ();

        /// <summary>
        /// Configures the priority of the Rule.
        /// </summary>
        /// <param name="priority">The <see cref="int">int</see> priority of the rule.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder&lt;TContext,TSubject&gt;</see>.</returns>
        IRuleBuilder<TContext, TSubject> WithPriority ( int priority );

        #endregion
    }
}
