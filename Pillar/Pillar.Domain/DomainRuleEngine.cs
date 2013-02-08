using System;
using System.Linq.Expressions;
using System.Reflection;
using Pillar.Common.Extension;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.Domain
{
    /// <summary>
    /// Helper for building Domain Rule Engines
    /// </summary>
    public class DomainRuleEngine
    {
        #region Constants and Fields

        private Func<IRuleEngineContext, RuleExecutionResult> _executeRules;
        private IRuleEngineContext _ruleEngineContext;

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the rule engine.
        /// </summary>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="ruleSetName">Name of the rule set.</param>
        /// <returns>A <see cref="DomainRuleEngine"/></returns>
        public static DomainRuleEngine CreateRuleEngine<TSubject> ( TSubject subject, string ruleSetName = null )
        {
            var ruleEngine = RuleEngine<TSubject>.CreateTypedRuleEngine ();

            var domainRuleEngine = new DomainRuleEngine
                {
                    _ruleEngineContext =
                        ruleSetName == null
                            ? new RuleEngineContext<TSubject> ( subject )
                            : new RuleEngineContext<TSubject> ( subject, new SelectAllRulesInRuleSetSelector ( ruleSetName ) ),
                    _executeRules = ruleEngine.ExecuteRules
                };
            return domainRuleEngine;
        }

        /// <summary>
        /// Creates the rule engine.
        /// </summary>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="methodExpression">The method expression.</param>
        /// <returns>A <see cref="DomainRuleEngine"/></returns>
        public static DomainRuleEngine CreateRuleEngine<TSubject> ( TSubject subject, Expression<Func<Action<TSubject>>> methodExpression )
        {
            var methodName = GetMethodName ( methodExpression );
            var domainRuleEngine = CreateRuleEngine ( subject, methodName.EndsWith ( "RuleSet" ) ? methodName : methodName + "RuleSet" );
            return domainRuleEngine;
        }

        /// <summary>
        /// Creates the rule engine.
        /// </summary>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="subject">The subject.</param>
        /// <param name="methodExpression">The method expression.</param>
        /// <returns>A <see cref="DomainRuleEngine"/></returns>
        public static DomainRuleEngine CreateRuleEngine<TSubject, TProperty> (
            TSubject subject, Expression<Func<Action<TProperty>>> methodExpression )
        {
            var methodName = GetMethodName ( methodExpression );
            var domainRuleEngine = CreateRuleEngine ( subject, methodName.EndsWith ( "RuleSet" ) ? methodName : methodName + "RuleSet" );
            return domainRuleEngine;
        }

        /// <summary>
        /// Executes rules and if successful runs specified callback.
        /// If not successful Raises <see cref="RuleViolationEvent">Rule Violation Events</see>.
        /// </summary>
        /// <param name="sucessCallBack">The sucess call back.</param>
        public void Execute ( Action sucessCallBack )
        {
            var results = _executeRules ( _ruleEngineContext );

            if ( results.HasRuleViolation )
            {
                DomainEvent.Raise ( new RuleViolationEvent { RuleViolations = results.RuleViolations } );
            }
            else
            {
                sucessCallBack ();
            }
        }

        /// <summary>
        /// Adds context object.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="contextObject">The context object.</param>
        /// <returns>A <see cref="DomainRuleEngine"/></returns>
        public DomainRuleEngine WithContext<TContext> ( TContext contextObject )
        {
            if ( !Equals ( contextObject, ( TContext )typeof( TContext ).GetDefault () ) )
            {
                _ruleEngineContext.WorkingMemory.AddContextObject ( contextObject );
            }
            return this;
        }

        #endregion

        #region Methods

        private static string GetMethodName ( LambdaExpression expression )
        {
            var exception = new ArgumentException ( "Invalid Expression. Expression should consist of a Method call only." );
            var body = ( expression.Body as UnaryExpression );
            if ( body == null )
            {
                throw exception;
            }
            var methodCall = ( body.Operand as MethodCallExpression );
            if ( methodCall == null )
            {
                throw exception;
            }
            if ( methodCall.Arguments.Count < 3 )
            {
                throw exception;
            }
            var outermostExpression = ( methodCall.Arguments[2] as ConstantExpression );
            if ( outermostExpression == null )
            {
                throw exception;
            }

            var methodInfo = ( outermostExpression.Value as MethodInfo );
            if ( methodInfo == null )
            {
                throw exception;
            }

            return methodInfo.Name;
        }

        #endregion
    }
}
