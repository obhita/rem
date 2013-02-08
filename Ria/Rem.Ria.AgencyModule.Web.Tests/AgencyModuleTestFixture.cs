using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Infrastructure.Tests.Domain;

namespace Rem.Ria.AgencyModule.Web.Tests
{
    [TestClass]
    public class AgencyModuleTestFixture : SafeHarborAgencyFixtureBase
    {
        protected override void OnSetup()
        {
            base.OnSetup();

            var infrastructureAutoMapperConfig = new Infrastructure.Web.Mapping.AutoMapperConfig ();
            infrastructureAutoMapperConfig.Execute ();

            var agencyAutoMapperConfig = new AutoMapperConfig ();
            agencyAutoMapperConfig.Execute ();
        }
    }
}
