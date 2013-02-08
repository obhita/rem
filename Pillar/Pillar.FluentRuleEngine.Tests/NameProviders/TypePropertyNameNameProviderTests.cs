using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.NameProviders;
using Pillar.FluentRuleEngine.Tests.Fixture;

namespace Pillar.FluentRuleEngine.Tests.NameProviders
{
    [TestClass]
    public class TypePropertyNameNameProviderTests
    {
        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void GetName_NullSubject_ThrowsArgumentException ()
        {
            var nameProvider = new TypePropertyNameNameProvider ();
            nameProvider.GetName ( null );
        }

        [TestMethod]
        [ExpectedException ( typeof( ArgumentException ) )]
        public void GetName_NullPropertyExpression_ThrowsArgumentException ()
        {
            var nameProvider = new TypePropertyNameNameProvider ();
            nameProvider.GetName<string, string> ( null, null );
        }

        [TestMethod]
        public void GetName_Customer_ReturnsCustomerTypeName ()
        {
            var nameProvider = new TypePropertyNameNameProvider ();
            var name = nameProvider.GetName ( new Customer () );

            var expectedName = typeof( Customer ).Name.SeparatePascalCaseWords ();

            Assert.AreEqual ( expectedName, name );
        }

        [TestMethod]
        public void GetName_CustomerFirstname_ReturnsFirstNamePropertyName ()
        {
            var nameProvider = new TypePropertyNameNameProvider ();
            var name = nameProvider.GetName ( new Customer (), c => c.FirstName );

            var expectedName = PropertyUtil.ExtractPropertyName<Customer, string> ( c => c.FirstName ).SeparatePascalCaseWords ();

            Assert.AreEqual ( expectedName, name );
        }
    }
}
