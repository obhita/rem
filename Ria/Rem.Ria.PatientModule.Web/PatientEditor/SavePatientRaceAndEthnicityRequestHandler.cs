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
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Class for handling save patient race and ethnicity request.
    /// </summary>
    public class SavePatientRaceAndEthnicityRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientRaceAndEthnicityDto>, DtoResponse<PatientRaceAndEthnicityDto>, PatientRaceAndEthnicityDto, Patient>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientRaceAndEthnicityRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SavePatientRaceAndEthnicityRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="patientRaceAndEthnicityDto">The patient race and ethnicity dto.</param>
        /// <param name="patient">The patient.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PatientRaceAndEthnicityDto patientRaceAndEthnicityDto, Patient patient )
        {
            var patientRaceResult = new AggregateNodeLookupCollectionMapper<LookupValueDto, Patient, PatientRace> (
                patientRaceAndEthnicityDto.Races,
                patient,
                patient.Races )
                .MapAddedItem ( AddPatientRace )
                .MapRemovedItem ( ( raceDto, localPatient, patientRace ) => localPatient.RemovePatientRace ( patientRace ) )
                .FindCollectionEntity ( ( pr, key ) => patient.Races.FirstOrDefault ( r => r.Race.Key == key ) )
                .Map ();

            var race = _mappingHelper.MapLookupField<Race> ( patientRaceAndEthnicityDto.PrimaryRace );
            patient.SetPrimaryRace ( race );

            var ethnicity = _mappingHelper.MapLookupField<Ethnicity> ( patientRaceAndEthnicityDto.Ethnicity );
            var detailedEthnicity = _mappingHelper.MapLookupField<DetailedEthnicity> ( patientRaceAndEthnicityDto.DetailedEthnicity );
            patient.ReviseEthnicity ( new PatientEthnicity ( ethnicity, detailedEthnicity ) );

            return patientRaceResult;
        }

        private void AddPatientRace ( LookupValueDto raceDto, Patient patient )
        {
            var race = _mappingHelper.MapLookupField<Race> ( raceDto );
            var patientRace = new PatientRace ( race );
            patient.AddPatientRace ( patientRace );
        }

        #endregion
    }
}
