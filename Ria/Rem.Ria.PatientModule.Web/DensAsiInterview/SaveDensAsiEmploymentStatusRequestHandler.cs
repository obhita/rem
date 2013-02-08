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

using Rem.Domain.Clinical.DensAsiModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Class for handling save dens asi employment status request.
    /// </summary>
    public class SaveDensAsiEmploymentStatusRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiEmploymentStatusDto>, DtoResponse<DensAsiEmploymentStatusDto>, DensAsiEmploymentStatusDto,
            Domain.Clinical.DensAsiModule.DensAsiInterview, DensAsiEmploymentStatus>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiEmploymentStatusRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiEmploymentStatusRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiEmploymentStatusDto">The dens asi employment status dto.</param>
        /// <param name="densAsiEmploymentStatus">The dens asi employment status.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            DensAsiEmploymentStatusDto densAsiEmploymentStatusDto, DensAsiEmploymentStatus densAsiEmploymentStatus )
        {
            var patientCounselingDensAsiInterviewerRating =
                _mappingHelper.MapLookupField<DensAsiInterviewerRating> ( densAsiEmploymentStatusDto.PatientCounselingDensAsiInterviewerRating );

            var densiAsiEmploymentStatusNew = new DensAsiEmploymentStatusSectionBuilder ()
                .WithYearsAndMonthsEducationCompletedTimeSpan (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.YearsAndMonthsEducationCompletedTimeSpan, _mappingHelper ) )
                .WithYearsAndMonthsEducationCompletedTimeSpanNote ( densAsiEmploymentStatusDto.YearsAndMonthsEducationCompletedTimeSpanNote )
                .WithTechnicalEducationCompletedMonthCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.TechnicalEducationCompletedMonthCount, _mappingHelper ) )
                .WithTechnicalEducationCompletedMonthCountNote ( densAsiEmploymentStatusDto.TechnicalEducationCompletedMonthCountNote )
                .WithProfessionTradeSkillIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.ProfessionTradeSkillIndicator, _mappingHelper ) )
                .WithProfessionTradeSkillDescription ( densAsiEmploymentStatusDto.ProfessionTradeSkillDescription )
                .WithProfessionTradeSkillNote ( densAsiEmploymentStatusDto.ProfessionTradeSkillNote )
                .WithValidDriversLicenseIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.ValidDriversLicenseIndicator, _mappingHelper ) )
                .WithValidDriversLicenseIndicatorNote ( densAsiEmploymentStatusDto.ValidDriversLicenseIndicatorNote )
                .WithAutomobileAvailableforUseIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.AutomobileAvailableforUseIndicator, _mappingHelper ) )
                .WithAutomobileAvailableforUseIndicatorNote ( densAsiEmploymentStatusDto.AutomobileAvailableforUseIndicatorNote )
                .WithYearsAndMonthsOfLongestFullTimeJobTimeSpan (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.YearsAndMonthsOfLongestFullTimeJobTimeSpan, _mappingHelper ) )
                .WithYearsAndMonthsOfLongestFullTimeJobTimeSpanNote ( densAsiEmploymentStatusDto.YearsAndMonthsOfLongestFullTimeJobTimeSpanNote )
                .WithUsualOrLastDensAsiOccupationType (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiOccupationType> (
                        densAsiEmploymentStatusDto.UsualOrLastDensAsiOccupationType, _mappingHelper ) )
                .WithUsualOrLastOccupationDescription ( densAsiEmploymentStatusDto.UsualOrLastOccupationDescription )
                .WithUsualOrLastOccupationNote ( densAsiEmploymentStatusDto.UsualOrLastOccupationNote )
                .WithContributionOfSomeoneToSupportIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.ContributionOfSomeoneToSupportIndicator, _mappingHelper ) )
                .WithContributionOfSomeoneToSupportIndicatorNote ( densAsiEmploymentStatusDto.ContributionOfSomeoneToSupportIndicatorNote )
                .WithContributionConstituteMajorityOfYourSupportIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.ContributionConstituteMajorityOfYourSupportIndicator, _mappingHelper ) )
                .WithContributionConstituteMajorityOfYourSupportIndicatorNote (
                    densAsiEmploymentStatusDto.ContributionConstituteMajorityOfYourSupportIndicatorNote )
                .WithPastThreeYearsDensAsiEmploymentPattern (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiEmploymentPattern> (
                        densAsiEmploymentStatusDto.PastThreeYearsDensAsiEmploymentPattern, _mappingHelper ) )
                .WithPastThreeYearsDensAsiEmploymentPatternNote ( densAsiEmploymentStatusDto.PastThreeYearsDensAsiEmploymentPatternNote )
                .WithWorkInLastThirtyDaysPaidDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.WorkInLastThirtyDaysPaidDayCount, _mappingHelper ) )
                .WithWorkInLastThirtyDaysPaidDayCountNote ( densAsiEmploymentStatusDto.WorkInLastThirtyDaysPaidDayCountNote )
                .WithNetIncomeAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiEmploymentStatusDto.NetIncomeAmount, _mappingHelper ) )
                .WithNetIncomeAmountNote ( densAsiEmploymentStatusDto.NetIncomeAmountNote )
                .WithUnemploymentCompensationAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.UnemploymentCompensationAmount, _mappingHelper ) )
                .WithUnemploymentCompensationAmountNote ( densAsiEmploymentStatusDto.UnemploymentCompensationAmountNote )
                .WithWelfareAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiEmploymentStatusDto.WelfareAmount, _mappingHelper ) )
                .WithWelfareAmountNote ( densAsiEmploymentStatusDto.WelfareAmountNote )
                .WithPensionBenefitsSocialSecurityAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiEmploymentStatusDto.PensionBenefitsSocialSecurityAmount, _mappingHelper ) )
                .WithPensionBenefitsSocialSecurityAmountNote ( densAsiEmploymentStatusDto.PensionBenefitsSocialSecurityAmountNote )
                .WithMateFamilyFriendsAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiEmploymentStatusDto.MateFamilyFriendsAmount, _mappingHelper ) )
                .WithMateFamilyFriendsAmountNote ( densAsiEmploymentStatusDto.MateFamilyFriendsAmountNote )
                .WithIllegalAmount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiEmploymentStatusDto.IllegalAmount, _mappingHelper ) )
                .WithIllegalAmountNote ( densAsiEmploymentStatusDto.IllegalAmountNote )
                .WithDependentPeopleCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiEmploymentStatusDto.DependentPeopleCount, _mappingHelper ) )
                .WithDependentPeopleCountNote ( densAsiEmploymentStatusDto.DependentPeopleCountNote )
                .WithEmploymentProblemsDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiEmploymentStatusDto.EmploymentProblemsDayCount, _mappingHelper ) )
                .WithEmploymentProblemsDayCountNote ( densAsiEmploymentStatusDto.EmploymentProblemsDayCountNote )
                .WithTroubledByEmploymentProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiEmploymentStatusDto.TroubledByEmploymentProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithTroubledByEmploymentProblemsDensAsiPatientRatingNote (
                    densAsiEmploymentStatusDto.TroubledByEmploymentProblemsDensAsiPatientRatingNote )
                .WithImportanceOfEmployementProblemCounselingDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiEmploymentStatusDto.ImportanceOfEmploymentProblemCounselingDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfEmployementProblemCounselingDensAsiPatientRatingNote (
                    densAsiEmploymentStatusDto.ImportanceOfEmploymentProblemCounselingDensAsiPatientRatingNote )
                .WithPatientCounselingDensAsiInterviewerRating ( patientCounselingDensAsiInterviewerRating )
                .WithPatientCounselingDensAsiInterviewerRatingNote ( densAsiEmploymentStatusDto.PatientCounselingDensAsiInterviewerRatingNote )
                .WithConfidenceDistortedByPatientMisrepresentationIndicator (
                    densAsiEmploymentStatusDto.ConfidenceDistortedByPatientMisrepresentationIndicator )
                .WithConfidenceDistortedByPatientMisrepresentationIndicatorNote (
                    densAsiEmploymentStatusDto.ConfidenceDistortedByPatientMisrepresentationIndicatorNote )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator (
                    densAsiEmploymentStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote (
                    densAsiEmploymentStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote )
                .WithSectionNote ( densAsiEmploymentStatusDto.SectionNote )
                .Build ();

            AggregateRoot.ReviseDensAsiEmploymentStatus ( densiAsiEmploymentStatusNew );

            densAsiEmploymentStatusDto.Key = AggregateRoot.DensAsiEmploymentStatus.Key;

            return true;
        }

        #endregion
    }
}
