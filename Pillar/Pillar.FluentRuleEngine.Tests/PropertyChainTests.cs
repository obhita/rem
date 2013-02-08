using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Pillar.FluentRuleEngine.Tests
{
    [TestClass]
    public class PropertyChainTests
    {
        [TestMethod]
        public void ToString_GivenPropertyChain_ReturnsAsExpected()
        {
            // Setup
            var fixture = new Ploeh.AutoFixture.Fixture ().Customize ( new AutoMoqCustomization () );
            var parentPropertyName = fixture.CreateAnonymous<string> ();
            var childPropertyName = fixture.CreateAnonymous<string> ();
            var propertyChainList = new List<string> { parentPropertyName, childPropertyName };
            var sut = new PropertyChain ( propertyChainList );

            // Exercise
            var str = sut.ToString ();

            // Verify
            Assert.AreEqual(parentPropertyName+"."+childPropertyName, str);
        }

        [TestMethod]
        public void FromLambdaExpression_GivenLambdaExpression_ReturnsPropertyChainCorretly()
        {
            // Setup
            Expression<Func<object>> propertyExpression = () => this.MyProperty.Length; 

            // Exercise
            var propertyChain = PropertyChain.FromLambdaExpression ( propertyExpression );
            var str = propertyChain.ToString();

            // Verify
            Assert.AreEqual("MyProperty" + "." + "Length", str);
        }

        [TestMethod]
        public void FromPropertyExpression_GivenPropertyExpression_ReturnsPropertyChainCorretly()
        {
            // Setup

            // Exercise
            var propertyChain = PropertyChain.FromPropertyExpression(() => this.MyProperty.Length);
            var str = propertyChain.ToString();

            // Verify
            Assert.AreEqual("MyProperty" + "." + "Length", str);
        }

        public string MyProperty { get; set; }
    }
}
