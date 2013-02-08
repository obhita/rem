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
    /// Class for handling save dens asi psychiatric status request.
    /// </summary>
    public class SaveDensAsiPsychiatricStatusRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiPsychiatricStatusDto>, DtoResponse<DensAsiPsychiatricStatusDto>, DensAsiPsychiatricStatusDto,
            Domain.Clinical.DensAsiModule.DensAsiInterview, DensAsiPsychiatricStatus>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiPsychiatricStatusRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiPsychiatricStatusRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiPsychiatricStatusDto">The dens asi psychiatric status dto.</param>
        /// <param name="densAsiPsychiatricStatus">The dens asi psychiatric status.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            DensAsiPsychiatricStatusDto densAsiPsychiatricStatusDto, DensAsiPsychiatricStatus densAsiPsychiatricStatus )
        {
            var patientCounselingDensAsiInterviewerRating =
                _mappingHelper.MapLookupField<DensAsiInterviewerRating> ( densAsiPsychiatricStatusDto.PatientCounselingDensAsiInterviewerRating );

            var densAsiPsychiatricStatusNew = new DensAsiPsychiatricStatusSectionBuilder ()
                .WithPsychologicalTreatmentInHospitalCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PsychologicalTreatmentInHospitalCount, _mappingHelper ) )
                .WithPsychologicalTreatmentInHospitalCountNote ( densAsiPsychiatricStatusDto.PsychologicalTreatmentInHospitalCountNote )
                .WithPsychologicalTreatmentAsOutpatientCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PsychologicalTreatmentAsOutpatientCount, _mappingHelper ) )
                .WithPsychologicalTreatmentAsOutpatientCountNote ( densAsiPsychiatricStatusDto.PsychologicalTreatmentAsOutpatientCountNote )
                .WithPsychiatricDisabilityPensionIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PsychiatricDisabilityPensionIndicator, _mappingHelper ) )
                .WithPsychiatricDisabilityPensionIndicatorNote ( densAsiPsychiatricStatusDto.PsychiatricDisabilityPensionIndicatorNote )
                .WithDepressionLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.DepressionLastThirtyDaysIndicator, _mappingHelper ) )
                .WithDepressionLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.DepressionLifetimeIndicator, _mappingHelper ) )
                .WithDepressionNote ( densAsiPsychiatricStatusDto.DepressionNote )
                .WithAnxietyLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.AnxietyLastThirtyDaysIndicator, _mappingHelper ) )
                .WithAnxietyLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiPsychiatricStatusDto.AnxietyLifetimeIndicator, _mappingHelper ) )
                .WithAnxietyNote ( densAsiPsychiatricStatusDto.AnxietyNote )
                .WithHallucinationLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.HallucinationLastThirtyDaysIndicator, _mappingHelper ) )
                .WithHallucinationLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.HallucinationLifetimeIndicator, _mappingHelper ) )
                .WithHallucinationNote ( densAsiPsychiatricStatusDto.HallucinationNote )
                .WithTroubleConcentratingLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.TroubleConcentratingLastThirtyDaysIndicator, _mappingHelper ) )
                .WithTroubleConcentratingLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.TroubleConcentratingLifetimeIndicator, _mappingHelper ) )
                .WithTroubleConcentratingNote ( densAsiPsychiatricStatusDto.TroubleConcentratingNote )
                .WithTroubleControllingRageLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.TroubleControllingRageLastThirtyDaysIndicator, _mappingHelper ) )
                .WithTroubleControllingRageLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.TroubleControllingRageLifetimeIndicator, _mappingHelper ) )
                .WithTroubleControllingRageNote ( densAsiPsychiatricStatusDto.TroubleControllingRageNote )
                .WithThoughtsOfSuicideLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.ThoughtsOfSuicideLastThirtyDaysIndicator, _mappingHelper ) )
                .WithThoughtsOfSuicideLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.ThoughtsOfSuicideLifetimeIndicator, _mappingHelper ) )
                .WithThoughtsOfSuicideNote ( densAsiPsychiatricStatusDto.ThoughtsOfSuicideNote )
                .WithAttemptedSuicideLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.AttemptedSuicideLastThirtyDaysIndicator, _mappingHelper ) )
                .WithAttemptedSuicideLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.AttemptedSuicideLifetimeIndicator, _mappingHelper ) )
                .WithAttemptedSuicideNote ( densAsiPsychiatricStatusDto.AttemptedSuicideNote )
                .WithPrescribedMedicationsLastThirtyDaysIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PrescribedMedicationsLastThirtyDaysIndicator, _mappingHelper ) )
                .WithPrescribedMedicationsLifetimeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PrescribedMedicationsLifetimeIndicator, _mappingHelper ) )
                .WithPrescribedMedicationsNote ( densAsiPsychiatricStatusDto.PrescribedMedicationsNote )
                .WithPsychologicalProblemsLastThirtyDaysDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PsychologicalProblemsLastThirtyDaysDayCount, _mappingHelper ) )
                .WithPsychologicalProblemsLastThirtyDaysDayCountNote ( densAsiPsychiatricStatusDto.PsychologicalProblemsLastThirtyDaysDayCountNote )
                .WithTroubledByPsychologicalProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiPsychiatricStatusDto.TroubledByPsychologicalProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithTroubledByPsychologicalProblemsDensAsiPatientRatingNote (
                    densAsiPsychiatricStatusDto.TroubledByPsychologicalProblemsDensAsiPatientRatingNote )
                .WithImportanceOfPsychologicalProblemCounselingDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiPsychiatricStatusDto.ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote (
                    densAsiPsychiatricStatusDto.ImportanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote )
                .WithPatientDepressedIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiPsychiatricStatusDto.PatientDepressedIndicator, _mappingHelper ) )
                .WithPatientDepressedIndicatorNote ( densAsiPsychiatricStatusDto.PatientDepressedIndicatorNote )
                .WithPatientHostileIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiPsychiatricStatusDto.PatientHostileIndicator, _mappingHelper ) )
                .WithPatientHostileIndicatorNote ( densAsiPsychiatricStatusDto.PatientHostileIndicatorNote )
                .WithPatientAnxiousIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiPsychiatricStatusDto.PatientAnxiousIndicator, _mappingHelper ) )
                .WithPatientAnxiousIndicatorNote ( densAsiPsychiatricStatusDto.PatientAnxiousIndicatorNote )
                .WithPatientParanoidIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiPsychiatricStatusDto.PatientParanoidIndicator, _mappingHelper ) )
                .WithPatientParanoidIndicatorNote ( densAsiPsychiatricStatusDto.PatientParanoidIndicatorNote )
                .WithPatientTroubleConcentratingIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PatientTroubleConcentratingIndicator, _mappingHelper ) )
                .WithPatientTroubleConcentratingIndicatorNote ( densAsiPsychiatricStatusDto.PatientTroubleConcentratingIndicatorNote )
                .WithPatientThoughtsOfSuicideIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.PatientThoughtsOfSuicideIndicator, _mappingHelper ) )
                .WithPatientThoughtsOfSuicideIndicatorNote ( densAsiPsychiatricStatusDto.PatientThoughtsOfSuicideIndicatorNote )
                .WithPatientCounselingDensAsiInterviewerRating ( patientCounselingDensAsiInterviewerRating )
                .WithPatientCounselingDensAsiInterviewerRatingNote ( densAsiPsychiatricStatusDto.PatientCounselingDensAsiInterviewerRatingNote )
                .WithConfidenceDistortedByPatientMisrepresentationIndicator (
                    densAsiPsychiatricStatusDto.ConfidenceDistortedByPatientMisrepresentationIndicator )
                .WithConfidenceDistortedByPatientMisrepresentationIndicatorNote (
                    densAsiPsychiatricStatusDto.ConfidenceDistortedByPatientMisrepresentationIndicatorNote )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator (
                    densAsiPsychiatricStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote (
                    densAsiPsychiatricStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote )
                .WithSectionNote ( densAsiPsychiatricStatusDto.SectionNote )
                .WithHorribleExperiencesIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiPsychiatricStatusDto.HorribleExperiencesIndicator, _mappingHelper ) )
                .WithHorribleExperiencesIndicatorNote ( densAsiPsychiatricStatusDto.HorribleExperiencesIndicatorNote )
                .WithNightmaresLastThirtyDaysDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiPsychiatricStatusDto.NightmaresLastThirtyDaysDensAsiPatientRating, _mappingHelper ) )
                .WithNightmaresLastThirtyDaysDensAsiPatientRatingNote ( densAsiPsychiatricStatusDto.NightmaresLastThirtyDaysDensAsiPatientRatingNote )
                .WithTraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiPsychiatricStatusDto.TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating, _mappingHelper ) )
                .WithTraumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote (
                    densAsiPsychiatricStatusDto.TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote )
                .WithOnGuardLastThirtyDaysDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiPsychiatricStatusDto.OnGuardLastThirtyDaysDensAsiPatientRating, _mappingHelper ) )
                .WithOnGuardLastThirtyDaysDensAsiPatientRatingNote ( densAsiPsychiatricStatusDto.OnGuardLastThirtyDaysDensAsiPatientRatingNote )
                .WithFeltNumbLastThirtyDaysDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiPsychiatricStatusDto.FeltNumbLastThirtyDaysDensAsiPatientRating, _mappingHelper ) )
                .WithFeltNumbLastThirtyDaysDensAsiPatientRatingNote ( densAsiPsychiatricStatusDto.FeltNumbLastThirtyDaysDensAsiPatientRatingNote )
                .Build ();

            AggregateRoot.ReviseDensAsiPsychiatricStatus ( densAsiPsychiatricStatusNew );

            densAsiPsychiatricStatusDto.Key = AggregateRoot.DensAsiPsychiatricStatus.Key;

            return true;
        }

        #endregion
    }
}
