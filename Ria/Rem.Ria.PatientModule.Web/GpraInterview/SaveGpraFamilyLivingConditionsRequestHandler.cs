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
    /// Class for handling save gpra family living conditions request.
    /// </summary>
    public class SaveGpraFamilyLivingConditionsRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<GpraFamilyLivingConditionsDto>, DtoResponse<GpraFamilyLivingConditionsDto>, GpraFamilyLivingConditionsDto,
            Domain.Clinical.GpraModule.GpraInterview, GpraFamilyLivingConditions>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGpraFamilyLivingConditionsRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveGpraFamilyLivingConditionsRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="gpraFamilyLivingConditionsDto">The gpra family living conditions dto.</param>
        /// <param name="gpraFamilyLivingConditions">The gpra family living conditions.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            GpraFamilyLivingConditionsDto gpraFamilyLivingConditionsDto, GpraFamilyLivingConditions gpraFamilyLivingConditions )
        {
            var propertyMappingResult = MappingProperties ( gpraFamilyLivingConditionsDto, gpraFamilyLivingConditions );
            _mappingResult &= propertyMappingResult;

            return _mappingResult;
        }

        private bool MappingProperties (
            GpraFamilyLivingConditionsDto gpraFamilyLivingConditionsDto, GpraFamilyLivingConditions gpraFamilyLivingConditions )
        {
            AggregateRoot.ReviseGpraFamilyLivingConditions (
                new GpraFamilyLivingConditionsSection (
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraFamilyLivingConditionsDto.ChildCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraFamilyLivingConditionsDto.ChildrenInChildProtectionIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraFamilyLivingConditionsDto.ChildrenInChildProtectionCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraFamilyLivingConditionsDto.ChildrenIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraEffectDueToDrugUse> (
                        gpraFamilyLivingConditionsDto.EmotionalProblemsGpraEffectDueToDrugUse, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraEffectDueToDrugUse> (
                        gpraFamilyLivingConditionsDto.GiveUpImportantActivitiesGpraEffectDueToDrugUse, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraHousingType> (
                        gpraFamilyLivingConditionsDto.GpraHousingType, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraPlaceToLive> (
                        gpraFamilyLivingConditionsDto.MostTimeGpraPlaceToLive, _mappingHelper ),
                    gpraFamilyLivingConditionsDto.OtherHousingTypeSpecificationNote,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraFamilyLivingConditionsDto.PatientLostParentalRightsChildCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraFamilyLivingConditionsDto.PregnancyIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraEffectDueToDrugUse> (
                        gpraFamilyLivingConditionsDto.StressGpraEffectDueToDrugUse, _mappingHelper )
                    ) );
            gpraFamilyLivingConditionsDto.Key = AggregateRoot.GpraFamilyLivingConditions.Key;
            return true;
        }

        #endregion
    }
}
