using System.Linq;
using System.Windows.Input;
using Castle.DynamicProxy;
using Pillar.Common.Commands;
using Pillar.Common.Interceptors;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Interceptor for executing rules when a Command is Executed.
    /// </summary>
    public class CheckRulesInterceptor : IIntercept<ICommand>, IInterceptor
    {
        private readonly IRuleEngineFactory _ruleEngineFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckRulesInterceptor"/> class.
        /// </summary>
        /// <param name="ruleEngineFactory">The rule engine factory.</param>
        public CheckRulesInterceptor(IRuleEngineFactory ruleEngineFactory)
        {
            _ruleEngineFactory = ruleEngineFactory;
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept ( IInvocation invocation )
        {
            if (invocation.Method.Name == "Execute")
            {
                if (invocation.Proxy is IExecuteRules)
                {
                    var parameter = invocation.Arguments.Count() > 0 ? invocation.GetArgumentValue ( 0 ) : null;

                    var ruleExecutor = invocation.Proxy as IExecuteRules;
                    var result = ruleExecutor.ExecuteRules ( parameter );
                    if(!result.HasRuleViolation)
                    {
                        invocation.Proceed();
                    }
                }
            }
            else
            {
                invocation.Proceed();
            }
        }

        /// <summary>
        /// Gets the interceptor options.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="frameworkCommandInfo">The framework command info.</param>
        /// <returns>Options object to add to Command.</returns>
        public object GetInterceptorOptions<TOwner> ( IFrameworkCommandInfo frameworkCommandInfo )
        {
            IRuleSelector ruleSelector;

            if(frameworkCommandInfo is IRuleCommandInfo)
            {
                ruleSelector = ( frameworkCommandInfo as IRuleCommandInfo ).RuleSelector;
            }
            else
            {
                ruleSelector =
                    new SelectAllRulesInRuleSetSelector (
                        frameworkCommandInfo.Name.EndsWith ( "RuleSet" ) ? frameworkCommandInfo.Name : frameworkCommandInfo.Name + "RuleSet" );
            }

            IRuleEngine<TOwner> ruleEngine = null; 
            var triedCreateRuleEngine = false;

            var context = new RuleEngineContext<TOwner> ( ( TOwner )frameworkCommandInfo.Owner, ruleSelector );

            var ruleExecutor = new RuleExecutor
                {
                    ExecuteRules = o =>
                        {
                            if(!triedCreateRuleEngine)
                            {
                                triedCreateRuleEngine = true;
                                ruleEngine = _ruleEngineFactory.CreateRuleEngine<TOwner>();
                            }
                            if ( ruleEngine != null )
                            {
                                context.Refresh ();
                                if ( o != null )
                                {
                                    context.WorkingMemory.AddContextObject ( o );
                                    if ( context.RuleSelector is ITakeParameter )
                                    {
                                        ( context.RuleSelector as ITakeParameter ).Parameter = o;
                                    }
                                }
                                return ruleEngine.ExecuteRules ( context );
                            }
                            return new RuleExecutionResult ( Enumerable.Empty<RuleViolation> () );
                        }
                };

            return ruleExecutor;
        }
    }
}
