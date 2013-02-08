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
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.SbirtModule.Event;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save social history dto request.
    /// </summary>
    public class SaveSocialHistoryDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<SocialHistoryDto>, DtoResponse<SocialHistoryDto>, SocialHistoryDto, SocialHistory>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private readonly ISocialHistoryFactory _socialHistoryFactory;
        private bool _isAuditCCreated;
        private bool _isDast10Created;
        private bool _isPhq9Created;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveSocialHistoryDtoRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        /// <param name="socialHistoryFactory">The social history factory.</param>
        public SaveSocialHistoryDtoRequestHandler (
            IDtoToDomainMappingHelper mappingHelper,
            ISocialHistoryFactory socialHistoryFactory )
        {
            _mappingHelper = mappingHelper;
            _socialHistoryFactory = socialHistoryFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Afters the dto refreshed.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        protected override void AfterDtoRefreshed ( SocialHistoryDto dto )
        {
            base.AfterDtoRefreshed ( dto );

            dto.IsPhq9Created = _isPhq9Created;
            dto.IsAuditCCreated = _isAuditCCreated;
            dto.IsDast10Created = _isDast10Created;
        }

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.VisitModule.SocialHistory"/></returns>
        protected override SocialHistory CreateNew ( SocialHistoryDto dto )
        {
            var visit = Session.Get<Visit> ( dto.VisitKey );
            var entity = _socialHistoryFactory.CreateSocialHistory ( visit );
            return entity;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="socialHistoryDto">The social history dto.</param>
        /// <param name="socialHistory">The social history.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( SocialHistoryDto socialHistoryDto, SocialHistory socialHistory )
        {
            // register for various 'created' events
            DomainEvent.Register<Phq9CreatedEvent> ( e => _isPhq9Created = true );
            DomainEvent.Register<AuditCCreatedEvent> ( e => _isAuditCCreated = true );
            DomainEvent.Register<Dast10CreatedEvent> ( e => _isDast10Created = true );

            var socialHistoryPhq2 = new SocialHistoryPhq2 (
                socialHistoryDto.Phq2LittleInterestInDoingThingsAnswerNumber,
                socialHistoryDto.Phq2FeelingDownAnswerNumber,
                socialHistoryDto.Phq2Score );
            socialHistory.ReviseSocialHistoryPhq2 ( socialHistoryPhq2 );

            var socialHistoryAuditC = new SocialHistoryAuditC (
                socialHistoryDto.AuditCDrinkBeerWineOrOtherAlcoholicBeveragesIndicator );
            socialHistory.ReviseSocialHistoryAuditC ( socialHistoryAuditC );

            var socialHistoryDast10 = new SocialHistoryDast10 (
                socialHistoryDto.Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber );
            socialHistory.ReviseSocialHistoryDast10 ( socialHistoryDast10 );

            var smokingStatus = _mappingHelper.MapLookupField<SmokingStatus> ( socialHistoryDto.SmokingStatus );
            var socialHistorySmoking = new SocialHistorySmoking (
                smokingStatus,
                socialHistoryDto.SmokingStatusAreYouWillingToQuitIndicator,
                socialHistoryDto.SmokingStatusAreYouWillingToQuitDate );
            socialHistory.ReviseSocialHistorySmoking ( socialHistorySmoking );

            return true;
        }

        #endregion
    }
}
