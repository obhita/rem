﻿#region License

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

using Rem.Domain.Billing.BillingOfficeModule;
using Rem.WellKnownNames.PayorModule;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// The PayorFactory implements lifetime management of the <see cref="T:Rem.Domain.Billing.PayorModule.Payor">Payor</see> .
    /// </summary>
    public class PayorFactory : IPayorFactory
    {
        #region Constants and Fields

        private readonly IPayorRepository _payorRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorFactory"/> class.
        /// </summary>
        /// <param name="payorRepository">The payor repository.</param>
        public PayorFactory ( IPayorRepository payorRepository )
        {
            _payorRepository = payorRepository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates the payor.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="billingOffice">The billing office.</param>
        /// <param name="electronicTransmitterIdentificationNumber">The electronic transmitter identification number.</param>
        /// <returns>A payor.</returns>
        public Payor CreatePayor ( string name, BillingOffice billingOffice, string electronicTransmitterIdentificationNumber )
        {
            var payor = new Payor( name, billingOffice, electronicTransmitterIdentificationNumber);
            _payorRepository.MakePersistent(payor);
            return payor;
        }

        /// <summary>
        /// Destroys the payor cache.
        /// </summary>
        /// <param name="payor">The payor.</param>
        public void DestroyPayor ( Payor payor )
        {
            _payorRepository.MakeTransient ( payor );
        }

        #endregion
    }
}
