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
using Pillar.Common.Extension;
using Pillar.Domain;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// TedsAdmissionInterview is an <see cref="Activity">Activity</see> that defines 
    /// TEDS Admission data with national outcomes measures (NOMS).
    /// </summary>
    /// <remarks>
    /// TEDS stands for Treatment Episode Data Set.
    /// </remarks>
    public class TedsAdmissionInterview : Activity
    {
        private readonly IList<TedsAdmissionInterviewSubstanceUsage> _substanceUsages;

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionInterview"/> class.
        /// </summary>
        protected internal TedsAdmissionInterview()
        {
            _substanceUsages = new List<TedsAdmissionInterviewSubstanceUsage> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionInterview"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal TedsAdmissionInterview(
            Visit visit,
            ActivityType activityType
            )
            : base(visit, activityType)
        {
            _substanceUsages = new List<TedsAdmissionInterviewSubstanceUsage>();
        }

        /// <summary>
        /// Gets a value indicating whether [co dependent indicator].
        /// MDS 3
        /// </summary>
        /// <value>
        /// <c>true</c> if [co dependent indicator]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool? CoDependentIndicator { get; protected set; }

        /// <summary>
        /// Gets the count of prior treatment episodes.
        /// MDS 6
        /// </summary>
        public virtual TedsAnswer<int?> PriorTreatmentEpisodesCount { get; private set; }

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
        public virtual TedsAdmissionInterviewSubstanceUsage PrimaryTedsAdmissionInterviewSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the secondary substance usage at admission.
        /// MDS 14 (B), MDS 15 (B), MDS 16 (B), MDS 17 (B), SUDS 2
        /// </summary>
        [NoneCascading]
        public virtual TedsAdmissionInterviewSubstanceUsage SecondaryTedsAdmissionInterviewSubstanceUsage { get; private set; }

        /// <summary>
        /// Gets the tertiary substance usage at admission.
        /// MDS 14 (C), MDS 15 (C), MDS 16 (C), MDS 17 (C), SUDS 3
        /// </summary>
        [NoneCascading]
        public virtual TedsAdmissionInterviewSubstanceUsage TertiaryTedsAdmissionInterviewSubstanceUsage { get; private set; }

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
        /// Gets the count of arrests in thirty days.
        /// SUDS 16
        /// </summary>
        public virtual TedsAnswer<int?> ArrestsInPastThirtyDaysCount { get; private set; }

        /// <summary>
        /// Gets the frequency of attendance at self help programs answer.
        /// SUDS 17
        /// </summary>
        public virtual TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> ParticipatedSelfHelpGroupInPastThirtyDaysType { get; private set; }

        /// <summary>
        /// Gets the substance usages.
        /// </summary>
        public virtual IEnumerable<TedsAdmissionInterviewSubstanceUsage> SubstanceUsages
        {
            get { return _substanceUsages.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Revises the co dependent indicator.
        /// </summary>
        /// <param name="coDependentIndicator">If set to <c>true</c> [co dependent indicator].</param>
        public virtual void ReviseCoDependentIndicator(bool? coDependentIndicator)
        {
            //TODO: Cross check with ServieType (MDS 18) from Program
            // It shouldn't be null
            //if ( !coDependentIndicator && !tedsServiceType.HasResponse
            //         && tedsServiceType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable )
            //    {
            //        throw new ArgumentException ( "Use not applicable for type of services only for co-dependents/collateral clients." );
            //    }
            CoDependentIndicator = coDependentIndicator;
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
        /// Revises the teds gender information.
        /// </summary>
        /// <param name="tedsGenderInformation">The teds gender information.</param>
        public virtual void ReviseTedsGenderInformation(TedsGenderInformation tedsGenderInformation)
        {
            TedsGenderInformation = tedsGenderInformation;
        }

        /// <summary>
        /// Revises the teds race.
        /// </summary>
        /// <param name="tedsRace">The teds race.</param>
        public virtual void ReviseTedsRace(TedsAnswer<TedsRace> tedsRace)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(tedsRace, () => this.TedsRace, "race");

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
        /// Revises the teds admission interview substance usages.
        /// </summary>
        /// <param name="primaryTedsAdmissionInterviewSubstanceUsage">The primary teds admission interview substance usage.</param>
        /// <param name="secondaryTedsAdmissionInterviewSubstanceUsage">The secondary teds admission interview substance usage.</param>
        /// <param name="tertiaryTedsAdmissionInterviewSubstanceUsage">The tertiary teds admission interview substance usage.</param>
        public virtual void ReviseTedsAdmissionInterviewSubstanceUsages(TedsAdmissionInterviewSubstanceUsage primaryTedsAdmissionInterviewSubstanceUsage, TedsAdmissionInterviewSubstanceUsage secondaryTedsAdmissionInterviewSubstanceUsage, TedsAdmissionInterviewSubstanceUsage tertiaryTedsAdmissionInterviewSubstanceUsage)
        {
            if (tertiaryTedsAdmissionInterviewSubstanceUsage != null)
            {
                if (primaryTedsAdmissionInterviewSubstanceUsage == null || secondaryTedsAdmissionInterviewSubstanceUsage == null)
                {
                    throw new ArgumentException("Primary or secondary substance usage cannot be null if tertiary substance usage is not null.");
                }
            }

            if (secondaryTedsAdmissionInterviewSubstanceUsage != null)
            {
                if (primaryTedsAdmissionInterviewSubstanceUsage == null)
                {
                    throw new ArgumentException("Primary substance usage cannot be null if secondary substance usage is not null.");
                }
            }

            var tedsAdmissionInterviewSubstanceUsages = new List<TedsAdmissionInterviewSubstanceUsage> ();
            if (primaryTedsAdmissionInterviewSubstanceUsage != null)
            {
                tedsAdmissionInterviewSubstanceUsages.Add ( primaryTedsAdmissionInterviewSubstanceUsage );
            }
            if (secondaryTedsAdmissionInterviewSubstanceUsage != null)
            {
                tedsAdmissionInterviewSubstanceUsages.Add ( secondaryTedsAdmissionInterviewSubstanceUsage );
            }
            if (tertiaryTedsAdmissionInterviewSubstanceUsage != null)
            {
                tedsAdmissionInterviewSubstanceUsages.Add ( tertiaryTedsAdmissionInterviewSubstanceUsage );
            }

            ReviseTedsAdmissionInterviewSubstanceUsages ( tedsAdmissionInterviewSubstanceUsages );
        }

        /// <summary>
        /// Revises the substance usages at admission.
        /// </summary>
        /// <param name="tedsAdmissionInterviewSubstanceUsages">The teds admission interview substance usages.</param>
        private void ReviseTedsAdmissionInterviewSubstanceUsages(IList<TedsAdmissionInterviewSubstanceUsage> tedsAdmissionInterviewSubstanceUsages)
        {
            if (tedsAdmissionInterviewSubstanceUsages != null)
            {
                if (tedsAdmissionInterviewSubstanceUsages.Count > 3)
                {
                    throw new ArgumentException ( "Substance usages for TEDS admission interview allow three entries at most." );
                }

                if ( tedsAdmissionInterviewSubstanceUsages.Any ( tedsAdmissionInterviewSubstanceUsage => tedsAdmissionInterviewSubstanceUsage != null && tedsAdmissionInterviewSubstanceUsages.Any(p => p != null && p != tedsAdmissionInterviewSubstanceUsage && p.ValuesEqual(tedsAdmissionInterviewSubstanceUsage)) ) )
                {
                    throw new ArgumentException("Shouldn’t have duplicated substance usage entry in the list.");
                }

                // For the primary, secondary and tertiary Substance Problem Code fields, a
                // client record may not have identical drug codes in two fields with identical
                // routes of administration AND identical SINGLE drug codes in the
                // ASSOCIATED Detailed Drug Code fields
                foreach ( var tedsAdmissionInterviewSubstanceUsage in tedsAdmissionInterviewSubstanceUsages )
                {
                    if (tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency != null 
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType != null 
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType.HasResponse
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType.Response.SingleDrugCategoryIndicator.HasValue
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType.Response.SingleDrugCategoryIndicator.Value
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType != null
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType.HasResponse
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode != null
                        && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode.HasResponse)
                    {
                        if (tedsAdmissionInterviewSubstanceUsages.Any(p => p != tedsAdmissionInterviewSubstanceUsage
                                                                           && p.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency != null
                                                                           && Equals(p.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType, tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.SubstanceProblemAndFrequency.SubstanceProblemType)
                                                                           && Equals(p.SubstanceUsageAtAdmission.UsualAdministrationRouteType, tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.UsualAdministrationRouteType)
                                                                           && Equals(p.SubstanceUsageAtAdmission.DetailedDrugCode, tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.DetailedDrugCode)))
                        {
                            throw new ArgumentException("For the primary, secondary and tertiary Substance Problem Code fields, a client record may not have identical drug codes in two fields with identical routes of administration AND identical SINGLE drug codes in the ASSOCIATED Detailed Drug Code fields");
                        }
                    }

                    // Cross Check Age of First Use with Patient Birth Date
                    var patient = this.Visit.ClinicalCase.Patient;
                    if (patient.Profile != null && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge != null && tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge.HasResponse)
                    {
                        var birthDate = patient.Profile.BirthDate;
                        if (birthDate.HasValue)
                        {
                            var patientAge = birthDate.Value.GetAge();
                            if (tedsAdmissionInterviewSubstanceUsage.SubstanceUsageAtAdmission.FirstUseAge.Response.Value > patientAge)
                            {
                                throw new ArgumentException(string.Format("Age of First Use is greater than the client's age {0}.", patientAge));
                            }
                        }
                    }
                }
            }

            PrimaryTedsAdmissionInterviewSubstanceUsage = null;
            SecondaryTedsAdmissionInterviewSubstanceUsage = null;
            TertiaryTedsAdmissionInterviewSubstanceUsage = null;

            _substanceUsages.Clear();

            if (tedsAdmissionInterviewSubstanceUsages != null)
            {
                foreach ( var tedsAdmissionInterviewSubstanceUsage in tedsAdmissionInterviewSubstanceUsages )
                {
                    if (tedsAdmissionInterviewSubstanceUsage != null)
                    {
                        tedsAdmissionInterviewSubstanceUsage.TedsAdmissionInterview = this;
                        _substanceUsages.Add ( tedsAdmissionInterviewSubstanceUsage );
                    }
                }

                var count = _substanceUsages.Count;
                PrimaryTedsAdmissionInterviewSubstanceUsage = count > 0 ? _substanceUsages[0] : null;
                SecondaryTedsAdmissionInterviewSubstanceUsage = count > 1 ? _substanceUsages[1] : null; 
                TertiaryTedsAdmissionInterviewSubstanceUsage = count > 2 ? _substanceUsages[2] : null;
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
        /// Revises the other psychiatric problem indicator.
        /// </summary>
        /// <param name="otherPsychiatricProblemIndicator">The other psychiatric problem indicator.</param>
        public virtual void ReviseOtherPsychiatricProblemIndicator(TedsAnswer<bool?> otherPsychiatricProblemIndicator)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(otherPsychiatricProblemIndicator, () => this.OtherPsychiatricProblemIndicator, "psychiatric problem in addition to alcohol or drug problem");
            OtherPsychiatricProblemIndicator = otherPsychiatricProblemIndicator;
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
        /// Revises the type of the income source.
        /// </summary>
        /// <param name="incomeSourceType">Type of the income source.</param>
        public virtual void ReviseIncomeSourceType(TedsAnswer<IncomeSourceType> incomeSourceType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(incomeSourceType, () => this.IncomeSourceType, "source of income/support");
            IncomeSourceType = incomeSourceType;
        }

        /// <summary>
        /// Revises the type of the primary source of payment.
        /// </summary>
        /// <param name="primaryPaymentSourceType">Type of the primary source of payment.</param>
        public virtual void RevisePrimaryPaymentSourceType(TedsAnswer<PrimaryPaymentSourceType> primaryPaymentSourceType)
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
        /// Revises the arrests in past thirty days count.
        /// </summary>
        /// <param name="arrestsInThirtyDaysCount">The arrests in thirty days count.</param>
        public virtual void ReviseArrestsInPastThirtyDaysCount(TedsAnswer<int?> arrestsInThirtyDaysCount)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(arrestsInThirtyDaysCount, () => this.ArrestsInPastThirtyDaysCount, "number of arrests in 30 days prior to admission");
            ArrestsInPastThirtyDaysCount = arrestsInThirtyDaysCount;
        }

        /// <summary>
        /// Revises the type of the participated self help group in past thirty days.
        /// </summary>
        /// <param name="participatedSelfHelpGroupInPastThirtyDaysType">Type of the participated self help group in past thirty days.</param>
        public virtual void ReviseParticipatedSelfHelpGroupInPastThirtyDaysType(TedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType> participatedSelfHelpGroupInPastThirtyDaysType)
        {
            CheckIfTedsAnswerHasInvalidNonResponse(participatedSelfHelpGroupInPastThirtyDaysType, () => this.ParticipatedSelfHelpGroupInPastThirtyDaysType, "frequency of attendance at self-help programs (e.g., AA, NA, etc.) in 30 days prior to admission");
            ParticipatedSelfHelpGroupInPastThirtyDaysType = participatedSelfHelpGroupInPastThirtyDaysType;
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

            //string propertyName =  PropertyUtil.ExtractPropertyName ( propertyExpression );

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
