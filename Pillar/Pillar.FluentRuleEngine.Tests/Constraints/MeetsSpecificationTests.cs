using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Specification;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Constraints
{
    [TestClass]
    public class MeetsSpecificationTests
    {
        [TestMethod]
        public void MeetsSpecification_PropertyMeetsSpecification_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.MeetsSpecification ( new TestSpecification () );

            var customer = new Customer { Age = 5 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }

        [TestMethod]
        public void MeetsSpecification_PropertyDoesNotMeetSpecification_RuleFails ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.MeetsSpecification ( new TestSpecification () );

            var customer = new Customer { Age = 15 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsFalse ( whenResult );
        }

        #region Nested type: TestSpecification

        private class TestSpecification : ISpecification<int>
        {
            #region ISpecification<int> Members

            public bool IsSatisfiedBy ( int entity )
            {
                return entity < 10;
            }

            #endregion
        }

        #endregion
    }
}
