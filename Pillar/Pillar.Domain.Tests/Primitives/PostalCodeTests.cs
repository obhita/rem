using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain.Primitives;

namespace Pillar.Domain.Tests.Primitives
{
    [TestClass]
    public class PostalCodeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidPostalCode_ThrowsException()
        {
            PostalCode postalCode = new PostalCode("ThePostalCode");
        }

        [TestMethod]
        public void Constructor_ValidPostalCode_Succeeds()
        {
            PostalCode postalCode1 = new PostalCode("21046");
            PostalCode postalCode2 = new PostalCode("21046-1234");
        }
    }
}
