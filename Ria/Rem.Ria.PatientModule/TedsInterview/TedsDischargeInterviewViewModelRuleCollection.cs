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

using Pillar.FluentRuleEngine;
using Pillar.Common.Utility;
using Pillar.Common.Metadata;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.TedsInterview;

namespace Rem.Ria.PatientModule.TedsInterview
{
    /// <summary>
    /// Contains client side business rules for Teds Discharge Interview.
    /// </summary>
    public class TedsDischargeInterviewViewModelRuleCollection : AbstractRuleCollection<TedsDischargeInterviewViewModel>
    {
        #region Constants and Fields

        private const string LastContactDateRequiredErrorMsg = "Last contact date is Required.";
        private const string LessThan96ErrorMsg = "Enter a number between 0 and 96.";
        private const string EmploymentErrorMsg = "Detailed not in labor force is answered but employment status is not answered.";
        private const string PrimarySubstanceUsedErrorMsg = "Primary substance used question must be answered if any other primary substance usage question is answered.";
        private const string SecondarySubstanceUsedErrorMsg = "Secondary substance used question must be answered if any other secondary substance usage question is answered.";
        private const string TertiarySubstanceUsedErrorMsg = "Tertiary substance used question must be answered if any other Tertiary substance usage question is answered.";
        private const string TertiarySecondaryPrimarySubstanceUsedErrorMsg = "Primary and/or Secondary substance used question must be answered if Tertiary substance usage question is answered.";
        private const string SecondaryPrimarySubstanceUsedErrorMsg = "Primary substance used question must be answered if Secondary substance usage question is answered.";
        private const string DuplicatedSubstanceUsedErrorMsg = "Should not have duplicated substance usage.";
        private const string DetailedNotInLaborForceErrorMsg = "Detailed not in labor force information should be coded as not applicable if employment status information is coded as full time, part time, unemployed, or unknown.";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsDischargeInterviewViewModelRuleCollection"/> class.
        /// </summary>
        public TedsDischargeInterviewViewModelRuleCollection()
        {
            BuildTedsDischargeInterviewRules();

            BuildPresentaionRules();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the last face to face contact date required rule.
        /// </summary>
        /// <value>
        /// The last face to face contact date required rule.
        /// </value>
        public IRule LastFaceToFaceContactDateRequiredRule { get; set; }

        /// <summary>
        /// Gets or sets the arrests in past thirty days count less than96 rule.
        /// </summary>
        /// <value>The arrests in past thirty days count less than96 rule.</value>
        public IRule ArrestsInPastThirtyDaysCountLessThan96Rule { get; set; }

        /// <summary>
        /// Gets or sets the employment status not answered and detailed not in labor force answered should not be allowed rule.
        /// </summary>
        /// <value>
        /// The employment status not answered and detailed not in labor force answered should not be allowed rule.
        /// </value>
        public IRule EmploymentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedRule { get; set; }

        /// <summary>
        /// Gets or sets the primary substance problem type must be answered if any other primary substance usage question is answered rule.
        /// </summary>
        /// <value>
        /// The primary substance problem type must be answered if any other primary substance usage question is answered rule.
        /// </value>
        public IRule PrimarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the secondary substance problem type must be answered if any other secondary substance usage question is answered rule.
        /// </summary>
        /// <value>
        /// The secondary substance problem type must be answered if any other secondary substance usage question is answered rule.
        /// </value>
        public IRule SecondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the tertiary substance problem type must be answered if any other tertiary substance usage question is answered rule.
        /// </summary>
        /// <value>
        /// The tertiary substance problem type must be answered if any other tertiary substance usage question is answered rule.
        /// </value>
        public IRule TertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the Primary Secondary substance must be answered if tertiary substance usage question is answered rule.
        /// </summary>
        /// <value>
        /// The  Primary Substance Secondary substance must be answered if tertiary substance usage question is answered rule.
        /// </value>
        public IRule PrimarySecondarySubstanceMustBeAnsweredIfTertiarySubstanceUsageQuestionIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the Primary substance must be answered if Secondary substance usage question is answered rule.
        /// </summary>
        /// <value>
        /// The Primary substance must be answered if Secondary substance usage question is answered rule.
        /// </value>
        public IRule PrimarySubstanceMustBeAnsweredIfSecondarySubstanceUsageQuestionIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the primary and second substance usage are duplicated rule.
        /// </summary>
        /// <value>
        /// The primary and second substance usage are duplicated rule.
        /// </value>
        public IRule PrimaryAndSecondSubstanceUsageAreDuplicated { get; set; }

        /// <summary>
        /// Gets or sets the primary and tertiary substance usage are duplicated rule.
        /// </summary>
        /// <value>
        /// The primary and tertiary substance usage are duplicated rule.
        /// </value>
        public IRule PrimaryAndTertiarySubstanceUsageAreDuplicated { get; set; }

        /// <summary>
        /// Gets or sets the tertiary and second substance usage are duplicated rule.
        /// </summary>
        /// <value>
        /// The tertiary and second substance usage are duplicated rule.
        /// </value>
        public IRule TertiaryAndSecondSubstanceUsageAreDuplicated { get; set; }

        /// <summary>
        /// Gets or sets the detailed not in labor force rule.
        /// </summary>
        /// <value>
        /// The detailed not in labor force rule.
        /// </value>
        public IRule DetailedNotInLaborForceRule { get; set; }


        #region Presentation Rules

        /// <summary>
        /// Gets or sets the employment status presentation rule.
        /// </summary>
        /// <value>
        /// The employment status presentation rule.
        /// </value>
        public IRule EmploymentStatusPresentationRule { get; set; }

        /// <summary>
        /// Gets or sets the primary substance usage presentation rule.
        /// </summary>
        /// <value>
        /// The primary substance usage presentation rule.
        /// </value>
        public IRule PrimarySubstanceUsagePresentationRule { get; set; }

        /// <summary>
        /// Gets or sets the secondary substance usage presentation rule.
        /// </summary>
        /// <value>
        /// The secondary substance usage presentation rule.
        /// </value>
        public IRule SecondarySubstanceUsagePresentationRule { get; set; }

        /// <summary>
        /// Gets or sets the tertiary substance usage presentation rule.
        /// </summary>
        /// <value>
        /// The tertiary substance usage presentation rule.
        /// </value>
        public IRule TertiarySubstanceUsagePresentationRule { get; set; }

        #endregion Presentation Rules

        #endregion

        #region Methods

        private void BuildTedsDischargeInterviewRules()
        {
            var arrestsInPastThirtyDaysCountLessThan96Error = new DataErrorInfo (
                LessThan96ErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.ArrestsInPastThirtyDaysCount ) );

            NewRule ( () => ArrestsInPastThirtyDaysCountLessThan96Rule )
                .RunForProperty ( s => s.EditingDto.ArrestsInPastThirtyDaysCount )
                .When (
                    s => s.EditingDto.ArrestsInPastThirtyDaysCount != null &&
                         s.EditingDto.ArrestsInPastThirtyDaysCount.HasValue () &&
                         ( s.EditingDto.ArrestsInPastThirtyDaysCount.Response.Value < 0
                           || s.EditingDto.ArrestsInPastThirtyDaysCount.Response.Value > 96 )
                )
                .ThenReportRuleViolation ( arrestsInPastThirtyDaysCountLessThan96Error.Message )
                .Then ( s => s.EditingDto.TryAddDataErrorInfo ( arrestsInPastThirtyDaysCountLessThan96Error ) )
                .ElseThen ( s => s.EditingDto.RemoveDataErrorInfo ( arrestsInPastThirtyDaysCountLessThan96Error ) );

            var lastFaceToFaceContactDateRequiredError = new DataErrorInfo(
                LastContactDateRequiredErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object>(dto => dto.LastFaceToFaceContactDate));

            NewRule(() => LastFaceToFaceContactDateRequiredRule)
                .RunForProperty(s => s.EditingDto.LastFaceToFaceContactDate)
                .When(s => !s.EditingDto.LastFaceToFaceContactDate.HasValue)
                .ThenReportRuleViolation(lastFaceToFaceContactDateRequiredError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(lastFaceToFaceContactDateRequiredError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(lastFaceToFaceContactDateRequiredError));

            var employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError = new DataErrorInfo (
                EmploymentErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.TedsEmploymentStatus ),
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.DetailedNotInLaborForce ) );

            NewRule ( () => EmploymentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedRule )
                .RunForProperty ( s => s.EditingDto.TedsEmploymentStatus )
                .RunForProperty ( s => s.EditingDto.DetailedNotInLaborForce )
                .When (
                    s =>
                    ( s.EditingDto.TedsEmploymentStatus == null
                      || ( s.EditingDto.TedsEmploymentStatus != null && !s.EditingDto.TedsEmploymentStatus.IsAnswered () ) ) &&
                    ( s.EditingDto.DetailedNotInLaborForce != null && s.EditingDto.DetailedNotInLaborForce.IsAnswered () )
                )
                .ThenReportRuleViolation ( employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError.Message )
                .Then (
                    s => s.EditingDto.TryAddDataErrorInfo ( employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError ) )
                .ElseThen (
                    s => s.EditingDto.RemoveDataErrorInfo ( employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError ) );

            var primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError = new DataErrorInfo (
                PrimarySubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.PrimarySubstanceProblemType ),
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.PrimaryUseFrequencyType ) );

            NewRule ( () => PrimarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredRule )
                .RunForProperty ( s => s.EditingDto.PrimarySubstanceProblemType )
                .RunForProperty ( s => s.EditingDto.PrimaryUseFrequencyType )
                .When (
                    s =>
                    ( s.EditingDto.PrimarySubstanceProblemType == null
                      || ( s.EditingDto.PrimarySubstanceProblemType != null && !s.EditingDto.PrimarySubstanceProblemType.IsAnswered () ) ) &&
                    ( s.EditingDto.PrimaryUseFrequencyType != null && s.EditingDto.PrimaryUseFrequencyType.IsAnswered () )
                )
                .ThenReportRuleViolation ( primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError ) );

            var secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError = new DataErrorInfo (
                SecondarySubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.SecondarySubstanceProblemType ),
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.SecondaryUseFrequencyType ) );

            NewRule ( () => SecondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredRule )
                .RunForProperty ( s => s.EditingDto.SecondarySubstanceProblemType )
                .RunForProperty ( s => s.EditingDto.SecondaryUseFrequencyType )
                .When (
                    s =>
                    ( s.EditingDto.SecondarySubstanceProblemType == null
                      || ( s.EditingDto.SecondarySubstanceProblemType != null && !s.EditingDto.SecondarySubstanceProblemType.IsAnswered () ) ) &&
                    ( s.EditingDto.SecondaryUseFrequencyType != null && s.EditingDto.SecondaryUseFrequencyType.IsAnswered () )
                )
                .ThenReportRuleViolation ( secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError ) );

            var tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError = new DataErrorInfo (
                TertiarySubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.TertiarySubstanceProblemType ),
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.TertiaryUseFrequencyType ) );

            NewRule ( () => TertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredRule )
                .RunForProperty ( s => s.EditingDto.TertiarySubstanceProblemType )
                .RunForProperty ( s => s.EditingDto.TertiaryUseFrequencyType )
                .When (
                    s =>
                    ( s.EditingDto.TertiarySubstanceProblemType == null
                      || ( s.EditingDto.TertiarySubstanceProblemType != null && !s.EditingDto.TertiarySubstanceProblemType.IsAnswered () ) ) &&
                    ( s.EditingDto.TertiaryUseFrequencyType != null && s.EditingDto.TertiaryUseFrequencyType.IsAnswered () )
                )
                .ThenReportRuleViolation ( tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError ) );

            var primarySecondarySubstanceMustBeAnsweredIfTertiarySubstanceUsageQuestionIsAnsweredRuleError = new DataErrorInfo (
                TertiarySecondaryPrimarySubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.TertiarySubstanceProblemType ) );

            NewRule ( () => PrimarySecondarySubstanceMustBeAnsweredIfTertiarySubstanceUsageQuestionIsAnsweredRule )
                .RunForProperty ( s => s.EditingDto.TertiarySubstanceProblemType )
                .When (
                    s =>
                    ( s.EditingDto.TertiarySubstanceProblemType != null && s.EditingDto.TertiarySubstanceProblemType.IsAnswered () ) &&
                    (
                        ( s.EditingDto.SecondarySubstanceProblemType == null
                          || ( s.EditingDto.SecondarySubstanceProblemType != null && !s.EditingDto.SecondarySubstanceProblemType.IsAnswered () ) ) ||
                        ( s.EditingDto.PrimarySubstanceProblemType == null )
                        || ( s.EditingDto.PrimarySubstanceProblemType != null && !s.EditingDto.PrimarySubstanceProblemType.IsAnswered () ) )
                )
                .ThenReportRuleViolation ( primarySecondarySubstanceMustBeAnsweredIfTertiarySubstanceUsageQuestionIsAnsweredRuleError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        primarySecondarySubstanceMustBeAnsweredIfTertiarySubstanceUsageQuestionIsAnsweredRuleError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        primarySecondarySubstanceMustBeAnsweredIfTertiarySubstanceUsageQuestionIsAnsweredRuleError ) );

            var primarySubstanceMustBeAnsweredIfSecondarySubstanceUsageQuestionIsAnsweredRuleError = new DataErrorInfo (
                SecondaryPrimarySubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.SecondarySubstanceProblemType ) );

            NewRule ( () => PrimarySubstanceMustBeAnsweredIfSecondarySubstanceUsageQuestionIsAnsweredRule )
                .RunForProperty ( s => s.EditingDto.SecondarySubstanceProblemType )
                .When (
                    s =>
                    ( s.EditingDto.SecondarySubstanceProblemType != null && s.EditingDto.SecondarySubstanceProblemType.IsAnswered () ) &&
                    (
                        s.EditingDto.PrimarySubstanceProblemType == null
                        || ( s.EditingDto.PrimarySubstanceProblemType != null && !s.EditingDto.PrimarySubstanceProblemType.IsAnswered () ) )
                )
                .ThenReportRuleViolation ( primarySubstanceMustBeAnsweredIfSecondarySubstanceUsageQuestionIsAnsweredRuleError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        primarySubstanceMustBeAnsweredIfSecondarySubstanceUsageQuestionIsAnsweredRuleError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        primarySubstanceMustBeAnsweredIfSecondarySubstanceUsageQuestionIsAnsweredRuleError ) );

            var primaryAndSecondSubstanceUsageAreDuplicatedError = new DataErrorInfo (
                DuplicatedSubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.PrimarySubstanceProblemType ),
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.SecondarySubstanceProblemType ) );

            NewRule ( () => PrimaryAndSecondSubstanceUsageAreDuplicated )
                .RunForProperty ( s => s.EditingDto.PrimarySubstanceProblemType )
                .RunForProperty ( s => s.EditingDto.SecondarySubstanceProblemType )
                .When (
                    s => s.EditingDto.PrimarySubstanceProblemType != null && s.EditingDto.PrimarySubstanceProblemType.IsAnswered () &&
                         s.EditingDto.SecondarySubstanceProblemType != null && s.EditingDto.SecondarySubstanceProblemType.IsAnswered () &&
                         s.EditingDto.PrimarySubstanceProblemType.Equals ( s.EditingDto.SecondarySubstanceProblemType )
                )
                .ThenReportRuleViolation ( primaryAndSecondSubstanceUsageAreDuplicatedError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        primaryAndSecondSubstanceUsageAreDuplicatedError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        primaryAndSecondSubstanceUsageAreDuplicatedError ) );

            var primaryAndTertiarySubstanceUsageAreDuplicatedError = new DataErrorInfo (
                DuplicatedSubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.PrimarySubstanceProblemType ),
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.TertiarySubstanceProblemType ) );

            NewRule ( () => PrimaryAndTertiarySubstanceUsageAreDuplicated )
                .RunForProperty ( s => s.EditingDto.PrimarySubstanceProblemType )
                .RunForProperty ( s => s.EditingDto.TertiarySubstanceProblemType )
                .When (
                    s =>
                    s.EditingDto.PrimarySubstanceProblemType != null && s.EditingDto.PrimarySubstanceProblemType.IsAnswered () &&
                    s.EditingDto.TertiarySubstanceProblemType != null && s.EditingDto.TertiarySubstanceProblemType.IsAnswered () &&
                    s.EditingDto.PrimarySubstanceProblemType.Equals ( s.EditingDto.TertiarySubstanceProblemType )
                )
                .ThenReportRuleViolation ( primaryAndTertiarySubstanceUsageAreDuplicatedError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        primaryAndTertiarySubstanceUsageAreDuplicatedError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        primaryAndTertiarySubstanceUsageAreDuplicatedError ) );

            var tertiaryAndSecondSubstanceUsageAreDuplicatedError = new DataErrorInfo (
                DuplicatedSubstanceUsedErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.SecondarySubstanceProblemType ),
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.TertiarySubstanceProblemType ) );

            NewRule ( () => TertiaryAndSecondSubstanceUsageAreDuplicated )
                .RunForProperty ( s => s.EditingDto.SecondarySubstanceProblemType )
                .RunForProperty ( s => s.EditingDto.TertiarySubstanceProblemType )
                .When (
                    s =>
                    s.EditingDto.TertiarySubstanceProblemType != null && s.EditingDto.TertiarySubstanceProblemType.IsAnswered () &&
                    s.EditingDto.SecondarySubstanceProblemType != null && s.EditingDto.SecondarySubstanceProblemType.IsAnswered () &&
                    s.EditingDto.TertiarySubstanceProblemType.Equals ( s.EditingDto.SecondarySubstanceProblemType )
                )
                .ThenReportRuleViolation ( tertiaryAndSecondSubstanceUsageAreDuplicatedError.Message )
                .Then (
                    s =>
                    s.EditingDto.TryAddDataErrorInfo (
                        tertiaryAndSecondSubstanceUsageAreDuplicatedError ) )
                .ElseThen (
                    s =>
                    s.EditingDto.RemoveDataErrorInfo (
                        tertiaryAndSecondSubstanceUsageAreDuplicatedError ) );

            var detailedNotInLaborForceError = new DataErrorInfo (
                DetailedNotInLaborForceErrorMsg,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsDischargeInterviewDto, object> ( dto => dto.DetailedNotInLaborForce ) );

            NewRule ( () => DetailedNotInLaborForceRule )
                .RunForProperty ( s => s.EditingDto.TedsEmploymentStatus )
                .RunForProperty ( s => s.EditingDto.DetailedNotInLaborForce )
                .When (
                    s => !( s.EditingDto.TedsEmploymentStatus != null &&
                            s.EditingDto.TedsEmploymentStatus.Response != null &&
                            s.EditingDto.TedsEmploymentStatus.Response.WellKnownName == WellKnownNames.TedsModule.TedsEmploymentStatus.NotInLaborForce ) &&
                         ( s.EditingDto.DetailedNotInLaborForce != null &&
                           ( s.EditingDto.DetailedNotInLaborForce.Response != null ||
                             ( s.EditingDto.DetailedNotInLaborForce.NonResponse != null &&
                               s.EditingDto.DetailedNotInLaborForce.NonResponse.WellKnownName
                               != WellKnownNames.TedsModule.TedsNonResponse.NotApplicable ) ) )
                )
                .ThenReportRuleViolation ( detailedNotInLaborForceError.Message )
                .Then ( s => s.EditingDto.TryAddDataErrorInfo ( detailedNotInLaborForceError ) )
                .ElseThen ( s => s.EditingDto.RemoveDataErrorInfo ( detailedNotInLaborForceError ) );
        }

        private void BuildPresentaionRules()
        {
            NewRule(() => EmploymentStatusPresentationRule)
                .RunForProperty(s => s.EditingDto.TedsEmploymentStatus)
                .When(
                    s => s.EditingDto.TedsEmploymentStatus.HasValue() &&
                         s.EditingDto.TedsEmploymentStatus.Response.WellKnownName == WellKnownNames.TedsModule.TedsEmploymentStatus.NotInLaborForce)
                .Then(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.DetailedNotInLaborForce);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.DetailedNotInLaborForce.SetAsNotAnswered();
                        s.EditingDto.Disable(dto => dto.DetailedNotInLaborForce);
                    });

            NewRule(() => PrimarySubstanceUsagePresentationRule)
                .RunForProperty(s => s.EditingDto.PrimarySubstanceProblemType)
                .When(
                    s => (!s.EditingDto.PrimarySubstanceProblemType.IsAnswered() ||
                        !s.EditingDto.PrimarySubstanceProblemType.HasValue() ||
                        (s.EditingDto.PrimarySubstanceProblemType.Response.WellKnownName == WellKnownNames.TedsModule.SubstanceProblemType.None))
                    )
                .Then(
                    s =>
                    {
                        s.EditingDto.PrimaryUseFrequencyType.SetAsNotAnswered();
                        s.EditingDto.Disable(dto => dto.PrimaryUseFrequencyType);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.PrimaryUseFrequencyType);
                    });

            NewRule(() => SecondarySubstanceUsagePresentationRule)
                .RunForProperty(s => s.EditingDto.SecondarySubstanceProblemType)
                .When(
                    s => (!s.EditingDto.SecondarySubstanceProblemType.IsAnswered() ||
                        !s.EditingDto.SecondarySubstanceProblemType.HasValue() ||
                        (s.EditingDto.SecondarySubstanceProblemType.Response.WellKnownName == WellKnownNames.TedsModule.SubstanceProblemType.None))
                    )
                .Then(
                    s =>
                    {
                        s.EditingDto.SecondaryUseFrequencyType.SetAsNotAnswered();
                        s.EditingDto.Disable(dto => dto.SecondaryUseFrequencyType);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.SecondaryUseFrequencyType);
                    });

            NewRule(() => TertiarySubstanceUsagePresentationRule)
                .RunForProperty(s => s.EditingDto.TertiarySubstanceProblemType)
                .When(
                    s => (!s.EditingDto.TertiarySubstanceProblemType.IsAnswered() ||
                        !s.EditingDto.TertiarySubstanceProblemType.HasValue() ||
                        (s.EditingDto.TertiarySubstanceProblemType.Response.WellKnownName == WellKnownNames.TedsModule.SubstanceProblemType.None))
                    )
                .Then(
                    s =>
                    {
                        s.EditingDto.TertiaryUseFrequencyType.SetAsNotAnswered();
                        s.EditingDto.Disable(dto => dto.TertiaryUseFrequencyType);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.TertiaryUseFrequencyType);
                    });
        }

        #endregion
    }
}