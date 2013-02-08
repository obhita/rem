using Pillar.Common.Specification;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Interface for the builder of a <see cref="ISpecificationRule">Specification Rule</see>.
    /// </summary>
    /// <typeparam name="TContext">The type of the <see cref="RuleEngineContext{TSubject}">RuleEngineContext</see> for the rule.</typeparam>
    /// <typeparam name="TSubject">The type of the subject of the rule.</typeparam>
    public interface ISpecificationRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        /// <summary>
        /// Sets the <see cref="ISpecification{TSubject}">Specification</see> the rule will check.
        /// </summary>
        /// <param name="specification">The <see cref="ISpecification{TSubject}">Specification</see>.</param>
        /// <returns>An <see cref="IRuleBuilder{TContext,TSubject}">IRuleBuilder</see></returns>
        IRuleBuilder<TContext, TSubject> WithSpecification ( ISpecification<TSubject> specification );
    }
}
