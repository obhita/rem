using System;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Class that has function that executes rules.
    /// </summary>
    public class RuleExecutor : IExecuteRules
    {
        /// <summary>
        /// Gets the execute rules <see cref="Func{TOBJECT,TResult}"/>.
        /// </summary>
        public Func<object, RuleExecutionResult> ExecuteRules { get; internal set; }
    }
}