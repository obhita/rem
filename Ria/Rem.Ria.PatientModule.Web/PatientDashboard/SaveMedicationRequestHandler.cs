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
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Extension;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save medication request.
    /// </summary>
    public class SaveMedicationRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<MedicationDto>, DtoResponse<MedicationDto>, MedicationDto, Medication>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly IPatientRepository _patientRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveMedicationRequestHandler"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveMedicationRequestHandler ( IPatientRepository patientRepository, IDtoToDomainMappingHelper mappingHelper )
        {
            _patientRepository = patientRepository;
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.PatientModule.Medication"/></returns>
        protected override Medication CreateNew ( MedicationDto dto )
        {
            CodedConcept medicationCode = null;
            if ( dto.MedicationCodeCodedConcept != null )
            {
                medicationCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.MedicationCodeCodedConcept );
            }

            CodedConcept rootMedicationCode = null;
            if ( dto.RootMedicationCodedConcept != null )
            {
                rootMedicationCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.RootMedicationCodedConcept );
            }

            var patient = _patientRepository.GetByKey ( dto.PatientKey );
            var entity = patient.AddMedication ( medicationCode, rootMedicationCode );

            return entity;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( MedicationDto dto, Medication entity )
        {
            CodedConcept medicationCode = null;
            if ( dto.MedicationCodeCodedConcept != null )
            {
                medicationCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.MedicationCodeCodedConcept );
            }

            CodedConcept rootMedicationCode = null;
            if ( dto.RootMedicationCodedConcept != null )
            {
                rootMedicationCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.RootMedicationCodedConcept );
            }

            var discontinuedReason = _mappingHelper.MapLookupField<DiscontinuedReason> ( dto.DiscontinuedReason );
            var medicationStatus = _mappingHelper.MapLookupField<MedicationStatus> ( dto.MedicationStatus );

            entity.ReviseOverTheCounterIndicator ( dto.OverTheCounterIndicator );
            entity.RevisePrescribingPhysicianName ( dto.PrescribingPhysicianName );
            entity.ReviseUsageDateRange ( new DateRange ( dto.StartDate, dto.EndDate ) );
            entity.ReviseDiscontinuedByPhysicianName ( dto.DiscontinuedByPhysicianName );
            entity.ReviseDiscontinuedReason ( discontinuedReason );
            entity.ReviseDiscontinuedReasonOtherDescription ( dto.DiscontinuedReasonOtherDescription );
            entity.ReviseFrequencyDescription ( dto.FrequencyDescription );
            entity.ReviseInstructionsNote ( dto.InstructionsNote );
            entity.ReviseMedicationStatus ( medicationStatus );
            entity.ReviseMedicationCodeCodedConcept ( medicationCode );
            entity.ReviseRootMedicationCodedConcept ( rootMedicationCode );

            return true;
        }

        #endregion
    }
}
