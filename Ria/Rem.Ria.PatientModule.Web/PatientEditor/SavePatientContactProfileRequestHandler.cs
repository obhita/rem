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
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient contact profile request.
    /// </summary>
    public class SavePatientContactProfileRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientContactProfileDto>, DtoResponse<PatientContactProfileDto>, PatientContactProfileDto, PatientContact>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientContactProfileRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientContactProfileRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
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
        protected override PatientContact CreateNew ( PatientContactProfileDto dto )
        {
            var patient = Session.Get<Patient> ( dto.PatientKey );
            if ( patient == null )
            {
                throw new InvalidOperationException ( "Patient does not exist" );
            }
            var patientContact = patient.AddContact ( dto.FirstName, dto.LastName );
            return patientContact;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="patientContact">The patient contact.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PatientContactProfileDto dto, PatientContact patientContact )
        {
            var result = MapProperties ( patientContact, dto );

            return _mappingResult & result;
        }

        private bool MapProperties ( PatientContact patientContact, PatientContactProfileDto patientContactDto )
        {
            patientContact.RenamePatientContact ( patientContactDto.FirstName, patientContactDto.MiddleName, patientContactDto.LastName );
            patientContact.ReviseCanContactIndicator ( patientContactDto.CanContactIndicator );
            patientContact.ReviseConsentExpirationDate ( patientContactDto.ConsentExpirationDate );
            patientContact.ReviseConsentOnFileIndicator ( patientContactDto.ConsentOnFileIndicator );
            patientContact.ReviseLegalAuthorizationType (
                _mappingHelper.MapLookupField<LegalAuthorizationType> ( patientContactDto.LegalAuthorizationType ) );
            patientContact.ReviseNote ( patientContactDto.Note );
            patientContact.RevisePrimaryIndicator ( patientContactDto.PrimaryIndicator );
            patientContact.RevisePatientContactRelationshipType ( _mappingHelper.MapLookupField<PatientContactRelationshipType> ( patientContactDto.PatientContactRelationshipType ) );
            patientContact.ReviseSocialSecurityNumber ( patientContactDto.SocialSecurityNumber );
            patientContact.ReviseEmergencyIndicator ( patientContactDto.EmergencyIndicator );
            patientContact.ReviseDesignatedFollowUpIndicator ( patientContactDto.DesignatedFollowUpIndicator );
            patientContact.ReviseGender ( _mappingHelper.MapLookupField<Gender> ( patientContactDto.Gender ) );
            patientContact.ReviseBirthDate ( patientContactDto.BirthDate );

            return true;
        }

        #endregion
    }
}
