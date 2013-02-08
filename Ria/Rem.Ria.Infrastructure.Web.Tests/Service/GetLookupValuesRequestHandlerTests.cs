using System;
using Agatha.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Infrastructure.Bootstrapper;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Tests.Domain;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.Infrastructure.Web.Tests.Service
{
    [TestClass]
    public class GetLookupValuesRequestHandlerTests : LoadedLookupsFixtureBase
    {
        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void GetLookupValuesRequest_GivenAnUnknownLookupName_ThrowsException ()
        {
            var handler = new GetLookupValuesRequestHandler(new LookupTypeService(new AssemblyLocator())) { SessionProvider = SessionProvider };
            Request request = new GetLookupValuesRequest { Name = "foo" };
            handler.Handle ( request );
        }

        [TestMethod]
        public void GetLookupValuesRequest_GivenAgencyTypeLookupName_ReturnsAllAgencyTypes ()
        {
            var handler = new GetLookupValuesRequestHandler(new LookupTypeService(new AssemblyLocator())) { SessionProvider = SessionProvider };
            Request request = new GetLookupValuesRequest { Name = "AgencyType" };
            var response = handler.Handle ( request );
            var lookupResponse = response as GetLookupValuesResponse;
            Assert.AreEqual ( 6, lookupResponse.LookupValues.Count );
        }
    }
}