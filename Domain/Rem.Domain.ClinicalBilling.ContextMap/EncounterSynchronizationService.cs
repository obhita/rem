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
using Pillar.Common.Utility;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// Class that defines an encounter synchronization service.
    /// </summary>
    public class EncounterSynchronizationService : IEncounterSynchronizationService
    {
        #region Constants and Fields

        private readonly IEncounterFactory _encounterFactory;
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly IEncounterRepository _encounterRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EncounterSynchronizationService"/> class.
        /// </summary>
        /// <param name="encounterRepository">The encounter repository.</param>
        /// <param name="encounterFactory">The encounter factory.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public EncounterSynchronizationService (
            IEncounterRepository encounterRepository,
            IEncounterFactory encounterFactory,
            ILookupValueRepository lookupValueRepository)
        {
            _encounterRepository = encounterRepository;
            _encounterFactory = encounterFactory;
            _lookupValueRepository = lookupValueRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Synchronizes the encounter with the clinical visit.
        /// </summary>
        /// <param name="patientAccount">The patient account.</param>
        /// <param name="visit">The visit.</param>
        /// <returns>
        /// An <see cref="Encounter"/> instance.
        /// </returns>
        public Encounter SynchronizeEncounter ( PatientAccount patientAccount, Visit visit )
        {
            Check.IsNotNull ( patientAccount, "Patient account is required." );
            Check.IsNotNull ( visit, "Visit is required." );

            var serviceProvider = visit.Staff;
            var placeOfService = visit.ServiceLocation;

            var encounter = _encounterRepository.GetByTrackingNumber ( visit.Key );
            if (encounter == null)
            {
                encounter = _encounterFactory.CreateEncounter ( 
                    patientAccount,
                    placeOfService,
                    serviceProvider,
                    visit.Key,
                    visit.CheckedInDateTime?? DateTime.Now);
            }
            else
            {
                if (encounter.PatientAccount.Key != patientAccount.Key)
                {
                    encounter.RevisePatientAccount(patientAccount );
                }

                if (encounter.ServiceLocation.Key != placeOfService.Key)
                {
                    encounter.RevisePlaceOfService(placeOfService);
                }

                if (encounter.ServiceProviderStaff.Key != serviceProvider.Key)
                {
                    encounter.ReviseServiceProvider ( serviceProvider );
                }
            }

            //TODO: move them to ctor
            var currency = _lookupValueRepository.GetLookupByWellKnownName<Currency>(WellKnownNames.CommonModule.Currency.USDollars);
            encounter.ReviseChargeAmount ( new Money ( currency, 2 ) );

            return encounter;
        }

        #endregion
    }
}
