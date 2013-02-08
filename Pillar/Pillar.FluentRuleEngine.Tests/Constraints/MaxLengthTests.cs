using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Constraints
{
    [TestClass]
    public class MaxLengthTests
    {
        [TestMethod]
        public void MaxLength_PropertyLengthIsLessThenMaxLength_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, string> ( rule );
            ruleBuilder.MaxLength ( 5 );

            var customer = new Customer { FirstName = "Fred" };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }

        [TestMethod]
        public void MaxLength_PropertyLengthIsGreaterThanMaxLength_RuleFails ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, string> ( rule );
            ruleBuilder.MaxLength ( 5 );

            var customer = new Customer { FirstName = "123456" };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsFalse ( whenResult );
        }

        [TestMethod]
        public void MaxLength_PropertyLengthIsEqualToMaxLength_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, string> ( rule );
            ruleBuilder.MaxLength ( 5 );

            var customer = new Customer { FirstName = "12345" };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }
    }
}
