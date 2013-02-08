using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.ClinicalBilling.ContextMap.Tests
{
    [TestClass]
    public class EncounterSynchronizationServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SynchronizeEncounter_GivenNullPatientAccount_ThrowsArgumentException()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var visit = new Mock<Visit> ();

            var encounterSynchronizationService = fixture.CreateAnonymous<EncounterSynchronizationService>();

            // Exercise
            encounterSynchronizationService.SynchronizeEncounter ( null, visit.Object );

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SynchronizeEncounter_GivenNullVisit_ThrowsArgumentException()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccount = new Mock<PatientAccount>();

            var encounterSynchronizationService = fixture.CreateAnonymous<EncounterSynchronizationService>();

            // Exercise
            encounterSynchronizationService.SynchronizeEncounter(patientAccount.Object, null);

            // Verify
        }

        [TestMethod]
        public void SynchronizeEncounter_EncounterDoesNotExist_EncounterIsCreated()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccount = new Mock<PatientAccount>();
            var visit = new Mock<Visit>();

            var encounterRepository = new Mock<IEncounterRepository> ();
            encounterRepository.Setup ( p => p.GetByTrackingNumber ( It.IsAny<long> () ) ).Returns<Encounter> ( null );
            fixture.Register(() => encounterRepository.Object);

            var encounterSynchronizationService = fixture.CreateAnonymous<EncounterSynchronizationService>();

            // Exercise
            var encounter = encounterSynchronizationService.SynchronizeEncounter(patientAccount.Object, visit.Object);

            // Verify
            Assert.IsNotNull ( encounter );
        }

        [TestMethod]
        public void SynchronizeEncounter_EncounterDoesNotExist_CreatesEncounterCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccount = new Mock<PatientAccount>();
            var visit = new Mock<Visit>();
            var serviceProvider = new Mock<Staff>();
            var placeOfService = new Mock<Location> ();
            visit.SetupGet ( p => p.ServiceLocation ).Returns ( placeOfService.Object );
            visit.SetupGet ( p => p.Staff ).Returns ( serviceProvider.Object );

            var encounterRepository = new Mock<IEncounterRepository>();
            encounterRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns<Encounter>(null);
            fixture.Register(() => encounterRepository.Object);

            var encounterFactory = new Mock<IEncounterFactory> ();
            encounterFactory.Setup (
                p =>
                p.CreateEncounter (
                    patientAccount.Object,
                    placeOfService.Object,
                    serviceProvider.Object,
                    visit.Object.Key,
                    It.IsAny<DateTime>() ) ).Returns ( new Mock<Encounter> ().Object );
            fixture.Register(() => encounterFactory.Object);

            var encounterSynchronizationService = fixture.CreateAnonymous<EncounterSynchronizationService>();

            // Exercise
            var encounter = encounterSynchronizationService.SynchronizeEncounter(patientAccount.Object, visit.Object);

            // Verify
            encounterFactory.Verify(p => p.CreateEncounter(patientAccount.Object, placeOfService.Object, serviceProvider.Object, visit.Object.Key, It.IsAny<DateTime>()));
        }

        [TestMethod]
        public void SynchronizeEncounter_EncounterWithDifferenctPatientAccountExistsAlready_RevisesPatientAccountCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccount = new Mock<PatientAccount>();
            patientAccount.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            var visit = new Mock<Visit>();
            var serviceProvider = new Mock<Staff>();
            var placeOfService = new Mock<Location>();
            visit.SetupGet(p => p.ServiceLocation).Returns(placeOfService.Object);
            visit.SetupGet(p => p.Staff).Returns(serviceProvider.Object);

            var encounterPatientAccount = new Mock<PatientAccount> ();
            encounterPatientAccount.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            var encounter = new Mock<Encounter> ();
            encounter.SetupGet ( p => p.PatientAccount ).Returns ( encounterPatientAccount.Object );
            encounter.SetupGet(p => p.ServiceLocation).Returns(placeOfService.Object);
            encounter.SetupGet(p => p.ServiceProviderStaff).Returns(serviceProvider.Object);

            var encounterRepository = new Mock<IEncounterRepository>();
            encounterRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(encounter.Object);
            fixture.Register(() => encounterRepository.Object);

            var encounterFactory = new Mock<IEncounterFactory>();
            fixture.Register(() => encounterFactory.Object);

            var encounterSynchronizationService = fixture.CreateAnonymous<EncounterSynchronizationService>();

            // Exercise
            encounterSynchronizationService.SynchronizeEncounter(patientAccount.Object, visit.Object);

            // Verify
            encounter.Verify(p => p.RevisePatientAccount(patientAccount.Object));
        }

        [TestMethod]
        public void SynchronizeEncounter_EncounterWithDifferenctServiceProviderExistsAlready_RevisesServiceProviderCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccount = new Mock<PatientAccount>();
            patientAccount.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
            var visit = new Mock<Visit>();
            var serviceProvider = new Mock<Staff>();
            var placeOfService = new Mock<Location>();
            visit.SetupGet(p => p.ServiceLocation).Returns(placeOfService.Object);
            visit.SetupGet(p => p.Staff).Returns(serviceProvider.Object);

            var encounterServiceProvider = new Mock<Staff>();
            encounterServiceProvider.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
            var encounter = new Mock<Encounter>();
            encounter.SetupGet(p => p.PatientAccount).Returns(patientAccount.Object);
            encounter.SetupGet(p => p.ServiceLocation).Returns(placeOfService.Object);
            encounter.SetupGet(p => p.ServiceProviderStaff).Returns(encounterServiceProvider.Object);

            var encounterRepository = new Mock<IEncounterRepository>();
            encounterRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(encounter.Object);
            fixture.Register(() => encounterRepository.Object);

            var encounterFactory = new Mock<IEncounterFactory>();
            fixture.Register(() => encounterFactory.Object);

            var encounterSynchronizationService = fixture.CreateAnonymous<EncounterSynchronizationService>();

            // Exercise
            encounterSynchronizationService.SynchronizeEncounter(patientAccount.Object, visit.Object);

            // Verify
            encounter.Verify(p => p.ReviseServiceProvider(serviceProvider.Object));
        }

        [TestMethod]
        public void SynchronizeEncounter_EncounterWithDifferenctPlaceOfServiceExistsAlready_RevisesPlaceOfServiceCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccount = new Mock<PatientAccount>();
            patientAccount.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
            var visit = new Mock<Visit>();
            var serviceProvider = new Mock<Staff>();
            var placeOfService = new Mock<Location>();
            visit.SetupGet(p => p.ServiceLocation).Returns(placeOfService.Object);
            visit.SetupGet(p => p.Staff).Returns(serviceProvider.Object);

            var encounterPlaceOfService = new Mock<Location>();
            encounterPlaceOfService.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
            var encounter = new Mock<Encounter>();
            encounter.SetupGet(p => p.PatientAccount).Returns(patientAccount.Object);
            encounter.SetupGet(p => p.ServiceLocation).Returns(encounterPlaceOfService.Object);
            encounter.SetupGet(p => p.ServiceProviderStaff).Returns(serviceProvider.Object);

            var encounterRepository = new Mock<IEncounterRepository>();
            encounterRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(encounter.Object);
            fixture.Register(() => encounterRepository.Object);

            var encounterFactory = new Mock<IEncounterFactory>();
            fixture.Register(() => encounterFactory.Object);

            var encounterSynchronizationService = fixture.CreateAnonymous<EncounterSynchronizationService>();

            // Exercise
            encounterSynchronizationService.SynchronizeEncounter(patientAccount.Object, visit.Object);

            // Verify
            encounter.Verify(p => p.RevisePlaceOfService(placeOfService.Object));
        }
    }
}
