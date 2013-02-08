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

using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient aliases request.
    /// </summary>
    public class SavePatientAliasesRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientAliasesDto>, DtoResponse<PatientAliasesDto>, PatientAliasesDto, Patient>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientAliasesRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientAliasesRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
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
        protected override bool ProcessSingleAggregate ( PatientAliasesDto dto, Patient patient )
        {
            _mappingResult &= new AggregateNodeCollectionMapper<PatientAliasDto, Patient, PatientAlias> ( dto.Aliases, patient, patient.Aliases )
                .MapRemovedItem ( RemovePatientAlias )
                .MapAddedItem ( AddPatientAlias )
                .MapChangedItem ( ChangePatientAlias )
                .Map ();

            return _mappingResult;
        }

        private static void RemovePatientAlias ( PatientAliasDto patientAliasDto, Patient patient, PatientAlias patientAlias )
        {
            patient.RemoveAlias ( patientAlias );
        }

        private void AddPatientAlias ( PatientAliasDto patientAliasDto, Patient patient )
        {
            var patientAliasType = _mappingHelper.MapLookupField<PatientAliasType> ( patientAliasDto.PatientAliasType );

            var patientAlias = new PatientAlias (
                patientAliasType,
                patientAliasDto.FirstName,
                patientAliasDto.MiddleName,
                patientAliasDto.LastName );

            patient.AddAlias ( patientAlias );
        }

        private void ChangePatientAlias ( PatientAliasDto patientAliasDto, Patient patient, PatientAlias patientAlias )
        {
            RemovePatientAlias ( patientAliasDto, patient, patientAlias );
            AddPatientAlias ( patientAliasDto, patient );
        }

        #endregion
    }
}
