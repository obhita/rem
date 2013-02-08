using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.Tests.CommonModule
{
    [TestClass]
    public class PersonNameTests
    {
        [TestMethod]
        public void PersonName_WithValidProperties_Succeeds()
        {
            var name = new PersonName("prefix", "first", "middle", "last", "suffix");
            Assert.AreEqual("prefix first middle last suffix", name.Complete);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PersonName_WithNullFirstName_ThrowsArgumentException()
        {
            new PersonName("prefix", null, "middle", "last", "suffix");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PersonName_WithNullLastName_ThrowsArgumentException()
        {
            new PersonName("prefix", "first", "middle", null, "suffix");
        }
    }
}
