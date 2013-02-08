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
    /// Class for handling save dast10 request.
    /// </summary>
    public class SaveDast10RequestHandler : SaveDtoRequestHandlerBase<SaveDtoRequest<Dast10Dto>, DtoResponse<Dast10Dto>, Dast10Dto>
    {
        #region Constants and Fields

        private bool _isNidaDrugQuestionnaireCreated;

        #endregion

        #region Methods

        /// <summary>
        /// Afters the dto refreshed.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        protected override void AfterDtoRefreshed ( Dast10Dto dto )
        {
            base.AfterDtoRefreshed ( dto );

            dto.IsNidaDrugQuestionnaireCreated = _isNidaDrugQuestionnaireCreated;
        }

        /// <summary>
        /// Processes the specified dast10 dto.
        /// </summary>
        /// <param name="dast10Dto">The dast10 dto.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool Process ( Dast10Dto dast10Dto )
        {
            // register for various 'created' events
            DomainEvent.Register<NidaDrugQuestionnaireCreatedEvent> ( e => _isNidaDrugQuestionnaireCreated = true );

            var dast10 = Session.Get<Dast10> ( dast10Dto.Key );

            dast10.ReviseAndCalculate (
                dast10Dto.HaveYouUsedDrugsIndicator,
                dast10Dto.HaveYouEngagedInIllegalActivitiesIndicator,
                dast10Dto.HaveYouEverExperiencedWithdrawalSymptomsIndicator,
                dast10Dto.HaveYouHadBlackoutsOrFlashbacksIndicator,
                dast10Dto.HaveYouHadMedicalProblemsIndicator,
                dast10Dto.HaveYouNeglectedYourFamilyIndicator,
                dast10Dto.AreYouAbleToStopUsingDrugsIndicator,
                dast10Dto.DoYouAbuseMoreThanOneDrugIndicator,
                dast10Dto.DoYouFeelBadOrGuiltyIndicator,
                dast10Dto.DoesYourSpouseOrParentComplainIndicator );

            return true;
        }

        #endregion
    }
}
