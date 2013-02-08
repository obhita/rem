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

using Pillar.Domain;
using Pillar.Domain.Primitives;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PayorCoverageFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.PatientModule.PayorCoverageCache">PayorCoverageCache</see>.
    /// </summary>
    public class PayorCoverageCacheFactory : IPayorCoverageCacheFactory
    {
        private readonly IPayorCoverageCacheRepository _payorCoverageCacheRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverageCacheFactory"/> class.
        /// </summary>
        /// <param name="payorCoverageCacheRepository">The payor coverage cache repository.</param>
        public PayorCoverageCacheFactory ( IPayorCoverageCacheRepository payorCoverageCacheRepository )
        {
            _payorCoverageCacheRepository = payorCoverageCacheRepository;
        }


        /// <summary>
        /// Creates the payor coverage.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="payorCache">The payor cache.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <param name="memberNumber">The member number.</param>
        /// <param name="payorSubscriberCache">The payor subscriber cache.</param>
        /// <param name="payorCoverageCacheType">Type of the payor coverage cache.</param>
        /// <returns>A Payor coverage cache.</returns>
        public PayorCoverageCache CreatePayorCoverage (
            Patient patient, 
            PayorCache payorCache, 
            DateRange effectiveDateRange, 
            string memberNumber, 
            PayorSubscriberCache payorSubscriberCache, 
            PayorCoverageCacheType payorCoverageCacheType )
        {
            var payorCoverage = new PayorCoverageCache ( patient, payorCache, effectiveDateRange, memberNumber, payorSubscriberCache, payorCoverageCacheType );

            PayorCoverageCache createdPayorCoverage = null;

            DomainRuleEngine.CreateRuleEngine(payorCoverage, "CreatePayorCoverageRuleSet")
                .WithContext ( payorCoverage.PayorCoverageCacheType )
                .WithContext ( payorCoverage.EffectiveDateRange )
                .Execute(() =>
                {
                    createdPayorCoverage = payorCoverage;

                    _payorCoverageCacheRepository.MakePersistent(payorCoverage);
                });

            return createdPayorCoverage;
        }

        /// <summary>
        /// Destroys the payor coverage.
        /// </summary>
        /// <param name="payorCoverageCache">
        /// The payor coverage cache.
        /// </param>
        public void DestroyPayorCoverage ( PayorCoverageCache payorCoverageCache )
        {
            _payorCoverageCacheRepository.MakeTransient ( payorCoverageCache );
        }
    }
}
