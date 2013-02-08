using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Rules
{
    [TestClass]
    public class CollectionPropertyRuleTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void CreateCollectionPropertyRule_WithNullPropertyExpression_ThrowsArgumentException ()
        {
            CollectionPropertyRule.CreateCollectionPropertyRule<Customer, string> ( null, "Rule 1" );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void CreateCollectionPropertyRule_WithNullName_ThrowsArgumentException ()
        {
            CollectionPropertyRule.CreateCollectionPropertyRule<Customer, string> ( c => c.FirstName, null );
        }

        [TestMethod]
        public void CreateCollectionPropertyRule_WithPropertyExpression_ReturnsPropertyRule ()
        {
            var rule = CollectionPropertyRule.CreateCollectionPropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            Assert.IsInstanceOfType ( rule, typeof( CollectionPropertyRule ) );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void WithRuleCollection_WithNullRuleCollection_ThrowsArgumentException ()
        {
            var rule = CollectionPropertyRule.CreateCollectionPropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            rule.WithRuleCollection<Customer> ( null );
        }

        [TestMethod]
        public void ThenClause_WithRuleCollection_CollectionRulesAreRun ()
        {
            var rule = CollectionPropertyRule.CreateCollectionPropertyRule<Patient, IEnumerable<Address>> (
                p => p.Addresses, "Rule 1" );
            var addressRuleCollection = new AddressRuleCollection ();
            var ruleCalled = false;
            addressRuleCollection.NewRule ( () => addressRuleCollection.MyEmptyRule ).When ( c => ruleCalled = true );
            rule.WithRuleCollection ( addressRuleCollection );

            var patient = new Patient ();
            patient.Addresses.Add ( new Address ( null, null, null, null, null ) );

            var ruleEngineContext = new RuleEngineContext<Patient> ( patient );

            foreach ( var thenClause in rule.ThenClauses )
            {
                thenClause ( ruleEngineContext );
            }

            Assert.IsTrue ( ruleCalled );
        }
    }
}
