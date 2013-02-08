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

using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.TedsModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Class for handling save grpa discharge request.
    /// </summary>
    public class SaveTedsAdmissionInterviewRequestHandler : SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<TedsAdmissionInterviewDto>, 
        DtoResponse<TedsAdmissionInterviewDto>, 
        TedsAdmissionInterviewDto, 
        TedsAdmissionInterview>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveTedsAdmissionInterviewRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveTedsAdmissionInterviewRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="tedsAdmissionInterview">The teds admission interview.</param>
        /// <returns>A bool.</returns>
        protected override bool ProcessSingleAggregate(TedsAdmissionInterviewDto dto, TedsAdmissionInterview tedsAdmissionInterview)
        {
            tedsAdmissionInterview.ReviseArrestsInPastThirtyDaysCount(TedsAnswerMapper.MapToTedsAnswer(dto.ArrestsInPastThirtyDaysCount, _mappingHelper));
            tedsAdmissionInterview.ReviseCoDependentIndicator(dto.CoDependentIndicator);

            //TedsAnswer<DsmDiagnosisResponse> dsmDiagnosis = null;
            //if (dto.DsmDiagnosis != null)
            //{
            //    if (dto.DsmDiagnosis.Response != null || dto.DsmDiagnosis.TedsNonResponse != null)
            //    {
            //        dsmDiagnosis = dto.DsmDiagnosis.TedsNonResponse != null
            //                     ? new TedsAnswer<DsmDiagnosisResponse>(
            //                           _mappingHelper.MapLookupField<TedsNonResponse>(dto.DsmDiagnosis.TedsNonResponse))
            //                     : new TedsAnswer<DsmDiagnosisResponse>(new DsmDiagnosisResponse(dto.DsmDiagnosis.Response));
            //    }
            //}
            //tedsAdmissionInterview.ReviseDsmDiagnosis(dsmDiagnosis);

            tedsAdmissionInterview.ReviseIncomeSourceType(TedsAnswerMapper.MapToTedsAnswer<IncomeSourceType>(dto.IncomeSourceType, _mappingHelper));

            tedsAdmissionInterview.ReviseLivingArrangementsType(TedsAnswerMapper.MapToTedsAnswer<LivingArrangementsType>(dto.LivingArrangementsType, _mappingHelper));

            tedsAdmissionInterview.ReviseVeteranStatusIndicator(TedsAnswerMapper.MapToTedsAnswer(dto.VeteranStatusIndicator, _mappingHelper));

            tedsAdmissionInterview.ReviseMedicationAssistedOpioidTherapyIndicator(TedsAnswerMapper.MapToTedsAnswer(dto.MedicationAssistedOpioidTherapyIndicator, _mappingHelper));

            tedsAdmissionInterview.ReviseOtherPsychiatricProblemIndicator(TedsAnswerMapper.MapToTedsAnswer(dto.OtherPsychiatricProblemIndicator, _mappingHelper));

            tedsAdmissionInterview.ReviseParticipatedSelfHelpGroupInPastThirtyDaysType(TedsAnswerMapper.MapToTedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType>(dto.ParticipatedSelfHelpGroupInPastThirtyDaysType, _mappingHelper));

            tedsAdmissionInterview.RevisePrimaryPaymentSourceType(TedsAnswerMapper.MapToTedsAnswer<PrimaryPaymentSourceType>(dto.PrimaryPaymentSourceType, _mappingHelper));

            tedsAdmissionInterview.RevisePriorTreatmentEpisodesCount(TedsAnswerMapper.MapToTedsAnswer(dto.PriorTreatmentEpisodesCount, _mappingHelper));

            TedsAdmissionInterviewSubstanceUsage primaryTedsAdmissionInterviewSubstanceUsage = null;
            var primarySubstanceProblemType = TedsAnswerMapper.MapToTedsAnswer<SubstanceProblemType> (dto.PrimarySubstanceProblemType, _mappingHelper );
            var primaryUseFrequencyType = TedsAnswerMapper.MapToTedsAnswer<UseFrequencyType> ( dto.PrimaryUseFrequencyType, _mappingHelper );
            var primaryFirstUseAge = TedsAnswerMapper.MapToTedsAnswer ( dto.PrimaryFirstUseAge, _mappingHelper );
            var primaryUsualAdministrationRouteType = TedsAnswerMapper.MapToTedsAnswer<UsualAdministrationRouteType> ( dto.PrimaryUsualAdministrationRouteType, _mappingHelper );
            var primaryDetailedDrugCode = TedsAnswerMapper.MapToTedsAnswer<DetailedDrugCode, DetailedDrugCodeDto> ( dto.PrimaryDetailedDrugCode, _mappingHelper );

            if (primarySubstanceProblemType != null || primaryUseFrequencyType != null || primaryFirstUseAge != null || primaryUsualAdministrationRouteType != null || primaryDetailedDrugCode != null)
            {
                primaryTedsAdmissionInterviewSubstanceUsage =
                    new TedsAdmissionInterviewSubstanceUsage(new SubstanceUsageAtAdmission(new SubstanceProblemAndFrequency(primarySubstanceProblemType, primaryUseFrequencyType), primaryUsualAdministrationRouteType, primaryFirstUseAge, primaryDetailedDrugCode));
            }

            TedsAdmissionInterviewSubstanceUsage secondaryTedsAdmissionInterviewSubstanceUsage = null;
            var secondarySubstanceProblemType = TedsAnswerMapper.MapToTedsAnswer<SubstanceProblemType>(dto.SecondarySubstanceProblemType, _mappingHelper);
            var secondaryUseFrequencyType = TedsAnswerMapper.MapToTedsAnswer<UseFrequencyType>(dto.SecondaryUseFrequencyType, _mappingHelper);
            var secondaryFirstUseAge = TedsAnswerMapper.MapToTedsAnswer(dto.SecondaryFirstUseAge, _mappingHelper);
            var secondaryUsualAdministrationRouteType = TedsAnswerMapper.MapToTedsAnswer<UsualAdministrationRouteType>(dto.SecondaryUsualAdministrationRouteType, _mappingHelper);
            var secondaryDetailedDrugCode = TedsAnswerMapper.MapToTedsAnswer<DetailedDrugCode, DetailedDrugCodeDto>(dto.SecondaryDetailedDrugCode, _mappingHelper);
            if (secondarySubstanceProblemType != null || secondaryUseFrequencyType != null || secondaryFirstUseAge != null || secondaryUsualAdministrationRouteType != null || secondaryDetailedDrugCode != null)
            {
                secondaryTedsAdmissionInterviewSubstanceUsage =
                    new TedsAdmissionInterviewSubstanceUsage(new SubstanceUsageAtAdmission(new SubstanceProblemAndFrequency(secondarySubstanceProblemType, secondaryUseFrequencyType), secondaryUsualAdministrationRouteType, secondaryFirstUseAge, secondaryDetailedDrugCode));
            }

            TedsAdmissionInterviewSubstanceUsage tertiaryTedsAdmissionInterviewSubstanceUsage = null;
            var tertiarySubstanceProblemType = TedsAnswerMapper.MapToTedsAnswer<SubstanceProblemType>(dto.TertiarySubstanceProblemType, _mappingHelper);
            var tertiaryUseFrequencyType = TedsAnswerMapper.MapToTedsAnswer<UseFrequencyType>(dto.TertiaryUseFrequencyType, _mappingHelper);
            var tertiaryFirstUseAge = TedsAnswerMapper.MapToTedsAnswer(dto.TertiaryFirstUseAge, _mappingHelper);
            var tertiaryUsualAdministrationRouteType = TedsAnswerMapper.MapToTedsAnswer<UsualAdministrationRouteType>(dto.TertiaryUsualAdministrationRouteType, _mappingHelper);
            var tertiaryDetailedDrugCode = TedsAnswerMapper.MapToTedsAnswer<DetailedDrugCode, DetailedDrugCodeDto>(dto.TertiaryDetailedDrugCode, _mappingHelper);

            if (tertiarySubstanceProblemType != null || tertiaryUseFrequencyType != null || tertiaryFirstUseAge != null || tertiaryUsualAdministrationRouteType != null || tertiaryDetailedDrugCode != null)
            {
                tertiaryTedsAdmissionInterviewSubstanceUsage =
                    new TedsAdmissionInterviewSubstanceUsage(new SubstanceUsageAtAdmission(new SubstanceProblemAndFrequency(tertiarySubstanceProblemType, tertiaryUseFrequencyType), tertiaryUsualAdministrationRouteType, tertiaryFirstUseAge, tertiaryDetailedDrugCode));
            }

            tedsAdmissionInterview.ReviseTedsAdmissionInterviewSubstanceUsages(primaryTedsAdmissionInterviewSubstanceUsage, secondaryTedsAdmissionInterviewSubstanceUsage, tertiaryTedsAdmissionInterviewSubstanceUsage);

            tedsAdmissionInterview.ReviseTedsEducationYearCount(TedsAnswerMapper.MapToTedsAnswer(dto.TedsEducationYearCount, _mappingHelper));

            var tedsEmploymentStatus = TedsAnswerMapper.MapToTedsAnswer<TedsEmploymentStatus>(dto.TedsEmploymentStatusInformationTedsEmploymentStatus, _mappingHelper);
            var detailedNotInLaborForce = TedsAnswerMapper.MapToTedsAnswer<DetailedNotInLaborForce>(dto.TedsEmploymentStatusInformationDetailedNotInLaborForce, _mappingHelper);
            if (tedsEmploymentStatus != null || detailedNotInLaborForce != null)
            {
                tedsAdmissionInterview.ReviseTedsEmploymentStatusInformation (new TedsEmploymentStatusInformation (tedsEmploymentStatus, detailedNotInLaborForce));       
            }
            else
            {
                tedsAdmissionInterview.ReviseTedsEmploymentStatusInformation ( null );
            }

            tedsAdmissionInterview.ReviseTedsEthnicity(TedsAnswerMapper.MapToTedsAnswer<TedsEthnicity>(dto.TedsEthnicity, _mappingHelper));

            var tedsGender = TedsAnswerMapper.MapToTedsAnswer<TedsGender>(dto.TedsGenderInformationTedsGender, _mappingHelper);
            var pregnantIndicator = TedsAnswerMapper.MapToTedsAnswer(dto.TedsGenderInformationPregnantIndicator, _mappingHelper);
            if (tedsGender != null || pregnantIndicator != null)
            {
                tedsAdmissionInterview.ReviseTedsGenderInformation ( new TedsGenderInformation ( tedsGender, pregnantIndicator ) );
            }
            else
            {
                tedsAdmissionInterview.ReviseTedsGenderInformation ( null );
            }

            tedsAdmissionInterview.ReviseTedsRace(TedsAnswerMapper.MapToTedsAnswer<TedsRace>(dto.TedsRace, _mappingHelper));

            tedsAdmissionInterview.ReviseTedsMartialStatus(TedsAnswerMapper.MapToTedsAnswer<MaritalStatus>(dto.MaritalStatus, _mappingHelper));
            
            return true;
        }
    }
}
