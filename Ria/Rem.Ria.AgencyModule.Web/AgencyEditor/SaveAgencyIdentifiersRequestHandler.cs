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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.AgencyEditor
{
    /// <summary>
    /// Class for handling save agency identifiers request.
    /// </summary>
    public class SaveAgencyIdentifiersRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<AgencyIdentifiersDto>, DtoResponse<AgencyIdentifiersDto>, AgencyIdentifiersDto, Agency>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAgencyIdentifiersRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveAgencyIdentifiersRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="agencyIdentifiersDto">The agency identifiers dto.</param>
        /// <param name="agency">The agency.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( AgencyIdentifiersDto agencyIdentifiersDto, Agency agency )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<AgencyIdentifierDto, Agency, AgencyIdentifier> (
                    agencyIdentifiersDto.AgencyIdentifiers, agency, agency.AgencyIdentifiers )
                    .MapRemovedItem ( RemoveAgencyIdentifier )
                    .MapAddedItem ( AddAgencyIdentifier )
                    .MapChangedItem ( ChangeAgencyIdentifier )
                    .Map ();

            return _mappingResult;
        }

        private static void RemoveAgencyIdentifier ( AgencyIdentifierDto agencyIdentifierDto, Agency agency, AgencyIdentifier agencyIdentifier )
        {
            agency.RemoveIdentifier ( agencyIdentifier );
        }

        private void AddAgencyIdentifier ( AgencyIdentifierDto agencyIdentifierDto, Agency agency )
        {
            var agencyIdentifierType = _mappingHelper.MapLookupField<AgencyIdentifierType> ( agencyIdentifierDto.AgencyIdentifierType );

            var addIdentifier =
                new AgencyIdentifierBuilder ().WithAgencyIdentifierType ( agencyIdentifierType ).WithIdentifierNumber (
                    agencyIdentifierDto.IdentifierNumber ).WithEffectiveDateRange (
                        new DateRange ( agencyIdentifierDto.StartDate, agencyIdentifierDto.EndDate ) ).Build ();

            agency.AddIdentifier ( addIdentifier );
        }

        private void ChangeAgencyIdentifier ( AgencyIdentifierDto agencyIdentifierDto, Agency agency, AgencyIdentifier agencyIdentifier )
        {
            RemoveAgencyIdentifier ( agencyIdentifierDto, agency, agencyIdentifier );
            AddAgencyIdentifier ( agencyIdentifierDto, agency );
        }

        #endregion
    }
}
