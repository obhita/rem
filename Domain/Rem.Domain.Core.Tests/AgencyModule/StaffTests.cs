using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    [TestClass]
    public class StaffTests
    {
        [TestMethod]
        public void SetPrimaryLocation_GivenNullLocation_Succeeds()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                // Setup
                var staff = new Staff(
                    new Mock<Agency>().Object,
                    new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("first name").WithLast("last name")));

                // Exercise
                staff.SetPrimaryLocation(null);

                // Verify
                Assert.IsNull(staff.PrimaryLocation);
            }
        }

        [TestMethod]
        public void SetPrimaryLocation_GivenLocationButWithoutAgencyLocations_PrimaryLocationIsNotChanged()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                var staff = new Staff(new Mock<Agency>().Object,
                        new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("first name").WithLast("last name")));

                var location = new Location(
                    new Mock<Agency>().Object,
                    new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName("location name")));

                staff.SetPrimaryLocation(null);

                // Exercise
                staff.SetPrimaryLocation(location);

                // Verify
                Assert.IsTrue ( staff.PrimaryLocation == null );
            }
        }

        [TestMethod]
        public void SetPrimaryLocation_GivenLocationButWithoutAgencyLocations_ValidationFailureEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var staff = new Staff(new Mock<Agency>().Object,
                          new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("first name").WithLast("last name")));

                var location = new Location(
                    new Mock<Agency>().Object,
                    new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName("location name")));

                bool eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                // Exercise
                staff.SetPrimaryLocation(location);

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

        //TODO: Fix these two next tests/or rule engine to play nice.
        //[TestMethod]
        //public void SetPrimaryLocation_GivenAgencyLocationsAndStaffLocationAssignments_PrimaryLocationIsChanged()
        //{
        //    using (var serviceLocatorFixture = new ServiceLocatorFixture())
        //    {
        //        // Setup
        //        SetupServiceLocatorFixture(serviceLocatorFixture);

        //        var agencyMock = new Mock<Agency>();
        //        agencyMock.SetupGet(p => p.Key).Returns(1);

        //        var location1Mock = new Mock<Location>();
        //        location1Mock.SetupGet(p => p.Key).Returns(11);
        //        location1Mock.SetupGet(p => p.Agency).Returns(agencyMock.Object);

        //        var location2Mock = new Mock<Location>();
        //        location2Mock.SetupGet(p => p.Key).Returns(12);
        //        location2Mock.SetupGet(p => p.Agency).Returns(agencyMock.Object);


        //        agencyMock.SetupGet(p => p.Locations).Returns(new[] { location1Mock.Object, location2Mock.Object });

        //        var staff = new Staff(agencyMock.Object,
        //                  new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirstName("first name").WithLastName("last name")));

        //        staff.AssignLocation(location1Mock.Object);



        //        // Exercise
        //        staff.SetPrimaryLocation(location1Mock.Object);

        //        // Verify
        //        Assert.IsTrue(staff.PrimaryLocation.Key == location1Mock.Object.Key);
        //    }
        //}

        //[TestMethod]
        //public void SetPrimaryLocation_GivenAgencyLocationsAndStaffLocationAssignments_ValidationFailureEventIsNotRaised()
        //{
        //    using (var serviceLocatorFixture = new ServiceLocatorFixture())
        //    {
        //        // Setup
        //        SetupServiceLocatorFixture(serviceLocatorFixture);

        //        var agencyMock = new Mock<Agency>();
        //        agencyMock.SetupGet(p => p.Key).Returns(1);

        //        var location1Mock = new Mock<Location>();
        //        location1Mock.SetupGet(p => p.Key).Returns(11);
        //        location1Mock.SetupGet(p => p.Agency).Returns(agencyMock.Object);

        //        var location2Mock = new Mock<Location>();
        //        location2Mock.SetupGet(p => p.Key).Returns(12);
        //        location2Mock.SetupGet(p => p.Agency).Returns(agencyMock.Object);


        //        agencyMock.SetupGet(p => p.Locations).Returns(new[] { location1Mock.Object, location2Mock.Object });

        //        var staff = new Staff(agencyMock.Object,
        //                  new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirstName("first name").WithLastName("last name")));

        //        staff.AssignLocation(location1Mock.Object);

        //        bool eventRaised = false;
        //        DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

        //        // Exercise
        //        staff.SetPrimaryLocation(location1Mock.Object);

        //        // Verify
        //        Assert.IsFalse(eventRaised);
        //    }
        //}

        //[TestMethod]
        //public void SetPrimaryLocation_GivenLocationInAgencyLocations_PrimaryLocationIsChanged()
        //{
        //    using (var serviceLocatorFixture = new ServiceLocatorFixture())
        //    {
        //        // Setup
        //        SetupServiceLocatorFixture(serviceLocatorFixture);

        //        var location1Mock = new Mock<Location> ();
        //        location1Mock.SetupGet ( p => p.Key ).Returns ( 1 );
        //        var location2Mock = new Mock<Location>();
        //        location2Mock.SetupGet ( p => p.Key ).Returns ( 2 );

        //        var agencyMock = new Mock<Agency>();
        //        agencyMock.SetupGet(p => p.Locations).Returns(new[] { location1Mock.Object, location2Mock.Object });

        //        var staff = new Staff(agencyMock.Object,
        //                  new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirstName("first name").WithLastName("last name")));

        //        staff.SetPrimaryLocation(null);

        //        // Exercise
        //        staff.SetPrimaryLocation(location1Mock.Object);

        //        // Verify
        //        Assert.IsTrue(staff.PrimaryLocation == location1Mock.Object);
        //    }
        //}

        //[TestMethod]
        //public void SetPrimaryLocation_GivenLocationInAgencyLocations_ValidationFailureEventIsNotRaised()
        //{
        //    using (var serviceLocatorFixture = new ServiceLocatorFixture())
        //    {
        //        // Setup
        //        SetupServiceLocatorFixture(serviceLocatorFixture);

        //        var location1Mock = new Mock<Location>();
        //        location1Mock.SetupGet(p => p.Key).Returns(1);
        //        var location2Mock = new Mock<Location>();
        //        location2Mock.SetupGet(p => p.Key).Returns(2);

        //        var agencyMock = new Mock<Agency>();
        //        agencyMock.SetupGet(p => p.Locations).Returns(new[] { location1Mock.Object, location2Mock.Object });

        //        var staff = new Staff(agencyMock.Object,
        //                  new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirstName("first name").WithLastName("last name")));

        //        staff.SetPrimaryLocation(null);

        //        bool eventRaised = false;
        //        DomainEvent.Register<ValidationFailureEvent>(p => eventRaised = true);

        //        // Exercise
        //        staff.SetPrimaryLocation(location1Mock.Object);

        //        // Verify
        //        Assert.IsFalse(eventRaised);
        //    }
        //}

        [TestMethod]
        public void SetPrimaryLocation_GivenLocationNotInAgencyLocations_PrimaryLocationIsNotChanged()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var location1Mock = new Mock<Location>();
                location1Mock.SetupGet(p => p.Key).Returns(1);
                var location2Mock = new Mock<Location>();
                location2Mock.SetupGet(p => p.Key).Returns(2);

                var agencyMock = new Mock<Agency>();
                agencyMock.SetupGet(p => p.Locations).Returns(new[] { location1Mock.Object, location2Mock.Object });

                var staff = new Staff(agencyMock.Object,
                          new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("first name").WithLast("last name")));

                staff.SetPrimaryLocation(null);

                // Exercise
                staff.SetPrimaryLocation(new Mock<Location>().Object);

                // Verify
                Assert.IsTrue(staff.PrimaryLocation == null);
            }
        }

        [TestMethod]
        public void SetPrimaryLocation_GivenLocationNotInAgencyLocations_ValidationFailureEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var location1Mock = new Mock<Location>();
                location1Mock.SetupGet(p => p.Key).Returns(1);
                var location2Mock = new Mock<Location>();
                location2Mock.SetupGet(p => p.Key).Returns(2);

                var agencyMock = new Mock<Agency>();
                agencyMock.SetupGet(p => p.Locations).Returns(new[] { location1Mock.Object, location2Mock.Object });

                var staff = new Staff(agencyMock.Object,
                          new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst("first name").WithLast("last name")));

                staff.SetPrimaryLocation(null);

                bool eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                // Exercise
                staff.SetPrimaryLocation(new Mock<Location>().Object);

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure(
                c => c.For<IDomainEventService>().HybridHttpOrThreadLocalScoped().Use<DomainEventService>());

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
