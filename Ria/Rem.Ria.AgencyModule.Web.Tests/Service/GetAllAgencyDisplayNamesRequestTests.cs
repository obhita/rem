using Agatha.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.AgencyModule.Web.Service;

namespace Rem.Ria.AgencyModule.Web.Tests.Service
{
    [TestClass]
    public class GetAllAgencyDisplayNamesRequestTests : AgencyModuleTestFixture
    {
        [TestMethod]
        public void GetAllAgencyDisplayNamesRequest_Succeeds()
        {
            var handler = new GetAllAgencyDisplayNamesRequestHandler { SessionProvider = SessionProvider };

            // When you do Agatha Request Handler testing, always declare the request as the base class type Agatha.Common.Request
            Request request = new GetAllAgencyDisplayNamesRequest ();
            var response = handler.Handle ( request );
            var agencyResponse = response as GetAllAgencyDisplayNamesResponse;
            Assert.AreEqual ( 1, agencyResponse.Agencies.Count );
        }
    }
}
