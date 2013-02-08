using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Cryptography;
using Pillar.Common.Tests;
using Pillar.FluentRuleEngine;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class PatientDocumentFactoryTests
    {
        [TestMethod]
        public void CreatePatientDocument_GivenValidArguments_Succeeds ()
        {
            var bytes = new byte[] { 0, 0, 0 };
            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            var hashingUtility = new Mock<IHashingUtility> ();
            hashingUtility.Setup ( m => m.ComputeHash ( bytes ) ).Returns ( "XXXXXXXXXXX" );
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object  );
            var patient = new Mock<Patient> ();
            var patientDocumentType = new Mock<PatientDocumentType> ();
           

            var patientDocument = patientDocumentFactory.CreatePatientDocument (
                patient.Object,
                patientDocumentType.Object,
                bytes,
                "filename" 
               );

            Assert.IsNotNull ( patientDocument );
        }

        [TestMethod]
        public void CreatePatientDocument_GivenValidArguments_EntityIsEditable ()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                SetupServiceLocatorFixture(serviceLocatorFixture);
                var bytes = new byte[] {0, 0, 0};
                var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository>();
                var hashingUtility = new Mock<IHashingUtility>();
                hashingUtility.Setup(m => m.ComputeHash(bytes)).Returns("XXXXXXXXXXX");

                var patientDocumentFactory = new PatientDocumentFactory(
                    patientDocumentRepositoryMock.Object, hashingUtility.Object);
                var patient = new Mock<Patient>();
                var patientDocumentType = new Mock<PatientDocumentType>();


                PatientDocument patientDocument = patientDocumentFactory.CreatePatientDocument(
                    patient.Object,
                    patientDocumentType.Object,
                    bytes,
                    "filename");

                patientDocument.ReviseOtherDocumentTypeName ( "Other"  );
            }
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientDocument_NullPatient_ThrowsException ()
        {
            var bytes = new byte[] { 0, 0, 0 };
            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(bytes)).Returns("XXXXXXXXXXX");
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patientDocumentType = new Mock<PatientDocumentType> ();

            patientDocumentFactory.CreatePatientDocument (
                null,
                patientDocumentType.Object,
                bytes,
                "filename");
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientDocument_NullPatientDocumentType_ThrowsException ()
        {
            var bytes = new byte[] { 0, 0, 0 };
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(It.IsAny<byte[]>())).Returns("XXXXXXXXXXX");

            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patient = new Mock<Patient> ();

            patientDocumentFactory.CreatePatientDocument (
                patient.Object,
                null,
                bytes,
                "filename");
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientDocument_NullBytes_ThrowsException ()
        {
            var bytes = new byte[] { 0, 0, 0 };
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(bytes)).Returns("XXXXXXXXXXX");
            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patient = new Mock<Patient> ();
            var patientDocumentType = new Mock<PatientDocumentType> ();

            patientDocumentFactory.CreatePatientDocument (
                patient.Object,
                patientDocumentType.Object,
                null,
                "filename");
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientDocument_NullFileName_ThrowsException ()
        {

            var bytes = new byte[] { 0, 0, 0 };
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(bytes)).Returns("XXXXXXXXXXX");

            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patient = new Mock<Patient> ();
            var patientDocumentType = new Mock<PatientDocumentType> ();

            patientDocumentFactory.CreatePatientDocument (
                patient.Object,
                patientDocumentType.Object,
                bytes,
                null);
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientDocument_BlankFileName_ThrowsException ()
        {
            var bytes = new byte[] { 0, 0, 0 };
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(bytes)).Returns("XXXXXXXXXXX");

            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patient = new Mock<Patient> ();
            var patientDocumentType = new Mock<PatientDocumentType> ();

            patientDocumentFactory.CreatePatientDocument (
                patient.Object,
                patientDocumentType.Object,
                bytes,
                "" 
                );
        }

        [TestMethod]
        public void CreatePatientDocument_GivenValidArguments_IsMadePersistent ()
        {
            bool isPersistent = false;
            var bytes = new byte[] { 0, 0, 0 };
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(bytes)).Returns("XXXXXXXXXXX");


            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            patientDocumentRepositoryMock
                .Setup ( p => p.MakePersistent ( It.IsAny<PatientDocument> () ) )
                .Callback ( () => isPersistent = true );
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patient = new Mock<Patient> ();
            var patientDocumentType = new Mock<PatientDocumentType> ();
            

            patientDocumentFactory.CreatePatientDocument (
                patient.Object,
                patientDocumentType.Object,
                bytes,
                "filename"
                );

            Assert.IsTrue ( isPersistent );
        }

        [TestMethod]
        public void DestroyPatientDocument_GivenPatientDocument_Succeeds ()
        {
            var bytes = new byte[] { 0, 0, 0 };
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(It.IsAny<byte[]>())).Returns("XXXXXXXXXXX");


            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patient = new Mock<Patient> ();

            var patientDocument = new Mock<PatientDocument> ();
            patientDocument.Setup ( p => p.Patient ).Returns ( patient.Object );

            patientDocumentFactory.DestroyPatientDocument ( patientDocument.Object );
        }

        [TestMethod]
        public void DestroyPatientDocument_GivenPatientDocument_MadeTransient ()
        {
            bool madeTransient = true;
            var hashingUtility = new Mock<IHashingUtility>();
            hashingUtility.Setup(m => m.ComputeHash(It.IsAny<byte[]>())).Returns("XXXXXXXXXXX");

            var patientDocumentRepositoryMock = new Mock<IPatientDocumentRepository> ();
            patientDocumentRepositoryMock
                .Setup ( p => p.MakeTransient ( It.IsAny<PatientDocument> () ) )
                .Callback ( () => madeTransient = true );
            var patientDocumentFactory = new PatientDocumentFactory (
                patientDocumentRepositoryMock.Object, hashingUtility.Object );
            var patient = new Mock<Patient> ();

            var patientDocument = new Mock<PatientDocument> ();
            patientDocument.Setup ( p => p.Patient ).Returns ( patient.Object );

            patientDocumentFactory.DestroyPatientDocument ( patientDocument.Object );

            Assert.IsTrue ( madeTransient );
        }

        private static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
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