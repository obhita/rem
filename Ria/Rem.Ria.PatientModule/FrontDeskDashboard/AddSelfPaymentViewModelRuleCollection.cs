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
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// Rule collection for AddSelfPaymentViewModel class.
    /// </summary>
    public class AddSelfPaymentViewModelRuleCollection : AbstractRuleCollection<AddSelfPaymentViewModel>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddSelfPaymentViewModelRuleCollection"/> class.
        /// </summary>
        public AddSelfPaymentViewModelRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            var amountError = new DataErrorInfo (
                "Amount must be greater then 0.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<SelfPaymentDto, object> ( dto => dto.Amount ) );
            NewPropertyRule ( () => AmountMustBeGreaterThanZero )
                .WithProperty ( s => s.SelfPayment.Amount )
                .GreaterThan ( 0 )
                .ElseThen ( s => s.SelfPayment.AddDataErrorInfo ( amountError ) )
                .Then ( s => s.SelfPayment.RemoveDataErrorInfo ( amountError ) );

            var paymentMethodError = new DataErrorInfo (
                "Payment Method is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<SelfPaymentDto, object> ( dto => dto.PaymentMethod ) );
            NewPropertyRule ( () => PaymentMethodIsRequired )
                .WithProperty ( s => s.SelfPayment.PaymentMethod )
                .NotNull ()
                .ElseThen ( s => s.SelfPayment.AddDataErrorInfo ( paymentMethodError ) )
                .Then ( s => s.SelfPayment.RemoveDataErrorInfo ( paymentMethodError ) );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the amount must be greater then zero.
        /// </summary>
        /// <value>The amount must be greater then zero.</value>
        public IPropertyRule AmountMustBeGreaterThanZero { get; set; }

        /// <summary>
        /// Gets or sets the payment method is required.
        /// </summary>
        /// <value>The payment method is required.</value>
        public IPropertyRule PaymentMethodIsRequired { get; set; }

        #endregion
    }
}
