using System;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Rule Violation from a rule.
    /// </summary>
    public class RuleViolation
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleViolation"/> class.
        /// </summary>
        /// <param name="rule"><see cref="IRule">Rule</see> that caused the violation.</param>
        /// <param name="offendingObject">Object that caused the violation.</param>
        /// <param name="message">Message for the Violation.</param>
        /// <param name="propertyNames">Property names of <paramref name="offendingObject">object</paramref> that caused the rule violation.</param>
        public RuleViolation ( IRule rule, object offendingObject, string message, params string[] propertyNames )
        {
            Rule = rule;
            OffendingObject = offendingObject;
            Message = message;
            PropertyNames = propertyNames;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Rule Violation Message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets teh Offending Object of the violation.
        /// </summary>
        public object OffendingObject { get; private set; }

        /// <summary>
        /// Gets the Property Names of the Offending Object of the Violation.
        /// </summary>
        public string[] PropertyNames { get; private set; }

        /// <summary>
        /// Gets the <see cref="IRule">Rule</see> that caused the Violation.
        /// </summary>
        public IRule Rule { get; private set; }

        #endregion
    }
}
