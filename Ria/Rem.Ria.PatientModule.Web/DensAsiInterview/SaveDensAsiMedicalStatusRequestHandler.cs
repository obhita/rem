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
    /// Class for handling save dens asi medical status request.
    /// </summary>
    public class SaveDensAsiMedicalStatusRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<DensAsiMedicalStatusDto>, DtoResponse<DensAsiMedicalStatusDto>, DensAsiMedicalStatusDto,
            Domain.Clinical.DensAsiModule.DensAsiInterview, DensAsiMedicalStatus>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDensAsiMedicalStatusRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveDensAsiMedicalStatusRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="densAsiMedicalStatusDto">The dens asi medical status dto.</param>
        /// <param name="densAsiMedicalStatus">The dens asi medical status.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( DensAsiMedicalStatusDto densAsiMedicalStatusDto, DensAsiMedicalStatus densAsiMedicalStatus )
        {
            var patientTreatmentDensAsiInterviewerRating =
                _mappingHelper.MapLookupField<DensAsiInterviewerRating> ( densAsiMedicalStatusDto.PatientTreatmentDensAsiInterviewerRating );

            var densAsiMedicalStatusNew = new DensAsiMedicalStatusSectionBuilder ()
                .WithHopitalizedForMedicalProblemsCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiMedicalStatusDto.HopitalizedForMedicalProblemsCount, _mappingHelper ) )
                .WithHopitalizedForMedicalProblemsCountNote ( densAsiMedicalStatusDto.HopitalizedForMedicalProblemsCountNote )
                .WithYearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiMedicalStatusDto.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan, _mappingHelper ) )
                .WithYearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote (
                    densAsiMedicalStatusDto.YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote )
                .WithChronicMedicalProblemThatInterferesWithLifeIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiMedicalStatusDto.ChronicMedicalProblemThatInterferesWithLifeIndicator, _mappingHelper ) )
                .WithChronicMedicalProblemThatInterferesWithLifeDescription (
                    densAsiMedicalStatusDto.ChronicMedicalProblemThatInterferesWithLifeDescription )
                .WithChronicMedicalProblemThatInterferesWithLifeNote ( densAsiMedicalStatusDto.ChronicMedicalProblemThatInterferesWithLifeNote )
                .WithTakingPrescribedMedicationsForPhysicalProblemIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiMedicalStatusDto.TakingPrescribedMedicationsForPhysicalProblemIndicator, _mappingHelper ) )
                .WithTakingPrescribedMedicationsForPhysicalProblemDescription (
                    densAsiMedicalStatusDto.TakingPrescribedMedicationsForPhysicalProblemDescription )
                .WithTakingPrescribedMedicationsForPhysicalProblemNote ( densAsiMedicalStatusDto.TakingPrescribedMedicationsForPhysicalProblemNote )
                .WithReceivePensionForPhysicalDisabilityIndicator (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType (
                        densAsiMedicalStatusDto.ReceivePensionForPhysicalDisabilityIndicator, _mappingHelper ) )
                .WithReceivePensionForPhysicalDisabilityDescription ( densAsiMedicalStatusDto.ReceivePensionForPhysicalDisabilityDescription )
                .WithReceivePensionForPhysicalDisabilityNote ( densAsiMedicalStatusDto.ReceivePensionForPhysicalDisabilityNote )
                .WithMedicalProblemsDayCount (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType ( densAsiMedicalStatusDto.MedicalProblemsDayCount, _mappingHelper ) )
                .WithMedicalProblemsDayCountNote ( densAsiMedicalStatusDto.MedicalProblemsDayCountNote )
                .WithTroubledByMedicalProblemsDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiMedicalStatusDto.TroubledByMedicalProblemsDensAsiPatientRating, _mappingHelper ) )
                .WithTroubledByMedicalProblemsDensAsiPatientRatingNote ( densAsiMedicalStatusDto.TroubledByMedicalProblemsDensAsiPatientRatingNote )
                .WithImportanceOfMedicalProblemTreatmentDensAsiPatientRating (
                    DensAsiNonResponseTypeMapper.MapToDensAsiNonResponseType<DensAsiPatientRating> (
                        densAsiMedicalStatusDto.ImportanceOfMedicalProblemTreatmentDensAsiPatientRating, _mappingHelper ) )
                .WithImportanceOfMedicalProblemTreatmentDensAsiPatientRatingNote (
                    densAsiMedicalStatusDto.ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingNote )
                .WithPatientTreatmentDensAsiInterviewerRating ( patientTreatmentDensAsiInterviewerRating )
                .WithPatientTreatmentDensAsiInterviewerRatingNote ( densAsiMedicalStatusDto.PatientTreatmentDensAsiInterviewerRatingNote )
                .WithConfidenceRateDistortedByPatientMisrepresentationIndicator (
                    densAsiMedicalStatusDto.ConfidenceRateDistortedByPatientMisrepresentationIndicator )
                .WithConfidenceRateDistortedByPatientMisrepresentationIndicatorNote (
                    densAsiMedicalStatusDto.ConfidenceRateDistortedByPatientMisrepresentationIndicatorNote )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator (
                    densAsiMedicalStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator )
                .WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote (
                    densAsiMedicalStatusDto.ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote )
                .WithSectionNote ( densAsiMedicalStatusDto.SectionNote )
                .Build ();

            AggregateRoot.ReviseDensAsiMedicalStatus ( densAsiMedicalStatusNew );
            densAsiMedicalStatusDto.Key = AggregateRoot.DensAsiMedicalStatus.Key;

            return true;
        }

        #endregion
    }
}
