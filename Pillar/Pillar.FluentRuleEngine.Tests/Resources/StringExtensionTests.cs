using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Tests.Resources
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void FormatCompareRuleEngineMessage_PassInCompareValueCompareOperator_StringIsFormatedCorrectly ()
        {
            var expectedString = "5 >";
            var stringToFormat = "{CompareValue} {ComparisonOperator}";
            var formattedString = stringToFormat.FormatCompareRuleEngineMessage ( 5, ">" );

            Assert.AreEqual ( expectedString, formattedString );
        }

        [TestMethod]
        public void FormatRuleEngineMessage_PassInPropertyName_StringIsFormatedCorrectly ()
        {
            var expectedString = "Property";
            var stringToFormat = "{PropertyName}";
            var formattedString = stringToFormat.FormatRuleEngineMessage ( "Property" );

            Assert.AreEqual ( expectedString, formattedString );
        }

        [TestMethod]
        public void FormatRuleEngineMessage_PassCustomNameDictionary_StringIsFormatedCorrectly ()
        {
            var expectedString = "MyValue";
            var stringToFormat = "{MyCustomExample}";
            var nameDictionary = new Dictionary<string, string> { { "MyCustomExample", "MyValue" } };
            var formattedString = stringToFormat.FormatRuleEngineMessage ( nameDictionary );

            Assert.AreEqual ( expectedString, formattedString );
        }
    }
}
