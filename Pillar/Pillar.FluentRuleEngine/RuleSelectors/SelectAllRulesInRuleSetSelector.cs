using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine.RuleSelectors
{
    /// <summary>
    /// Rule Selector that selects rules within a Rule Set.
    /// </summary>
    public class SelectAllRulesInRuleSetSelector : IRuleSelector
    {
        #region Constants and Fields

        private readonly IRuleSet _ruleSet;
        private readonly string _ruleSetName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAllRulesInRuleSetSelector"/> class.
        /// </summary>
        /// <param name="ruleSetName">Name of <see cref="IRuleSet">Rule Set</see> to select.</param>
        public SelectAllRulesInRuleSetSelector ( string ruleSetName )
        {
            _ruleSetName = ruleSetName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectAllRulesInRuleSetSelector"/> class.
        /// </summary>
        /// <param name="ruleSet"><see cref="IRuleSet">Rule Set</see> to select.</param>
        public SelectAllRulesInRuleSetSelector ( IRuleSet ruleSet )
        {
            Check.IsNotNull ( ruleSet, "RuleSet is required." );
            _ruleSet = ruleSet;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public IEnumerable<IRule> SelectRules<TSubject> ( IRuleCollection<TSubject> ruleCollection, IRuleEngineContext context )
        {
            IEnumerable<IRule> rulesToRun = _ruleSet;

            if ( rulesToRun == null )
            {
                var propertyInfo = ruleCollection.GetType ().GetProperty ( _ruleSetName );
                if ( propertyInfo != null )
                {
                    rulesToRun = propertyInfo.GetValue ( ruleCollection, null ) as IEnumerable<IRule>;
                }
            }

            return rulesToRun ?? Enumerable.Empty<IRule> ();
        }

        #endregion
    }
}
