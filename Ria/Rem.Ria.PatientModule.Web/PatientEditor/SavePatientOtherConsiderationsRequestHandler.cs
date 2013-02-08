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

using System.Linq;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient other considerations request.
    /// </summary>
    public class SavePatientOtherConsiderationsRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientOtherConsiderationsDto>, DtoResponse<PatientOtherConsiderationsDto>, PatientOtherConsiderationsDto, Patient>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientOtherConsiderationsRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientOtherConsiderationsRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="patientOtherConsiderationsDto">The patient other considerations dto.</param>
        /// <param name="patient">The patient.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PatientOtherConsiderationsDto patientOtherConsiderationsDto, Patient patient )
        {
            var language = _mappingHelper.MapLookupField<Language> ( patientOtherConsiderationsDto.Language );
            var smokingStatus = _mappingHelper.MapLookupField<SmokingStatus> ( patientOtherConsiderationsDto.SmokingStatus );

            patient.ReviseLanguage ( new PatientLanguage ( language, patientOtherConsiderationsDto.InterpreterNeededIndicator ) );
            patient.ReviseSmokingStatus ( smokingStatus );
            patient.ReviseNotes ( patientOtherConsiderationsDto.Note );
            patient.RevisePaperRecord ( patientOtherConsiderationsDto.PaperFileIndicator );

            // Process Disabilities
            var patientDisabilityResult = new AggregateNodeLookupCollectionMapper<LookupValueDto, Patient, PatientDisability> (
                patientOtherConsiderationsDto.Disabilities,
                patient,
                patient.Disabilities )
                .MapAddedItem ( AddPatientDisability )
                .MapRemovedItem ( ( raceDto, localPatient, patientDisability ) => localPatient.RemovePatientDisabilility ( patientDisability ) )
                .FindCollectionEntity ( ( pr, key ) => patient.Disabilities.FirstOrDefault ( r => r.Disability.Key == key ) )
                .Map ();

            // Process Special Needs
            var patientSpecialNeedResult = new AggregateNodeLookupCollectionMapper<LookupValueDto, Patient, PatientSpecialNeed> (
                patientOtherConsiderationsDto.SpecialNeeds,
                patient,
                patient.SpecialNeeds )
                .MapAddedItem ( AddPatientSpecialNeed )
                .MapRemovedItem ( ( raceDto, localPatient, patientSpecialNeed ) => localPatient.RemovePatientSpecialNeed ( patientSpecialNeed ) )
                .FindCollectionEntity ( ( pr, key ) => patient.SpecialNeeds.FirstOrDefault ( r => r.SpecialNeed.Key == key ) )
                .Map ();

            return patientDisabilityResult && patientSpecialNeedResult;
        }

        private void AddPatientDisability ( LookupValueDto lookupValueDto, Patient patient )
        {
            var disability = _mappingHelper.MapLookupField<Disability> ( lookupValueDto );
            var patientDisability = new PatientDisability ( disability );
            patient.AddPatientDisability ( patientDisability );
        }

        private void AddPatientSpecialNeed ( LookupValueDto lookupValueDto, Patient patient )
        {
            var specialNeed = _mappingHelper.MapLookupField<SpecialNeed> ( lookupValueDto );
            var patientSpecialNeed = new PatientSpecialNeed ( specialNeed );
            patient.AddPatientSpecialNeed ( patientSpecialNeed );
        }

        #endregion
    }
}
