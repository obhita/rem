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
using AutoMapper;
using Rem.Domain.Clinical.GpraModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Factory for gpra interview dto.
    /// </summary>
    public class GpraInterviewDtoFactory : IKeyedDtoFactory<GpraInterviewDto>
    {
        #region Constants and Fields

        private readonly IKeyedDtoFactory<GpraCrimeCriminalJusticeDto> _gpraCrimeCriminalJusticeDtoFactory;
        private readonly IKeyedDtoFactory<GpraDemographicsDto> _gpraDemographicsDtoFactory;
        private readonly IKeyedDtoFactory<GpraDischargeDto> _gpraDischargeDtoFactory;
        private readonly IKeyedDtoFactory<GpraDrugAlcoholUseDto> _gpraDrugAlcoholUseDtoFactory;
        private readonly IKeyedDtoFactory<GpraFamilyLivingConditionsDto> _gpraFamilyLivingConditionsDtoFactory;
        private readonly IKeyedDtoFactory<GpraFollowUpDto> _gpraFollowUpDtoFactory;
        private readonly IKeyedDtoFactory<GpraInterviewInformationDto> _gpraInterviewInformationDtoFactory;
        private readonly IGpraInterviewRepository _gpraInterviewRepository;
        private readonly IKeyedDtoFactory<GpraPlannedServicesDto> _gpraPlannedServicesDtoFactory;
        private readonly IKeyedDtoFactory<GpraProblemsTreatmentRecoveryDto> _gpraProblemsTreatmentRecoveryDtoFactory;
        private readonly IKeyedDtoFactory<GpraProfessionalInformationDto> _gpraProfessionalInformationDtoFactory;
        private readonly IKeyedDtoFactory<GpraSocialConnectednessDto> _gpraSocialConnectednessDtoFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterviewDtoFactory"/> class.
        /// </summary>
        /// <param name="gpraInterviewRepository">The gpra interview repository.</param>
        /// <param name="gpraInterviewInformationDtoFactory">The gpra interview information dto factory.</param>
        /// <param name="gpraProblemsTreatmentRecoveryDtoFactory">The gpra problems treatment recovery dto factory.</param>
        /// <param name="gpraCrimeCriminalJusticeDtoFactory">The gpra crime criminal justice dto factory.</param>
        /// <param name="gpraProfessionalInformationDtoFactory">The gpra professional information dto factory.</param>
        /// <param name="gpraSocialConnectednessDtoFactory">The gpra social connectedness dto factory.</param>
        /// <param name="gpraDemographicsDtoFactory">The gpra demographics dto factory.</param>
        /// <param name="gpraDrugAlcoholUseDtoFactory">The gpra drug alcohol use dto factory.</param>
        /// <param name="gpraPlannedServicesDtoFactory">The gpra planned services dto factory.</param>
        /// <param name="gpraFamilyLivingConditionsDtoFactory">The gpra family living conditions dto factory.</param>
        /// <param name="gpraFollowUpDtoFactory">The gpra follow up dto factory.</param>
        /// <param name="gpraDischargeDtoFactory">The gpra discharge dto factory.</param>
        public GpraInterviewDtoFactory (
            IGpraInterviewRepository gpraInterviewRepository,
            IKeyedDtoFactory<GpraInterviewInformationDto> gpraInterviewInformationDtoFactory,
            IKeyedDtoFactory<GpraProblemsTreatmentRecoveryDto> gpraProblemsTreatmentRecoveryDtoFactory,
            IKeyedDtoFactory<GpraCrimeCriminalJusticeDto> gpraCrimeCriminalJusticeDtoFactory,
            IKeyedDtoFactory<GpraProfessionalInformationDto> gpraProfessionalInformationDtoFactory,
            IKeyedDtoFactory<GpraSocialConnectednessDto> gpraSocialConnectednessDtoFactory,
            IKeyedDtoFactory<GpraDemographicsDto> gpraDemographicsDtoFactory,
            IKeyedDtoFactory<GpraDrugAlcoholUseDto> gpraDrugAlcoholUseDtoFactory,
            IKeyedDtoFactory<GpraPlannedServicesDto> gpraPlannedServicesDtoFactory,
            IKeyedDtoFactory<GpraFamilyLivingConditionsDto> gpraFamilyLivingConditionsDtoFactory,
            IKeyedDtoFactory<GpraFollowUpDto> gpraFollowUpDtoFactory,
            IKeyedDtoFactory<GpraDischargeDto> gpraDischargeDtoFactory )
        {
            _gpraInterviewRepository = gpraInterviewRepository;
            _gpraInterviewInformationDtoFactory = gpraInterviewInformationDtoFactory;
            _gpraProblemsTreatmentRecoveryDtoFactory = gpraProblemsTreatmentRecoveryDtoFactory;
            _gpraCrimeCriminalJusticeDtoFactory = gpraCrimeCriminalJusticeDtoFactory;
            _gpraProfessionalInformationDtoFactory = gpraProfessionalInformationDtoFactory;
            _gpraSocialConnectednessDtoFactory = gpraSocialConnectednessDtoFactory;
            _gpraDemographicsDtoFactory = gpraDemographicsDtoFactory;
            _gpraDrugAlcoholUseDtoFactory = gpraDrugAlcoholUseDtoFactory;
            _gpraPlannedServicesDtoFactory = gpraPlannedServicesDtoFactory;
            _gpraFamilyLivingConditionsDtoFactory = gpraFamilyLivingConditionsDtoFactory;
            _gpraFollowUpDtoFactory = gpraFollowUpDtoFactory;
            _gpraDischargeDtoFactory = gpraDischargeDtoFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the keyed dto.
        /// </summary>
        /// <param name="key">The key of the object.</param>
        /// <returns>A <see cref="Rem.Ria.PatientModule.Web.GpraInterview.GpraInterviewDto"/></returns>
        public GpraInterviewDto CreateKeyedDto ( long key )
        {
            var gpraInterview = _gpraInterviewRepository.GetByKey ( key );

            var dto = new GpraInterviewDto ();
            dto.Key = gpraInterview.Key;

            dto.ActivityStartDateTime = gpraInterview.ActivityDateTimeRange.StartDateTime;
            dto.ActivityEndDateTime = gpraInterview.ActivityDateTimeRange.EndDateTime;
            dto.AppointmentStartDateTime = Mapper.Map<DateTime?, DateTime> ( gpraInterview.Visit.CheckedInDateTime );
            dto.ActivityType = Mapper.Map<ActivityType, ActivityTypeDto> ( gpraInterview.ActivityType );
            dto.VisitKey = gpraInterview.Visit.Key;
            dto.ClinicianKey = gpraInterview.Visit.Staff.Key;
            dto.PatientKey = gpraInterview.Visit.ClinicalCase.Patient.Key;
            dto.VisitTemplateName = gpraInterview.Visit.Name;

            // GpraInterviewInformation
            dto.GpraInterviewInfromationDto = _gpraInterviewInformationDtoFactory.CreateKeyedDto ( gpraInterview.Key );

            // GpraProblemsTreatmentRecoverySection
            dto.GpraProblemsTreatmentRecovery =
                _gpraProblemsTreatmentRecoveryDtoFactory.CreateKeyedDto ( gpraInterview.GpraProblemsTreatmentRecovery.Key );

            // GpraCrimeCriminalJusticeSection
            dto.GpraCrimeCriminalJustice = _gpraCrimeCriminalJusticeDtoFactory.CreateKeyedDto ( gpraInterview.GpraCrimeCriminalJustice.Key );

            // GpraProfessionalInformationSection
            dto.GpraProfessionalInformation = _gpraProfessionalInformationDtoFactory.CreateKeyedDto ( gpraInterview.GpraProfessionalInformation.Key );

            // GpraSocialConnectednessSection
            dto.GpraSocialConnectedness = _gpraSocialConnectednessDtoFactory.CreateKeyedDto ( gpraInterview.GpraSocialConnectedness.Key );

            // GpraDemographicsSection
            dto.GpraDemographics = _gpraDemographicsDtoFactory.CreateKeyedDto ( gpraInterview.GpraDemographics.Key );

            // GpraDrugAlcoholUseSection
            dto.GpraDrugAlcoholUse = _gpraDrugAlcoholUseDtoFactory.CreateKeyedDto ( gpraInterview.GpraDrugAlcoholUse.Key );

            // GpraPlannedServicesSection
            dto.GpraPlannedServices = _gpraPlannedServicesDtoFactory.CreateKeyedDto ( gpraInterview.GpraPlannedServices.Key );

            // GpraFamilyLivingConditionsSection
            dto.GpraFamilyLivingConditions = _gpraFamilyLivingConditionsDtoFactory.CreateKeyedDto ( gpraInterview.GpraFamilyLivingConditions.Key );

            // GpraFollowUpSection
            dto.GpraFollowUp = _gpraFollowUpDtoFactory.CreateKeyedDto ( gpraInterview.GpraFollowUp.Key );

            // GpraDischargeSection
            dto.GpraDischarge = _gpraDischargeDtoFactory.CreateKeyedDto ( gpraInterview.GpraDischarge.Key );

            return dto;
        }

        #endregion
    }
}
