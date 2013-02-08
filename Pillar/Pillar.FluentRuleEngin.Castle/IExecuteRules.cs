using System;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface to executing rules.
    /// </summary>
    public interface IExecuteRules
    {
        /// <summary>
        /// Gets the execute rules <see cref="Func{TOBJECT,TResult}"/>.
        /// </summary>
        Func<object, RuleExecutionResult> ExecuteRules { get; }
    }
}
