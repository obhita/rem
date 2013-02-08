using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Pillar.Domain.Primitives;
using Pillar.FluentRuleEngine;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.PatientModule.Event;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;
using Pillar.Common.Extension;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class PatientTests
    {
        [TestMethod]
        public void Patient_AllPropertiesReadOnly_ShouldBeTrue ()
        {
            var patientType = typeof ( Patient );
            PropertyInfo[] properties;
            var allPropertiesAreReadonly = patientType.AllPropertiesAreReadonly ( out properties );
            Assert.IsTrue (
                allPropertiesAreReadonly,
                string.Format ( "The following properties are writable: {0}", ( string.Join ( ", ", properties.Select ( s => s.Name ).ToArray () ) ) ) );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rename_GivenNullAgency_ThrowsArgumentException()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                Agency agency = null;
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agency, name, new PatientProfileBuilder().Build());

                // Exercise
                patient.Rename(null);

                // Verify
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rename_GivenNullPersonName_ThrowsArgumentException()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());

                // Exercise
                patient.Rename(null);

                // Verify
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rename_GivenNullPatientProfile_ThrowsArgumentException()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, null);

                // Exercise
                patient.Rename(null);

                // Verify
            }
        }

        [TestMethod]
        public void Rename_GivenValidName_NameIsChanged()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());

                var newName = new PersonNameBuilder()
                    .WithFirst("Fred")
                    .WithLast("Thomas");

                // Exercise
                patient.Rename(newName);

                // Verify
                Assert.IsTrue(patient.Name == newName);
            }
        }

        [TestMethod]
        public void Rename_GivenValidName_PatientRenamedEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<PatientRenamedEvent> ( p => eventRaised = true );

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());

                var newName = new PersonNameBuilder()
                    .WithFirst("Fred")
                    .WithLast("Thomas");

                // Exercise
                patient.Rename(newName);

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

        [TestMethod]
        public void Rename_GivenValidName_PatientRenamedEventWithCorrectInfoIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                Patient patientArgumentInEvent = null;
                DomainEvent.Register<PatientRenamedEvent> ( p => patientArgumentInEvent = p.Patient );

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());

                var newName = new PersonNameBuilder()
                    .WithFirst("Fred")
                    .WithLast("Thomas");

                // Exercise
                patient.Rename(newName);

                // Verify
                Assert.IsTrue(ReferenceEquals(patient, patientArgumentInEvent) && patientArgumentInEvent.Name == newName);
            }
        }

        [TestMethod]
        public void Rename_GivenValidName_ValidationFailureEventIsNotRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<RuleViolationEvent> ( p => eventRaised = true );

                var agency = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agency.Object, name, new PatientProfileBuilder().Build());

                var newName = new PersonNameBuilder()
                    .WithFirst("Fred")
                    .WithLast("Thomas");

                // Exercise
                patient.Rename(newName);

                // Verify
                Assert.IsFalse(eventRaised);
            }
        }

        [TestMethod]
        public void Rename_GivenTheSameName_NameIsNotChanged()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());

                var newName = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");

                // Exercise
                patient.Rename(newName);

                // Verify
                Assert.IsTrue(patient.Name == name);
            }
        }

        [TestMethod]
        public void Rename_GivenTheSameName_PatientRenamedEventIsNotRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<PatientRenamedEvent>(p => eventRaised = true);

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());

                // Exercise
                patient.Rename(name);

                // Verify
                Assert.IsFalse(eventRaised);
            }
        }

        [TestMethod]
        public void Rename_GivenTheSameName_ValidationFailureEventIsRaised()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);

                bool eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");
                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());

                // Exercise
                patient.Rename(name);

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }

        [TestMethod]
        public void AddAddress_GivenDuplicate_ValidationFailureEventIsRaised ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );

                var eventRaised = false;
                DomainEvent.Register<RuleViolationEvent>(p => eventRaised = true);

                var agencyMock = new Mock<Agency>();
                var name = new PersonNameBuilder()
                    .WithFirst("Albert")
                    .WithLast("Smith");

                var address = new AddressBuilder ()
                    .WithFirstStreetAddress ( "123 Test Street" )
                    .WithCityName ( "Testville" )
                    .WithStateProvince ( new StateProvince () )
                    .WithPostalCode ( new PostalCode ( "12345" ) )
                    .Build ();

                var patientAddress = new PatientAddressBuilder ()
                    .WithPatientAddressType ( new PatientAddressType () )
                    .WithAddress(address)
                    .Build ();

                var patient = new Patient(agencyMock.Object, name, new PatientProfileBuilder().Build());
                patient.AddAddress ( patientAddress );

                // Exercise
                patient.AddAddress(patientAddress);

                // Verify
                Assert.IsTrue(eventRaised);
            }
        }


        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure (
                c => c.For<IDomainEventService> ().HybridHttpOrThreadLocalScoped ().Use<DomainEventService> () );

            serviceLocatorFixture.StructureMapContainer.Configure (
                c => c.Scan ( x =>
                       {
                           // in the scan operation, include all needed dlls (Rem.*)
                           // be cautious in the future - this could still pick up unwanted assemblies,
                           // such as the stray test project that mistakenly ends up in the bin folder.
                           // so consider those possibilities if errors pop up, and you're led here.
                           x.AssembliesFromApplicationBaseDirectory ( p => ( p.FullName == null ) ? false : p.FullName.Contains ( "Rem." ) );

                           x.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));

                           x.ConnectImplementationsToTypesClosing(typeof(IRuleCollectionCustomizer<,>));
                       }) ); 
        }
    }
}