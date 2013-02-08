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
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Payor Coverage cache defines the information about a payor.
    /// </summary>
    public class PayorCoverageCache : AuditableAggregateRootBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverageCache"/> class.
        /// </summary>
        protected PayorCoverageCache ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverageCache"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="payorCache">The payor cache.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <param name="memberNumber">The member number.</param>
        /// <param name="payorSubscriber">The payor subscriber.</param>
        /// <param name="payorCoverageCacheType">Type of the payor coverage cache.</param>
        protected internal PayorCoverageCache (
            Patient patient, 
            PayorCache payorCache, 
            DateRange effectiveDateRange, 
            string memberNumber, 
            PayorSubscriberCache payorSubscriber, 
            PayorCoverageCacheType payorCoverageCacheType )
        {
            Patient = patient;
            PayorCache = payorCache;
            EffectiveDateRange = effectiveDateRange;
            MemberNumber = memberNumber;
            PayorSubscriberCache = payorSubscriber;
            PayorCoverageCacheType = payorCoverageCacheType;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        public virtual Patient Patient { get; private set; }

        /// <summary>
        /// Gets the payor cache.
        /// </summary>
        public virtual PayorCache PayorCache { get; private set; }

        /// <summary>
        /// Gets the effective date range.
        /// </summary>
        public virtual DateRange EffectiveDateRange { get; private set; }

        /// <summary>
        /// Gets the member number.
        /// </summary>
        [NotNull]
        public virtual string MemberNumber { get; private set; }

        /// <summary>
        /// Gets the payor subscriber cache.
        /// </summary>
        [NotNull]
        public virtual PayorSubscriberCache PayorSubscriberCache { get; private set; }

        /// <summary>
        /// Gets the type of the payor coverage cache.
        /// </summary>
        /// <value>
        /// The type of the payor coverage cache.
        /// </value>
        public virtual PayorCoverageCacheType PayorCoverageCacheType { get; private set; }

        /// <summary>
        /// Revises the payor cache.
        /// </summary>
        /// <param name="payorCache">The payor cache.</param>
        public virtual void RevisePayorCache ( PayorCache payorCache )
        {
            PayorCache = payorCache;
        }

        /// <summary>
        /// Revises the payor coverage cache info.
        /// </summary>
        /// <param name="payorCoverageCacheType">Type of the payor coverage cache.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <param name="memberNumber">The member number.</param>
        public virtual void RevisePayorCoverageCacheInfo(PayorCoverageCacheType payorCoverageCacheType, DateRange effectiveDateRange, string memberNumber )
        {
            Check.IsNotNull(payorCoverageCacheType, () => PayorCoverageCacheType);
            Check.IsNotNull(effectiveDateRange, () => EffectiveDateRange);
            Check.IsNotNull(memberNumber, () => MemberNumber);

            DomainRuleEngine.CreateRuleEngine(this, "RevisePayorCoverageCacheInfoRuleSet")
                .WithContext(effectiveDateRange)
                .WithContext(payorCoverageCacheType)
                .Execute(
                    () =>
                    {
                        PayorCoverageCacheType = payorCoverageCacheType;
                        EffectiveDateRange = effectiveDateRange;
                        MemberNumber = memberNumber;
                    });
        }

        /// <summary>
        /// Revises the payor subscriber cache.
        /// </summary>
        /// <param name="payorSubscriberCache">The payor subscriber cache.</param>
        public virtual void RevisePayorSubscriberCache ( PayorSubscriberCache payorSubscriberCache )
        {
            PayorSubscriberCache = payorSubscriberCache;
        }
    }
}
