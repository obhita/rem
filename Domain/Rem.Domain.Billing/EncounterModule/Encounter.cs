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
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Billing.ClaimModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.EncounterModule
{
    /// <summary>
    /// The class defines an encounter entity.
    /// </summary>
    public class Encounter : AuditableAggregateRootBase
    {
        private readonly IList<Service> _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="Encounter"/> class.
        /// </summary>
        protected Encounter ()
        {
            _services = new List<Service> ();
        }

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Encounter"/> class.
        /// </summary>
        /// <param name="patientAccount">The patient account.</param>
        /// <param name="serviceLocation">The service location.</param>
        /// <param name="serviceProviderStaff">The service provider staff.</param>
        /// <param name="trackingNumber">The tracking number.</param>
        /// <param name="serviceDate">The service date.</param>
        protected internal Encounter (
            PatientAccount patientAccount,
            Location serviceLocation,
            Staff serviceProviderStaff,
            long trackingNumber,
            DateTime serviceDate)
            : this ()
        {
            Check.IsNotNull ( patientAccount, "Patient account is required." );
            Check.IsNotNull ( serviceLocation, "Place of service is required." );
            Check.IsNotNull ( serviceProviderStaff, "Service provider staff is required." );
            Check.IsNotNull ( serviceDate, "Service date staff is required." );

            PatientAccount = patientAccount;
            ServiceLocation = serviceLocation;
            ServiceProviderStaff = serviceProviderStaff;
            TrackingNumber = trackingNumber;
            ServiceDate = serviceDate;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the patient account.
        /// </summary>
        [NotNull]
        public virtual PatientAccount PatientAccount { get; private set; }

        /// <summary>
        /// Gets the service location.
        /// </summary>
        [NotNull]
        public virtual Location ServiceLocation { get; private set; }

        /// <summary>
        /// Gets the service provider staff.
        /// </summary>
        [NotNull]
        public virtual Staff ServiceProviderStaff { get; private set; }

        /// <summary>
        /// Gets the service date.
        /// </summary>
        [NotNull]
        public virtual DateTime ServiceDate { get; private set; }

        /// <summary>
        /// Gets the charge amount.
        /// </summary>
        public virtual Money ChargeAmount { get; private set; }

        /// <summary>
        /// Gets the services.
        /// </summary>
        public virtual IEnumerable<Service> Services
        {
            get { return _services; }
            private set { }
        }

        /// <summary>
        /// Gets the tracking number.
        /// </summary>
        [NotNull]
        [NaturalIndex]
        public virtual long TrackingNumber { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Revises the patient account.
        /// </summary>
        /// <param name="patientAccount">The patient account.</param>
        public virtual void RevisePatientAccount(PatientAccount patientAccount)
        {
            Check.IsNotNull(patientAccount, "Patient account is required.");
            PatientAccount = patientAccount;
        }

        /// <summary>
        /// Revises the place of service.
        /// </summary>
        /// <param name="placeOfService">The place of service.</param>
        public virtual void RevisePlaceOfService ( Location placeOfService )
        {
            Check.IsNotNull ( placeOfService, "Place of service is required." );
            ServiceLocation = placeOfService;
        }

        /// <summary>
        /// Revises the service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public virtual void ReviseServiceProvider ( Staff serviceProvider )
        {
            Check.IsNotNull ( serviceProvider, "Service Provider is required." );
            ServiceProviderStaff = serviceProvider;
        }

        /// <summary>
        /// Revises the service date.
        /// </summary>
        /// <param name="serviceDate">The service date.</param>
        public virtual void ReviseServiceDate(DateTime serviceDate)
        {
            Check.IsNotNull(serviceDate, "Service Date is required.");
            ServiceDate = serviceDate;
        }

        /// <summary>
        /// Revises the charge amount.
        /// </summary>
        /// <param name="chargeAmount">The charge amount.</param>
        public virtual void ReviseChargeAmount(Money chargeAmount)
        {
            Check.IsNotNull(chargeAmount, "Charge Amount is required.");
            ChargeAmount = chargeAmount;
        }

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="service">The service.</param>
        public virtual void AddService(Service service)
        {
            Check.IsNotNull(service, "Service is required.");
            service.ReviseEncounter(this);
            _services.Add ( service );
            NotifyItemAdded(() => Services, service);
        }

        /// <summary>
        /// Removes the service.
        /// </summary>
        /// <param name="service">The service.</param>
        public virtual void RemoveService(Service service)
        {
            Check.IsNotNull(service, "Service is required.");
            _services.Remove(service);
            NotifyItemRemoved(() => Services, service);
        }

        /// <summary>
        /// Generates the claim.
        /// </summary>
        /// <returns>A claim object.</returns>
        public virtual Claim GenerateClaim()
        {
            var claimFactory = IoC.CurrentContainer.Resolve<IClaimFactory> ();

            var patientAccount = PatientAccount;
            var payor = (from payorCoverage in patientAccount.PayorCoverages
                         where payorCoverage.PayorCoverageType.WellKnownName == WellKnownNames.PatientAccountModule.PayorCoverageType.Primary &&
                         (payorCoverage.EffectiveCoverageDateRange.StartDate <= ServiceDate && payorCoverage.EffectiveCoverageDateRange.EndDate >= ServiceDate) //TODO: make sure the endDate is not null
                         select payorCoverage.Payor).FirstOrDefault();

            if (payor == null)
            {
                throw new InvalidOperationException(string.Format("{0} does not have primary payor coverage.", patientAccount.Name));
            }

            // Only get the active primary payor coverage in the  1st round
            // TODO: implement claims for 2nd or 3rd payor coverage
            var claim = claimFactory.CreateClaim(
                this, payor, ChargeAmount, PatientAccount, ServiceLocation, ServiceDate);

            //Create Claim line item
            foreach (var service in Services)
            {
                var claimLineItem = new ClaimLineItem(
                    service.BillingUnitCount, service.ChargeAmount, service.Diagnosis, service.MedicalProcedure.ProcedureCode);

                claim.AddClaimLineItem(claimLineItem);
            }

            return claim;
        }
        #endregion
    }
}
