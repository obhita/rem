using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.PatientModule.Event;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class PatientFactoryTests 
    {
        [TestMethod]
        public void CreatePatient_GivenValidParameters_PatientIsCreated()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var agency = new Mock<Agency>();
                var patientRepositoryMock = new Mock<IPatientRepository>();
                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator> ();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                var patient = patientFactory.CreatePatient(agency.Object, CreateValidName(), ValidPatientProfile());

                // Verify
                Assert.IsNotNull(patient);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenValidParameters_PatientIsMadePersistent()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var isPersistent = false;

                var patientRepositoryMock = new Mock<IPatientRepository>();
                patientRepositoryMock
                    .Setup(p => p.MakePersistent(It.IsAny<Patient>()))
                    .Callback(() => isPersistent = true);
                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), ValidPatientProfile());

                // Verify
                Assert.IsTrue(isPersistent);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenValidParameters_PatientCreatedEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<PatientCreatedEvent>(p => eventRaised = true);

                var patientRepositoryMock = new Mock<IPatientRepository>();

                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), ValidPatientProfile());

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenValidParameters_ValidationFailureEventIsNotRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                var patientRepositoryMock = new Mock<IPatientRepository>();

                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), ValidPatientProfile());

                // Verify
                Assert.IsFalse(eventRaised);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullBirthDate_PatientIsNotCreated()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var agency = new Mock<Agency>();
                var patientRepositoryMock = new Mock<IPatientRepository>();
                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                var patient = patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullBirthDate());

                // Verify
                Assert.IsNull(patient);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullBirthDate_PatientIsNotMadePersistent()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var isPersistent = false;

                var patientRepositoryMock = new Mock<IPatientRepository>();
                patientRepositoryMock
                    .Setup(p => p.MakePersistent(It.IsAny<Patient>()))
                    .Callback(() => isPersistent = true);
                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullBirthDate());

                // Verify
                Assert.IsFalse(isPersistent);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullBirthDate_PatientCreatedEventIsNotRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<PatientCreatedEvent>(p => eventRaised = true);

                var patientRepositoryMock = new Mock<IPatientRepository>();

                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullBirthDate());

                // Verify
                Assert.IsFalse(eventRaised);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullBirthDate_ValidationFailureEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                var patientRepositoryMock = new Mock<IPatientRepository>();

                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullBirthDate());

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullPatientGender_PatientIsNotCreated()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var agency = new Mock<Agency>();
                var patientRepositoryMock = new Mock<IPatientRepository>();
                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                var patient = patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullPatientGender());

                // Verify
                Assert.IsNull(patient);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullPatientGender_PatientIsNotMadePersistent()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var isPersistent = false;

                var patientRepositoryMock = new Mock<IPatientRepository>();
                patientRepositoryMock
                    .Setup(p => p.MakePersistent(It.IsAny<Patient>()))
                    .Callback(() => isPersistent = true);
                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullPatientGender());

                // Verify
                Assert.IsFalse(isPersistent);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullPatientGender_PatientCreatedEventIsNotRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<PatientCreatedEvent>(p => eventRaised = true);

                var patientRepositoryMock = new Mock<IPatientRepository>();

                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullPatientGender());

                // Verify
                Assert.IsFalse(eventRaised);
            }
        }

        [TestMethod]
        public void CreatePatient_GivenPatientProfileWithNullPatientGender_ValidationFailureEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                var patientRepositoryMock = new Mock<IPatientRepository>();

                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                patientFactory.CreatePatient(agency.Object, CreateValidName(), InvalidPatientProfileWithNullPatientGender());

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

         [TestMethod]
        public void CreatePatient_GivenValidParameters_PatientIsMadePersistent2()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                string uniqueIdentifier = "unique identifier";

                var patientRepositoryMock = new Mock<IPatientRepository>();
                
                var agency = new Mock<Agency>();

                var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();

                patientUniqueIdentifierCalculatorMock
                    .Setup ( p => p.GenerateUniqueIdentifier( It.IsAny<Patient> () ) )
                    .Returns(() => uniqueIdentifier);

                var patientFactory = new PatientFactory(
                    patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);

                // Exercise
                var patient = patientFactory.CreatePatient(agency.Object, CreateValidName(), ValidPatientProfile());

                // Verify
                Assert.AreSame(uniqueIdentifier, patient.UniqueIdentifier, "Patient uniqueIdentifier is not assigned correctly.");
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( NotImplementedException ) )]
        public void DestroyPatient_GivenAPatient_ThrowsNotImplementedException ()
        {
            // Setup
            var patientRepositoryMock = new Mock<IPatientRepository> ();
            var patientUniqueIdentifierCalculatorMock = new Mock<IPatientUniqueIdentifierGenerator>();
            var patientFactory = new PatientFactory(
                patientRepositoryMock.Object, patientUniqueIdentifierCalculatorMock.Object);
            var patient = new Mock<Patient> ();

            // Exercise
            patientFactory.DestroyPatient ( patient.Object );

            // Verify
        }

        private static PersonName CreateValidName ()
        {
            return new PersonName ( "prefix", "first", "middle", "last", "suffix" );
        }

        private static PatientProfile ValidPatientProfile()
        {
            return new PatientProfileBuilder()
                .WithBirthDate(new DateTime(2000, 1, 1))
                .WithPatientGender(new Mock<PatientGender>().Object);
        }

        private static PatientProfile InvalidPatientProfileWithNullBirthDate()
        {
            return new PatientProfileBuilder()
                .WithPatientGender(new Mock<PatientGender>().Object);
        }

        private static PatientProfile InvalidPatientProfileWithNullPatientGender()
        {
            return new PatientProfileBuilder()
                .WithBirthDate(new DateTime(2000, 1, 1));
        }

        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure (
                c => c.For<IDomainEventService> ().HybridHttpOrThreadLocalScoped ().Use<DomainEventService> () );

            serviceLocatorFixture.StructureMapContainer.Configure(
                c => c.Scan(x =>
                {
                    // in the scan operation, include all needed dlls (Rem.*)
                    // be cautious in the future - this could still pick up unwanted assemblies,
                    // such as the stray test project that mistakenly ends up in the bin folder.
                    // so consider those possibilities if errors pop up, and you're led here.
                    x.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("Rem.Domain"));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollectionCustomizer<,>));
                }));
        }
    }
}