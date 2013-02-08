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
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The SelfPaymentFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.SelfPayment">SelfPayment</see>.
    /// </summary>
    public class SelfPaymentFactory : ISelfPaymentFactory
    {
        private readonly ISelfPaymentRepository _selfPaymentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfPaymentFactory"/> class.
        /// </summary>
        /// <param name="selfPaymentRepository">The self payment repository.</param>
        public SelfPaymentFactory ( ISelfPaymentRepository selfPaymentRepository )
        {
            _selfPaymentRepository = selfPaymentRepository;
        }

        /// <summary>
        /// Creates the self payment.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="staff">The staff.</param>
        /// <param name="money">The money.</param>
        /// <param name="paymentMethod">The payment method.</param>
        /// <param name="collectedDate">The collected date.</param>
        /// <returns>A Self Payment.</returns>
        public SelfPayment CreateSelfPayment ( Patient patient, Staff staff, Money money, PaymentMethod paymentMethod, DateTime? collectedDate )
        {
            var selfPayment = new SelfPayment ( patient, staff, money, paymentMethod, collectedDate );
            SelfPayment createdSelfPayment = null;

            DomainRuleEngine.CreateRuleEngine ( selfPayment, "CreateSelfPaymentRuleSet" )
                .Execute ( () =>
                    {
                        createdSelfPayment = selfPayment;

                        _selfPaymentRepository.MakePersistent(createdSelfPayment);
                    });

            return createdSelfPayment;
        }

        /// <summary>
        /// Destroys the self payment.
        /// </summary>
        /// <param name="selfPayment">The self payment.</param>
        public void DestroySelfPayment ( SelfPayment selfPayment )
        {
            _selfPaymentRepository.MakeTransient ( selfPayment );
        }
    }
}
