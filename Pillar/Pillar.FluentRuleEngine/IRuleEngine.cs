using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interface for Defining a RuleEngine for running rules in a RuleCollection.
    /// </summary>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    public interface IRuleEngine<in TSubject>
    {
        /// <summary>
        /// Executes All Rules in the Rule Collection.
        /// </summary>
        /// <param name="subject">Subject to run rules on.</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results from executing the Rules.</returns>
        RuleExecutionResult ExecuteAllRules ( TSubject subject );

        /// <summary>
        /// Exectutes rules selected by the rule selector in the Rule Collection of the Rule Engine.
        /// </summary>
        /// <param name="subject">Subject the rules are run against.</param>
        /// <param name="ruleSelector">The <see cref="IRuleSelector">Rule Selector</see> to determin the list of rules that will be executed.</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results of the execution pass.</returns>
        RuleExecutionResult ExecuteSelectedRules ( TSubject subject, IRuleSelector ruleSelector );

        /// <summary>
        /// Exectutes rules that are part of a Rule Set in the Rule Collection of the Rule Engine.
        /// </summary>
        /// <param name="subject">Subject the rules are run against.</param>
        /// <param name="ruleSetName">The Rule Set name of the rules to execute..</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results of the execution pass.</returns>
        RuleExecutionResult ExecuteRuleSet ( TSubject subject, string ruleSetName );

        /// <summary>
        /// Executes Rules using specified <see cref="IRuleEngineContext"/>
        /// </summary>
        /// <param name="ruleEngineContext">Context to use when executing rules.</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results of the execution pass.</returns>
        RuleExecutionResult ExecuteRules ( IRuleEngineContext ruleEngineContext );
    }
}
