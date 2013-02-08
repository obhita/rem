using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Rules
{
    [TestClass]
    public class RuleTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void Rule_WithNullName_ThrowsArgumentException ()
        {
            new Rule ( null );
        }

        [TestMethod]
        public void Rule_WithName_RuleIsNamed ()
        {
            var rule = new Rule ( "Name" );
            Assert.AreEqual ( "Name", rule.Name );
        }

        [TestMethod]
        public void Rule_WithWhenClause_WhenClauseIsCallable ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );
            Predicate<IRuleEngineContext> whenClause = e => string.IsNullOrWhiteSpace ( ( ( RuleEngineContext<Customer> )e ).Subject.FirstName );

            var rule = new Rule ( "Name" ) { WhenClause = whenClause };
            var isNullOrWhiteSpace = rule.WhenClause ( ruleEngineContext );

            Assert.IsTrue ( isNullOrWhiteSpace );
        }

        [TestMethod]
        public void Rule_WithThenClauses_ThenClausesAreCallable ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );

            var rule = new Rule ( "Name" );

            Action<IRuleEngineContext> thenClause1 = e => ( ( Customer )e.Subject ).Age = 20;
            rule.AddThenClause ( thenClause1 );

            Action<IRuleEngineContext> thenClause2 = e => ( ( Customer )e.Subject ).Age++;
            rule.AddThenClause ( thenClause2 );

            foreach ( var clause in rule.ThenClauses )
            {
                clause ( ruleEngineContext );
            }

            Assert.AreEqual ( 21, customer.Age );
        }

        [TestMethod]
        public void Rule_WithElseThenClauses_ElseThenClausesAreCallable ()
        {
            var customer = new Customer ();
            var ruleEngineContext = new RuleEngineContext<Customer> ( customer );

            var rule = new Rule ( "Name" );

            Action<IRuleEngineContext> elseThenClause1 = e => ( ( Customer )e.Subject ).Age = 20;
            rule.AddElseThenClause ( elseThenClause1 );

            Action<IRuleEngineContext> elseThenClause2 = e => ( ( Customer )e.Subject ).Age++;
            rule.AddElseThenClause ( elseThenClause2 );

            foreach ( var clause in rule.ElseThenClauses )
            {
                clause ( ruleEngineContext );
            }

            Assert.AreEqual ( 21, customer.Age );
        }
    }
}
