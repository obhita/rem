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

using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PayorCacheFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.PayorCache">PayorCache</see>.
    /// </summary>
    public class PayorCacheFactory : IPayorCacheFactory
    {
        private readonly IPayorCacheRepository _payorCacheRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCacheFactory"/> class.
        /// </summary>
        /// <param name="payorCacheRepository">
        /// The payor cache repository.
        /// </param>
        public PayorCacheFactory ( IPayorCacheRepository payorCacheRepository )
        {
            _payorCacheRepository = payorCacheRepository;
        }

        /// <summary>
        /// Creates the payor cache.
        /// </summary>
        /// <param name="billingOfficeCache">
        /// The billing office cache.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <returns>
        /// The payor cache.
        /// </returns>
        public PayorCache CreatePayorCache ( BillingOfficeCache billingOfficeCache, string name, Address address )
        {
            var payorCache = new PayorCache ( billingOfficeCache, name, address );
            _payorCacheRepository.MakePersistent ( payorCache );
            return payorCache;
        }

        /// <summary>
        /// Destroys the payor cache.
        /// </summary>
        /// <param name="payorCache">
        /// The payor cache.
        /// </param>
        public void DestroyPayorCache ( PayorCache payorCache )
        {
            _payorCacheRepository.MakeTransient ( payorCache );
        }
    }
}
