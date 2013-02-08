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

using System;
using System.Linq;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// The class defines a encounter import service.
    /// </summary>
    public class VisitImportService : IVisitImportService
    {
        #region Constants and Fields
        private readonly ICodingContextRepository _codingContextRepository;
        private readonly IEncounterSynchronizationService _encounterSynchronizationService;
        private readonly IServiceSynchronizationService _serviceSynchronizationService;
        private readonly IPatientAccountSynchronizationService _patientAccountSynchronizationService;
        private readonly IVisitRepository _visitRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitImportService"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        /// <param name="codingContextRepository">The coding context repository.</param>
        /// <param name="patientAccountSynchronizationService">The patient account synchronization service.</param>
        /// <param name="encounterSynchronizationService">The encounter synchronization service.</param>
        /// <param name="serviceSynchronizationService">The service synchronization service.</param>
        public VisitImportService (
            IVisitRepository visitRepository,
            /*IBillingOfficeRepository billingOfficeRepository,*/
            ICodingContextRepository codingContextRepository,
            IPatientAccountSynchronizationService patientAccountSynchronizationService,
            IEncounterSynchronizationService encounterSynchronizationService,
            IServiceSynchronizationService serviceSynchronizationService)
        {
            _visitRepository = visitRepository;
            /*_billingOfficeRepository = billingOfficeRepository;*/
            _codingContextRepository = codingContextRepository;
            _patientAccountSynchronizationService = patientAccountSynchronizationService;
            _encounterSynchronizationService = encounterSynchronizationService;
            _serviceSynchronizationService = serviceSynchronizationService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Imports a visit from in the clinical system.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        public virtual void ImportVisit ( long visitKey )
        {
            var visit = _visitRepository.GetByKey ( visitKey );
            var patient = visit.ClinicalCase.Patient;
            var agency = patient.Agency;

            var codingContext = _codingContextRepository.GetByVisitKey ( visitKey );
            if (codingContext == null)
            {
                throw new ApplicationException("Coding context does not exist.");
            }

            //TODO: needs move this check into rule collections.
            // More rules such as needing primary payor, procedure codes, admin staff 

            //var billingOffice = _billingOfficeRepository.GetByAgencyKey ( agency.Key );
            //if ( billingOffice == null )
            //{
            //    // If the billing office does not exist then we need to mark the coding context
            //    // with an error
            //    codingContext.ReportError ( Resource.BillingOfficeNotFoundCannotCreateEncounter );
            //}
            //else
            {
                var patientAccount = _patientAccountSynchronizationService.SynchronizePatientAccount ( patient );

                var encounter = _encounterSynchronizationService.SynchronizeEncounter ( patientAccount, visit );


                foreach ( var procedure in codingContext.Procedures )
                {
                    //TODO: Get the diagnosis correctly. For now we get the diagnosis from the first Problem.
                    var visitProblem = visit.Problems.First ();
                    var diagnosis = visitProblem.Problem.ProblemCodeCodedConcept;

                    _serviceSynchronizationService.SynchronizeService ( encounter, procedure, diagnosis );
                }

                var claim = encounter.GenerateClaim ();
                var claimBatch = claim.AssignClaimBatch ();

                //var healthCareClaim837Professional = claimBatch.GenerateHealthCareClaim837Professional ();
            }
        }

        #endregion
    }
}
