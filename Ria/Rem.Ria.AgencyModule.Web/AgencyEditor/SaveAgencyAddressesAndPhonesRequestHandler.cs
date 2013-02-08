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
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.AgencyModule.Web.AgencyEditor
{
    /// <summary>
    /// Class for handling save agency addresses and phones request.
    /// </summary>
    public class SaveAgencyAddressesAndPhonesRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<AgencyAddressesAndPhonesDto>, DtoResponse<AgencyAddressesAndPhonesDto>, AgencyAddressesAndPhonesDto, Agency>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveAgencyAddressesAndPhonesRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveAgencyAddressesAndPhonesRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="agency">The agency.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( AgencyAddressesAndPhonesDto dto, Agency agency )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<AgencyAddressAndPhoneDto, Agency, AgencyAddressAndPhone> (
                    dto.AddressesAndPhones, agency, agency.AddressesAndPhones ).MapRemovedItem (
                        RemoveAgencyAddress ).MapAddedItem ( AddAgencyAddress ).MapChangedItem ( ChangeAgencyAddress ).Map ();

            return _mappingResult;
        }

        private static void RemoveAgencyPhone (
            AgencyPhoneDto agencyPhoneDto, AgencyAddressAndPhone agencyAddressAndPhone, AgencyPhone agencyPhone )
        {
            agencyAddressAndPhone.RemovePhone ( agencyPhone );
        }

        private void AddAgencyAddress ( AgencyAddressAndPhoneDto agencyAddressAndPhoneDto, Agency agency )
        {
            var agencyAddressType = _mappingHelper.MapLookupField<AgencyAddressType> ( agencyAddressAndPhoneDto.AgencyAddressType );
            var stateProvince = _mappingHelper.MapLookupField<StateProvince> ( agencyAddressAndPhoneDto.StateProvince );
            var countyArea = _mappingHelper.MapLookupField<CountyArea> ( agencyAddressAndPhoneDto.CountyArea );
            var country = _mappingHelper.MapLookupField<Country> ( agencyAddressAndPhoneDto.Country );

            var agencyAddress =
                agency.AddAddressAndPhone (
                    new AgencyAddress (
                        agencyAddressType,
                        new AddressBuilder ().WithFirstStreetAddress ( agencyAddressAndPhoneDto.FirstStreetAddress ).WithSecondStreetAddress (
                            agencyAddressAndPhoneDto.SecondStreetAddress ).WithCityName ( agencyAddressAndPhoneDto.CityName ).WithCountyArea (
                                countyArea ).WithStateProvince ( stateProvince )
                                .WithCountry ( country ).WithPostalCode ( new PostalCode ( agencyAddressAndPhoneDto.PostalCode ) ) ) );

            MapAgencyPhone ( agencyAddress, agencyAddressAndPhoneDto );
        }

        private void AddAgencyPhone ( AgencyPhoneDto agencyPhoneDto, AgencyAddressAndPhone agencyAddressAndPhone )
        {
            var agencyPhoneType = _mappingHelper.MapLookupField<AgencyPhoneType> ( agencyPhoneDto.AgencyPhoneType );
            agencyAddressAndPhone.AddPhone (
                new AgencyPhoneBuilder ().WithAgencyPhoneType ( agencyPhoneType ).WithPhone (
                    new PhoneBuilder ().WithPhoneNumber ( agencyPhoneDto.PhoneNumber ).WithPhoneExtensionNumber (
                        agencyPhoneDto.PhoneExtensionNumber ) ) );
        }

        private void ChangeAgencyAddress (
            AgencyAddressAndPhoneDto agencyAddressAndPhoneDto, Agency agency, AgencyAddressAndPhone agencyAddressAndPhone )
        {
            var agencyAddressType = _mappingHelper.MapLookupField<AgencyAddressType> ( agencyAddressAndPhoneDto.AgencyAddressType );
            var stateProvince = _mappingHelper.MapLookupField<StateProvince> ( agencyAddressAndPhoneDto.StateProvince );
            var countyArea = _mappingHelper.MapLookupField<CountyArea> ( agencyAddressAndPhoneDto.CountyArea );
            var country = _mappingHelper.MapLookupField<Country> ( agencyAddressAndPhoneDto.Country );

            agencyAddressAndPhone.ReviseAgencyAddress (
                new AgencyAddress (
                    agencyAddressType,
                    new AddressBuilder ().WithFirstStreetAddress ( agencyAddressAndPhoneDto.FirstStreetAddress ).WithSecondStreetAddress (
                        agencyAddressAndPhoneDto.SecondStreetAddress ).WithCityName ( agencyAddressAndPhoneDto.CityName ).WithCountyArea (
                            countyArea ).WithStateProvince ( stateProvince )
                        .WithCountry ( country ).WithPostalCode ( new PostalCode ( agencyAddressAndPhoneDto.PostalCode ) ) ) );

            MapAgencyPhone ( agencyAddressAndPhone, agencyAddressAndPhoneDto );
        }

        private void ChangeAgencyPhone (
            AgencyPhoneDto agencyPhoneDto, AgencyAddressAndPhone agencyAddressAndPhone, AgencyPhone agencyPhone )
        {
            RemoveAgencyPhone ( agencyPhoneDto, agencyAddressAndPhone, agencyPhone );
            AddAgencyPhone ( agencyPhoneDto, agencyAddressAndPhone );
        }

        private void MapAgencyPhone ( AgencyAddressAndPhone agencyAddressAndPhone, AgencyAddressAndPhoneDto agencyAddressAndPhoneDto )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<AgencyPhoneDto, AgencyAddressAndPhone, AgencyPhone> (
                    agencyAddressAndPhoneDto.PhoneNumbers, agencyAddressAndPhone, agencyAddressAndPhone.PhoneNumbers ).MapAddedItem ( AddAgencyPhone )
                    .MapChangedItem (
                        ChangeAgencyPhone ).MapRemovedItem ( RemoveAgencyPhone ).Map ();
        }

        private void RemoveAgencyAddress (
            AgencyAddressAndPhoneDto agencyAddressAndPhoneDto, Agency agency, AgencyAddressAndPhone agencyAddressAndPhone )
        {
            agency.RemoveAddress ( agencyAddressAndPhone );
        }

        #endregion
    }
}
