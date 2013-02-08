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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// TedsAdmissionRecord defines TEDS Admission data with national outcomes measures (NOMS).
    /// </summary>
    /// <remarks>
    /// TEDS stands for Treatment Episode Data Set.
    /// </remarks>
    public class TedsAdmissionRecord : AuditableAggregateRootBase
    {
        private readonly IList<TedsAdmissionRecordSubstanceUsage> _substanceUsages;

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionRecord"/> class.
        /// </summary>
        protected internal TedsAdmissionRecord()
        {
            _substanceUsages = new List<TedsAdmissionRecordSubstanceUsage> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionRecord"/> class.
        /// </summary>
        /// <param name="tedsAdmissionBatch">The teds admission batch.</param>
        protected internal TedsAdmissionRecord(TedsAdmissionBatch tedsAdmissionBatch)
        {
            Check.IsNotNull(tedsAdmissionBatch, () => this.TedsAdmissionBatch);
            _substanceUsages = new List<TedsAdmissionRecordSubstanceUsage>();
        }

        // All fields from the Transfer (T) record should be updated to reflect values at the
        // time of transfer except the following fields, which must have the same values as
        // in the associated (preceding) admission record (Client Transaction Type == A):
        // • Client ID (MDS 2)
        // • Co-Dependent/Collateral (MDS 3)
        // • Date of Birth (MDS 8)
        // • Sex (MDS 9)
        // • Race (MDS 10)
        // • Ethnicity (MDS 11)

        /// <summary>
        /// Gets the teds admission interview batch.
        /// </summary>
        public virtual TedsAdmissionBatch TedsAdmissionBatch { get; private set; }

        /// <summary>
        /// Gets the teds admission key fields.
        /// SDS 1 - 3
        /// MDS 1 - 5, MDS 18
        /// </summary>
        public virtual TedsAdmissionKeyFields TedsAdmissionKeyFields { get; private set; }

        /// <summary>
        /// Gets the count of prior treatment episodes.
        /// MDS 6
        /// </summary>
        public virtual TedsAnswer<int?> PriorTreatmentEpisodesCount { get; private set; }

        /// <summary>
        /// Gets the principal source of referral information.
        /// MDS 7, SuDs 13
        /// </summary>
        public virtual PrincipalReferralSourceInformation PrincipalReferralSourceInformation { get; private set; }

        /// <summary>
        /// Gets the birth date answer.
        /// MDS 8
        /// </summary>
        public virtual DateTime? BirthDate { get; private set; }

        /// <summary>
        /// Gets the teds gender information.
        /// MDS 9, SuDs 6
        /// </summary>
        public virtual TedsGenderInformation TedsGenderInformation { get; private set; }

        /// <summary>
        /// Gets the race answer.
        /// MDS 10
        /// </summary>
        public virtual TedsAnswer<TedsRace> TedsRace { get; private set; }

        /// <summary>
        /// Gets the teds ethnicity answer.
        /// MDS 11
        /// </summary>
        public virtual TedsAnswer<TedsEthnicity> TedsEthnicity { get; private set; }

        /// <summary>
        /// Gets the teds education answer.
        /// MDS 12
        /// </summary>
        public virtual TedsAnswer<int?> TedsEducationYearCount { get; private set; }

        /// <summary>
        /// Gets the teds employment information.
        /// MDS 13, SuDs 12
        /// </summary>
        public virtual TedsEmploymentStatusInformation TedsEmploymentStatusInformation { get; private set; }

        /// <summary>
        /// Gets the primary substance usage at admission.
        /// MDS 14 (A), MDS 15 (A), MDS 16 (A), MDS 17 (A), SUDS 1
        /// </summary>
        [NoneCascading]
        public virtual TedsAdmissionRecordSubstanceUsage PrimaryTedsAdmissionRecordSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the secondary substance usage at admission.
        /// MDS 14 (B), MDS 15 (B), MDS 16 (B), MDS 17 (B), SUDS 2
        /// </summary>
        [NoneCascading]
        public virtual TedsAdmissionRecordSubstanceUsage SecondaryTedsAdmissionRecordSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the tertiary substance usage at admission.
        /// MDS 14 (C), MDS 15 (C), MDS 16 (C), MDS 17 (C), SUDS 3
        /// </summary>
        [NoneCascading]
        public virtual TedsAdmissionRecordSubstanceUsage TertiaryTedsAdmissionRecordSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the medication assisted opioid therapy answer.
        /// MDS 19
        /// </summary>
        public virtual TedsAnswer<bool?> MedicationAssistedOpioidTherapyIndicator { get; private set; }

        /// <summary>
        /// Gets the DSM diagnosis answer.
        /// SUDS 4
        /// </summary>
        public virtual TedsAnswer<DsmDiagnosisResponse> DsmDiagnosis { get; private set; }

        /// <summary>
        /// Gets the other psychiatric problem answer.
        /// SUDS 5
        /// </summary>
        public virtual TedsAnswer<bool?> OtherPsychiatricProblemIndicator { get; private set; }

        /// <summary>
        /// Gets the veteran status answer.
        /// SUDS 7
        /// </summary>
        public virtual TedsAnswer<bool?> VeteranStatusIndicator { get; private set; }

        /// <summary>
        /// Gets the living arrangements answer.
        /// SUDS 8
        /// </summary>
        public virtual TedsAnswer<LivingArrangementsType> LivingArrangementsType { get; private set; }

        /// <summary>
        /// Gets the source of income answer.
        /// SUDS 9
        /// </summary>
        public virtual TedsAnswer<IncomeSourceType> IncomeSourceType { get; private set; }

        /// <summary>
        /// Gets the health insurance answer.
        /// SUDS 10
        /// </summary>
        public virtual TedsAnswer<HealthInsuranceType> HealthInsuranceType { get; private set; }

        /// <summary>
        /// Gets the primary source of payment answer.
        /// SUDS 11
        /// </summary>
        public virtual TedsAnswer<PrimaryPaymentSourceType> PrimaryPaymentSourceType { get; private set; }

        /// <summary>
        /// Gets the martial status answer.
        /// SUDS 14
        /// </summary>
        public virtual TedsAnswer<MaritalStatus> MaritalStatus { get; private set; }

        /// <summary>
        /// Gets the days waiting to enter treatment answer.
        /// SUDS 15
        /// </summary>
        public virtual TedsAnswer<int?> DaysWaitingToEnterTreatmentCount { get; private set; }

        /// <summary>
        /// Gets the count of arrests in thirty days.
        /// SUDS 16
        /// </summary>
        public virtual TedsAnswer<int?> ArrestsInThirtyDaysCount { get; private set; }

        /// <summary>
        /// Gets the frequency of attendance at self help programs answer.
        /// SUDS 17
        /// </summary>
        public virtual TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> ParticipatedSelfHelpGroupInPastThirtyDaysType { get; private set; }

        /// <summary>
        /// Gets the substance usages.
        /// </summary>
        public virtual IEnumerable<TedsAdmissionRecordSubstanceUsage> SubstanceUsages
        {
            get { return _substanceUsages.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Revises the system data set.
        /// </summary>
        /// <param name="tedsAdmissionKeyFields">The teds admission key fields.</param>
        public virtual void ReviseTedsAdmissionKeyFields(TedsAdmissionKeyFields tedsAdmissionKeyFields)
        {
            Check.IsNotNull(tedsAdmissionKeyFields, () => TedsAdmissionKeyFields);
            TedsAdmissionKeyFields = tedsAdmissionKeyFields;
        }

        /// <summary>
        /// Revises the prior treatment episodes count.
        /// </summary>
        /// <param name="priorTreatmentEpisodesCount">The prior treatment episodes count.</param>
        public virtual void RevisePriorTreatmentEpisodesCount(TedsAnswer<int?> priorTreatmentEpisodesCount)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(priorTreatmentEpisodesCount, () => this.PriorTreatmentEpisodesCount, "Number of prior treatment episodes");

            PriorTreatmentEpisodesCount = priorTreatmentEpisodesCount;
        }

        /// <summary>
        /// Revises the type of the principal source of referral.
        /// </summary>
        /// <param name="principalReferralSourceInformation">The principal source of referral.</param>
        public virtual void RevisePrincipalSourceOfReferral(PrincipalReferralSourceInformation principalReferralSourceInformation)
        {
            PrincipalReferralSourceInformation = principalReferralSourceInformation;
        }

        /// <summary>
        /// Revises the teds gender information.
        /// </summary>
        /// <param name="tedsGenderInformation">The teds gender information.</param>
        public virtual void ReviseTedsGenderInformation(TedsGenderInformation tedsGenderInformation)
        {
            TedsGenderInformation = tedsGenderInformation;
        }

        //TODO: BirthDate and SubstanceUsageAtAdmission.FirstUseAge should be updated together.

        /// <summary>
        /// Revises the birth date.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        public virtual void ReviseBirthDate(DateTime? birthDate)
        {
            if (birthDate != null)
            {
                // Cross Check Age of First Use with Patient Birth Date
                foreach ( var TedsAdmissionRecordSubstanceUsage in SubstanceUsages )
                {
                    if ( TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge != null
                         && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge.HasResponse
                         && BirthDate.HasValue )
                    {
                        var patientAge = BirthDate.Value.GetAge ();
                        if ( TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge.Response.Value > patientAge )
                        {
                            throw new ArgumentException ( "Age of First Use is greater than the client's age." );
                        }
                    }
                }
            }

            DomainRuleEngine.CreateRuleEngine<TedsAdmissionRecord, DateTime?>(this, () => ReviseBirthDate)
                .WithContext(birthDate)
                .Execute(
                    () =>
                        {
                            BirthDate = birthDate;
                        });
        }

        /// <summary>
        /// Revises the teds race.
        /// </summary>
        /// <param name="tedsRace">The teds race.</param>
        public virtual void ReviseTedsRace(TedsAnswer<TedsRace> tedsRace)
        {
            CheckIfTedsAnswerHasInvalidNonResponse ( tedsRace, () => this.TedsRace, "race" );

            TedsRace = tedsRace;
        }

        /// <summary>
        /// Revises the teds ethnicity.
        /// </summary>
        /// <param name="tedsEthnicity">The teds ethnicity.</param>
        public virtual void ReviseTedsEthnicity(TedsAnswer<TedsEthnicity> tedsEthnicity)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(tedsEthnicity, () => this.TedsEthnicity, "Ethnicity");

            TedsEthnicity = tedsEthnicity;
        }

        /// <summary>
        /// Revises the teds education year count.
        /// </summary>
        /// <param name="tedsEducationYearCount">The teds education year count.</param>
        public virtual void ReviseTedsEducationYearCount(TedsAnswer<int?> tedsEducationYearCount)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(tedsEducationYearCount, () => this.TedsEducationYearCount, "Education");
            TedsEducationYearCount = tedsEducationYearCount;
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
        /// Revises the substance usages at admission.
        /// </summary>
        /// <param name="tedsAdmissionRecordSubstanceUsages">The teds admission interview substance usages.</param>
        public virtual void ReviseTedsAdmissionRecordSubstanceUsages(IList<TedsAdmissionRecordSubstanceUsage> tedsAdmissionRecordSubstanceUsages)
        {
            if (tedsAdmissionRecordSubstanceUsages != null)
            {
                if (tedsAdmissionRecordSubstanceUsages.Count > 3)
                {
                    throw new ArgumentException ( "Substance usages for TEDS admission reccord allow three entries at most." );
                }
                else if ( tedsAdmissionRecordSubstanceUsages.Any ( TedsAdmissionRecordSubstanceUsage => tedsAdmissionRecordSubstanceUsages.Any(p => p != TedsAdmissionRecordSubstanceUsage && p.ValuesEqual(TedsAdmissionRecordSubstanceUsage)) ) )
                {
                    throw new ArgumentException("Shouldn’t have duplicated substance usage entry in the list.");
                }
                else
                {
                    // For the primary, secondary and tertiary Substance Problem Code fields, a
                    // client record may not have identical drug codes in two fields with identical
                    // routes of administration AND identical SINGLE drug codes in the
                    // ASSOCIATED Detailed Drug Code fields
                    foreach ( var TedsAdmissionRecordSubstanceUsage in tedsAdmissionRecordSubstanceUsages )
                    {
                        if (TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency != null 
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType != null 
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType.HasResponse
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType.Response.SingleDrugCategoryIndicator.HasValue
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType.Response.SingleDrugCategoryIndicator.Value
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType != null
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType.HasResponse
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode != null
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode.HasResponse)
                        {
                            if (tedsAdmissionRecordSubstanceUsages.Any(p => p != TedsAdmissionRecordSubstanceUsage
                            && p.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency != null
                            && Equals(p.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType, TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType)
                            && Equals(p.SubstanceUsageAtAdmission.UsualAdministrationRouteType, TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType)
                            && Equals(p.SubstanceUsageAtAdmission.DetailedDrugCode, TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode)))
                            {
                                throw new ArgumentException("For the primary, secondary and tertiary Substance Problem Code fields, a client record may not have identical drug codes in two fields with identical routes of administration AND identical SINGLE drug codes in the ASSOCIATED Detailed Drug Code fields");
                            }
                        }

                        // Cross Check Age of First Use with Patient Birth Date
                        if (TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge != null
                            && TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge.HasResponse
                            && BirthDate.HasValue)
                        {
                            var patientAge = BirthDate.Value.GetAge ();
                            if (TedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge.Response.Value > patientAge)
                            {
                                throw new ArgumentException("Age of First Use is greater than the client's age.");
                            }
                        }
                    }
                }
            }

            PrimaryTedsAdmissionRecordSubstanceUsage = null;
            SecondaryTedsAdmissionRecordSubstanceUsage = null;
            TertiaryTedsAdmissionRecordSubstanceUsage = null;

            var count = _substanceUsages.Count;
            for (var i = 0; i < count; i++)
            {
                _substanceUsages.RemoveAt ( i );
            }

            if (tedsAdmissionRecordSubstanceUsages != null)
            {
                foreach ( var TedsAdmissionRecordSubstanceUsage in tedsAdmissionRecordSubstanceUsages )
                {
                    if (TedsAdmissionRecordSubstanceUsage != null)
                    {
                        TedsAdmissionRecordSubstanceUsage.TedsAdmissionRecord = this;
                        _substanceUsages.Add ( TedsAdmissionRecordSubstanceUsage );
                    }
                }

                count = _substanceUsages.Count;
                PrimaryTedsAdmissionRecordSubstanceUsage = count > 0 ? _substanceUsages[0] : null;
                SecondaryTedsAdmissionRecordSubstanceUsage = count > 1 ? _substanceUsages[0] : null; 
                TertiaryTedsAdmissionRecordSubstanceUsage = count > 2 ? _substanceUsages[0] : null;
            }
        }

        /// <summary>
        /// Revises the medication assisted opioid therapy indicator.
        /// </summary>
        /// <param name="medicationAssistedOpioidTherapyIndicator">The medication assisted opioid therapy indicator.</param>
        public virtual void ReviseMedicationAssistedOpioidTherapyIndicator(TedsAnswer<bool?> medicationAssistedOpioidTherapyIndicator)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(medicationAssistedOpioidTherapyIndicator, () => this.MedicationAssistedOpioidTherapyIndicator, "medication-assisted opioid therapy");
            MedicationAssistedOpioidTherapyIndicator = medicationAssistedOpioidTherapyIndicator;
        }

        /// <summary>
        /// Revises the DSM diagnosis.
        /// </summary>
        /// <param name="dsmDiagnosis">The DSM diagnosis.</param>
        public virtual void ReviseDsmDiagnosis(TedsAnswer<DsmDiagnosisResponse> dsmDiagnosis)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(dsmDiagnosis, () => this.DsmDiagnosis, "DSM diagnosis");
            DsmDiagnosis = dsmDiagnosis;
        }

        /// <summary>
        /// Revises the having other psychiatric problem indicator.
        /// </summary>
        /// <param name="havingOtherPsychiatricProblemIndicator">The having other psychiatric problem indicator.</param>
        public virtual void ReviseHavingOtherPsychiatricProblemIndicator(TedsAnswer<bool?> havingOtherPsychiatricProblemIndicator)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(havingOtherPsychiatricProblemIndicator, () => this.OtherPsychiatricProblemIndicator, "psychiatric problem in addition to alcohol or drug problem");
            OtherPsychiatricProblemIndicator = havingOtherPsychiatricProblemIndicator;
        }

        /// <summary>
        /// Revises the veteran status indicator.
        /// </summary>
        /// <param name="veteranStatusIndicator">The veteran status indicator.</param>
        public virtual void ReviseVeteranStatusIndicator(TedsAnswer<bool?> veteranStatusIndicator)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(veteranStatusIndicator, () => this.VeteranStatusIndicator, "veteran status");
            VeteranStatusIndicator = veteranStatusIndicator;
        }

        /// <summary>
        /// Revises the type of the living arrangements.
        /// </summary>
        /// <param name="livingArrangementsType">Type of the living arrangements.</param>
        public virtual void ReviseLivingArrangementsType(TedsAnswer<LivingArrangementsType> livingArrangementsType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(livingArrangementsType, () => this.LivingArrangementsType, "living arrangements");
            LivingArrangementsType = livingArrangementsType;
        }

        /// <summary>
        /// Revises the type of the source of income.
        /// </summary>
        /// <param name="incomeSourceType">Type of the source of income.</param>
        public virtual void ReviseSourceOfIncomeType(TedsAnswer<IncomeSourceType> incomeSourceType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(incomeSourceType, () => this.IncomeSourceType, "source of income/support");
            IncomeSourceType = incomeSourceType;
        }

        /// <summary>
        /// Revises the type of the health insurance.
        /// </summary>
        /// <param name="healthInsuranceType">Type of the health insurance.</param>
        public virtual void ReviseHealthInsuranceType(TedsAnswer<HealthInsuranceType> healthInsuranceType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(healthInsuranceType, () => this.HealthInsuranceType, "health insurance");
            HealthInsuranceType = healthInsuranceType;
        }

        /// <summary>
        /// Revises the type of the primary source of payment.
        /// </summary>
        /// <param name="primaryPaymentSourceType">Type of the primary source of payment.</param>
        public virtual void RevisePrimarySourceOfPaymentType(TedsAnswer<PrimaryPaymentSourceType> primaryPaymentSourceType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(primaryPaymentSourceType, () => this.PrimaryPaymentSourceType, "expected/actual primary source of payment");
            PrimaryPaymentSourceType = primaryPaymentSourceType;
        }

        /// <summary>
        /// Revises the teds martial status.
        /// </summary>
        /// <param name="maritalStatus">The teds martial status.</param>
        public virtual void ReviseTedsMartialStatus(TedsAnswer<MaritalStatus> maritalStatus)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(maritalStatus, () => this.MaritalStatus, "marital status");
            MaritalStatus = maritalStatus;
        }

        /// <summary>
        /// Revises the days waiting to enter treatment count.
        /// </summary>
        /// <param name="daysWaitingToEnterTreatmentCount">The days waiting to enter treatment count.</param>
        public virtual void ReviseDaysWaitingToEnterTreatmentCount(TedsAnswer<int?> daysWaitingToEnterTreatmentCount)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(daysWaitingToEnterTreatmentCount, () => this.DaysWaitingToEnterTreatmentCount, "days waiting to enter treatment");
            DaysWaitingToEnterTreatmentCount = daysWaitingToEnterTreatmentCount;
        }

        /// <summary>
        /// Revises the arrests in thirty days count.
        /// </summary>
        /// <param name="arrestsInThirtyDaysCount">The arrests in thirty days count.</param>
        public virtual void ReviseArrestsInThirtyDaysCount(TedsAnswer<int?> arrestsInThirtyDaysCount)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(arrestsInThirtyDaysCount, () => this.ArrestsInThirtyDaysCount, "number of arrests in 30 days prior to admission");
            ArrestsInThirtyDaysCount = arrestsInThirtyDaysCount;
        }

        /// <summary>
        /// Revises the frequency of attendance at self help programs.
        /// </summary>
        /// <param name="participatedSelfHelpGroupInPastThirtyDaysType">The frequency of attendance at self help programs.</param>
        public virtual void ReviseFrequencyOfAttendanceAtSelfHelpProgramsType(TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> participatedSelfHelpGroupInPastThirtyDaysType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(participatedSelfHelpGroupInPastThirtyDaysType, () => this.ParticipatedSelfHelpGroupInPastThirtyDaysType, "frequency of attendance at self-help programs (e.g., AA, NA, etc.) in 30 days prior to admission");
            ParticipatedSelfHelpGroupInPastThirtyDaysType = participatedSelfHelpGroupInPastThirtyDaysType;
        }

        /// <summary>
        /// Determines whether this instance is complete.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsComplete()
        {
            // The following properties shouldn’t be null
            // SystemDataSet
            // ProviderIdentifier
            // ClientIdentifier
            // CoDependentIndicator
            // ClientTransactionType
            // TedsServiceType
            // FrequencyOfAttendanceAtSelfHelpPrograms

            bool isComplete = !( TedsAdmissionKeyFields == null
                                 || ParticipatedSelfHelpGroupInPastThirtyDaysType == null );

            return isComplete;
        }

        /// <summary>
        /// Generates the Admission record.
        /// </summary>
        /// <returns>A string.</returns>
        public virtual string GenerateAdmissionRecord()
        {
            if (!IsComplete())
            {
                throw  new ApplicationException("This TEDS admission interview is not completed yet.");
            }

            // Included in both the admission and the Admission data sets are several key fields. The key fields
            // combine to form a unique identifier (retrieval key) for the record in the TEDS Admission
            // database. Any Admission record submitted to TEDS that matches a record already in the TEDS
            // database on all the Admission key fields is rejected as a duplicate.
            // Admission Data Key fields are: 
            // State Code
            // Provider Identifier
            // Client Identifier
            // Co-dependent/Collateral code
            // Client transaction type
            // Type of Service at Admission
            // Date of Admission

            // All states should begin submitting SuDS 16 and SuDS 17 as soon as practicable. Also, all State should begin
            // submitting, as soon as practicable, their admissions data with a file format that includes all columns through
            // column 138. If Frequency of attendance at self-help programs or any other variable is not yet collected by the
            // State, its field should be coded as “not collected” (98 for the self-help variable).

            var recordBuilder = new StringBuilder();

            // System Data Set (SDS) - 3 processing control data items that are reported by all States.

            // SDS 1 - SYSTEM TRANSACTION TYPE
            // Valid Entries: A, C, D
            // An invalid entry in this field is automatically changed to "A."
            var sds1 = TedsAdmissionKeyFields.SystemDataSet.SystemTransactionType.ShortName;
            recordBuilder.Append(sds1);

            // SDS 2 - STATE CODE – (KEY FIELD)
            // Valid Entries: The valid FIPS two-letter state code for the submitting State.
            // An invalid entry in this field automatically causes record to fail.
            var sds2 = TedsAdmissionKeyFields.SystemDataSet.StateProvince.ShortName;
            recordBuilder.Append(sds2);

            // SDS 3 - REPORTING DATE
            // Valid Entries: MMYYYY
            // Every record in a state submission must contain the same date of submission.
            var sds3 = string.Format("{0:MMyyyy}", TedsAdmissionKeyFields.SystemDataSet.ReportingDate);
            recordBuilder.Append(sds3);

            // Minimum Data Set (MDS) - 27 data items, including demographic and drug history data
            // that are reported by nearly all States.

            // MDS 1 - PROVIDER IDENTIFIER - (KEY FIELD)
            // Valid Entries: Entry must contain a valid provider ID that matches the State ID or the I-SATS ID in SAMHSA's I-SATS.
            // If this field is blank, the record will not be processed.
            // Field Length: 15
            // Data Type: Alphanumeric (Left-justified and filled with blank spaces)
            var mds1 = string.Format("{0,-15}", TedsAdmissionKeyFields.ProviderIdentifier.IdentifierValue);
            recordBuilder.Append(mds1);

            // MDS 2 - CLIENT IDENTIFIER - (KEY FIELD)
            // Valid entries: An identifier of from 1 to 15 alphanumeric characters that is unique within the
            // state for NOMS participation and for states not participating in NOMS must, at a
            // minimum, be unique within the provider. If the field is blank, the record will not 
            // be processed.
            // Field Length: 15
            // Data Type: Alphanumeric (Left-justified and filled with blank spaces)
            var mds2 = string.Format("{0,-15}", TedsAdmissionKeyFields.ClientIdentifier.IdentifierValue);
            recordBuilder.Append(mds2);

            // MDS 3 - CO-DEPENDENT/COLLATERAL - (KEY FIELD)
            // Valid Entries: 
            // 1 Yes 
            // 2 No
            var mds3 = "2";
            if (TedsAdmissionKeyFields.CoDependentIndicator)
            {
                mds3 = "1";
            }
            recordBuilder.Append(mds3);

            // MDS 4 - CLIENT TRANSACTION TYPE — (KEY FIELD)
            // Valid entries:
            // A Admission
            // T Transfer / Change in service
            var md4 = TedsAdmissionKeyFields.ClientTransactionType.ShortName;
            recordBuilder.Append(md4);

            // MDS 5: DATE OF ADMISSION — (Key Field) 
            // Valid Entries: MMDDYYYY
            var mds5 = string.Format("{0:MMddyyyy}", TedsAdmissionKeyFields.AdmissionDate);
            recordBuilder.Append(mds5);

            // MDS 6: NUMBER OF PRIOR TREATMENT EPISODES 
            // Indicates the number of previous treatment episodes the client has received in any
            // drug or alcohol program. Changes in service for the same episode (transfers)
            // should not be counted as separate prior episodes.
            // The number of prior treatments for a co-dependent/collateral record should
            // include only treatments as a co-dependent.
            // 8 Not Collected
            var mds6 = "8";
            if (PriorTreatmentEpisodesCount != null)
            {
                if ( PriorTreatmentEpisodesCount.HasResponse )
                {
                    if ( PriorTreatmentEpisodesCount.Response.Value <= 5 )
                    {
                        mds6 = string.Format ( "{0,1}", PriorTreatmentEpisodesCount.Response.Value );
                    }
                    else
                    {
                        mds6 = "5";
                    }
                }
                else if (PriorTreatmentEpisodesCount.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    mds6 = "7";
                }
            }
            recordBuilder.Append(mds6);

            // MDS 7 - PRINCIPAL SOURCE OF REFERRAL 
            // 98 Not Collected
            var mds7 = "98";
            if (PrimaryPaymentSourceType != null)
            {
                if (PrimaryPaymentSourceType.HasResponse)
                {
                    mds7 = PrimaryPaymentSourceType.Response.ShortName;
                }
                else if (PrimaryPaymentSourceType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    mds7 = "97";
                }
            }
            recordBuilder.Append(mds7);

            // MDS 8 - DATE OF BIRTH 
            // Valid entries: MMDDYYYY
            // Unknown (01010007) Use this code if the State collects these data, but for some
            // reason this record does not reflect an acceptable value.
            // Not Collected (01010008) Use this code if the State does not collect these data
            // for submission to TEDS.
            var mds8 = "01010008";
            if (BirthDate != null)
            {
                mds8 = string.Format("{0:MMddyyyy}", BirthDate.Value);
            }
            recordBuilder.Append(mds8);

            // MDS 9 - SEX
            // Unknown (7) Use this code if the State collects these data, but for some reason
            // this record does not reflect an acceptable value.
            // Not Collected (8) Use this code if the State does not collect these data for
            // submission to TEDS.
            var mds9 = "8";
            if (TedsGenderInformation != null)
            {
                if (TedsGenderInformation.TedsGender != null)
                {
                    if (TedsGenderInformation.TedsGender.HasResponse)
                    {
                        mds9 = TedsGenderInformation.TedsGender.Response.ShortName;
                    }
                    else if (TedsGenderInformation.TedsGender.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                    {
                        mds9 = "7";
                    }
                }
            }
            recordBuilder.Append(mds9);

            // MDS 10 - RACE
            // 97 Unknown - Use this code if the State collects these data, but for some
            // reason this record does not reflect an acceptable value.
            // 98 Not Collected - Use this code for all records if the State does not collect
            // these data for submission to TEDS.
            var mds10 = "98";
            if (TedsRace != null)
            {
                if (TedsRace.HasResponse)
                {
                    mds10 = TedsRace.Response.ShortName;
                }
                else if(TedsRace.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    mds10 = "97";
                }
            }
            recordBuilder.Append(mds10);

            // MDS 11 - ETHNICITY 
            var mds11 = "98";
            if (TedsEthnicity != null)
            {
                if (TedsEthnicity.HasResponse)
                {
                    mds11 = TedsEthnicity.Response.ShortName;
                }
                else if (TedsEthnicity.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    mds11 = "97";
                }
            }
            recordBuilder.Append(mds11);

            // MDS 12 - EDUCATION 
            var mds12 = "98";
            if (TedsEducationYearCount != null)
            {
                if (TedsEducationYearCount.HasResponse)
                {
                    if (TedsEducationYearCount.Response.Value <= 25)
                    {
                        mds12 = string.Format("{0,2:00}", TedsEducationYearCount.Response.Value);
                    }
                    else
                    {
                        mds12 = "25";
                    }
                }
                else if (TedsEducationYearCount.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    mds12 = "97";
                }
            }
            recordBuilder.Append(mds12);

            // MDS 13 - EMPLOYMENT STATUS 
            var mds13 = "98";
            if (TedsEmploymentStatusInformation != null)
            {
                if (TedsEmploymentStatusInformation.TedsEmploymentStatus != null)
                {
                    if (TedsEmploymentStatusInformation.TedsEmploymentStatus.HasResponse)
                    {
                        mds13 = TedsEmploymentStatusInformation.TedsEmploymentStatus.Response.ShortName;
                    }
                    else if (TedsEmploymentStatusInformation.TedsEmploymentStatus.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                    {
                        mds13 = "97";
                    }
                }
            }
            recordBuilder.Append(mds13);

            // MDS 14 (A) - SUBSTANCE PROBLEM CODE, PRIMARY 
            // MDS 15 (A) - USUAL ROUTE OF ADMINISTRATION, PRIMARY 
            // MDS 16 (A) - FREQUENCY OF USE, PRIMARY 
            // MDS 17 (A) - AGE OF FIRST USE, PRIMARY 
            var mds14a = SubstanceUsageAtAdmission.GetSubstanceProblemTypeCode(PrimaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds15a = SubstanceUsageAtAdmission.GetUsualRouteOfAdministrationTypeCode(PrimaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds16a = SubstanceUsageAtAdmission.GetSubstanceUseFrequencyTypeCode(PrimaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds17a = SubstanceUsageAtAdmission.GetFirstUseAgeCode(PrimaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            recordBuilder.Append(mds14a);
            recordBuilder.Append(mds15a);
            recordBuilder.Append(mds16a);
            recordBuilder.Append(mds17a);

            // MDS 14 (B) - SUBSTANCE PROBLEM CODE, SECONDARY
            // MDS 15 (B) - USUAL ROUTE OF ADMINISTRATION, SECONDARY
            // MDS 16 (B) - FREQUENCY OF USE, SECONDARY
            // MDS 17 (B) - AGE OF FIRST USE, SECONDARY
            var mds14b = SubstanceUsageAtAdmission.GetSubstanceProblemTypeCode(SecondaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds15b = SubstanceUsageAtAdmission.GetUsualRouteOfAdministrationTypeCode(SecondaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds16b = SubstanceUsageAtAdmission.GetSubstanceUseFrequencyTypeCode(SecondaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds17b = SubstanceUsageAtAdmission.GetFirstUseAgeCode(SecondaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            recordBuilder.Append(mds14b);
            recordBuilder.Append(mds15b);
            recordBuilder.Append(mds16b);
            recordBuilder.Append(mds17b);

            // MDS 14 (C) - SUBSTANCE PROBLEM CODE, TERTIARY
            // MDS 15 (C) - USUAL ROUTE OF ADMINISTRATION, TERTIARY
            // MDS 16 (C) - FREQUENCY OF USE, TERTIARY
            // MDS 17 (C) - AGE OF FIRST USE, TERTIARY
            var mds14c = SubstanceUsageAtAdmission.GetSubstanceProblemTypeCode(TertiaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds15c = SubstanceUsageAtAdmission.GetUsualRouteOfAdministrationTypeCode(TertiaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds16c = SubstanceUsageAtAdmission.GetSubstanceUseFrequencyTypeCode(TertiaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            var mds17c = SubstanceUsageAtAdmission.GetFirstUseAgeCode(TertiaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            recordBuilder.Append(mds14c);
            recordBuilder.Append(mds15c);
            recordBuilder.Append(mds16c);
            recordBuilder.Append(mds17c);

            // MDS 18 - TYPE OF SERVICE (KEY FIELD)
            // Field Length: 2
            // Data Type: Numeric
            var mds18 = string.Empty;
            if (TedsAdmissionKeyFields.TedsServiceType.HasResponse)
            {
                mds18 = TedsAdmissionKeyFields.TedsServiceType.Response.ShortName;
            }
            else if (TedsAdmissionKeyFields.TedsServiceType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
            {
                mds18 = "96";
            }
            recordBuilder.Append(mds18);

            // MDS 19 - MEDICATION-ASSISTED OPIOID THERAPY 
            // Field Length 1
            var mds19 = "8";
            if (MedicationAssistedOpioidTherapyIndicator.HasResponse)
            {
                if (MedicationAssistedOpioidTherapyIndicator.Response.Value)
                {
                    mds19 = "1";
                }
                else
                {
                    mds19 = "2";
                }
            }
            recordBuilder.Append(mds19);

            // Supplemental Data Set (SuDS) - 17 data items that provide additional client information
            // and more detailed information for some MDS items. States are encouraged to report all
            // SuDS items, and should report all items available from the State data system.
            // Four of the SuDS are NOMS and are required reporting for states with SOMMS
            // subcontracts for reporting NOMS. These are “Living arrangements”, “Detailed not in
            // labor force” and two relatively new data elements, “Number of arrests in 30 days prior
            // to admission” and “Frequency of attendance at self-help programs in 30 days prior to admission”.

            // SUDS 1 - DETAILED DRUG CODE, PRIMARY
            // Field Length 4
            var suds1 = SubstanceUsageAtAdmission.GetDetailedDrugCode(PrimaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            recordBuilder.Append(suds1);

            // SUDS 2 - DETAILED DRUG CODE, SECONDARY
            var suds2 = SubstanceUsageAtAdmission.GetDetailedDrugCode(SecondaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            recordBuilder.Append(suds2);

            // SUDS 3 - DETAILED DRUG CODE, TERTIARY
            var suds3 = SubstanceUsageAtAdmission.GetDetailedDrugCode(TertiaryTedsAdmissionRecordSubstanceUsage.SubstanceUsageAtAdmission);
            recordBuilder.Append(suds3);

            // SUDS 4 - DSM DIAGNOSIS 
            // If the DSM IV is not used, the State must specify the coding system in State crosswalk.
            // Valid entries: (XXX.xx) (XXX.x- ) (XXX . - - ) (XXX - - - ) where – represents a blank.
            // 999.97 Unknown
            // 999.98 Not collected
            // Field Length 6
            var suds4 = "999.98";
            if (DsmDiagnosis != null)
            {
                if (DsmDiagnosis.HasResponse)
                {
                    suds4 = DsmDiagnosis.Response.Code;
                }
                else if (DsmDiagnosis.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds4 = "999.97";
                }
            }
            recordBuilder.Append(suds4);

            // SUDS 5 - PSYCHIATRIC PROBLEM IN ADDITION TO ALCOHOL OR DRUG PROBLEM
            // Valid entries: 1 Yes
            // 2 No
            // 7 Unknown
            // 8 Not Collected
            var suds5 = "8";
            if (OtherPsychiatricProblemIndicator != null)
            {
                if (OtherPsychiatricProblemIndicator.HasResponse)
                {
                    if (OtherPsychiatricProblemIndicator.Response.Value)
                    {
                        suds5 = "1";
                    }
                    else
                    {
                        suds5 = "2";
                    }
                }
                else if (OtherPsychiatricProblemIndicator.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds5 = "7";
                }
            }
            recordBuilder.Append(suds5);

            // SUDS 6 - PREGNANT AT TIME OF ADMISSION 
            var suds6 = "8";
            if (TedsGenderInformation != null)
            {
                if (TedsGenderInformation.PregnantIndicator != null)
                {
                    if (TedsGenderInformation.PregnantIndicator.HasResponse)
                    {
                        if (TedsGenderInformation.PregnantIndicator.Response.Value)
                        {
                            suds6 = "1";
                        }
                        else
                        {
                            suds6 = "2";
                        }
                    }
                    else if (TedsGenderInformation.PregnantIndicator.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                    {
                        suds6 = "7";
                    }
                    else if (TedsGenderInformation.PregnantIndicator.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
                    {
                        suds6 = "6";
                    }
                }
            }
            recordBuilder.Append(suds6);

            // SUDS 7 - VETERAN STATUS 
            var suds7 = "8";
            if (VeteranStatusIndicator != null)
            {
                if (VeteranStatusIndicator.HasResponse)
                {
                    if (VeteranStatusIndicator.Response.Value)
                    {
                        suds7 = "1";
                    }
                    else
                    {
                        suds7 = "2";
                    }
                }
                else if (VeteranStatusIndicator.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds7 = "7";
                }
            }
            recordBuilder.Append(suds7);

            // SUDS 8 - LIVING ARRANGEMENTS 
            var suds8 = "98";
            if (LivingArrangementsType != null)
            {
                if (LivingArrangementsType.HasResponse)
                {
                    suds8 = LivingArrangementsType.Response.ShortName;
                }
                else if (LivingArrangementsType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds8 = "97";
                }
            }
            recordBuilder.Append(suds8);

            // SUDS 9 - SOURCE OF INCOME/SUPPORT 
            var suds9 = "98";
            if (IncomeSourceType != null)
            {
                if (IncomeSourceType.HasResponse)
                {
                    suds9 = IncomeSourceType.Response.ShortName;
                }
                else if (IncomeSourceType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds9 = "97";
                }
            }
            recordBuilder.Append(suds9);

            // SUDS 10 - HEALTH INSURANCE 
            var suds10 = "98";
            if (HealthInsuranceType != null)
            {
                if (HealthInsuranceType.HasResponse)
                {
                    suds10 = HealthInsuranceType.Response.ShortName;
                }
                else if (HealthInsuranceType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds10 = "97";
                }
            }
            recordBuilder.Append ( suds10 );

            // SUDS 11 - EXPECTED/ACTUAL PRIMARY SOURCE OF PAYMENT 
            var suds11 = "98";
            if (PrimaryPaymentSourceType != null)
            {
                if (PrimaryPaymentSourceType.HasResponse)
                {
                    suds11 = PrimaryPaymentSourceType.Response.ShortName;
                }
                else if (PrimaryPaymentSourceType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds11 = "97";
                }
            }
            recordBuilder.Append(suds11);

            // SUDS 12 - DETAILED NOT IN LABOR FORCE 
            var suds12 = "98";
            if (TedsEmploymentStatusInformation != null)
            {
                if (TedsEmploymentStatusInformation.DetailedNotInLaborForce != null)
                {
                    if (TedsEmploymentStatusInformation.DetailedNotInLaborForce.HasResponse)
                    {
                        suds12 = TedsEmploymentStatusInformation.DetailedNotInLaborForce.Response.ShortName;
                    }
                    else if (TedsEmploymentStatusInformation.DetailedNotInLaborForce.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                    {
                        suds12 = "97";
                    }
                    else if (TedsEmploymentStatusInformation.DetailedNotInLaborForce.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
                    {
                        suds12 = "96";
                    }
                }
            }
            recordBuilder.Append(suds12);

            // SUDS 13 - DETAILED CRIMINAL JUSTICE REFERRAL 
            // This field is to be used only when Principal Source of Referral (MDS 7) is coded
            // 07 “Criminal Justice Referral." For all other Principal Source of Referral codes
            // (01 through 06 and 97), this field should be coded 96 (Not Applicable).
            var suds13 = "98";
            if (PrincipalReferralSourceInformation != null)
            {
                if (PrincipalReferralSourceInformation.DetailedCriminalJusticeReferral != null)
                {
                    if (PrincipalReferralSourceInformation.DetailedCriminalJusticeReferral.HasResponse)
                    {
                        suds13 = PrincipalReferralSourceInformation.DetailedCriminalJusticeReferral.Response.ShortName;
                    }
                    else if (PrincipalReferralSourceInformation.DetailedCriminalJusticeReferral.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                    {
                        suds13 = "97";
                    }
                    else if (PrincipalReferralSourceInformation.DetailedCriminalJusticeReferral.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
                    {
                        suds13 = "96";
                    }
                }
            }
            recordBuilder.Append(suds13);

            //TODO: Get the teds code

            // SUDS 14 - MARITAL STATUS 
            var suds14 = "98";

            //if (MaritalStatus != null)
            //{
            //    if (MaritalStatus.HasResponse)
            //    {
            //        suds14 = MaritalStatus.Response.ShortName;
            //    }
            //    else if (MaritalStatus.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
            //    {
            //        suds14 = "97";
            //    }
            //}
            recordBuilder.Append(suds14);

            // SUDS 15 - DAYS WAITING TO ENTER TREATMENT 
            var suds15 = "998";
            if (DaysWaitingToEnterTreatmentCount != null)
            {
                if ( DaysWaitingToEnterTreatmentCount.HasResponse )
                {
                    if (DaysWaitingToEnterTreatmentCount.Response.Value <= 996)
                    {
                        suds15 = string.Format("{0,3:000}", DaysWaitingToEnterTreatmentCount.Response.Value);
                    }
                    else
                    {
                        suds15 = "996";
                    }
                }
                else if (DaysWaitingToEnterTreatmentCount.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds15 = "997";
                }
            }
            recordBuilder.Append(suds15);

            // SUDS 16 - NUMBER OF ARRESTS IN 30 DAYS PRIOR TO ADMISSION 
            var suds16 = "98";
            if (ArrestsInThirtyDaysCount != null)
            {
                if (ArrestsInThirtyDaysCount.HasResponse)
                {
                    if (ArrestsInThirtyDaysCount.Response.Value <= 96)
                    {
                        suds16 = string.Format("{0,2:00}", ArrestsInThirtyDaysCount.Response.Value);
                    }
                    else
                    {
                        suds16 = "96";
                    }
                }
                else if (ArrestsInThirtyDaysCount.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds16 = "97";
                }
            }
            recordBuilder.Append(suds16);

            // SUDS 17 - FREQUENCY OF ATTENDANCE AT SELF-HELP PROGRAMS (e.g., AA, NA, etc.) IN 30 DAYS PRIOR TO ADMISSION
            // Begin Column: 137
            // End Column: 138
            var suds17 = "98";
            if (ParticipatedSelfHelpGroupInPastThirtyDaysType != null)
            {
                if (ParticipatedSelfHelpGroupInPastThirtyDaysType.HasResponse)
                {
                    suds17 = ParticipatedSelfHelpGroupInPastThirtyDaysType.Response.ShortName;
                }
                else if (ParticipatedSelfHelpGroupInPastThirtyDaysType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    suds17 = "97";
                }
            }
            recordBuilder.Append(suds17);

            return recordBuilder.ToString();
        }

        /// <summary>
        /// Gets the default non response lookup well known names.
        /// </summary>
        public static IEnumerable<string> DefaultNonResponseLookupWellKnownNames
        {
            get
            {
                return new List<string>
                           {
                               WellKnownNames.TedsModule.TedsNonResponse.Unknown
                           };
            }
        }


        private IEnumerable<string> GetNonResponseLookupWellKnownNames<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            IEnumerable<string> wellKnownNames = DefaultNonResponseLookupWellKnownNames;

            //var wellKnownNamesForMostProperties = new List<string> { WellKnownNames.TedsModule.TedsNonResponse.NotApplicable, WellKnownNames.TedsModule.TedsNonResponse.Unknown };

            //string propertyName = PropertyUtil.ExtractPropertyName(propertyExpression);

            return wellKnownNames;
        }

        private void CheckIfTedsAnswerHasInvalidNonResponse<T>(TedsAnswer<T> tedsAnswer, Expression<Func<TedsAnswer<T>>> propertyExpression, string tedsQuestion)
        {
            if (tedsAnswer != null
                && !tedsAnswer.HasResponse
                && !GetNonResponseLookupWellKnownNames(propertyExpression).ToList().Contains(tedsAnswer.TedsNonResponse.WellKnownName)
                )
            {
                throw new ArgumentException(
                    string.Format("{0} has invalid non-reponse.", tedsQuestion.Substring(0, 1).ToUpper() + tedsQuestion.Substring(1)));
            }
        }
    }
}
