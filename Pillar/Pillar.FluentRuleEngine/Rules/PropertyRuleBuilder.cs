using System;
using Pillar.FluentRuleEngine.Constraints;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Builder that configures <see cref="IPropertyRule">Property Rules</see>.
    /// </summary>
    /// <typeparam name="TContext">Type of <see cref="RuleEngineContext{TSubject}">context</see> for the rule.</typeparam>
    /// <typeparam name="TSubject">Type of the subject for the rule.</typeparam>
    /// <typeparam name="TProperty">Type of the property of the subject for the rule.</typeparam>
    public class PropertyRuleBuilder<TContext, TSubject, TProperty> : RuleBuilder<TContext, TSubject>,
                                                                      IPropertyRuleBuilder<TContext, TSubject, TProperty>
        where TContext : RuleEngineContext<TSubject>
    {
        private readonly PropertyRule _propertyRule;

        internal PropertyRuleBuilder ( PropertyRule propertyRule )
            : base ( propertyRule )
        {
            _propertyRule = propertyRule;
        }

        #region IPropertyRuleBuilder<TContext,TSubject,TProperty> Members

        /// <inheritdoc/>
        public IPropertyRule PropertyRule
        {
            get { return _propertyRule; }
        }

        /// <inheritdoc/>
        public new IPropertyRuleBuilder<TContext, TSubject, TProperty> Then ( Action<TSubject, TContext> thenClause )
        {
            base.Then ( thenClause );
            return this;
        }

        /// <inheritdoc/>
        public new IPropertyRuleBuilder<TContext, TSubject, TProperty> ElseThen ( Action<TSubject> elseThenClause )
        {
            base.ElseThen ( elseThenClause );
            return this;
        }

        /// <inheritdoc/>
        public new IPropertyRuleBuilder<TContext, TSubject, TProperty> Then ( Action<TSubject> thenClause )
        {
            base.Then ( thenClause );
            return this;
        }

        /// <inheritdoc/>
        public new IPropertyRuleBuilder<TContext, TSubject, TProperty> ElseThen ( Action<TSubject, TContext> elseThenClause )
        {
            base.ElseThen ( elseThenClause );
            return this;
        }

        /// <inheritdoc/>
        public new IPropertyRuleBuilder<TContext, TSubject, TProperty> DoNotRunIfHasRuleViolation ()
        {
            base.DoNotRunIfHasRuleViolation ();
            return this;
        }

        /// <inheritdoc/>
        public new IPropertyRuleBuilder<TContext, TSubject, TProperty> WithPriority ( int priority )
        {
            base.WithPriority ( priority );
            return this;
        }

        /// <inheritdoc/>
        public IPropertyRuleBuilder<TContext, TSubject, TProperty> Constrain ( IConstraint constraint )
        {
            _propertyRule.AddConstraint ( constraint );

            return this;
        }

        /// <inheritdoc/>
        public IPropertyRuleBuilder<TContext, TSubject, TProperty> AutoValidate()
        {
            _propertyRule.AutoValidate = true;

            return this;
        }

        /// <inheritdoc/>
        public IPropertyRuleBuilder<TContext, TSubject, TProperty> DoNotAutoValidate()
        {
            _propertyRule.AutoValidate = false;

            return this;
        }

        #endregion
    }
}
