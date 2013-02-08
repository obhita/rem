using System.Collections.Generic;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// An <see cref="IEnumerable{T}">IEnumerable&lt;<see cref="RuleViolation">RuleVioloation</see>&gt;</see> that
    /// also allows for the
    /// </summary>
    public interface IRuleViolationReporter : IEnumerable<RuleViolation>
    {
        /// <summary>
        /// Gets a value indicating whether this instance has rule violation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has a rule violation; otherwise, <c>false</c>.
        /// </value>
        bool HasRuleViolation { get; }

        /// <summary>
        /// Reports a <see cref="RuleViolation">RuleVioloation</see> to the RuleViolationReporter
        /// </summary>
        /// <param name="ruleViolation"><see cref="RuleViolation">RuleVioloation</see> to report.</param>
        void Report ( RuleViolation ruleViolation );

        /// <summary>
        /// Clears any current rule violations.
        /// </summary>
        void ClearViolations ();
    }
}
