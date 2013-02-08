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

namespace Rem.Ria.AgencyModule.Web.StaffEditor
{
    /// <summary>
    /// Class for handling save staff address request.
    /// </summary>
    public class SaveStaffAddressRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<StaffAddressesDto>, DtoResponse<StaffAddressesDto>, StaffAddressesDto, Staff>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStaffAddressRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveStaffAddressRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="staff">The staff.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( StaffAddressesDto dto, Staff staff )
        {
            _mappingResult &= new AggregateNodeCollectionMapper<StaffAddressDto, Staff, StaffAddress> ( dto.Addresses, staff, staff.Addresses )
                .MapRemovedItem ( RemoveStaffAddress )
                .MapAddedItem ( AddStaffAddress )
                .MapChangedItem ( ChangeStaffAddress )
                .Map ();

            return _mappingResult;
        }

        private void AddStaffAddress ( StaffAddressDto staffAddressDto, Staff staff )
        {
            var addressType = _mappingHelper.MapLookupField<StaffAddressType> ( staffAddressDto.StaffAddressType );
            var countyAreaLookup = _mappingHelper.MapLookupField<CountyArea> ( staffAddressDto.CountyArea );
            var stateProvinceLookup = _mappingHelper.MapLookupField<StateProvince> ( staffAddressDto.StateProvince );

            var staffAddress =
                new StaffAddressBuilder ().WithStaffAddressType ( addressType ).WithAddress (
                    new AddressBuilder ().WithFirstStreetAddress ( staffAddressDto.FirstStreetAddress ).WithSecondStreetAddress (
                        staffAddressDto.SecondStreetAddress ).WithCityName ( staffAddressDto.CityName ).WithCountyArea ( countyAreaLookup )
                        .WithStateProvince ( stateProvinceLookup ).WithPostalCode ( new PostalCode ( staffAddressDto.PostalCode ) ) );

            staff.AddAddress ( staffAddress );
        }

        private void ChangeStaffAddress ( StaffAddressDto dto, Staff staff, StaffAddress staffAddress )
        {
            RemoveStaffAddress ( dto, staff, staffAddress );
            AddStaffAddress ( dto, staff );
        }

        private void RemoveStaffAddress ( StaffAddressDto dto, Staff staff, StaffAddress staffAddress )
        {
            staff.RemoveAddress ( staffAddress );
        }

        #endregion
    }
}
