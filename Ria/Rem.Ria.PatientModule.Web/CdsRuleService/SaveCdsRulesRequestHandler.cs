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
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Extension;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.CdsRuleService
{
    /// <summary>
    /// Class for handling save CDS rules request.
    /// </summary>
    public class SaveCdsRulesRequestHandler :
        SaveDtoRequestHandlerBase<SaveDtoRequest<CdsRulesDto>, DtoResponse<CdsRulesDto>, CdsRulesDto>
    {
        #region Constants and Fields

        private readonly ICdsRuleFactory _cdsRuleFactory;
        private readonly IDtoToDomainMappingHelper _dtoToDomainMappingHelper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveCdsRulesRequestHandler"/> class.
        /// </summary>
        /// <param name="cdsRuleFactory">The CDS rule factory.</param>
        /// <param name="dtoToDomainMappingHelper">The dto to domain mapping helper.</param>
        public SaveCdsRulesRequestHandler (
            ICdsRuleFactory cdsRuleFactory,
            IDtoToDomainMappingHelper dtoToDomainMappingHelper )
        {
            _cdsRuleFactory = cdsRuleFactory;
            _dtoToDomainMappingHelper = dtoToDomainMappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the specified CDS rules dto.
        /// </summary>
        /// <param name="cdsRulesDto">The CDS rules dto.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool Process ( CdsRulesDto cdsRulesDto )
        {
            var processSuccessful = true;

            foreach ( var cdsRuleDto in cdsRulesDto.CdsRules.CurrentItems )
            {
                if ( cdsRuleDto.EditStatus == EditStatus.Noop )
                {
                    continue;
                }

                var cdsRule = cdsRuleDto.EditStatus == EditStatus.Create
                                      ? _cdsRuleFactory.CreateCdsRule ()
                                      : Session.Get<CdsRule> ( cdsRuleDto.Key );
                var ret = MapProperties ( cdsRule, cdsRuleDto );
                processSuccessful &= ret;
            }
            foreach ( var cdsRuleDto in cdsRulesDto.CdsRules.RemovedItems )
            {
                var cdsRule = Session.Get<CdsRule> ( cdsRuleDto.Key );
                _cdsRuleFactory.DestroyCdsRule ( cdsRule );
            }
            return processSuccessful;
        }

        private bool MapProperties ( CdsRule cdsRule, CdsRuleDto cdsRuleDto )
        {
            CodedConcept medicationCodedConcept = null;
            if ( cdsRuleDto.MedicationCodedConcept != null )
            {
                medicationCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( cdsRuleDto.MedicationCodedConcept );
            }

            CodedConcept problemCodedConcept = null;
            if ( cdsRuleDto.ProblemCodedConcept != null )
            {
                problemCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( cdsRuleDto.ProblemCodedConcept.ProblemCodeCodedConcept );
            }

            var labTestName = _dtoToDomainMappingHelper.MapLookupField<LabTestName> ( cdsRuleDto.LabTestName );

            cdsRule.Rename ( cdsRuleDto.Name );
            cdsRule.ReviseRecommendationNote ( cdsRuleDto.RecommendationNote );
            cdsRule.ReviseMedicationCodedConcept ( medicationCodedConcept );
            cdsRule.ReviseProblemCodedConcept ( problemCodedConcept );
            cdsRule.ReviseLabTestName ( labTestName );
            cdsRule.ReviseAge ( cdsRuleDto.Age );
            cdsRule.ReviseValidLabOrderMonthCount ( cdsRuleDto.ValidLabOrderMonthCount );

            return true;
        }

        #endregion
    }
}
