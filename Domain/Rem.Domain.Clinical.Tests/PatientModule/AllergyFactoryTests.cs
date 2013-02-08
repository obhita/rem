using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class AllergyFactoryTests
    {
        [TestMethod]
        public void CreateAllergy_GivenValidArguments_CreatesAllergy ()
        {
            var allergy = CreateAllergyByAllergyFactory ();
            Assert.IsNotNull ( allergy );
        }

        [TestMethod]
        public void CreateAllergy_GivenValidArguments_AllergyIsEditable ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                var allergy = CreateAllergyByAllergyFactory();
                allergy.ReviseAllergySeverityType ( new AllergySeverityType() );
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateAllergy_NullPatient_ThrowsArgumentException ()
        {
            var allergyRepository = new Mock<IAllergyRepository> ();
            var allergyFactory = new AllergyFactory (
                allergyRepository.Object );
            var allergyStatus = new Mock<AllergyStatus> ();
            var allergen = new Mock<CodedConcept> ();
            allergyFactory.CreateAllergy ( null, allergyStatus.Object, allergen.Object );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateAllergy_NullAllergen_ThrowsArgumentException ()
        {
            var allergyRepository = new Mock<IAllergyRepository> ();
            var allergyFactory = new AllergyFactory (
                allergyRepository.Object );

            var patient = new Mock<Patient> ();
            var allergyStatus = new Mock<AllergyStatus> ();
            allergyFactory.CreateAllergy ( patient.Object, allergyStatus.Object, null );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateAllergy_NullAllergyStatus_ThrowsArgumentException ()
        {
            var allergyRepository = new Mock<IAllergyRepository> ();
            var allergyFactory = new AllergyFactory (
                allergyRepository.Object );

            var patient = new Mock<Patient> ();
            var allergen = new Mock<CodedConcept> ();
            allergyFactory.CreateAllergy ( patient.Object, null, allergen.Object );
        }

        [TestMethod]
        public void DestroyAllergy_GivenAnAllergy_AllergyMadeTransient ()
        {
            var isTransient = false;

            var allergyRepository = new Mock<IAllergyRepository> ();
            allergyRepository
                .Setup ( a => a.MakeTransient ( It.IsAny<Allergy> () ) )
                .Callback ( () => isTransient = true );
            var allergyFactory = new AllergyFactory (
                allergyRepository.Object );

            var patient = new Mock<Patient> ();

            var allergy = new Mock<Allergy> ();
            allergy.Setup ( a => a.Patient ).Returns ( patient.Object );

            allergyFactory.DestroyAllergy ( allergy.Object );

            Assert.IsTrue ( isTransient );
        }

        private Allergy CreateAllergyByAllergyFactory ()
        {
            var allergyRepository = new Mock<IAllergyRepository> ();
            var allergyFactory = new AllergyFactory (
                allergyRepository.Object );

            var patient = new Mock<Patient> ();
            var allergyStatus = new Mock<AllergyStatus> ();
            var allergen = new CodedConceptBuilder().WithCodedConceptCode("TheCode");
            var allergy = allergyFactory.CreateAllergy ( patient.Object, allergyStatus.Object, allergen );

            return allergy;
        }
    }
}