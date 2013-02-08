using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Clinical.LabModule;

namespace Rem.Domain.Clinical.Tests.LabModule
{
    [TestClass]
    public class LabFacilityTests
    {
        [TestMethod]
        public void Equals_GivenVariousScenarios_ReturnsAsExpected()
        {
            LabFacility first;
            LabFacility second;

            first = new LabFacility(null, null, null, null, null);
            second = new LabFacility(null, null, null, null, null);
            Assert.IsTrue(first.Equals(second));

            first = new LabFacility(null, null, null, null, null);
            second = new LabFacility("", null, null, null, null);
            Assert.IsFalse(first.Equals(second));

            first = new LabFacility("", null, null, null, null);
            second = new LabFacility("", null, null, null, null);
            Assert.IsTrue(first.Equals(second));
        }
    }
}