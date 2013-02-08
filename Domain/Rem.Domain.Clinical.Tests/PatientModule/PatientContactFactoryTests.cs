using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class PatientContactFactoryTests
    {
        [TestMethod]
        public void CreatePatientContact_GivenValidArguments_CreatesContact ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );
                var patientContactRepository = new Mock<IPatientContactRepository> ();
                var patientContactFactory = new PatientContactFactory ( patientContactRepository.Object );

                var patient = new Mock<Patient> ();

                PatientContact patientContact = patientContactFactory.CreatePatientContact ( patient.Object, "Fred", "Smith" );

                Assert.IsNotNull ( patientContact );
            }
        }

        [TestMethod]
        public void CreatePatientContact_GivenValidArguments_ContactIsPersistent ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture ( serviceLocatorFixture );
                bool isPersistent = false;

                var patientContactRepository = new Mock<IPatientContactRepository> ();
                patientContactRepository.Setup ( p => p.MakePersistent ( It.IsAny<PatientContact> () ) ).Callback ( () => isPersistent = true );
                var patientContactFactory = new PatientContactFactory ( patientContactRepository.Object );

                var patient = new Mock<Patient> ();

                patientContactFactory.CreatePatientContact ( patient.Object, "Fred", "Smith" );

                Assert.IsTrue ( isPersistent );
            }
        }

        [TestMethod]
        public void CreatePatientContact_GivenValidArguments_ContactIsEditable ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var patientContactRepository = new Mock<IPatientContactRepository>();
                var patientContactFactory = new PatientContactFactory(
                    patientContactRepository.Object);

                var patient = new Mock<Patient>();

                PatientContact patientContact = patientContactFactory.CreatePatientContact(
                    patient.Object, "Fred", "Smith");

                patientContact.ReviseNote("some note");
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientContact_NullPatient_ThrowsArgumentException ()
        {
            var patientContactRepository = new Mock<IPatientContactRepository> ();
            var patientContactFactory = new PatientContactFactory (
                patientContactRepository.Object );

            patientContactFactory.CreatePatientContact ( null, "Fred", "Smith" );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientContact_NullFirstName_ThrowsArgumentException ()
        {
            var patientContactRepository = new Mock<IPatientContactRepository> ();
            var patientContactFactory = new PatientContactFactory (
                patientContactRepository.Object );

            var patient = new Mock<Patient> ();

            patientContactFactory.CreatePatientContact ( patient.Object, null, "Smith" );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientContact_NullLastName_ThrowsArgumentException ()
        {
            var patientContactRepository = new Mock<IPatientContactRepository> ();
            var patientContactFactory = new PatientContactFactory (
                patientContactRepository.Object );

            var patient = new Mock<Patient> ();

            patientContactFactory.CreatePatientContact ( patient.Object, "Fred", null );
        }

        [TestMethod]
        public void DestroyPatientContact_GivenAPatientContact_ContactIsTransient ()
        {
            bool isTransient = false;

            var patientContactRepository = new Mock<IPatientContactRepository> ();
            patientContactRepository
                .Setup ( p => p.MakeTransient ( It.IsAny<PatientContact> () ) )
                .Callback ( () => isTransient = true );
            var patientContactFactory = new PatientContactFactory (
                patientContactRepository.Object );

            var patient = new Mock<Patient> ();
            var patientContact = new Mock<PatientContact> ();
            patientContact.Setup ( p => p.Patient ).Returns ( patient.Object );

            patientContactFactory.DestroyPatientContact ( patientContact.Object );

            Assert.IsTrue ( isTransient );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void DestroyPatientContact_GivenANullPatientContact_ThrowsArgumentException ()
        {
            var patientContactRepository = new Mock<IPatientContactRepository> ();
            var patientContactFactory = new PatientContactFactory (
                patientContactRepository.Object );

            patientContactFactory.DestroyPatientContact ( null );
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
                    x.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("Rem."));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollectionCustomizer<,>));
                }));
        }
    }
}