using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Billing.ClaimModule;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap.Tests
{
    [TestClass]
    public class VisitImportServiceTests
    {
        //[TestMethod]
        //public void ImportEncounterFromVisit_BillingOfficeDoesNotExist_ErrorShouldBeReportedToCodingContext()
        //{
        //    // Setup
        //    var fixture = new Fixture().Customize(new AutoMoqCustomization());

        //    var billingOfficeRepository = new Mock<IBillingOfficeRepository>();
        //    billingOfficeRepository.Setup(p => p.GetByAgencyKey(It.IsAny<long>())).Returns<BillingOffice>(null);
        //    fixture.Register(() => billingOfficeRepository.Object);

        //    var codingContext = new Mock<CodingContext>();
        //    var codeContextRepository = new Mock<ICodingContextRepository> ();
        //    codeContextRepository.Setup ( p => p.GetByVisitKey ( It.IsAny<long> () ) ).Returns ( codingContext.Object );
        //    fixture.Register ( () => codeContextRepository.Object );

        //    var patient = new Mock<Patient>();
        //    var agency = new Mock<Agency> ();
        //    patient.Setup ( p => p.Agency ).Returns ( agency.Object );

        //    var visit = new Mock<Visit>();
        //    visit.Setup(v => v.ClinicalCase.Patient).Returns(patient.Object);

        //    var visitRepository = new Mock<IVisitRepository>();
        //    visitRepository.Setup(p => p.GetByKey((It.IsAny<long>()))).Returns(visit.Object);
        //    fixture.Register(() => visitRepository.Object);

        //    var visitImportService = fixture.CreateAnonymous<VisitImportService>();

        //    // Exercise
        //    visitImportService.ImportVisit(fixture.CreateAnonymous<long>());

        //    // Verify
        //    codingContext.Verify(p => p.ReportError(It.IsAny<string>()), Times.Exactly(1));
        //}

        [TestMethod]
        public void ImportEncounterFromVisit_BillingOfficeExists_SynchronizePatientAccountCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var billingOffice = new Mock<BillingOffice> ();
            var billingOfficeRepository = new Mock<IBillingOfficeRepository>();
            billingOfficeRepository.Setup(p => p.GetByAgencyKey(It.IsAny<long>())).Returns(billingOffice.Object);
            fixture.Register(() => billingOfficeRepository.Object);

            var patientAccountSynchronizationService = new Mock<IPatientAccountSynchronizationService> ();
            fixture.Register(() => patientAccountSynchronizationService.Object);

            var patient = new Mock<Patient>();
            var agency = new Mock<Agency>();
            patient.Setup(p => p.Agency).Returns(agency.Object);

            var visit = new Mock<Visit>();
            visit.Setup(v => v.ClinicalCase.Patient).Returns(patient.Object);

            var visitRepository = new Mock<IVisitRepository>();
            visitRepository.Setup(p => p.GetByKey((It.IsAny<long>()))).Returns(visit.Object);
            fixture.Register(() => visitRepository.Object);

            var claimBatch = new Mock<ClaimBatch>();
            var claim = new Mock<Claim>();
            claim.Setup(p => p.AssignClaimBatch()).Returns(() => claimBatch.Object);

            var encounter = new Mock<Encounter>();
            encounter.Setup(p => p.GenerateClaim()).Returns(claim.Object);
            var encounterSynchronizationService = new Mock<IEncounterSynchronizationService>();
            encounterSynchronizationService.Setup(p => p.SynchronizeEncounter(It.IsAny<PatientAccount>(), It.IsAny<Visit>())).Returns(
                () => encounter.Object);
            fixture.Register(() => encounterSynchronizationService);

            var visitImportService = fixture.CreateAnonymous<VisitImportService>();

            // Exercise
            visitImportService.ImportVisit(fixture.CreateAnonymous<long>());

            // Verify
            patientAccountSynchronizationService.Verify(p => p.SynchronizePatientAccount(patient.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void ImportEncounterFromVisit_BillingOfficeExists_SynchronizeEncounterCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var billingOffice = new Mock<BillingOffice>();
            var billingOfficeRepository = new Mock<IBillingOfficeRepository>();
            billingOfficeRepository.Setup(p => p.GetByAgencyKey(It.IsAny<long>())).Returns(billingOffice.Object);
            fixture.Register(() => billingOfficeRepository.Object);

            var patientAccount = new Mock<PatientAccount> ();
            var patientAccountSynchronizationService = new Mock<IPatientAccountSynchronizationService>();
            patientAccountSynchronizationService.Setup ( p => p.SynchronizePatientAccount ( It.IsAny<Patient> () ) ).Returns ( patientAccount.Object );
            fixture.Register(() => patientAccountSynchronizationService.Object);

            var patient = new Mock<Patient>();
            var agency = new Mock<Agency>();
            patient.Setup(p => p.Agency).Returns(agency.Object);

            var visit = new Mock<Visit>();
            visit.Setup(v => v.ClinicalCase.Patient).Returns(patient.Object);

            var visitRepository = new Mock<IVisitRepository>();
            visitRepository.Setup(p => p.GetByKey((It.IsAny<long>()))).Returns(visit.Object);
            fixture.Register(() => visitRepository.Object);

            var claimBatch = new Mock<ClaimBatch>();
            var claim = new Mock<Claim>();
            claim.Setup(p => p.AssignClaimBatch()).Returns(() => claimBatch.Object);

            var encounter = new Mock<Encounter>();
            encounter.Setup(p => p.GenerateClaim()).Returns(claim.Object);

            var encounterSynchronizationService = new Mock<IEncounterSynchronizationService> ();

            encounterSynchronizationService.Setup ( p => p.SynchronizeEncounter ( It.IsAny<PatientAccount> (), It.IsAny<Visit> () ) ).Returns (
                () => encounter.Object );
            fixture.Register(() => encounterSynchronizationService);

            var visitImportService = fixture.CreateAnonymous<VisitImportService>();

            // Exercise
            visitImportService.ImportVisit(fixture.CreateAnonymous<long>());

            // Verify
            encounterSynchronizationService.Verify(p => p.SynchronizeEncounter(patientAccount.Object, visit.Object), Times.Exactly(1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ImportEncounterFromVisit_BillingOfficeExistsAndCodingContextHasProceduresAndVisitHasNoProblem_SynchronizeEncounterCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var billingOffice = new Mock<BillingOffice>();
            var billingOfficeRepository = new Mock<IBillingOfficeRepository>();
            billingOfficeRepository.Setup(p => p.GetByAgencyKey(It.IsAny<long>())).Returns(billingOffice.Object);
            fixture.Register(() => billingOfficeRepository.Object);

            var patientAccount = new Mock<PatientAccount>();
            var patientAccountSynchronizationService = new Mock<IPatientAccountSynchronizationService>();
            patientAccountSynchronizationService.Setup(p => p.SynchronizePatientAccount(It.IsAny<Patient>())).Returns(patientAccount.Object);
            fixture.Register(() => patientAccountSynchronizationService.Object);

            var patient = new Mock<Patient>();
            var agency = new Mock<Agency>();
            patient.Setup(p => p.Agency).Returns(agency.Object);

            var visit = new Mock<Visit>();
            visit.SetupGet ( p => p.Problems ).Returns ( new List<VisitProblem> () );
            visit.Setup(v => v.ClinicalCase.Patient).Returns(patient.Object);

            var visitRepository = new Mock<IVisitRepository>();
            visitRepository.Setup(p => p.GetByKey((It.IsAny<long>()))).Returns(visit.Object);
            fixture.Register(() => visitRepository.Object);

            var codingContext = new Mock<CodingContext>();
            var procedures = new List<Procedure> { new Mock<Procedure> ().Object, new Mock<Procedure> ().Object};
            codingContext.SetupGet ( p => p.Procedures ).Returns ( () => procedures );
            var codeContextRepository = new Mock<ICodingContextRepository>();
            codeContextRepository.Setup(p => p.GetByVisitKey(It.IsAny<long>())).Returns(codingContext.Object);
            fixture.Register(() => codeContextRepository.Object);

            var serviceSynchronizationService = new Mock<IServiceSynchronizationService> ();
            fixture.Register(() => serviceSynchronizationService.Object);

            var visitImportService = fixture.CreateAnonymous<VisitImportService>();

            // Exercise
            visitImportService.ImportVisit(fixture.CreateAnonymous<long>());

            // Verify
        }

        [TestMethod]
        public void ImportEncounterFromVisit_BillingOfficeExistsAndCodingContextHasProceduresAndVisitHasProblems_SynchronizeEncounterCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var billingOffice = new Mock<BillingOffice>();
            var billingOfficeRepository = new Mock<IBillingOfficeRepository>();
            billingOfficeRepository.Setup(p => p.GetByAgencyKey(It.IsAny<long>())).Returns(billingOffice.Object);
            fixture.Register(() => billingOfficeRepository.Object);

            var patientAccount = new Mock<PatientAccount>();
            var patientAccountSynchronizationService = new Mock<IPatientAccountSynchronizationService>();
            patientAccountSynchronizationService.Setup(p => p.SynchronizePatientAccount(It.IsAny<Patient>())).Returns(patientAccount.Object);
            fixture.Register(() => patientAccountSynchronizationService.Object);

            var patient = new Mock<Patient>();
            var agency = new Mock<Agency>();
            patient.Setup(p => p.Agency).Returns(agency.Object);

            var diagnosisCodedConcept = fixture.CreateAnonymous<CodedConcept>();

            var problem = new Mock<Problem> ();
            problem.SetupGet ( p => p.ProblemCodeCodedConcept ).Returns ( diagnosisCodedConcept );

            var visitProblem = new Mock<VisitProblem> ();
            visitProblem.SetupGet ( p => p.Problem ).Returns ( problem.Object );

            var visit = new Mock<Visit>();
            visit.SetupGet(p => p.Problems).Returns(new List<VisitProblem>{visitProblem.Object});
            visit.Setup(v => v.ClinicalCase.Patient).Returns(patient.Object);

            var visitRepository = new Mock<IVisitRepository>();
            visitRepository.Setup(p => p.GetByKey((It.IsAny<long>()))).Returns(visit.Object);
            fixture.Register(() => visitRepository.Object);

            var claimBatch = new Mock<ClaimBatch> ();
            var claim = new Mock<Claim> ();
            claim.Setup ( p => p.AssignClaimBatch () ).Returns ( () => claimBatch.Object );

            var encounter = new Mock<Encounter> ();
            encounter.Setup(p => p.GenerateClaim()).Returns(claim.Object);
            var encounterSynchronizationService = new Mock<IEncounterSynchronizationService>();
            encounterSynchronizationService.Setup(p => p.SynchronizeEncounter(It.IsAny<PatientAccount>(), visit.Object)).Returns(encounter.Object);
            fixture.Register(() => encounterSynchronizationService);

            var procedureOne = new Mock<Procedure> ();
            var procedureTwo = new Mock<Procedure> ();
            var procedures = new List<Procedure> { procedureOne.Object, procedureTwo.Object };

            var codingContext = new Mock<CodingContext>();
            codingContext.SetupGet ( p => p.Procedures ).Returns ( () => procedures );
            var codeContextRepository = new Mock<ICodingContextRepository>();
            codeContextRepository.Setup(p => p.GetByVisitKey(It.IsAny<long>())).Returns(codingContext.Object);
            fixture.Register(() => codeContextRepository.Object);

            var serviceSynchronizationService = new Mock<IServiceSynchronizationService> ();
            fixture.Register(() => serviceSynchronizationService.Object);

            var visitImportService = fixture.CreateAnonymous<VisitImportService>();

            // Exercise
            visitImportService.ImportVisit(fixture.CreateAnonymous<long>());

            // Verify
            serviceSynchronizationService.Verify(p => p.SynchronizeService(encounter.Object, It.IsAny<Procedure>(), diagnosisCodedConcept), Times.Exactly(2));

            serviceSynchronizationService.Verify(p => p.SynchronizeService(encounter.Object, procedureOne.Object, diagnosisCodedConcept), Times.Exactly(1));

            serviceSynchronizationService.Verify(p => p.SynchronizeService(encounter.Object, procedureOne.Object, diagnosisCodedConcept), Times.Exactly(1));
        }
    }
}
