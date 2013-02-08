using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Pillar.Common.Tests
{
    [TestClass]
    public abstract class TestFixtureBase
    {
        protected virtual void OnSetup() { }
        protected virtual void OnTeardown() { }

        [TestInitialize]
        public void Setup()
        {
            OnSetup();
        }

        [TestCleanup]
        public void Teardown()
        {
            OnTeardown();
        }
    }
}
