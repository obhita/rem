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

using Pillar.Common.Utility;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.EncounterModule
{
    /// <summary>
    /// The class defines a service entity.
    /// </summary>
    public class Service : AuditableAggregateRootBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        protected internal Service ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        /// <param name="diagnosis">The diagnosis.</param>
        /// <param name="medicalProcedure">The medical procedure.</param>
        /// <param name="primaryIndicator">If set to <c>true</c> [primary indicator].</param>
        /// <param name="trackingNumber">The tracking number.</param>
        protected internal Service(Encounter encounter, CodedConcept diagnosis, MedicalProcedure medicalProcedure, bool primaryIndicator, long trackingNumber)
        {
            Check.IsNotNull(encounter, "Encounter is required.");
            Check.IsNotNull(diagnosis, "Diagnosis is required.");
            Check.IsNotNull(medicalProcedure, "Medical procedure is required.");

            PrimaryIndicator = primaryIndicator;
            Encounter = encounter;
            Diagnosis = diagnosis;
            MedicalProcedure = medicalProcedure;
            TrackingNumber = trackingNumber;
        }

        #region Public Properties

        /// <summary>
        /// Gets the encounter.
        /// </summary>
        public virtual Encounter Encounter { get; private set; }

        /// <summary>
        /// Gets the medical procedure.
        /// </summary>
        public virtual MedicalProcedure MedicalProcedure { get; private set; }

        /// <summary>
        /// Gets the tracking number.
        /// </summary>
        public virtual long TrackingNumber { get; private set; }

        /// <summary>
        /// Gets the rate per billing unit.
        /// </summary>
        public virtual Money RatePerBillingUnit { get; private set; }

        /// <summary>
        /// Gets the diagnosis.
        /// </summary>
        public virtual CodedConcept Diagnosis { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [primary indicator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [primary indicator]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool PrimaryIndicator { get; private set; }

        /// <summary>
        /// Gets the charge amount.
        /// </summary>
        public virtual Money ChargeAmount { get; private set; } 

        /// <summary>
        /// Gets the billing unit count.
        /// </summary>
        public virtual UnitCount BillingUnitCount { get; private set; }

        #endregion

        /// <summary>
        /// Revises the encounter.
        /// </summary>
        /// <param name="encounter">The encounter.</param>
        public virtual void ReviseEncounter(Encounter encounter)
        {
            Check.IsNotNull(encounter, "Encounter is required.");
            Encounter = encounter;
        }

        /// <summary>
        /// Revises the medical procedure.
        /// </summary>
        /// <param name="medicalProcedure">The medical procedure.</param>
        public virtual void ReviseMedicalProcedure(MedicalProcedure medicalProcedure)
        {
            Check.IsNotNull(medicalProcedure, "Medical procedure is required.");
            MedicalProcedure = medicalProcedure;
        }

        /// <summary>
        /// Revises the diagnosis.
        /// </summary>
        /// <param name="diagnosis">The diagnosis.</param>
        public virtual void ReviseDiagnosis(CodedConcept diagnosis)
        {
            Check.IsNotNull(diagnosis, "Diagnosis is required.");
            Diagnosis = diagnosis;
        }

        /// <summary>
        /// Revises the primary indicator.
        /// </summary>
        /// <param name="primaryIndicator">If set to <c>true</c> [primary indicator].</param>
        public virtual void RevisePrimaryIndicator(bool primaryIndicator)
        {
            PrimaryIndicator = primaryIndicator;
        }

        /// <summary>
        /// Revises the tracking number.
        /// </summary>
        /// <param name="trackingNumber">The tracking number.</param>
        public virtual void ReviseTrackingNumber(long trackingNumber)
        {
            TrackingNumber = trackingNumber;
        }

        /// <summary>
        /// Revises the billing unit count.
        /// </summary>
        /// <param name="unitCount">The unit count.</param>
        public virtual void ReviseBillingUnitCount(UnitCount unitCount)
        {
            BillingUnitCount = unitCount;
        }

        /// <summary>
        /// Revises the charge amount.
        /// </summary>
        /// <param name="chargeAmount">The charge amount.</param>
        public virtual void ReviseChargeAmount(Money chargeAmount)
        {
            ChargeAmount = chargeAmount;
        }
    }
}
