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
    /// Class for handling save location addresses and phones request.
    /// </summary>
    public class SaveLocationAddressesAndPhonesRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<LocationAddressesAndPhonesDto>, DtoResponse<LocationAddressesAndPhonesDto>, LocationAddressesAndPhonesDto, Location>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLocationAddressesAndPhonesRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveLocationAddressesAndPhonesRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
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
        protected override bool ProcessSingleAggregate ( LocationAddressesAndPhonesDto dto, Location location )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<LocationAddressAndPhoneDto, Location, LocationAddressAndPhone> (
                    dto.LocationAddressesAndPhones, location, location.LocationAddressesAndPhones ).MapRemovedItem ( RemoveLocationAddress )
                    .MapAddedItem (
                        AddLocationAddress ).MapChangedItem ( ChangeLocationAddress ).Map ();

            return _mappingResult;
        }

        private static void RemoveLocationPhone (
            LocationPhoneDto locationAddressPhoneDto, LocationAddressAndPhone locationAddressAndPhone, LocationPhone locationPhone )
        {
            locationAddressAndPhone.RemovePhone ( locationPhone );
        }

        private void AddLocationAddress ( LocationAddressAndPhoneDto locationAddressAndPhoneDto, Location location )
        {
            var locationAddressType = _mappingHelper.MapLookupField<LocationAddressType> ( locationAddressAndPhoneDto.LocationAddressType );
            var stateProvince = _mappingHelper.MapLookupField<StateProvince> ( locationAddressAndPhoneDto.StateProvince );
            var countyArea = _mappingHelper.MapLookupField<CountyArea> ( locationAddressAndPhoneDto.CountyArea );
            var country = _mappingHelper.MapLookupField<Country> ( locationAddressAndPhoneDto.Country );

            var locationAddress =
                location.AddAddressAndPhone (
                    new LocationAddress (
                        locationAddressType,
                        new AddressBuilder ().WithFirstStreetAddress ( locationAddressAndPhoneDto.FirstStreetAddress ).WithSecondStreetAddress (
                            locationAddressAndPhoneDto.SecondStreetAddress ).WithCityName ( locationAddressAndPhoneDto.CityName ).WithCountyArea (
                                countyArea ).WithStateProvince ( stateProvince ).WithCountry ( country ).WithPostalCode (
                                new PostalCode ( locationAddressAndPhoneDto.PostalCode ) ),
                        locationAddressAndPhoneDto.ConfidentialIndicator ) );

            MapLocationPhone ( locationAddress, locationAddressAndPhoneDto );
        }

        private void AddLocationPhone ( LocationPhoneDto locationPhoneDto, LocationAddressAndPhone locationAddressAndPhone )
        {
            var locationAddressPhoneType = _mappingHelper.MapLookupField<LocationPhoneType> ( locationPhoneDto.LocationPhoneType );
            locationAddressAndPhone.AddPhone (
                new LocationPhoneBuilder ().WithLocationPhoneType ( locationAddressPhoneType ).WithPhone (
                    new PhoneBuilder ().WithPhoneNumber ( locationPhoneDto.PhoneNumber ).WithPhoneExtensionNumber (
                        locationPhoneDto.PhoneExtensionNumber ) ) );
        }

        private void ChangeLocationAddress (
            LocationAddressAndPhoneDto locationAddressDto, Location location, LocationAddressAndPhone locationAddressAndPhone )
        {
            var locationAddressType = _mappingHelper.MapLookupField<LocationAddressType> ( locationAddressDto.LocationAddressType );
            var stateProvince = _mappingHelper.MapLookupField<StateProvince> ( locationAddressDto.StateProvince );
            var countyArea = _mappingHelper.MapLookupField<CountyArea> ( locationAddressDto.CountyArea );
            var country = _mappingHelper.MapLookupField<Country> ( locationAddressDto.Country );

            locationAddressAndPhone.ReviseLocationAddress (
                new LocationAddress (
                    locationAddressType,
                    new AddressBuilder ().WithFirstStreetAddress ( locationAddressDto.FirstStreetAddress ).WithSecondStreetAddress (
                        locationAddressDto.SecondStreetAddress ).WithCityName ( locationAddressDto.CityName ).WithCountyArea ( countyArea )
                        .WithStateProvince ( stateProvince ).WithCountry ( country ).WithPostalCode ( new PostalCode ( locationAddressDto.PostalCode ) ),
                    locationAddressDto.ConfidentialIndicator ) );

            MapLocationPhone ( locationAddressAndPhone, locationAddressDto );
        }

        private void ChangeLocationPhone (
            LocationPhoneDto locationAddressPhoneDto, LocationAddressAndPhone locationAddressAndPhone, LocationPhone locationPhone )
        {
            RemoveLocationPhone ( locationAddressPhoneDto, locationAddressAndPhone, locationPhone );
            AddLocationPhone ( locationAddressPhoneDto, locationAddressAndPhone );
        }

        private void MapLocationPhone ( LocationAddressAndPhone locationAddressAndPhone, LocationAddressAndPhoneDto locationAddressDto )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<LocationPhoneDto, LocationAddressAndPhone, LocationPhone> (
                    locationAddressDto.PhoneNumbers, locationAddressAndPhone, locationAddressAndPhone.PhoneNumbers ).MapAddedItem ( AddLocationPhone )
                    .MapChangedItem ( ChangeLocationPhone ).MapRemovedItem ( RemoveLocationPhone ).Map ();
        }

        private void RemoveLocationAddress (
            LocationAddressAndPhoneDto locationAddressAndPhoneDto, Location location, LocationAddressAndPhone locationAddressAndPhone )
        {
            location.RemoveAddressAndPhone ( locationAddressAndPhone );
        }

        #endregion
    }
}
