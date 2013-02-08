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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Common.Extension;
using Pillar.Domain;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Billing.PayorModule;
using Rem.Domain.Core.CommonModule;
using Rem.WellKnownNames.AgencyModule;
using Rem.WellKnownNames.X12Codes;
using AdministrativeGender = Rem.WellKnownNames.CommonModule.AdministrativeGender;
using Gender = OopFactory.X12.Parsing.Model.Typed.Gender;
using PayorCoverageType = Rem.WellKnownNames.PatientAccountModule.PayorCoverageType;
using PayorSubscriberRelationshipType = Rem.WellKnownNames.PatientAccountModule.PayorSubscriberRelationshipType;

namespace Rem.Domain.Billing.ClaimModule
{
    /// <summary>
    ///   The Claim defines an entity that encapsulates the entire Health Care Claim information in a given 837 Professional document.
    /// </summary>
    public class ClaimBatch : AuditableAggregateRootBase
    {
        #region Constants

        /// <summary>
        /// X12 Element ID 127. Beginning of hierarchical transaction - Reference Identification (BHT03). This field is limited to 30 characters. 
        /// TODO: Value is '0' until we support batches. More information: BHT03 is the number assigned by the originator to identify the transaction 
        /// within the originator’s business application system. OR The inventory file number of the transmission assigned by the submitter’s system. 
        /// This number operates as a batch control number.
        /// </summary>
        private const int BatchControlNumber = 0;

        /// <summary>
        /// To define the business hierarchical structure of the transaction set and identify the business application purpose and reference data, 
        /// i.e., number, date, and time.
        /// </summary>
        private const string BeginningOfHierarchicalTransaction = "BHT";

        /// <summary>
        /// X12 Element ID 127. Beginning of hierarchical transaction - Reference Identification (BHT03). This field is limited to 30 characters. 
        /// TODO: Currently this is 'Mental Health' value from http://www.wpc-edi.com/codes/taxonomy, The Healthcare Provider Taxonomy code set 
        /// until we support it in the UI using a terminology service.
        /// </summary>
        private const string ProviderTaxonomyCode = "101YM0800X";

        /*
        /// <summary>
        /// X12 Element ID I65. The repetition separator is a delimiter and not a data element; this field provides the delimiter used to separate 
        /// repeated occurrences of a simple data element or a composite data structure; this value must be different than the data element separator, 
        /// component element separator, and the segment terminator.
        /// </summary>
        private const string RepetitionSeparator = "^";
         * */

        /// <summary>
        /// X12 Element ID 1328. A pointer to the diagnosis code in the order of importance to this service. 
        /// TODO: No functionality in REM. Default to 1 as per Kate.
        /// </summary>
        private const string DiagnosisCodePointer = "1";

        #endregion

        #region Fields

        private readonly IList<Claim> _claims;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimBatch"/> class.
        /// </summary>
        protected internal ClaimBatch ()
        {
            _claims = new List<Claim> ();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimBatch"/> class.
        /// </summary>
        /// <param name="chargeAmount">The charge amount.</param>
        /// <param name="payorType">Type of the payor.</param>
        protected internal ClaimBatch ( Money chargeAmount, PayorType payorType )
            : this ()
        {
            Check.IsNotNull ( chargeAmount, () => ChargeAmount );
            Check.IsNotNull ( payorType, () => PayorType );

            ChargeAmount = chargeAmount;
            PayorType = payorType;

            var lookupValueRepository = IoC.CurrentContainer.Resolve<ILookupValueRepository>();
            ClaimBatchStatus = lookupValueRepository.GetLookupByWellKnownName<ClaimBatchStatus>(WellKnownNames.ClaimModule.ClaimBatchStatus.Active);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the charge amount.
        /// </summary>
        [NotNull]
        public virtual Money ChargeAmount { get; private set; }

        /// <summary>
        ///   Gets the claims.
        /// </summary>
        public virtual IEnumerable<Claim> Claims
        {
            get { return _claims; }
            private set { }
        }


        /// <summary>
        /// Gets the type of the payor.
        /// </summary>
        /// <value>
        /// The type of the payor.
        /// </value>
        [NotNull]
        public virtual PayorType PayorType { get; private set; }


        /// <summary>
        /// Gets the claim batch status.
        /// </summary>
        public virtual ClaimBatchStatus ClaimBatchStatus { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Adds the claim.
        /// </summary>
        /// <param name="claim"> The claim. </param>
        public virtual void AddClaim(Claim claim)
        {
            Check.IsNotNull ( claim, "Claim is required." );
            claim.ReviseClaimBatch ( this );
            _claims.Add ( claim );
            NotifyItemAdded ( () => Claims, claim );
        }

        /// <summary>
        ///   Removes the claim.
        /// </summary>
        /// <param name="claim"> The claim. </param>
        public virtual void RemoveClaim(Claim claim)
        {
            Check.IsNotNull ( claim, "Claim is required." );
            _claims.Remove ( claim );
            NotifyItemRemoved ( () => Claims, claim );
        }

        /// <summary>
        ///   Revises the charge amount.
        /// </summary>
        /// <param name="chargeAmount"> The charge amount. </param>
        public virtual void ReviseChargeAmount(Money chargeAmount)
        {
            Check.IsNotNull ( chargeAmount, () => ChargeAmount );
            ChargeAmount = chargeAmount;
        }

        /// <summary>
        /// Revises the type of the payor.
        /// </summary>
        /// <param name="payorType">Type of the payor.</param>
        public virtual void RevisePayorType(PayorType payorType)
        {
            Check.IsNotNull ( payorType, () => PayorType );
            PayorType = payorType;
        }

        /// <summary>
        /// Revises the claim batch status.
        /// </summary>
        /// <param name="claimBatchStatus">The claim batch status.</param>
        public virtual void ReviseClaimBatchStatus ( ClaimBatchStatus claimBatchStatus)
        {
            ClaimBatchStatus = claimBatchStatus;
        }

        /// <summary>
        ///   Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns> A <see cref="System.String" /> that represents this instance. </returns>
        public override string ToString()
        {
            return string.Format("To {0} total {1} with {2} claim(s) created on {3:MM-dd-yy}", PayorType.Name, ChargeAmount, _claims.Count, CreatedTimestamp);
        }

        /// <summary>
        /// Generates the health care claim837 professional.
        /// </summary>
        /// <returns>A _healthCareClaim837Professional message.</returns>
        public virtual HealthCareClaim837Professional GenerateHealthCareClaim837Professional ( )
        {
            var healthCareClaim837ProfessionalFactory = IoC.CurrentContainer.Resolve<IHealthCareClaim837ProfessionalFactory> ();

            var currenDateTime = DateTime.Now;
            var autoIncrementedHierarchicalIDNumber = 1; // NOTE: The first HL01 within each ST-SE envelope must begin with "1".

            var interchangeCopntrolNumber = GetInterchangeControlNumber ( Key );

            // Interchange.
            var message = new Interchange (
                currenDateTime,
                interchangeCopntrolNumber,
                false, // TODO: Use web.config to set InterchangeUsageIndicator.
                PayorType.HealthCareClaim837Setup.X12Delimiters.SegmentDelimiter, 
                PayorType.HealthCareClaim837Setup.X12Delimiters.ElementDelimiter,
                PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter )
                {
                    InterchangeSenderIdQualifier = InterchangeIDQualifier.MutuallyDefined,
                    InterchangeSenderId = PayorType.HealthCareClaim837Setup.InterchangeSenderNumber,
                    InterchangeReceiverIdQualifier = InterchangeIDQualifier.MutuallyDefined,
                    InterchangeReceiverId = PayorType.HealthCareClaim837Setup.InterchangeReceiverNumber,
                };

            message.SetElement ( 11, PayorType.HealthCareClaim837Setup.X12Delimiters.RepetitionDelimiter.ToString ( CultureInfo.InvariantCulture ) );
            message.SetElement ( 12, InterchangeVersionControlNumber.ApprovedForPublicationByASCX12ProceduresReviewBoardThroughOctober2003 );
            message.SetElement ( 14, AcknowledgmentRequested.InterchangeAcknowledgmentRequested );

            // Functional group.
            var group = message.AddFunctionGroup ( FunctionalIdentifierCode.HealthCareClaim, currenDateTime, interchangeCopntrolNumber, ImplementationConventionReference.Version5010);

            group.ApplicationSendersCode = PayorType.HealthCareClaim837Setup.InterchangeSenderNumber;
            group.ApplicationReceiversCode = PayorType.HealthCareClaim837Setup.InterchangeReceiverNumber;
            group.ResponsibleAgencyCode = ResponsibleAgencyCode.AccreditedStandardsCommitteeX12;

            var interchangeControlNumberString = interchangeCopntrolNumber.ToString ( CultureInfo.InvariantCulture );
            var transactionControlIdentifier = interchangeControlNumberString.Length < 4
                                                   ? interchangeControlNumberString.PadLeft ( 4, '0' )
                                                   : interchangeControlNumberString;

            // Transaction set.
            var transaction = group.AddTransaction ( TransactionSetIdentifierCode.HealthCareClaim, transactionControlIdentifier );
            transaction.SetElement ( 3, ImplementationConventionReference.Version5010 );

            // Beginning of hierarchical transaction.
            var bhtSegment = transaction.AddSegment ( BeginningOfHierarchicalTransaction );
            bhtSegment.SetElement ( 1, HierarchicalStructureCode.InformationSourceOrSubscriberOrDependent );
            bhtSegment.SetElement ( 2, TransactionSetPurposeCode.Original );
            bhtSegment.SetElement ( 3, BatchControlNumber.ToString ( CultureInfo.InvariantCulture ) );
            bhtSegment.SetElement ( 4, currenDateTime.Date.ToString ( "yyyyMMdd" ) );
            bhtSegment.SetElement ( 5, currenDateTime.ToString ( "HHmm" ) );
            bhtSegment.SetElement ( 6, TransactionTypeCode.Chargeable );

            // Submitter name.
            var submitterLoop = transaction.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCode.Submitter ) );
            submitterLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            submitterLoop.NM103_NameLastOrOrganizationName = PayorType.BillingOffice.Agency.AgencyProfile.AgencyName.LegalName;
            submitterLoop.NM108_IdCodeQualifier = IdentificationCodeQualifier.ElectronicTransmitterIdentificationNumber;
            submitterLoop.NM109_IdCode = PayorType.BillingOffice.ElectronicTransmitterIdentificationNumber;

            // Submitter EDI contact information.
            var perSegment = submitterLoop.AddSegment ( new TypedSegmentPER () );
            perSegment.PER01_ContactFunctionCode = ContactFunctionCode.InfromationContact;

            // NOTE: PER02_Name is situational. But, as we have provided a nonperson entity for submitter, this field is required.
            perSegment.PER02_Name = PayorType.BillingOffice.AdministratorStaff.StaffProfile.StaffName.Complete;

            perSegment.PER03_CommunicationNumberQualifier = CommunicationNumberQualifer.Telephone;

            var staffPhone =
                PayorType.BillingOffice.AdministratorStaff.PhoneNumbers.FirstOrDefault (
                    phoneNumber => phoneNumber.StaffPhoneType.WellKnownName == StaffPhoneType.Work );
            if ( staffPhone == null || staffPhone.Phone == null )
            {
                throw new ArgumentException (
                    string.Format (
                        "Unable to find '{0}' staff work phone number",
                        PayorType.BillingOffice.AdministratorStaff.StaffProfile.StaffName.Complete ) );
            }

            perSegment.PER04_CommunicationNumber = staffPhone.Phone.PhoneNumber.RemoveNonAlphanumericChar();
            if ( !string.IsNullOrEmpty ( staffPhone.Phone.PhoneExtensionNumber ) )
            {
                perSegment.PER05_CommunicationNumberQualifier = CommunicationNumberQualifer.TelephoneExtension;
                perSegment.PER06_CommunicationNumber = staffPhone.Phone.PhoneExtensionNumber;
            }

            // Receiver name.
            var receiverLoop = transaction.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCode.Receiver ) );
            receiverLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            receiverLoop.NM103_NameLastOrOrganizationName = PayorType.Name;
            receiverLoop.NM108_IdCodeQualifier = IdentificationCodeQualifier.ElectronicTransmitterIdentificationNumber;
            receiverLoop.NM109_IdCode = PayorType.SubmitterIdentifier;

            // Billing provider hierarchical level. ***** HL1 *****
            var provider2000AHierachicalLoop = transaction.AddHLoop (
                autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                HierarchicalLevelCode.InformationSource,
                HierarchicalChildCode.AdditionalSubordinateHLDataSegmentInThisHierarchicalStructure );

            // Billing provide specialty information.
            var prvSegment = provider2000AHierachicalLoop.AddSegment ( new TypedSegmentPRV () );
            prvSegment.PRV01_ProviderCode = ProviderCode.Billing;
            prvSegment.PRV02_ReferenceIdQualifier = ReferenceIdentificationQualifier.HealthCareProviderTaxonomyCode;
            prvSegment.PRV03_ProviderTaxonomyCode = ProviderTaxonomyCode;

            // Billing provider Name
            var provider2010AaLoop = provider2000AHierachicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCode.BillingProvider ) );
            provider2010AaLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            provider2010AaLoop.NM103_NameLastOrOrganizationName = PayorType.BillingOffice.Agency.AgencyProfile.AgencyName.LegalName;
            provider2010AaLoop.NM108_IdCodeQualifier = IdentificationCodeQualifier.CentersForMedicareAndMedicaidServicesNationalProviderIdentifier;

            var agencyNpi =
                PayorType.BillingOffice.Agency.AgencyIdentifiers.FirstOrDefault (
                    identifier => identifier.AgencyIdentifierType.WellKnownName == AgencyIdentifierType.Npi );
            if ( agencyNpi == null || agencyNpi.IdentifierNumber == null )
            {
                throw new ArgumentException (
                    string.Format (
                        "Unable to find '{0}' agency NPI identifier number", PayorType.BillingOffice.Agency.AgencyProfile.AgencyName.LegalName ) );
            }

            provider2010AaLoop.NM109_IdCode = agencyNpi.IdentifierNumber;

            var agencyBillingAddressAndPhone =
                PayorType.BillingOffice.Agency.AddressesAndPhones.FirstOrDefault (
                    address => address.AgencyAddress.AgencyAddressType.WellKnownName == AgencyAddressType.Billing );
            if ( agencyBillingAddressAndPhone == null || agencyBillingAddressAndPhone.AgencyAddress == null )
            {
                throw new ArgumentException (
                    string.Format (
                        "Unable to find '{0}' agency Billing address", PayorType.BillingOffice.Agency.AgencyProfile.AgencyName.LegalName ) );
            }

            // Billing provider address.
            var provider2010AaN3Segment = provider2010AaLoop.AddSegment ( new TypedSegmentN3 () );
            provider2010AaN3Segment.N301_AddressInformation = agencyBillingAddressAndPhone.AgencyAddress.Address.FirstStreetAddress
                                                              +
                                                              ( string.IsNullOrWhiteSpace (
                                                                  agencyBillingAddressAndPhone.AgencyAddress.Address.SecondStreetAddress )
                                                                    ? string.Empty
                                                                    : " "
                                                                      + agencyBillingAddressAndPhone.AgencyAddress.Address.SecondStreetAddress );

            // Billing provider City,State,Zip code
            var provider2010AaN4Segment = provider2010AaLoop.AddSegment ( new TypedSegmentN4 () );
            provider2010AaN4Segment.N401_CityName = agencyBillingAddressAndPhone.AgencyAddress.Address.CityName;
            provider2010AaN4Segment.N402_StateOrProvinceCode = agencyBillingAddressAndPhone.AgencyAddress.Address.StateProvince.ShortName;
            provider2010AaN4Segment.N403_PostalCode = agencyBillingAddressAndPhone.AgencyAddress.Address.PostalCode.Code;

            var agencyFederalTaxID =
                PayorType.BillingOffice.Agency.AgencyIdentifiers.FirstOrDefault (
                    identifier => identifier.AgencyIdentifierType.WellKnownName == AgencyIdentifierType.FederalTaxId );
            if ( agencyFederalTaxID == null || agencyFederalTaxID.IdentifierNumber == null )
            {
                throw new ArgumentException (
                    string.Format (
                        "Unable to find '{0}' agency Federal Tax ID", PayorType.BillingOffice.Agency.AgencyProfile.AgencyName.LegalName ) );
            }

            // Billing provider - tax identification
            var provider2010AaRefSegment = provider2010AaLoop.AddSegment ( new TypedSegmentREF () );
            provider2010AaRefSegment.REF01_ReferenceIdQualifier = ReferenceIdentificationQualifier.EmployersIdentificationNumber;
            provider2010AaRefSegment.REF02_ReferenceId = agencyFederalTaxID.IdentifierNumber;

            //If it is the same as Billing provider, no need.
            /*
            var provider2010ABLoop = provider2000AHierachicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCode.PayToProvider ) );
            provider2010ABLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;

            // NOTE: Pay-To address is same as Billing provider address ie., same N3 and N4 segments. 
            provider2010ABLoop.AddSegment ( provider2010AAN3Segment );
            provider2010ABLoop.AddSegment ( provider2010AAN4Segment );
             */ 

            var claimDictionary = GroupClaimsByPrimaryPayorCoverage ( Claims );
           
            //TODO: needs confirm if this loop pulling the correct data *** need bill every insurance or just the primary???
            foreach (var keyValuePair in claimDictionary)
            {
                var payorCoverage = keyValuePair.Key;
                var claimList = keyValuePair.Value;

                var isPatientTheSubscriber =
                    payorCoverage.PayorSubscriber.PayorSubscriberRelationshipType.WellKnownName.Equals ( PayorSubscriberRelationshipType.Self );

                autoIncrementedHierarchicalIDNumber++;

                // Subscriber hierarchical level. **** HL 2  ******
                var subscriber2000BHierarchicalLoop =
                    provider2000AHierachicalLoop.AddHLoop (
                        autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                        HierarchicalLevelCode.Subscriber,
                        isPatientTheSubscriber
                            ? HierarchicalChildCode.NoSubordinateHLSegmentInThisHierarchicalStructure
                            : HierarchicalChildCode.AdditionalSubordinateHLDataSegmentInThisHierarchicalStructure );

                // Subscriber information. 
                var sbrSegment = subscriber2000BHierarchicalLoop.AddSegment ( new TypedSegmentSBR () );

                sbrSegment.SBR01_PayerResponsibilitySequenceNumberCode = payorCoverage.PayorCoverageType.ShortName;

                if ( isPatientTheSubscriber )
                {
                    sbrSegment.SBR02_IndividualRelationshipCode = payorCoverage.PayorSubscriber.PayorSubscriberRelationshipType.ShortName;
                }

                // Subscriber Name
                var subscriberName2010BaLoop =
                    subscriber2000BHierarchicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCode.InsuredOrSubscriber ) );
                subscriberName2010BaLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
                subscriberName2010BaLoop.NM104_NameFirst = payorCoverage.PayorSubscriber.Name.First;
                subscriberName2010BaLoop.NM103_NameLastOrOrganizationName = payorCoverage.PayorSubscriber.Name.Last;
                subscriberName2010BaLoop.NM108_IdCodeQualifier = IdentificationCodeQualifier.MemberIdentificationNumber;
                subscriberName2010BaLoop.NM109_IdCode = payorCoverage.MemberNumber;

                if ( isPatientTheSubscriber )
                {
                    // Subscriber address
                    var provider2010BaN3Segment = subscriberName2010BaLoop.AddSegment ( new TypedSegmentN3 () );
                    provider2010BaN3Segment.N301_AddressInformation = payorCoverage.PayorSubscriber.Address.FirstStreetAddress +
                                                                      ( string.IsNullOrWhiteSpace (
                                                                          payorCoverage.PayorSubscriber.Address.SecondStreetAddress )
                                                                            ? string.Empty
                                                                            : " "
                                                                              + payorCoverage.PayorSubscriber.Address.SecondStreetAddress );

                    // Subscriber city,state,postal code.
                    var provider2010BaN4Segment = subscriberName2010BaLoop.AddSegment ( new TypedSegmentN4 () );
                    provider2010BaN4Segment.N401_CityName = payorCoverage.PayorSubscriber.Address.CityName;
                    provider2010BaN4Segment.N402_StateOrProvinceCode = payorCoverage.PayorSubscriber.Address.StateProvince.ShortName;
                    provider2010BaN4Segment.N403_PostalCode = payorCoverage.PayorSubscriber.Address.PostalCode.Code;

                    // Demographic information
                    var subscriberDmgSegment = subscriberName2010BaLoop.AddSegment ( new TypedSegmentDMG () );
                    subscriberDmgSegment.DMG01_DateTimePeriodFormatQualifier = DateTimePeriodFormatQualifier.DateExpressedAsCCYYMMDD;
                    subscriberDmgSegment.DMG02_DateOfBirth = payorCoverage.PayorSubscriber.BirthDate;
                    subscriberDmgSegment.DMG03_Gender =
                        ConvertToGenderCodeFromAdministrativeGender ( payorCoverage.PayorSubscriber.AdministrativeGender );
                }

                // Loop 2010BB - Payer Name.
                var subscriberName2010BbLoop = subscriber2000BHierarchicalLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCode.Payer ) );
                subscriberName2010BbLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
                subscriberName2010BbLoop.NM103_NameLastOrOrganizationName = payorCoverage.Payor.Name;
                subscriberName2010BbLoop.NM108_IdCodeQualifier = IdentificationCodeQualifier.PayorIdentification;
                subscriberName2010BbLoop.NM109_IdCode = payorCoverage.Payor.ElectronicTransmitterIdentificationNumber;

                // TODO: Kate needs to get back on this.
                HierarchicalLoop parentHierarchicalLoop = subscriber2000BHierarchicalLoop;
                if ( !isPatientTheSubscriber )
                {
                    autoIncrementedHierarchicalIDNumber++;

                    // PATIENT HIERARCHICAL LEVEL Loop Repeat: >1  **** HL 3  ******
                    var patientDetailLoop2000CLoop =
                        subscriber2000BHierarchicalLoop.AddHLoop (
                            autoIncrementedHierarchicalIDNumber.ToString ( CultureInfo.InvariantCulture ),
                            HierarchicalLevelCode.Dependent,
                            HierarchicalChildCode.NoSubordinateHLSegmentInThisHierarchicalStructure );

                    parentHierarchicalLoop = patientDetailLoop2000CLoop;

                    var hl3PatSegment = patientDetailLoop2000CLoop.AddSegment ( new TypedSegmentPAT () );
                    hl3PatSegment.PAT01_IndividualRelationshipCode =
                        payorCoverage.PayorSubscriber.PayorSubscriberRelationshipType.ShortName;

                    var hl3Nm1Segment = patientDetailLoop2000CLoop.AddLoop ( new TypedLoopNM1 ( EntityIdentifierCode.Patient ) );
                    hl3Nm1Segment.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
                    hl3Nm1Segment.NM104_NameFirst = payorCoverage.PatientAccount.Name.First; //TODO: confirm it comes from primaryPayorCoverage.PatientAccount, not from Claim.PatientAccount, the later make more sense
                    hl3Nm1Segment.NM103_NameLastOrOrganizationName = payorCoverage.PatientAccount.Name.Last;

                    var hl3Nm1N3Segment = hl3Nm1Segment.AddSegment ( new TypedSegmentN3 () );
                    hl3Nm1N3Segment.N301_AddressInformation = payorCoverage.PatientAccount.HomeAddress.FirstStreetAddress
                                                                +
                                                                ( string.IsNullOrWhiteSpace (
                                                                    payorCoverage.PatientAccount.HomeAddress.SecondStreetAddress )
                                                                      ? string.Empty
                                                                      : " " + payorCoverage.PatientAccount.HomeAddress.SecondStreetAddress );

                    var hl3Nm1N4Segment = hl3Nm1Segment.AddSegment ( new TypedSegmentN4 () );
                    hl3Nm1N4Segment.N401_CityName = payorCoverage.PatientAccount.HomeAddress.CityName;
                    hl3Nm1N4Segment.N402_StateOrProvinceCode = payorCoverage.PatientAccount.HomeAddress.StateProvince.ShortName;
                    hl3Nm1N4Segment.N403_PostalCode = payorCoverage.PatientAccount.HomeAddress.PostalCode.Code;

                    var hl3Nm1DmgSegment = hl3Nm1Segment.AddSegment ( new TypedSegmentDMG () );
                    hl3Nm1DmgSegment.DMG01_DateTimePeriodFormatQualifier = DateTimePeriodFormatQualifier.DateExpressedAsCCYYMMDD;
                    hl3Nm1DmgSegment.DMG02_DateOfBirth = payorCoverage.PatientAccount.BirthDate;
                    hl3Nm1DmgSegment.DMG03_Gender =
                        ConvertToGenderCodeFromAdministrativeGender ( payorCoverage.PatientAccount.AdministrativeGender );
                }

                // 2300 Claim Information loop repeat 100
                // LX loop repeat 50 => total 5000 claims in a HCC837 message or a Subscriber HL? ( seems the later  make more sense)
                // TODO: confirm it with Kate and how to handle it if exceed the max loop repeat
                foreach ( var claim in claimList )
                {
                    var claim2300Loop = parentHierarchicalLoop.AddLoop ( new TypedLoopCLM () ); // Claim 2300 loop: repeat 100
                    claim2300Loop.CLM01_PatientControlNumber = claim.Key.ToString ( CultureInfo.InvariantCulture );
                    claim2300Loop.CLM02_TotalClaimChargeAmount = decimal.Parse ( X12Utility.ConvertToDecimalString ( claim.ChargeAmount.Amount ) );
                    claim2300Loop.CLM05._1_FacilityCodeValue = PlaceOfServiceCode.Office;
                    claim2300Loop.CLM05._2_FacilityCodeQualifier = FacilityCodeQualifier.PlaceOfServiceCodesForProfessionalOrDentalService;
                    claim2300Loop.CLM05._3_ClaimFrequencyTypeCode = ClaimFrequencyTypeCode.Original;
                    claim2300Loop.CLM06_ProviderOrSupplierSignatureIndicator = YesOrNoConditionResponseCode.YesIndicator;
                    claim2300Loop.CLM07_ProviderAcceptAssignmentCode = ProviderAcceptAssignementCode.Assigned;
                    claim2300Loop.CLM08_BenefitsAssignmentCerficationIndicator = YesOrNoConditionResponseCode.Yes;
                    claim2300Loop.CLM09_ReleaseOfInformationCode = ReleaseOfInformationCode.Yes;

                    // TODO: diagnosis code -- Diagnosis is associated with Claim Line Item
                    // List the principal Dx with BK and others with BF, like HI✽BK:8901✽BF:87200✽BF:5559~, up to 12 diagnosis
                    // assume the 1st Dx is the principal Dx
                    
                    var hiSegment = claim2300Loop.AddSegment ( new TypedSegmentHI () );
                    var index = 0;
                    foreach (var claimLineItem in claim.ClaimLineItems)
                    {
                        index++;
                        if (index == 1)
                        {
                            hiSegment.HI01_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement (
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMPrincipleDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode );
                        }
                        else if (index == 2)
                        {
                            hiSegment.HI02_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 3)
                        {
                            hiSegment.HI03_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 4)
                        {
                            hiSegment.HI04_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 5)
                        {
                            hiSegment.HI05_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 6)
                        {
                            hiSegment.HI06_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        } 
                        else if (index == 7)
                        {
                            hiSegment.HI07_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        } 
                        else if (index == 8)
                        {
                            hiSegment.HI08_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 9)
                        {
                            hiSegment.HI09_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 10)
                        {
                            hiSegment.HI10_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 11)
                        {
                            hiSegment.HI11_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                        else if (index == 12)
                        {
                            hiSegment.HI12_HealthCareCodeInformation =
                                X12Utility.BuildCompositeElement(
                                    PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                    CodeListQualifierCode.ICD9CMDiagnostics,
                                    claimLineItem.Diagnosis.CodedConceptCode);
                        }
                    }


                    var autoIncrementedServiceLineNumber = 0;
                    foreach ( var claimLineItem in claim.ClaimLineItems )
                    {
                        autoIncrementedServiceLineNumber++;

                        var lxLoop = claim2300Loop.AddLoop ( new TypedLoopLX ( string.Empty ) ); // LX loop repeat: 50
                        lxLoop.LX01_AssignedNumber = autoIncrementedServiceLineNumber.ToString ( CultureInfo.InvariantCulture );

                        var sv1Segment = lxLoop.AddSegment ( new TypedSegmentSV1 () );
                        sv1Segment.SV101_CompositeMedicalProcedure =
                            X12Utility.BuildCompositeElement (
                                PayorType.HealthCareClaim837Setup.X12Delimiters.CompositeDelimiter,
                                ProductOrServiceIDQualifier.HCPCS, //TODO: Confirm with Kate if we can use HCPCS for all procedures.
                                claimLineItem.Procedure.CodedConceptCode );

                        sv1Segment.SV102_MonetaryAmount = X12Utility.ConvertToDecimalString ( claimLineItem.ChargeAmount.Amount );
                        sv1Segment.SV103_UnitBasisMeasCode = UnitsOrBasisOfMeasurementCode.Unit;
                        sv1Segment.SV104_Quantity = claimLineItem.BillingUnitCount.Count.ToString ( CultureInfo.InvariantCulture );
                        sv1Segment.SV107_CompDiagCodePoint = DiagnosisCodePointer;

                        var dtpSegment = lxLoop.AddSegment ( new TypedSegmentDTP () );
                        dtpSegment.DTP01_DateTimeQualifier = DateTimeQualifier.Service;
                        dtpSegment.DTP02_DateTimePeriodFormatQualifier = DateTimePeriodFormatQualifier.DateExpressedAsCCYYMMDD;
                        dtpSegment.DTP03_Date = claim.ServiceDate;
                    }
                }
            }

            var healthCareClaim837ProfessionalX12Message = message.SerializeToX12 ( true ); //TODO: Configure the whitespace option.
            var healthCareClaim837Professional = healthCareClaim837ProfessionalFactory.CreateHealthCareClaim837Professional ( this, Encoding.ASCII.GetBytes ( healthCareClaim837ProfessionalX12Message ) );

            var lookupValueRepository = IoC.CurrentContainer.Resolve<ILookupValueRepository>();
            ReviseClaimBatchStatus ( lookupValueRepository.GetLookupByWellKnownName<ClaimBatchStatus>(WellKnownNames.ClaimModule.ClaimBatchStatus.Hcc837PGenerated) );

            return healthCareClaim837Professional;
        }

        #endregion


        #region Private Methods

        private static Dictionary<PayorCoverage, List<Claim>> GroupClaimsByPrimaryPayorCoverage(IEnumerable<Claim> claims)
        {
            var claimDictionary = new Dictionary<PayorCoverage, List<Claim>> ();
            foreach (var claim in claims)
            {
                // get the primary payor coverage
                var primaryPayorCoverage =
                    claim.PatientAccount.PayorCoverages.FirstOrDefault(p => p.PayorCoverageType.WellKnownName == PayorCoverageType.Primary);

                if (primaryPayorCoverage == null)
                {
                    throw new NotImplementedException( string.Format("No 'Primary' payor coverage type defined for patient account '{0}'. ", claim.PatientAccount.Name));
                }

                if (claimDictionary.ContainsKey(primaryPayorCoverage))
                {
                    claimDictionary[primaryPayorCoverage].Add ( claim );
                }
                else
                {
                    claimDictionary.Add ( primaryPayorCoverage, new List<Claim> { claim, } );
                }
            }
            return claimDictionary;
        }

        private static Gender ConvertToGenderCodeFromAdministrativeGender(Core.CommonModule.AdministrativeGender administrativeGender)
        {
            if (administrativeGender.WellKnownName == AdministrativeGender.Undifferentiated)
            {
                return Gender.Unknown;
            }
            if (administrativeGender.WellKnownName == AdministrativeGender.Male)
            {
                return Gender.Male;
            }
            if (administrativeGender.WellKnownName == AdministrativeGender.Female)
            {
                return Gender.Female;
            }
            throw new ArgumentException(
                string.Format("Unable to convert administrative gender '{0}' to X12 Gender Code.", administrativeGender));
        }

        private static int GetInterchangeControlNumber(long claimBatchKey)
        {
            if ( claimBatchKey <= 999999999 )
            {
                return unchecked( ( int )claimBatchKey );
            }

            var claimBatchKeyString = claimBatchKey.ToString ( CultureInfo.InvariantCulture );
            claimBatchKeyString = claimBatchKeyString.Substring ( claimBatchKeyString.Length - 9 );
            return int.Parse ( claimBatchKeyString );
        }

        #endregion
    }
}
