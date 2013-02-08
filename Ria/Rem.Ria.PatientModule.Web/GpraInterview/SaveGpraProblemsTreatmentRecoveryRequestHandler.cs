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
    /// Class for handling save gpra problems treatment recovery request.
    /// </summary>
    public class SaveGpraProblemsTreatmentRecoveryRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<GpraProblemsTreatmentRecoveryDto>, DtoResponse<GpraProblemsTreatmentRecoveryDto>, GpraProblemsTreatmentRecoveryDto,
            Domain.Clinical.GpraModule.GpraInterview, GpraProblemsTreatmentRecovery>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGpraProblemsTreatmentRecoveryRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveGpraProblemsTreatmentRecoveryRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="gpraProblemsTreatmentRecoveryDto">The gpra problems treatment recovery dto.</param>
        /// <param name="gpraProblemsTreatmentRecovery">The gpra problems treatment recovery.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            GpraProblemsTreatmentRecoveryDto gpraProblemsTreatmentRecoveryDto, GpraProblemsTreatmentRecovery gpraProblemsTreatmentRecovery )
        {
            var propertyMappingResult = MappingProperties ( gpraProblemsTreatmentRecoveryDto, gpraProblemsTreatmentRecovery );
            _mappingResult &= propertyMappingResult;

            return _mappingResult;
        }

        private bool MappingProperties (
            GpraProblemsTreatmentRecoveryDto gpraProblemsTreatmentRecoveryDto, GpraProblemsTreatmentRecovery gpraProblemsTreatmentRecovery )
        {
            AggregateRoot.ReviseGpraProblemsTreatmentRecovery (
                new GpraProblemsTreatmentRecoverySection (
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.AnxietyDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.BrainMisfunctionDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.DepressionDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.ErMentalEmotionalDifficultiesTimeCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.ErPhysicalComplaintIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.ErPhysicalComplaintTimeCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.ErAlcoholSubstanceAbuseIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.ErAlcoholSubstanceAbuseTimeCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.ErMentalEmotionalDifficultiesIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraOverallHealth> (
                        gpraProblemsTreatmentRecoveryDto.GpraOverallHealth, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraPsychologicalImpact> (
                        gpraProblemsTreatmentRecoveryDto.GpraPsychologicalImpact, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraSexualActivity> (
                        gpraProblemsTreatmentRecoveryDto.GpraSexualActivity, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.HallucinationsDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.HivTestIndicator, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.HivTestResultsKnownIndicator,
                    gpraProblemsTreatmentRecoveryDto.InpatientAlcoholSubstanceAbuseIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.InpatientAlcoholSubstanceAbuseNightCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.InpatientMentalEmotionalDifficultiesIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.InpatientMentalEmotionalDifficultiesNightCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.InpatientPhysicalComplaintIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.InpatientPhysicalComplaintNightCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.OutpatientAlcoholSubstanceAbuseIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.OutpatientAlcoholSubstanceAbuseTimeCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.OutpatientMentalEmotionalDifficultiesIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.OutpatientMentalEmotionalDifficultiesTimeCount, _mappingHelper ),
                    gpraProblemsTreatmentRecoveryDto.OutpatientPhysicalComplaintIndicator,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.OutpatientPhysicalComplaintTimeCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.PsychologicalEmotionalMedicationDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.SexualContactsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.SuicideDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.UnprotectedSexualContactsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.UnprotectedSexualHighSaContactsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.UnprotectedSexualHivContactsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraProblemsTreatmentRecoveryDto.UnprotectedSexualInjectionDrugContactsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraProblemsTreatmentRecoveryDto.ViolentBehaviorDayCount, _mappingHelper ) ) );
            gpraProblemsTreatmentRecoveryDto.Key = AggregateRoot.GpraProblemsTreatmentRecovery.Key;
            return true;
        }

        #endregion
    }
}
