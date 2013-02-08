using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    [TestClass]
    public class AgencyTests
    {
        [TestMethod]
        public void ReviseAgencyProfile_LegalNameChangeSuccessful()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                //Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                const string newName = "ADifferentName";
                var agencyType = new Mock<AgencyType>();
                Agency agency =
                    new AgencyBuilder().WithAgencyProfile(
                        new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(new AgencyNameBuilder().WithLegalName("SomeName")));

                agency.ReviseAgencyProfile(
                    new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(newName)));

                Assert.AreEqual(agency.AgencyProfile.AgencyName.LegalName, newName);
            }
        }

        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure(c => c.For<IDomainEventService>().HybridHttpOrThreadLocalScoped().Use<DomainEventService>());
        }
    }
}