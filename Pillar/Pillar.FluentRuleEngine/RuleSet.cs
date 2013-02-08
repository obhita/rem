using System.Collections.Generic;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// A collection of <see cref="IRule">Rules</see>.
    /// </summary>
    public class RuleSet : List<IRule>, IRuleSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleSet"/> class.
        /// </summary>
        /// <param name="name">Name of Rule Set.</param>
        public RuleSet ( string name )
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleSet"/> class.
        /// </summary>
        /// <param name="name">Name of Rule Set.</param>
        /// <param name="capacity">Initial capacity of the Rule Set.</param>
        public RuleSet ( string name, int capacity )
            : base ( capacity )
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleSet"/> class.
        /// </summary>
        /// <param name="name">Name of the Rule Set.</param>
        /// <param name="collection">Initial rules to add to the Rule Set.</param>
        public RuleSet ( string name, IEnumerable<IRule> collection )
            : base ( collection )
        {
            Name = name;
        }

        #region IRuleSet Members

        /// <inheritdoc/>
        public string Name { get; private set; }

        #endregion

        /// <summary>
        /// Adds a <see cref="IRule">Rule</see> to the rule set.
        /// </summary>
        /// <param name="rule">Rule to add to Rule Set.</param>
        /// <exception cref="T:System.ArgumentException">Rule is required.</exception>
        public new void Add ( IRule rule )
        {
            Check.IsNotNull ( rule, "Rule is required." );

            base.Add ( rule );
        }
    }
}
