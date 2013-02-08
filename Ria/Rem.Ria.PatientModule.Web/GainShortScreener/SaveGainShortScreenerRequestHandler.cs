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

using Rem.Domain.Clinical.GainShortScreenerModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.GainShortScreener
{
    /// <summary>
    /// Class for handling save gain short screener request.
    /// </summary>
    public class SaveGainShortScreenerRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<GainShortScreenerDto>, DtoResponse<GainShortScreenerDto>, GainShortScreenerDto, Domain.Clinical.GainShortScreenerModule.GainShortScreener>
    {
        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="aggregateRoot">The aggregate root.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( GainShortScreenerDto dto, Domain.Clinical.GainShortScreenerModule.GainShortScreener aggregateRoot )
        {
            var gainShortScreenerInternalizingDisorder = new GainShortScreenerInternalizingDisorder (
                dto.ProblemFeelingDepressedNumber,
                dto.ProblemSleepingNumber,
                dto.ProblemFeelingAnxiousNumber,
                dto.ProblemBecomingDistressedNumber,
                dto.ProblemCommittingSuicideNumber );

            var gainShortScreenerExternalizingDisorder = new GainShortScreenerExternalizingDisorder (
                dto.TwoOrMoreLiedNumber,
                dto.TwoOrMoreHardTimePayingAttentionNumber,
                dto.TwoOrMoreHardTimeListeningNumber,
                dto.TwoOrMoreThreatenedOthersNumber,
                dto.TwoOrMoreStartedFightNumber );

            var gainShortScreenerSubstanceDisorder = new GainShortScreenerSubstanceDisorder (
                dto.LastTimeUsedAlcoholDrugsNumber,
                dto.LastTimeSpentALotOfTimeGettingAlcoholNumber,
                dto.LastTimeKeptUsingAlcoholNumber,
                dto.LastTimeUseAlcoholCauseYouToGiveUpNumber,
                dto.LastTimeHadWithdrawProblemsNumber );

            var gainShortScreenerCrimeViolence = new GainShortScreenerCrimeViolence (
                dto.LastTimeYouHadDisagreementNumber,
                dto.LastTimeYouTookSomethingNumber,
                dto.LastTimeYouSoldIllegalDrugsNumber,
                dto.LastTimeYouDroveUnderTheInfluenceNumber,
                dto.LastTimeYouPurposelyDamagedPropertyNumber,
                dto.SignificantProblemsSeekingTreatmentIndicator,
                dto.SignificantProblemsSeekingTreatmentNote );

            aggregateRoot.ReviseAndCalculateScores (
                gainShortScreenerInternalizingDisorder,
                gainShortScreenerExternalizingDisorder,
                gainShortScreenerSubstanceDisorder,
                gainShortScreenerCrimeViolence );
            return true;
        }

        #endregion
    }
}
