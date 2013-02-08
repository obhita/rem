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
    /// Class for handling save dens asi family social relationships request.
    /// </summary>
    public class SaveDensAsiFamilySocialRelationshipsRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiFamilySocialRelationshipsDto>, DtoResponse<DensAsiFamilySocialRelationshipsDto>,
            DensAsiFamilySocialRelationshipsDto, Domain.Clinical.DensAsiModule.DensAsiInterview, DensAsiFamilySocialRelationships>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiFamilySocialRelationshipsRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiFamilySocialRelationshipsRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiFamilySocialRelationshipsDto">The dens asi family social relationships dto.</param>
        /// <param name="densAsiFamilySocialRelationships">The dens asi family social relationships.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            DensAsiFamilySocialRelationshipsDto densAsiFamilySocialRelationshipsDto, DensAsiFamilySocialRelationships densAsiFamilySocialRelationships )
        {
            var patientFamilySocialCounselingDensAsiInterviewerRating =
                _mappingHelper.MapLookupField<DensAsiInterviewerRating> (
                    densAsiFamilySocialRelationshipsDto.PatientFamilySocialCounselingDensAsiInterviewerRating );
            var densAsiFamilySocialRelationshipsNew = new DensAsiFamilySocialRelationshipsSectionBuilder ()
                .WithDensAsiMaritalStatus (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiMaritalStatus> (
                        densAsiFamilySocialRelationshipsDto.DensAsiMaritalStatus, _mappingHelper ) )
                .WithDensAsiMaritalStatusNote ( densAsiFamilySocialRelationshipsDto.DensAsiMaritalStatusNote )
                .WithYearsAndMonthsWithMaritalStatusTimeSpan (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.YearsAndMonthsWithMaritalStatusTimeSpan, _mappingHelper ) )
                .WithYearsAndMonthsWithMaritalStatusTimeSpanNote ( densAsiFamilySocialRelationshipsDto.YearsAndMonthsWithMaritalStatusTimeSpanNote )
                .WithMaritalStatusSatisfactionIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiSatisfaction> (
                        densAsiFamilySocialRelationshipsDto.MaritalStatusDensAsiSatisfaction, _mappingHelper ) )
                .WithMaritalStatusSatisfactionIndicatorNote ( densAsiFamilySocialRelationshipsDto.MaritalStatusDensAsiSatisfactionNote )
                .WithPastThreeYearsDensAsiLivingArrangementType (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiLivingArrangementType> (
                        densAsiFamilySocialRelationshipsDto.PastThreeYearsDensAsiLivingArrangementType, _mappingHelper ) )
                .WithPastThreeYearsDensAsiLivingArrangementTypeNote (
                    densAsiFamilySocialRelationshipsDto.PastThreeYearsDensAsiLivingArrangementTypeNote )
                .WithYearsAndMonthsInLivingArrangementTypeTimeSpan (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.YearsAndMonthsInLivingArrangementTypeTimeSpan, _mappingHelper ) )
                .WithYearsAndMonthsInLivingArrangementTypeTimeSpanNote (
                    densAsiFamilySocialRelationshipsDto.YearsAndMonthsInLivingArrangementTypeTimeSpanNote )
                .WithLivingArrangementTypeSatisfactionIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiSatisfaction> (
                        densAsiFamilySocialRelationshipsDto.LivingArrangementTypeDensAsiSatisfaction, _mappingHelper ) )
                .WithLivingArrangementTypeSatisfactionIndicatorNote (
                    densAsiFamilySocialRelationshipsDto.LivingArrangementTypeDensAsiSatisfactionNote )
                .WithLivingWithAnyoneWhoHasAlcoholProblemIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.LivingWithAnyoneWhoHasAlcoholProblemIndicator, _mappingHelper ) )
                .WithLivingWithAnyoneWhoHasAlcoholProblemIndicatorNote (
                    densAsiFamilySocialRelationshipsDto.LivingWithAnyoneWhoHasAlcoholProblemIndicatorNote )
                .WithLivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicator, _mappingHelper ) )
                .WithLivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote (
                    densAsiFamilySocialRelationshipsDto.LivingWithAnyoneWhoUsesNonPrescribedDrugsIndicatorNote )
                .WithDensAsiFreeTimeSpentType (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiFreeTimeSpentType> (
                        densAsiFamilySocialRelationshipsDto.DensAsiFreeTimeSpentType, _mappingHelper ) )
                .WithDensAsiFreeTimeSpentTypeNote ( densAsiFamilySocialRelationshipsDto.DensAsiFreeTimeSpentTypeNote )
                .WithFreeTimeSpentTypeSatisfactionIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiSatisfaction> (
                        densAsiFamilySocialRelationshipsDto.FreeTimeSpentTypeDensAsiSatisfaction, _mappingHelper ) )
                .WithFreeTimeSpentTypeSatisfactionIndicatorNote ( densAsiFamilySocialRelationshipsDto.FreeTimeSpentTypeDensAsiSatisfactionNote )
                .WithCloseFriendsCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiFamilySocialRelationshipsDto.CloseFriendsCount, _mappingHelper ) )
                .WithCloseFriendsCountNote ( densAsiFamilySocialRelationshipsDto.CloseFriendsCountNote )
                .WithReciprocalRelationshipMother (
                    _mappingHelper.MapLookupField<DensAsiHasParentalRelationshipOption>(
                        densAsiFamilySocialRelationshipsDto.MotherDensAsiHasParentalRelationshipOption ) )
                .WithReciprocalRelationshipMotherNote ( densAsiFamilySocialRelationshipsDto.MotherDensAsiHasParentalRelationshipOptionNote )
                .WithReciprocalRelationshipFather (
                    _mappingHelper.MapLookupField<DensAsiHasParentalRelationshipOption>(
                        densAsiFamilySocialRelationshipsDto.FatherDensAsiHasParentalRelationshipOption ) )
                .WithReciprocalRelationshipFatherNote ( densAsiFamilySocialRelationshipsDto.FatherDensAsiHasParentalRelationshipOptionNote )
                .WithReciprocalRelationshipBrotherSister (
                    _mappingHelper.MapLookupField<DensAsiHasRelationshipOption>(
                        densAsiFamilySocialRelationshipsDto.BrotherSisterDensAsiHasRelationshipOption ) )
                .WithReciprocalRelationshipBrotherSisterNote (
                    densAsiFamilySocialRelationshipsDto.BrotherSisterDensAsiHasRelationshipOptionNote )
                .WithReciprocalRelationshipSexualPartner (
                    _mappingHelper.MapLookupField<DensAsiHasRelationshipOption>(
                        densAsiFamilySocialRelationshipsDto.SexualPartnerDensAsiHasRelationshipOption ) )
                .WithReciprocalRelationshipSexualPartnerNote (
                    densAsiFamilySocialRelationshipsDto.SexualPartnerDensAsiHasRelationshipOptionNote )
                .WithReciprocalRelationshipChildren (
                    _mappingHelper.MapLookupField<DensAsiHasRelationshipOption>(
                        densAsiFamilySocialRelationshipsDto.ChildrenDensAsiHasRelationshipOption ) )
                .WithReciprocalRelationshipChildrenNote ( densAsiFamilySocialRelationshipsDto.ChildrenDensAsiHasRelationshipOptionNote )
                .WithReciprocalRelationshipFriends (
                    _mappingHelper.MapLookupField<DensAsiHasRelationshipOption>(
                        densAsiFamilySocialRelationshipsDto.FriendsDensAsiHasRelationshipOption ) )
                .WithReciprocalRelationshipFriendsNote ( densAsiFamilySocialRelationshipsDto.FriendsDensAsiHasRelationshipOptionNote )
                .WithProblemsMotherInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsMotherInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsMotherInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsMotherInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsMotherNote ( densAsiFamilySocialRelationshipsDto.ProblemsMotherNote )
                .WithProblemsFatherInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsFatherInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsFatherInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsFatherInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsFatherNote ( densAsiFamilySocialRelationshipsDto.ProblemsFatherNote )
                .WithProblemsBrotherSisterInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsBrotherSisterInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsBrotherSisterInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsBrotherSisterInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsBrotherSisterNote ( densAsiFamilySocialRelationshipsDto.ProblemsBrotherSisterNote )
                .WithProblemsSexualPartnerInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsSexualPartnerInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsSexualPartnerInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsSexualPartnerInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsSexualPartnerNote ( densAsiFamilySocialRelationshipsDto.ProblemsSexualPartnerNote )
                .WithProblemsChildrenInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsChildrenInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsChildrenInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsChildrenInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsChildrenNote ( densAsiFamilySocialRelationshipsDto.ProblemsChildrenNote )
                .WithProblemsOtherSignificantFamilyInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsOtherSignificantFamilyInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsOtherSignificantFamilyInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsOtherSignificantFamilyInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsOtherSignificantFamilyDescription ( densAsiFamilySocialRelationshipsDto.ProblemsOtherSignificantFamilyDescription )
                .WithProblemsOtherSignificantFamilyNote ( densAsiFamilySocialRelationshipsDto.ProblemsOtherSignificantFamilyNote )
                .WithProblemsCloseFriendsInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsCloseFriendsInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsCloseFriendsInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsCloseFriendsInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsCloseFriendsNote ( densAsiFamilySocialRelationshipsDto.ProblemsCloseFriendsNote )
                .WithProblemsNeighborsInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsNeighborsInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsNeighborsInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsNeighborsInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsNeighborsNote ( densAsiFamilySocialRelationshipsDto.ProblemsNeighborsNote )
                .WithProblemsCoworkersInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsCoworkersInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithProblemsCoworkersInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ProblemsCoworkersInLifetimeIndicator, _mappingHelper ) )
                .WithProblemsCoworkersNote ( densAsiFamilySocialRelationshipsDto.ProblemsCoworkersNote )
                .WithAbusedEmotionallyInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.AbusedEmotionallyInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithAbusedEmotionallyInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.AbusedEmotionallyInLifetimeIndicator, _mappingHelper ) )
                .WithAbusedEmotionallyNote ( densAsiFamilySocialRelationshipsDto.AbusedEmotionallyNote )
                .WithAbusedPhysicallyInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.AbusedPhysicallyInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithAbusedPhysicallyInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.AbusedPhysicallyInLifetimeIndicator, _mappingHelper ) )
                .WithAbusedPhysicallyNote ( densAsiFamilySocialRelationshipsDto.AbusedPhysicallyNote )
                .WithAbusedSexuallyInLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.AbusedSexuallyInLastThirtyDaysIndicator, _mappingHelper ) )
                .WithAbusedSexuallyInLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.AbusedSexuallyInLifetimeIndicator, _mappingHelper ) )
                .WithAbusedSexuallyNote ( densAsiFamilySocialRelationshipsDto.AbusedSexuallyNote )
                .WithSeriousFamilyConflictsInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.SeriousFamilyConflictsInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithSeriousFamilyConflictsInLastThirtyDaysDayCountNote (
                    densAsiFamilySocialRelationshipsDto.SeriousFamilyConflictsInLastThirtyDaysDayCountNote )
                .WithTroubledByFamilyProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiFamilySocialRelationshipsDto.TroubledByFamilyProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithTroubledByFamilyProblemsDensAsiPatientRatingNote (
                    densAsiFamilySocialRelationshipsDto.TroubledByFamilyProblemsDensAsiPatientRatingNote )
                .WithImportanceOfFamilyProblemCounselingDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiFamilySocialRelationshipsDto.ImportanceOfFamilyProblemCounselingDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfFamilyProblemCounselingDensAsiPatientRatingNote (
                    densAsiFamilySocialRelationshipsDto.ImportanceOfFamilyProblemCounselingDensAsiPatientRatingNote )
                .WithConflictsWithOthersInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ConflictsWithOthersInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithConflictsWithOthersInLastThirtyDaysDayCountNote (
                    densAsiFamilySocialRelationshipsDto.ConflictsWithOthersInLastThirtyDaysDayCountNote )
                .WithTroubledBySocialProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiFamilySocialRelationshipsDto.TroubledBySocialProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithTroubledBySocialProblemsDensAsiPatientRatingNote (
                    densAsiFamilySocialRelationshipsDto.TroubledBySocialProblemsDensAsiPatientRatingNote )
                .WithImportanceOfSocialProblemCounselingDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiFamilySocialRelationshipsDto.ImportanceOfSocialProblemCounselingDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfSocialProblemCounselingDensAsiPatientRatingNote (
                    densAsiFamilySocialRelationshipsDto.ImportanceOfSocialProblemCounselingDensAsiPatientRatingNote )
                .WithPatientFamilySocialCounselingDensAsiInterviewerRating ( patientFamilySocialCounselingDensAsiInterviewerRating )
                .WithPatientFamilySocialCounselingDensAsiInterviewerRatingNote (
                    densAsiFamilySocialRelationshipsDto.PatientFamilySocialCounselingDensAsiInterviewerRatingNote )
                .WithConfidenceDistortedByPatientMisrepresentationIndicator (
                    densAsiFamilySocialRelationshipsDto.ConfidenceDistortedByPatientMisrepresentationIndicator )
                .WithConfidenceDistortedByPatientMisrepresentationIndicatorNote (
                    densAsiFamilySocialRelationshipsDto.ConfidenceDistortedByPatientMisrepresentationIndicatorNote )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator (
                    densAsiFamilySocialRelationshipsDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote (
                    densAsiFamilySocialRelationshipsDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote )
                .WithSectionNote ( densAsiFamilySocialRelationshipsDto.SectionNote )
                .WithHomelessInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.HomelessInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithHomelessInLastThirtyDaysDayCountNote ( densAsiFamilySocialRelationshipsDto.HomelessInLastThirtyDaysDayCountNote )
                .WithShelterInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.ShelterInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithShelterInLastThirtyDaysDayCountNote ( densAsiFamilySocialRelationshipsDto.ShelterInLastThirtyDaysDayCountNote )
                .WithNotOwnedHouseInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.NotOwnedHouseInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithNotOwnedHouseInLastThirtyDaysDayCountNote ( densAsiFamilySocialRelationshipsDto.NotOwnedHouseInLastThirtyDaysDayCountNote )
                .WithHospitalJailInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiFamilySocialRelationshipsDto.HospitalJailInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithHospitalJailInLastThirtyDaysDayCountNote ( densAsiFamilySocialRelationshipsDto.HospitalJailInLastThirtyDaysDayCountNote )
                .Build ();

            AggregateRoot.ReviseDensAsiFamilySocialRelationships ( densAsiFamilySocialRelationshipsNew );

            densAsiFamilySocialRelationshipsDto.Key = AggregateRoot.DensAsiFamilySocialRelationships.Key;

            return true;
        }

        #endregion
    }
}
