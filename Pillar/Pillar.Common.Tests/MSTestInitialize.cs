using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pillar.Common.Tests
{
    //TODO: This class should be gone after the new ServiceLocatorFixture is used in all the places
    [TestClass]
    public class MSTestInitialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            ServiceLocatorUnitTestHelper.Initialize();
        }
    }
}
