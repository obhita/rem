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
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.ClaimModule
{
    /// <summary>
    ///   The Claim defines an entity that encapsulates the Health Care Claim information per procedure.
    /// </summary>
    public class ClaimLineItem : AuditableAggregateNodeBase, IAggregateNodeValueObject
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="ClaimLineItem" /> class.
        /// </summary>
        protected internal ClaimLineItem ()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="ClaimLineItem" /> class.
        /// </summary>
        /// <param name="billingUnitCount"> The billing unit count. </param>
        /// <param name="chargeAmount"> The charge amount. </param>
        /// <param name="diagnosis"> The diagnosis. </param>
        /// <param name="procedure"> The procedure. </param>
        public ClaimLineItem ( UnitCount billingUnitCount, Money chargeAmount, CodedConcept diagnosis, CodedConcept procedure )
        {
            Check.IsNotNull ( chargeAmount, () => ChargeAmount );
            Check.IsNotNull ( diagnosis, () => Diagnosis );
            Check.IsNotNull ( procedure, () => Procedure );

            BillingUnitCount = billingUnitCount;
            ChargeAmount = chargeAmount;
            Diagnosis = diagnosis;
            Procedure = procedure;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return Claim; }
        }

        /// <summary>
        ///   Gets the billing unit count.
        /// </summary>
        [NotNull]
        public virtual UnitCount BillingUnitCount { get; private set; }

        /// <summary>
        ///   Gets the charge amount.
        /// </summary>
        [NotNull]
        public virtual Money ChargeAmount { get; private set; }

        /// <summary>
        ///   Gets the claim.
        /// </summary>
        /// <value> The claim. </value>
        [NotNull]
        public virtual Claim Claim { get; private set; }

        /// <summary>
        ///   Gets the diagnosis.
        /// </summary>
        [NotNull]
        public virtual CodedConcept Diagnosis { get; private set; }

        /// <summary>
        ///   Gets the procedure.
        /// </summary>
        [NotNull]
        public virtual CodedConcept Procedure { get; private set; }

        /// <summary>
        ///   Gets the rate per billing unit.
        /// </summary>
        public virtual Money RatePerBillingUnit { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Revises the billing unit count.
        /// </summary>
        /// <param name="billingUnitCount"> The billing unit count. </param>
        public virtual void ReviseBillingUnitCount(UnitCount billingUnitCount)
        {
            BillingUnitCount = billingUnitCount;
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
        ///   Revises the diagnosis.
        /// </summary>
        /// <param name="diagnosis"> The diagnosis. </param>
        public virtual void ReviseDiagnosis(CodedConcept diagnosis)
        {
            Check.IsNotNull ( diagnosis, () => Diagnosis );
            Diagnosis = diagnosis;
        }

        /// <summary>
        ///   Revises the procedure.
        /// </summary>
        /// <param name="procedure"> The procedure. </param>
        public virtual void ReviseProcedure(CodedConcept procedure)
        {
            Check.IsNotNull ( procedure, () => Procedure );
            Procedure = procedure;
        }

        /// <summary>
        ///   Revises the rate per billing unit.
        /// </summary>
        /// <param name="ratePerBillingUnit"> The rate per billing unit. </param>
        public virtual void ReviseRatePerBillingUnit(Money ratePerBillingUnit)
        {
            RatePerBillingUnit = ratePerBillingUnit;
        }

        /// <summary>
        /// Revises the claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        public virtual void ReviseClaim(Claim claim)
        {
            Check.IsNotNull(claim, () => Claim);
            Claim = claim;
        }

        /// <summary>
        ///   Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns> A <see cref="System.String" /> that represents this instance. </returns>
        public override string ToString ()
        {
            return string.Format (
                "Claim Line Item for '{0}' procedure and '{1}' diagnostics , with charge amount '{2}'", Procedure, Diagnosis, ChargeAmount );
        }

        #endregion
    }
}
