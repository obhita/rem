using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using Pillar.Security.AccessControl;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Infrastructure.Tests.Domain
{
    [TestClass]
    public class LoadedLookupsFixtureBase : NHibernateFixtureBase
    {
        public IAccessControlManager AccessControlManager { get; private set; }

        // Agency Types
        public AgencyType TreatmentProviderAgencyType { get; private set; }
        public AgencyType CriminalJusticeAgencyType { get; private set; }
        public AgencyType SingleStateAgencyAgencyType { get; private set; }
        public AgencyType RecoverySupportServicesProviderAgencyType { get; private set; }
        public AgencyType SystemTestingAgencyType { get; private set; }
        public AgencyType StateAgencyAgencyType { get; private set; }

        // Agency Address Types
        public AgencyAddressType AdministrationAgencyAddressType { get; private set; }

        // Agency Address Phone Type
        public AgencyPhoneType TollFreeAgencyPhoneType { get; private set; }
        public AgencyPhoneType FaxAgencyPhoneType { get; private set; }
        public AgencyPhoneType MainAgencPhoneType { get; private set; }
        public AgencyPhoneType EmergencyAgencyPhoneType { get; private set; }

        // Agency Contact Types
        public AgencyContactType BillingAgencyContactType { get; private set; }
        public AgencyContactType EdiAgencyContactType { get; private set; }
        public AgencyContactType FiscalAdminAgencyContactType { get; private set; }
        public AgencyContactType ExecutiveContactType { get; private set; }
        public AgencyContactType CeoAgencyContactType { get; private set; }

        // Agency Identifier Types
        public AgencyIdentifierType NpiAgencyIdentifierType { get; private set; }

        // Countries
        public Country UnitedStatesCountry { get; private set; }

        // State Provinces
        public StateProvince MarylandStateProvince { get; private set; }

        // Gender
        public PatientGender MaleGender { get; private set; }
        public PatientGender FemaleGender { get; private set; }

        // PatientAddressType
        public PatientAddressType HomePatientAddressType { get; private set; }

        // PatientPhoneType
        public PatientPhoneType HomePatientPhoneType { get; private set; }

        // Race
        public Race WhiteRace { get; private set; }

        // PatientAliasType
        public PatientAliasType NicknamePatientAliasType { get; private set; }

        // Disability
        public Disability DeafDisability { get; private set; }

        // AllergyStatus
        public AllergyStatus ActiveAllergyStatus { get; private set; }

        // CodedConcept
        public CodedConcept AllergenCodedConcept { get; private set; }
        public CodedConcept MedicationCodeCodedConcept { get; private set; }
        public CodedConcept ProblemCodeCodedConcept { get; private set; }
        public CodedConcept LabTestResultNameCodedConcept { get; private set; }
        public CodedConcept MmrVaccineCodedConcept { get; private set; }

        // VisitStatus
        public VisitStatus ScheduledVisitStatus { get; private set; }
        public VisitStatus CheckedInVisitStatus { get; private set; }

        // ProblemStatus
        public ProblemStatus ActiveProblemStatus { get; private set; }

        // ActivityType
        public ActivityType VitalSignActivityType { get; private set; }
        public ActivityType LabSpecimentActivityType { get; private set; }
        public ActivityType ImmunizationActivityType { get; private set; }

        // LabSpecimenType
        public LabSpecimenType DefaultLabSpecimenType { get; private set; }

        //LabTestName
        public LabTestName LabTestNameCodedConcept { get; private set; }

        protected override void OnSetup ()
        {
            base.OnSetup ();

            var accessControlManagerMock = new Mock<IAccessControlManager> ();
            accessControlManagerMock.Setup ( p => p.CanAccess ( It.IsAny<ResourceRequest> () ) ).Returns ( true );
            AccessControlManager = accessControlManagerMock.Object;

            using ( ITransaction trans = Session.BeginTransaction () )
            {
                // Agency Types
                TreatmentProviderAgencyType = BuildLookup<AgencyType> ( "Treatment Provider" );
                CriminalJusticeAgencyType = BuildLookup<AgencyType> ( "Criminal Justice" );
                SingleStateAgencyAgencyType = BuildLookup<AgencyType> ( "Single State Agency" );
                RecoverySupportServicesProviderAgencyType = BuildLookup<AgencyType> ( "Recovery Support Services Provider" );
                SystemTestingAgencyType = BuildLookup<AgencyType> ( "System Testing Agency" );
                StateAgencyAgencyType = BuildLookup<AgencyType> ( "State Agency" );

                // Agency Address Types
                AdministrationAgencyAddressType = BuildLookup<AgencyAddressType> ( "Administration" );

                // Agency Address Phone Types
                TollFreeAgencyPhoneType = BuildLookup<AgencyPhoneType> ( "Toll Free" );
                FaxAgencyPhoneType = BuildLookup<AgencyPhoneType> ( "Fax" );
                MainAgencPhoneType = BuildLookup<AgencyPhoneType> ( "Main" );
                EmergencyAgencyPhoneType = BuildLookup<AgencyPhoneType> ( "Emergency" );

                // Agency Contact Types
                BillingAgencyContactType = BuildLookup<AgencyContactType> ( "Billing" );
                EdiAgencyContactType = BuildLookup<AgencyContactType> ( "EDI" );
                FiscalAdminAgencyContactType = BuildLookup<AgencyContactType> ( "Fiscal/Contract Administration" );
                ExecutiveContactType = BuildLookup<AgencyContactType> ( "Executive" );
                CeoAgencyContactType = BuildLookup<AgencyContactType> ( "CEO" );

                // Agency Identifier Types
                NpiAgencyIdentifierType = BuildLookup<AgencyIdentifierType> ( "NPI" );

                // Countries
                UnitedStatesCountry = BuildLookup<Country> ( "United States" );

                // State Provinces
                MarylandStateProvince = new StateProvince ( UnitedStatesCountry ) { Name = "Maryland" };
                Session.SaveOrUpdate ( MarylandStateProvince );

                // Gender
                var maleAdministrativeGender = BuildCodedConceptLookup<AdministrativeGender> ( "M", "Male", "M" );
                var femaleAdministrativeGender = BuildCodedConceptLookup<AdministrativeGender> ( "F", "Female", "F");
                MaleGender = new PatientGender ( maleAdministrativeGender ) { Name = "Female Becoming Male" };
                FemaleGender = new PatientGender ( femaleAdministrativeGender ) { Name = "Male Becoming Female" };
                Session.SaveOrUpdate ( MaleGender );
                Session.SaveOrUpdate ( FemaleGender );

                // PatientAddressType
                HomePatientAddressType = BuildLookup<PatientAddressType> ( "Home", WellKnownNames.PatientModule.PatientAddressType.Home );

                // PatientPhoneType
                HomePatientPhoneType = BuildLookup<PatientPhoneType> ( "Home", WellKnownNames.PatientModule.PatientPhoneType.Home );

                // Race
                WhiteRace = BuildLookup<Race> ( "White" );

                // PatientAliasType
                NicknamePatientAliasType = BuildLookup<PatientAliasType> ( "Nickname" );

                // Disability
                DeafDisability = BuildLookup<Disability> ( "Deaf" );

                // Allergy Status
                ActiveAllergyStatus = BuildCodedConceptLookup<AllergyStatus> ( "55561003", "Active" );

                // PatientAccessEventTypes
                BuildLookup<PatientAccessEventType> ( "Insert", "Insert" );
                BuildLookup<PatientAccessEventType> ( "Update", "Update" );
                BuildLookup<PatientAccessEventType> ( "Delete", "Delete" );
                BuildLookup<PatientAccessEventType> ( "Read", "Read" );

                // CodedConcepts
                AllergenCodedConcept = BuildCodedConcept ( "Allergen" );
                MedicationCodeCodedConcept = BuildCodedConcept ( "Med Code" );
                ProblemCodeCodedConcept = BuildCodedConcept ( "P Code" );
                LabTestResultNameCodedConcept = BuildCodedConcept ( "LTRN Code" );
                MmrVaccineCodedConcept = BuildCodedConcept("03", "measles, mumps and rubella virus vaccine");

                // VisitStatus
                ScheduledVisitStatus = BuildLookup<VisitStatus> ( "Scheduled", WellKnownNames.VisitModule.VisitStatus.Scheduled );
                CheckedInVisitStatus = BuildLookup<VisitStatus> ( "CheckedIn", WellKnownNames.VisitModule.VisitStatus.CheckedIn );

                // ProblemStatus
                ActiveProblemStatus = BuildCodedConceptLookup<ProblemStatus>("Active", "ACTIVE", "Active");

                // ActivityType
                VitalSignActivityType = BuildLookup<ActivityType> ( "VitalSign", WellKnownNames.VisitModule.ActivityType.VitalSign );
                LabSpecimentActivityType = BuildLookup<ActivityType> ( "Lab Speciment", WellKnownNames.VisitModule.ActivityType.LabSpecimen );
                ImmunizationActivityType = BuildLookup<ActivityType>("Immunization", WellKnownNames.VisitModule.ActivityType.Immunization);

                //LabTestName
                LabTestNameCodedConcept = BuildCodedConceptLookup<LabTestName> ( "55561003", "LTN Code" );

                trans.Commit ();
            }

            Session.Clear ();
        }

        [TestMethod]
        public void LoadLookups_Succeeds ()
        {
            // Agency Types
            Assert.IsTrue ( TreatmentProviderAgencyType.Key > 0 );
            Assert.IsTrue ( CriminalJusticeAgencyType.Key > 0 );
            Assert.IsTrue ( SingleStateAgencyAgencyType.Key > 0 );
            Assert.IsTrue ( RecoverySupportServicesProviderAgencyType.Key > 0 );
            Assert.IsTrue ( SystemTestingAgencyType.Key > 0 );
            Assert.IsTrue ( StateAgencyAgencyType.Key > 0 );

            // Agency Address Types
            Assert.IsTrue ( AdministrationAgencyAddressType.Key > 0 );

            // Agency Address Phone Types
            Assert.IsTrue ( TollFreeAgencyPhoneType.Key > 0 );
            Assert.IsTrue ( FaxAgencyPhoneType.Key > 0 );
            Assert.IsTrue ( MainAgencPhoneType.Key > 0 );
            Assert.IsTrue ( EmergencyAgencyPhoneType.Key > 0 );

            // Agency Contact Types
            Assert.IsTrue ( BillingAgencyContactType.Key > 0 );
            Assert.IsTrue ( EdiAgencyContactType.Key > 0 );
            Assert.IsTrue ( FiscalAdminAgencyContactType.Key > 0 );
            Assert.IsTrue ( ExecutiveContactType.Key > 0 );
            Assert.IsTrue ( CeoAgencyContactType.Key > 0 );

            // Agency Identifier Types
            Assert.IsTrue ( NpiAgencyIdentifierType.Key > 0 );

            // Countries
            Assert.IsTrue ( UnitedStatesCountry.Key > 0 );

            // State Provinces
            Assert.IsTrue ( MarylandStateProvince.Key > 0 );

            // Gender
            Assert.IsTrue ( MaleGender.Key > 0 );
            Assert.IsTrue ( FemaleGender.Key > 0 );

            // PatientAddressType
            Assert.IsTrue ( HomePatientAddressType.Key > 0 );

            // PatientPhoneType
            Assert.IsTrue ( HomePatientPhoneType.Key > 0 );

            // Race
            Assert.IsTrue ( WhiteRace.Key > 0 );

            // PatientAliasType
            Assert.IsTrue ( NicknamePatientAliasType.Key > 0 );

            // Disability
            Assert.IsTrue ( DeafDisability.Key > 0 );

            // Allergy Status
            Assert.IsTrue ( ActiveAllergyStatus.Key > 0 );

            // PatientAccessEventTypes

            // CodedConcepts
            Assert.IsTrue ( AllergenCodedConcept != null );
            Assert.IsTrue ( MedicationCodeCodedConcept != null );
            Assert.IsTrue(ProblemCodeCodedConcept != null);
            Assert.IsTrue(LabTestNameCodedConcept != null);
            Assert.IsTrue(LabTestResultNameCodedConcept != null);

            // VisitStatus
            Assert.IsTrue ( ScheduledVisitStatus.Key > 0 );
            Assert.IsTrue ( CheckedInVisitStatus.Key > 0 );

            // ActivityType
            Assert.IsTrue ( VitalSignActivityType.Key > 0 );
            Assert.IsTrue ( LabSpecimentActivityType.Key > 0 );
            Assert.IsTrue ( ImmunizationActivityType.Key > 0 );
        }

        protected TLookup BuildLookup<TLookup> ( string name, string wellKnownName = null ) where TLookup : LookupBase
        {
            var lookup = Activator.CreateInstance<TLookup> ();
            lookup.Name = name;
            lookup.WellKnownName = wellKnownName;
            Session.SaveOrUpdate ( lookup );
            return lookup;
        }

        private TLookup BuildCodedConceptLookup<TLookup> ( string codedConceptCode, string name, string wellKnownName = null )
            where TLookup : CodedConceptLookupBase
        {
            var codedConceptLookup = Activator.CreateInstance<TLookup> ();

            codedConceptLookup.CodedConceptCode = codedConceptCode;
            codedConceptLookup.Name = name;
            codedConceptLookup.WellKnownName = wellKnownName;

            Session.SaveOrUpdate ( codedConceptLookup );

            return codedConceptLookup;
        }

        private CodedConcept BuildCodedConcept ( string codedConceptCode, string displayName = null)
        {
            var codedConcept = new CodedConceptBuilder ()
                    .WithCodedConceptCode ( codedConceptCode )
                    .WithDisplayName (displayName);

            return codedConcept;
        }
    }
}