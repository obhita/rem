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
using Pillar.FluentRuleEngine;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Extension;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save patient document request.
    /// </summary>
    public class SavePatientDocumentRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<PatientDocumentDto>, DtoResponse<PatientDocumentDto>, PatientDocumentDto, PatientDocument>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly IPatientDocumentFactory _patientDocumentFactory;
        private readonly IPatientRepository _patientRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SavePatientDocumentRequestHandler"/> class.
        /// </summary>
        /// <param name="patientRepository">The patient repository.</param>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="patientDocumentFactory">The patient document factory.</param>
        public SavePatientDocumentRequestHandler (
            IPatientRepository patientRepository,
            IDtoToDomainMappingHelper mappingHelper,
            IPatientDocumentFactory patientDocumentFactory )
        {
            _patientRepository = patientRepository;
            _mappingHelper = mappingHelper;
            _patientDocumentFactory = patientDocumentFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.PatientModule.PatientDocument"/></returns>
        protected override PatientDocument CreateNew ( PatientDocumentDto dto )
        {
            var patient = _patientRepository.GetByKey ( dto.PatientKey );
            var patientDocumentType = _mappingHelper.MapLookupField<PatientDocumentType> ( dto.PatientDocumentType );
            var entity = _patientDocumentFactory.CreatePatientDocument ( patient, patientDocumentType, dto.Document, dto.FileName );
            return entity;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( PatientDocumentDto dto, PatientDocument entity )
        {
            var result = MapProperties ( dto, entity );

            //TODO: Why these rules are here?
            var ruleEngine = RuleEngine<PatientDocument>.CreateTypedRuleEngine ();
            var executionResults = ruleEngine.ExecuteRuleSet ( entity, "SavePatientDocument" );
            result &= !executionResults.HasRuleViolation;

            dto.MapViolations ( executionResults.RuleViolations );

            return result;
        }

        private bool MapProperties ( PatientDocumentDto dto, PatientDocument entity )
        {
            var patientDocumentType = _mappingHelper.MapLookupField<PatientDocumentType> ( dto.PatientDocumentType );
            var clinicalDateRange = new DateRange ( dto.ClinicalStartDate, dto.ClinicalEndDate );

            entity.RevisePatientDocumentType ( patientDocumentType );
            entity.ReviseClinicalDateRange ( clinicalDateRange );
            entity.ReviseDescription ( dto.Description );
            entity.ReviseDocumentProviderName ( dto.DocumentProviderName );
            entity.ReviseOtherDocumentTypeName ( dto.OtherDocumentTypeName );

            return true;
        }

        #endregion
    }
}
