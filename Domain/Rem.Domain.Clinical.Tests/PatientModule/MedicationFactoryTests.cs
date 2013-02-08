using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class MedicationFactoryTests 
    {
        [TestMethod]
        public void CreateMedication_GivenValidArguments_Succeeds ()
        {
            var mediationRepository = new Mock<IMedicationRepository> ();
            var medicationFactory = new MedicationFactory (
                mediationRepository.Object );

            var patient = new Mock<Patient> ();

            var medicationCode = new CodedConceptBuilder().WithCodedConceptCode("TheCode");
            var medication = medicationFactory.CreateMedication ( patient.Object, medicationCode, medicationCode );

            Assert.IsNotNull ( medication );
        }

        [TestMethod]
        public void CreateMedication_GivenValidArguments_MedicationIsEditable ()
        {

            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var mediationRepository = new Mock<IMedicationRepository>();
                var medicationFactory = new MedicationFactory(
                    mediationRepository.Object);

                var patient = new Mock<Patient>();

                var medicationCode = new CodedConceptBuilder().WithCodedConceptCode("TheCode");
                var medication = medicationFactory.CreateMedication(patient.Object, medicationCode, medicationCode);

                medication.ReviseInstructionsNote ( "some instruction" );
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateMedication_NullPatient_ThrowsArgumentException ()
        {
            var mediationRepository = new Mock<IMedicationRepository> ();
            var medicationFactory = new MedicationFactory (
                mediationRepository.Object );

            var medicationCode = new Mock<CodedConcept> ();
            medicationFactory.CreateMedication ( null, medicationCode.Object, medicationCode.Object );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateMedication_NullMedicationCode_ThrowsArgumentException ()
        {
            var mediationRepository = new Mock<IMedicationRepository> ();
            var medicationFactory = new MedicationFactory (
                mediationRepository.Object );

            var patient = new Mock<Patient> ();
            var provenance = new Mock<Provenance> ();

            medicationFactory.CreateMedication ( patient.Object, null, provenance.Object );
        }

        [TestMethod]
        public void CreateMedication_GivenValidArguments_MedicationMadePersistent ()
        {
            var isPersistent = false;

            var mediationRepository = new Mock<IMedicationRepository> ();
            mediationRepository
                .Setup ( m => m.MakePersistent ( It.IsAny<Medication> () ) )
                .Callback ( () => isPersistent = true );
            var medicationFactory = new MedicationFactory (
                mediationRepository.Object );

            var patient = new Mock<Patient> ();

            var medicationCode = new CodedConceptBuilder().WithCodedConceptCode("TheCode");
            medicationFactory.CreateMedication ( patient.Object, medicationCode, medicationCode );

            Assert.IsTrue ( isPersistent );
        }

        [TestMethod]
        public void DestroyMedication_GivenAMedication_MedicationMadeTransient ()
        {
            var isTransient = false;

            var mediationRepository = new Mock<IMedicationRepository> ();
            mediationRepository
                .Setup ( m => m.MakeTransient ( It.IsAny<Medication> () ) )
                .Callback ( () => isTransient = true );
            var medicationFactory = new MedicationFactory (
                mediationRepository.Object );
            var medication = new Mock<Medication> ();
            var patient = new Mock<Patient> ();
            medication.Setup ( m => m.Patient ).Returns ( patient.Object );

            medicationFactory.DestroyMedication ( medication.Object );

            Assert.IsTrue ( isTransient );
        }
    }
}