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

using Pillar.Domain.Event;
using Rem.Domain.Clinical.SbirtModule;
using Rem.Domain.Clinical.SbirtModule.Event;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save nida drug questionnaire request.
    /// </summary>
    public class SaveNidaDrugQuestionnaireRequestHandler :
        SaveDtoRequestHandlerBase<SaveDtoRequest<NidaDrugQuestionnaireDto>, DtoResponse<NidaDrugQuestionnaireDto>, NidaDrugQuestionnaireDto>
    {
        #region Constants and Fields

        private bool _isDast10ResultChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Afters the dto refreshed.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        protected override void AfterDtoRefreshed ( NidaDrugQuestionnaireDto dto )
        {
            base.AfterDtoRefreshed ( dto );

            dto.IsDast10ResultChanged = _isDast10ResultChanged;
        }

        /// <summary>
        /// Processes the specified nida drug questionnaire dto.
        /// </summary>
        /// <param name="nidaDrugQuestionnaireDto">The nida drug questionnaire dto.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool Process ( NidaDrugQuestionnaireDto nidaDrugQuestionnaireDto )
        {
            // register for various 'created' events
            DomainEvent.Register<NidaDrugQuestionnaireIndicatorCalculatedEvent> ( e => _isDast10ResultChanged = e.IsDast10ResultChanged );

            var nidaDrugQuestionnaire = Session.Get<NidaDrugQuestionnaire> ( nidaDrugQuestionnaireDto.Key );

            nidaDrugQuestionnaire.ReviseAndCalculate (
                new NidaDrugQuestionnaireDrugTypeAndFrequencyOfUse (
                    nidaDrugQuestionnaireDto.CannabisUseAnswerNumber,
                    nidaDrugQuestionnaireDto.CocaineUseAnswerNumber,
                    nidaDrugQuestionnaireDto.OpioidsUseAnswerNumber,
                    nidaDrugQuestionnaireDto.MethamphetamineUseAnswerNumber,
                    nidaDrugQuestionnaireDto.SedativesUseAnswerNumber,
                    new NidaDrugQuestionnaireOtherDrugInfo (
                        nidaDrugQuestionnaireDto.OtherDrug1TypeName, nidaDrugQuestionnaireDto.OtherDrug1AnswerNumber ),
                    new NidaDrugQuestionnaireOtherDrugInfo (
                        nidaDrugQuestionnaireDto.OtherDrug2TypeName, nidaDrugQuestionnaireDto.OtherDrug2AnswerNumber ),
                    new NidaDrugQuestionnaireOtherDrugInfo (
                        nidaDrugQuestionnaireDto.OtherDrug3TypeName, nidaDrugQuestionnaireDto.OtherDrug3AnswerNumber ) ),
                new NidaDrugQuestionnaireInjectionDrugUse (
                    nidaDrugQuestionnaireDto.DrugUseByInjectionIndicator, nidaDrugQuestionnaireDto.LastDrugUseByInjectionAnswerNumber ) );

            return true;
        }

        #endregion
    }
}
