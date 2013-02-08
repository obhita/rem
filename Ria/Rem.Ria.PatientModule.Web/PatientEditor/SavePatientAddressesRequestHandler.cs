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

using System;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient addresses request.
    /// </summary>
    public class SavePatientAddressesRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientAddressesDto>, DtoResponse<PatientAddressesDto>, PatientAddressesDto, Patient>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientAddressesRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientAddressesRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="patient">The patient.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PatientAddressesDto dto, Patient patient )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<PatientAddressDto, Patient, PatientAddress> ( dto.Addresses, patient, patient.Addresses )
                    .MapRemovedItem ( RemovePatientAddress )
                    .MapAddedItem ( AddPatientAddress )
                    .MapChangedItem ( ChangePatientAddress )
                    .Map ();

            return _mappingResult;
        }

        private static void RemovePatientAddress ( PatientAddressDto patientAddressDto, Patient patient, PatientAddress patientAddress )
        {
            patient.RemoveAddress ( patientAddress );
        }

        private void AddPatientAddress ( PatientAddressDto patientAddressDto, Patient patient )
        {
            var addressType = _mappingHelper.MapLookupField<PatientAddressType> ( patientAddressDto.PatientAddressType );
            var countyAreaLookup = _mappingHelper.MapLookupField<CountyArea> ( patientAddressDto.CountyArea );
            var stateProvinceLookup = _mappingHelper.MapLookupField<StateProvince> ( patientAddressDto.StateProvince );
            var countryLookup = _mappingHelper.MapLookupField<Country> ( patientAddressDto.Country );

            var address = new AddressBuilder ()
                .WithFirstStreetAddress ( patientAddressDto.FirstStreetAddress )
                .WithSecondStreetAddress ( patientAddressDto.SecondStreetAddress )
                .WithCityName ( patientAddressDto.CityName )
                .WithCountyArea ( countyAreaLookup )
                .WithStateProvince ( stateProvinceLookup )
                .WithCountry ( countryLookup )
                .WithPostalCode (
                    string.IsNullOrWhiteSpace ( patientAddressDto.PostalCode ) ? null : new PostalCode ( patientAddressDto.PostalCode ) )
                .Build ();

            var patientAddress = new PatientAddressBuilder ()
                .WithPatientAddressType ( addressType )
                .WithAddress(address)
                .WithConfidentialIndicator ( patientAddressDto.ConfidentialIndicator )
                .WithYearsOfStayNumber ( patientAddressDto.YearsOfStayNumber )
                .Build ();

            patient.AddAddress ( patientAddress );
        }

        private void ChangePatientAddress ( PatientAddressDto patientAddressDto, Patient patient, PatientAddress patientAddress )
        {
            RemovePatientAddress ( patientAddressDto, patient, patientAddress );
            AddPatientAddress ( patientAddressDto, patient );
        }

        #endregion
    }
}
