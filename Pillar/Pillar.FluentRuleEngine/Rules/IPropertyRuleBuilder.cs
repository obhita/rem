using System;
using Pillar.FluentRuleEngine.Constraints;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Interface for the builder of a <see cref="IPropertyRule">property rule</see>.
    /// </summary>
    /// <typeparam name="TContext">Type of the <see cref="RuleEngineContext{TSubject}">context</see> of the rule.</typeparam>
    /// <typeparam name="TSubject">Type of the subject of the rule.</typeparam>
    /// <typeparam name="TProperty">Type of the property of the <typeparamref name="TSubject">subject</typeparamref>.</typeparam>
    public interface IPropertyRuleBuilder<TContext, TSubject, out TProperty>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Gets the ProperyRule the Builder is building.
        /// </summary>
        IPropertyRule PropertyRule { get; }

        /// <summary>
        /// Adds a Constraint to the rule.
        /// </summary>
        /// <param name="constraint"><see cref="IConstraint">Constraint</see> to add to rule.</param>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> Constrain ( IConstraint constraint );

        /// <summary>
        /// Adds an elsethen <see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to the <see cref="IPropertyRule">Rule</see>.
        /// </summary>
        /// <param name="elseThenClause"><see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to add to <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> ElseThen ( Action<TSubject> elseThenClause );

        /// <summary>
        /// Adds a then <see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to the <see cref="IPropertyRule">Rule</see>.
        /// </summary>
        /// <param name="thenClause"><see cref="Action{TSubject}">Action&lt;TSubject&gt;</see> to add to <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> Then ( Action<TSubject> thenClause );

        /// <summary>
        /// Adds an elsethen <see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to the <see cref="IRule">Rule</see>.
        /// </summary>
        /// <param name="elseThenClause"><see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to add to <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> ElseThen ( Action<TSubject, TContext> elseThenClause );

        /// <summary>
        /// Adds a then <see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to the <see cref="IRule">Rule</see>.
        /// </summary>
        /// <param name="thenClause"><see cref="Action{TSubject,TContext}">Action&lt;TSubject,TContext&gt;</see> to add to the <see cref="IRule">Rule</see>.</param>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> Then ( Action<TSubject, TContext> thenClause );

        /// <summary>
        /// Configures the Rule to not be run if a <see cref="RuleViolation">Rule Violation</see> has occured.
        /// </summary>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> DoNotRunIfHasRuleViolation ();

        /// <summary>
        /// Configures the priority of the Rule.
        /// </summary>
        /// <param name="priority">The <see cref="int">int</see> priority of the rule.</param>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> WithPriority ( int priority );

        /// <summary>
        /// Updates Auto Validation of rule.
        /// </summary>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> AutoValidate ();

        /// <summary>
        /// Updates Auto Validation of rule.
        /// </summary>
        /// <returns>An <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}">IPropertyRuleBuilder&lt;TContext,TSubject,TProperty&gt;</see>.</returns>
        IPropertyRuleBuilder<TContext, TSubject, TProperty> DoNotAutoValidate ();
    }
}
