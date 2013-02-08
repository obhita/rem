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
    /// Class for handling save gpra demographics request.
    /// </summary>
    public class SaveGpraDemographicsRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<GpraDemographicsDto>, DtoResponse<GpraDemographicsDto>, GpraDemographicsDto, Domain.Clinical.GpraModule.GpraInterview,
            GpraDemographics>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGpraDemographicsRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveGpraDemographicsRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="gpraDemographicsDto">The gpra demographics dto.</param>
        /// <param name="gpraDemographics">The gpra demographics.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( GpraDemographicsDto gpraDemographicsDto, GpraDemographics gpraDemographics )
        {
            var propertyMappingResult = MappingProperties ( gpraDemographicsDto, gpraDemographics );
            _mappingResult &= propertyMappingResult;

            return _mappingResult;
        }

        private bool MappingProperties ( GpraDemographicsDto gpraDemographicsDto, GpraDemographics gpraDemographics )
        {
            AggregateRoot.ReviseGpraDemographics (
                new GpraDemographicsSection (
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.BirthDate, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.EthnicGroupCentralAmericanIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.EthnicGroupCubanIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.EthnicGroupDominicanIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.EthnicGroupMexicanIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.EthnicGroupOtherIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.EthnicGroupPuertoRicanIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.EthnicGroupSouthAmericanIndicator, _mappingHelper ),
                    gpraDemographicsDto.EthnicGroupSpecificationNote,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraPatientGender> ( gpraDemographicsDto.GpraPatientGender, _mappingHelper ),
                    gpraDemographicsDto.GpraPatientGenderSpecificationNote,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.HispanicLatinoIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.RaceAlaskaNativeIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.RaceAmericanIndianIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.RaceAsianIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.RaceBlackAfricanAmericanIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType (
                        gpraDemographicsDto.RaceNativeHawaiianOtherPacificIslanderIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.RaceWhiteIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraDemographicsDto.VeteranIndicator, _mappingHelper )
                    ) );
            gpraDemographicsDto.Key = AggregateRoot.GpraDemographics.Key;
            return true;
        }

        #endregion
    }
}
