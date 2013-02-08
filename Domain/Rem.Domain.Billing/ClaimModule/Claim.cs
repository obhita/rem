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
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Billing.EncounterModule;
using Rem.Domain.Billing.PatientAccountModule;
using Rem.Domain.Billing.PayorModule;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.ClaimModule
{
    /// <summary>
    ///   The Claim defines an entity that encapsulates the Health Care Claim information.
    /// </summary>
    public class Claim : AuditableAggregateRootBase
    {
        private readonly IList<ClaimLineItem> _claimLineItems;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class.
        /// </summary>
        protected internal Claim ()
        {
            _claimLineItems = new List<ClaimLineItem> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        /// <param name="payor">The payor.</param>
        /// <param name="chargeAmount">The charge amount.</param>
        /// <param name="patientAccount">The patient account.</param>
        /// <param name="placeOfService">The place of service.</param>
        /// <param name="serviceDate">The service date.</param>
        public Claim (Encounter encounter, Payor payor, Money chargeAmount, PatientAccount patientAccount, Location placeOfService, DateTime serviceDate )
            : this ()
        {
            Check.IsNotNull ( encounter, () => Encounter );
            Check.IsNotNull ( payor, () => Payor );
            Check.IsNotNull ( chargeAmount, () => ChargeAmount );
            Check.IsNotNull ( patientAccount, () => PatientAccount );
            Check.IsNotNull ( placeOfService, () => ServiceLocation );
            Check.IsNotNull ( serviceDate, () => ServiceDate );
            
            Encounter = encounter;
            Payor = payor;
            ChargeAmount = chargeAmount;
            PatientAccount = patientAccount;
            ServiceLocation = placeOfService;
            ServiceDate = serviceDate;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the encounter.
        /// </summary>
        [NotNull]
        public virtual Encounter Encounter { get; private set; }

        /// <summary>
        /// Gets the payor.
        /// </summary>
        [NotNull]
        public virtual Payor Payor { get; private set; }

        /// <summary>
        ///   Gets the charge amount.
        /// </summary>
        [NotNull]
        public virtual Money ChargeAmount { get; private set; }

        /// <summary>
        ///   Gets the claim batch.
        /// </summary>
        public virtual ClaimBatch ClaimBatch { get; private set; }

        /// <summary>
        ///   Gets the patient's account.
        /// </summary>
        [NotNull]
        public virtual PatientAccount PatientAccount { get; private set; }

        /// <summary>
        ///   Gets the place of service.
        /// </summary>
        [NotNull]
        public virtual Location ServiceLocation { get; private set; }

        /// <summary>
        /// Gets the service date.
        /// </summary>
        [NotNull]
        public virtual DateTime ServiceDate { get; private set; }

        /// <summary>
        /// Gets the claim line items.
        /// </summary>
        public virtual IEnumerable<ClaimLineItem> ClaimLineItems
        {
            get { return _claimLineItems; }
            private set { }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Revises the encounter.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        public virtual void ReviseEncounter( Encounter encounter)
        {
            Check.IsNotNull ( encounter, () => Encounter );
            Encounter = encounter;
        }

        /// <summary>
        /// Revises the payor.
        /// </summary>
        /// <param name="payor">The payor.</param>
        public virtual void RevisePayor(Payor payor)
        {
            Check.IsNotNull ( payor, () => Payor );
            Payor = payor;
        }

        /// <summary>
        ///   Revises the charge amount.
        /// </summary>
        /// <param name="chargeAmount"> The charge amount. </param>
        public virtual void ReviseChargeAmount(Money chargeAmount)
        {
            Check.IsNotNull ( chargeAmount, () => ChargeAmount );
            ChargeAmount = chargeAmount;
        }

        /// <summary>
        ///   Revises the claim batch.
        /// </summary>
        /// <param name="claimBatch"> The claim batch. </param>
        public virtual void ReviseClaimBatch(ClaimBatch claimBatch)
        {
            Check.IsNotNull ( claimBatch, () => ClaimBatch );
            ClaimBatch = claimBatch;
        }

        /// <summary>
        ///   Revises the patient account.
        /// </summary>
        /// <param name="patinetAccount"> The patient account. </param>
        public virtual void RevisePatientAccount(PatientAccount patinetAccount)
        {
            Check.IsNotNull ( patinetAccount, () => PatientAccount );
            PatientAccount = patinetAccount;
        }

        /// <summary>
        ///   Revises the place of service.
        /// </summary>
        /// <param name="placeOfService"> The place of service. </param>
        public virtual void RevisePlaceOfService(Location placeOfService)
        {
            Check.IsNotNull ( placeOfService, () => ServiceLocation );
            ServiceLocation = placeOfService;
        }

        /// <summary>
        ///   Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns> A <see cref="System.String" /> that represents this instance. </returns>
        public override string ToString ()
        {
            return string.Format ( "Claim for patient '{0}' with charge amount '{1}'", PatientAccount.Name, ChargeAmount );
        }


        /// <summary>
        /// Adds the claim line item.
        /// </summary>
        /// <param name="claimLineItem">The claim line item.</param>
        public virtual void AddClaimLineItem(ClaimLineItem claimLineItem)
        {
            Check.IsNotNull(claimLineItem, "Claim Line Item is required.");
            claimLineItem.ReviseClaim ( this );
            _claimLineItems.Add ( claimLineItem );
            NotifyItemAdded ( () => ClaimLineItems, claimLineItem );
        }

        /// <summary>
        /// Removes the claim line item.
        /// </summary>
        /// <param name="claimLineItem">The claim line item.</param>
        public virtual void RemoveClaimLineItem(ClaimLineItem claimLineItem)
        {
            Check.IsNotNull(claimLineItem, "Claim Line Item is required.");
            _claimLineItems.Remove ( claimLineItem );
            NotifyItemRemoved ( () => ClaimLineItems, claimLineItem );
        }

        
        /// <summary>
        /// Assigns the claim batch.
        /// </summary>
        /// <returns>A claim batch.</returns>
        public virtual ClaimBatch AssignClaimBatch()
        {
            var claimBatchFactory = IoC.CurrentContainer.Resolve<IClaimBatchFactory> ();
            var claimBatchRepository = IoC.CurrentContainer.Resolve<IClaimBatchRepository>();
            var lookupValueRepository = IoC.CurrentContainer.Resolve<ILookupValueRepository>();
           
            var claimBatch = claimBatchRepository.GetClaimBatch(lookupValueRepository.GetLookupByWellKnownName<ClaimBatchStatus>(WellKnownNames.ClaimModule.ClaimBatchStatus.Active), Payor.PrimaryPayorTypeMember.PayorType);

            if (claimBatch == null)
            {
                claimBatch = claimBatchFactory.CreateClaimBatch(ChargeAmount, Payor.PrimaryPayorTypeMember.PayorType);
            }
            else
            {
                claimBatch.ReviseChargeAmount(claimBatch.ChargeAmount + ChargeAmount);
            }

            ReviseClaimBatch(claimBatch);
            claimBatch.AddClaim ( this );

            return claimBatch;
        }
        #endregion
    }
}
