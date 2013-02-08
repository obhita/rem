using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Constraints
{
    [TestClass]
    public class InclusiveBetweenTests
    {
        [TestMethod]
        public void InclusiveBetween_PropertyInBetweenValues_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.InclusiveBetween ( 5, 15 );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }

        [TestMethod]
        public void InclusiveBetween_PropertyEqualsStartValue_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.InclusiveBetween ( 5, 15 );

            var customer = new Customer { Age = 5 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }

        [TestMethod]
        public void InclusiveBetween_PropertyEqualsEndValue_RulePasses ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.InclusiveBetween ( 5, 15 );

            var customer = new Customer { Age = 15 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsTrue ( whenResult );
        }

        [TestMethod]
        public void InclusiveBetween_PropertyIsLessThenStartValue_RuleFails ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.InclusiveBetween ( 5, 15 );

            var customer = new Customer { Age = 1 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsFalse ( whenResult );
        }

        [TestMethod]
        public void InclusiveBetween_PropertyIsGreaterThanEndValue_RuleFails ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, int> ( c => c.Age, "Rule 1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, int> ( rule );
            ruleBuilder.InclusiveBetween ( 5, 15 );

            var customer = new Customer { Age = 25 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsFalse ( whenResult );
        }
    }
}
