using System;
using Pillar.Common.Specification;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Builder that intializes a <see cref="ISpecificationRule">Specification Rule</see>
    /// </summary>
    /// <typeparam name="TContext">Type of the <see cref="RuleEngineContext{TSubject}">context</see> for the rule.</typeparam>
    /// <typeparam name="TSubject">Tyoe of the subject for a rule.</typeparam>
    public class SpecificationRuleBuilderInitializer<TContext, TSubject> : ISpecificationRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        #region Constants and Fields

        private readonly Action<SpecificationRule> _addRuleCallBack;

        private readonly string _name;

        #endregion

        #region Constructors and Destructors

        internal SpecificationRuleBuilderInitializer ( string name, Action<SpecificationRule> addRuleCallBack )
        {
            _name = name;
            _addRuleCallBack = addRuleCallBack;
        }

        #endregion

        #region ISpecificationRuleBuilderInitializer<TContext,TSubject> Members

        /// <inheritdoc/>
        public IRuleBuilder<TContext, TSubject> WithSpecification ( ISpecification<TSubject> specification )
        {
            var rule = SpecificationRule.CreateSpecificationRule ( specification, _name );
            _addRuleCallBack ( rule );
            return new RuleBuilder<TContext, TSubject> ( rule );
        }

        #endregion
    }
}
