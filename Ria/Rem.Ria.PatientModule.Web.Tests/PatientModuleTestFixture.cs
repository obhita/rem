using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Infrastructure.Tests.Domain;

namespace Rem.Ria.PatientModule.Web.Tests
{
    [TestClass]
    public class PatientModuleTestFixture : SafeHarborAgencyFixtureBase
    {
        protected override void OnSetup()
        {
            base.OnSetup();

            var infrastructureAutoMapperConfig = new Infrastructure.Web.Mapping.AutoMapperConfig();
            infrastructureAutoMapperConfig.Execute();

            var patientAutoMapperConfig = new AutoMapperConfig();
            patientAutoMapperConfig.Execute();
        }
    }
}
