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

using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.PatientModule.Rule
{
    /// <summary>
    /// Rule collection for SelfPayment class.
    /// </summary>
    public class SelfPaymentRuleCollection : AbstractRuleCollection<SelfPayment>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfPaymentRuleCollection"/> class.
        /// </summary>
        public SelfPaymentRuleCollection ()
        {
            AutoValidatePropertyRules = true;

            NewPropertyRule ( () => AmountMustBeGreaterThanZero )
                .WithProperty ( sp => sp.Money.Amount )
                .GreaterThan ( 0 );

            NewPropertyRule ( () => CollectedDateCannotBeInTheFuture )
                .WithProperty ( sp => sp.CollectedDate.Value )
                .CannotBeFutureDate();

            NewRuleSet ( () => CreateSelfPaymentRuleSet, AmountMustBeGreaterThanZero, CollectedDateCannotBeInTheFuture );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the amount must be greater then zero rule.
        /// </summary>
        public IPropertyRule AmountMustBeGreaterThanZero { get; private set; }

        /// <summary>
        /// Gets the collected date cannot be in the future.
        /// </summary>
        public IPropertyRule CollectedDateCannotBeInTheFuture { get; private set; }

        /// <summary>
        /// Gets the create self payment rule set.
        /// </summary>
        public IRuleSet CreateSelfPaymentRuleSet { get; private set; }

        #endregion
    }
}
