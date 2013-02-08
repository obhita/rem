using System.Collections.Generic;
using System.Linq;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Results of a Rule Engine Execution.
    /// </summary>
    public class RuleExecutionResult
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleExecutionResult"/> class.
        /// </summary>
        /// <param name="ruleViolations">List of <see cref="RuleViolation">rule violations</see> that occured to rule engine execution.</param>
        public RuleExecutionResult ( IEnumerable<RuleViolation> ruleViolations )
        {
            RuleViolations = ruleViolations ?? Enumerable.Empty<RuleViolation> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the list of <see cref="RuleViolation">rule violations</see>.
        /// </summary>
        public IEnumerable<RuleViolation> RuleViolations { get; private set; }

        /// <summary>
        /// Gets whether has <see cref="RuleViolation">rule violations</see>.
        /// </summary>
        public bool HasRuleViolation
        {
            get { return RuleViolations.Count () > 0; }
        }

        #endregion
    }
}
