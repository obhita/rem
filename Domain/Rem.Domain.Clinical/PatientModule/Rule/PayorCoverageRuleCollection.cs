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

using Pillar.Domain.Primitives;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Clinical.PatientModule.Rule
{
    /// <summary>
    /// Rule collection for PayorCoverage class.
    /// </summary>
    public class PayorCoverageCacheRuleCollection : AbstractRuleCollection<PayorCoverageCache>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverageCacheRuleCollection"/> class.
        /// </summary>
        /// <param name="payorCoverageCacheRepository">The payor coverage cache repository.</param>
        public PayorCoverageCacheRuleCollection ( IPayorCoverageCacheRepository payorCoverageCacheRepository )
        {
            NewRule ( () => NoDuplicatePayorCoverageCacheTypesWithSameEffectiveDateRangeRule )
                .When (
                    ( s, ctx ) =>
                        {
                            var coverageType = ctx.WorkingMemory.GetContextObject<PayorCoverageCacheType> ();
                            var effectiveDateRange = ctx.WorkingMemory.GetContextObject<DateRange> ();
                            var conflicts = payorCoverageCacheRepository.AnyPayorCoverageTypeInEffectiveDateRange (
                                s.Key, s.Patient.Key, coverageType, effectiveDateRange );
                            return conflicts;
                        } )
                .ThenReportRuleViolation ( "Cannot have two payor coverages with the same payor coverage type active at the same time." );

            NewRuleSet ( () => CreatePayorCoverageCacheRuleSet, NoDuplicatePayorCoverageCacheTypesWithSameEffectiveDateRangeRule );
            NewRuleSet( () => RevisePayorCoverageCacheInfoRuleSet, NoDuplicatePayorCoverageCacheTypesWithSameEffectiveDateRangeRule );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the create payor coverage cache rule set.
        /// </summary>
        public IRuleSet CreatePayorCoverageCacheRuleSet { get; private set; }

        /// <summary>
        /// Gets the no duplicate payor coverage cache types with same effective date range rule.
        /// </summary>
        public IRule NoDuplicatePayorCoverageCacheTypesWithSameEffectiveDateRangeRule { get; private set; }

        /// <summary>
        /// Gets the revise payor coverage cache info.
        /// </summary>
        public IRuleSet RevisePayorCoverageCacheInfoRuleSet { get; private set; }

        #endregion
    }
}
