using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pillar.Common.Utility;
using Rem.Domain.Clinical.TedsModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Class for handling save teds discharge interview request.
    /// </summary>
    public class SaveTedsDischargeInterviewRequestHandler : 
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<TedsDischargeInterviewDto>, DtoResponse<TedsDischargeInterviewDto>, TedsDischargeInterviewDto, TedsDischargeInterview>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveTedsDischargeInterviewRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveTedsDischargeInterviewRequestHandler(IDtoToDomainMappingHelper mappingHelper)
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The teds discharge interview dto.</param>
        /// <param name="tedsDischargeInterview">The teds discharge interview.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate(TedsDischargeInterviewDto dto, TedsDischargeInterview tedsDischargeInterview)
        {
            tedsDischargeInterview.ReviseLastFaceToFaceContactDate(dto.LastFaceToFaceContactDate);

            tedsDischargeInterview.ReviseTedsDischargeReason(
              TedsAnswerMapper.MapToTedsAnswer<TedsDischargeReason, TedsLookupBaseDto>(dto.TedsDischargeReason, _mappingHelper));

            var primarySubstance = CreateSubstanceUsage(dto.PrimarySubstanceProblemType, dto.PrimaryUseFrequencyType);
            var secondarySubstance = CreateSubstanceUsage(dto.SecondarySubstanceProblemType, dto.SecondaryUseFrequencyType);
            var tertiarySubstance = CreateSubstanceUsage(dto.TertiarySubstanceProblemType, dto.TertiaryUseFrequencyType);
            tedsDischargeInterview.ReviseTedsDischargeInterviewSubstanceUsages(primarySubstance, secondarySubstance, tertiarySubstance);

            tedsDischargeInterview.ReviseLivingArrangementsType(
              TedsAnswerMapper.MapToTedsAnswer<LivingArrangementsType, TedsLookupBaseDto>(dto.LivingArrangementsType, _mappingHelper));

            var employmentStatusInformation = CreateTedsEmploymentStatusInformation(dto);
            tedsDischargeInterview.ReviseTedsEmploymentStatusInformation ( employmentStatusInformation );

            tedsDischargeInterview.ReviseArrestsInThirtyDaysNumber(
                TedsAnswerMapper.MapToTedsAnswer<int?>(dto.ArrestsInPastThirtyDaysCount, _mappingHelper));

            tedsDischargeInterview.ReviseFrequencyOfAttendanceAtSelfHelpProgramsType(
                TedsAnswerMapper.MapToTedsAnswer<ParticipatedSelfHelpGroupInPastThirtyDaysType, TedsLookupBaseDto>(dto.ParticipatedSelfHelpGroupInPastThirtyDaysType, _mappingHelper));

            return true;
        }

        private TedsEmploymentStatusInformation CreateTedsEmploymentStatusInformation(TedsDischargeInterviewDto dto)
        {
            TedsEmploymentStatusInformation employmentStatusInformation = null;

            var employmentStatus = TedsAnswerMapper.MapToTedsAnswer<TedsEmploymentStatus, TedsLookupBaseDto> (
                dto.TedsEmploymentStatus, _mappingHelper );
            var notInLaborForce = TedsAnswerMapper.MapToTedsAnswer<DetailedNotInLaborForce, TedsLookupBaseDto>(
                    dto.DetailedNotInLaborForce, _mappingHelper);
            if (employmentStatus != null || notInLaborForce != null)
            { 
                employmentStatusInformation = new TedsEmploymentStatusInformation ( employmentStatus, notInLaborForce );
            }

            return employmentStatusInformation;
        }

        private IList<TedsDischargeInterviewSubstanceUsage> CreateSubstanceUsages(TedsDischargeInterviewDto dto)
        {
            IList<TedsDischargeInterviewSubstanceUsage> substanceUsages = new List<TedsDischargeInterviewSubstanceUsage> ();

            TedsDischargeInterviewSubstanceUsage substanceUsage = null;

            if ( dto.PrimarySubstanceProblemType != null )
            {
                substanceUsage = CreateSubstanceUsage ( dto.PrimarySubstanceProblemType, dto.PrimaryUseFrequencyType );
                if ( substanceUsage != null )
                {
                    substanceUsages.Add ( substanceUsage );
                }
            }

            if ( dto.SecondarySubstanceProblemType != null )
            {
                substanceUsage = CreateSubstanceUsage ( dto.SecondarySubstanceProblemType, dto.SecondaryUseFrequencyType );
                if ( substanceUsage != null )
                {
                    substanceUsages.Add ( substanceUsage );
                }
            }

            if ( dto.TertiarySubstanceProblemType != null )
            {
                substanceUsage = CreateSubstanceUsage ( dto.TertiarySubstanceProblemType, dto.TertiaryUseFrequencyType );
                if ( substanceUsage != null )
                {
                    substanceUsages.Add ( substanceUsage );
                }
            }

            return substanceUsages;
        }

        private TedsDischargeInterviewSubstanceUsage CreateSubstanceUsage(TedsAnswerDto<SubstanceProblemTypeDto> substanceProblemType, TedsAnswerDto<TedsLookupBaseDto> useFrequencyType)
        {
            TedsDischargeInterviewSubstanceUsage substanceUsage = null;

            var problem = TedsAnswerMapper.MapToTedsAnswer<SubstanceProblemType, SubstanceProblemTypeDto> ( substanceProblemType, _mappingHelper );
            var frequency = TedsAnswerMapper.MapToTedsAnswer<UseFrequencyType, TedsLookupBaseDto>(useFrequencyType, _mappingHelper);

            if (problem != null || frequency != null)
            {  
                var problemAndFrequency = new SubstanceProblemAndFrequency ( problem, frequency );
                substanceUsage = new TedsDischargeInterviewSubstanceUsage ( problemAndFrequency );
            }

            return substanceUsage;
        }

        #endregion
    }
}
