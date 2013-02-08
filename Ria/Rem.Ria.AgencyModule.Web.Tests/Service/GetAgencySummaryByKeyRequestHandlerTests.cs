using System.Linq;
using Agatha.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.Service;

namespace Rem.Ria.AgencyModule.Web.Tests.Service
{
    [TestClass]
    public class GetAgencySummaryByKeyRequestHandlerTests : AgencyModuleTestFixture
    {
        [TestMethod]
        public void Handle_GivenValidKey_ReturnsAgencySummaryDto ()
        {
            var handler = new GetAgencySummaryByKeyRequestHandler() { SessionProvider = SessionProvider };

            // When you do Agatha Request Handler testing, always declare the request as the base class type Agatha.Common.Request
            Request request = new GetAgencySummaryByKeyRequest { Key = SafeHarborAgency.Key };

            Response response = handler.Handle ( request );

            var dtoResponse = response as GetAgencySummaryByKeyResponse;

            AgencySummaryDto agencySummaryDto = dtoResponse.AgencySummaryDto;

            Assert.AreEqual ( "Safe Harbor", agencySummaryDto.DisplayName );

            AgencyAddressAndPhoneDto addressDto = agencySummaryDto.AddressesAndPhones.Single ();
            Assert.AreEqual("123 Safe Harbor Way", addressDto.FirstStreetAddress);
            Assert.AreEqual(2, addressDto.PhoneNumbers.Count);

            // Make sure that we are only returning active contacts
            Assert.AreEqual ( 1,  agencySummaryDto.AgencyContacts.Count() );

            AgencyIdentifierDto agencyIdentifierDto = agencySummaryDto.AgencyIdentifiers.Single ();
            Assert.AreEqual("154975646", agencyIdentifierDto.IdentifierNumber);
        }
    }
}