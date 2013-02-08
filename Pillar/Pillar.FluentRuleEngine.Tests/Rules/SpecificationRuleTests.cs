using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Specification;
using Pillar.FluentRuleEngine.Rules;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.Rules
{
    [TestClass]
    public class SpecificationRuleTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void CreateSpecificationRule_WithNullSpecification_ThrowsArgumentException ()
        {
            SpecificationRule.CreateSpecificationRule<Customer> ( null, "Rule 1" );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void CreateSpecificationRule_WithNullName_ThrowsArgumentException ()
        {
            SpecificationRule.CreateSpecificationRule ( new TestSpecification (), null );
        }

        [TestMethod]
        public void CreateSpecificationRule_WithSpecification_ReturnsSpecificationRule ()
        {
            var rule = SpecificationRule.CreateSpecificationRule ( new TestSpecification (), "Rule 1" );
            Assert.IsInstanceOfType ( rule, typeof( SpecificationRule ) );
        }

        [TestMethod]
        public void CreateSpecificationRule_WithSpecification_SpecificationIsUsed ()
        {
            var specification = new TestSpecification ();
            var rule = SpecificationRule.CreateSpecificationRule ( specification, "Rule 1" );

            var customer = new Customer ();
            var context = new RuleEngineContext<Customer> ( customer );

            rule.WhenClause ( context );

            Assert.IsTrue ( specification.IsSatisfiedByCalled );
        }

        #region Nested type: TestSpecification

        private class TestSpecification : ISpecification<Customer>
        {
            public bool IsSatisfiedByCalled;

            #region ISpecification<Customer> Members

            public bool IsSatisfiedBy ( Customer entity )
            {
                return IsSatisfiedByCalled = true;
            }

            #endregion
        }

        #endregion
    }
}
