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
    /// Class for handling save dens asi legal status request.
    /// </summary>
    public class SaveDensAsiLegalStatusRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiLegalStatusDto>, DtoResponse<DensAsiLegalStatusDto>, DensAsiLegalStatusDto, Domain.Clinical.DensAsiModule.DensAsiInterview,
            DensAsiLegalStatus>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiLegalStatusRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiLegalStatusRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiLegalStatusDto">The dens asi legal status dto.</param>
        /// <param name="densAsiLegalStatus">The dens asi legal status.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( DensAsiLegalStatusDto densAsiLegalStatusDto, DensAsiLegalStatus densAsiLegalStatus )
        {
            var patientCounselingDensAsiInterviewerRating =
                _mappingHelper.MapLookupField<DensAsiInterviewerRating> ( densAsiLegalStatusDto.PatientCounselingDensAsiInterviewerRating );

            var densAsiLegalStatusNew = new DensAsiLegalStatusSectionBuilder ()
                .WithAdmissionPromptedByCriminalJusticeSystemIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.AdmissionPromptedByCriminalJusticeSystemIndicator, _mappingHelper ) )
                .WithAdmissionPromptedByCriminalJusticeSystemIndicatorNote (
                    densAsiLegalStatusDto.AdmissionPromptedByCriminalJusticeSystemIndicatorNote )
                .WithProbationOrParoleIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ProbationOrParoleIndicator, _mappingHelper ) )
                .WithProbationOrParoleIndicatorNote ( densAsiLegalStatusDto.ProbationOrParoleIndicatorNote )
                .WithArrestedChargedShopliftingCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedShopliftingCount, _mappingHelper ) )
                .WithArrestedChargedShopliftingCountNote ( densAsiLegalStatusDto.ArrestedChargedShopliftingCountNote )
                .WithArrestedChargedProbationParoleViolationCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ArrestedChargedProbationParoleViolationCount, _mappingHelper ) )
                .WithArrestedChargedProbationParoleViolationCountNote ( densAsiLegalStatusDto.ArrestedChargedProbationParoleViolationCountNote )
                .WithArrestedChargedDrugChargesCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedDrugChargesCount, _mappingHelper ) )
                .WithArrestedChargedDrugChargesCountNote ( densAsiLegalStatusDto.ArrestedChargedDrugChargesCountNote )
                .WithArrestedChargedForgeryCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedForgeryCount, _mappingHelper ) )
                .WithArrestedChargedForgeryCountNote ( densAsiLegalStatusDto.ArrestedChargedForgeryCountNote )
                .WithArrestedChargedWeaponsOffenseCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ArrestedChargedWeaponsOffenseCount, _mappingHelper ) )
                .WithArrestedChargedWeaponsOffenseCountNote ( densAsiLegalStatusDto.ArrestedChargedWeaponsOffenseCountNote )
                .WithArrestedChargedBurglaryLarcencyCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ArrestedChargedBurglaryLarcencyCount, _mappingHelper ) )
                .WithArrestedChargedBurglaryLarcencyCountNote ( densAsiLegalStatusDto.ArrestedChargedBurglaryLarcencyCountNote )
                .WithArrestedChargedRobberyCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedRobberyCount, _mappingHelper ) )
                .WithArrestedChargedRobberyCountNote ( densAsiLegalStatusDto.ArrestedChargedRobberyCountNote )
                .WithArrestedChargedAssaultCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedAssaultCount, _mappingHelper ) )
                .WithArrestedChargedAssaultCountNote ( densAsiLegalStatusDto.ArrestedChargedAssaultCountNote )
                .WithArrestedChargedArsonCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedArsonCount, _mappingHelper ) )
                .WithArrestedChargedArsonCountNote ( densAsiLegalStatusDto.ArrestedChargedArsonCountNote )
                .WithArrestedChargedRapeCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedRapeCount, _mappingHelper ) )
                .WithArrestedChargedRapeCountNote ( densAsiLegalStatusDto.ArrestedChargedRapeCountNote )
                .WithArrestedChargedHomicideManslaughterCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ArrestedChargedHomicideManslaughterCount, _mappingHelper ) )
                .WithArrestedChargedHomicideManslaughterCountNote ( densAsiLegalStatusDto.ArrestedChargedHomicideManslaughterCountNote )
                .WithArrestedChargedProstitutionCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ArrestedChargedProstitutionCount, _mappingHelper ) )
                .WithArrestedChargedProstitutionCountNote ( densAsiLegalStatusDto.ArrestedChargedProstitutionCountNote )
                .WithArrestedChargedContemptOfCountCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ArrestedChargedContemptOfCountCount, _mappingHelper ) )
                .WithArrestedChargedContemptOfCountCountNote ( densAsiLegalStatusDto.ArrestedChargedContemptOfCountCountNote )
                .WithArrestedChargedOtherCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.ArrestedChargedOtherCount, _mappingHelper ) )
                .WithArrestedChargedOtherDescription ( densAsiLegalStatusDto.ArrestedChargedOtherDescription )
                .WithArrestedChargedOtherNote ( densAsiLegalStatusDto.ArrestedChargedOtherNote )
                .WithArrestChargesResultedInConvictionsCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ArrestChargesResultedInConvictionsCount, _mappingHelper ) )
                .WithArrestChargesResultedInConvictionsCountNote ( densAsiLegalStatusDto.ArrestChargesResultedInConvictionsCountNote )
                .WithChargedWithDisorderlyConductCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ChargedWithDisorderlyConductCount, _mappingHelper ) )
                .WithChargedWithDisorderlyConductCountNote ( densAsiLegalStatusDto.ChargedWithDisorderlyConductCountNote )
                .WithChargedWithDrivingWhileIntoxicatedCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ChargedWithDrivingWhileIntoxicatedCount, _mappingHelper ) )
                .WithChargedWithDrivingWhileIntoxicatedCountNote ( densAsiLegalStatusDto.ChargedWithDrivingWhileIntoxicatedCountNote )
                .WithChargedWithMajorDrivingViolationsCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.ChargedWithMajorDrivingViolationsCount, _mappingHelper ) )
                .WithChargedWithMajorDrivingViolationsCountNote ( densAsiLegalStatusDto.ChargedWithMajorDrivingViolationsCountNote )
                .WithIncarcerationInLifeMonthCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiLegalStatusDto.IncarcerationInLifeMonthCount, _mappingHelper ) )
                .WithIncarcerationInLifeMonthCountNote ( densAsiLegalStatusDto.IncarcerationInLifeMonthCountNote )
                .WithLastIncarcerationLengthMonthCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.LastIncarcerationLengthMonthCount, _mappingHelper ) )
                .WithIncarcerationLengthMonthCountNote ( densAsiLegalStatusDto.IncarcerationLengthMonthCountNote )
                .WithIncarcerationForDensAsiViolationType (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiViolationType> (
                        densAsiLegalStatusDto.IncarcerationForDensAsiViolationType, _mappingHelper ) )
                .WithIncarcerationForDensAsiViolationTypeNote ( densAsiLegalStatusDto.IncarcerationForDensAsiViolationTypeNote )
                .WithPresentlyAwaitingChargesIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.PresentlyAwaitingChargesIndicator, _mappingHelper ) )
                .WithPresentlyAwaitingChargesIndicatorNote ( densAsiLegalStatusDto.PresentlyAwaitingChargesIndicatorNote )
                .WithPresentlyAwaitingChargesForDensAsiViolationType (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiViolationType> (
                        densAsiLegalStatusDto.PresentlyAwaitingChargesForDensAsiViolationType, _mappingHelper ) )
                .WithPresentlyAwaitingChargesForNote ( densAsiLegalStatusDto.PresentlyAwaitingChargesForNote )
                .WithIncarceratedInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.IncarceratedInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithIncarceratedInLastThirtyDaysDayCountNote ( densAsiLegalStatusDto.IncarceratedInLastThirtyDaysDayCountNote )
                .WithIllegalActivityInLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.IllegalActivityInLastThirtyDaysDayCount, _mappingHelper ) )
                .WithIllegalActivityInLastThirtyDaysDayCountNote ( densAsiLegalStatusDto.IllegalActivityInLastThirtyDaysDayCountNote )
                .WithSeriousnessOfLegalProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiLegalStatusDto.SeriousnessOfLegalProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithSeriousnessOfLegalProblemsDensAsiPatientRatingNote ( densAsiLegalStatusDto.SeriousnessOfLegalProblemsDensAsiPatientRatingNote )
                .WithImportanceOfLegalProblemCounselingDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiLegalStatusDto.ImportanceOfLegalProblemCounselingDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfLegalProblemCounselingDensAsiPatientRatingNote (
                    densAsiLegalStatusDto.ImportanceOfLegalProblemCounselingDensAsiPatientRatingNote )
                .WithPatientCounselingDensAsiInterviewerRating ( patientCounselingDensAsiInterviewerRating )
                .WithPatientCounselingDensAsiInterviewerRatingNote ( densAsiLegalStatusDto.PatientCounselingDensAsiInterviewerRatingNote )
                .WithConfidenceDistortedByPatientMisrepresentationIndicator (
                    densAsiLegalStatusDto.ConfidenceDistortedByPatientMisrepresentationIndicator )
                .WithConfidenceDistortedByPatientMisrepresentationIndicatorNote (
                    densAsiLegalStatusDto.ConfidenceDistortedByPatientMisrepresentationIndicatorNote )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator (
                    densAsiLegalStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote (
                    densAsiLegalStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote )
                .WithSectionNote ( densAsiLegalStatusDto.SectionNote )
                .WithTreatmentMandatoryForCriminalJusticeSystemIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.TreatmentMandatoryForCriminalJusticeSystemIndicator, _mappingHelper ) )
                .WithTreatmentMandatoryForCriminalJusticeSystemIndicatorNote (
                    densAsiLegalStatusDto.TreatmentMandatoryForCriminalJusticeSystemIndicatorNote )
                .WithTreatmentInsteadOfIncarcerationInPrisonIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiLegalStatusDto.TreatmentInsteadOfIncarcerationInPrisonIndicator, _mappingHelper ) )
                .WithTreatmentInsteadOfIncarcerationInPrisonIndicatorNote (
                    densAsiLegalStatusDto.TreatmentInsteadOfIncarcerationInPrisonIndicatorNote )
                .Build ();

            AggregateRoot.ReviseDensAsiLegalStatus ( densAsiLegalStatusNew );

            densAsiLegalStatusDto.Key = AggregateRoot.DensAsiLegalStatus.Key;

            return true;
        }

        #endregion
    }
}
