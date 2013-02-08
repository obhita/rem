using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rem.Domain.Clinical.Tests.ProgramModule
{
    [TestClass]
    public class ProgramEnrollmentFactoryTest
    {
        //TODO: FIX unit tests/rule engine to play nice.
        //[TestMethod]
        //public void Create_WithValidArguments_Succeed()
        //{
        //    using (var serviceLocatorFixture = new ServiceLocatorFixture())
        //    {
        //        // Setup
        //        TestFixture.SetupServiceLocatorFixture(serviceLocatorFixture);
        //        var repository = new Mock<IProgramEnrollmentRepository>();
        //        var programOffering = new Mock<ProgramOffering>();
        //        var clinicalCase = new Mock<ClinicalCase>();
        //        var enrollmentDate = new DateTime(2011, 10, 1);
        //        var enrollingStaff = new Mock<Staff>();
        //        programOffering.SetupGet(x => x.StartDate).Returns(new DateTime(2011, 1, 1));
        //        clinicalCase.SetupGet(x => x.ClinicalCaseStatus).Returns(
        //            new ClinicalCaseStatus { WellKnownName = WellKnownNames.ClinicalCaseModule.ClinicalCaseStatus.Active });
        //        enrollingStaff.SetupGet(x => x.StaffProfile.EmploymentDateRange).Returns(new DateRange(new DateTime(2011, 6, 1), new DateTime(2011, 12, 31)));

        //        var factory = new ProgramEnrollmentFactory(repository.Object);
        //        var programEnrollment = factory.CreateProgramEnrollment(
        //            programOffering.Object, clinicalCase.Object, enrollmentDate, enrollingStaff.Object);

        //        Assert.IsNotNull(programEnrollment);
        //    }
        //}

        //[TestMethod]
        //public void Create_WithClosedClinicalCase_RaiseValidationFailureEvent()
        //{
        //    using (var serviceLocatorFixture = new ServiceLocatorFixture())
        //    {
        //        // Setup
        //        TestFixture.SetupServiceLocatorFixture(serviceLocatorFixture);
        //        var repository = new Mock<IProgramEnrollmentRepository>();
        //        var programOffering = new Mock<ProgramOffering>();
        //        var clinicalCase = new Mock<ClinicalCase>();
        //        var enrollmentDate = new DateTime(2011, 10, 1);
        //        var enrollingStaff = new Mock<Staff>();
        //        programOffering.SetupGet(x => x.StartDate).Returns(new DateTime(2011, 1, 1));
        //        clinicalCase.SetupGet(x => x.ClinicalCaseStatus).Returns(
        //            new ClinicalCaseStatus { WellKnownName = WellKnownNames.ClinicalCaseModule.ClinicalCaseStatus.Closed });
        //        enrollingStaff.SetupGet(x => x.StaffProfile.EmploymentDateRange).Returns(new DateRange(new DateTime(2011, 6, 1), new DateTime(2011, 12, 31)));

        //        bool validationFailureEventRaised = false;
        //        DomainEvent.Register<RuleViolationEvent>(e => validationFailureEventRaised = true);

        //        var factory = new ProgramEnrollmentFactory(repository.Object);
        //        var programEnrollment = factory.CreateProgramEnrollment(
        //            programOffering.Object, clinicalCase.Object, enrollmentDate, enrollingStaff.Object);

        //        Assert.IsNull(programEnrollment);
        //        Assert.IsTrue(validationFailureEventRaised);
        //    }
        //}
    }
}
