using System.Collections.Generic;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// A <see cref="IRuleViolationReporter">Rule Violation Reporter</see> that is a list of the <see cref="RuleViolation">rule violations</see> that have been reported.
    /// </summary>
    public class RuleViolationCollection : List<RuleViolation>, IRuleViolationReporter
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance has rule violation.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has a rule violation; otherwise, <c>false</c>.
        /// </value>
        public bool HasRuleViolation
        {
            get { return Count > 0; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a <see cref="RuleViolation"/> to the RuleViolationCollection.
        /// </summary>
        /// <param name="ruleViolation"><see cref="RuleViolation"/> to add.</param>
        public void Report ( RuleViolation ruleViolation )
        {
            Add ( ruleViolation );
        }

        /// <summary>
        /// Clears any current rule violations.
        /// </summary>
        public void ClearViolations()
        {
            Clear ();
        }

        #endregion
    }
}
