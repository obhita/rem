namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for processing a <see cref="IRule">rule</see>.
    /// </summary>
    public interface IRuleProcessor
    {
        #region Public Methods

        /// <summary>
        /// Processes the rule using the context.
        /// </summary>
        /// <param name="ruleEngineContext"><see cref="IRuleEngineContext">Rule Engine Context</see> to use when processing the rule.</param>
        /// <param name="rule">The <see cref="IRule">Rule</see> to process.</param>
        void Process ( IRuleEngineContext ruleEngineContext, IRule rule );

        #endregion
    }
}
