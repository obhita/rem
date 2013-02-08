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
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Extension;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save problem dto request.
    /// </summary>
    public class SaveProblemDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<ProblemDto>, DtoResponse<ProblemDto>, ProblemDto, Problem>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly IProblemFactory _problemFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveProblemDtoRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="problemFactory">The problem factory.</param>
        public SaveProblemDtoRequestHandler (
            IDtoToDomainMappingHelper mappingHelper,
            IProblemFactory problemFactory )
        {
            _mappingHelper = mappingHelper;
            _problemFactory = problemFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.ClinicalCaseModule.Problem"/></returns>
        protected override Problem CreateNew ( ProblemDto dto )
        {
            var clinicalCase = Session.Load<ClinicalCase> ( dto.ClinicalCaseKey );

            CodedConcept problemCode = null;
            if ( dto.ProblemCodeCodedConcept != null )
            {
                problemCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.ProblemCodeCodedConcept );
            }

            var entity = _problemFactory.CreateProblem ( clinicalCase, problemCode );

            return entity;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( ProblemDto dto, Problem entity )
        {
            var mappingResult = MapProperties ( dto, entity );

            return mappingResult;
        }

        private bool MapProperties ( ProblemDto dto, Problem entity )
        {
            CodedConcept problemCode = null;
            if ( dto.ProblemCodeCodedConcept != null )
            {
                problemCode = new CodedConceptBuilder ().WithCodedConceptDto ( dto.ProblemCodeCodedConcept );
            }

            entity.ReviseProblemCode ( problemCode );

            var problemType = _mappingHelper.MapLookupField<ProblemType> ( dto.ProblemType );
            var problemStatus = _mappingHelper.MapLookupField<ProblemStatus> ( dto.ProblemStatus );
            var staff = Session.Load<Staff> ( dto.ObservedByStaff.Key );

            entity.ReviseProblemType ( problemType );
            entity.ReviseOnsetDateRange ( new DateRange ( dto.OnsetStartDate, dto.OnsetEndDate ) );

            entity.UpdateProblemStatus ( problemStatus, dto.StatusChangedDate );
            entity.ReviseObservationInfo ( staff, dto.ObservedDate );
            entity.ReviseCauseOfDeathIndicator ( dto.CauseOfDeathIndicator );

            return true;
        }

        #endregion
    }
}
