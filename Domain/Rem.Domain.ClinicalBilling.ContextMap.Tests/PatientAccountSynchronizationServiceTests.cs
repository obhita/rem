using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Primitives;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Billing.BillingOfficeModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap.Tests
{
    [TestClass]
    public class PatientAccountSynchronizationServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SynchronizePatientAccount_GivenNullPatient_ThrowsArgumentException()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccountSynchronizationService = fixture.CreateAnonymous<PatientAccountSynchronizationService> ();

            // Exercise
            patientAccountSynchronizationService.SynchronizePatientAccount(null);

            // Verify
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SynchronizePatientAccount_GivenPatientWithoutExistingBillingOffice_ThrowsArgumentException()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var fixture = new Fixture ().Customize ( new AutoMoqCustomization () );

                var billingOfficeRepository = new Mock<IBillingOfficeRepository> ();
                billingOfficeRepository.Setup ( p => p.GetByAgencyKey ( It.IsAny<long> () ) ).Returns ( () => null );
                fixture.Register ( () => billingOfficeRepository.Object );

                var agencyProfile = fixture.CreateAnonymous<AgencyProfile> ();

                var agency = new Mock<Agency> ();
                agency.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
                agency.SetupGet ( p => p.AgencyProfile ).Returns ( agencyProfile );

                var patient = new Mock<Patient> ();
                patient.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
                patient.SetupGet ( p => p.Agency ).Returns ( agency.Object );

                var patientAccountSynchronizationService = fixture.CreateAnonymous<PatientAccountSynchronizationService> ();

                // Exercise
                patientAccountSynchronizationService.SynchronizePatientAccount ( patient.Object );

                // Verify
            }
        }

        [TestMethod]
        public void SynchronizePatientAccount_PatientAccountDoesNotExist_CreatesPatientAccountCorrectly()
        {
            // Setup
            var fixture = new Fixture ().Customize ( new AutoMoqCustomization () );

            var billingOffice = new Mock<BillingOffice> ();
            billingOffice.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> );
            var billingOfficeRepository = new Mock<IBillingOfficeRepository> ();
            billingOfficeRepository.Setup ( p => p.GetByAgencyKey ( It.IsAny<long> () ) ).Returns ( () => billingOffice.Object );
            fixture.Register ( () => billingOfficeRepository.Object );

            var patientAccountRepository = new Mock<IPatientAccountRepository> ();
            patientAccountRepository.Setup ( p => p.GetByMedicalRecordNumber ( It.IsAny<long> () ) ).Returns ( () => null );
            fixture.Register ( () => patientAccountRepository.Object );

            var patientAccount = new Mock<PatientAccount> ();
            patientAccount.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            var patientAccountFactory = new Mock<IPatientAccountFactory> ();
            patientAccountFactory.Setup (
                p => p.CreatePatientAccount(It.IsAny<BillingOffice>(), It.IsAny<long>(), It.IsAny<PersonName>(), It.IsAny<DateTime>(), It.IsAny<Address>(), It.IsAny<AdministrativeGender> ())).
                Returns ( patientAccount.Object );
            fixture.Register ( () => patientAccountFactory.Object );

            var agency = new Mock<Agency> ();
            agency.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );

            var patient = new Mock<Patient> ();
            patient.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            patient.SetupGet ( p => p.Agency ).Returns ( agency.Object );

            var patientName = fixture.CreateAnonymous<PersonName> ();
            patient.SetupGet ( p => p.Name ).Returns ( patientName );

            var patientProfile = new PatientProfile (
                new Mock<PatientGender> ().Object, fixture.CreateAnonymous<DateTime?> (), null, null, new EmailAddress("test@test.com") );
            patient.SetupGet(p => p.Profile).Returns(patientProfile);

            var patientAccountSynchronizationService = fixture.CreateAnonymous<PatientAccountSynchronizationService> ();

            // Exercise
            patientAccountSynchronizationService.SynchronizePatientAccount ( patient.Object );

            // Verify
            patientAccountFactory.Verify (
                p => p.CreatePatientAccount(billingOffice.Object, patient.Object.Key, patientName, It.IsAny<DateTime?>(), It.IsAny<Address> (), It.IsAny<AdministrativeGender> ()));

        }

        [TestMethod]
        public void SynchronizePatientAccount_PatientNameChanged_ReviseNameForPatientAccountCorrectly()
        {
            // Setup
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var patientAccount = new Mock<PatientAccount> ();
            patientAccount.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
            patientAccount.SetupGet ( p => p.Name ).Returns ( fixture.CreateAnonymous<PersonName> );
            var patientAccountRepository = new Mock<IPatientAccountRepository>();
            patientAccountRepository.Setup(p => p.GetByMedicalRecordNumber(It.IsAny<long>())).Returns(() => patientAccount.Object);
            fixture.Register(() => patientAccountRepository.Object);

            var agency = new Mock<Agency>();
            agency.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());

            var patient = new Mock<Patient>();
            patient.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
            patient.SetupGet(p => p.Agency).Returns(agency.Object);

            var patientName = fixture.CreateAnonymous<PersonName>();
            patient.SetupGet(p => p.Name).Returns(patientName);

            var patientAccountSynchronizationService = fixture.CreateAnonymous<PatientAccountSynchronizationService>();

            // Exercise
            patientAccountSynchronizationService.SynchronizePatientAccount(patient.Object);

            // Verify
            patientAccount.Verify(p => p.ReviseName(patientName));
        }

        [TestMethod]
        public void SynchronizePatientAccount_PatientHomeAddressChanged_ReviseHomeAddressForPatientAccountCorrectly()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var fixture = new Fixture ().Customize ( new AutoMoqCustomization () );

                var patientAccountHomeAddress = new Address(
                    fixture.CreateAnonymous<string>(),
                    fixture.CreateAnonymous<string>(),
                    fixture.CreateAnonymous<string>(),
                    fixture.CreateAnonymous<CountyArea>(),
                    fixture.CreateAnonymous<StateProvince>(),
                    fixture.CreateAnonymous<Country>(),
                    new PostalCode("20146"));
                var patientAccount = new Mock<PatientAccount> ();
                patientAccount.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
                patientAccount.SetupGet ( p => p.HomeAddress ).Returns ( patientAccountHomeAddress );
                var patientAccountRepository = new Mock<IPatientAccountRepository> ();
                patientAccountRepository.Setup ( p => p.GetByMedicalRecordNumber ( It.IsAny<long> () ) ).Returns ( () => patientAccount.Object );
                fixture.Register ( () => patientAccountRepository.Object );

                var agency = new Mock<Agency> ();
                agency.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );

                var patient = new Mock<Patient> ();
                patient.SetupGet ( p => p.Key ).Returns ( fixture.CreateAnonymous<long> () );
                patient.SetupGet ( p => p.Agency ).Returns ( agency.Object );

                var patientHomeAddress = new Address(
                    fixture.CreateAnonymous<string>(),
                    fixture.CreateAnonymous<string>(),
                    fixture.CreateAnonymous<string>(),
                    fixture.CreateAnonymous<CountyArea>(),
                    fixture.CreateAnonymous<StateProvince>(),
                    fixture.CreateAnonymous<Country>(),
                    new PostalCode("20146"));
                patient.SetupGet(p => p.HomeAddress).Returns(patientHomeAddress);

                var patientAccountSynchronizationService = fixture.CreateAnonymous<PatientAccountSynchronizationService> ();

                // Exercise
                patientAccountSynchronizationService.SynchronizePatientAccount ( patient.Object );

                // Verify
                patientAccount.Verify ( p => p.ReviseHomeAddress ( patientHomeAddress ) );
            }
        }

        [TestMethod]
        public void SynchronizePatientAccount_GivenPatient_RevisePhoneNumbersForPatientAccountCorrectly()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var fixture = new Fixture().Customize(new AutoMoqCustomization());

                var patientAccountPhoneNumbers = fixture.CreateMany<PatientAccountPhone> ().ToList();

                var patientAccountPhoneTranslator = new Mock<IPatientAccountPhoneTranslator> ();
                patientAccountPhoneTranslator.Setup(p => p.Translate(It.IsAny<IList<PatientPhone>>())).Returns(patientAccountPhoneNumbers);
                fixture.Register(() => patientAccountPhoneTranslator);

                var patientAccount = new Mock<PatientAccount>();
                patientAccount.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
                var patientAccountRepository = new Mock<IPatientAccountRepository>();
                patientAccountRepository.Setup(p => p.GetByMedicalRecordNumber(It.IsAny<long>())).Returns(() => patientAccount.Object);
                fixture.Register(() => patientAccountRepository.Object);

                var agency = new Mock<Agency>();
                agency.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());

                var patient = new Mock<Patient>();
                patient.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
                patient.SetupGet(p => p.Agency).Returns(agency.Object);

                var patientPhoneNumbers = fixture.CreateMany<PatientPhone> ();
                patient.SetupGet(p => p.PhoneNumbers).Returns(patientPhoneNumbers);

                var patientAccountSynchronizationService = fixture.CreateAnonymous<PatientAccountSynchronizationService>();

                // Exercise
                patientAccountSynchronizationService.SynchronizePatientAccount(patient.Object);

                // Verify
                patientAccount.Verify(p => p.RevisePhones(patientAccountPhoneNumbers));
            }
        }

        [TestMethod]
        public void SynchronizePatientAccount_GivenPatient_RevisePayorCoveragesForPatientAccountCorrectly()
        {
            using (var serviceLocatorFixture = new ServiceLocatorFixture())
            {
                // Setup
                var fixture = new Fixture().Customize(new AutoMoqCustomization());

                var billingPayorCoverages = new List<PayorCoverage> ();

                var payorCoverageTranslator = new Mock<IPayorCoverageTranslator>();
                payorCoverageTranslator.Setup(p => p.Translate(It.IsAny<IList<PayorCoverageCache>>())).Returns(billingPayorCoverages);
                fixture.Register(() => payorCoverageTranslator);

                var patientAccount = new Mock<PatientAccount>();
                patientAccount.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
                var patientAccountRepository = new Mock<IPatientAccountRepository>();
                patientAccountRepository.Setup(p => p.GetByMedicalRecordNumber(It.IsAny<long>())).Returns(() => patientAccount.Object);
                fixture.Register(() => patientAccountRepository.Object);

                var agency = new Mock<Agency>();
                agency.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());

                var patient = new Mock<Patient>();
                patient.SetupGet(p => p.Key).Returns(fixture.CreateAnonymous<long>());
                patient.SetupGet(p => p.Agency).Returns(agency.Object);

                var patientPhoneNumbers = fixture.CreateMany<PatientPhone>();
                patient.SetupGet(p => p.PhoneNumbers).Returns(patientPhoneNumbers);

                var patientAccountSynchronizationService = fixture.CreateAnonymous<PatientAccountSynchronizationService>();

                // Exercise
                patientAccountSynchronizationService.SynchronizePatientAccount(patient.Object);

                // Verify
                patientAccount.Verify(p => p.RevisePayorCoverages(billingPayorCoverages));
            }
        }
    }
}
