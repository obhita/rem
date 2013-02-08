using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Domain.Clinical.TedsModule;

namespace Rem.Domain.Clinical.Tests.TedsModule
{
    [TestClass]
    public class TedsSexInformationTests
    {
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Constructor_GivenNullTedsSex_ThrowsException()
        {
            // Setup
            TedsAnswer<TedsGender> tedsSex = null;

            // Exercise
            new TedsGenderInformation(tedsSex, null);

            // Verify
        }
    }
}
