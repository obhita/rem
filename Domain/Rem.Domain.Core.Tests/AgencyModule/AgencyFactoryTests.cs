using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    [TestClass]
    public class AgencyFactoryTests
    {
        private const string LegalName = "My Agency";

        [TestMethod]
        public void CreateAgency_WithValidArguments_CreatesAnAgency()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var agencyType = new Mock<AgencyType>();

            AgencyProfile agencyProfile =
                new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(new AgencyNameBuilder().WithLegalName(LegalName));

            var agency = agencyFactory.CreateAgency(agencyProfile);

            Assert.IsNotNull(agency);
        }

        [TestMethod]
        public void CreateAgency_WithValidArguments_AgencyIsEditable()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var agencyRepository = new Mock<IAgencyRepository>();
                var agencyFactory = new AgencyFactory(agencyRepository.Object);
                var agencyType = new Mock<AgencyType>();

                AgencyProfile agencyProfileWithoutDisplayName =
                    new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(new AgencyNameBuilder().WithLegalName(LegalName));

                var agency = agencyFactory.CreateAgency(agencyProfileWithoutDisplayName);

                AgencyProfile agencyProfileWithDisplayName =
                    new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(LegalName).WithDisplayName("My Agency Display Name"));

                agency.ReviseAgencyProfile(agencyProfileWithDisplayName);
            }
        }

        [TestMethod]
        public void CreateAgency_WithValidArguments_AgencyIsMadePersistent()
        {
            bool isPersistent = false;

            var agencyRepository = new Mock<IAgencyRepository>();

            agencyRepository.Setup(a => a.MakePersistent(It.IsAny<Agency>())).Callback(() => isPersistent = true);
            var agencyFactory = new AgencyFactory(agencyRepository.Object);

            var agencyType = new Mock<AgencyType>();

            AgencyProfile agencyProfile =
                new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(new AgencyNameBuilder().WithLegalName(LegalName));

            agencyFactory.CreateAgency(agencyProfile);

            Assert.IsTrue(isPersistent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateAgency_NullLegalName_CreatesAnAgency()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var agencyType = new Mock<AgencyType>();

            AgencyProfile agencyProfileWithoutLegalName = new AgencyProfileBuilder().WithAgencyType(agencyType.Object);

            agencyFactory.CreateAgency(agencyProfileWithoutLegalName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateAgency_WhitespaceLegalName_CreatesAnAgency()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var agencyType = new Mock<AgencyType>();

            AgencyProfile agencyProfileWithWhitespaceLegalName =
                new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(new AgencyNameBuilder().WithLegalName("   "));

            agencyFactory.CreateAgency(agencyProfileWithWhitespaceLegalName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateAgency_NullAgencyType_CreatesAnAgency()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);

            AgencyProfile agencyProfileWithoutAgencyType = new AgencyProfileBuilder().WithAgencyName(new AgencyNameBuilder().WithLegalName(LegalName));

            agencyFactory.CreateAgency(agencyProfileWithoutAgencyType);
        }

        [TestMethod]
        public void CreateChildAgency_WithParentAgency_CreatesAChildAgency()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var parentAgency = new Mock<Agency>();
            var agencyType = new Mock<AgencyType>();

            Agency agency =
                agencyFactory.CreateChildAgency(
                new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(LegalName)), parentAgency.Object);

            Assert.AreSame(agency.ParentAgency, parentAgency.Object);
        }

        [TestMethod]
        public void CreateChildAgency_WithParentAgency_AgencyIsEditable()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var agencyRepository = new Mock<IAgencyRepository>();
                var agencyFactory = new AgencyFactory(agencyRepository.Object);
                var parentAgency = new Mock<Agency>();
                var agencyType = new Mock<AgencyType>();

                Agency agency = agencyFactory.CreateChildAgency(
                    new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(LegalName)), parentAgency.Object);

                agency.ReviseAgencyProfile(new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(LegalName).WithDisplayName("My Agency Display Name")));
            }
        }

        [TestMethod]
        public void CreateChildAgency_WithParentAgency_AgencyIsMadePersistent()
        {
            bool isPersistent = false;

            var agencyRepository = new Mock<IAgencyRepository>();

            agencyRepository
                .Setup(a => a.MakePersistent(It.IsAny<Agency>()))
                .Callback(() => isPersistent = true);
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var parentAgency = new Mock<Agency>();
            var agencyType = new Mock<AgencyType>();

            agencyFactory.CreateChildAgency(
                    new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(LegalName)), parentAgency.Object);

            Assert.IsTrue(isPersistent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateChildAgency_WithNullParentAgency_ThrowsArgumentException()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var agencyType = new Mock<AgencyType>();

            agencyFactory.CreateChildAgency(new AgencyProfileBuilder().WithAgencyType(agencyType.Object).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(LegalName)), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateChildAgency_WithNullLegalName_ThrowsArgumentException()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var parentAgency = new Mock<Agency>();
            var agencyType = new Mock<AgencyType>();

            agencyFactory.CreateChildAgency(new AgencyProfileBuilder().WithAgencyType(agencyType.Object), parentAgency.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateChildAgency_WithNullAgencyType_ThrowsArgumentException()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var parentAgency = new Mock<Agency>();

            agencyFactory.CreateChildAgency(new AgencyProfileBuilder().WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(LegalName)), parentAgency.Object);
        }

        [TestMethod]
        public void DestroyAgency_GivenAnAgency_AgencyIsMadeTransient()
        {
            bool isTransient = false;

            var agencyRepository = new Mock<IAgencyRepository>();

            agencyRepository.Setup(a => a.MakeTransient(It.IsAny<Agency>())).Callback(() => isTransient = true);
            var agencyFactory = new AgencyFactory(agencyRepository.Object);
            var agency = new Mock<Agency>();

            agencyFactory.DestroyAgency(agency.Object);

            Assert.IsTrue(isTransient);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DestroyAgency_GivenANullAgency_ThrowsArgumentException()
        {
            var agencyRepository = new Mock<IAgencyRepository>();
            var agencyFactory = new AgencyFactory(agencyRepository.Object);

            agencyFactory.DestroyAgency(null);
        }

        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure(c => c.For<IDomainEventService>().HybridHttpOrThreadLocalScoped().Use<DomainEventService>());
        }
    }
}