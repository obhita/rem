using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Rules
{
    [TestClass]
    public class PropertyRuleBuilderTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void Constructor_GivenANullRule_ShouldThrowArgumentNullException ()
        {
            new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, string> ( null );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void Constrain_GivenNullConstraint_ThrowsArgumentException ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, string> ( rule );

            ruleBuilder.Constrain ( null );
        }

        [TestMethod]
        public void Constrain_WithAConstraint_AssignsConstraintToRule ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, string> ( rule );

            ruleBuilder.Constrain ( new NotNullConstraint () );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );

            Assert.IsFalse ( whenResult );
        }

        [TestMethod]
        public void WithPrioroty_RulePriorityIsSet ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule1" );
            var ruleBuilder = new PropertyRuleBuilder<RuleEngineContext<Customer>, Customer, string> ( rule );
            ruleBuilder.WithPriority ( 5 );

            Assert.AreEqual ( 5, rule.Priority );
        }
    }
}
