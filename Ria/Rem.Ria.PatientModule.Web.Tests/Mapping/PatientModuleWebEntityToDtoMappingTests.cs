using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Ria.Infrastructure.Web.Tests.Mapping;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientEditor;
using Rem.Ria.PatientModule.Web.PatientReminder;
using Rem.Ria.PatientModule.Web.GainShortScreener;
using StructureMap;

namespace Rem.Ria.PatientModule.Web.Tests.Mapping
{
    [TestClass]
    public class PatientModuleWebEntityToDtoMappingTests : EntityToDtoMappingTestFixtureBase
    {
        [TestMethod]
        public void Allergy_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Allergy, AllergyDto>());
        }

        [TestMethod]
        public void Patient_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Patient, PatientDto>());
        }

        [TestMethod]
        public void Patient_PhoneNumbers_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Patient, PatientPhoneNumbersDto>());
        }

        [TestMethod]
        public void Patient_DemographicDetails_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Patient, PatientDemographicDetailsDto>());
        }

        [TestMethod]
        public void PatientPhone_PatientPhoneDto_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<PatientPhone, PatientPhoneDto>());
        }

        [TestMethod]
        public void VitalSign_EntityToActivityDtoMapping_Exits()
        {
            Assert.IsTrue(HasEntityToDtoMapping<VitalSign, VitalSignDto>());
        }

        [TestMethod]
        public void Medication_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue ( HasEntityToDtoMapping<Medication, MedicationDto> () );
        }

        [TestMethod]
        public void Problem_EntityToDotMapping_Exists()
        {
            Assert.IsTrue ( HasEntityToDtoMapping<Problem, ProblemDto> () );
        }

        [TestMethod]
        public void Visit_EntityToDotMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Visit, VisitDto>());
        }

        [TestMethod]
        public void Patient_EntityToSearchResultDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Patient, PatientSearchResultDto>());
        }

        [TestMethod]
        public void PatientDocument_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<PatientDocument, PatientDocumentDto>());
        }

        [TestMethod]
        public void PatientAccessEvent_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<PatientAccessEvent, PatientAccessEventDto>());
        }

        [TestMethod]
        public void PatientReminder_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Patient, PatientReminderResultDto>());
        }

        [TestMethod]
        public void GainShortScreener_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<Domain.Clinical.GainShortScreenerModule.GainShortScreener, GainShortScreenerDto>());
        }

        [TestMethod]
        public void AllMapping_EntityToDto_Succeeds()
        {
            AssertAutoMapperConfigurationIsValid();
        }

        protected override void OnSetup()
        {
            Mapper.Reset();

            var appContainer = new Container();
            appContainer.Configure(x => x.Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("Rem."));
                scanner.LookForRegistries();

                //Register all ObjectMappers
                scanner.AddAllTypesOf<IObjectMapper>();
            }));
            var originalMapperFunction = MapperRegistry.AllMappers;
            MapperRegistry.AllMappers = () =>
            {
                var mappers = (originalMapperFunction.Invoke() as IObjectMapper[]).ToList();
                var objectMappers = appContainer.GetAllInstances<IObjectMapper>();
                mappers.AddRange(objectMappers);
                return mappers.ToArray();
            };
            new Infrastructure.Web.Mapping.AutoMapperConfig().Execute();
            new AgencyModule.Web.AutoMapperConfig ().Execute ();

            new AutoMapperConfig().Execute();

            base.OnSetup();
        }
    }
}
