using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Rules
{
    [TestClass]
    public class PropertyRuleTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void CreatePropertyRule_WithNullPropertyExpression_ThrowsArgumentException ()
        {
            PropertyRule.CreatePropertyRule<Customer, string> ( null, "Rule 1" );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void CreatePropertyRule_WithNullName_ThrowsArgumentException ()
        {
            PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, null );
        }

        [TestMethod]
        public void CreatePropertyRule_WithPropertyExpression_ReturnsPropertyRule ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            Assert.IsInstanceOfType ( rule, typeof( PropertyRule ) );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void AddConstraint_WithNullContraint_ThrowsArgumentException ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            rule.AddConstraint ( null );
        }

        [TestMethod]
        public void AddConstraint_WithNotNullContraint_ConstraintIsAdded ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            rule.AddConstraint ( new NotNullConstraint () );

            var customer = new Customer ();
            var context = new RuleEngineContext<Customer> ( customer );

            var whenResult = rule.WhenClause ( context );
            Assert.IsFalse ( whenResult );
        }

        [TestMethod]
        public void AddConstraint_WithNotNullContraint_ARuleViolationIsAdded ()
        {
            var rule = PropertyRule.CreatePropertyRule<Customer, string> ( c => c.FirstName, "Rule 1" );
            rule.AddConstraint ( new NotNullConstraint () );
            rule.AutoValidate = true;

            var customer = new Customer ();
            var context = new RuleEngineContext<Customer> ( customer );

            rule.WhenClause ( context );
            foreach ( var elseThenClause in rule.ElseThenClauses )
            {
                elseThenClause ( context );
            }

            Assert.IsTrue ( context.RuleViolationReporter.Count () == 1, "No Rule Violation was created." );
        }
    }
}
