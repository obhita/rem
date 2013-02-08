using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.LabModule
{
    [TestClass]
    public class LabSpecimenFactoryTests
    {
        [TestMethod]
        public void CreateLabSpecimen_GivenValidArguments_CreatesLabSpecimen()
        {
            var activityType = new Mock<ActivityType>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            lookupValueRepository
                .Setup(l => l.GetLookupByWellKnownName<ActivityType>(It.IsAny<string>()))
                .Returns(activityType.Object);

            var labSpecimenRepository = new Mock<ILabSpecimenRepository>();

            var labSpecimenFactory = new LabSpecimenFactory(
                labSpecimenRepository.Object,
                lookupValueRepository.Object);

            var visit = new Mock<Visit>();
            var clinicalCase = new Mock<ClinicalCase>();
            var patient = new Mock<Patient>();

            visit.Setup(v => v.ClinicalCase).Returns(clinicalCase.Object);
            clinicalCase.Setup(c => c.Patient).Returns(patient.Object);

            LabSpecimen labSpecimen = labSpecimenFactory.CreateLabSpecimen(visit.Object);

            Assert.IsNotNull(labSpecimen);
        }

        [TestMethod]
        public void CreateLabSpecimen_GivenValidArguments_LabSpecimenIsEditable()
        {

            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var activityType = new Mock<ActivityType>();
                var lookupValueRepository = new Mock<ILookupValueRepository>();
                lookupValueRepository
                    .Setup(l => l.GetLookupByWellKnownName<ActivityType>(It.IsAny<string>()))
                    .Returns(activityType.Object);

                var labSpecimenRepository = new Mock<ILabSpecimenRepository>();

                var labSpecimenFactory = new LabSpecimenFactory(
                    labSpecimenRepository.Object,
                    lookupValueRepository.Object);

                var visit = new Mock<Visit>();
                var clinicalCase = new Mock<ClinicalCase>();
                var patient = new Mock<Patient>();
                var labSpecimenType = new Mock<LabSpecimenType>();

                visit.Setup(v => v.ClinicalCase).Returns(clinicalCase.Object);
                clinicalCase.Setup(c => c.Patient).Returns(patient.Object);

                var labSpecimen = labSpecimenFactory.CreateLabSpecimen(visit.Object);
                labSpecimen.ReviseLabSpecimenType ( labSpecimenType.Object );
            }
        }

        [TestMethod]
        public void CreateLabSpecimen_GivenValidArguments_LabSpecimenIsMadePersistent()
        {
            bool isPersistent = false;

            var activityType = new Mock<ActivityType>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            lookupValueRepository
                .Setup(l => l.GetLookupByWellKnownName<ActivityType>(It.IsAny<string>()))
                .Returns(activityType.Object);

            var labSpecimenRepository = new Mock<ILabSpecimenRepository>();
            labSpecimenRepository
                .Setup(v => v.MakePersistent(It.IsAny<LabSpecimen>()))
                .Callback(() => isPersistent = true);

            var labSpecimenFactory = new LabSpecimenFactory(
                labSpecimenRepository.Object,
                lookupValueRepository.Object);

            var visit = new Mock<Visit>();
            var clinicalCase = new Mock<ClinicalCase>();
            var patient = new Mock<Patient>();

            visit.Setup(v => v.ClinicalCase).Returns(clinicalCase.Object);
            clinicalCase.Setup(c => c.Patient).Returns(patient.Object);

            labSpecimenFactory.CreateLabSpecimen(visit.Object);

            Assert.IsTrue(isPersistent);
        }

        [TestMethod]
        public void CreateLabSpecimen_NullVisit_ThrowsArgumentException()
        {
            var activityType = new Mock<ActivityType>();
            var lookupValueRepository = new Mock<ILookupValueRepository>();
            lookupValueRepository
                .Setup(l => l.GetLookupByWellKnownName<ActivityType>(It.IsAny<string>()))
                .Returns(activityType.Object);

            var labSpecimenRepository = new Mock<ILabSpecimenRepository>();

            var labSpecimenFactory = new LabSpecimenFactory(
                labSpecimenRepository.Object,
                lookupValueRepository.Object);

            var labSpecimenType = new Mock<LabSpecimenType>();

            try
            {
                labSpecimenFactory.CreateLabSpecimen(null);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void DestoryLabSpecimen_GivenLabSpecimen_LabSpecimenIsTransient()
        {
            bool isTransient = false;

            var lookupValueRepository = new Mock<ILookupValueRepository>();
            var labSpecimenRepository = new Mock<ILabSpecimenRepository>();
            labSpecimenRepository
                .Setup(v => v.MakeTransient(It.IsAny<LabSpecimen>()))
                .Callback(() => isTransient = true);

            var labSpecimenFactory = new LabSpecimenFactory(
                labSpecimenRepository.Object,
                lookupValueRepository.Object);

            var labSpecimen = new Mock<LabSpecimen>();
            var visit = new Mock<Visit>();
            var clinicalCase = new Mock<ClinicalCase>();
            var patient = new Mock<Patient>();

            labSpecimen.Setup(v => v.Visit).Returns(visit.Object);
            visit.Setup(v => v.ClinicalCase).Returns(clinicalCase.Object);
            clinicalCase.Setup(c => c.Patient).Returns(patient.Object);

            labSpecimenFactory.DestroyLabSpecimen(labSpecimen.Object);

            Assert.IsTrue(isTransient);
        }
    }
}