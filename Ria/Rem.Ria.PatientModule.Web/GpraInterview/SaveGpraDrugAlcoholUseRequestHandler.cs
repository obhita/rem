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
    /// Class for handling save gpra drug alcohol use request.
    /// </summary>
    public class SaveGpraDrugAlcoholUseRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<GpraDrugAlcoholUseDto>, DtoResponse<GpraDrugAlcoholUseDto>, GpraDrugAlcoholUseDto, Domain.Clinical.GpraModule.GpraInterview,
            GpraDrugAlcoholUse>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGpraDrugAlcoholUseRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveGpraDrugAlcoholUseRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="gpraDrugAlcoholUseDto">The gpra drug alcohol use dto.</param>
        /// <param name="gpraDrugAlcoholUse">The gpra drug alcohol use.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( GpraDrugAlcoholUseDto gpraDrugAlcoholUseDto, GpraDrugAlcoholUse gpraDrugAlcoholUse )
        {
            var propertyMappingResult = MappingProperties ( gpraDrugAlcoholUseDto, gpraDrugAlcoholUse );
            _mappingResult &= propertyMappingResult;

            return _mappingResult;
        }

        private bool MappingProperties ( GpraDrugAlcoholUseDto gpraDrugAlcoholUseDto, GpraDrugAlcoholUse gpraDrugAlcoholUse )
        {
            AggregateRoot.ReviseGpraDrugAlcoholUse (
                new GpraDrugAlcoholUseSection (
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraDrugAlcoholUseDto.AlcoholIntoxicationFivePlusDrinksDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraDrugAlcoholUseDto.AlcoholIntoxicationFourOrFewerDrinksDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.AnyAlcoholDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.BarbituratesDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.BarbituratesGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.BenzondiazepinesDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.BenzondiazepinesGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.CocaineCrackDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.CocaineCrackGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.CodeineDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.CodeineGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.DarvonDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.DarvonGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.DermerolDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.DermerolGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.DiluadidDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.DiluadidGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.HallucinogensDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.HallucinogensGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.HeroinDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.HeroinGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.IllegalDrugsDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.InhalantsDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.InhalantsGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.InjectedDrugsIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraFrequencyOfUseOfUsedItems> (
                        gpraDrugAlcoholUseDto.InjectionGpraFrequencyOfUseOfUsedItems, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.KetamineDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.KetamineGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.MarijuanaHashishDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.MarijuanaHashishGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.MethamphetamineDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.MethamphetamineGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.MorphineDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.MorphineGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.NonPrescriptionGhbDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.NonPrescriptionGhbGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.NonPrescriptionMethadoneDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.NonPrescriptionMethodoneGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.OtherIllegalDrugsDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.OtherIllegalDrugsGpraDrugRoute, _mappingHelper ),
                    gpraDrugAlcoholUseDto.OtherIllegalDrugsSpecificationNote,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.OxycontinOxycodoneDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.OxycontinOxycodoneGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.PercocetDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.PercocetGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.SameDayAlcoholDrugsDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.TranquilizersDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> (
                        gpraDrugAlcoholUseDto.TranquilizersGpraDrugRoute, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDrugAlcoholUseDto.TylenolDayCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraDrugRoute> ( gpraDrugAlcoholUseDto.TylenolGpraDrugRoute, _mappingHelper )
                    ) );
            gpraDrugAlcoholUseDto.Key = AggregateRoot.GpraDrugAlcoholUse.Key;
            return true;
        }

        #endregion
    }
}
