using System.Linq;
using Agatha.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.AgencyModule.Web.AgencyEditor;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.Web.Tests.Service
{
    [TestClass]
    public class GetAgencyByKeyRequestHandlerTests : AgencyModuleTestFixture
    {
        [TestMethod]
        public void GetAgencyByKeyRequest_GivenValidKey_ReturnsAgencyDto()
        {
            Session.Clear ();
            var handler = new GetAgencyByKeyRequestHandler { SessionProvider = SessionProvider };

            // When you do Agatha Request Handler testing, always declare the request as the base class type Agatha.Common.Request
            Request request = new GetAgencyByKeyRequest { Key = SafeHarborAgency.Key };

            Response response = handler.Handle(request);
            var agencyResponse = response as GetAgencyByKeyResponse;
            AgencyDto agencyDto = agencyResponse.AgencyDto;

            Assert.AreEqual ( 1, agencyDto.AddressesAndPhones.AddressesAndPhones.Count );
        }
    }
}
