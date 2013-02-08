using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.Infrastructure.Web.Tests.Mapping;

namespace Rem.Ria.AgencyModule.Web.Tests.Mapping
{
    [TestClass]
    public class AgencyModuleWebEntityToDtoMappingTests : EntityToDtoMappingTestFixtureBase
    {
        [TestMethod]
        public void AllMapping_EntityToDto_Succeeds()
        {
            AssertAutoMapperConfigurationIsValid();
        }

        protected override void OnSetup()
        {
            new Infrastructure.Web.Mapping.AutoMapperConfig().Execute();
            new AgencyModule.Web.AutoMapperConfig().Execute();

            new AutoMapperConfig().Execute();

            base.OnSetup();
        }
    }
}
