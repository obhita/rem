using Agatha.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.Web.Tests.PatientDashboard
{
    [TestClass]
    public class GetGrowthRateInformationRequestHandlerTests : PatientModuleTestFixture
    {
        [TestMethod]
        public void GetGrowthInfo_GivenAValidPatientKey_ReturnsGrowthRateInfo()
        {
            Session.Clear();
            var handler = new GetGrowthInformationByPatientKeyRequestHandler { SessionProvider = SessionProvider };
            Request request = new GetGrowthInformationByPatientKeyRequest { Key = TaddYoungPatient.Key };

            Response response = handler.Handle(request);
            var growthRateInfoResponse = response as GetGrowthInformationByPatientKeyResponse;
            
            Assert.IsNotNull ( growthRateInfoResponse );
            Assert.IsTrue ( growthRateInfoResponse.GrowthInfoDto.GrowthRateHeightDtos.Count > 0 );
            Assert.IsTrue ( growthRateInfoResponse.GrowthInfoDto.GrowthRateWeightDtos.Count > 0 );
        }
    }
}
