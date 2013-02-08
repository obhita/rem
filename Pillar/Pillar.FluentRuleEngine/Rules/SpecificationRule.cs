using Pillar.Common.Specification;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Rule that requires a subject to meet a <see cref="ISpecification{TEntity}">Specification</see>.
    /// </summary>
    public class SpecificationRule : Rule, ISpecificationRule
    {
        private SpecificationRule ( string name )
            : base ( name )
        {
        }

        /// <summary>
        /// Creates an instance of a Specification Rule
        /// </summary>
        /// <typeparam name="TSubject">Type of the subject of the rule.</typeparam>
        /// <param name="specification">Specification rule must meet.</param>
        /// <param name="ruleName">Name of the rule</param>
        /// <returns>A <see cref="SpecificationRule">Specification Rule</see>.</returns>
        public static SpecificationRule CreateSpecificationRule<TSubject> ( ISpecification<TSubject> specification, string ruleName )
        {
            Check.IsNotNull ( specification, "specification is required." );

            var specificationRule = new SpecificationRule ( ruleName ) { WhenClause = ctx => specification.IsSatisfiedBy ( ( TSubject )ctx.Subject ) };
            return specificationRule;
        }
    }
}
