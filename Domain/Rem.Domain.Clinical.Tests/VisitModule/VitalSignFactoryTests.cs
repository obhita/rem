using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.VisitModule
{
    [TestClass]
    public class VitalSignFactoryTests
    {
        [TestMethod]
        public void CreateVitalSign_GivenValidArguments_CreatesVitalSign ()
        {
            var activityType = new Mock<ActivityType> ();
            var lookupValueRepository = new Mock<ILookupValueRepository> ();
            lookupValueRepository
                .Setup ( l => l.GetLookupByWellKnownName<ActivityType> ( It.IsAny<string> () ) )
                .Returns ( activityType.Object );

            var vitalSignRepository = new Mock<IVitalSignRepository> ();

            var vitalSignFactory = new VitalSignFactory (
                vitalSignRepository.Object,
                lookupValueRepository.Object );

            var visit = new Mock<Visit> ();
            var clinicalCase = new Mock<ClinicalCase> ();
            var patient = new Mock<Patient> ();

            visit.Setup ( v => v.ClinicalCase ).Returns ( clinicalCase.Object );
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );

            var vitalSign = vitalSignFactory.CreateVitalSign ( visit.Object );

            Assert.IsNotNull ( vitalSign );
        }

        [TestMethod]
        public void CreateVitalSign_GivenValidArguments_VitalSignIsEditable ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var activityType = new Mock<ActivityType>();
                var lookupValueRepository = new Mock<ILookupValueRepository>();
                lookupValueRepository
                    .Setup(l => l.GetLookupByWellKnownName<ActivityType>(It.IsAny<string>()))
                    .Returns(activityType.Object);

                var vitalSignRepository = new Mock<IVitalSignRepository>();

                var vitalSignFactory = new VitalSignFactory(
                    vitalSignRepository.Object,
                    lookupValueRepository.Object);

                var visit = new Mock<Visit>();
                var clinicalCase = new Mock<ClinicalCase>();
                var patient = new Mock<Patient>();

                visit.Setup(v => v.ClinicalCase).Returns(clinicalCase.Object);
                clinicalCase.Setup(c => c.Patient).Returns(patient.Object);

                var vitalSign = vitalSignFactory.CreateVitalSign(visit.Object);

                vitalSign.ReviseHeight ( new Height ( 100, null ) );
            }
        }

        [TestMethod]
        public void CreateVitalSign_GivenValidArguments_VitalSignIsMadePersistent ()
        {
            var isPersistent = false;

            var activityType = new Mock<ActivityType> ();
            var lookupValueRepository = new Mock<ILookupValueRepository> ();
            lookupValueRepository
                .Setup ( l => l.GetLookupByWellKnownName<ActivityType> ( It.IsAny<string> () ) )
                .Returns ( activityType.Object );

            var vitalSignRepository = new Mock<IVitalSignRepository> ();
            vitalSignRepository
                .Setup ( v => v.MakePersistent ( It.IsAny<VitalSign> () ) )
                .Callback ( () => isPersistent = true );

            var vitalSignFactory = new VitalSignFactory (
                vitalSignRepository.Object,
                lookupValueRepository.Object );

            var visit = new Mock<Visit> ();
            var clinicalCase = new Mock<ClinicalCase> ();
            var patient = new Mock<Patient> ();

            visit.Setup ( v => v.ClinicalCase ).Returns ( clinicalCase.Object );
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );

            vitalSignFactory.CreateVitalSign ( visit.Object );

            Assert.IsTrue ( isPersistent );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateVitalSign_NullVisit_ThrowsArgumentException ()
        {
            var activityType = new Mock<ActivityType> ();
            var lookupValueRepository = new Mock<ILookupValueRepository> ();
            lookupValueRepository
                .Setup ( l => l.GetLookupByWellKnownName<ActivityType> ( It.IsAny<string> () ) )
                .Returns ( activityType.Object );

            var vitalSignRepository = new Mock<IVitalSignRepository> ();

            var vitalSignFactory = new VitalSignFactory (
                vitalSignRepository.Object,
                lookupValueRepository.Object );

            vitalSignFactory.CreateVitalSign ( null );
        }

        [TestMethod]
        public void DestoryVitalSign_GivenVitalSign_VitalSignIsTransient ()
        {
            var isTransient = false;

            var lookupValueRepository = new Mock<ILookupValueRepository> ();
            var vitalSignRepository = new Mock<IVitalSignRepository> ();
            vitalSignRepository
                .Setup ( v => v.MakeTransient ( It.IsAny<VitalSign> () ) )
                .Callback ( () => isTransient = true );

            var vitalSignFactory = new VitalSignFactory (
                vitalSignRepository.Object,
                lookupValueRepository.Object );

            var vitalSign = new Mock<VitalSign> ();
            var visit = new Mock<Visit> ();
            var clinicalCase = new Mock<ClinicalCase> ();
            var patient = new Mock<Patient> ();

            vitalSign.Setup ( v => v.Visit ).Returns ( visit.Object );
            visit.Setup ( v => v.ClinicalCase ).Returns ( clinicalCase.Object );
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );

            vitalSignFactory.DestroyVitalSign ( vitalSign.Object );

            Assert.IsTrue ( isTransient );
        }
    }
}