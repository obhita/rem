using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Rules
{
    [TestClass]
    public class RuleBuilderTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void Constructor_GivenANullRule_ShouldThrowArgumentNullException ()
        {
            new RuleBuilder<RuleEngineContext<Customer>, Customer> ( null );
        }

        [TestMethod]
        public void When_GivenAWhenPredicate_AssignsWhenPredicateToRule ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.When ( c => c.Age < 20 );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var lessThan20 = rule.WhenClause ( context );

            Assert.IsTrue ( lessThan20 );
        }

        [TestMethod]
        public void When_WithAWhenFunction_AssignsWhenFunctionToRule ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.When ( ( cust, ctxt ) => cust.Age < 20 );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            var lessThan20 = rule.WhenClause ( context );

            Assert.IsTrue ( lessThan20 );
        }

        [TestMethod]
        [ExpectedException ( typeof( InvalidRuleException ) )]
        public void When_GivenAThenClauseWithoutAWhenClause_ThrowsInvalidRuleException ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.Then ( c => c.Age++ );
        }

        [TestMethod]
        [ExpectedException ( typeof( InvalidRuleException ) )]
        public void When_GivenAThenClauseOverrideWithoutAWhenClause_ThrowsInvalidRuleException ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.Then ( ( cust, ctxt ) => cust.Age++ );
        }

        [TestMethod]
        public void Then_ProvidingAThenClauseToTheBuilder_RuleShouldHaveAThenClause ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.When ( ( cust, ctxt ) => true ).Then ( c => c.Age++ );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            foreach ( var clause in rule.ThenClauses )
            {
                clause ( context );
            }

            Assert.AreEqual ( 11, customer.Age );
        }

        [TestMethod]
        public void Then_ProvidingAThenClauseOverrideToTheBuilder_RuleShouldHaveAThenClauseOverride ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.When ( ( cust, ctxt ) => true ).Then ( ( cust, ctxt ) => cust.Age++ );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            foreach ( var clause in rule.ThenClauses )
            {
                clause ( context );
            }

            Assert.AreEqual ( 11, customer.Age );
        }

        [TestMethod]
        public void Then_ProvidingAElseThenClauseToTheBuilder_RuleShouldHaveAElseThenClause ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.When ( ( cust, ctxt ) => true ).ElseThen ( c => c.Age++ );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            foreach ( var clause in rule.ElseThenClauses )
            {
                clause ( context );
            }

            Assert.AreEqual ( 11, customer.Age );
        }

        [TestMethod]
        public void Then_ProvidingAElseThenClauseOverrideToTheBuilder_RuleShouldHaveAElseThenClauseOverride ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.When ( ( cust, ctxt ) => true ).ElseThen ( ( cust, ctxt ) => cust.Age++ );

            var customer = new Customer { Age = 10 };
            var context = new RuleEngineContext<Customer> ( customer );

            foreach ( var clause in rule.ElseThenClauses )
            {
                clause ( context );
            }

            Assert.AreEqual ( 11, customer.Age );
        }

        [TestMethod]
        public void DoNotRunIfHasRuleViolation_ShouldRunClauseAdded ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.DoNotRunIfHasRuleViolation ();

            var ruleEngineContext = new RuleEngineContext ( new Customer () );
            ruleEngineContext.RuleViolationReporter.Report ( new RuleViolation ( rule, null, null ) );

            var shouldRunResult = rule.ShouldRunRule ( ruleEngineContext );

            Assert.IsFalse ( shouldRunResult );
        }

        [TestMethod]
        public void WithPrioroty_RulePriorityIsSet ()
        {
            var rule = new Rule ( "Rule1" );
            var ruleBuilder = new RuleBuilder<RuleEngineContext<Customer>, Customer> ( rule );
            ruleBuilder.WithPriority ( 5 );

            Assert.AreEqual ( 5, rule.Priority );
        }
    }
}
