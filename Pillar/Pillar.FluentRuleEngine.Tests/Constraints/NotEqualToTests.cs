using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Constraints
{
    [TestClass]
    public class NotEqualToTests
    {
        [TestMethod]
        public void NotEqualTo_ValueEqualToProperty_RuleFails ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.NotEqualTo ( 5 );

            var customer = new Customer { Age = 5 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsFalse ( whenResult );
        }

        [TestMethod]
        public void NotEqualTo_ValueNotEqualToProperty_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.NotEqualTo ( 15 );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }
    }
}
