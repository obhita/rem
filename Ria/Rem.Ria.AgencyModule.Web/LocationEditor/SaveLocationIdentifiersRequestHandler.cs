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
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Class for handling save location identifiers request.
    /// </summary>
    public class SaveLocationIdentifiersRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<LocationIdentifiersDto>, DtoResponse<LocationIdentifiersDto>, LocationIdentifiersDto, Location>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLocationIdentifiersRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveLocationIdentifiersRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="location">The location.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( LocationIdentifiersDto dto, Location location )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<LocationIdentifierDto, Location, LocationIdentifier> (
                    dto.LocationIdentifiers, location, location.LocationIdentifiers ).MapRemovedItem ( RemoveLocationIdentifier ).MapAddedItem (
                        AddLocationIdentifier ).MapChangedItem ( ChangeLocationIdentifier ).Map ();

            return _mappingResult;
        }

        private static void RemoveLocationIdentifier ( LocationIdentifierDto dto, Location location, LocationIdentifier locationIdentifier )
        {
            location.RemoveIdentifier ( locationIdentifier );
        }

        private void AddLocationIdentifier ( LocationIdentifierDto dto, Location location )
        {
            var locationIdentifierType = _mappingHelper.MapLookupField<LocationIdentifierType> ( dto.LocationIdentifierType );
            location.AddIdentifier (
                new LocationIdentifierBuilder ().WithLocationIdentifierType ( locationIdentifierType ).WithIdentifierNumber ( dto.IdentifierNumber )
                .WithEffectiveDateRange ( new DateRange ( dto.StartDate, dto.EndDate ) ) );
        }

        private void ChangeLocationIdentifier ( LocationIdentifierDto dto, Location location, LocationIdentifier locationIdentifier )
        {
            RemoveLocationIdentifier ( dto, location, locationIdentifier );
            AddLocationIdentifier ( dto, location );
        }

        #endregion
    }
}
