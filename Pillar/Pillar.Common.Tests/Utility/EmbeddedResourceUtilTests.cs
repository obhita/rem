using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Utility;

namespace Pillar.Common.Tests.Utility
{
    [TestClass]
    public class EmbeddedResourceUtilTests
    {
        private const string ValidEmbeddedResourceName = "Pillar.Common.Tests.Utility.MyEmbeddedResource.txt";
        private const string InvalidEmbeddedResourceName = "MyEmbeddedResource.txt";
        private const string ValueInEmbeddedResource = "my embedded resource value";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetEmbeddedResourceValue_ResourceNameAsNull_ThrowsException()
        {
            EmbeddedResourceUtil.GetEmbeddedResourceValue(null, Assembly.GetExecutingAssembly());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEmbeddedResourceValue_ResourceNameAsWhiteSpace_ThrowsException()
        {
            EmbeddedResourceUtil.GetEmbeddedResourceValue(" ", Assembly.GetExecutingAssembly());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetEmbeddedResourceValue_AssemblyAsNull_ThrowsException()
        {
            EmbeddedResourceUtil.GetEmbeddedResourceValue("test", null);
        }

        [TestMethod]
        public void GetEmbeddedResourceValue_InvalidResourceNameReturnsNull_Success()
        {
            string result = EmbeddedResourceUtil.GetEmbeddedResourceValue(InvalidEmbeddedResourceName, Assembly.GetExecutingAssembly());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetEmbeddedResourceValue_ValidResourceNameReturnsValue_Success()
        {
            string result = EmbeddedResourceUtil.GetEmbeddedResourceValue(ValidEmbeddedResourceName, Assembly.GetExecutingAssembly());
            Assert.AreEqual( ValueInEmbeddedResource, result );
        }
    }
}
