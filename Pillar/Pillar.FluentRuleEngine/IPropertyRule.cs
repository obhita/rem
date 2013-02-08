using System.Collections.Generic;
using System.Linq.Expressions;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Rules;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for a <see cref="PropertyRule">Property Rule</see>.
    /// </summary>
    public interface IPropertyRule : IRule
    {
        /// <summary>
        /// Gets the <see cref="IEnumerable{T}">constraints</see> of the rule.
        /// </summary>
        IEnumerable<IConstraint> Constraints { get; }

        /// <summary>
        /// Gets the PropertyExpression of the rule.
        /// </summary>
        LambdaExpression PropertyExpression { get; }

        /// <summary>
        /// Gets the PropertyChain of the rule.
        /// </summary>
        PropertyChain PropertyChain { get; }
    }
}
