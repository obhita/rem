using System;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Factory that creates a <see cref="RuleEngineContext{TSubject}"/>
    /// </summary>
    public class RuleEngineFactory : IRuleEngineFactory
    {
        private readonly IRuleCollectionFactory _ruleCollectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineFactory"/> class.
        /// </summary>
        /// <param name="ruleCollectionFactory">The rule collection factory.</param>
        public RuleEngineFactory ( IRuleCollectionFactory ruleCollectionFactory )
        {
            _ruleCollectionFactory = ruleCollectionFactory;
        }

        #region IRuleEngineFactory Members

        /// <summary>
        /// Creates the <see cref="RuleEngine{TSubject}">Rule Engine</see> using the <see cref="IRuleCollection{TSubject}">Rule Collection</see> passed into the constructor.
        /// </summary>
        /// <typeparam name="TSubject">The Type of the subject for the rules.</typeparam>
        /// <returns>A <see cref="IRuleEngine{TSubject}">Rule Engine</see>.</returns>
        public IRuleEngine<TSubject> CreateRuleEngine<TSubject> ()
        {
            IRuleEngine<TSubject> ruleEngine;
            try
            {
                var ruleCollection = _ruleCollectionFactory.CreateRuleCollection<TSubject>();
                ruleEngine = new RuleEngine<TSubject>(ruleCollection);
            }
            catch ( Exception )
            {
                ruleEngine = null;
            }
            return ruleEngine;
        }

        /// <summary>
        /// Creates the <see cref="RuleEngine{TSubject}">Rule Engine</see> using the <see cref="IRuleCollection{TSubject}">Rule Collection</see> passed into the constructor.
        /// </summary>
        /// <typeparam name="TSubject">The Type of the subject for the rules.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <returns>
        /// A <see cref="IRuleEngine{TSubject}">Rule Engine</see>.
        /// </returns>
        public IRuleEngine<TSubject> CreateRuleEngine<TSubject> ( TSubject subject )
        {
            return CreateRuleEngine<TSubject> ();
        }

        #endregion
    }
}
