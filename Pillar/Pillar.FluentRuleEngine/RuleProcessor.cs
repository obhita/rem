using Pillar.Common.Extension;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Processor that manages executing a singe rule.
    /// </summary>
    public class RuleProcessor : IRuleProcessor
    {
        #region IRuleProcessor Members

        /// <inheritdoc/>
        public void Process ( IRuleEngineContext ruleEngineContext, IRule rule )
        {
            Check.IsNotNull ( ruleEngineContext, "ruleEngineContext is required." );
            Check.IsNotNull ( rule, "rule is required." );

            if ( rule.ShouldRunRule ( ruleEngineContext ) )
            {
                var whenClauseResult = rule.WhenClause ( ruleEngineContext );
                if ( whenClauseResult )
                {
                    rule.ThenClauses.ForEach ( t => t ( ruleEngineContext ) );
                }
                else
                {
                    rule.ElseThenClauses.ForEach ( t => t ( ruleEngineContext ) );
                }
            }
        }

        #endregion
    }
}
