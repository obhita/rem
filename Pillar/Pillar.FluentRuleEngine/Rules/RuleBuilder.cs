using System;
using System.Linq;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Builder that configures a <see cref="IRule">Rule</see>.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    public class RuleBuilder<TContext, TSubject> : IRuleBuilder<TContext, TSubject>, IRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        #region Constants and Fields

        private readonly Rule _rule;

        #endregion

        #region Constructors and Destructors

        internal RuleBuilder ( Rule rule )
        {
            Check.IsNotNull ( rule, "Rule is required." );

            _rule = rule;
        }

        #endregion

        #region IRuleBuilder<TContext,TSubject> Members

        /// <inheritdoc/>
        public IRule Rule
        {
            get { return _rule; }
        }

        #endregion

        #region Public Methods

        #region IRuleBuilder<TContext,TSubject> Members

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> ElseThen ( Action<TSubject> elseThenClause )
        {
            _rule.AddElseThenClause ( ruleContext => elseThenClause ( ( TSubject )ruleContext.Subject ) );

            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> ElseThen ( Action<TSubject, TContext> elseThenClause )
        {
            _rule.AddElseThenClause ( ruleContext => elseThenClause ( ( TSubject )ruleContext.Subject, ( TContext )ruleContext ) );

            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> Then ( Action<TSubject> thenClause )
        {
            if ( _rule.WhenClause == null )
            {
                throw new InvalidRuleException ( Messages.WhenClauseMustPrecedeThenClause );
            }

            _rule.AddThenClause ( ruleContext => thenClause ( ( TSubject )ruleContext.Subject ) );

            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> Then ( Action<TSubject, TContext> thenClause )
        {
            if ( _rule.WhenClause == null )
            {
                throw new InvalidRuleException ( Messages.WhenClauseMustPrecedeThenClause );
            }

            _rule.AddThenClause ( ruleContext => thenClause ( ( TSubject )ruleContext.Subject, ( TContext )ruleContext ) );

            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> DoNotRunIfHasRuleViolation ()
        {
            _rule.AddShouldRunClause ( ctx => ctx.RuleViolationReporter.Count () == 0 );
            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> WithPriority ( int priority )
        {
            _rule.Priority = priority;
            return this;
        }

        #endregion

        #region IRuleBuilderInitializer<TContext,TSubject> Members

        /// <summary>
        /// Creates a rule builder that also provides a context object.
        /// </summary>
        /// <typeparam name="TContextObject">The type of the context object.</typeparam>
        /// <param name="contextObjectName">Name of the context object.</param>
        /// <returns>An <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> OnContextObject<TContextObject> (
            string contextObjectName = null )
        {
            return new ContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ( _rule, contextObjectName );
        }

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> When ( Predicate<TSubject> whenClause )
        {
            _rule.WhenClause = ruleContext => whenClause ( ( TSubject )ruleContext.Subject );

            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> When ( Func<TSubject, TContext, bool> whenClause )
        {
            _rule.WhenClause = ruleContext => whenClause ( ( TSubject )ruleContext.Subject, ( TContext )ruleContext );

            return this;
        }

        /// <inheritdoc/>
        public IRuleBuilderInitializer<TContext, TSubject> AddAttribute(string key, object value)
        {
            _rule.AddAttribute ( key, value );

            return this;
        }

        #endregion

        #endregion
    }
}
