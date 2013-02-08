using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;
using Pillar.Domain.Primitives;
using Pillar.Security.AccessControl;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Domain.Repository;
using Pillar.FluentRuleEngine;

namespace Rem.Infrastructure.Tests.Domain
{
    [TestClass]
    public class SafeHarborAgencyFixtureBase : LoadedLookupsFixtureBase
    {
        // Agency
        public Agency SafeHarborAgency { get; private set; }

        // Location
        public Location SafeHarborLocation { get; private set; }

        // Staff
        public Staff LeoSmithStaff { get; private set; }
        public Staff WendyJonesStaff { get; private set; }
        public Staff SheilaAndersonStaff { get; private set; }
        public Staff BethKnoxStaff { get; private set; }
        public Staff AnthonyBellStaff { get; private set; }

        // Patient
        public Patient AlbertSmithPatient { get; private set; }
        public Patient TaddYoungPatient { get; private set; }

        // Allergy
        public Allergy AlbertSmithPatientAllergyOne { get; private set; }

        // Medication
        public Medication AlbertSmithPatientMedicationOne { get; private set; }

        // ClinicalCase
        public ClinicalCase AlbertSmithPatientClinicalCaseOne { get; private set; }
        public ClinicalCase AlbertSmithPatientClinicalCaseTwo { get; private set; }
        public ClinicalCase TaddYoungPatientClinicalCaseOne { get; private set; }

        // Problem
        public Problem AlbertSmithPatientClinicalCaseOneProblemOne { get; private set; }
        public Problem AlbertSmithPatientClinicalCaseOneProblemTwo { get; private set; }
        public Problem AlbertSmithPatientClinicalCaseOneProblemThree { get; private set; }

        // Visit
        public Visit AlbertSmithPatientClinicalCaseOneVisitOne { get; private set; }
        public Visit AlbertSmithPatientClinicalCaseOneVisitTwo { get; private set; }
        public Visit AlbertSmithPatientClinicalCaseOneVisitThree { get; private set; }
        public Visit AlbertSmithPatientClinicalCaseOneVisitFour { get; private set; }
        public Visit TaddYoungVisitOne { get; private set; }

        // Vital Sign
        public VitalSign AlbertSmithPatientClinicalCaseOneVisitOneVitalSignOne { get; private set; }
        public VitalSign TaddYoungVitalSignOne { get; private set; }

        // LabSpecimen
        public LabSpecimen AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOne { get; private set; }
        public LabSpecimen AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenTwo { get; private set; }

        // LabTest
        public LabTest AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOne { get; private set; }
        public LabTest AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestTwo { get; private set; }

        // LabResult
        public LabResult AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOneLabResultOne { get; private set; }
        public LabResult AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOneLabResultTwo { get; private set; }

        // Immunization
        public Immunization AlbertSmithPatientClinicalCaseOneVisitFourImmunizationOne { get; private set; }
        public Immunization AlbertSmithPatientClinicalCaseOneVisitFourImmunizationTwo { get; private set; }

        protected override void OnSetup()
        {
            base.OnSetup();

            StructureMapContainer.Configure(
                c =>
                    {
                        c.For<IAccessControlManager> ().Use ( AccessControlManager );
                        c.For<IVisitStatusRepository> ().Use ( new VisitStatusRepository ( SessionProvider ) );
                        c.For<IRuleCollectionFactory>().Use<RuleCollectionFactory>();
                        c.Scan(x =>
                        {
                            // in the scan operation, include all needed dlls (Rem.*)
                            // be cautious in the future - this could still pick up unwanted assemblies,
                            // such as the stray test project that mistakenly ends up in the bin folder.
                            // so consider those possibilities if errors pop up, and you're led here.
                            x.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("Rem."));

                            x.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));

                            x.ConnectImplementationsToTypesClosing(typeof(IRuleCollectionCustomizer<,>));
                        });
                    } );

            using (ITransaction trans = Session.BeginTransaction())
            {
                SafeHarborAgency = BuildAgency(TreatmentProviderAgencyType, "Safe Harbor", "Safe Harbor");

                AgencyAddressAndPhone safeHarborWayAgencyAddressAndPhone =
                    SafeHarborAgency.AddAddressAndPhone(
                        new AgencyAddress(
                            AdministrationAgencyAddressType,
                            new AddressBuilder().WithFirstStreetAddress("123 Safe Harbor Way").WithCityName("Columbia").WithStateProvince(
                                MarylandStateProvince).WithPostalCode(new PostalCode("21046"))));

                safeHarborWayAgencyAddressAndPhone.AddPhone(
                    new AgencyPhoneBuilder().WithAgencyPhoneType(TollFreeAgencyPhoneType).WithPhone(
                        new PhoneBuilder().WithPhoneNumber(("555-555-5555"))));
                safeHarborWayAgencyAddressAndPhone.AddPhone(
                    new AgencyPhoneBuilder().WithAgencyPhoneType(FaxAgencyPhoneType).WithPhone(
                        new PhoneBuilder().WithPhoneNumber(("666-666-6666"))));

                SafeHarborAgency.AddIdentifier(
                    new AgencyIdentifierBuilder().WithAgencyIdentifierType(NpiAgencyIdentifierType).WithIdentifierNumber("154975646"));

                SafeHarborAgency.AddAgencyAlias(new AgencyAliasBuilder().WithName("Safe Harbor Ent").Build());

                LeoSmithStaff = BuildStaff(SafeHarborAgency, "Leo", "Smith");

                WendyJonesStaff = BuildStaff(SafeHarborAgency, "Wendy", "Jones");
                SheilaAndersonStaff = BuildStaff(SafeHarborAgency, "Sheila", "Anderson");
                BethKnoxStaff = BuildStaff(SafeHarborAgency, "Beth", "Knox");
                AnthonyBellStaff = BuildStaff(SafeHarborAgency, "Anthony", "Bell");

                SafeHarborAgency.AddContact(
                    new AgencyContactBuilder().WithAgencyContactType(CeoAgencyContactType).WithContactStaff(LeoSmithStaff).WithStatusIndicator(true));

                SafeHarborAgency.AddContact(
                    new AgencyContactBuilder().WithAgencyContactType(BillingAgencyContactType).WithContactStaff(BethKnoxStaff).WithStatusIndicator(
                        false));

                SafeHarborLocation = BuildLocation(SafeHarborAgency, "Safe Harbor Counseling Center");

                BuildAlbertSmithPatient();

                AlbertSmithPatientAllergyOne = BuildAllergy(AlbertSmithPatient, ActiveAllergyStatus, AllergenCodedConcept);

                AlbertSmithPatientMedicationOne = BuildMedication(AlbertSmithPatient, MedicationCodeCodedConcept);

                AlbertSmithPatientClinicalCaseOne = BuildClinicalCase(AlbertSmithPatient, SafeHarborLocation, 1);
                AlbertSmithPatientClinicalCaseTwo = BuildClinicalCase(AlbertSmithPatient, SafeHarborLocation, 2);

                AlbertSmithPatientClinicalCaseOneProblemOne = BuildProblem(AlbertSmithPatientClinicalCaseOne, ProblemCodeCodedConcept);
                AlbertSmithPatientClinicalCaseOneProblemTwo = BuildProblem(AlbertSmithPatientClinicalCaseOne, ProblemCodeCodedConcept);

                AlbertSmithPatientClinicalCaseOneProblemThree = BuildProblem(AlbertSmithPatientClinicalCaseOne, ProblemCodeCodedConcept);
                AlbertSmithPatientClinicalCaseOneProblemThree.UpdateProblemStatus(ActiveProblemStatus, DateTime.Now);

                AlbertSmithPatientClinicalCaseOneVisitOne = BuildScheduledVisit(
                    LeoSmithStaff,
                    new DateTimeRange(new DateTime(2002, 3, 28, 9, 0, 0), new DateTime(2002, 3, 28, 10, 0, 0)),
                    AlbertSmithPatientClinicalCaseOne);
                AlbertSmithPatientClinicalCaseOneVisitTwo = BuildScheduledVisit(
                    LeoSmithStaff,
                    new DateTimeRange(new DateTime(2010, 3, 28, 9, 0, 0), new DateTime(2010, 3, 28, 10, 0, 0)),
                    AlbertSmithPatientClinicalCaseOne);
                AlbertSmithPatientClinicalCaseOneVisitThree = BuildScheduledVisit(
                    LeoSmithStaff,
                    new DateTimeRange(new DateTime(2011, 3, 28, 9, 0, 0), new DateTime(2011, 3, 28, 10, 0, 0)),
                    AlbertSmithPatientClinicalCaseOne,
                    AlbertSmithPatientClinicalCaseOneProblemThree);
                AlbertSmithPatientClinicalCaseOneVisitFour = BuildCheckedInVisit(
                    LeoSmithStaff,
                    new DateTimeRange(new DateTime(2011, 3, 28, 9, 0, 0), new DateTime(2011, 3, 28, 10, 0, 0)),
                    AlbertSmithPatientClinicalCaseOne);

                AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOne = BuildLabSpecimen(
                    AlbertSmithPatientClinicalCaseOneVisitOne, LabSpecimentActivityType);
                AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenTwo = BuildLabSpecimen(
                    AlbertSmithPatientClinicalCaseOneVisitOne, LabSpecimentActivityType);

                AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOne =
                    BuildLabTest(AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOne, LabTestNameCodedConcept);
                AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestTwo =
                    BuildLabTest(AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOne, LabTestNameCodedConcept);

                AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOneLabResultOne =
                    BuildLabResult(AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOne, LabTestResultNameCodedConcept, 15.09);
                AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOneLabResultTwo =
                    BuildLabResult(AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOne, LabTestResultNameCodedConcept, 19.67);

                AlbertSmithPatientClinicalCaseOneVisitFourImmunizationOne = BuildImmunization(
                    AlbertSmithPatientClinicalCaseOneVisitFour, ImmunizationActivityType);

                AlbertSmithPatientClinicalCaseOneVisitFourImmunizationTwo = BuildImmunization(
                    AlbertSmithPatientClinicalCaseOneVisitFour, ImmunizationActivityType, MmrVaccineCodedConcept);

                AlbertSmithPatientClinicalCaseOneVisitOneVitalSignOne = BuildVitalSign(AlbertSmithPatientClinicalCaseOneVisitOne, 3, 0, 50);

                BuildTaddYoungPatient();
                TaddYoungPatientClinicalCaseOne = BuildClinicalCase(TaddYoungPatient, SafeHarborLocation, 1);

                TaddYoungVisitOne = BuildScheduledVisit(
                    LeoSmithStaff,
                    new DateTimeRange(new DateTime(2002, 3, 28, 9, 0, 0), new DateTime(2002, 3, 28, 10, 0, 0)),
                    TaddYoungPatientClinicalCaseOne);

                ChangeTaddYoungVisitOneToCheckedIn();

                TaddYoungVitalSignOne = BuildVitalSign(TaddYoungVisitOne, 3, 0, 50);

                trans.Commit();
            }

            Session.Clear();
        }

        private void ChangeTaddYoungVisitOneToCheckedIn ()
        {
            StructureMapContainer.Configure (
                s =>
                s.For<IVisitStatusRepository> ().Use ( new VisitStatusRepository ( SessionProvider ) ) );
            TaddYoungVisitOne.CheckIn ( new DateTime ( 2002, 3, 28, 9, 0, 0 ) );
            Session.SaveOrUpdate ( TaddYoungVisitOne );
        }


        [TestMethod]
        public void FixtureSetup_Succeeds ()
        {
            Assert.IsTrue ( SafeHarborAgency.Key > 0 );

            AgencyAddressAndPhone addressAndPhone = SafeHarborAgency.AddressesAndPhones.Single ();
            Assert.IsTrue ( addressAndPhone.Key > 0 );

            AgencyPhone tollFreeNumber = addressAndPhone.PhoneNumbers.Where ( a => a.AgencyPhoneType == TollFreeAgencyPhoneType ).Single ();
            Assert.IsTrue ( tollFreeNumber.Key > 0 );
            AgencyPhone faxNumber = addressAndPhone.PhoneNumbers.Where ( a => a.AgencyPhoneType == FaxAgencyPhoneType ).Single ();
            Assert.IsTrue ( faxNumber.Key > 0 );

            Assert.IsTrue ( LeoSmithStaff.Key > 0 );
            Assert.IsTrue ( WendyJonesStaff.Key > 0 );
            Assert.IsTrue ( SheilaAndersonStaff.Key > 0 );
            Assert.IsTrue ( BethKnoxStaff.Key > 0 );
            Assert.IsTrue ( AnthonyBellStaff.Key > 0 );

            AgencyIdentifier npiIdentifier = SafeHarborAgency.AgencyIdentifiers.Single ();
            Assert.IsTrue ( npiIdentifier.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatient.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOne.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientAllergyOne.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientMedicationOne.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOne.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseTwo.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneProblemOne.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneProblemTwo.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOne.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitTwo.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitThree.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitFour.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOne.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenTwo.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOne.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestTwo.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOneLabResultOne.Key > 0 );
            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOneLabSpecimenOneLabTestOneLabResultTwo.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitFourImmunizationOne.Key > 0 );

            Assert.IsTrue ( AlbertSmithPatientClinicalCaseOneVisitOneVitalSignOne.Key > 0 );
        }

        private Agency BuildAgency(AgencyType agencyType, string legalName, string displayName = null)
        {
            Agency agency = new AgencyBuilder().WithAgencyProfile(
                    new AgencyProfileBuilder().WithAgencyType(agencyType).WithAgencyName(
                        new AgencyNameBuilder().WithLegalName(legalName).WithDisplayName(displayName)));

            Session.SaveOrUpdate(agency);
            return agency;
        }

        private Staff BuildStaff(Agency agency, string firstName, string lastName)
        {
            var staff = new Staff(
                agency,
                new StaffProfileBuilder().WithStaffName(new PersonNameBuilder().WithFirst(firstName).WithLast(lastName)));

            Session.SaveOrUpdate(staff);
            return staff;
        }

        private Location BuildLocation(Agency agency, string name)
        {
            var location = new Location(agency, new LocationProfileBuilder().WithLocationName(new LocationNameBuilder().WithName(name)));
            Session.SaveOrUpdate(location);
            return location;
        }

        private void BuildAlbertSmithPatient ()
        {
            PersonName name = new PersonNameBuilder ()
                .WithFirst ( "Albert" )
                .WithLast ( "Smith" )
                .Build ();
            PatientProfile profile = new PatientProfileBuilder ()
                .WithBirthDate ( DateTime.Parse ( "5/10/1971" ) )
                .WithPatientGender ( MaleGender )
                .Build ();
            AlbertSmithPatient = new Patient ( SafeHarborAgency, name, profile );
            AlbertSmithPatient.UpdateUniqueIdentifier("albertsmith");

            var address = new AddressBuilder ()
                .WithFirstStreetAddress ( "14235 South St" )
                .WithCityName ( "Baltimore" )
                .WithPostalCode ( new PostalCode ( "21075" ) )
                .WithStateProvince ( MarylandStateProvince )
                .Build ();

            PatientAddress albertSmithAddress = new PatientAddressBuilder ()
                .WithPatientAddressType ( HomePatientAddressType )
                .WithAddress(address)
                .Build ();

            AlbertSmithPatient.AddAddress ( albertSmithAddress );

            AlbertSmithPatient.AddPhoneNumber ( new PatientPhone ( HomePatientPhoneType, "555-255-5454" ) );

            var patientRace = new PatientRace ( WhiteRace );
            AlbertSmithPatient.AddPatientRace ( patientRace );
            AlbertSmithPatient.SetPrimaryRace(WhiteRace); 

            AlbertSmithPatient.AddAlias ( new PatientAlias ( NicknamePatientAliasType, "Al-bear" ) );

            AlbertSmithPatient.AddPatientDisability ( new PatientDisability ( DeafDisability ) );

            Session.SaveOrUpdate ( AlbertSmithPatient );
        }

        private void BuildTaddYoungPatient ()
        {
            PersonName name = new PersonNameBuilder ()
                .WithFirst ( "Tadd" )
                .WithLast ( "Young" )
                .Build ();
            PatientProfile profile = new PatientProfileBuilder ()
                .WithBirthDate ( DateTime.Parse ( "5/10/2000" ) )
                .WithPatientGender ( MaleGender )
                .Build ();
            TaddYoungPatient = new Patient ( SafeHarborAgency, name, profile );
            TaddYoungPatient.UpdateUniqueIdentifier("taddyoung");

            TaddYoungPatient.AddPhoneNumber ( new PatientPhone ( HomePatientPhoneType, "555-255-5454" ) );

            var patientRace = new PatientRace ( WhiteRace );
            TaddYoungPatient.AddPatientRace ( patientRace );
            TaddYoungPatient.SetPrimaryRace ( WhiteRace );

            TaddYoungPatient.AddAlias ( new PatientAlias ( NicknamePatientAliasType, "Tadd" ) );

            TaddYoungPatient.AddPatientDisability ( new PatientDisability ( DeafDisability ) );

            Session.SaveOrUpdate ( TaddYoungPatient );
        }

        private Visit BuildScheduledVisit ( Staff staff, DateTimeRange appointmentDateTimeRange, ClinicalCase clinicalCase, Problem problem = null )
        {
            var visitTemplate = new VisitTemplate ( SafeHarborAgency, "Initial Behavioral Health - Adult", "99204" );
            Session.SaveOrUpdate ( visitTemplate );

            var visit = new Visit ( staff, appointmentDateTimeRange, clinicalCase, ScheduledVisitStatus, SafeHarborLocation, visitTemplate.Name, visitTemplate.CptCode );

            if ( problem != null )
            {
                visit.AddProblem ( problem );
            }

            Session.SaveOrUpdate ( visit );

            return visit;
        }

        private Visit BuildCheckedInVisit ( Staff staff, DateTimeRange appointmentDateTimeRange, ClinicalCase clinicalCase, Problem problem = null )
        {
            Visit visit = BuildScheduledVisit ( staff, appointmentDateTimeRange, clinicalCase, problem );

            visit.CheckIn ( new DateTime ( 2002, 3, 28, 9, 0, 0 ) );

            Session.SaveOrUpdate ( visit );

            return visit;
        }

        private VitalSign BuildVitalSign ( Visit visit, int? heightFeetMeasure, int? heightInchesMeasure, int? weightMeasure )
        {
            var vitalSign = new VitalSign ( visit, VitalSignActivityType );
            vitalSign.ReviseHeight ( new Height ( heightFeetMeasure, heightInchesMeasure ) );
            vitalSign.ReviseWeight ( weightMeasure );

            Session.SaveOrUpdate ( vitalSign );
            return vitalSign;
        }

        private Allergy BuildAllergy ( Patient patient, AllergyStatus allergyStatus, CodedConcept allergen )
        {
            var allergy = new Allergy ( patient, allergyStatus, allergen );
            Session.SaveOrUpdate ( allergy );
            return allergy;
        }

        private Medication BuildMedication ( Patient patient, CodedConcept medicationCode )
        {
            var medication = new Medication ( patient, medicationCode, medicationCode );
            Session.SaveOrUpdate ( medication );
            return medication;
        }

        private ClinicalCase BuildClinicalCase ( Patient patient, Location location, long clinicalCaseNumber )
        {
            var clinicalCase = new ClinicalCase ( patient, new ClinicalCaseProfileBuilder().WithInitialLocation(location), clinicalCaseNumber );
            Session.SaveOrUpdate ( clinicalCase );
            return clinicalCase;
        }

        private Problem BuildProblem ( ClinicalCase clinicalCase, CodedConcept problemCode )
        {
            var problem = new Problem ( clinicalCase, problemCode );
            Session.SaveOrUpdate ( problem );
            return problem;
        }

        private LabSpecimen BuildLabSpecimen(Visit visit,
                                              ActivityType activityType)
        {
            var labSpecimen = new LabSpecimen(visit, activityType);
            Session.SaveOrUpdate(labSpecimen);
            return labSpecimen;
        }

        private LabTest BuildLabTest ( LabSpecimen labSpecimen, LabTestName labTestNameCodedConcept )
        {
            var labTest = labSpecimen.AddLabTest(new LabTestInfoBuilder().WithLabTestName(labTestNameCodedConcept));
            
            Session.SaveOrUpdate ( labTest );
            return labTest;
        }

        private LabResult BuildLabResult ( LabTest labTest, CodedConcept labTestResultNameCodedConcept, double value )
        {
            var labResult = new LabResultBuilder ()
                .WithLabTestResultNameCodedConcept ( labTestResultNameCodedConcept )
                .WithValue ( value ).Build ();
            labTest.AddLabResult ( labResult );
            Session.SaveOrUpdate ( labResult );
            return labResult;
        }

        private Immunization BuildImmunization(Visit visit,
                                                ActivityType activityType)
        {
            var immunization = new Immunization(visit, activityType);
            Session.SaveOrUpdate(immunization);
            return immunization;
        }

        private Immunization BuildImmunization ( Visit visit,
                                                 ActivityType activityType, CodedConcept vaccineCodedConcept )
        {
            var immunization = new Immunization ( visit, activityType );
            immunization.ReviseImmunizationVaccineInfo(new ImmunizationVaccineInfo(vaccineCodedConcept, null, new ImmunizationVaccineManufacturer(null, null)));
            Session.SaveOrUpdate ( immunization );
            return immunization;
        }
    }
}