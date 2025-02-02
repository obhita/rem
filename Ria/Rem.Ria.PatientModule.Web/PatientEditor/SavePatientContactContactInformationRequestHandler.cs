﻿#region License

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
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient contact contact information request.
    /// </summary>
    public class SavePatientContactContactInformationRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientContactContactInformationDto>, DtoResponse<PatientContactContactInformationDto>,
            PatientContactContactInformationDto, PatientContact>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientContactContactInformationRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientContactContactInformationRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.PatientModule.PatientContact"/></returns>
        protected override PatientContact CreateNew ( PatientContactContactInformationDto dto )
        {
            throw new InvalidOperationException ( "You must save Profile Section First." );
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="patientContact">The patient contact.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PatientContactContactInformationDto dto, PatientContact patientContact )
        {
            var result = MapProperties ( patientContact, dto );

            return _mappingResult & result;
        }

        private void MapPhoneProperties (
            PatientContactPhoneDto patientContactPhoneDto, PatientContact patientContact, PatientContactPhone patientContactPhone )
        {
            var result = new PropertyMapper<PatientContactPhone> ( patientContactPhone, patientContactPhoneDto )
                .MapProperty ( p => p.PhoneNumber, patientContactPhoneDto.PhoneNumber )
                .MapProperty ( p => p.PhoneExtensionNumber, patientContactPhoneDto.PhoneExtensionNumber )
                .MapProperty (
                    p => p.PatientContactPhoneType,
                    _mappingHelper.MapLookupField<PatientContactPhoneType> ( patientContactPhoneDto.PatientContactPhoneType ) )
                .MapProperty ( p => p.ConfidentialIndicator, patientContactPhoneDto.ConfidentialIndicator )
                .Map ();

            _mappingResult &= result;
        }

        private bool MapProperties ( PatientContact patientContact, PatientContactContactInformationDto patientContactDto )
        {
            var result = true;

            var phoneMapResult =
                new AggregateNodeCollectionMapper<PatientContactPhoneDto, PatientContact, PatientContactPhone> (
                    patientContactDto.PhoneNumbers, patientContact, patientContact.PhoneNumbers )
                    .MapAddedItem (
                        ( dto, entity ) =>
                        entity.AddContactPhone (
                            dto.PhoneNumber,
                            _mappingHelper.MapLookupField<PatientContactPhoneType> ( dto.PatientContactPhoneType ),
                            dto.PhoneExtensionNumber,
                            dto.ConfidentialIndicator ) )
                    .MapChangedItem ( MapPhoneProperties )
                    .MapRemovedItem ( ( dto, entity, node ) => entity.RemoveContactPhone ( node ) )
                    .Map ();

            result &= phoneMapResult;

            patientContact.ReviseCityName ( patientContactDto.CityName );
            patientContact.ReviseCountry ( _mappingHelper.MapLookupField<Country> ( patientContactDto.Country ) );
            patientContact.ReviseCountyArea ( _mappingHelper.MapLookupField<CountyArea> ( patientContactDto.CountyArea ) );
            patientContact.ReviseEmailAddress ( patientContactDto.EmailAddress );
            patientContact.ReviseFirstStreetAddress ( patientContactDto.FirstStreetAddress );
            patientContact.ReviseSecondStreetAddress ( patientContactDto.SecondStreetAddress );
            patientContact.RevisePostalCode ( patientContactDto.PostalCode );
            patientContact.ReviseStateProvince ( _mappingHelper.MapLookupField<StateProvince> ( patientContactDto.StateProvince ) );

            return result;
        }

        #endregion
    }
}
