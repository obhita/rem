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
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Class for handling save location profile request.
    /// </summary>
    public class SaveLocationProfileRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<LocationProfileDto>, DtoResponse<LocationProfileDto>, LocationProfileDto, Location>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLocationProfileRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveLocationProfileRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
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
        protected override bool ProcessSingleAggregate ( LocationProfileDto dto, Location location )
        {
            var geographicalRegion = _mappingHelper.MapLookupField<GeographicalRegion> ( dto.GeographicalRegion );

            location.ReviseLocationProfile (
                new LocationProfileBuilder ().WithLocationName (
                    new LocationNameBuilder ().WithName ( dto.Name ).WithDisplayName ( dto.DisplayName ).Build () ).WithEffectiveDateRange (
                        new DateRange ( dto.StartDate, dto.EndDate ) ).WithWebsiteUrlName ( dto.WebsiteUrlName ).WithGeographicalRegion (
                            geographicalRegion ).Build () );

            _mappingResult &= MapEmailAddresses ( location, dto );

            return _mappingResult;
        }

        private static void RemoveLocationEmailAddress ( LocationEmailAddressDto dto, Location location, LocationEmailAddress locationEmailAddress )
        {
            location.RemoveEmailAddress ( locationEmailAddress );
        }

        private void AddLocationEmailAddress ( LocationEmailAddressDto dto, Location location )
        {
            var emailAddressType = _mappingHelper.MapLookupField<LocationEmailAddressType> ( dto.LocationEmailAddressType );
            location.AddEmailAddress ( new LocationEmailAddress ( new EmailAddress ( dto.EmailAddress ), emailAddressType ) );
        }

        private void ChangeLocationEmailAddress (
            LocationEmailAddressDto locationEmailAddressDto, Location location, LocationEmailAddress locationEmailAddress )
        {
            RemoveLocationEmailAddress ( locationEmailAddressDto, location, locationEmailAddress );
            AddLocationEmailAddress ( locationEmailAddressDto, location );
        }

        private bool MapEmailAddresses ( Location location, LocationProfileDto dto )
        {
            var result =
                new AggregateNodeCollectionMapper<LocationEmailAddressDto, Location, LocationEmailAddress> (
                    dto.EmailAddresses, location, location.EmailAddresses ).MapRemovedItem ( RemoveLocationEmailAddress ).MapAddedItem (
                        AddLocationEmailAddress ).MapChangedItem ( ChangeLocationEmailAddress ).Map ();

            return result;
        }

        #endregion
    }
}
