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
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;
using Pillar.Common.Utility;
using Rem.Domain.Billing.PayorModule;

namespace Rem.Domain.Billing.Tests
{
    /// <summary>
    /// Code table for data element 98. They are used in NM101 and N101 segment.
    /// </summary>
    public static class EntityIdentifierCodes
    {
        #region Constants and Fields

        public const string BUILLING_PROVIDER = "85";
        public const string DROPOFF_LOCATION = "45";
        public const string INSURED_OR_SUBSCRIBER = "IL";
        public const string PATIENT = "QC";
        public const string PAYEE = "PE";
        public const string PAYER = "PR";
        public const string PAY_TO_PROVIDER = "87";
        public const string PICKUP_ADDRESS = "PW";
        public const string PRIMARYCARE_PROVIDER = "P3";
        public const string PURCHASE_SERVICE_PROVIDER = "QB";
        public const string RECEIVER = "40";
        public const string REFERRING_PROVIDER = "DN";
        public const string RENDERING_PROVIDER = "82";
        public const string SERVICE_LOCATION = "77";
        public const string SUBMITTER = "41";
        public const string SUPERVISING_PHYSICIAN = "DQ";

        #endregion
    }

    /// <summary>
    /// Code table for data element 735. They are used in HL03 segment.
    /// </summary>
    public static class HierarchicalLevelCodes
    {
        #region Constants and Fields

        public const string DEPENDENT = "23";
        public const string INFORMATIONSOURCE = "20";
        public const string SUBSCRIBER = "22";

        #endregion
    }

    /// <summary>
    /// Code table for data element 736. They are used in HL04 segment.
    /// </summary>
    public static class HierarchicalChildCode
    {
        #region Constants and Fields

        public const string NoSubordinateHlSegmentInThisHl = "0";
        public const string AdditionalSubordinateHlDataSegmentInhisHl = "1";
        #endregion
    }

    /// <summary>
    /// Code table for data element 128. They are used in REF01 segment and PRV02 segment.
    /// </summary>
    public static class ReferenceIdentificationCodes
    {
        #region Constants and Fields
        //TODO: There are much more to add. 
        // such as: 0B, 1A, 1B, 1C, 1D, 1G, 1H, 1J, D3, G2, LU , 1L, 1W, 28, 6P, 9A, 9C, BB, CE, EA, F8, G1, G3, IG, SY
        // 2U, EI,  FY, NF, D9, G2, LU, PXC

        public const string EMPLOYERS_IDENTIFICATION_NUMBER = "EI";
        public const string HEALTHCARE_PROVIDER_TAXONOMY = "PXC";
        public const string LOCATION_NUMBER = "LU";
        public const string PROVIDER_COMMERCIAL_NUMBER = "G2";
                            //This code designates a proprietary provider number for the destination payer identified in the Payer Name loop, Loop ID-2010BB, associated with this claim. This is to be used by all payers including: Medicare, Medicaid, Blue Cross, etc.
        public const string SOCIAL_SECURITY_NUMBER = "SY";

        #endregion
    }

    /// <summary>
    /// Code table for data element 66. They are used in NM108 segment.
    /// </summary>
    public static class IdentificationCodes
    {
        #region Constants and Fields

        public const string CENTERS_FOR_MEDICARE_AND_MEDICAID_SERVICES_NATIONAL_PROVIDER_IDENTIFIER = "XX";
        public const string CENTERS_FOR_MEDICARE_MEDICAID_SERVICES_PLANID = "XV";
        public const string ELECTRONIC_TRANSMITTER_IDENTIFICATION_NUMBER = "46";
        public const string MEMBER_IDENTIFICATION_NUMBER = "MI";
        public const string PAYOR_IDENTIFICATION = "PI";
        public const string STANDARD_UNIQUE_HEALTH_IDENTIFIER_FOR_EACH_INDIVIDUAL_IN_UNITED_STATES = "II";
        public const string FEDERAL_TAXPAYERS_IDENTIFICATION_NUMBER = "FI";

        #endregion
    }

    /// <summary>
    /// Code table for data element 1138. They are used in SBR01 segment.
    /// </summary>
    public static class PayerResponsibilitySequenceCodes
    {
        #region Constants and Fields

        public const string PAYER_RESPONSIBILITY_EIGHT = "E";
        public const string PAYER_RESPONSIBILITY_ELEVEN = "H";
        public const string PAYER_RESPONSIBILITY_FIVE = "B";
        public const string PAYER_RESPONSIBILITY_FOUR = "A";
        public const string PAYER_RESPONSIBILITY_NINE = "F";
        public const string PAYER_RESPONSIBILITY_SEVEN = "D";
        public const string PAYER_RESPONSIBILITY_SIX = "C";
        public const string PAYER_RESPONSIBILITY_TEN = "G";
        public const string PRIMARY = "P";
        public const string SECONDARY = "S";
        public const string TERTIARY = "T";
        public const string UNKNOWN = "U";

        #endregion
    }


    /// <summary>
    /// Code table for data element 1069. They are used in PAT01 segment.
    /// </summary>
    public static class IndividualRelationshipCodes
    {
        #region Constants and Fields

        public const string CADAVER_DONOR = "40";
        public const string CHILD = "19";
        public const string EMPLOYEE = "20";
        public const string LIFE_PARTNER = "53";
        public const string ORGAN_DONOR = "39";
        public const string OTHER_RELATIONSHIP = "G8";
        public const string SELF = "18";
        public const string SPOUSE = "01";
        public const string UNKNOWN = "21";

        #endregion
    }


    /// <summary>
    /// Code table for data element 1032. Used in SBR09 segment.
    /// </summary>
    public static class ClaimFilingIndicator
    {
        public const string OtherNonPrivateFederalPrograms = "11";
        public const string PreferredProviderOrganizationPpo = "12";
        public const string PointOfServicePos = "13";
        public const string ExclusiveProviderOrganizatioEpo = "14";
        public const string IndemnityInsurance = "15";
        public const string HealthMaintenanceOrganizationHmoMedicareRisk = "16";
        public const string DentalMaintenanceOrganization = "17";
        public const string AutomobileMedical = "AM";
        public const string BlueCrossBlueShield = "BL";
        public const string Champus = "CH";
        public const string CommercialInsuranceCo = "CI";
        public const string Disability = "DS";
        public const string FederalEmployeesProgram = "FI";
        public const string HealthMaintenanceOrganization = "HM";
        public const string LiabilityMedical = "LM";
        public const string MedicarePartA = "MA";
        public const string MedicarePartB = "MB";
        public const string Medicaid = "MC";
        public const string OtherFederalProgram = "OF";
        public const string TitleV = "TV";
        public const string VeteransAffairsPlan = "VA";
        public const string WorkersCompensationHealthClaim = "WC";
        public const string MutuallyDefined = "ZZ";
    }

    ///// <summary>
    ///// Code table for data element 365. used in PER02 and PER05 segment.
    ///// </summary>
    //public static class CommunicationNumberQualifier
    //{
    //    public const string ElectronicMail = "EM";
    //    public const string Facsimile = "FX";
    //    public const string Telephone = "TE";
    //    public const string TelephoneExtension = "EX";
    //}


    ///// <summary>
    ///// Code table for data element 1065. used in NM102 segment.
    ///// </summary>
    //public static class EntityTypeQualifier
    //{
    //    public const string Person = "1";
    //    public const string NonPersonEntity = "2";
    //}
    
    /// <summary>
    /// Code table for data element 1250. used in PAT05, DMG01, and DTP02 segment.
    /// </summary>
    public static class DateTimePeriodFormatQualifier
    {
        public const string DateExpressedInFormatCcyymmdd = "D8";
        public const string RangeOfDatesExpressedInFormatCcyymmddCCyymmdd = "RD8";
    }

    /// <summary>
    /// Code table for data element 374. used in DTP01 segment.
    /// </summary>
    public static class DateTimeQualifier
    {
        public const string OnsetOfCurrentSymptomsOrIllness = "431";
        public const string Service = "472";
    }

    /// <summary>
    /// Code table for data element 1270. Used in HI01-1 and HI02-1 segment, and more.
    /// </summary>
    public static class CodeListQualifierCode
    {
        public const string Icd10CmPrincipalDiagnosis = "ABK";
        public const string Icd9CmPrincipalDiagnosis = "BK";

        public const string Icd10CmDiagnosis = "ABF";
        public const string Icd9CmDiagnosis = "BF";
    }


    /// <summary>
    /// Code table for data  element 640. used in BHT06 segment.
    /// </summary>
    public static class TransactionTypeCode
    {
        public const string SubrogationDemand = "31";
        public const string Chargeable = "CH";
        public const string Reporting = "RP";
    }


    /// <summary>
    /// Code table for data element 1029. used in CLP02 segment.
    /// </summary>
    public static class ClaimStatusCode
    {
        public const string ProcessedAsPrimary = "1";
        public const string ProcessedAsSecondary = "2";
        public const string ProcessedAsTertiary = "3";
        public const string Denied = "4";
        public const string ProcessedAsPrimaryAndForwardedToAdditionalPayers = "19";
        public const string ProcessedAsSecondaryAndForwardedToAdditionalPayers = "20";
        public const string ProcessedAsTertiaryAndForwardedToAdditionalPayers = "21"; 
        public const string ReversalOfPreviousPayment = "22";
        public const string NotOurClaimAndForwardedToAdditionalPayers= "23"; 
        public const string PredeterminationPricingOnlyAndNoPayment = "25";
    }

    /// <summary>
    ///   N3 & N4 exist in pair.
    /// </summary>
    [TestClass]
    public class GenerationOf837pTests
    {
        #region Constants and Fields

        /// <summary>
        ///   N = no signature, Y = Has signature, W = patient refuses to assign benefits.
        /// </summary>
        public const string BENIFITS_ASSIGNMENT_CERTIFICATION_INDICATOR_CODE_YES = "Y";
        public const bool BILLING_PROVIDER_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS = true;
        /// <summary>
        ///   1 = original claim 6 = corrected 7 = replacement 8 = void
        /// </summary>
        public const string CLAIM_FREQUENCY_TYPE_CODE_ORIGINAL = "1";
        public const string DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD = "D8";
        public const string DATE_TIME_QUALIFIER_CODE_SERVICE = "472";
        public const string INTERNATIONAL_CLASSIFICATION_OF_DISEASES_CLINICAL_MODIFICATION_ICD9CM_PRINCIPLE_DIAGNOSTICS = "BK";
        public const bool PATIENT_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS = false;
        public const string PLACE_OF_SERVICE_CODES_FOR_PROFESSIONAL_OR_DENTAL_SERVICES = "B";
        /// <summary>
        ///   Code table: https://www.cms.gov/PhysicianFeeSched/Downloads/Website_POS_database.pdf
        /// </summary>
        public const string PLACE_OF_SERVICE_CODE_OFFICE = "11";
        public const string PROVIDER_ACCEPTANCE_ASSIGNMENT_CODE_ASSIGNED = "A";
        /// <summary>
        ///   “Y” value indicates the provider signature is on file; an “N” value indicates the provider signature is not on file.
        /// </summary>
        public const bool PROVIDER_OR_SUPPLIER_SIGNATURE_INDICATOR_CODE_YES = true;
        /// <summary>
        ///   I = Informed Consent to Release Medical Information for Conditions or Diagnoses Regulated by Federal Statutes (Required when the provider has not collected a signature AND state or federal laws do not require a signature be collected) Y = Yes, Provider has a Signed Statement Permitting Release of Medical Billing Data Related to a Claim. (Required when the provider has collected a signature. OR Required when state or federal laws require a signature be collected)
        /// </summary>
        public const string RELEASE_OF_INFORMATION_CODE_YES = "Y";
        public const string SERVICE_ID_QUALIFIER_CODE_HCPCS = "HC";
        public const bool SUBSCRIBER_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS = true;
        public const string UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT = "UN";
        private const string BEGINING_OF_HIERARCHICAL_TRANSACTION = "BHT";
        private const string BILLING_PROVIDER = "85";
        private const string FUNCTIONAL_IDENTIFIER_CODE = "HC"; //Always 'HC' - Health care claim (837).
        private const string HEALTHCARE_PROVIDER_TAXONOMY_VALUE = "203BF0100Y";
                             //CODE SOURCE 682: Health Care Provider Taxonomy. External source : http://www.wpc-edi.com/reference/
        private const string HIERARCHICAL_STRUCTURE_CODE_INFORMATIONSOURCE_SUBSCRIBER_DEPENDENT = "0019";
        private const string IMPLEMENTATION_GUIDE_VERSION_NAME = "005010X222";
                             //Always the same. Refer Pg: 1. See code table 'Version / Release / Industry Identifier Code'.
        private const string INFORMATION_CONTACT = "IC";
        private const string INTERCHANGE_CONTROL_VERSION_NUMBER = "00501";
        private const string INTERCHANGE_IDENTIFIER_CODE_MUTUALLY_DEFINED = "ZZ";
        private const bool IS_PRODUCTION = false;
        private const string PROVIDER_CODE_BILLING = "BI";
        private const string REPITITION_SEPARATOR = "^";
        private const string TRANSACTIONSET_IDENTIFIER_CODE = "837"; //Always same. See code table 'Transaction Set Identifier'
        private const string TRANSACTIONSET_PURPOSE_CODE_ORIGINAL = "00";
        private const string REFERENCE_IDENTIFICATION_BATCH_CONTROL_NUMBER = "0";
        private static X12Delimiters x12Delimiters = new X12Delimiters ( ':', '*', '~', '^' );
        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void PatientIsNotSelfInsuredTest ()
        {
            var creationDateTime = new DateTime ( 2011, 02, 13, 12, 53, 00 ); //DateTime.Now.
            int autoIncrementedControlNumber = 1; //Must be persisted, so that it will be unique per interchange.
            int autoIncrementedHierarchicalIDNumber = 1; //The first HL01 within each ST-SE envelope must begin with “1”.
            int autoIncrementedServiceLineNumber = 1;

            var message = new Interchange ( creationDateTime, autoIncrementedControlNumber, IS_PRODUCTION, x12Delimiters.SegmentDelimiter, x12Delimiters.ElementDelimiter, x12Delimiters.CompositeDelimiter )
                {
                    //When Authorization information is not provided, it chooses 00 as qualifier with empty value. Refer 'Authorization Information Qualifier' code table.

                    //SecurityInfoQualifier = SECURITY_INFORMATION_CODE_PASSWORD, //Refer 'Security Information Qualifier'. What does WITS do ?
                    //SecurityInfo = "THEPASSWORD", //What does WITS do ?

                    InterchangeSenderIdQualifier = INTERCHANGE_IDENTIFIER_CODE_MUTUALLY_DEFINED,
                    //Need to a code table for 'Interchange ID Qualifier'. Pg: 638. ZZ - 'Mutually Defined'.
                    InterchangeSenderId = "SUBMITTERS.ID",
                    //HealthCareClaim837Setup.InterchangeSenderNumber

                    InterchangeReceiverIdQualifier = INTERCHANGE_IDENTIFIER_CODE_MUTUALLY_DEFINED,
                    //Refer 'Interchange ID Qualifier'. Can we reuse the same code table ?
                    InterchangeReceiverId = "RECEIVERS.ID" //HealthCareClaim837Setup.InterchangeRecieverNumber
                };

            message.SetElement ( 12, INTERCHANGE_CONTROL_VERSION_NUMBER );
                //Refer 'Interchange Control Version Number' code table. It has only 1 value.
            //message.SetElement(10, "1253"); //Interchange time, can be part of 'interchangeDateTime' variable declared above.
            message.SetElement ( 11, REPITITION_SEPARATOR ); //Repetition separator.
            message.SetElement(14, "1");

            var group = message.AddFunctionGroup (
                FUNCTIONAL_IDENTIFIER_CODE, creationDateTime, autoIncrementedControlNumber, IMPLEMENTATION_GUIDE_VERSION_NAME );
            group.ApplicationSendersCode = "SENDER CODE"; //HealthCareClaim837Setup.InterchangeSenderNumber
            group.ApplicationReceiversCode = "RECEIVER CODE"; //HealthCareClaim837Setup.InterchangeRecieverNumber
            group.ResponsibleAgencyCode = "X";
            //group.Date = Convert.ToDateTime("12/31/1999"); //Overwriting given DateTime.Now value. Functional Group Creation Date.
            //group.ControlNumber = 1; //Control number overwritten. Same as 'autoIncrementedFunctionalGroupControlNumber' variable.
            //group.SetElement(5, "0802"); //Overwriting given DateTime.Now value. Functional Group Creation Time. hh:mm

            string interchangeControlNumber = autoIncrementedControlNumber.ToString ( CultureInfo.InvariantCulture );
            interchangeControlNumber = interchangeControlNumber.Length < 4 ? interchangeControlNumber.PadLeft ( 4, '0' ) : interchangeControlNumber;
            var transaction = group.AddTransaction ( TRANSACTIONSET_IDENTIFIER_CODE, interchangeControlNumber ); //Pg: 70.
            //transaction.SetElement(2, transactionSetControlNumber); 
            transaction.SetElement ( 3, IMPLEMENTATION_GUIDE_VERSION_NAME );
                //It is the Implementation Guide Version Name. Refer 'Version / Release / Industry Identifier' Code table

            var bhtSegment = transaction.AddSegment ( BEGINING_OF_HIERARCHICAL_TRANSACTION );
            bhtSegment.SetElement ( 1, HIERARCHICAL_STRUCTURE_CODE_INFORMATIONSOURCE_SUBSCRIBER_DEPENDENT );
                //Refer 'Hierarchical Structure' Code table.
            bhtSegment.SetElement ( 2, TRANSACTIONSET_PURPOSE_CODE_ORIGINAL ); //Refer 'Transaction Set Purpose' code table.
            bhtSegment.SetElement(3, REFERENCE_IDENTIFICATION_BATCH_CONTROL_NUMBER); //Acts as batch control number. 30 chars.
            bhtSegment.SetElement(4, "20061015"); //date that the original submitter created the claim file from their business application system.
            bhtSegment.SetElement(5, "1023"); //- time that the original submitter created the claim file from their business application system.
            bhtSegment.SetElement(6, "CH"); // - indicates that the transaction contains only fee for service claims or claims with at least one chargeable line item. Refer code table 'Transaction Type'. 

            //Submitter Name
            var submitterLoop = transaction.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.SUBMITTER ) );
                //submitter identifier code. See 'Entity Identifier' code table
            submitterLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity; //See 'Entity Type Qualifier' code table.
            submitterLoop.NM103_NameLastOrOrganizationName = "SAFE HARBOR"; //BillingOffice.Agency.LegalName
            //submitterLoop.NM104_NameFirst = ""; //Always empty when EntityTypeQualifier is Non-Person entity.
            submitterLoop.NM108_IdCodeQualifier = IdentificationCodes.ELECTRONIC_TRANSMITTER_IDENTIFICATION_NUMBER;
                //See code table 'Identification Code Qualifier' - Electronic Transmitter Identification Number - established by trading partner agreement.
            submitterLoop.NM109_IdCode = "SAFEHARBOR_ETIN"; //BillingOffice.ElectronicTransmitterIdentificationNumber.

            //SUBMITTER EDI CONTACT INFORMATION.
            var perSegment = submitterLoop.AddSegment ( new TypedSegmentPER () );
            perSegment.PER01_ContactFunctionCode = INFORMATION_CONTACT; //information contact function code
            perSegment.PER02_Name = "JERRY";
                //BillingOffice.Administrator - Situational - required when the person to contact is different from the subscriber. As we provided a nonperson entity we need to provide the person name.
            perSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone; //See code table 'Communication Number Qualifier'.
            perSegment.PER04_CommunicationNumber = "3055552222"; //telephone. //BillingOffice.Administrator.TelephoneNumber
            //if(There is telephone extension)
            perSegment.PER05_CommunicationNumberQualifier = CommunicationNumberQualifer.TelephoneExtension;
                //See code table 'Communication Number Qualifier'.
            perSegment.PER06_CommunicationNumber = "231"; //telephone extension //BillingOffice.Administrator.TelephoneNumberExtension

            //RECEIVER NAME
            var receiverLoop = transaction.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.RECEIVER ) );
                //receiver identifier code. See 'Entity Identifier' code table
            receiverLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity; //See 'Entity Type Qualifier' code table.
            receiverLoop.NM103_NameLastOrOrganizationName = "KEY INSURANCE COMPANY"; //Payor.Name
            //receiverLoop.NM104_NameFirst = ""; //Always empty when EntityTypeQualifier is Non-Person entity.
            receiverLoop.NM108_IdCodeQualifier = IdentificationCodes.ELECTRONIC_TRANSMITTER_IDENTIFICATION_NUMBER;
                //See code table 'Identification Code Qualifier' - Electronic Transmitter Identification Number - established by trading partner agreement.
            receiverLoop.NM109_IdCode = "KEY_INSURANCE_ETIN"; //Payor.ElectronicTransmitterIdentificationNumber.

            //Billing Provider Hierarchical Level.
            var provider2000AHierachicalLoop = transaction.AddHLoop (
                autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                HierarchicalLevelCodes.INFORMATIONSOURCE,
                BILLING_PROVIDER_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS ); //*********HL 1 ******. 
            var prvSegment = provider2000AHierachicalLoop.AddSegment ( new TypedSegmentPRV () );
                //Specialty Segment. PRV - BILLING PROVIDER SPECIALTY INFORMATION.
            prvSegment.PRV01_ProviderCode = PROVIDER_CODE_BILLING; //Refer 'Provider Code' code table.
            prvSegment.PRV02_ReferenceIdQualifier = ReferenceIdentificationCodes.HEALTHCARE_PROVIDER_TAXONOMY;
                //Refer 'Reference Identification Qualifier' code table
            prvSegment.PRV03_ProviderTaxonomyCode = HEALTHCARE_PROVIDER_TAXONOMY_VALUE;

            //Billing provider Name
            var provider2010AALoop = provider2000AHierachicalLoop.AddLoop ( new TypedLoopNM1 ( BILLING_PROVIDER ) );
            provider2010AALoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            provider2010AALoop.NM103_NameLastOrOrganizationName = "Safe Harbor"; //BillingOffice.Agency.LegalName
            provider2010AALoop.NM108_IdCodeQualifier = IdentificationCodes.CENTERS_FOR_MEDICARE_AND_MEDICAID_SERVICES_NATIONAL_PROVIDER_IDENTIFIER;
            provider2010AALoop.NM109_IdCode = "9876543210"; //BillingOffice.Agency.Identifiers.NPINumber

            //Billing provider address
            var provider2010AA_N3Segment = provider2010AALoop.AddSegment ( new TypedSegmentN3 () );
            provider2010AA_N3Segment.N301_AddressInformation = "234 SEAWAY ST";
                //BillingOffice.Agency.Address.FirstStreetAddress & SecondStreetAddress

            //Billing provider City,State,Zipcode
            var provider2010AA_N4Segment = provider2010AALoop.AddSegment ( new TypedSegmentN4 () );
            provider2010AA_N4Segment.N401_CityName = "MIAMI"; //BillingOffice.Agency.Address.CityName
            provider2010AA_N4Segment.N402_StateOrProvinceCode = "FL"; //BillingOffice.Agency.Address.StateProvince
            provider2010AA_N4Segment.N403_PostalCode = "33111"; //BillingOffice.Agency.Address.PostalCode

            //Billing provider - tax identification
            var provider2010AA_REFSegment = provider2010AALoop.AddSegment ( new TypedSegmentREF () );
            provider2010AA_REFSegment.REF01_ReferenceIdQualifier = ReferenceIdentificationCodes.EMPLOYERS_IDENTIFICATION_NUMBER;
            provider2010AA_REFSegment.REF02_ReferenceId = "587654321"; //BillingOffice.Agency.Identifiers.FederalTaxID

            var provider2010ABLoop = provider2000AHierachicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.PAY_TO_PROVIDER ) );
            provider2010ABLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;

            var provider2010AB_N3Segment = provider2010ABLoop.AddSegment ( new TypedSegmentN3 () );
            provider2010AB_N3Segment.N301_AddressInformation = "234 SEAWAY ST";
                //BillingOffice.Agency.Address.FirstStreetAddress & SecondStreetAddress

            var provider2010AB_N4Segment = provider2010ABLoop.AddSegment ( new TypedSegmentN4 () );
            provider2010AB_N4Segment.N401_CityName = "MIAMI"; //BillingOffice.Agency.Address.CityName
            provider2010AB_N4Segment.N402_StateOrProvinceCode = "FL"; //BillingOffice.Agency.Address.StateProvince
            provider2010AB_N4Segment.N403_PostalCode = "33111"; //BillingOffice.Agency.Address.PostalCode

            autoIncrementedHierarchicalIDNumber++;
            var subscriber2000BHierarchicalLoop =
                provider2000AHierachicalLoop.AddHLoop (
                    autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                    HierarchicalLevelCodes.SUBSCRIBER,
                    SUBSCRIBER_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS ); // **** HL 2  ******

            var segmentSBR = subscriber2000BHierarchicalLoop.AddSegment ( new TypedSegmentSBR () );
            segmentSBR.SBR01_PayerResponsibilitySequenceNumberCode = PayerResponsibilitySequenceCodes.PRIMARY;
            //segmentSBR.SBR03_PolicyOrGroupNumber = "2222-SJ"; //Required when the subscriber’s identification card for the destination payer (Loop ID-2010BB) shows a group number.
            //segmentSBR.SBR09_ClaimFilingIndicatorCode = "CI"; //Not in practical use.

            var subscriberName2010BALoop = subscriber2000BHierarchicalLoop.AddLoop (
                new TypedLoopNM1 ( EntityIdentifierCodes.INSURED_OR_SUBSCRIBER ) );
            subscriberName2010BALoop.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            subscriberName2010BALoop.NM104_NameFirst = "JANE"; //PayorSubscriber.LastName
            subscriberName2010BALoop.NM103_NameLastOrOrganizationName = "SMITH"; //PayorSubscriber.FirstName
            subscriberName2010BALoop.NM109_IdCode = "JS00111223333"; //PayorSubscriber.PayorCoverage.MemberNumber
            subscriberName2010BALoop.NM108_IdCodeQualifier = IdentificationCodes.MEMBER_IDENTIFICATION_NUMBER;

            //var subscriber_DMGSegment = subscriberName2010BALoop.AddSegment(new TypedSegmentDMG());
            //subscriber_DMGSegment.DMG01_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            //subscriber_DMGSegment.DMG02_DateOfBirth = DateTime.Parse("5/1/1943"); //PayorSubscriber.BirthDate
            //subscriber_DMGSegment.DMG03_Gender = Gender.Female; //PayorSubscriber.AdministrativeGender

            var subscriberName2010BBLoop = subscriber2000BHierarchicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.PAYER ) );
            subscriberName2010BBLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            subscriberName2010BBLoop.NM103_NameLastOrOrganizationName = "KEY INSURANCE COMPANY"; //Payor.Name
            subscriberName2010BBLoop.NM108_IdCodeQualifier = IdentificationCodes.PAYOR_IDENTIFICATION;
            subscriberName2010BBLoop.NM109_IdCode = "999996666"; //NPI? Kate needs time to check.

            //optional.
            //var refSegment2 = subscriberName2010BBLoop.AddSegment(new TypedSegmentREF());
            //refSegment2.REF01_ReferenceIdQualifier = ReferenceIdentificationCodes.PROVIDER_COMMERCIAL_NUMBER;
            //refSegment2.REF02_ReferenceId = "KA6663";

            autoIncrementedHierarchicalIDNumber++;
            //PATIENT HIERARCHICAL LEVEL Loop Repeat: >1
            var patientDetailLoop2000CLoop =
                subscriber2000BHierarchicalLoop.AddHLoop (
                    autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                    HierarchicalLevelCodes.DEPENDENT,
                    PATIENT_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS ); // **** HL 3  ******

            var HL3PATSegment = patientDetailLoop2000CLoop.AddSegment(new TypedSegmentPAT());
            HL3PATSegment.PAT01_IndividualRelationshipCode = IndividualRelationshipCodes.CHILD;

            //How does the billing side access patient information on the clinical side.
            var HL3NM1Segment = patientDetailLoop2000CLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.PATIENT ) );
            HL3NM1Segment.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            HL3NM1Segment.NM104_NameFirst = "TED"; //Patinet.FirstName
            HL3NM1Segment.NM103_NameLastOrOrganizationName = "SMITH"; //Patinet.LastName

            // add N3 and N4 segments under the above NM1 loop

            var HL3NM1_N3_Segment = HL3NM1Segment.AddSegment ( new TypedSegmentN3 () );
            HL3NM1_N3_Segment.N301_AddressInformation = "236 N MAIN ST"; //Patient.Address.FirstStreetAddress & SecondStreetAddress

            var HL3NM1_N4_Segment = HL3NM1Segment.AddSegment ( new TypedSegmentN4 () );
            HL3NM1_N4_Segment.N401_CityName = "MIAMI"; //Patient.Address.CityName
            HL3NM1_N4_Segment.N402_StateOrProvinceCode = "FL"; //Patient.Address.StateProvince
            HL3NM1_N4_Segment.N403_PostalCode = "33413"; //Patient.Address.PostalCode

            var HL3NM1_DMG_Segment = HL3NM1Segment.AddSegment ( new TypedSegmentDMG () );
            HL3NM1_DMG_Segment.DMG01_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            HL3NM1_DMG_Segment.DMG02_DateOfBirth = Convert.ToDateTime ( "5/1/1973" ); //Patinet.BirthDate
            HL3NM1_DMG_Segment.DMG03_Gender = Gender.Male; //Patinet.AdministrativeGender

            var claim2300Loop = patientDetailLoop2000CLoop.AddLoop ( new TypedLoopCLM () );
            claim2300Loop.CLM01_PatientControlNumber = "26463774"; //BillingOffice.PlaceOfService.Claims[0].Key
            claim2300Loop.CLM02_TotalClaimChargeAmount = Convert.ToDecimal ( 100 ); //BillingOffice.PlaceOfService.Claims[0].ChargeAmount
            claim2300Loop.CLM05._1_FacilityCodeValue = PLACE_OF_SERVICE_CODE_OFFICE;
            claim2300Loop.CLM05._2_FacilityCodeQualifier = PLACE_OF_SERVICE_CODES_FOR_PROFESSIONAL_OR_DENTAL_SERVICES;
            claim2300Loop.CLM05._3_ClaimFrequencyTypeCode = CLAIM_FREQUENCY_TYPE_CODE_ORIGINAL;
            claim2300Loop.CLM06_ProviderOrSupplierSignatureIndicator = PROVIDER_OR_SUPPLIER_SIGNATURE_INDICATOR_CODE_YES;
            claim2300Loop.CLM07_ProviderAcceptAssignmentCode = PROVIDER_ACCEPTANCE_ASSIGNMENT_CODE_ASSIGNED;
            claim2300Loop.CLM08_BenefitsAssignmentCerficationIndicator = BENIFITS_ASSIGNMENT_CERTIFICATION_INDICATOR_CODE_YES;
            claim2300Loop.CLM09_ReleaseOfInformationCode = RELEASE_OF_INFORMATION_CODE_YES; //Kate says 'I don't think it matters too much'

            //Claim Identifier for Transmission Intermediaries is the new name for the Claim Identification Number for Clearinghouses 
            //and Other Transmission Intermediaries segment. The qualifier (REF01 = D9) did not change. ??? Do we use the intermediaries ? Kate says 'Not Yet'.
            //var refSegment = claim2300Loop.AddSegment(new TypedSegmentREF());
            //refSegment.REF01_ReferenceIdQualifier = "D9";
            //refSegment.REF02_ReferenceId = "17312345600006351";

            var hiSegment = claim2300Loop.AddSegment ( new TypedSegmentHI () );
            //Which value to use ?
            hiSegment.HI01_HealthCareCodeInformation =
                X12Utility.BuildCompositeElement( x12Delimiters.CompositeDelimiter, INTERNATIONAL_CLASSIFICATION_OF_DISEASES_CLINICAL_MODIFICATION_ICD9CM_PRINCIPLE_DIAGNOSTICS, "0340" );
            //hiSegment.HI02_HealthCareCodeInformation = hiSegment.X12Utility.BuildCompositeElement("BF", "V7389"); - optional

            var lxLoop = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment = lxLoop.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "99213" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment.SV102_MonetaryAmount = "40"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].ChargeAmount
            sv1Segment.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT; //Constant.
            sv1Segment.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment = lxLoop.AddSegment ( new TypedSegmentDTP () );
            dtpSegment.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate = DateTime.ParseExact ( "20061003", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment.DTP03_Date = theDate;

            autoIncrementedServiceLineNumber++;
            var lxLoop2 = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop2.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment2 = lxLoop2.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment2.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "87070" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment2.SV102_MonetaryAmount = "15"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].ChargeAmount
            sv1Segment2.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT; //
            sv1Segment2.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment2.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment2 = lxLoop2.AddSegment ( new TypedSegmentDTP () );
            dtpSegment2.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment2.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate2 = DateTime.ParseExact ( "20061003", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment2.DTP03_Date = theDate2;

            autoIncrementedServiceLineNumber++;
            var lxLoop3 = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop3.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment3 = lxLoop3.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment3.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "99214" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment3.SV102_MonetaryAmount = "35";
            sv1Segment3.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT;
            sv1Segment3.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment3.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment3 = lxLoop3.AddSegment ( new TypedSegmentDTP () );
            dtpSegment3.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment3.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate3 = DateTime.ParseExact ( "20061010", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment3.DTP03_Date = theDate3;

            autoIncrementedServiceLineNumber++;
            var lxLoop4 = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop4.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment4 = lxLoop4.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment4.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "86663" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment4.SV102_MonetaryAmount = "10";
            sv1Segment4.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT;
            sv1Segment4.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment4.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment4 = lxLoop4.AddSegment ( new TypedSegmentDTP () );
            dtpSegment4.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment4.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate4 = DateTime.ParseExact ( "20061010", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment4.DTP03_Date = theDate4;

            var x12 = message.SerializeToX12 ( true );
            var ediString = Pillar.Common.Utility.EmbeddedResourceUtil.GetEmbeddedResourceValue("Rem.Domain.Billing.Tests.TestData._837p.Example1_PatientIsChildOfSubscriber.txt", Assembly.GetExecutingAssembly());
            Assert.AreEqual(ediString, x12);
        }

        /// <summary>
        /// 1. In SBR (page: 116), SBR02 element is needed. 
        /// 2. The PAT (page: 119) segment after SBR is needed. Only PAT09 is used when the patient is alive. 
        /// 3. Loop2010BA: 
        ///     a. N3 (page: 124)-Subscriber Address is required. 
        ///     b. N3 & N4 go together. 
        ///     c. DMG - is needed
        /// 4. Loop2000C: HL, NM1, N3, N4 not needed.
        /// </summary>
        [TestMethod]
        public void PatientIsSelfInsuredTest ()
        {
            var creationDateTime = new DateTime ( 2011, 02, 13, 12, 53, 00 ); //DateTime.Now.
            int autoIncrementedControlNumber = 1; //Must be persisted, so that it will be unique per interchange.
            int autoIncrementedHierarchicalIDNumber = 1; //The first HL01 within each ST-SE envelope must begin with “1”.
            int autoIncrementedServiceLineNumber = 1;

            var message = new Interchange ( creationDateTime, autoIncrementedControlNumber, IS_PRODUCTION )
                {
                    //When Authorization information is not provided, it chooses 00 as qualifier with empty value. Refer 'Authorization Information Qualifier' code table.

                    //SecurityInfoQualifier = SECURITY_INFORMATION_CODE_PASSWORD, //Refer 'Security Information Qualifier'. What does WITS do ?
                    //SecurityInfo = "THEPASSWORD", //What does WITS do ?

                    InterchangeSenderIdQualifier = INTERCHANGE_IDENTIFIER_CODE_MUTUALLY_DEFINED,
                    //Need to a code table for 'Interchange ID Qualifier'. Pg: 638. ZZ - 'Mutually Defined'.
                    InterchangeSenderId = "SUBMITTERS.ID",
                    //HealthCareClaim837Setup.InterchangeSenderNumber

                    InterchangeReceiverIdQualifier = INTERCHANGE_IDENTIFIER_CODE_MUTUALLY_DEFINED,
                    //Refer 'Interchange ID Qualifier'. Can we reuse the same code table ?
                    InterchangeReceiverId = "RECEIVERS.ID" //HealthCareClaim837Setup.InterchangeRecieverNumber
                };

            message.SetElement ( 12, INTERCHANGE_CONTROL_VERSION_NUMBER );
                //Refer 'Interchange Control Version Number' code table. It has only 1 value.
            //message.SetElement(10, "1253"); //Interchange time, can be part of 'interchangeDateTime' variable declared above.
            message.SetElement ( 11, REPITITION_SEPARATOR ); //Repetition separator.
            message.SetElement(14, "1");

            var group = message.AddFunctionGroup (
                FUNCTIONAL_IDENTIFIER_CODE, creationDateTime, autoIncrementedControlNumber, IMPLEMENTATION_GUIDE_VERSION_NAME );
            group.ApplicationSendersCode = "SENDER CODE"; //HealthCareClaim837Setup.InterchangeSenderNumber
            group.ApplicationReceiversCode = "RECEIVER CODE"; //HealthCareClaim837Setup.InterchangeRecieverNumber
            group.ResponsibleAgencyCode = "X";
            //group.Date = Convert.ToDateTime("12/31/1999"); //Overwriting given DateTime.Now value. Functional Group Creation Date.
            //group.ControlNumber = 1; //Control number overwritten. Same as 'autoIncrementedFunctionalGroupControlNumber' variable.
            //group.SetElement(5, "0802"); //Overwriting given DateTime.Now value. Functional Group Creation Time. hh:mm

            string interchangeControlNumber = autoIncrementedControlNumber.ToString ( CultureInfo.InvariantCulture );
            interchangeControlNumber = interchangeControlNumber.Length < 4 ? interchangeControlNumber.PadLeft ( 4, '0' ) : interchangeControlNumber;
            var transaction = group.AddTransaction ( TRANSACTIONSET_IDENTIFIER_CODE, interchangeControlNumber ); //Pg: 70.
            //transaction.SetElement(2, transactionSetControlNumber); 
            transaction.SetElement ( 3, IMPLEMENTATION_GUIDE_VERSION_NAME );
                //It is the Implementation Guide Version Name. Refer 'Version / Release / Industry Identifier' Code table

            var bhtSegment = transaction.AddSegment ( BEGINING_OF_HIERARCHICAL_TRANSACTION );
            bhtSegment.SetElement ( 1, HIERARCHICAL_STRUCTURE_CODE_INFORMATIONSOURCE_SUBSCRIBER_DEPENDENT );
                //Refer 'Hierarchical Structure' Code table.
            bhtSegment.SetElement ( 2, TRANSACTIONSET_PURPOSE_CODE_ORIGINAL ); //Refer 'Transaction Set Purpose' code table.
            bhtSegment.SetElement(3, REFERENCE_IDENTIFICATION_BATCH_CONTROL_NUMBER); //Acts as batch control number. 30 chars.
            bhtSegment.SetElement(4, "20061015"); //date that the original submitter created the claim file from their business application system.
            bhtSegment.SetElement(5, "1023"); //- time that the original submitter created the claim file from their business application system.
            bhtSegment.SetElement(6, "CH"); // - indicates that the transaction contains only fee for service claims or claims with at least one chargeable line item. Refer code table 'Transaction Type'. 

            //Submitter Name
            var submitterLoop = transaction.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.SUBMITTER ) );
                //submitter identifier code. See 'Entity Identifier' code table
            submitterLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity; //See 'Entity Type Qualifier' code table.
            submitterLoop.NM103_NameLastOrOrganizationName = "SAFE HARBOR"; //BillingOffice.Agency.LegalName
            //submitterLoop.NM104_NameFirst = ""; //Always empty when EntityTypeQualifier is Non-Person entity.
            submitterLoop.NM108_IdCodeQualifier = IdentificationCodes.ELECTRONIC_TRANSMITTER_IDENTIFICATION_NUMBER;
                //See code table 'Identification Code Qualifier' - Electronic Transmitter Identification Number - established by trading partner agreement.
            submitterLoop.NM109_IdCode = "SAFEHARBOR_ETIN"; //BillingOffice.ElectronicTransmitterIdentificationNumber.

            //SUBMITTER EDI CONTACT INFORMATION.
            var perSegment = submitterLoop.AddSegment ( new TypedSegmentPER () );
            perSegment.PER01_ContactFunctionCode = INFORMATION_CONTACT; //information contact function code
            perSegment.PER02_Name = "JERRY";
                //BillingOffice.Administrator - Situational - required when the person to contact is different from the subscriber. As we provided a nonperson entity we need to provide the person name.
            perSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone; //See code table 'Communication Number Qualifier'.
            perSegment.PER04_CommunicationNumber = "3055552222"; //telephone. //BillingOffice.Administrator.TelephoneNumber
            //if(There is telephone extension)
            perSegment.PER05_CommunicationNumberQualifier = CommunicationNumberQualifer.TelephoneExtension;
                //See code table 'Communication Number Qualifier'.
            perSegment.PER06_CommunicationNumber = "231"; //telephone extension //BillingOffice.Administrator.TelephoneNumberExtension

            //RECEIVER NAME
            var receiverLoop = transaction.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.RECEIVER ) );
                //receiver identifier code. See 'Entity Identifier' code table
            receiverLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity; //See 'Entity Type Qualifier' code table.
            receiverLoop.NM103_NameLastOrOrganizationName = "KEY INSURANCE COMPANY"; //Payor.Name
            //receiverLoop.NM104_NameFirst = ""; //Always empty when EntityTypeQualifier is Non-Person entity.
            receiverLoop.NM108_IdCodeQualifier = IdentificationCodes.ELECTRONIC_TRANSMITTER_IDENTIFICATION_NUMBER;
                //See code table 'Identification Code Qualifier' - Electronic Transmitter Identification Number - established by trading partner agreement.
            receiverLoop.NM109_IdCode = "KEY_INSURANCE_ETIN"; //Payor.ElectronicTransmitterIdentificationNumber.

            //Billing Provider Hierarchical Level.
            var provider2000AHierachicalLoop = transaction.AddHLoop (
                autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                HierarchicalLevelCodes.INFORMATIONSOURCE,
                BILLING_PROVIDER_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS ); //*********HL 1 ******. 
            var prvSegment = provider2000AHierachicalLoop.AddSegment ( new TypedSegmentPRV () );
                //Specialty Segment. PRV - BILLING PROVIDER SPECIALTY INFORMATION.
            prvSegment.PRV01_ProviderCode = PROVIDER_CODE_BILLING; //Refer 'Provider Code' code table.
            prvSegment.PRV02_ReferenceIdQualifier = ReferenceIdentificationCodes.HEALTHCARE_PROVIDER_TAXONOMY;
                //Refer 'Reference Identification Qualifier' code table
            prvSegment.PRV03_ProviderTaxonomyCode = HEALTHCARE_PROVIDER_TAXONOMY_VALUE;

            //Billing provider Name
            var provider2010AALoop = provider2000AHierachicalLoop.AddLoop ( new TypedLoopNM1 ( BILLING_PROVIDER ) );
            provider2010AALoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            provider2010AALoop.NM103_NameLastOrOrganizationName = "Safe Harbor"; //BillingOffice.Agency.LegalName
            provider2010AALoop.NM108_IdCodeQualifier = IdentificationCodes.CENTERS_FOR_MEDICARE_AND_MEDICAID_SERVICES_NATIONAL_PROVIDER_IDENTIFIER;
            provider2010AALoop.NM109_IdCode = "9876543210"; //BillingOffice.Agency.Identifiers.NPINumber

            //Billing provider address
            var provider2010AA_N3Segment = provider2010AALoop.AddSegment ( new TypedSegmentN3 () );
            provider2010AA_N3Segment.N301_AddressInformation = "234 SEAWAY ST";
                //BillingOffice.Agency.Address.FirstStreetAddress & SecondStreetAddress

            //Billing provider City,State,Zipcode
            var provider2010AA_N4Segment = provider2010AALoop.AddSegment ( new TypedSegmentN4 () );
            provider2010AA_N4Segment.N401_CityName = "MIAMI"; //BillingOffice.Agency.Address.CityName
            provider2010AA_N4Segment.N402_StateOrProvinceCode = "FL"; //BillingOffice.Agency.Address.StateProvince
            provider2010AA_N4Segment.N403_PostalCode = "33111"; //BillingOffice.Agency.Address.PostalCode

            //Billing provider - tax identification
            var provider2010AA_REFSegment = provider2010AALoop.AddSegment ( new TypedSegmentREF () );
            provider2010AA_REFSegment.REF01_ReferenceIdQualifier = ReferenceIdentificationCodes.EMPLOYERS_IDENTIFICATION_NUMBER;
            provider2010AA_REFSegment.REF02_ReferenceId = "587654321"; //BillingOffice.Agency.Identifiers.FederalTaxID

            var provider2010ABLoop = provider2000AHierachicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.PAY_TO_PROVIDER ) );
            provider2010ABLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;

            var provider2010AB_N3Segment = provider2010ABLoop.AddSegment ( new TypedSegmentN3 () );
            provider2010AB_N3Segment.N301_AddressInformation = "234 SEAWAY ST";
                //BillingOffice.Agency.Address.FirstStreetAddress & SecondStreetAddress

            var provider2010AB_N4Segment = provider2010ABLoop.AddSegment ( new TypedSegmentN4 () );
            provider2010AB_N4Segment.N401_CityName = "MIAMI"; //BillingOffice.Agency.Address.CityName
            provider2010AB_N4Segment.N402_StateOrProvinceCode = "FL"; //BillingOffice.Agency.Address.StateProvince
            provider2010AB_N4Segment.N403_PostalCode = "33111"; //BillingOffice.Agency.Address.PostalCode

            autoIncrementedHierarchicalIDNumber++;
            var subscriber2000BHierarchicalLoop =
                provider2000AHierachicalLoop.AddHLoop (
                    autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                    HierarchicalLevelCodes.SUBSCRIBER,
                    false /*SUBSCRIBER_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS*/ ); // **** HL 2  ****** No Child HL

            //Subscriber information
            var segmentSBR = subscriber2000BHierarchicalLoop.AddSegment ( new TypedSegmentSBR () );
            segmentSBR.SBR01_PayerResponsibilitySequenceNumberCode = PayerResponsibilitySequenceCodes.PRIMARY;
            segmentSBR.SBR02_IndividualRelationshipCode = IndividualRelationshipCodes.SELF;
            //segmentSBR.SBR03_PolicyOrGroupNumber = "2222-SJ"; //Required when the subscriber’s identification card for the destination payer (Loop ID-2010BB) shows a group number.
            //segmentSBR.SBR09_ClaimFilingIndicatorCode = "CI"; //Not in practical use.

            //Patient information - Required when the patient is the subscriber.
            //var segmentPAT = subscriber2000BHierarchicalLoop.AddSegment ( new TypedSegmentPAT () );
            //segmentPAT.PAT09_PregnancyIndicator = false;

            //Loop 2010BA - Subscriber Name
            var subscriberName2010BALoop = subscriber2000BHierarchicalLoop.AddLoop (
                new TypedLoopNM1 ( EntityIdentifierCodes.INSURED_OR_SUBSCRIBER ) );
            subscriberName2010BALoop.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            subscriberName2010BALoop.NM104_NameFirst = "JANE"; //PayorSubscriber.LastName
            subscriberName2010BALoop.NM103_NameLastOrOrganizationName = "SMITH"; //PayorSubscriber.FirstName
            subscriberName2010BALoop.NM109_IdCode = "JS00111223333"; //PayorSubscriber.PayorCoverage.MemberNumber
            subscriberName2010BALoop.NM108_IdCodeQualifier = IdentificationCodes.MEMBER_IDENTIFICATION_NUMBER;

            //Loop 2010BA - Subscriber address
            var provider2010BA_N3Segment = subscriberName2010BALoop.AddSegment ( new TypedSegmentN3 () );
            provider2010BA_N3Segment.N301_AddressInformation = "234 SEAWAY ST"; //PayorSubscriber.Address.FirstStreetAddress & SecondStreetAddress

            //Loop 2010BA - Subscriber city,state,postal code.
            var provider2010BA_N4Segment = subscriberName2010BALoop.AddSegment ( new TypedSegmentN4 () );
            provider2010BA_N4Segment.N401_CityName = "MIAMI"; //PayorSubscriber.Address.CityName
            provider2010BA_N4Segment.N402_StateOrProvinceCode = "FL"; //PayorSubscriber.Address.StateProvince
            provider2010BA_N4Segment.N403_PostalCode = "33413"; //PayorSubscriber.Address.PostalCode

            //Loop 2010BA - Demographic information
            var subscriber_DMGSegment = subscriberName2010BALoop.AddSegment ( new TypedSegmentDMG () );
            subscriber_DMGSegment.DMG01_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            subscriber_DMGSegment.DMG02_DateOfBirth = DateTime.Parse ( "5/1/1943" ); //PayorSubscriber.BirthDate
            subscriber_DMGSegment.DMG03_Gender = Gender.Female; //PayorSubscriber.AdministrativeGender

            //Loop 2010BB - Payer Name.
            var subscriberName2010BBLoop = subscriber2000BHierarchicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCodes.PAYER ) );
            subscriberName2010BBLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            subscriberName2010BBLoop.NM103_NameLastOrOrganizationName = "KEY INSURANCE COMPANY"; //Payor.Name
            subscriberName2010BBLoop.NM108_IdCodeQualifier = IdentificationCodes.PAYOR_IDENTIFICATION;
            subscriberName2010BBLoop.NM109_IdCode = "999996666"; //NPI? Kate needs time to check.

            //optional.
            //var refSegment2 = subscriberName2010BBLoop.AddSegment(new TypedSegmentREF());
            //refSegment2.REF01_ReferenceIdQualifier = ReferenceIdentificationCodes.PROVIDER_COMMERCIAL_NUMBER;
            //refSegment2.REF02_ReferenceId = "KA6663";

            //autoIncrementedHierarchicalIDNumber++;
            //PATIENT HIERARCHICAL LEVEL Loop Repeat: >1
            //var patientDetailLoop2000CLoop = subscriber2000BHierarchicalLoop.AddHLoop(autoIncrementedHierarchicalIDNumber.ToString(CultureInfo.InvariantCulture), HierarchicalLevelCodes.DEPENDENT, PATIENT_HIERARCHICAL_LOOP_CAN_HOLD_CHILD_HIERARCHICAL_LOOPS);   // **** HL 3  ******

            //var HL3PATSegment = subscriber2000BHierarchicalLoop.AddSegment(new TypedSegmentPAT());
            //HL3PATSegment.PAT01_IndividualRelationshipCode = IndividualRelationshipCodes.CHILD;

            //How does the billing side access patient information on the clinical side.
            //var HL3NM1Segment = subscriber2000BHierarchicalLoop.AddLoop(new TypedLoopNM1(EntityIdentifierCodes.PATIENT));
            //HL3NM1Segment.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            //HL3NM1Segment.NM104_NameFirst = "TED"; //Patinet.FirstName
            //HL3NM1Segment.NM103_NameLastOrOrganizationName = "SMITH"; //Patinet.LastName

            //// add N3 and N4 segments under the above NM1 loop

            //var HL3NM1_N3_Segment = HL3NM1Segment.AddSegment(new TypedSegmentN3());
            //HL3NM1_N3_Segment.N301_AddressInformation = "236 N MAIN ST"; //Patient.Address.FirstStreetAddress & SecondStreetAddress

            //var HL3NM1_N4_Segment = HL3NM1Segment.AddSegment(new TypedSegmentN4());
            //HL3NM1_N4_Segment.N401_CityName = "MIAMI"; //Patient.Address.CityName
            //HL3NM1_N4_Segment.N402_StateOrProvinceCode = "FL"; //Patient.Address.StateProvince
            //HL3NM1_N4_Segment.N403_PostalCode = "33413"; //Patient.Address.PostalCode

            //var HL3NM1_DMG_Segment = HL3NM1Segment.AddSegment(new TypedSegmentDMG());
            //HL3NM1_DMG_Segment.DMG01_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            //HL3NM1_DMG_Segment.DMG02_DateOfBirth = Convert.ToDateTime("5/1/1973"); //Patinet.BirthDate
            //HL3NM1_DMG_Segment.DMG03_Gender = Gender.Male; //Patinet.AdministrativeGender

            var claim2300Loop = subscriber2000BHierarchicalLoop.AddLoop ( new TypedLoopCLM () );
            claim2300Loop.CLM01_PatientControlNumber = "26463774"; //BillingOffice.PlaceOfService.Claims[0].Key
            claim2300Loop.CLM02_TotalClaimChargeAmount = Convert.ToDecimal ( 100 ); //BillingOffice.PlaceOfService.Claims[0].ChargeAmount
            claim2300Loop.CLM05._1_FacilityCodeValue = PLACE_OF_SERVICE_CODE_OFFICE;
            claim2300Loop.CLM05._2_FacilityCodeQualifier = PLACE_OF_SERVICE_CODES_FOR_PROFESSIONAL_OR_DENTAL_SERVICES;
            claim2300Loop.CLM05._3_ClaimFrequencyTypeCode = CLAIM_FREQUENCY_TYPE_CODE_ORIGINAL;
            claim2300Loop.CLM06_ProviderOrSupplierSignatureIndicator = PROVIDER_OR_SUPPLIER_SIGNATURE_INDICATOR_CODE_YES;
            claim2300Loop.CLM07_ProviderAcceptAssignmentCode = PROVIDER_ACCEPTANCE_ASSIGNMENT_CODE_ASSIGNED;
            claim2300Loop.CLM08_BenefitsAssignmentCerficationIndicator = BENIFITS_ASSIGNMENT_CERTIFICATION_INDICATOR_CODE_YES;
            claim2300Loop.CLM09_ReleaseOfInformationCode = RELEASE_OF_INFORMATION_CODE_YES; //Kate says 'I don't think it matters too much'

            //Claim Identifier for Transmission Intermediaries is the new name for the Claim Identification Number for Clearinghouses 
            //and Other Transmission Intermediaries segment. The qualifier (REF01 = D9) did not change. ??? Do we use the intermediaries ? Kate says 'Not Yet'.
            //var refSegment = claim2300Loop.AddSegment(new TypedSegmentREF());
            //refSegment.REF01_ReferenceIdQualifier = "D9";
            //refSegment.REF02_ReferenceId = "17312345600006351";

            var hiSegment = claim2300Loop.AddSegment ( new TypedSegmentHI () );
            //Which value to use ?
            hiSegment.HI01_HealthCareCodeInformation =
                X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  INTERNATIONAL_CLASSIFICATION_OF_DISEASES_CLINICAL_MODIFICATION_ICD9CM_PRINCIPLE_DIAGNOSTICS, "0340" );
            //hiSegment.HI02_HealthCareCodeInformation = hiSegment.X12Utility.BuildCompositeElement("BF", "V7389"); - optional

            var lxLoop = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment = lxLoop.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "99213" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment.SV102_MonetaryAmount = "40"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].ChargeAmount
            sv1Segment.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT; //Constant.
            sv1Segment.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment = lxLoop.AddSegment ( new TypedSegmentDTP () );
            dtpSegment.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate = DateTime.ParseExact ( "20061003", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment.DTP03_Date = theDate;

            autoIncrementedServiceLineNumber++;
            var lxLoop2 = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop2.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment2 = lxLoop2.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment2.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "87070" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment2.SV102_MonetaryAmount = "15"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].ChargeAmount
            sv1Segment2.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT; //
            sv1Segment2.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment2.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment2 = lxLoop2.AddSegment ( new TypedSegmentDTP () );
            dtpSegment2.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment2.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate2 = DateTime.ParseExact ( "20061003", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment2.DTP03_Date = theDate2;

            autoIncrementedServiceLineNumber++;
            var lxLoop3 = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop3.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment3 = lxLoop3.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment3.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "99214" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment3.SV102_MonetaryAmount = "35";
            sv1Segment3.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT;
            sv1Segment3.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment3.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment3 = lxLoop3.AddSegment ( new TypedSegmentDTP () );
            dtpSegment3.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment3.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate3 = DateTime.ParseExact ( "20061010", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment3.DTP03_Date = theDate3;

            autoIncrementedServiceLineNumber++;
            var lxLoop4 = claim2300Loop.AddLoop ( new TypedLoopLX ( "LX" ) );
            lxLoop4.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

            var sv1Segment4 = lxLoop4.AddSegment ( new TypedSegmentSV1 () );
            sv1Segment4.SV101_CompositeMedicalProcedure = X12Utility.BuildCompositeElement ( x12Delimiters.CompositeDelimiter,  SERVICE_ID_QUALIFIER_CODE_HCPCS, "86663" );
                //HC for HCPCS - constant for now. BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].Procedure.
            sv1Segment4.SV102_MonetaryAmount = "10";
            sv1Segment4.SV103_UnitBasisMeasCode = UNIT_OR_BASIS_FOR_MEASUREMENT_CODE_UNIT;
            sv1Segment4.SV104_Quantity = "1"; //BillingOffice.PlaceOfService.Claims[0].ClaimLineItem[0].BillingUnitCount
            sv1Segment4.SV107_CompDiagCodePoint = "1"; //No functionality in REM. Default to 1.

            var dtpSegment4 = lxLoop4.AddSegment ( new TypedSegmentDTP () );
            dtpSegment4.DTP01_DateTimeQualifier = DATE_TIME_QUALIFIER_CODE_SERVICE;
            dtpSegment4.DTP02_DateTimePeriodFormatQualifier = DATE_TIME_PERIOD_FORMAT_QUALIFIER_DATE_EXPRESSED_IN_FORMAT_CCYYMMDD;
            DateTime theDate4 = DateTime.ParseExact ( "20061010", "yyyyMMdd", null ); //BillingOffice.PlaceOfService.Claims[0].ServiceDate
            dtpSegment4.DTP03_Date = theDate4;

            var x12 = message.SerializeToX12 ( true );
            var ediString = Pillar.Common.Utility.EmbeddedResourceUtil.GetEmbeddedResourceValue("Rem.Domain.Billing.Tests.TestData._837p.Example2_SelfInsured.txt", Assembly.GetExecutingAssembly());
            Assert.AreEqual(ediString, x12);
        }

        #endregion
    }
}
