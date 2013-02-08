using Pillar.Common.Utility;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.ClinicalBilling.ContextMap
{
    /// <summary>
    /// This class synchronizes procedure in clinical side to service in the billing side.
    /// </summary>
    public class ServiceSynchronizationService : IServiceSynchronizationService
    {
        private readonly IMedicalProcedureTranslator _medicalProcedureTranslator;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceFactory _serviceFactory;
        private readonly ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceSynchronizationService"/> class.
        /// </summary>
        /// <param name="medicalProcedureTranslator">The medical procedure translator.</param>
        /// <param name="serviceRepository">The service repository.</param>
        /// <param name="serviceFactory">The service factory.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public ServiceSynchronizationService (IMedicalProcedureTranslator medicalProcedureTranslator, IServiceRepository serviceRepository, IServiceFactory serviceFactory, ILookupValueRepository lookupValueRepository)
        {
            _medicalProcedureTranslator = medicalProcedureTranslator;
            _serviceRepository = serviceRepository;
            _serviceFactory = serviceFactory;
            _lookupValueRepository = lookupValueRepository;
        }

        /// <summary>
        /// Synchronizes the service.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        /// <param name="procedure">The procedure.</param>
        /// <param name="diagnosis">The diagnosis.</param>
        /// <returns>A service.</returns>
        public Service SynchronizeService(Encounter encounter, Procedure procedure, CodedConcept diagnosis)
        {
            Check.IsNotNull(encounter, "Encounter is required.");
            Check.IsNotNull(procedure, "Procedure is required.");
            Check.IsNotNull(diagnosis, "Diagnosis is required.");

            var medicalProcedure = _medicalProcedureTranslator.Translate ( procedure );

            var service = _serviceRepository.GetByTrackingNumber ( procedure.Key );

            var primaryIndicator = ( procedure.ProcedureType != ProcedureType.Activity );

            if (service == null)
            {
                service = _serviceFactory.CreateService ( encounter, diagnosis, medicalProcedure, primaryIndicator, procedure.Key );
            }
            else
            {
                if (encounter.Key != service.Encounter.Key)
                {
                    service.ReviseEncounter(encounter);
                }

                if (diagnosis != service.Diagnosis)
                {
                    service.ReviseDiagnosis(diagnosis);
                }

                if (medicalProcedure != service.MedicalProcedure)
                {
                    service.ReviseMedicalProcedure(medicalProcedure);
                }

                if (procedure.Key != service.TrackingNumber)
                {
                    service.ReviseTrackingNumber(procedure.Key);
                }

                if (primaryIndicator != service.PrimaryIndicator)
                {
                    service.RevisePrimaryIndicator(primaryIndicator);
                }

                if (procedure.BillingUnitCount != service.BillingUnitCount)
                {
                    service.ReviseBillingUnitCount(procedure.BillingUnitCount);
                }
            }

            //TODO: move them to ctor
            var currency = _lookupValueRepository.GetLookupByWellKnownName<Currency>(WellKnownNames.CommonModule.Currency.USDollars);
            service.ReviseChargeAmount(new Money(currency, 1));

            var c = new UnitCount ( 1 );
            service.ReviseBillingUnitCount ( c );
            return service;
        }
    }
}