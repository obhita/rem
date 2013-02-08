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

using Agatha.Common;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.ClinicalBilling.ContextMap;
using Rem.Infrastructure.Service;

namespace Rem.Ria.BillingModule.Web.Service
{
    /// <summary>
    /// Class for handling send to billing request.
    /// </summary>
    public class SendToBillingRequestHandler :
        NHibernateSessionRequestHandler<SendToBillingRequest, SendToBillingResponse>
    {
        #region Constants and Fields

        private readonly ICodingContextRepository _codingContextRepository;
        private readonly IEncounterSynchronizationService _encounterSynchronizationService;
        private readonly IPatientAccountSynchronizationService _patientAccountSynchronizationService;
        private readonly IServiceSynchronizationService _serviceSynchronizationService;
        private readonly IVisitRepository _visitRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SendToBillingRequestHandler"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        /// <param name="codingContextRepository">The coding context repository.</param>
        /// <param name="patientAccountSynchronizationService">The patient account synchronization service.</param>
        /// <param name="encounterSynchronizationService">The encounter synchronization service.</param>
        /// <param name="serviceSynchronizationService">The service synchronization service.</param>
        public SendToBillingRequestHandler (
            IVisitRepository visitRepository,
            ICodingContextRepository codingContextRepository,
            IPatientAccountSynchronizationService patientAccountSynchronizationService,
            IEncounterSynchronizationService encounterSynchronizationService,
            IServiceSynchronizationService serviceSynchronizationService
            )
        {
            _visitRepository = visitRepository;
            _codingContextRepository = codingContextRepository;
            _patientAccountSynchronizationService = patientAccountSynchronizationService;
            _encounterSynchronizationService = encounterSynchronizationService;
            _serviceSynchronizationService = serviceSynchronizationService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( SendToBillingRequest request )
        {
            var response = CreateTypedResponse ();

            //TODO: after the demo, remove the reference to Domain.Clinical since this will be moved to Rem.Ria.PatientModule.Web.FrontDeskDashboard.SendVisitToBillingRequestHandler
            if ( request.VisBus )
            {
                var codingConext = _codingContextRepository.GetByVisitKey ( request.VisitKey );
                codingConext.ReviewVisit ();
            }
            else
            {
                var visitImportService = new VisitImportService (
                    _visitRepository,
                    _codingContextRepository,
                    _patientAccountSynchronizationService,
                    _encounterSynchronizationService,
                    _serviceSynchronizationService );

                visitImportService.ImportVisit ( request.VisitKey );
            }

            return response;
        }

        #endregion
    }
}
