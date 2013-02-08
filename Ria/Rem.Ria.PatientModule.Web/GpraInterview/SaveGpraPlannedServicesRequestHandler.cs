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

using Rem.Domain.Clinical.GpraModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Class for handling save gpra planned services request.
    /// </summary>
    public class SaveGpraPlannedServicesRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<GpraPlannedServicesDto>, DtoResponse<GpraPlannedServicesDto>, GpraPlannedServicesDto, Domain.Clinical.GpraModule.GpraInterview,
            GpraPlannedServices>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGpraPlannedServicesRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveGpraPlannedServicesRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="gpraPlannedServicesDto">The gpra planned services dto.</param>
        /// <param name="gpraPlannedServices">The gpra planned services.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( GpraPlannedServicesDto gpraPlannedServicesDto, GpraPlannedServices gpraPlannedServices )
        {
            var propertyMappingResult = MappingProperties ( gpraPlannedServicesDto, gpraPlannedServices );
            _mappingResult &= propertyMappingResult;

            return _mappingResult;
        }

        private bool MappingProperties ( GpraPlannedServicesDto gpraPlannedServicesDto, GpraPlannedServices gpraPlannedServices )
        {
            var gpraDetoxificationLocation =
                _mappingHelper.MapLookupField<GpraDetoxificationLocation> ( gpraPlannedServicesDto.ModalityGpraDetoxificationLocation );

            AggregateRoot.ReviseGpraPlannedServices (
                new GpraPlannedServicesSection (
                    gpraPlannedServicesDto.AfterCareContinuingCareIndicator,
                    gpraPlannedServicesDto.AfterCareOtherIndicator,
                    gpraPlannedServicesDto.AfterCareRecoveryCoachingIndicator,
                    gpraPlannedServicesDto.AfterCareReplasePreventionIndicator,
                    gpraPlannedServicesDto.AfterCareSelfHelpSupportGroupsIndicator,
                    gpraPlannedServicesDto.AfterCareSpecificationNote,
                    gpraPlannedServicesDto.AfterCareSpiritualSupportIndicator,
                    gpraPlannedServicesDto.CaseMgmtChildCareIndicator,
                    gpraPlannedServicesDto.CaseMgmtEmploymentCoachingIndicator,
                    gpraPlannedServicesDto.CaseMgmtFamilyServicesIndicator,
                    gpraPlannedServicesDto.CaseMgmtHivAidsServiceIndicator,
                    gpraPlannedServicesDto.CaseMgmtIndividualServicesCoordinationIndicator,
                    gpraPlannedServicesDto.CaseMgmtOtherIndicator,
                    gpraPlannedServicesDto.CaseMgmtPreemploymentIndicator,
                    gpraPlannedServicesDto.CaseMgmtSpecificationNote,
                    gpraPlannedServicesDto.CaseMgmtTransitionalDrugFreeHousingIndicator,
                    gpraPlannedServicesDto.CaseMgmtTransportationIndicator,
                    gpraPlannedServicesDto.EducationHivAidsIndicator,
                    gpraPlannedServicesDto.EducationOtherIndicator,
                    gpraPlannedServicesDto.EducationSaIndicator,
                    gpraPlannedServicesDto.EducationSpecificationNote,
                    gpraPlannedServicesDto.MedicalAlcoholDrugTestIndicator,
                    gpraPlannedServicesDto.MedicalCareIndicator,
                    gpraPlannedServicesDto.MedicalHivAidsSupportAndTestingIndicator,
                    gpraPlannedServicesDto.MedicalOtherIndicator,
                    gpraPlannedServicesDto.MedicalSpecificationNote,
                    gpraPlannedServicesDto.ModalityAfterCareIndicator,
                    gpraPlannedServicesDto.ModalityCaseMgmtIndicator,
                    gpraPlannedServicesDto.ModalityDayTreatmentIndicator,
                    gpraDetoxificationLocation,
                    gpraPlannedServicesDto.ModalityInpatientHospitalIndicator,
                    gpraPlannedServicesDto.ModalityIntensiveOutpatientIndicator,
                    gpraPlannedServicesDto.ModalityMethadoneIndicator,
                    gpraPlannedServicesDto.ModalityOtherSpecificationIndicator,
                    gpraPlannedServicesDto.ModalityOutpatientIndicator,
                    gpraPlannedServicesDto.ModalityOutreachIndicator,
                    gpraPlannedServicesDto.ModalityRecoverySupportIndicator,
                    gpraPlannedServicesDto.ModalityResidentialRehabilitationIndicator,
                    gpraPlannedServicesDto.ModalitySpecificationNote,
                    gpraPlannedServicesDto.PeerToPeerRecoverySupportAlcholDrugFreeActivitiesIndicator,
                    gpraPlannedServicesDto.PeerToPeerRecoverySupportCoachingIndicator,
                    gpraPlannedServicesDto.PeerToPeerRecoverySupportHousingIndicator,
                    gpraPlannedServicesDto.PeerToPeerRecoverySupportInformationReferralIndicator,
                    gpraPlannedServicesDto.PeerToPeerRecoverySupportOtherIndicator,
                    gpraPlannedServicesDto.PeerToPeerRecoverySupportSpecificationNote,
                    gpraPlannedServicesDto.TreatmentAssessmentIndicator,
                    gpraPlannedServicesDto.TreatmentBriefInterventionIndicator,
                    gpraPlannedServicesDto.TreatmentBriefTreatmentIndicator,
                    gpraPlannedServicesDto.TreatmentCooccuringTreatmentIndicator,
                    gpraPlannedServicesDto.TreatmentFamilyCounselingIndicator,
                    gpraPlannedServicesDto.TreatmentGroupCounselingIndicator,
                    gpraPlannedServicesDto.TreatmentHivAidsCounselingIndicator,
                    gpraPlannedServicesDto.TreatmentIndividualCounselingIndicator,
                    gpraPlannedServicesDto.TreatmentOtherSpecificationIndicator,
                    gpraPlannedServicesDto.TreatmentPharmacologicalInterventionsIndicator,
                    gpraPlannedServicesDto.TreatmentRecoveryPlanningIndicator,
                    gpraPlannedServicesDto.TreatmentReferralToTreatmentIndicator,
                    gpraPlannedServicesDto.TreatmentScreeningIndicator,
                    gpraPlannedServicesDto.TreatmentSpecificationNote ) );
            gpraPlannedServicesDto.Key = AggregateRoot.GpraPlannedServices.Key;
            return true;
        }

        #endregion
    }
}
