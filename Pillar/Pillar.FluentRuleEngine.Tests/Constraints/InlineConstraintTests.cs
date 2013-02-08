using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Constraints;

namespace Pillar.FluentRuleEngine.Tests.Constraints
{
    [TestClass]
    public class InlineConstraintTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void IsCompliant_GivenNullPredicate_ShouldThrowArgumentException ()
        {
            new InlineConstraint<string> ( null );
        }

        [TestMethod]
        public void IsCompliant_GivenAPredicate_ShouldCallPredicate ()
        {
            var called = false;
            var constraint = new InlineConstraint<string> ( p => called = true, null );
            constraint.IsCompliant ( "value", null );
            Assert.IsTrue ( called );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void IsCompliant_GivenAWrongTypeValue_ShouldThrowArgumentException ()
        {
            var constraint = new InlineConstraint<string> ( p => true, null );
            constraint.IsCompliant ( 5, null );
        }
    }
}
