using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Domain.ClinicalBilling.ContextMap.Tests
{
    [TestClass]
    public class PatientAccountPhoneTranslatorTests
    {
        [TestMethod]
        public void Translate_PatientPhoneIsNull_ReturnsNullPatientAccountPhone()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var sut = fixture.CreateAnonymous<PatientAccountPhoneTranslator>();

            PatientPhone patientPhone = null;

            // Exercise
            var patientAccountPhone = sut.Translate(patientPhone);

            // Verify
            Assert.IsNull(patientAccountPhone);
        }

        [TestMethod]
        public void Translate_GivenPatientPhone_ReturnsPatientAccountPhoneCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientPhoneType = new Mock<PatientPhoneType> ();
            var patientPhone = new Mock<PatientPhone> ();
            patientPhone.SetupGet ( p => p.PatientPhoneType ).Returns ( patientPhoneType.Object );
            var phoneNumber = fixture.CreateAnonymous<string> ();
            patientPhone.SetupGet( p => p.PhoneNumber).Returns ( phoneNumber  );

            var sut = fixture.CreateAnonymous<PatientAccountPhoneTranslator>();

            // Exercise
            var patientAccountPhone = sut.Translate(patientPhone.Object);

            // Verify
            Assert.AreEqual(phoneNumber, patientAccountPhone.Phone.PhoneNumber);
        }

        [TestMethod]
        public void Translate_PatientPhoneListIsNull_ReturnsNullPatientAccountPhoneList()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var sut = fixture.CreateAnonymous<PatientAccountPhoneTranslator>();

            IList<PatientPhone> patientPhoneList = null;

            // Exercise
            var patientAccountPhoneList = sut.Translate(patientPhoneList);

            // Verify
            Assert.IsNull(patientAccountPhoneList);
        }
    }
}
