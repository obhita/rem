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
using System.Linq;
using Pillar.Common.Metadata;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.TedsInterview;

namespace Rem.Ria.PatientModule.TedsInterview
{
    /// <summary>
    /// Contains client side business rules for TEDS admission Interview.
    /// </summary>
    public class TedsAdmissionInterviewViewModelRuleCollection : AbstractRuleCollection<TedsAdmissionInterviewViewModel>
    {
        private static readonly string FirstUseAgeErrorMessage = "Age of first use must not less than zero.";
        private static readonly string TedsNonResponseTypeName = "TedsNonResponse";

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionInterviewViewModelRuleCollection"/> class.
        /// </summary>
        public TedsAdmissionInterviewViewModelRuleCollection ()
        {
            // Validation rules
            var educationYearCountMustNotLessThanZeroError = new DataErrorInfo(
                "Years of school (highest grade) completed must not less than zero.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsEducationYearCount));

            NewRule(() => EducationYearCountMustNotLessThanZeroRule)
                .RunForProperty(s => s.EditingDto.TedsEducationYearCount)
                .When(
                    s => s.EditingDto.TedsEducationYearCount != null &&
                         s.EditingDto.TedsEducationYearCount.HasValue() 
                         && s.EditingDto.TedsEducationYearCount.Response.Value < 0
                )
                .ThenReportRuleViolation(educationYearCountMustNotLessThanZeroError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(educationYearCountMustNotLessThanZeroError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(educationYearCountMustNotLessThanZeroError));

            var primaryFirstUseAgeMustNotLessThanZeroError = new DataErrorInfo(
                FirstUseAgeErrorMessage,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.PrimaryFirstUseAge));

            NewRule(() => PrimaryFirstUseAgeMustNotLessThanZeroRule)
                .RunForProperty(s => s.EditingDto.PrimaryFirstUseAge)
                .When(
                    s => s.EditingDto.PrimaryFirstUseAge != null &&
                         s.EditingDto.PrimaryFirstUseAge.HasValue()
                         && s.EditingDto.PrimaryFirstUseAge.Response.Value < 0
                )
                .ThenReportRuleViolation(primaryFirstUseAgeMustNotLessThanZeroError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(primaryFirstUseAgeMustNotLessThanZeroError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(primaryFirstUseAgeMustNotLessThanZeroError));

            var secondaryFirstUseAgeMustNotLessThanZeroError = new DataErrorInfo(
                FirstUseAgeErrorMessage,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondaryFirstUseAge));

            NewRule(() => SecondaryFirstUseAgeMustNotLessThanZeroRule)
                .RunForProperty(s => s.EditingDto.SecondaryFirstUseAge)
                .When(
                    s => s.EditingDto.SecondaryFirstUseAge != null &&
                         s.EditingDto.SecondaryFirstUseAge.HasValue()
                         && s.EditingDto.SecondaryFirstUseAge.Response.Value < 0
                )
                .ThenReportRuleViolation(secondaryFirstUseAgeMustNotLessThanZeroError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(secondaryFirstUseAgeMustNotLessThanZeroError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(secondaryFirstUseAgeMustNotLessThanZeroError));

            var tertiaryFirstUseAgeMustNotLessThanZeroError = new DataErrorInfo(
                FirstUseAgeErrorMessage,
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryFirstUseAge));

            NewRule(() => TertiaryFirstUseAgeMustNotLessThanZeroRule)
                .RunForProperty(s => s.EditingDto.TertiaryFirstUseAge)
                .When(
                    s => s.EditingDto.TertiaryFirstUseAge != null &&
                         s.EditingDto.TertiaryFirstUseAge.HasValue()
                         && s.EditingDto.TertiaryFirstUseAge.Response.Value < 0
                )
                .ThenReportRuleViolation(tertiaryFirstUseAgeMustNotLessThanZeroError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(tertiaryFirstUseAgeMustNotLessThanZeroError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(tertiaryFirstUseAgeMustNotLessThanZeroError));

            var genderNotAnsweredAndPregnantIndicatorAnsweredShouldNotBeAllowedError = new DataErrorInfo(
               "Pregnant indicator is answered but gender is not answered.",
               ErrorLevel.Error,
               PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsGenderInformationTedsGender),
               PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsGenderInformationPregnantIndicator));

            NewRule(() => GenderNotAnsweredAndPregnantIndicatorAnsweredShouldNotBeAllowedRule)
                .RunForProperty(s => s.EditingDto.TedsGenderInformationTedsGender)
                .RunForProperty(s => s.EditingDto.TedsGenderInformationPregnantIndicator)
                .When(
                    s => (s.EditingDto.TedsGenderInformationTedsGender == null || (s.EditingDto.TedsGenderInformationTedsGender != null && !s.EditingDto.TedsGenderInformationTedsGender.IsAnswered())) &&
                         (s.EditingDto.TedsGenderInformationPregnantIndicator != null && s.EditingDto.TedsGenderInformationPregnantIndicator.IsAnswered())
                         )
                .ThenReportRuleViolation(genderNotAnsweredAndPregnantIndicatorAnsweredShouldNotBeAllowedError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(genderNotAnsweredAndPregnantIndicatorAnsweredShouldNotBeAllowedError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(genderNotAnsweredAndPregnantIndicatorAnsweredShouldNotBeAllowedError));

            var genderAndPregnantIndicatorMustMatchError = new DataErrorInfo(
                "For non-female client, pregnant information should be coded as Not Applicable.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsGenderInformationTedsGender),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsGenderInformationPregnantIndicator));

            NewRule(() => GenderAndPregnantIndicatorMustMatchRule)
                .RunForProperty(s => s.EditingDto.TedsGenderInformationTedsGender)
                .RunForProperty(s => s.EditingDto.TedsGenderInformationPregnantIndicator)
                .When(
                    s =>!(s.EditingDto.TedsGenderInformationTedsGender == null || (s.EditingDto.TedsGenderInformationTedsGender != null && !s.EditingDto.TedsGenderInformationTedsGender.IsAnswered())) && (s.EditingDto.TedsGenderInformationPregnantIndicator != null && s.EditingDto.TedsGenderInformationPregnantIndicator.IsAnswered()) && 
                        !(s.EditingDto.TedsGenderInformationTedsGender != null && s.EditingDto.TedsGenderInformationTedsGender.HasValue() && s.EditingDto.TedsGenderInformationTedsGender.Response.WellKnownName == WellKnownNames.TedsModule.TedsGender.Female) &&
                         (s.EditingDto.TedsGenderInformationPregnantIndicator != null && s.EditingDto.TedsGenderInformationPregnantIndicator.IsAnswered() && (s.EditingDto.TedsGenderInformationPregnantIndicator.HasValue() || s.EditingDto.TedsGenderInformationPregnantIndicator.TedsNonResponse.WellKnownName != WellKnownNames.TedsModule.TedsNonResponse.NotApplicable))
                         )
                .ThenReportRuleViolation(genderAndPregnantIndicatorMustMatchError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(genderAndPregnantIndicatorMustMatchError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(genderAndPregnantIndicatorMustMatchError));

            var employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError = new DataErrorInfo(
                "Detailed not in labor force is answered but employment status is not answered.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsEmploymentStatusInformationTedsEmploymentStatus),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsEmploymentStatusInformationDetailedNotInLaborForce));

            NewRule(() => EmploymentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedRule)
                .RunForProperty(s => s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus)
                .RunForProperty(s => s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce)
                .When(
                    s => (s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus == null || (s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus != null && !s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus.IsAnswered())) &&
                         (s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce != null && s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce.IsAnswered())
                         )
                .ThenReportRuleViolation(employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(employmentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedError));

            var employmentStatusAndDetailedNotInLaborForceMustMatchError = new DataErrorInfo(
                "Detailed not in labor force information should be coded as not applicable if employment status information is coded as full time, part time, unemployed, or unknown.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsEmploymentStatusInformationTedsEmploymentStatus),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TedsEmploymentStatusInformationDetailedNotInLaborForce));

            NewRule(() => EmploymentStatusAndDetailedNotInLaborForceMustMatchRule)
                .RunForProperty(s => s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus)
                .RunForProperty(s => s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce)
                .When(
                    s => !(s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus == null || (s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus != null && !s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus.IsAnswered())) && (s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce != null && s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce.IsAnswered()) && 
                        (s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus != null && s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus.IsAnswered()) && 
                        !(s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus.HasValue() && s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus.Response.WellKnownName == WellKnownNames.TedsModule.TedsEmploymentStatus.NotInLaborForce) &&
                         ((s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce != null && s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce.IsAnswered() && (s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce.HasValue() || (!s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce.HasValue() && s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce.TedsNonResponse.WellKnownName != WellKnownNames.TedsModule.TedsNonResponse.NotApplicable))))
                         )
                .ThenReportRuleViolation(employmentStatusAndDetailedNotInLaborForceMustMatchError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(employmentStatusAndDetailedNotInLaborForceMustMatchError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(employmentStatusAndDetailedNotInLaborForceMustMatchError));

            var primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError = new DataErrorInfo(
                "Primary substance used question must be answered if any other primary substance usage question is answered.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.PrimarySubstanceProblemType));

            NewRule(() => PrimarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredRule)
                .RunForProperty(s => s.EditingDto.PrimarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.PrimaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.PrimaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.PrimaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.PrimaryDetailedDrugCode)
                .When(
                    s => (s.EditingDto.PrimarySubstanceProblemType == null || (s.EditingDto.PrimarySubstanceProblemType != null && !s.EditingDto.PrimarySubstanceProblemType.IsAnswered())) &&
                         (
                         (s.EditingDto.PrimaryUseFrequencyType != null && s.EditingDto.PrimaryUseFrequencyType.IsAnswered()) ||
                         (s.EditingDto.PrimaryUsualAdministrationRouteType != null && s.EditingDto.PrimaryUsualAdministrationRouteType.IsAnswered()) ||
                         (s.EditingDto.PrimaryFirstUseAge != null && s.EditingDto.PrimaryFirstUseAge.IsAnswered()) ||
                         (s.EditingDto.PrimaryDetailedDrugCode != null && s.EditingDto.PrimaryDetailedDrugCode.IsAnswered())
                         )
                    )
                .ThenReportRuleViolation(primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(primarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredError));

            var secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredError = new DataErrorInfo(
                "Secondary substance used question must be answered if any other secondary substance usage question is answered.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondarySubstanceProblemType));

            NewRule(() => SecondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredRule)
                .RunForProperty(s => s.EditingDto.SecondarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.SecondaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.SecondaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.SecondaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.SecondaryDetailedDrugCode)
                .When(
                    s => (s.EditingDto.SecondarySubstanceProblemType == null || (s.EditingDto.SecondarySubstanceProblemType != null && !s.EditingDto.SecondarySubstanceProblemType.IsAnswered())) &&
                         (
                         (s.EditingDto.SecondaryUseFrequencyType != null && s.EditingDto.SecondaryUseFrequencyType.IsAnswered()) ||
                         (s.EditingDto.SecondaryUsualAdministrationRouteType != null && s.EditingDto.SecondaryUsualAdministrationRouteType.IsAnswered()) ||
                         (s.EditingDto.SecondaryFirstUseAge != null && s.EditingDto.SecondaryFirstUseAge.IsAnswered()) ||
                         (s.EditingDto.SecondaryDetailedDrugCode != null && s.EditingDto.SecondaryDetailedDrugCode.IsAnswered())
                         )
                    )
                .ThenReportRuleViolation(secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(secondarySubstanceProblemTypeMustBeAnsweredIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredError));

            var tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredError = new DataErrorInfo(
                "Tertiary substance used question must be answered if any other secondary substance usage question is answered.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiarySubstanceProblemType));

            NewRule(() => TertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredRule)
                .RunForProperty(s => s.EditingDto.TertiarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.TertiaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.TertiaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.TertiaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.TertiaryDetailedDrugCode)
                .When(
                    s => (s.EditingDto.TertiarySubstanceProblemType == null || (s.EditingDto.TertiarySubstanceProblemType != null && !s.EditingDto.TertiarySubstanceProblemType.IsAnswered())) &&
                         (
                         (s.EditingDto.TertiaryUseFrequencyType != null && s.EditingDto.TertiaryUseFrequencyType.IsAnswered()) ||
                         (s.EditingDto.TertiaryUsualAdministrationRouteType != null && s.EditingDto.TertiaryUsualAdministrationRouteType.IsAnswered()) ||
                         (s.EditingDto.TertiaryFirstUseAge != null && s.EditingDto.TertiaryFirstUseAge.IsAnswered()) ||
                         (s.EditingDto.TertiaryDetailedDrugCode != null && s.EditingDto.TertiaryDetailedDrugCode.IsAnswered())
                         )
                    )
                .ThenReportRuleViolation(tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(tertiarySubstanceProblemTypeMustBeAnsweredIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredError));

            var primarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredWithResponseError = new DataErrorInfo(
                "Primary substance used question must be answered with substance if any other primary substance usage question is answered with response.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.PrimarySubstanceProblemType));

            NewRule(() => PrimarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredWithResponseRule)
                .RunForProperty(s => s.EditingDto.PrimarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.PrimaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.PrimaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.PrimaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.PrimaryDetailedDrugCode)
                .When(
                    s => (!s.EditingDto.PrimarySubstanceProblemType.IsAnswered() || !s.EditingDto.PrimarySubstanceProblemType.HasValue() || (s.EditingDto.PrimarySubstanceProblemType.Response.WellKnownName == WellKnownNames.TedsModule.SubstanceProblemType.None)) &&
                         (
                             (s.EditingDto.PrimaryUseFrequencyType.HasValue()) ||
                             (s.EditingDto.PrimaryUsualAdministrationRouteType.HasValue()) ||
                             (s.EditingDto.PrimaryFirstUseAge.HasValue()) ||
                             (s.EditingDto.PrimaryDetailedDrugCode.HasValue())
                         )
                    )
                .ThenReportRuleViolation(primarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredWithResponseError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(primarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredWithResponseError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(primarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredWithResponseError));

            var secondarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredWithResponseError = new DataErrorInfo(
                "Secondary substance used question must be answered with substance if any other secondary substance usage question is answered with response.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondarySubstanceProblemType));

            NewRule(() => SecondarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredWithResponseRule)
                .RunForProperty(s => s.EditingDto.SecondarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.SecondaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.SecondaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.SecondaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.SecondaryDetailedDrugCode)
                .When(
                    s => (!s.EditingDto.SecondarySubstanceProblemType.IsAnswered() || !s.EditingDto.SecondarySubstanceProblemType.HasValue() || (s.EditingDto.SecondarySubstanceProblemType.Response.WellKnownName == WellKnownNames.TedsModule.SubstanceProblemType.None)) &&
                         (
                             (s.EditingDto.SecondaryUseFrequencyType.HasValue()) ||
                             (s.EditingDto.SecondaryUsualAdministrationRouteType.HasValue()) ||
                             (s.EditingDto.SecondaryFirstUseAge.HasValue()) ||
                             (s.EditingDto.SecondaryDetailedDrugCode.HasValue())
                         )
                    )
                .ThenReportRuleViolation(secondarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredWithResponseError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(secondarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredWithResponseError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(secondarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredWithResponseError));

            var tertiarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredWithResponseError = new DataErrorInfo(
                "Tertiary substance used question must be answered with substance if any other tertiary substance usage question is answered with response.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiarySubstanceProblemType));

            NewRule(() => TertiarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredWithResponseRule)
                .RunForProperty(s => s.EditingDto.TertiarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.TertiaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.TertiaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.TertiaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.TertiaryDetailedDrugCode)
                .When(
                    s => (!s.EditingDto.TertiarySubstanceProblemType.IsAnswered() || !s.EditingDto.TertiarySubstanceProblemType.HasValue() || (s.EditingDto.TertiarySubstanceProblemType.Response.WellKnownName == WellKnownNames.TedsModule.SubstanceProblemType.None)) &&
                         (
                             (s.EditingDto.TertiaryUseFrequencyType.HasValue()) ||
                             (s.EditingDto.TertiaryUsualAdministrationRouteType.HasValue()) ||
                             (s.EditingDto.TertiaryFirstUseAge.HasValue()) ||
                             (s.EditingDto.TertiaryDetailedDrugCode.HasValue())
                         )
                    )
                .ThenReportRuleViolation(tertiarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredWithResponseError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(tertiarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredWithResponseError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(tertiarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredWithResponseError));

            var secondarySubstanceUsageMustBeAnsweredIfTertiarySubstanceUsageIsAnsweredError = new DataErrorInfo(
                "Secondary substance usage must be answered if tertiary substanceUsage is answered.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondarySubstanceProblemType));

            NewRule(() => SecondarySubstanceUsageMustBeAnsweredIfTertiarySubstanceUsageIsAnsweredRule)
                .RunForProperty(s => s.EditingDto.SecondarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.SecondaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.SecondaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.SecondaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.SecondaryDetailedDrugCode)

                .RunForProperty(s => s.EditingDto.TertiarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.TertiaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.TertiaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.TertiaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.TertiaryDetailedDrugCode)
                .When(
                    s => (s.EditingDto.TertiarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.TertiaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.TertiaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.TertiaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.TertiaryDetailedDrugCode.IsAnswered()
                          ) &&
                          ! (
                            s.EditingDto.SecondarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.SecondaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.SecondaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.SecondaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.SecondaryDetailedDrugCode.IsAnswered()
                            )
                    )
                .ThenReportRuleViolation(secondarySubstanceUsageMustBeAnsweredIfTertiarySubstanceUsageIsAnsweredError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(secondarySubstanceUsageMustBeAnsweredIfTertiarySubstanceUsageIsAnsweredError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(secondarySubstanceUsageMustBeAnsweredIfTertiarySubstanceUsageIsAnsweredError));

            var primarySubstanceUsageMustBeAnsweredIfSecondarySubstanceUsageIsAnsweredError = new DataErrorInfo(
                "Primary substance usage must be answered if secondary substanceUsage is answered.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.PrimarySubstanceProblemType));

            NewRule(() => PrimarySubstanceUsageMustBeAnsweredIfSecondarySubstanceUsageIsAnsweredRule)
                .RunForProperty(s => s.EditingDto.PrimarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.PrimaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.PrimaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.PrimaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.PrimaryDetailedDrugCode)

                .RunForProperty(s => s.EditingDto.SecondarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.SecondaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.SecondaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.SecondaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.SecondaryDetailedDrugCode)

                .When(
                    s => (s.EditingDto.SecondarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.SecondaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.SecondaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.SecondaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.SecondaryDetailedDrugCode.IsAnswered()
                          ) &&
                          !(
                            s.EditingDto.PrimarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.PrimaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.PrimaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.PrimaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.PrimaryDetailedDrugCode.IsAnswered()
                            )
                    )
                .ThenReportRuleViolation(primarySubstanceUsageMustBeAnsweredIfSecondarySubstanceUsageIsAnsweredError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(primarySubstanceUsageMustBeAnsweredIfSecondarySubstanceUsageIsAnsweredError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(primarySubstanceUsageMustBeAnsweredIfSecondarySubstanceUsageIsAnsweredError));

            var secondarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError = new DataErrorInfo(
                "Secondary Substance usage can not be same as primary substance usage.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondarySubstanceProblemType),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondaryDetailedDrugCode),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondaryFirstUseAge),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondaryUseFrequencyType),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.SecondaryUsualAdministrationRouteType));

            NewRule(() => SecondarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageRule)
                .RunForProperty(s => s.EditingDto.PrimarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.PrimaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.PrimaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.PrimaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.PrimaryDetailedDrugCode)

                .RunForProperty(s => s.EditingDto.SecondarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.SecondaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.SecondaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.SecondaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.SecondaryDetailedDrugCode)

                .When(
                    s => (  s.EditingDto.SecondarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.SecondaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.SecondaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.SecondaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.SecondaryDetailedDrugCode.IsAnswered()
                          ) &&
                          (
                            s.EditingDto.PrimarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.PrimaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.PrimaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.PrimaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.PrimaryDetailedDrugCode.IsAnswered()
                          ) &&
                          ( s.EditingDto.SecondarySubstanceProblemType.Equals(s.EditingDto.PrimarySubstanceProblemType) &&
                            s.EditingDto.SecondaryUseFrequencyType.Equals(s.EditingDto.PrimaryUseFrequencyType) &&
                            s.EditingDto.SecondaryUsualAdministrationRouteType.Equals(s.EditingDto.PrimaryUsualAdministrationRouteType) &&
                            s.EditingDto.SecondaryFirstUseAge.Equals(s.EditingDto.PrimaryFirstUseAge) &&
                            s.EditingDto.SecondaryDetailedDrugCode.Equals(s.EditingDto.PrimaryDetailedDrugCode) 
                          )
                    )
                .ThenReportRuleViolation(secondarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(secondarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(secondarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError));

            var tertiarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError = new DataErrorInfo(
                "Tertiary Substance usage can not be same as primary substance usage.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiarySubstanceProblemType),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryDetailedDrugCode),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryFirstUseAge),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryUseFrequencyType),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryUsualAdministrationRouteType));

            NewRule(() => TertiarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageRule)
                .RunForProperty(s => s.EditingDto.PrimarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.PrimaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.PrimaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.PrimaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.PrimaryDetailedDrugCode)

                .RunForProperty(s => s.EditingDto.TertiarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.TertiaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.TertiaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.TertiaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.TertiaryDetailedDrugCode)

                .When(
                    s => (  s.EditingDto.TertiarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.TertiaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.TertiaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.TertiaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.TertiaryDetailedDrugCode.IsAnswered()
                          ) &&
                          (
                            s.EditingDto.PrimarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.PrimaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.PrimaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.PrimaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.PrimaryDetailedDrugCode.IsAnswered()
                          ) && 
                          ( s.EditingDto.TertiarySubstanceProblemType.Equals(s.EditingDto.PrimarySubstanceProblemType) &&
                            s.EditingDto.TertiaryUseFrequencyType.Equals(s.EditingDto.PrimaryUseFrequencyType) &&
                            s.EditingDto.TertiaryUsualAdministrationRouteType.Equals(s.EditingDto.PrimaryUsualAdministrationRouteType) &&
                            s.EditingDto.TertiaryFirstUseAge.Equals(s.EditingDto.PrimaryFirstUseAge) &&
                            s.EditingDto.TertiaryDetailedDrugCode.Equals(s.EditingDto.PrimaryDetailedDrugCode)
                          )
                    )
                .ThenReportRuleViolation(tertiarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(tertiarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(tertiarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageError));

            var tertiarySubstanceUsageCanNotBeSameAsSecondarySubstanceUsageError = new DataErrorInfo(
                "Tertiary Substance usage can not be same as secondary substance usage.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiarySubstanceProblemType),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryDetailedDrugCode),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryFirstUseAge),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryUseFrequencyType),
                PropertyUtil.ExtractPropertyName<TedsAdmissionInterviewDto, object>(dto => dto.TertiaryUsualAdministrationRouteType));

            NewRule(() => TertiarySubstanceUsageCanNotBeSameAsSecondarySubstanceUsageRule)
                .RunForProperty(s => s.EditingDto.SecondarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.SecondaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.SecondaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.SecondaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.SecondaryDetailedDrugCode)

                .RunForProperty(s => s.EditingDto.TertiarySubstanceProblemType)
                .RunForProperty(s => s.EditingDto.TertiaryUseFrequencyType)
                .RunForProperty(s => s.EditingDto.TertiaryUsualAdministrationRouteType)
                .RunForProperty(s => s.EditingDto.TertiaryFirstUseAge)
                .RunForProperty(s => s.EditingDto.TertiaryDetailedDrugCode)

                .When(
                    s => (  s.EditingDto.TertiarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.TertiaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.TertiaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.TertiaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.TertiaryDetailedDrugCode.IsAnswered()
                          ) &&
                          (
                            s.EditingDto.SecondarySubstanceProblemType.IsAnswered() ||
                            s.EditingDto.SecondaryUseFrequencyType.IsAnswered() ||
                            s.EditingDto.SecondaryUsualAdministrationRouteType.IsAnswered() ||
                            s.EditingDto.SecondaryFirstUseAge.IsAnswered() ||
                            s.EditingDto.SecondaryDetailedDrugCode.IsAnswered()
                          ) && 
                          ( s.EditingDto.TertiarySubstanceProblemType.Equals(s.EditingDto.SecondarySubstanceProblemType) &&
                            s.EditingDto.TertiaryUseFrequencyType.Equals(s.EditingDto.SecondaryUseFrequencyType) &&
                            s.EditingDto.TertiaryUsualAdministrationRouteType.Equals(s.EditingDto.SecondaryUsualAdministrationRouteType) &&
                            s.EditingDto.TertiaryFirstUseAge.Equals(s.EditingDto.SecondaryFirstUseAge) &&
                            s.EditingDto.TertiaryDetailedDrugCode.Equals(s.EditingDto.SecondaryDetailedDrugCode)
                          )
                    )
                .ThenReportRuleViolation(tertiarySubstanceUsageCanNotBeSameAsSecondarySubstanceUsageError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(tertiarySubstanceUsageCanNotBeSameAsSecondarySubstanceUsageError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(tertiarySubstanceUsageCanNotBeSameAsSecondarySubstanceUsageError));

            BuildPresentaionRules();
        }

        /// <summary>
        /// Gets or sets the education year count must not less than zero rule.
        /// </summary>
        /// <value>
        /// The education year count must not less than zero rule.
        /// </value>
        public IRule EducationYearCountMustNotLessThanZeroRule { get; set; }

        /// <summary>
        /// Gets or sets the primary first use age must not less than zero rule.
        /// </summary>
        /// <value>
        /// The primary first use age must not less than zero rule.
        /// </value>
        public IRule PrimaryFirstUseAgeMustNotLessThanZeroRule { get; set; }

        /// <summary>
        /// Gets or sets the secondary first use age must not less than zero rule.
        /// </summary>
        /// <value>
        /// The secondary first use age must not less than zero rule.
        /// </value>
        public IRule SecondaryFirstUseAgeMustNotLessThanZeroRule { get; set; }

        /// <summary>
        /// Gets or sets the tertiary first use age must not less than zero rule.
        /// </summary>
        /// <value>
        /// The tertiary first use age must not less than zero rule.
        /// </value>
        public IRule TertiaryFirstUseAgeMustNotLessThanZeroRule { get; set; }

        /// <summary>
        /// Gets or sets the gender not answered and pregnant indicator answered should not be allowed rule.
        /// </summary>
        /// <value>
        /// The gender not answered and pregnant indicator answered should not be allowed rule.
        /// </value>
        public IRule GenderNotAnsweredAndPregnantIndicatorAnsweredShouldNotBeAllowedRule { get; set; }

        /// <summary>
        /// Gets or sets the gender and pregnant indicator must match rule.
        /// </summary>
        /// <value>
        /// The gender and pregnant indicator must match rule.
        /// </value>
        public IRule GenderAndPregnantIndicatorMustMatchRule { get; set; }

        /// <summary>
        /// Gets or sets the employment status not answered and detailed not in labor force answered should not be allowed rule.
        /// </summary>
        /// <value>
        /// The employment status not answered and detailed not in labor force answered should not be allowed rule.
        /// </value>
        public IRule EmploymentStatusNotAnsweredAndDetailedNotInLaborForceAnsweredShouldNotBeAllowedRule { get; set; }

        /// <summary>
        /// Gets or sets the employment status and detailed not in labor force must match rule.
        /// </summary>
        /// <value>
        /// The employment status and detailed not in labor force must match rule.
        /// </value>
        public IRule EmploymentStatusAndDetailedNotInLaborForceMustMatchRule { get; set; }

        /// <summary>
        /// Gets or sets the primary substance problem type must be answered if any other primary substance usage question is answered rule.
        /// </summary>
        /// <value>
        /// The primary substance problem type must be answered if any other primary substance usage question is answered rule.
        /// </value>
        public IRule PrimarySubstanceProblemTypeMustBeAnsweredIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the primary substance problem type must be answered with substance if any other primary substance usage question is answered with response rule.
        /// </summary>
        /// <value>
        /// The primary substance problem type must be answered with substance if any other primary substance usage question is answered with response rule.
        /// </value>
        public IRule PrimarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherPrimarySubstanceUsageQuestionIsAnsweredWithResponseRule { get; set; }

        /// <summary>
        /// Gets or sets the secondary substance problem type must be answered with substance if any other secondary substance usage question is answered with response rule.
        /// </summary>
        /// <value>
        /// The secondary substance problem type must be answered with substance if any other secondary substance usage question is answered with response rule.
        /// </value>
        public IRule SecondarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherSecondarySubstanceUsageQuestionIsAnsweredWithResponseRule { get; set; }

        /// <summary>
        /// Gets or sets the tertiary substance problem type must be answered with substance if any other tertiary substance usage question is answered with response rule.
        /// </summary>
        /// <value>
        /// The tertiary substance problem type must be answered with substance if any other tertiary substance usage question is answered with response rule.
        /// </value>
        public IRule TertiarySubstanceProblemTypeMustBeAnsweredWithSubstanceIfAnyOtherTertiarySubstanceUsageQuestionIsAnsweredWithResponseRule { get; set; }

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
        /// Gets or sets the secondary substance usage must be answered if tertiary substance usage is answered rule.
        /// </summary>
        /// <value>
        /// The secondary substance usage must be answered if tertiary substance usage is answered rule.
        /// </value>
        public IRule SecondarySubstanceUsageMustBeAnsweredIfTertiarySubstanceUsageIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the primary substance usage must be answered if secondary substance usage is answered rule.
        /// </summary>
        /// <value>
        /// The primary substance usage must be answered if secondary substance usage is answered rule.
        /// </value>
        public IRule PrimarySubstanceUsageMustBeAnsweredIfSecondarySubstanceUsageIsAnsweredRule { get; set; }

        /// <summary>
        /// Gets or sets the secondary substance usage can not be same as primary substance usage rule.
        /// </summary>
        /// <value>
        /// The secondary substance usage can not be same as primary substance usage rule.
        /// </value>
        public IRule SecondarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageRule { get; set; }

        /// <summary>
        /// Gets or sets the tertiary substance usage can not be same as primary substance usage rule.
        /// </summary>
        /// <value>
        /// The tertiary substance usage can not be same as primary substance usage rule.
        /// </value>
        public IRule TertiarySubstanceUsageCanNotBeSameAsPrimarySubstanceUsageRule { get; set; }

        /// <summary>
        /// Gets or sets the tertiary substance usage can not be same as secondary substance usage rule.
        /// </summary>
        /// <value>
        /// The tertiary substance usage can not be same as secondary substance usage rule.
        /// </value>
        public IRule TertiarySubstanceUsageCanNotBeSameAsSecondarySubstanceUsageRule { get; set; }

        #region --- Begin Presentation Rules ---

        /// <summary>
        /// Gets or sets the gender information presentation rule.
        /// </summary>
        /// <value>
        /// The gender information presentation rule.
        /// </value>
        public IRule GenderInformationPresentationRule { get; set; }

        /// <summary>
        /// Gets or sets the employment status information presentation rule.
        /// </summary>
        /// <value>
        /// The employment status information presentation rule.
        /// </value>
        public IRule EmploymentStatusInformationPresentationRule { get; set; }

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

        #endregion --- End Presentation Rules ---

        /// <summary>
        /// Builds the presentation rules.
        /// </summary>
        public void BuildPresentaionRules()
        {
            NewRule(() => GenderInformationPresentationRule)
                .RunForProperty(s => s.EditingDto.TedsGenderInformationTedsGender)
                .When(
                    s => !(s.EditingDto.TedsGenderInformationTedsGender.HasValue() &&
                         s.EditingDto.TedsGenderInformationTedsGender.Response.WellKnownName == WellKnownNames.TedsModule.TedsGender.Female))
                .Then(
                    s =>
                    {
                        if (s.EditingDto.TedsGenderInformationTedsGender.HasValue() && s.EditingDto.TedsGenderInformationTedsGender.Response.WellKnownName == WellKnownNames.TedsModule.TedsGender.Male)
                        {
                            s.EditingDto.TedsGenderInformationPregnantIndicator.Response = null;
                            s.EditingDto.TedsGenderInformationPregnantIndicator.NonResponse = s.LookupValueLists[TedsNonResponseTypeName].ToList().FirstOrDefault(p => p.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable);
                        }
                        else
                        {
                            s.EditingDto.TedsGenderInformationPregnantIndicator.SetAsNotAnswered();
                        }
                        s.EditingDto.Disable(dto => dto.TedsGenderInformationPregnantIndicator);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.TedsGenderInformationPregnantIndicator);
                    });

            NewRule(() => EmploymentStatusInformationPresentationRule)
                .RunForProperty(s => s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus)
                .When(
                    s => s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus.HasValue() &&
                         s.EditingDto.TedsEmploymentStatusInformationTedsEmploymentStatus.Response.WellKnownName == WellKnownNames.TedsModule.TedsEmploymentStatus.NotInLaborForce)
                .Then(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.TedsEmploymentStatusInformationDetailedNotInLaborForce);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.TedsEmploymentStatusInformationDetailedNotInLaborForce.SetAsNotAnswered();
                        s.EditingDto.Disable(dto => dto.TedsEmploymentStatusInformationDetailedNotInLaborForce);
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
                        s.EditingDto.PrimaryUsualAdministrationRouteType.SetAsNotAnswered();
                        s.EditingDto.PrimaryFirstUseAge.SetAsNotAnswered();
                        s.EditingDto.PrimaryDetailedDrugCode.SetAsNotAnswered();

                        s.EditingDto.Disable(dto => dto.PrimaryUseFrequencyType);
                        s.EditingDto.Disable(dto => dto.PrimaryUsualAdministrationRouteType);
                        s.EditingDto.Disable(dto => dto.PrimaryFirstUseAge);
                        s.EditingDto.Disable(dto => dto.PrimaryDetailedDrugCode);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.PrimaryUseFrequencyType);
                        s.EditingDto.Enable(dto => dto.PrimaryUsualAdministrationRouteType);
                        s.EditingDto.Enable(dto => dto.PrimaryFirstUseAge);
                        s.EditingDto.Enable(dto => dto.PrimaryDetailedDrugCode);
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
                        s.EditingDto.SecondaryUsualAdministrationRouteType.SetAsNotAnswered();
                        s.EditingDto.SecondaryFirstUseAge.SetAsNotAnswered();
                        s.EditingDto.SecondaryDetailedDrugCode.SetAsNotAnswered();

                        s.EditingDto.Disable(dto => dto.SecondaryUseFrequencyType);
                        s.EditingDto.Disable(dto => dto.SecondaryUsualAdministrationRouteType);
                        s.EditingDto.Disable(dto => dto.SecondaryFirstUseAge);
                        s.EditingDto.Disable(dto => dto.SecondaryDetailedDrugCode);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.SecondaryUseFrequencyType);
                        s.EditingDto.Enable(dto => dto.SecondaryUsualAdministrationRouteType);
                        s.EditingDto.Enable(dto => dto.SecondaryFirstUseAge);
                        s.EditingDto.Enable(dto => dto.SecondaryDetailedDrugCode);
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
                        s.EditingDto.TertiaryUsualAdministrationRouteType.SetAsNotAnswered();
                        s.EditingDto.TertiaryFirstUseAge.SetAsNotAnswered();
                        s.EditingDto.TertiaryDetailedDrugCode.SetAsNotAnswered();

                        s.EditingDto.Disable(dto => dto.TertiaryUseFrequencyType);
                        s.EditingDto.Disable(dto => dto.TertiaryUsualAdministrationRouteType);
                        s.EditingDto.Disable(dto => dto.TertiaryFirstUseAge);
                        s.EditingDto.Disable(dto => dto.TertiaryDetailedDrugCode);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.TertiaryUseFrequencyType);
                        s.EditingDto.Enable(dto => dto.TertiaryUsualAdministrationRouteType);
                        s.EditingDto.Enable(dto => dto.TertiaryFirstUseAge);
                        s.EditingDto.Enable(dto => dto.TertiaryDetailedDrugCode);
                    });
        }
    }
}
