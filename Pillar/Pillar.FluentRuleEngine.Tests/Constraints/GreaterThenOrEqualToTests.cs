using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Constraints
{
    [TestClass]
    public class GreaterThanOrEqualToTests
    {
        [TestMethod]
        public void GreaterThanOrEqualTo_ValueIsLessThenProperty_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.GreaterThanOrEqualTo ( 5 );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }

        [TestMethod]
        public void GreaterThanOrEqualTo_ValueIsGreaterThanProperty_RuleFails ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.GreaterThanOrEqualTo ( 15 );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsFalse ( whenResult );
        }

        [TestMethod]
        public void GreaterThanOrEqualTo_ValueIsEqualToProperty_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.GreaterThanOrEqualTo ( 10 );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }
    }
}
