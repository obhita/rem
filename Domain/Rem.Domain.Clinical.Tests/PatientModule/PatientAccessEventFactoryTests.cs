using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.Clinical.Tests.PatientModule
{
    [TestClass]
    public class PatientAccessEventFactoryTests
    {
        [TestMethod]
        public void CreatePatientAccessEvent_GivenValidArguments_CreatesAnEvent ()
        {
            var patientAccessEventFactory = new PatientAccessEventFactory ();

            var patient = new Mock<Patient> ();
            var patientAccessEventType = new Mock<PatientAccessEventType> ();

            PatientAccessEvent patientAccessEvent = patientAccessEventFactory.CreatePatientAccessEvent (
                patient.Object, patientAccessEventType.Object, "audited context decription", "some note" );

            Assert.IsNotNull ( patientAccessEvent );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientAccessEvent_NullPatient_ThrowsArgumentException ()
        {
            var patientAccessEventFactory = new PatientAccessEventFactory ();

            var patientAccessEventType = new Mock<PatientAccessEventType> ();

            patientAccessEventFactory.CreatePatientAccessEvent (
                null, patientAccessEventType.Object, "audited context decription", "some note");
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientAccessEvent_NullPatientAccessEventType_ThrowsArgumentException ()
        {
            var patientAccessEventFactory = new PatientAccessEventFactory ();

            var patient = new Mock<Patient> ();

            patientAccessEventFactory.CreatePatientAccessEvent (
                patient.Object, null, "audited context decription", "some note");
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreatePatientAccessEvent_NullNote_ThrowsArgumentException ()
        {
            var patientAccessEventFactory = new PatientAccessEventFactory ();

            var patient = new Mock<Patient> ();
            var patientAccessEventType = new Mock<PatientAccessEventType> ();

            patientAccessEventFactory.CreatePatientAccessEvent (
                patient.Object, patientAccessEventType.Object, "audited context decription", null);
        }
    }
}