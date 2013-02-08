using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pillar.FluentRuleEngine.Constraints;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// A rule builder that also provides a context object.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <typeparam name="TContextObject">The type of the context object.</typeparam>
    public interface IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> : IRuleBuilder<TContext, TSubject>,
                                                                                             IRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Gets the type of the context object.
        /// </summary>
        /// <value>
        /// The type of the context object.
        /// </value>
        Type ContextObjectType { get; }

        /// <summary>
        /// Gets the name of the context object.
        /// </summary>
        /// <value>
        /// The name of the context object.
        /// </value>
        string ContextObjectName { get; }

        /// <summary>
        /// Gets the constraints.
        /// </summary>
        IEnumerable<IConstraint> Constraints { get; }

        /// <summary>
        /// Gets the context object.
        /// </summary>
        /// <param name="workingMemory">The workingMemory to get the context object from.</param>
        /// <returns>A context object.</returns>
        object GetContextObject ( WorkingMemory workingMemory );

        /// <summary>
        /// Withes the property.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> WithProperty (
            Expression<Func<TContextObject, object>> propertyExpression );

        /// <summary>
        /// Adds Constraint to Builder.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> Constrain ( IConstraint constraint );
    }
}
