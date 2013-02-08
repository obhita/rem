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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient profile request.
    /// </summary>
    public class SavePatientProfileRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientProfileDto>, DtoResponse<PatientProfileDto>, PatientProfileDto, Patient>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientProfileRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientProfileRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
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
        protected override bool ProcessSingleAggregate ( PatientProfileDto dto, Patient patient )
        {
            var name = new PersonNameBuilder ()
                .WithFirst ( dto.FirstName )
                .WithMiddle ( dto.MiddleName )
                .WithLast ( dto.LastName )
                .WithSuffix ( dto.SuffixName )
                .Build ();

            if ( patient.Name != name )
            {
                patient.Rename ( name );
            }

            var patientGender = ( dto.PatientGender == null ) ? null : _mappingHelper.MapLookupField<PatientGender> ( dto.PatientGender );
            var contactPreference = _mappingHelper.MapLookupField<ContactPreference> ( dto.ContactPreference );

            var patientProfile = new PatientProfileBuilder ()
                .WithPatientGender ( patientGender )
                .WithBirthDate ( dto.BirthDate )
                .WithDeathDate ( dto.DeathDate )
                .WithContactPreference ( contactPreference )
                .WithEmailAddress ( string.IsNullOrWhiteSpace ( dto.EmailAddress ) ? null : new EmailAddress ( dto.EmailAddress ) )
                .Build ();

            patient.ReviseProfile ( patientProfile );

            return true;
        }

        #endregion
    }
}