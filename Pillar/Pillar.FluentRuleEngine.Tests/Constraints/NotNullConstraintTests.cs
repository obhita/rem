using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Constraints;

namespace Pillar.FluentRuleEngine.Tests.Constraints
{
    [TestClass]
    public class NotNullConstraintTests
    {
        [TestMethod]
        public void IsCompliant_GivenNull_ShouldReturnFalse ()
        {
            var constraint = new NotNullConstraint ();
            var result = constraint.IsCompliant ( null, null );
            Assert.IsFalse ( result );
        }

        [TestMethod]
        public void IsCompliant_GivenAnInt_ShouldReturnTrue ()
        {
            var constraint = new NotNullConstraint ();
            var result = constraint.IsCompliant ( 1, null );
            Assert.IsTrue ( result );
        }

        [TestMethod]
        public void IsCompliant_GivenANullableIntThatIsNotNull_ShouldReturnTrue ()
        {
            var constraint = new NotNullConstraint ();
            int? nullableInt = 5;
            var result = constraint.IsCompliant ( nullableInt, null );
            Assert.IsTrue ( result );
        }

        [TestMethod]
        public void IsCompliant_GivenANullableIntThatIsNull_ShouldReturnTrue ()
        {
            var constraint = new NotNullConstraint ();
            int? nullableInt = null;
            var result = constraint.IsCompliant ( nullableInt, null );
            Assert.IsFalse ( result );
        }
    }
}
