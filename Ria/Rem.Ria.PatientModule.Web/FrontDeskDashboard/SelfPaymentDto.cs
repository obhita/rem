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
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.WellKnownNames.CommonModule;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Data transfer object for SelfPayment class.
    /// </summary>
    public class SelfPaymentDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private decimal _amount;

        private string _cultureName;

        private string _currencyWellKnownName;
        private LookupValueDto _paymentMethod;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfPaymentDto"/> class.
        /// </summary>
        public SelfPaymentDto ()
        {
            CultureName = "en-US";
            CurrencyWellKnownName = Currency.USDollars;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>The amount.</value>
        public decimal Amount
        {
            get { return _amount; }
            set { ApplyPropertyChange ( ref _amount, () => Amount, value ); }
        }

        /// <summary>
        /// Gets the staff that collected the payment.
        /// </summary>
        /// <value>The staff that collected the payment.</value>
        public long CollectedByStaffKey { get; set; }

        /// <summary>
        /// Gets the collected date.
        /// </summary>
        /// <value>The collected date.</value>
        public DateTime? CollectedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the culture.
        /// </summary>
        /// <value>The name of the culture.</value>
        public string CultureName
        {
            get { return _cultureName; }
            set { ApplyPropertyChange ( ref _cultureName, () => CultureName, value ); }
        }

        /// <summary>
        /// Gets or sets the currency well known name.
        /// </summary>
        /// <value>The currency well known name.</value>
        public string CurrencyWellKnownName
        {
            get { return _currencyWellKnownName; }
            set { ApplyPropertyChange ( ref _currencyWellKnownName, () => CurrencyWellKnownName, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        public long PatientKey { get; set; }

        /// <summary>
        /// Gets the payment method.
        /// </summary>
        /// <value>The payment method.</value>
        public LookupValueDto PaymentMethod
        {
            get { return _paymentMethod; }
            set { ApplyPropertyChange ( ref _paymentMethod, () => PaymentMethod, value ); }
        }

        #endregion
    }
}
