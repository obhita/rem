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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines the complete data set for a TEDS discharge record. 
    /// </summary>
    public class TedsDischargeRecord : AuditableAggregateRootBase
    {
        private readonly IList<TedsDischargeRecordSubstanceUsage> _substanceUsages;
        private string _completenessMessage;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeRecord"/> class.
        /// </summary>
        protected internal TedsDischargeRecord ()
        {
            _substanceUsages = new List<TedsDischargeRecordSubstanceUsage> ();
        }
        #endregion
        
        #region Properties

        /// <summary>
        /// Gets the teds discharge batch.
        /// </summary>
        public virtual TedsDischargeBatch TedsDischargeBatch { get; private set; }

        /// <summary>
        /// Gets the teds admission record.
        /// </summary>
        public virtual TedsAdmissionRecord TedsAdmissionRecord { get; private set; }

        /// <summary>
        /// Gets the teds discharge key fields.
        /// </summary>
        public virtual TedsDischargeKeyFields TedsDischargeKeyFields { get; private set; }

     
        // TODO: ProviderIdentifier and ClientIdetifier should be the same as the entry in AdmissionDataSet when client ID is unique within the state.
        // TODO: CoDependentIndicator:  96 Not Applicable – Use this code only for co-dependents/collateral clients.
        // TODO: Type of Service should be same as at Admission


        /// <summary>
        /// Gets the last face to face contact date.
        /// </summary>
        public virtual DateTime LastFaceToFaceContactDate { get; private set; }

        // TODO: DischargeDate must not be late than DischargeDate

       
        /// <summary>
        /// Gets the discharge reason.
        /// </summary>
        public virtual TedsAnswer<TedsDischargeReason> TedsDischargeReason { get; private set; }

        /// <summary>
        /// Gets the primary substance problem and frequency.
        /// </summary>
        [NoneCascading]
        public virtual TedsDischargeRecordSubstanceUsage PrimaryTedsDischargeRecordSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the secondary substance problem and frequency.
        /// </summary>
        [NoneCascading]
        public virtual TedsDischargeRecordSubstanceUsage SecondaryTedsDischargeRecordSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the tertiary substance problem and frequency.
        /// </summary>
        [NoneCascading]
        public virtual TedsDischargeRecordSubstanceUsage TertiaryTedsDischargeRecordSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the living arrangements.
        /// </summary>
        public virtual TedsAnswer<LivingArrangementsType> LivingArrangementsType { get; private set; }


        /// <summary>
        /// Gets the teds employment status information.
        /// </summary>
        public virtual TedsEmploymentStatusInformation TedsEmploymentStatusInformation { get; private set; }

        /// <summary>
        /// Gets the number of arrests in thirty days.
        /// </summary>
        public virtual TedsAnswer<int?> ArrestsInPastThirtyDaysCount { get; private set; }

        /// <summary>
        /// Gets the participated in self help group in past thirty days count.
        /// </summary>
        public virtual TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> ParticipatedSelfHelpGroupInPastThirtyDaysType { get; private set; }

        /// <summary>
        /// Gets the substance usages.
        /// </summary>
        public virtual IEnumerable<TedsDischargeRecordSubstanceUsage> SubstanceUsages
        {
            get { return _substanceUsages.ToList().AsReadOnly(); }
            private set { }
        }

        #endregion

        #region Public Mehtods

        /// <summary>
        /// Revises the teds discharge batch.
        /// </summary>
        /// <param name="tedsDischargeBatch">The teds discharge batch.</param>
        public virtual void ReviseTedsDischargeBatch(TedsDischargeBatch tedsDischargeBatch)
        {
            Check.IsNotNull(tedsDischargeBatch, () => TedsDischargeBatch);
            TedsDischargeBatch = tedsDischargeBatch;
        }

        /// <summary>
        /// Revises the teds admission record.
        /// </summary>
        /// <param name="tedsAdmissionRecord">The teds admission record.</param>
        public virtual void ReviseTedsAdmissionRecord (TedsAdmissionRecord tedsAdmissionRecord)
        {
            Check.IsNotNull(tedsAdmissionRecord, () => TedsAdmissionRecord);
            TedsAdmissionRecord = tedsAdmissionRecord;
        }

        /// <summary>
        /// Revises the teds discharge key fields.
        /// </summary>
        /// <param name="tedsDischargeKeyFields">The teds discharge key fields.</param>
        public virtual void ReviseTedsDischargeKeyFields(TedsDischargeKeyFields tedsDischargeKeyFields)
        {
            Check.IsNotNull(tedsDischargeKeyFields, () => TedsDischargeKeyFields);
            DomainRuleEngine.CreateRuleEngine ( tedsDischargeKeyFields )
                .WithContext ( tedsDischargeKeyFields )
                .Execute (
                    () =>
                        {
                            TedsDischargeKeyFields = tedsDischargeKeyFields;
                        } );
        }

        /// <summary>
        /// Revises the last face to face contact date.
        /// </summary>
        /// <param name="lastFaceToFaceContactDate">The last face to face contact date.</param>
        public virtual void ReviseLastFaceToFaceContactDate (DateTime lastFaceToFaceContactDate)
        {
            Check.IsNotNull(lastFaceToFaceContactDate, () => lastFaceToFaceContactDate);

            DomainRuleEngine.CreateRuleEngine(lastFaceToFaceContactDate)
                .WithContext(lastFaceToFaceContactDate)
                .Execute (
                    () =>
                        {
                            LastFaceToFaceContactDate = lastFaceToFaceContactDate;
                        } );
        }


        /// <summary>
        /// Revises the teds discharge reason.
        /// </summary>
        /// <param name="tedsDischargeReason">The teds discharge reason.</param>
        public virtual void ReviseTedsDischargeReason (TedsAnswer<TedsDischargeReason> tedsDischargeReason)
        {
            //CheckIfTedsAnswerHasInvalidNonResponse(tedsDischargeReason, () => TedsDischargeReason, "Discharge reason");
            TedsDischargeReason = tedsDischargeReason;
        }

        /// <summary>
        /// Revises the type of the living arrangements.
        /// </summary>
        /// <param name="livingArrangementsType">Type of the living arrangements.</param>
        public virtual void ReviseLivingArrangementsType( TedsAnswer<LivingArrangementsType>  livingArrangementsType)
        {
            //CheckIfTedsAnswerHasInvalidNonResponse(livingArrangementsType, () => LivingArrangementsType, "Living arrangement type");
            LivingArrangementsType = livingArrangementsType;
        }

        /// <summary>
        /// Revises the teds employment status information.
        /// </summary>
        /// <param name="tedsEmploymentStatusInformation">The teds employment status information.</param>
        public virtual void ReviseTedsEmploymentStatusInformation(TedsEmploymentStatusInformation tedsEmploymentStatusInformation)
        {
            TedsEmploymentStatusInformation = tedsEmploymentStatusInformation;
        }

        /// <summary>
        /// Revises the arrests in past thirty days count.
        /// </summary>
        /// <param name="arrestsInPastThirtyDaysCount">The arrests in past thirty days count.</param>
        public virtual void ReviseArrestsInPastThirtyDaysCount(TedsAnswer<int?> arrestsInPastThirtyDaysCount)
        {
            ArrestsInPastThirtyDaysCount = arrestsInPastThirtyDaysCount;
        }

        /// <summary>
        /// Revises the type of the participated self help group in past thirty days.
        /// </summary>
        /// <param name="participatedSelfHelpGroupInPastThirtyDaysType">Type of the participated self help group in past thirty days.</param>
        public virtual void ReviseParticipatedSelfHelpGroupInPastThirtyDaysType(TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> participatedSelfHelpGroupInPastThirtyDaysType)
        {
            //CheckIfTedsAnswerHasInvalidNonResponse(frequencyOfAttendanceAtSelfHelpProgramsType, () => ParticipatedSelfHelpGroupInPastThirtyDaysType, "Frequency of attendance at self help programs");
            ParticipatedSelfHelpGroupInPastThirtyDaysType = participatedSelfHelpGroupInPastThirtyDaysType;
        }

        /// <summary>
        /// Determines whether this instance is complete, which means all required fields are populated.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsComplete()
        {
            var msgBuilder = new StringBuilder ();
            var result = true;

            if (TedsAdmissionRecord == null)
            {
                result = false;
                msgBuilder.Append("Teds Admission Record is required.");
                msgBuilder.Append(Environment.NewLine);
            }

            if (TedsDischargeKeyFields == null)
            {
                result = false;
                msgBuilder.Append ( "All TEDS Discharge Interview Key fields are required." );
                msgBuilder.Append ( Environment.NewLine );
            }
          
            if (LastFaceToFaceContactDate == (DateTime)typeof(DateTime).GetDefault())
            {
                result = false;
                msgBuilder.Append("Last Contact Date is required.");
                msgBuilder.Append ( Environment.NewLine );
            }

            if (TedsDischargeReason == null)
            {
                result = false;
                msgBuilder.Append("Teds Discharge Reason is required.");
                msgBuilder.Append ( Environment.NewLine );
            }

            if (ParticipatedSelfHelpGroupInPastThirtyDaysType == null)
            {
                result = false;
                msgBuilder.Append("Participated Self-Help Group In Past Thirty Days Type is required.");
                msgBuilder.Append(Environment.NewLine);
            }

            _completenessMessage = msgBuilder.ToString ();
            return result;
        }

        /// <summary>
        /// Generates the discharge record.
        /// </summary>
        /// <returns>A string.</returns>
        public virtual string GenerateDischargeRecord()
        {
            // Included in both the admission and the discharge data sets are several key fields. The key fields
            // combine to form a unique identifier (retrieval key) for the record in the TEDS discharge
            // database. Any discharge record submitted to TEDS that matches a record already in the TEDS
            // database on all the discharge key fields is rejected as a duplicate.
            // Discharge Data Key fields are: 
            // State Code
            // Provider Identifier
            // Client Identifier
            // Co-dependent/Collateral code
            // Type of Service at Discharge
            // Date of Discharge


            if (!IsComplete())
            {
                return "Cannot generate discharge record due to the incompleteness of the interview.\r\n " + _completenessMessage;
            }

            var recordBuilder = new StringBuilder();
          
            // *** 1. DATA SUBMISSION INFORMATION
            // i.e. System Data Set (SDS) - 3 processing control data items that are reported by all States.

            // DIS 1 - SYSTEM TRANSACTION TYPE
            // Valid Entries: A, C, D
            // An invalid entry in this field is automatically changed to "A."
            var dis1 = TedsDischargeKeyFields.SystemDataSet.SystemTransactionType.Code;
            recordBuilder.Append ( dis1  );

            // DIS 2 - STATE CODE - (KEY FIELD)
            // Valid Entries: The valid FIPS two-letter state code for the submitting State.
            // An invalid entry in this field automatically causes record to fail.
            var dis2 = TedsDischargeKeyFields.SystemDataSet.StateProvince;
            recordBuilder.Append ( dis2 );

            // DIS 3 - REPORTING DATE
            // Valid Entries: MMYYYY
            // Every record in a state submission must contain the same date of submission.
            var dis3 = string.Format("{0:MMyyyy}", TedsDischargeKeyFields.SystemDataSet.ReportingDate);
            recordBuilder.Append ( dis3 );

            // *** 2. BASIC TEDS DISCHARGE DATA
            // Data in fields 4 through 10.

            // DIS 4 - PROVIDER IDENTIFIER - (KEY FIELD)
            // Valid Entries: Entry must contain a valid provider ID that matches the State ID or the I-SATS ID in SAMHSA's I-SATS.
            // If this field is blank, the record will not be processed.
            // Field Length: 15
            // Data Type: Alphanumeric (Left-justified and filled with blank spaces)
            var dis4 = string.Format("{0,-15}", TedsDischargeKeyFields.ProviderIdentifier.IdentifierValue);
            recordBuilder.Append ( dis4 );

            // DIS 5 - CLIENT IDENTIFIER - (KEY FIELD)
            // Valid entries: An identifier of from 1 to 15 alphanumeric characters that is unique within the
            // state for NOMS participation and for states not participating in NOMS must, at a
            // minimum, be unique within the provider. If the field is blank, the record will not 
            // be processed.
            // Field Length: 15
            // Data Type: Alphanumeric (Left-justified and filled with blank spaces)
            var dis5 = string.Format("{0,-15}", TedsDischargeKeyFields.ClientIdentifier.IdentifierValue);
            recordBuilder.Append ( dis5 );

            // DIS 6 - CO-DEPENDENT/COLLATERAL - (KEY FIELD)
            // Valid Entries: 
            // 1 Yes 
            // 2 No
            var dis6 = "2";
            if (TedsDischargeKeyFields.CoDependentIndicator)
            {
                dis6 = "1";
            }
            recordBuilder.Append ( dis6 );

            // DIS 7 - TYPE OF SERVICE AT DISCHARGE (KEY FIELD)
            // Field Length: 2
            // Data Type: Numeric
            var dis7 = TedsDischargeKeyFields.TedsServiceType.Response.Code;
            recordBuilder.Append ( dis7 );

            // DIS 8 - DATE OF LAST CONTACT
            // Valid Entries: MMDDYYYY
            var dis8 = string.Format("{0:MMddyyyy}", LastFaceToFaceContactDate);            
            recordBuilder.Append ( dis8 );

            // DIS 9 - DATE OF DISCHARGE - (KEY FIELD)
            // Valid Entries: MMDDYYYY
            var dis9 = string.Format("{0:MMddyyyy}", TedsDischargeKeyFields.DischargeDate);
            recordBuilder.Append ( dis9 );

            // DIS 10 - REASON FOR DISCHARGE, TRANSFER, OR DISCONTINUANCE OF TREATMENT
            // Field Length: 2
            var dis10 = string.Format ( "{0, 2}", string.Empty );
            if ( TedsDischargeReason.HasResponse)
            {
                dis10 = TedsDischargeReason.Response.Code;
            }
            else if ( TedsDischargeReason.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown )
            {
                dis10 = "08";
            }
            else
            {
                throw new InvalidDataException ( string.Format ( "Invalid Teds Discharge reason '{0}' for Teds Discharge interview '{1}'.", TedsDischargeReason.TedsNonResponse.Name, Key ) );
            }
            recordBuilder.Append ( dis10 );

            // *** 3. ADMISSIONS DATA REPORTED ON DISCHARGE RECORD
            // The data in fields 11 through 20 are for the admission (or transfer) that corresponds to
            // the discharge reported in the record. The data in these fields should match exactly with
            // the data reported in the corresponding TEDS admission (or transfer) record so that the
            // discharge record can be matched with the appropriate admission (or transfer) record in
            // the TEDS system.

            // DIS 11 - PROVIDER IDENTIFIER AT ADMISSION
            // This number will usually be the same as the entry in DIS 4 (provider ID at discharge), but may be different.
            var dis11 = string.Format("{0,-15}", TedsAdmissionRecord.TedsAdmissionKeyFields.ProviderIdentifier.IdentifierValue);
            recordBuilder.Append ( dis11 );

            // DIS 12 - CLIENT IDENTIFIER AT ADMISSION
            // This number should be the same as the entry in DIS.5 (client ID at discharge) when client ID is unique within the state.
            var dis12 = dis5;
            recordBuilder.Append ( dis12 );

            // DIS 13 - CO-DEPENDENT/COLLATERAL (from admission record)
            // A Co-Dependent/Collateral is defined in item DIS 6 above.
            var dis13 = dis6;
            recordBuilder.Append ( dis13 );

            // DIS 14 - CLIENT TRANSACTION TYPE (from admission record)
            // This field identifies whether the Admission record is for an initial admission (A)
            // or a Transfer/change in service (T).
            // Valid entries: (Admission Record)
            // A Admission
            // T Transfer
            var dis14 = TedsAdmissionRecord.TedsAdmissionKeyFields.ClientTransactionType.Code;
            recordBuilder.Append ( dis14 );

            // DIS 15 - DATE OF ADMISSION (from admission record)
            // Valid entries: MMDDYYYY
            var dis15 = string.Format("{0:MMddyyyy}", TedsAdmissionRecord.TedsAdmissionKeyFields.AdmissionDate);
            recordBuilder.Append ( dis15 );

            // DIS 16 - TYPE OF SERVICE AT ADMISSION
            var dis16 = TedsAdmissionRecord.TedsAdmissionKeyFields.TedsServiceType.Response.Code;
            recordBuilder.Append ( dis16 );

            // DIS 17 - DATE OF BIRTH
            // Valid entries: MMDDYYYY
            var dis17 = string.Format("{0:MMddyyyy}", TedsAdmissionRecord.BirthDate);
            recordBuilder.Append ( dis17 );

            // DIS 18 - SEX
            var dis18 = "8";
            if (TedsAdmissionRecord.TedsGenderInformation != null)
            {
                if (TedsAdmissionRecord.TedsGenderInformation.TedsGender.HasResponse)
                {
                    dis18 = TedsAdmissionRecord.TedsGenderInformation.TedsGender.Response.Code;
                }
                else if (TedsAdmissionRecord.TedsGenderInformation.TedsGender.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    dis18 = "7";
                }
            }
            recordBuilder.Append ( dis18 );

            // DIS 19 - RACE
            var dis19 = "98";
            if (TedsAdmissionRecord.TedsRace.HasResponse)
            {
                dis19 = TedsAdmissionRecord.TedsRace.Response.Code;
            }
            else if (TedsAdmissionRecord.TedsRace.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            {
                dis19 = "97";
            }
            recordBuilder.Append ( dis19 );

            // DIS 20 - ETHNICITY
            var dis20 = "98";
            if (TedsAdmissionRecord.TedsEthnicity.HasResponse)
            {
                dis20 = TedsAdmissionRecord.TedsEthnicity.Response.Code;
            }
            else if (TedsAdmissionRecord.TedsEthnicity.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            {
                dis20 = "97";
            }
            recordBuilder.Append ( dis20 );

            // **** 4. DISCHARGE DATA NOMS ELEMENTS
            // The data in fields 21-30 are the NOMS data elements for the discharge data set.

            // DIS 21(a) - SUBSTANCE PROBLEM AT DISCHARGE, PRIMARY
            recordBuilder.Append(SubstanceProblemAndFrequency.GetSubstanceProblemTypeCode(PrimaryTedsDischargeRecordSubstanceUsage.SubstanceProblemAndFrequency));

            // DIS 21(b) - SUBSTANCE PROBLEM AT DISCHARGE, SECONDARY
            recordBuilder.Append(SubstanceProblemAndFrequency.GetSubstanceProblemTypeCode(SecondaryTedsDischargeRecordSubstanceUsage.SubstanceProblemAndFrequency));

            // DIS 21(c) - SUBSTANCE PROBLEM AT DISCHARGE, TERTIARY
            recordBuilder.Append(SubstanceProblemAndFrequency.GetSubstanceProblemTypeCode(TertiaryTedsDischargeRecordSubstanceUsage.SubstanceProblemAndFrequency));

            // DIS 22(a) - FREQUENCY OF USE AT DISCHARGE, PRIMARY
            recordBuilder.Append(SubstanceProblemAndFrequency.GetSubstanceUseFrequencyTypeCode(PrimaryTedsDischargeRecordSubstanceUsage.SubstanceProblemAndFrequency));

            // DIS 22(b) - FREQUENCY OF USE AT DISCHARGE, SECONDARY
            recordBuilder.Append(SubstanceProblemAndFrequency.GetSubstanceUseFrequencyTypeCode(SecondaryTedsDischargeRecordSubstanceUsage.SubstanceProblemAndFrequency));

            // DIS 22(c) - FREQUENCY OF USE AT DISCHARGE, TERTIARY
            recordBuilder.Append(SubstanceProblemAndFrequency.GetSubstanceUseFrequencyTypeCode(TertiaryTedsDischargeRecordSubstanceUsage.SubstanceProblemAndFrequency));

            // DIS 23 - LIVING ARRANGEMENTS AT DISCHARGE
            var dis23 = "98";
            if (LivingArrangementsType.HasResponse)
            {
                dis23 = LivingArrangementsType.Response.Code;
            }
            else if (LivingArrangementsType.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            {
                dis23 = "97";
            }
            recordBuilder.Append ( dis23 );

            // DIS 24 - EMPLOYMENT STATUS AT DISCHARGE
            var dis24 = "98";
            if ( TedsEmploymentStatusInformation.TedsEmploymentStatus.HasResponse)
            {
                dis24 = TedsEmploymentStatusInformation.TedsEmploymentStatus.Response.Code;
            }
            else if (TedsEmploymentStatusInformation.TedsEmploymentStatus.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            {
                dis24 = "97";
            }
            recordBuilder.Append ( dis24 );

            // DIS 25 - DETAILED NOT IN LABOR FORCE AT DISCHARGE
            var dis25 ="98";
            if (TedsEmploymentStatusInformation.DetailedNotInLaborForce.HasResponse)
            {
                dis25 = TedsEmploymentStatusInformation.DetailedNotInLaborForce.Response.Code;
            }
            else if (TedsEmploymentStatusInformation.DetailedNotInLaborForce.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            {
                dis25 = "97";
            }
            else if (TedsEmploymentStatusInformation.DetailedNotInLaborForce.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
            {
                dis25 = "96";
            }
            recordBuilder.Append ( dis25 );

            // DIS 26 - NUMBER OF ARRESTS IN 30 DAYS PRIOR TO DISCHARGE
            // Valid entries: 00-96 Number of arrests
            // 97 Unknown
            // 98 Not Collected
            var dis26 = "98";
            if (ArrestsInPastThirtyDaysCount.HasResponse)
            {
                Debug.Assert ( ArrestsInPastThirtyDaysCount.Response != null, "ArrestsInThirtyDaysNumber.Response != null" );
                dis26 = string.Format ( "{0,2}", ArrestsInPastThirtyDaysCount.Response.Value);
            }
            else if (ArrestsInPastThirtyDaysCount.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            {
                dis26 = "97";
            }
            recordBuilder.Append ( dis26 );

            // DIS 27 - FREQUENCY OF ATTENDANCE AT SELF-HELP PROGRAMS DIS 27 (e.g., AA, NA, etc.) IN 30 DAYS PRIOR TO DISCHARGE
            // Field: 31
            // Begin Column: 136
            // End Column: 137
            var dis27 = string.Format ( "{0,2}", string.Empty );
            if (ParticipatedSelfHelpGroupInPastThirtyDaysType.HasResponse)
            {
                dis27 = ParticipatedSelfHelpGroupInPastThirtyDaysType.Response.Code;
            }
            else if (ArrestsInPastThirtyDaysCount.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            {
                dis27 = "97";
            }
            recordBuilder.Append ( dis27 );
             

            return recordBuilder.ToString();
        }
        
        #endregion
    }
}
