#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NHibernate;
using NHibernate.Criterion;
using NLog;
using Pillar.Common.Configuration;
using Pillar.Common.Utility;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Domain;
using Rem.Ria.NewCropModule.Web.NCScript;
using Rem.WellKnownNames.CommonModule;
using Rem.WellKnownNames.NewCropModule;
using AddressPhoneType = Rem.WellKnownNames.AgencyModule.AgencyPhoneType;
using AllergyStatus = Rem.WellKnownNames.PatientModule.AllergyStatus;
using AllergyType = Rem.WellKnownNames.PatientModule.AllergyType;
using LocationAddressType = Rem.WellKnownNames.AgencyModule.LocationAddressType;
using LocationPhoneType = Rem.WellKnownNames.AgencyModule.LocationPhoneType;
using MedicationStatus = Rem.WellKnownNames.PatientModule.MedicationStatus;
using StaffIdentifierType = Rem.WellKnownNames.AgencyModule.StaffIdentifierType;

namespace Rem.Ria.NewCropModule.Web
{
    /// <summary>
    /// Class for building NCS cript.
    /// </summary>
    public class NcsCriptBuilder : INcsCriptBuilder
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();
        private readonly IConfigurationPropertiesProvider _configProvider;

        private readonly ISession _session;
        private Agency _agency;
        private Location _location;
        private string _newCropAccountId;
        private string _newCropAccountName;
        private string _newCropPartnerName;
        private string _newCropPassword;
        private string _newCropProductName;
        private string _newCropProductVersion;
        private string _newCropUserName;

        private Patient _patient;
        private Staff _staff;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NcsCriptBuilder"/> class.
        /// </summary>
        /// <param name="configProvider">The config provider.</param>
        /// <param name="sessionProvider">The session provider.</param>
        public NcsCriptBuilder ( IConfigurationPropertiesProvider configProvider, ISessionProvider sessionProvider )
        {
            _configProvider = configProvider;
            _session = sessionProvider.GetSession ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds to XML.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <param name="staffKey">The staff key.</param>
        /// <param name="agencyKey">The agency key.</param>
        /// <param name="locationKey">The location key.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public string BuildToXml ( long patientKey, long staffKey, long agencyKey, long locationKey )
        {
            LoadConfiguration (); //TODO: Refactor into Helper Class with Interface + Dto 

            LoadEntitiesFromDomain ( patientKey, staffKey, agencyKey, locationKey );

            Logger.Debug ( "Building NCScript Object" );

            var script = new NCScript.NCScript
                {
                    Credentials = BuildCredentialsType (),
                    UserRole = DetermineUserRole (),
                    Destination = DetermineDestinationPage (),
                    Account = BuildAccountType ( _agency ),
                    Location = BuildLocationType ( _location ),
                    LicensedPrescriber = BuildLicensedPrescriberType ( _staff ),
                    Patient = BuildPatientType ( _patient ),
                    OutsidePrescription = BuildOutsidePrescriptionType ( _patient ),
                };

            var serializer = new XmlSerializer ( typeof( NCScript.NCScript ) );
            var stream = new MemoryStream ();

            serializer.Serialize ( stream, script );
            var xml = Encoding.UTF8.GetString ( stream.ToArray () );

            Logger.Debug ( "NSCript Object successfully Generated. {0}", xml );

            return xml;
        }

        #endregion

        #region Methods

        private static PatientType BuildDummyPatient ()
        {
            return new PatientType
                {
                    //TODO: All this data must come from the Patient 
                    ID = "ALBERTSMITH",
                    PatientName = new PersonNameType { Last = "Albert", First = "Smith (Fake)" },
                    MedicalRecordNumber = "123456",
                    Memo = "This is just a demo",
                    PatientAddress =
                        new AddressOptionalType
                            {
                                Address1 = "11 Main St",
                                City = "Columbia",
                                Country = "US",
                                State = "MD",
                                Zip = "21113"
                            },
                    PatientCharacteristics =
                        new PatientCharacteristicsType { Dob = "19800410", Gender = GenderType.M, GenderSpecified = true },
                };
        }

        private static PatientAllergyFreeformType BuildPatientAllergyFreeformType ( Allergy drugAllergy )
        {
            var da = new PatientAllergyFreeformType
                {
                    AllergyName = NcScriptHelper.RemoveUnwantedPartsFromDrugName ( drugAllergy.AllergenCodedConcept.DisplayName ),
                    AllergySeverityTypeID =
                        NcScriptHelper.TransformRemAllergySeverityTypeToNewCropAllergySeverityType (
                            drugAllergy.AllergySeverityType ),
                };
            return da;
        }

        private static DestinationType DetermineDestinationPage ()
        {
            return new DestinationType { RequestedPage = RequestedPageType.Compose };
        }

        // TODO: Based on some business rule
        private static UserRoleType DetermineUserRole ()
        {
            return new UserRoleType { Role = RoleType.Doctor };
        }

        private static string ExtractStaffIdentifier ( Staff staff, string identifierType, bool isRequired = false )
        {
            var candidate = staff.Identifiers.FirstOrDefault ( c => c.StaffIdentifierType.WellKnownName == identifierType );

            if ( candidate != null )
            {
                return candidate.IdentifierNumber;
            }

            if ( isRequired )
            {
                throw new ApplicationException ( string.Format ( "Staff Does not contain an Identifier of type {0}", identifierType ) );
            }

            return string.Empty;
        }

        private static AddressOptionalType PatientAddress ( Patient patient )
        {
            var candidate = patient.Addresses.FirstOrDefault ();

            if ( candidate != null )
            {
                return new AddressOptionalType
                    {
                        Address1 = candidate.Address.FirstStreetAddress,
                        City = candidate.Address.CityName,
                        Country = ( candidate.Address.Country == null ) ? "US" : candidate.Address.Country.WellKnownName,
                        State = candidate.Address.StateProvince.ShortName,
                        Zip = candidate.Address.PostalCode.Code
                    };
            }

            return new AddressOptionalType ();
        }

        private static OutsidePrescriptionType TransformRemMedicationIntoOutsidePrescriptionAsFreeText ( Medication medication )
        {
            Check.IsNotNull (
                medication.UsageDateRange, "Medication Usage Range was provided for " + medication.MedicationCodeCodedConcept.DisplayName );

            Check.IsNotNull ( medication.UsageDateRange.StartDate, "Medication Usage Start Date cannot be null" );
            Debug.Assert ( medication.UsageDateRange.StartDate != null, "medication.UsageDateRange.StartDate != null" );

            var prescription = new OutsidePrescriptionType
                {
                    ExternalId = medication.Key.ToString (),
                    Date = medication.UsageDateRange.StartDate.Value.ToString ( "yyyMMdd" ),
                    DoctorName = medication.PrescribingPhysicianName,
                    DrugIdentifierTypeSpecified = false,
                    Drug = NcScriptHelper.RemoveUnwantedPartsFromDrugName ( medication.RootMedicationCodedConcept.DisplayName ),
                    //// DrugIdentifier = medication.MedicationCodeCodedConcept.CodedConceptCode,
                    PrescriptionType = "reconcile",
                    ////DispenseNumber = "0",
                    ////RefillCount = "0",
                    Sig = medication.FrequencyDescription,
                    PrescriptionStatus =
                        NcScriptHelper.TransformRemMedicationStatusIntoNewCropPrescriptionStatus (
                            medication.MedicationStatus.WellKnownName ),
                    PrescriptionArchiveStatus =
                        NcScriptHelper.TransformRemMedicationIntoNewCropPrescriptionArchiveStatus (
                            medication.MedicationStatus.WellKnownName ),
                };
            return prescription;
        }

        private AddressType BuildAccountAddress ( Agency agency )
        {
            Logger.Debug ( "BuildAccountAddress - Building Account Address" );
            var candidate = agency.AddressesAndPhones.FirstOrDefault ();

            if ( candidate != null )
            {
                return new AddressType
                    {
                        Address1 = candidate.AgencyAddress.Address.FirstStreetAddress,
                        Address2 = candidate.AgencyAddress.Address.SecondStreetAddress,
                        City = candidate.AgencyAddress.Address.CityName,
                        Country = ( candidate.AgencyAddress.Address.Country != null )
                                      ? candidate.AgencyAddress.Address.Country.WellKnownName
                                      : "US",
                        State =
                            candidate.AgencyAddress.Address.StateProvince != null
                                ? candidate.AgencyAddress.Address.StateProvince.ShortName
                                : "MD",
                        Zip = candidate.AgencyAddress.Address.PostalCode.Code.Substring ( 0, 5 )
                    };
            }

            throw new ApplicationException (
                string.Format ( "Agency  {0} Does not contain an Address", agency.AgencyProfile.AgencyName.DisplayName ) );
        }

        private string BuildAccountAddressPhone ( Agency agency, string identifierType )
        {
            Logger.Debug (
                "BuildAccountAddressPhone - Building Account Address Phone for agency {0}, Phone Type:{1}",
                agency.AgencyProfile.AgencyName.DisplayName,
                identifierType );

            var candidate = agency.AddressesAndPhones.FirstOrDefault ();

            if ( candidate != null )
            {
                var phoneNumber = candidate.PhoneNumbers
                    .FirstOrDefault ( phone => phone.AgencyPhoneType.WellKnownName == identifierType );

                if ( phoneNumber == null )
                {
                    throw new ApplicationException (
                        string.Format (
                            "Agency {0} must have a valid Phone Number of type {1}",
                            identifierType,
                            agency.AgencyProfile.AgencyName.DisplayName ) );
                }

                return phoneNumber.Phone.PhoneNumber;
            }

            throw new ApplicationException (
                string.Format (
                    "Agency {0} must have a valid Address",
                    agency.AgencyProfile.AgencyName.DisplayName ) );
        }

        private AccountType BuildAccountType ( Agency agency )
        {
            Logger.Debug ( "BuildAccountType - Building Account for Agency {0}", agency.AgencyProfile.AgencyName.DisplayName );

            return new AccountType
                {
                    AccountName = _newCropAccountName,
                    ID = _newCropAccountId,
                    SiteID = _agency.Key.ToString (),
                    AccountAddress = BuildAccountAddress ( agency ),
                    AccountPrimaryFaxNumber = BuildAccountAddressPhone ( agency, AddressPhoneType.Fax ),
                    AccountPrimaryPhoneNumber = BuildAccountAddressPhone ( agency, AddressPhoneType.Fax ),
                    //TODO: Get the right Phone Number from Agency
                };
        }

        private CredentialsType BuildCredentialsType ()
        {
            return new CredentialsType
                {
                    PartnerName = _newCropPartnerName,
                    Name = _newCropUserName,
                    Password = _newCropPassword,
                    ProductName = _newCropProductName,
                    ProductVersion = _newCropProductVersion
                };
        }

        private LicensedPrescriberType BuildLicensedPrescriberType ( Staff staff )
        {
            return new LicensedPrescriberType
                {
                    ID = staff.Key.ToString (),
                    LicensedPrescriberName = new PersonNameType
                        {
                            First = staff.StaffProfile.StaffName.First,
                            Last = staff.StaffProfile.StaffName.Last,
                            Middle = staff.StaffProfile.StaffName.Middle
                        },
                    Dea = ExtractStaffIdentifier ( staff, StaffIdentifierType.Dea, true ),
                    Npi = ExtractStaffIdentifier ( staff, StaffIdentifierType.Npi ),
                };
        }

        private AddressType BuildLocationAddress ( Location location )
        {
            Logger.Debug ( "BuildLocationAddress - Building Location Address for Location {0}", location.LocationProfile.LocationName.DisplayName );

            var candidate =
                location.LocationAddressesAndPhones.FirstOrDefault (
                    a => a.LocationAddress.LocationAddressType.WellKnownName == LocationAddressType.Physical );

            if ( candidate != null )
            {
                return new AddressType
                    {
                        Address1 = candidate.LocationAddress.Address.FirstStreetAddress,
                        Address2 = candidate.LocationAddress.Address.SecondStreetAddress,
                        City = candidate.LocationAddress.Address.CityName,
                        Country = candidate.LocationAddress.Address.Country.WellKnownName ?? "US",
                        State = candidate.LocationAddress.Address.StateProvince.WellKnownName,
                        Zip = candidate.LocationAddress.Address.PostalCode.Code.Substring ( 0, 5 )
                    };
            }

            throw new ApplicationException (
                string.Format (
                    "Location {0} Does not contain an Address  of type {1}",
                    location.LocationProfile.LocationName.Name,
                    LocationAddressType.Physical ) );
        }

        private string BuildLocationPhone ( Location location, string identifierType )
        {
            Logger.Debug (
                "BuildLocationPhone - Building Phone For Location {0}, Phone Type {1}",
                location.LocationProfile.LocationName.DisplayName,
                identifierType );

            var candidate =
                location.LocationAddressesAndPhones.FirstOrDefault (
                    a => a.LocationAddress.LocationAddressType.WellKnownName == LocationAddressType.Physical );

            if ( candidate != null )
            {
                var phoneNumber = candidate.PhoneNumbers
                    .FirstOrDefault ( phone => phone.LocationPhoneType.WellKnownName == identifierType );

                if ( phoneNumber == null )
                {
                    throw new ApplicationException (
                        string.Format (
                            "Location {0} must have a valid Phone Number of type {1}",
                            identifierType,
                            location.LocationProfile.LocationName.DisplayName ) );
                }

                return phoneNumber.Phone.PhoneNumber;
            }

            throw new ApplicationException (
                string.Format (
                    "Location {0} Does not contain an Address  of type {1}",
                    location.LocationProfile.LocationName.Name,
                    LocationAddressType.Physical ) );
        }

        private LocationType BuildLocationType ( Location location )
        {
            Logger.Debug ( "BuildLocationType - Building Location with Location {0}", location.LocationProfile.LocationName.DisplayName );

            return new LocationType
                {
                    ID = location.Key.ToString (),
                    LocationName = location.LocationProfile.LocationName.DisplayName,
                    LocationShortName = location.LocationProfile.LocationName.Name,
                    LocationAddress = BuildLocationAddress ( location ),
                    PharmacyContactNumber = BuildLocationPhone ( location, LocationPhoneType.Main ),
                    PrimaryFaxNumber = BuildLocationPhone ( location, LocationPhoneType.Fax ),
                    PrimaryPhoneNumber = BuildLocationPhone ( location, LocationPhoneType.Main )
                };
        }

        private List<OutsidePrescriptionType> BuildOutsidePrescriptionType ( Patient patient )
        {
            if ( patient != null && patient.Medications.Any () )
            {
                var outsidePrescriptions = new List<OutsidePrescriptionType> ();

                var medications = patient.Medications
                    .Where ( med => med.MedicationStatus.WellKnownName == MedicationStatus.Active )
                    .ToList ();

                Logger.Debug ( "Patient's Medication List contained {0} active Medications.", medications.Count ().ToString () );

                medications.ForEach (
                    medication =>
                        {
                            outsidePrescriptions.Add ( TransformRemMedicationIntoOutsidePrescriptionAsFreeText ( medication ) );
                            Logger.Debug (
                                "Sending {0} as Free Text OutsidePrescription to NewCrop.",
                                medication.MedicationCodeCodedConcept.DisplayName );
                            // TODO: Make the Sending Rx as Mapped Drug Configurable
                            //outsidePrescriptions.Add ( TransformRemMedicationIntoOutsidePrescriptionAsMappedDrug ( medication ) );
                            //Logger.Debug ( "Sending {0} as Mapped OutsidePrescription to NewCrop.",
                            //                      medication.MedicationCodeCodedConcept.DisplayName );
                        } );

                return outsidePrescriptions;
            }

            return null;
        }

        private PatientCharacteristicsType BuildPatientCharacteristics ( Patient patient )
        {
            Check.IsNotNull ( patient.Profile.BirthDate, "Patient's Birthdate cannot be null" );
            Debug.Assert ( patient.Profile.BirthDate != null, "patient.BirthDate != null" );

            return new PatientCharacteristicsType
                {
                    Dob = patient.Profile.BirthDate.Value.Date.ToString ( "yyyyMMdd" ),
                    Gender = TransformGenderType ( patient.Profile.PatientGender ),
                    // This assumes Gender is always provided
                    GenderSpecified = true
                };
        }

        private List<PatientAllergyFreeformType> BuildPatientFreeFormAllergies ( Patient patient )
        {
            var allergies = new List<PatientAllergyFreeformType> ();

            var drugAllergies = patient.Allergies
                .Where (
                    a => a.AllergyType.WellKnownName == AllergyType.DrugAllergy
                         && a.AllergyStatus.WellKnownName == AllergyStatus.Active )
                .ToList ();

            drugAllergies.ForEach (
                drugAllergy =>
                    {
                        var da = BuildPatientAllergyFreeformType ( drugAllergy );

                        allergies.Add ( da );
                    } );

            return allergies;
        }

        private PatientType BuildPatientType ( Patient patient )
        {
            if ( patient != null )
            {
                return new PatientType
                    {
                        ID = patient.Key.ToString (),
                        PatientName = new PersonNameType { Last = patient.Name.Last, First = patient.Name.First, Middle = patient.Name.Middle },
                        MedicalRecordNumber = patient.UniqueIdentifier,
                        Memo = string.Empty,
                        PatientAddress = PatientAddress ( patient ),
                        PatientCharacteristics = BuildPatientCharacteristics ( patient ),
                        PatientFreeformAllergy = BuildPatientFreeFormAllergies ( patient )
                    };
            }
            return BuildDummyPatient ();
        }

        private void LoadConfiguration ()
        {
            _newCropPartnerName = _configProvider.GetProperty ( NewCropConfigurationStoreProperty.PartnerNamePropertyName );
            _newCropUserName = _configProvider.GetProperty ( NewCropConfigurationStoreProperty.UserNamePropertyName );
            _newCropPassword = _configProvider.GetProperty ( NewCropConfigurationStoreProperty.PasswordPropertyName );
            _newCropProductName = _configProvider.GetProperty ( NewCropConfigurationStoreProperty.ProductNamePropertyName );
            _newCropProductVersion = _configProvider.GetProperty ( NewCropConfigurationStoreProperty.ProductVersionPropertyName );
            _newCropAccountId = _configProvider.GetProperty ( NewCropConfigurationStoreProperty.AccountIdPropertyName );
            _newCropAccountName = _configProvider.GetProperty ( NewCropConfigurationStoreProperty.AccountNamePropertyName );

            var format = "The Configuration store does not contain a property named {0}";

            Check.IsNotNullOrWhitespace
                (
                    _newCropPartnerName,
                    string.Format ( format, NewCropConfigurationStoreProperty.PartnerNamePropertyName ) );

            Check.IsNotNullOrWhitespace
                (
                    _newCropUserName,
                    string.Format ( format, NewCropConfigurationStoreProperty.UserNamePropertyName ) );

            Check.IsNotNullOrWhitespace
                (
                    _newCropPassword,
                    string.Format ( format, NewCropConfigurationStoreProperty.PasswordPropertyName ) );

            Check.IsNotNullOrWhitespace
                (
                    _newCropProductName,
                    string.Format ( format, NewCropConfigurationStoreProperty.ProductNamePropertyName ) );

            Check.IsNotNullOrWhitespace
                (
                    _newCropProductVersion,
                    string.Format ( format, NewCropConfigurationStoreProperty.ProductVersionPropertyName ) );
        }

        private void LoadEntitiesFromDomain ( long patientKey, long staffKey, long agencyKey, long locationKey )
        {
            Logger.Debug ( "LoadEntitiesFromDomain - Loading Entities" );

            var patientCriteria =
                DetachedCriteria.For<Patient> ( "p" ).Add ( Restrictions.Eq ( Projections.Property ( "p.Key" ), patientKey ) );

            var staffCriteria = DetachedCriteria
                .For<Staff> ( "s" )
                .Add ( Restrictions.Eq ( Projections.Property ( "s.Key" ), staffKey ) );

            var agencyCriteria =
                DetachedCriteria.For<Agency> ( "a" ).Add ( Restrictions.Eq ( Projections.Property ( "a.Key" ), agencyKey ) );

            var locationCriteria =
                DetachedCriteria.For<Location> ( "l" ).Add ( Restrictions.Eq ( Projections.Property ( "l.Key" ), locationKey ) );

            var multiCriteria =
                _session.CreateMultiCriteria ()
                    .Add ( patientCriteria )
                    .Add ( staffCriteria )
                    .Add ( agencyCriteria )
                    .Add ( locationCriteria );

            var criteriaList = multiCriteria.List ();

            try
            {
                _patient = ( ( IList )criteriaList[0] )[0] as Patient;
            }
            catch ( ArgumentOutOfRangeException )
            {
                //// No patient with an Id of [_patientKey] was found. 
                _patient = null;
            }
            _staff = ( ( IList )criteriaList[1] )[0] as Staff;
            _agency = ( ( IList )criteriaList[2] )[0] as Agency;
            _location = ( ( IList )criteriaList[3] )[0] as Location;
        }

        private GenderType TransformGenderType ( PatientGender gender )
        {
            return gender.WellKnownName == Gender.Male ? GenderType.M : GenderType.F;
        }

        // TODO: Based on some business rule

        private OutsidePrescriptionType TransformRemMedicationIntoOutsidePrescriptionAsMappedDrug ( Medication medication )
        {
            Check.IsNotNull ( medication.UsageDateRange.StartDate, "Medication Usage Start Date cannot be null" );
            Debug.Assert ( medication.UsageDateRange.StartDate != null, "medication.UsageDateRange.StartDate != null" );

            var prescription = new OutsidePrescriptionType
                {
                    ExternalId = medication.Key.ToString (),
                    Date = medication.UsageDateRange.StartDate.Value.ToString ( "yyyMMdd" ),
                    DoctorName = medication.PrescribingPhysicianName,
                    DrugIdentifierTypeSpecified = true,
                    DrugIdentifier = medication.MedicationCodeCodedConcept.CodedConceptCode,
                    DrugIdentifierType = DrugDatabaseType.RXNORM,
                    PrescriptionType = "reconcile",
                };
            return prescription;
        }

        #endregion
    }
}
