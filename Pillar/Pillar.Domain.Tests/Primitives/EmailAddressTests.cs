using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain.Primitives;

namespace Pillar.Domain.Tests.Primitives
{
    [TestClass]
    public class EmailAddressTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullEmailAddress_ThrowsException()
        {
            EmailAddress emailAddress = new EmailAddress(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidEmailAddress_ThrowsException()
        {
            EmailAddress emailAddress = new EmailAddress("TheEmailAddress");
        }

        [TestMethod]
        public void Constructor_ValidEmailAddress_Succeeds()
        {
            EmailAddress emailAddress = new EmailAddress("test@test.com");
        }
    }
}
