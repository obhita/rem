#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
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
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// SelfPayment defines and records a payment.
    /// </summary>
    public class SelfPayment : AuditableAggregateRootBase, IPatientAccessAuditable
    {
        private readonly Staff _collectedByStaff;
        private readonly Patient _patient;
        private DateTime? _collectedDate;
        private Money _money;
        private PaymentMethod _paymentMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfPayment"/> class.
        /// </summary>
        protected internal SelfPayment ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfPayment"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="collectedByStaff">The collected by staff.</param>
        /// <param name="money">The money.</param>
        /// <param name="paymentMethod">The payment method.</param>
        /// <param name="collectedDate">The collected date.</param>
        protected internal SelfPayment ( Patient patient, Staff collectedByStaff, Money money, PaymentMethod paymentMethod, DateTime? collectedDate )
        {
            Check.IsNotNull ( patient, () => Patient );
            Check.IsNotNull ( collectedByStaff, () => CollectedByStaff );
            Check.IsNotNull ( money, () => Money );
            Check.IsNotNull ( paymentMethod, () => PaymentMethod );
            Check.IsNotNull ( collectedDate, () => CollectedDate );

            _patient = patient;
            _collectedByStaff = collectedByStaff;
            _money = money;
            _paymentMethod = paymentMethod;
            _collectedDate = collectedDate;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        [NotNull]
        public virtual Patient Patient
        {
            get { return _patient; }
            private set { }
        }

        /// <summary>
        /// Gets the collected by.
        /// </summary>
        [NotNull]
        public virtual Staff CollectedByStaff
        {
            get { return _collectedByStaff; }
            private set { }
        }

        /// <summary>
        /// Gets the collected date.
        /// </summary>
        public virtual DateTime? CollectedDate
        {
            get { return _collectedDate; }
            private set { ApplyPropertyChange ( ref _collectedDate, () => CollectedDate, value ); }
        }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        public virtual Money Money
        {
            get { return _money; }
            private set { ApplyPropertyChange ( ref _money, () => Money, value ); }
        }

        /// <summary>
        /// Gets the payment method.
        /// </summary>
        public virtual PaymentMethod PaymentMethod
        {
            get { return _paymentMethod; }
            private set { ApplyPropertyChange ( ref _paymentMethod, () => PaymentMethod, value ); }
        }

        /// <summary>
        /// Gets the audited patient.
        /// </summary>
        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }
    }
}
