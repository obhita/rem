using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests
{
    [TestClass]
    public class AbstractRuleCollectionTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void NewRule_WithNull_ThrowsArgumentException ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            abstractRuleCollection.NewRule<IRule> ( null );
        }

        [TestMethod]
        [ExpectedException ( typeof( InvalidRuleException ) )]
        public void NewRule_WithDuplicateRuleName_ThrowsInvalidRuleException ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            abstractRuleCollection.NewRule ( () => abstractRuleCollection.FirstAndLastNameMustBeDifferent ).When ( c => c.FirstName == null );
        }

        [TestMethod]
        public void NewRule_WithRuleName_RuleAddedWithName ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            abstractRuleCollection.NewRule ( () => abstractRuleCollection.MyEmptyRule ).When ( c => c.FirstName == null );
            var fooRule =
                abstractRuleCollection.FirstOrDefault ( r => r.Name == PropertyUtil.ExtractPropertyName ( () => abstractRuleCollection.MyEmptyRule ) );

            Assert.IsNotNull ( fooRule );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void NewPropertyRule_WithNull_ThrowsArgumentException ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            abstractRuleCollection.NewPropertyRule<IPropertyRule> ( null );
        }

        [TestMethod]
        [ExpectedException ( typeof( InvalidRuleException ) )]
        public void NewPropertyRule_WithDuplicateRuleName_ThrowsInvalidRuleException ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            abstractRuleCollection.NewPropertyRule ( () => abstractRuleCollection.FirstNameRequired ).WithProperty ( c => c.FirstName );
        }

        [TestMethod]
        public void NewPropertyRule_WithRuleName_RuleAddedWithName ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            abstractRuleCollection.NewPropertyRule ( () => abstractRuleCollection.MyEmptyPropertyRule ).WithProperty ( c => c.FirstName );
            var fooRule =
                abstractRuleCollection.FirstOrDefault (
                    r => r.Name == PropertyUtil.ExtractPropertyName ( () => abstractRuleCollection.MyEmptyPropertyRule ) );

            Assert.IsNotNull ( fooRule );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void NewRule_WithInvalidRuleName_ThrowsArgumentException ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            var stringRuleCollection = new StringRuleCollection ();
            abstractRuleCollection.NewRule ( () => stringRuleCollection.StringEmptyRule ).When ( c => c.FirstName == null );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void NewPropertyRule_WithInvalidRuleName_ThrowsArgumentException ()
        {
            var abstractRuleCollection = new CustomerRuleCollection ();
            var stringRuleCollection = new StringRuleCollection ();
            abstractRuleCollection.NewPropertyRule ( () => stringRuleCollection.StringEmptyPropertyRule ).WithProperty ( c => c.FirstName );
        }

        [TestMethod]
        public void Ctor_WithCustomizer_CustomizerRulesAreAdded ()
        {
            var customerRuleCollectionCustomizer = new CustomerRuleCollectionCustomizer ();

            var customerRuleCollection = new CustomerRuleCollection ();

            customerRuleCollectionCustomizer.Customize ( customerRuleCollection );

            var allRulesFound = true;

            foreach ( var rule in customerRuleCollectionCustomizer )
            {
                if ( !customerRuleCollection.Contains ( rule ) )
                {
                    allRulesFound = false;
                }
            }
            Assert.IsTrue ( allRulesFound );
        }

        [TestMethod]
        public void Ctor_WithCustomizerThatDissablesRules_AppropriateRulesAreDissable ()
        {
            var customerRuleCollectionCustomizer = new CustomerRuleCollectionCustomizer ();

            var customerRuleCollection = new CustomerRuleCollection ();

            customerRuleCollectionCustomizer.Customize ( customerRuleCollection );

            var dissableRule = customerRuleCollection.FirstOrDefault ( r => r == customerRuleCollection.FirstNameRequired );

            Assert.IsTrue ( dissableRule.IsDisabled );
        }
    }
}
