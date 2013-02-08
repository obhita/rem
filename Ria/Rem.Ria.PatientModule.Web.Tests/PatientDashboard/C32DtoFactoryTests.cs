using System.Linq;
using C32Gen;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Clinical.LabModule;
using Rem.Infrastructure.Tests.Domain;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Ria.PatientModule.Web.Tests.PatientDashboard
{
    [TestClass]
    public class C32DtoFactoryTests : SafeHarborAgencyFixtureBase
    {
        [TestMethod]
        public void GetPatientForC32DtoGeneration_GivenPatientKey_Succeeds()
        {
            C32DtoFactory c32DtoFactory = new C32DtoFactory(SessionProvider);

            var patient = c32DtoFactory.GetPatientForC32DtoGeneration(AlbertSmithPatient.Key);

            Assert.AreEqual(1, patient.Addresses.Count());
            Assert.AreEqual(WellKnownNames.PatientModule.PatientAddressType.Home, patient.Addresses.First().PatientAddressType.WellKnownName);
            Assert.AreEqual(1, patient.PhoneNumbers.Count());
            Assert.AreEqual(1, patient.Allergies.Count());
            Assert.AreEqual(1, patient.Medications.Count());
            Assert.AreEqual(2, patient.ClinicalCases.Count());
            Assert.AreEqual(3, patient.ClinicalCases.First().Problems.Count());
            Assert.AreEqual(4, patient.ClinicalCases.First().Visits.Count());
            Assert.AreEqual(2, patient.ClinicalCases.First().Visits.First().Activities.Where(p => p.ActivityType.WellKnownName == WellKnownNames.VisitModule.ActivityType.LabSpecimen).Count());
            Assert.AreEqual(3, patient.ClinicalCases.First().Visits.First().Activities.Count());
            Assert.AreEqual(2, ((patient.ClinicalCases.First().Visits.First().Activities.Where(p => p.ActivityType.WellKnownName == WellKnownNames.VisitModule.ActivityType.LabSpecimen).First()) as LabSpecimen).LabTests.Count);
            Assert.AreEqual(2, ((patient.ClinicalCases.First().Visits.First().Activities.Where(p => p.ActivityType.WellKnownName == WellKnownNames.VisitModule.ActivityType.LabSpecimen).First()) as LabSpecimen).LabTests.First().LabResults.Count);
        }

        [TestMethod]
        public void CreateC32Dto_GivePatientKey_Succeeds()
        {
            C32DtoFactory c32DtoFactory = new C32DtoFactory(SessionProvider);
            var c32Dto = c32DtoFactory.CreateC32Dto(AlbertSmithPatient.Key);

            Assert.AreEqual("Maryland", c32Dto.Header.PersonalInfo.PatientInfo.PersonAddress.State);
            Assert.AreEqual("555-255-5454", c32Dto.Header.PersonalInfo.PatientInfo.PersonPhone.Value);
            Assert.AreEqual(3, c32Dto.Body.Conditions.Count());
            Assert.AreEqual(1, c32Dto.Body.Allergies.Count());
            Assert.AreEqual(1, c32Dto.Body.Medications.Count());
        }

        [TestMethod]
        public void BuildC32Xml_GivePatientKey_Succeeds()
        {
            C32DtoFactory c32DtoFactory = new C32DtoFactory(SessionProvider);
            
            IC32Builder c32Builder = new C32Builder(c32DtoFactory, new C32DtoSerializer(), new GreenCdaToC32Transformer(), new C32Validator(), new C32ToGreenCdaTransformer ());

            string c32Xml = c32Builder.BuildC32Xml(AlbertSmithPatient.Key, false);

            System.Diagnostics.Debug.WriteLine(c32Xml);
        }
    }
}
