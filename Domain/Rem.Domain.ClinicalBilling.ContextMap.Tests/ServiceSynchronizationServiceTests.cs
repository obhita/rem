using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap.Tests
{
    [TestClass]
    public class ServiceSynchronizationServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SynchronizeService_GivenNullEncounter_ThrowsArgumentException()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var procedure = new Mock<Procedure> ();

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(null, procedure.Object, fixture.CreateAnonymous<CodedConcept>());

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SynchronizeService_GivenNullProcedure_ThrowsArgumentException()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, null, fixture.CreateAnonymous<CodedConcept>());

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SynchronizeService_GivenNullDiagnosis_ThrowsArgumentException()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter> ();
            var procedure = new Mock<Procedure> ();

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, null);

            // Verify
        }

        [TestMethod]
        public void SynchronizeService_GivenNotNullProcedure_TranslatesToMedicalProcedureCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            var procedure = new Mock<Procedure>();
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service>();
            service.SetupGet ( p => p.Encounter ).Returns ( new Mock<Encounter> ().Object );
            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            fixture.Register(() => medicalProcedureTranslator);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            medicalProcedureTranslator.Verify( p => p.Translate(procedure.Object));
        }

        [TestMethod]
        public void SynchronizeService_ServiceExists_ReturnsExistingServiceCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            var procedure = new Mock<Procedure>();
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service> ();
            service.SetupGet(p => p.Encounter).Returns(new Mock<Encounter>().Object);
            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            medicalProcedureTranslator.Setup(p => p.Translate(It.IsAny<Procedure>())).Returns(medicalProcedure);
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory>();
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            var returnedService = serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            Assert.AreSame(service.Object, returnedService);
        }

        [TestMethod]
        public void SynchronizeService_ServiceDoesNotExist_CreatesServiceCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter> ();
            var procedure = new Mock<Procedure>();
            procedure.SetupGet ( p => p.ProcedureType ).Returns ( ProcedureType.Activity );
            var diagnosis = fixture.CreateAnonymous<CodedConcept> ();

            var serviceRepository = new Mock<IServiceRepository> ();
            serviceRepository.Setup ( p => p.GetByTrackingNumber ( It.IsAny<long> () ) ).Returns<Service> ( null );
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure> ();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator> ();
            medicalProcedureTranslator.Setup ( p => p.Translate ( It.IsAny<Procedure> () ) ).Returns ( medicalProcedure );
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory> ();
            serviceFactory.Setup(p => p.CreateService(encounter.Object, diagnosis, medicalProcedure, false, procedure.Object.Key)).Returns(
                new Mock<Service>().Object);
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            serviceFactory.Verify(p => p.CreateService(encounter.Object, diagnosis, medicalProcedure, false, procedure.Object.Key ));
        }

        [TestMethod]
        public void SynchronizeService_EncounterIsChanged_RevisesEncounterCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            encounter.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            var procedure = new Mock<Procedure>();
            procedure.SetupGet(p => p.ProcedureType).Returns(ProcedureType.Activity);
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service> ();
            var serviceEncounter = new Mock<Encounter> ();
            serviceEncounter.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            service.SetupGet ( p => p.Encounter ).Returns ( serviceEncounter.Object );

            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            medicalProcedureTranslator.Setup(p => p.Translate(It.IsAny<Procedure>())).Returns(medicalProcedure);
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory>();
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            service.Verify(p => p.ReviseEncounter(encounter.Object));
        }

        [TestMethod]
        public void SynchronizeService_DiagnosisIsChanged_RevisesDiagnosisCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            var procedure = new Mock<Procedure>();
            procedure.SetupGet(p => p.ProcedureType).Returns(ProcedureType.Activity);
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service>();
            var serviceDiagnosis = fixture.CreateAnonymous<CodedConcept> ();
            service.SetupGet ( p => p.Diagnosis ).Returns ( serviceDiagnosis );
            service.SetupGet ( p => p.Encounter ).Returns ( encounter.Object );

            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            medicalProcedureTranslator.Setup(p => p.Translate(It.IsAny<Procedure>())).Returns(medicalProcedure);
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory>();
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            service.Verify(p => p.ReviseDiagnosis(diagnosis));
        }

        [TestMethod]
        public void SynchronizeService_MedicalProcedureIsChanged_RevisesMedicalProcedureCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            var procedure = new Mock<Procedure>();
            procedure.SetupGet(p => p.ProcedureType).Returns(ProcedureType.Activity);
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service>();
            var serviceMMedicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            service.SetupGet(p => p.MedicalProcedure).Returns(serviceMMedicalProcedure);
            service.SetupGet(p => p.Encounter).Returns(encounter.Object);

            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            medicalProcedureTranslator.Setup(p => p.Translate(It.IsAny<Procedure>())).Returns(medicalProcedure);
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory>();
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            service.Verify(p => p.ReviseMedicalProcedure(medicalProcedure));
        }

        [TestMethod]
        public void SynchronizeService_PrimaryIndicatorIsChanged_RevisesPrimaryIndicatorCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            var procedure = new Mock<Procedure>();
            procedure.SetupGet(p => p.ProcedureType).Returns(ProcedureType.Activity);
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service>();
            service.SetupGet(p => p.PrimaryIndicator).Returns(true);
            service.SetupGet(p => p.Encounter).Returns(encounter.Object);

            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            medicalProcedureTranslator.Setup(p => p.Translate(It.IsAny<Procedure>())).Returns(medicalProcedure);
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory>();
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            service.Verify(p => p.RevisePrimaryIndicator(false));
        }

        [TestMethod]
        public void SynchronizeService_TrackingNumberIsChanged_RevisesTrackingNumberCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            var procedure = new Mock<Procedure>();
            procedure.SetupGet(p => p.ProcedureType).Returns(ProcedureType.Activity);
            procedure.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service>();
            service.SetupGet(p => p.TrackingNumber).Returns(fixture.CreateAnonymous<long>());
            service.SetupGet(p => p.Encounter).Returns(encounter.Object);

            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            medicalProcedureTranslator.Setup(p => p.Translate(It.IsAny<Procedure>())).Returns(medicalProcedure);
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory>();
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            service.Verify(p => p.ReviseTrackingNumber(procedure.Object.Key));
        }

        [TestMethod]
        public void SynchronizeService_BillingUnitCountIsChanged_RevisesBillingUnitCountCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var encounter = new Mock<Encounter>();
            var procedure = new Mock<Procedure>();
            procedure.SetupGet(p => p.ProcedureType).Returns(ProcedureType.Activity);
            procedure.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
            var procedureBillingUnitCount = fixture.CreateAnonymous<UnitCount> ();
            procedure.SetupGet ( p => p.BillingUnitCount ).Returns ( procedureBillingUnitCount );
            var diagnosis = fixture.CreateAnonymous<CodedConcept>();

            var service = new Mock<Service>();
            service.SetupGet(p => p.BillingUnitCount).Returns(fixture.CreateAnonymous<UnitCount>());
            service.SetupGet(p => p.Encounter).Returns(encounter.Object);

            var serviceRepository = new Mock<IServiceRepository>();
            serviceRepository.Setup(p => p.GetByTrackingNumber(It.IsAny<long>())).Returns(service.Object);
            fixture.Register(() => serviceRepository.Object);

            var medicalProcedure = fixture.CreateAnonymous<MedicalProcedure>();
            var medicalProcedureTranslator = new Mock<IMedicalProcedureTranslator>();
            medicalProcedureTranslator.Setup(p => p.Translate(It.IsAny<Procedure>())).Returns(medicalProcedure);
            fixture.Register(() => medicalProcedureTranslator);

            var serviceFactory = new Mock<IServiceFactory>();
            fixture.Register(() => serviceFactory);

            var serviceSynchronizationService = fixture.CreateAnonymous<ServiceSynchronizationService>();

            // Exercise
            serviceSynchronizationService.SynchronizeService(encounter.Object, procedure.Object, diagnosis);

            // Verify
            service.Verify(p => p.ReviseBillingUnitCount(procedureBillingUnitCount));
        }
    }
}
