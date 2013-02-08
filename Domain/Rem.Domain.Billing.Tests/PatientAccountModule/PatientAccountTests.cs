using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Domain.Primitives;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.Tests.PatientAccountModule
{
    [TestClass]
    public class PatientAccountTests
    {
        [TestMethod]
        public void RevisePhones_GivenAPhoneListAndPatientAccountHasNoPhone_PatientAccountHasTheSamePhoneList()
        {
            // Setup
            var fixture = new Fixture ().Customize ( new AutoMoqCustomization () );
            var address = new Address (
                fixture.CreateAnonymous<string> (),
                fixture.CreateAnonymous<string> (),
                fixture.CreateAnonymous<string> (),
                new Mock<CountyArea> ().Object,
                new Mock<StateProvince> ().Object,
                new Mock<Country> ().Object,
                new PostalCode ( "21046" ) );
            var patientAccount = new PatientAccount (
                new Mock<BillingOffice> ().Object,
                fixture.CreateAnonymous<long> (),
                fixture.CreateAnonymous<PersonName> (),
                fixture.CreateAnonymous<DateTime> (),
                address,
                new Mock<AdministrativeGender> ().Object );

            var inputPhoneList = new List<PatientAccountPhone> {new Mock<PatientAccountPhone>().Object};

            // Exercise
            patientAccount.RevisePhones ( inputPhoneList );

            // Verify
            Assert.AreEqual(inputPhoneList.Count, patientAccount.Phones.Count());
        }

        [TestMethod]
        public void RevisePhones_GivenNullPhoneList_PatientAccountHasNoPhone()
        {
            // Setup
            var fixture = new Fixture ().Customize ( new AutoMoqCustomization () );
            var address = new Address (
                fixture.CreateAnonymous<string> (),
                fixture.CreateAnonymous<string> (),
                fixture.CreateAnonymous<string> (),
                new Mock<CountyArea> ().Object,
                new Mock<StateProvince> ().Object,
                new Mock<Country> ().Object,
                new PostalCode ( "21046" ) );
            var patientAccount = new PatientAccount (
                new Mock<BillingOffice> ().Object,
                fixture.CreateAnonymous<long> (),
                fixture.CreateAnonymous<PersonName> (),
                fixture.CreateAnonymous<DateTime> (),
                address,
                new Mock<AdministrativeGender> ().Object );

            var initialPhoneList = new List<PatientAccountPhone> { new Mock<PatientAccountPhone>().Object };
            patientAccount.RevisePhones(initialPhoneList);

            // Exercise
            patientAccount.RevisePhones(null);

            // Verify
            Assert.AreEqual(0, patientAccount.Phones.Count());
        }

        [TestMethod]
        public void RevisePhones_GivenAPhoneListAndPatientAccountHasPhones_RevisesPhonesCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var address = new Address (
                fixture.CreateAnonymous<string> (),
                fixture.CreateAnonymous<string> (),
                fixture.CreateAnonymous<string> (),
                new Mock<CountyArea> ().Object,
                new Mock<StateProvince> ().Object,
                new Mock<Country> ().Object,
                new PostalCode ( "21046" ) );
            var patientAccount = new PatientAccount (
                new Mock<BillingOffice> ().Object,
                fixture.CreateAnonymous<long> (),
                fixture.CreateAnonymous<PersonName> (),
                fixture.CreateAnonymous<DateTime> (),
                address,
                new Mock<AdministrativeGender> ().Object );

            var patientAccountPhoneType1 = new Mock<PatientAccountPhoneType> ();
            patientAccountPhoneType1.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );

            var patientAccountPhoneType2 = new Mock<PatientAccountPhoneType> ();
            patientAccountPhoneType2.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );

            var patientAccountPhoneType3 = new Mock<PatientAccountPhoneType> ();
            patientAccountPhoneType3.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );

            var phone = fixture.CreateAnonymous<Phone> ();

            var patientAccountPhone1 = new PatientAccountPhone ( patientAccountPhoneType1.Object, phone );
            var sameAsPatientAccountPhone1 = new PatientAccountPhone(patientAccountPhoneType1.Object, phone);

            var patientAccountPhone2 = new PatientAccountPhone(patientAccountPhoneType2.Object, phone);

            var patientAccountPhone3 = new PatientAccountPhone(patientAccountPhoneType3.Object, phone);

            var initialPhoneList = new List<PatientAccountPhone> { patientAccountPhone1, patientAccountPhone2 };
            patientAccount.RevisePhones(initialPhoneList);

            var inputPhoneList = new List<PatientAccountPhone> { sameAsPatientAccountPhone1, patientAccountPhone3 };

            // Exercise
            patientAccount.RevisePhones(inputPhoneList);

            // Verify
            Assert.AreEqual(2, patientAccount.Phones.Count());
            Assert.IsTrue(patientAccount.Phones.Contains(patientAccountPhone1));
            Assert.IsFalse(patientAccount.Phones.Contains(patientAccountPhone2));
            Assert.IsTrue(patientAccount.Phones.Contains(patientAccountPhone3));
        }
    }
}
