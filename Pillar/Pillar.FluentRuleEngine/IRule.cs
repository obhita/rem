using System;
using System.Collections.Generic;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for a Rule.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Gets the name of the rule.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="Predicate{T}">WhenClause</see> of the rule.
        /// </summary>
        Predicate<IRuleEngineContext> WhenClause { get; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}">List of ThenClauses</see> of the rule.
        /// </summary>
        IEnumerable<Action<IRuleEngineContext>> ThenClauses { get; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}">List of ElseThenClauses</see> of the rule.
        /// </summary>
        IEnumerable<Action<IRuleEngineContext>> ElseThenClauses { get; }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        IAttributeCollection Attributes { get; }

        /// <summary>
        /// Gets or Sets whether the rule is dissabled.
        /// Rule will not run if dissabled.
        /// </summary>
        bool IsDisabled { get; set; }

        /// <summary>
        /// Gets the priority of the rule.
        /// Dictates the order the rule will get run (lower runs first).
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Gets whether rule should be run in this context.
        /// </summary>
        /// <param name="ruleEngineContext">RuleEngineContext of current run.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        bool ShouldRunRule ( IRuleEngineContext ruleEngineContext );
    }
}
