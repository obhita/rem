using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Primitives;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.Tests.VisitModule
{
    [TestClass]
    public class VisitFactoryTests
    {
        [TestMethod]
        public void CreateVisit_GivenValidArguments_CreatesVisit ()
        {
            var visitRepository = new Mock<IVisitRepository> ();
            var visitStatus = new Mock<VisitStatus> ();
            visitStatus.SetupProperty ( v => v.WellKnownName, WellKnownNames.VisitModule.VisitStatus.Scheduled );

            var visitStatusRepository = new Mock<IVisitStatusRepository> ();
            visitStatusRepository
                .Setup ( v => v.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled ) )
                .Returns ( visitStatus.Object );

            var visitFactory = new VisitFactory (
                visitRepository.Object,
                visitStatusRepository.Object
                );

            var patient = new Mock<Patient> ();

            var clinicalCase = new Mock<ClinicalCase> ();
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );

            var agency = new Mock<Agency> ();
            var visitTemplate = new VisitTemplate( agency.Object, "Initial Behavioral Health - Adult", "99204" );

            var staff = new Mock<Staff> ();
            var initialLocation = new Mock<Location> ();
            var appointmentDateTimeRange = new DateTimeRange ( new DateTime (), new DateTime () );

            var visit = visitFactory.CreateVisit (
                staff.Object,
                appointmentDateTimeRange,
                clinicalCase.Object,
                visitTemplate,
                initialLocation.Object );

            Assert.IsNotNull ( visit );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateVisit_NullAppointment_ThrowsArgumentException ()
        {
            var visitRepository = new Mock<IVisitRepository> ();
            var visitStatus = new Mock<VisitStatus> ();
            visitStatus.SetupProperty ( v => v.WellKnownName, WellKnownNames.VisitModule.VisitStatus.Scheduled );
            var visitStatusRepository = new Mock<IVisitStatusRepository> ();
            visitStatusRepository
                .Setup ( v => v.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled ) )
                .Returns ( visitStatus.Object );

            var visitFactory = new VisitFactory (
                visitRepository.Object,
                visitStatusRepository.Object );

            var patient = new Mock<Patient> ();

            var clinicalCase = new Mock<ClinicalCase> ();
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );
            var visitTemplate = new Mock<VisitTemplate> ();
            var initialLocation = new Mock<Location> ();

            var appointmentDateTimeRange = new DateTimeRange ( new DateTime (), new DateTime () );

            visitFactory.CreateVisit (
                null,
                appointmentDateTimeRange,
                clinicalCase.Object,
                visitTemplate.Object,
                initialLocation.Object );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateVisit_NullClinicalCase_ThrowsArgumentException ()
        {
            var visitRepository = new Mock<IVisitRepository> ();
            var visitStatus = new Mock<VisitStatus> ();
            visitStatus.SetupProperty ( v => v.WellKnownName, WellKnownNames.VisitModule.VisitStatus.Scheduled );
            var visitStatusRepository = new Mock<IVisitStatusRepository> ();
            visitStatusRepository
                .Setup ( v => v.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled ) )
                .Returns ( visitStatus.Object );

            var visitFactory = new VisitFactory (
                visitRepository.Object,
                visitStatusRepository.Object );

            var patient = new Mock<Patient> ();

            var visitTemplate = new Mock<VisitTemplate> ();
            var initialLocation = new Mock<Location> ();

            var staff = new Mock<Staff> ();
            var appointmentDateTimeRange = new DateTimeRange ( new DateTime (), new DateTime () );

            visitFactory.CreateVisit (
                staff.Object,
                appointmentDateTimeRange,
                null,
                visitTemplate.Object,
                initialLocation.Object );
        }

        [TestMethod]
        [ExpectedException ( typeof ( NullReferenceException ) )]
        public void CreateVisit_NullVisitTemplate_ThrowsNullReferenceException ()
        {
            var visitRepository = new Mock<IVisitRepository> ();
            var visitStatus = new Mock<VisitStatus> ();
            visitStatus.SetupProperty ( v => v.WellKnownName, WellKnownNames.VisitModule.VisitStatus.Scheduled );
            var visitStatusRepository = new Mock<IVisitStatusRepository> ();
            visitStatusRepository
                .Setup ( v => v.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled ) )
                .Returns ( visitStatus.Object );

            var visitFactory = new VisitFactory (
                visitRepository.Object,
                visitStatusRepository.Object );

            var patient = new Mock<Patient> ();

            var clinicalCase = new Mock<ClinicalCase> ();
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );
            var initialLocation = new Mock<Location> ();

            var staff = new Mock<Staff> ();
            var appointmentDateTimeRange = new DateTimeRange ( new DateTime (), new DateTime () );

            visitFactory.CreateVisit (
                staff.Object,
                appointmentDateTimeRange,
                clinicalCase.Object,
                null,
                initialLocation.Object );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateVisit_NullInitialLocation_ThrowsArgumentException ()
        {
            var visitRepository = new Mock<IVisitRepository> ();
            var visitStatus = new Mock<VisitStatus> ();
            visitStatus.SetupProperty ( v => v.WellKnownName, WellKnownNames.VisitModule.VisitStatus.Scheduled );
            var visitStatusRepository = new Mock<IVisitStatusRepository> ();
            visitStatusRepository
                .Setup ( v => v.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled ) )
                .Returns ( visitStatus.Object );

            var visitFactory = new VisitFactory (
                visitRepository.Object,
                visitStatusRepository.Object );

            var patient = new Mock<Patient> ();

            var clinicalCase = new Mock<ClinicalCase> ();
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );
            var visitTemplate = new Mock<VisitTemplate> ();

            var staff = new Mock<Staff> ();
            var appointmentDateTimeRange = new DateTimeRange ( new DateTime (), new DateTime () );

            visitFactory.CreateVisit (
                staff.Object,
                appointmentDateTimeRange,
                clinicalCase.Object,
                visitTemplate.Object,
                null );
        }

        [TestMethod]
        public void CreateVisit_GivenValidArguments_VisitIsEditable ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                var visitRepository = new Mock<IVisitRepository> ();
                var visitStatus = new Mock<VisitStatus> ();
                visitStatus.SetupProperty ( v => v.WellKnownName, WellKnownNames.VisitModule.VisitStatus.Scheduled );
                var visitStatusRepository = new Mock<IVisitStatusRepository> ();
                visitStatusRepository
                    .Setup ( v => v.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled ) )
                    .Returns ( visitStatus.Object );

                var visitFactory = new VisitFactory (
                    visitRepository.Object,
                    visitStatusRepository.Object );

                var patient = new Mock<Patient> ();

                var clinicalCase = new Mock<ClinicalCase> ();
                clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );

                var agency = new Mock<Agency>();
                var visitTemplate = new VisitTemplate(agency.Object, "Initial Behavioral Health - Adult", "99204" );

                var initialLocation = new Mock<Location> ();

                var staff = new Mock<Staff> ();
                var appointmentDateTimeRange = new DateTimeRange ( new DateTime (), new DateTime () );

                var visit = visitFactory.CreateVisit (
                    staff.Object,
                    appointmentDateTimeRange,
                    clinicalCase.Object,
                    visitTemplate,
                    initialLocation.Object );

                visit.ReviseNote ( "note" );
            }
        }

        [TestMethod]
        public void DestroyVisit_GivenAVisit_VisitIsMadeTransient ()
        {
            bool isTransient = false;

            var visitRepository = new Mock<IVisitRepository> ();
            visitRepository
                .Setup ( v => v.MakeTransient ( It.IsAny<Visit> () ) )
                .Callback ( () => isTransient = true );
            var visitStatus = new Mock<VisitStatus> ();
            visitStatus.SetupProperty ( v => v.WellKnownName, WellKnownNames.VisitModule.VisitStatus.Scheduled );
            var visitStatusRepository = new Mock<IVisitStatusRepository> ();
            visitStatusRepository
                .Setup ( v => v.GetByWellKnownName ( WellKnownNames.VisitModule.VisitStatus.Scheduled ) )
                .Returns ( visitStatus.Object );

            var visitFactory = new VisitFactory (
                visitRepository.Object,
                visitStatusRepository.Object );

            var patient = new Mock<Patient> ();

            var clinicalCase = new Mock<ClinicalCase> ();
            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );
            var visit = new Mock<Visit> ();
            visit.Setup ( v => v.ClinicalCase ).Returns ( clinicalCase.Object );

            visitFactory.DestroyVisit ( visit.Object );

            Assert.IsTrue ( isTransient );
        }
    }
}