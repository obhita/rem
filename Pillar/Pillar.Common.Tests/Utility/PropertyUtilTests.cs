using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Utility;

namespace Pillar.Common.Tests.Utility
{

    [TestClass]
    public class PropertyUtilTests
    {
        [TestMethod]
        public void ExtractProperty_GivenPropertyExpression_ReturnsWithCorrectDeclaringType()
        {
            var propertyInfo = PropertyUtil.ExtractProperty<TestFake, string>(p => p.OverridableProperty);
            Assert.AreEqual(typeof(TestFake), propertyInfo.DeclaringType);

            var propertyInfoBase = PropertyUtil.ExtractProperty<TestFakeBase, string>(p => p.OverridableProperty);
            Assert.AreEqual(typeof(TestFakeBase), propertyInfoBase.DeclaringType);
        }

        [TestMethod]
        public void ExtractPropertyName_GivenPropertyExpressionWithOwerTypeAndReturnType_ReturnsPropertyName()
        {
            string propertyName = PropertyUtil.ExtractPropertyName<TestFake, string> ( p => p.StringProperty );
            Assert.AreEqual(TestFake.StringPropertyName, propertyName);
        }

        [TestMethod]
        public void ExtractPropertyName_GivenPropertyExpressionWithOwerTypeAndObjectReturnType_ReturnsPropertyName()
        {
            string propertyName = PropertyUtil.ExtractPropertyName<TestFake, Object>(p => p.StringProperty);
            Assert.AreEqual(TestFake.StringPropertyName, propertyName);
        }

        [TestMethod]
        public void ExtractPropertyName_GivenPropertyExpressionWithReturnType_ReturnsPropertyName()
        {
            var testFake = new TestFake();
            string propertyName = PropertyUtil.ExtractPropertyName<string>(() => testFake.StringProperty);
            Assert.AreEqual(TestFake.StringPropertyName, propertyName);
        }

        [TestMethod]
        public void ExtractPropertyName_GivenPropertyExpressionWithObjectReturnType_ReturnsPropertyName ()
        {
            var testFake = new TestFake();
            string propertyName = PropertyUtil.ExtractPropertyName ( () => testFake.StringProperty );
            Assert.AreEqual ( TestFake.StringPropertyName, propertyName );
        }

        #region Nested type: TestFake

        private class TestFake : TestFakeBase
        {
            public static readonly string StringPropertyName = "StringProperty";
            public static readonly string OverridablePropertyName = "OverridableProperty";

            public string StringProperty { get; set; }

            public override string OverridableProperty { get; set; }
        }

        private class TestFakeBase
        {
            public virtual string OverridableProperty { get; set; }
        }

        #endregion
    }
}