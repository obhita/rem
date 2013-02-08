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
    /// Class for handling save gpra social connectedness request.
    /// </summary>
    public class SaveGpraSocialConnectednessRequestHandler :
        SaveAggregateNodeDtoRequestHandlerBase<SaveDtoRequest<GpraSocialConnectednessDto>, DtoResponse<GpraSocialConnectednessDto>, GpraSocialConnectednessDto,
            Domain.Clinical.GpraModule.GpraInterview, GpraSocialConnectedness>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGpraSocialConnectednessRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveGpraSocialConnectednessRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="gpraSocialConnectednessDto">The gpra social connectedness dto.</param>
        /// <param name="gpraSocialConnectedness">The gpra social connectedness.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate (
            GpraSocialConnectednessDto gpraSocialConnectednessDto, GpraSocialConnectedness gpraSocialConnectedness )
        {
            var propertyMappingResult = MappingProperties ( gpraSocialConnectednessDto, gpraSocialConnectedness );
            _mappingResult &= propertyMappingResult;

            return _mappingResult;
        }

        private bool MappingProperties ( GpraSocialConnectednessDto gpraSocialConnectednessDto, GpraSocialConnectedness gpraSocialConnectedness )
        {
            AggregateRoot.ReviseGpraSocialConnectedness (
                new GpraSocialConnectednessSection (
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraSocialConnectednessDto.AttendOtherGroupsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraSocialConnectednessDto.AttendOtherGroupsIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraSocialConnectednessDto.AttendReligiousGroupsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraSocialConnectednessDto.AttendReligiousGroupsIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraSocialConnectednessDto.AttendVoluntaryGroupsCount, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraSocialConnectednessDto.AttendVoluntaryGroupsIndicator, _mappingHelper ),
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType<GpraTroubleContact> (
                        gpraSocialConnectednessDto.GpraTroubleContact, _mappingHelper ),
                    gpraSocialConnectednessDto.GpraTroubleContactSpecificationNote,
                    GpraNonResponseTypeMapper.MapToGpraNonResponseType ( gpraSocialConnectednessDto.InteractFamilyFriendsIndicator, _mappingHelper )
                    ) );
            gpraSocialConnectednessDto.Key = AggregateRoot.GpraSocialConnectedness.Key;
            return true;
        }

        #endregion
    }
}
