using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.Tests.ClinicalCaseModule
{
    [TestClass]
    public class ClinicalCaseFactoryTests
    {
        [TestMethod]
        public void CreateClinicalCase_GivenValidArguments_Succeeds ()
        {
            var clinicalCaseRepositoryMock = new Mock<IClinicalCaseRepository> ();
            var clinicalCaseFactory = new ClinicalCaseFactory (
                clinicalCaseRepositoryMock.Object );

            var patient = new Mock<Patient> ();
            var location = new Mock<Location> ();

            ClinicalCase clinicalCase = clinicalCaseFactory.CreateClinicalCase (
                patient.Object,
                new ClinicalCaseProfileBuilder().WithInitialLocation(location.Object) );

            Assert.IsNotNull ( clinicalCase );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateClinicalCase_NullPatient_ThrowsException ()
        {
            var clinicalCaseRepositoryMock = new Mock<IClinicalCaseRepository> ();
            var clinicalCaseFactory = new ClinicalCaseFactory (
                clinicalCaseRepositoryMock.Object );
            var location = new Mock<Location> ();

            clinicalCaseFactory.CreateClinicalCase (
                null,
                new ClinicalCaseProfileBuilder().WithInitialLocation(location.Object));
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateClinicalCase_NullClinicalCaseProfile_ThrowsException ()
        {
            var clinicalCaseRepositoryMock = new Mock<IClinicalCaseRepository> ();
            var clinicalCaseFactory = new ClinicalCaseFactory (
                clinicalCaseRepositoryMock.Object );
            var patient = new Mock<Patient> ();

            clinicalCaseFactory.CreateClinicalCase (
                patient.Object,
                null );
        }

        [TestMethod]
        public void DestroyMedication_GivenAMedication_ClinicalCaseMadeTransient ()
        {
            bool isTransient = false;

            var clinicalCaseRepositoryMock = new Mock<IClinicalCaseRepository> ();
            clinicalCaseRepositoryMock
                .Setup ( c => c.MakeTransient ( It.IsAny<ClinicalCase> () ) )
                .Callback ( () => isTransient = true );
            var clinicalCaseFactory = new ClinicalCaseFactory (
                clinicalCaseRepositoryMock.Object );

            var clinicalCase = new Mock<ClinicalCase> ();
            var patient = new Mock<Patient> ();

            clinicalCase.Setup ( c => c.Patient ).Returns ( patient.Object );

            clinicalCaseFactory.DestroyClinicalCase ( clinicalCase.Object );

            Assert.IsTrue ( isTransient );
        }
    }
}