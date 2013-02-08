using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    [TestClass]
    public class LocationFactoryTests
    {
        private const string LegalName = "My Location";

        [TestMethod]
        public void CreateLocation_GivenValidArguments_CreatesALocation ()
        {

            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var locationRepository = new Mock<ILocationRepository>();
                var locationFactory = new LocationFactory(locationRepository.Object);
                var agency = new Mock<Agency>();

                var location = locationFactory.CreateLocation(
                    agency.Object, new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName(LegalName)));

                Assert.IsNotNull(location);
            }
        }

        [TestMethod]
        public void CreateLocation_GivenValidArguments_LocationIsMadePersistent ()
        {

            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var isPersistent = false;

                var locationRepository = new Mock<ILocationRepository>();
                locationRepository.Setup(l => l.MakePersistent(It.IsAny<Location>())).Callback(() => isPersistent = true);

                var locationFactory = new LocationFactory(locationRepository.Object);
                var agency = new Mock<Agency>();

                locationFactory.CreateLocation(
                    agency.Object, new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName(LegalName)));

                Assert.IsTrue(isPersistent);
            }
        }

        [TestMethod]
        public void CreateLocation_GivenValidArguments_LocationIsEditable ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var locationRepository = new Mock<ILocationRepository>();
                var locationFactory = new LocationFactory(locationRepository.Object);
                var agency = new Mock<Agency>();

                var location = locationFactory.CreateLocation(
                    agency.Object,
                        new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName(LegalName)));

                location.ReviseLocationProfile(
                    new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName("Some Name").Build()).Build());
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateLocation_NullAgency_ThrowsArgumentException ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var locationRepository = new Mock<ILocationRepository>();
                var locationFactory = new LocationFactory(locationRepository.Object);

                locationFactory.CreateLocation(null, new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName(LegalName)));
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateLocation_NullLegalName_ThrowsArgumentException ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var locationRepository = new Mock<ILocationRepository>();
                var locationFactory = new LocationFactory(locationRepository.Object);
                var agency = new Mock<Agency>();

                locationFactory.CreateLocation(agency.Object, null);
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateLocation_BlankLegalName_ThrowsArgumentException ()
        {
            var locationRepository = new Mock<ILocationRepository> ();
            var locationFactory = new LocationFactory ( locationRepository.Object );
            var agency = new Mock<Agency> ();

            locationFactory.CreateLocation ( agency.Object,
                        new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName("   ")));
        }

        [TestMethod]
        public void DestroyLocation_GivenALocation_LocationIsTransient ()
        {
            var isTransient = false;

            var locationRepository = new Mock<ILocationRepository> ();
            locationRepository
                .Setup ( l => l.MakeTransient ( It.IsAny<Location> () ) )
                .Callback ( () => isTransient = true );

            var locationFactory = new LocationFactory ( locationRepository.Object );
            var location = new Mock<Location> ();

            locationFactory.DestroyLocation ( location.Object );

            Assert.IsTrue ( isTransient );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void DestroyLocation_GivenANullLocation_ThrowsArgumentException ()
        {
            var locationRepository = new Mock<ILocationRepository> ();
            var locationFactory = new LocationFactory ( locationRepository.Object );

            locationFactory.DestroyLocation ( null );
        }

        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure(c => c.For<IDomainEventService>().HybridHttpOrThreadLocalScoped().Use<DomainEventService>());

            serviceLocatorFixture.StructureMapContainer.Configure(
                c => c.Scan(x =>
                {
                    // in the scan operation, include all needed dlls (Rem.*)
                    // be cautious in the future - this could still pick up unwanted assemblies,
                    // such as the stray test project that mistakenly ends up in the bin folder.
                    // so consider those possibilities if errors pop up, and you're led here.
                    x.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("Rem."));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollectionCustomizer<,>));
                }));
        }
    }
}