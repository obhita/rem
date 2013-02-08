using System.Linq;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Class used to manage the runing of rules contained in <see cref="IRuleCollection{TSubject}">Rule Collections</see>.
    /// </summary>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    public class RuleEngine<TSubject> : IRuleEngine<TSubject>
    {
        #region Constants and Fields

        private readonly IRuleCollection<TSubject> _ruleCollection;

        private readonly IRuleProcessor _ruleProcessor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngine&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="ruleCollection"><see cref="IRuleCollection{TSubject}">Rule Collection</see> containing the list of rules that will be executed.</param>
        public RuleEngine ( IRuleCollection<TSubject> ruleCollection )
            : this ( ruleCollection, new RuleProcessor () )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngine&lt;TSubject&gt;"/> class.
        /// </summary>
        /// <param name="ruleCollection"><see cref="IRuleCollection{TSubject}">Rule Collection</see> containing the list of rules that will be executed.</param>
        /// <param name="ruleProcessor"><see cref="IRuleProcessor">Rule Processor</see> to use to process each rule when executed.</param>
        public RuleEngine ( IRuleCollection<TSubject> ruleCollection, IRuleProcessor ruleProcessor )
        {
            Check.IsNotNull ( ruleCollection, "ruleCollection is required." );
            Check.IsNotNull ( ruleProcessor, "ruleProcessor is required." );

            _ruleCollection = ruleCollection;
            _ruleProcessor = ruleProcessor;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Exectues all rules in the Rule Collection of the Rule Engine.
        /// </summary>
        /// <param name="subject">Subject the rules are run against.</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results of the execution pass.</returns>
        public RuleExecutionResult ExecuteAllRules ( TSubject subject )
        {
            var ruleEngineContext = new RuleEngineContext<TSubject> ( subject );
            return ExecuteRules ( ruleEngineContext );
        }

        /// <summary>
        /// Exectutes rules selected by the rule selector in the Rule Collection of the Rule Engine.
        /// </summary>
        /// <param name="subject">Subject the rules are run against.</param>
        /// <param name="ruleSelector">The <see cref="IRuleSelector">Rule Selector</see> to determin the list of rules that will be executed.</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results of the execution pass.</returns>
        public RuleExecutionResult ExecuteSelectedRules ( TSubject subject, IRuleSelector ruleSelector )
        {
            var ruleEngineContext = new RuleEngineContext<TSubject> ( subject, ruleSelector );
            return ExecuteRules ( ruleEngineContext );
        }

        /// <summary>
        /// Exectutes rules that are part of a Rule Set in the Rule Collection of the Rule Engine.
        /// </summary>
        /// <param name="subject">Subject the rules are run against.</param>
        /// <param name="ruleSetName">The Rule Set name of the rules to execute..</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results of the execution pass.</returns>
        public RuleExecutionResult ExecuteRuleSet ( TSubject subject, string ruleSetName )
        {
            var ruleSetSelector = new SelectAllRulesInRuleSetSelector ( ruleSetName );
            return ExecuteSelectedRules ( subject, ruleSetSelector );
        }

        /// <summary>
        /// Executes Rules using specified <see cref="IRuleEngineContext"/>
        /// </summary>
        /// <param name="ruleEngineContext">Context to use when executing rules.</param>
        /// <returns>A <see cref="RuleExecutionResult"/> containing the results of the execution pass.</returns>
        public RuleExecutionResult ExecuteRules ( IRuleEngineContext ruleEngineContext )
        {
            var rules = ruleEngineContext.RuleSelector.SelectRules ( _ruleCollection, ruleEngineContext )
                .Where ( r => !r.IsDisabled )
                .OrderBy ( r => r.Priority )
                .ToList();
            rules.ForEach ( rule => _ruleProcessor.Process ( ruleEngineContext, rule ) );

            var result = new RuleExecutionResult ( ruleEngineContext.RuleViolationReporter );

            return result;
        }

        /// <summary>
        /// Creates the typed rule engine.
        /// </summary>
        /// <returns>A <see cref="IRuleEngine{TSubject}"/></returns>
        public static IRuleEngine<TSubject> CreateTypedRuleEngine ()
        {
            var ruleEngineFactory = new RuleEngineFactory ( new RuleCollectionFactory () );
            return ruleEngineFactory.CreateRuleEngine<TSubject> ();
        }

        #endregion
    }
}
