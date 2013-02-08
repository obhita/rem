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
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Clinical.GainShortScreenerModule
{
    /// <summary>
    /// The GainShortScreenerSubstanceDisorder section of the screener.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof ( PropertyNameAsColumnNameNamingStrategy ) )]
    public class GainShortScreenerSubstanceDisorder : IEquatable<GainShortScreenerSubstanceDisorder>
    {
        private GainShortScreenerSubstanceDisorder ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreenerSubstanceDisorder"/> class.
        /// </summary>
        /// <param name="lastTimeUsedAlcoholDrugsNumber">The last time used alcohol drugs number.</param>
        /// <param name="lastTimeSpentALotOfTimeGettingAlcoholNumber">The last time spent A lot of time getting alcohol number.</param>
        /// <param name="lastTimeKeptUsingAlcoholNumber">The last time kept using alcohol number.</param>
        /// <param name="lastTimeUseAlcoholCauseYouToGiveUpNumber">The last time use alcohol cause you to give up number.</param>
        /// <param name="lastTimeHadWithdrawProblemsNumber">The last time had withdraw problems number.</param>
        public GainShortScreenerSubstanceDisorder ( int? lastTimeUsedAlcoholDrugsNumber,
                                                    int? lastTimeSpentALotOfTimeGettingAlcoholNumber,
                                                    int? lastTimeKeptUsingAlcoholNumber,
                                                    int? lastTimeUseAlcoholCauseYouToGiveUpNumber,
                                                    int? lastTimeHadWithdrawProblemsNumber )
        {
            Check.IsInRange ( lastTimeUsedAlcoholDrugsNumber, 0, 3, () => LastTimeUsedAlcoholDrugsNumber );
            Check.IsInRange ( lastTimeSpentALotOfTimeGettingAlcoholNumber, 0, 3, () => LastTimeSpentALotOfTimeGettingAlcoholNumber );
            Check.IsInRange ( lastTimeKeptUsingAlcoholNumber, 0, 3, () => LastTimeKeptUsingAlcoholNumber );
            Check.IsInRange ( lastTimeUseAlcoholCauseYouToGiveUpNumber, 0, 3, () => LastTimeUseAlcoholCauseYouToGiveUpNumber );
            Check.IsInRange ( lastTimeHadWithdrawProblemsNumber, 0, 3, () => LastTimeHadWithdrawProblemsNumber );

            LastTimeUsedAlcoholDrugsNumber = lastTimeUsedAlcoholDrugsNumber;
            LastTimeSpentALotOfTimeGettingAlcoholNumber = lastTimeSpentALotOfTimeGettingAlcoholNumber;
            LastTimeKeptUsingAlcoholNumber = lastTimeKeptUsingAlcoholNumber;
            LastTimeUseAlcoholCauseYouToGiveUpNumber = lastTimeUseAlcoholCauseYouToGiveUpNumber;
            LastTimeHadWithdrawProblemsNumber = lastTimeHadWithdrawProblemsNumber;
            CalculateSubstanceDisorderScores();
        }

        /// <summary>
        /// Gets the respondant last time used alcohol drugs number.
        /// Question Number: 3.a
        /// </summary>
        public virtual int? LastTimeUsedAlcoholDrugsNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time spent A lot of time getting alcohol number.
        /// Question Number: 3.b
        /// </summary>
        public virtual int? LastTimeSpentALotOfTimeGettingAlcoholNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time kept using alcohol number. 
        /// Question Number: 3.c
        /// </summary>
        public virtual int? LastTimeKeptUsingAlcoholNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time use alcohol cause you to give up number.
        /// Question Number: 3.d
        /// </summary>
        public virtual int? LastTimeUseAlcoholCauseYouToGiveUpNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time had withdraw problems number.
        /// Question Number: 3.e
        /// </summary>
        public virtual int? LastTimeHadWithdrawProblemsNumber { get; private set; }

        /// <summary>
        /// Gets the respondant substance disorder screener past month score.
        /// </summary>
        public virtual int? SubstanceDisorderScreenerPastMonthScore { get; private set; }

        /// <summary>
        /// Gets the respondant substance disorder screener past year score.
        /// </summary>
        public virtual int? SubstanceDisorderScreenerPastYearScore { get; private set; }

        /// <summary>
        /// Gets the respondant substance disorder screener lifetime score.
        /// </summary>
        public virtual int? SubstanceDisorderScreenerLifetimeScore { get; private set; }

        /// <summary>
        /// Calculates the substance disorder scores.
        /// </summary>
        private void CalculateSubstanceDisorderScores()
        {
            var gainShortScreenerCalculationService = new GainShortScreenerCalculationService();
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeUsedAlcoholDrugsNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeSpentALotOfTimeGettingAlcoholNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeKeptUsingAlcoholNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeUseAlcoholCauseYouToGiveUpNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeHadWithdrawProblemsNumber);

            SubstanceDisorderScreenerPastMonthScore = gainShortScreenerCalculationService.ScreenerPastMonthScore;
            SubstanceDisorderScreenerPastYearScore = gainShortScreenerCalculationService.ScreenerPastYearScore;
            SubstanceDisorderScreenerLifetimeScore = gainShortScreenerCalculationService.ScreenerLifetimeScore;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals ( GainShortScreenerSubstanceDisorder other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.LastTimeUsedAlcoholDrugsNumber.Equals ( LastTimeUsedAlcoholDrugsNumber ) && other.LastTimeSpentALotOfTimeGettingAlcoholNumber.Equals ( LastTimeSpentALotOfTimeGettingAlcoholNumber ) && other.LastTimeKeptUsingAlcoholNumber.Equals ( LastTimeKeptUsingAlcoholNumber ) && other.LastTimeUseAlcoholCauseYouToGiveUpNumber.Equals ( LastTimeUseAlcoholCauseYouToGiveUpNumber ) && other.LastTimeHadWithdrawProblemsNumber.Equals ( LastTimeHadWithdrawProblemsNumber ) && other.SubstanceDisorderScreenerPastMonthScore.Equals ( SubstanceDisorderScreenerPastMonthScore ) && other.SubstanceDisorderScreenerPastYearScore.Equals ( SubstanceDisorderScreenerPastYearScore ) && other.SubstanceDisorderScreenerLifetimeScore.Equals ( SubstanceDisorderScreenerLifetimeScore );
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof ( GainShortScreenerSubstanceDisorder ) )
            {
                return false;
            }
            return Equals ( ( GainShortScreenerSubstanceDisorder ) obj );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode ()
        {
            unchecked
            {
                int result = ( LastTimeUsedAlcoholDrugsNumber.HasValue ? LastTimeUsedAlcoholDrugsNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeSpentALotOfTimeGettingAlcoholNumber.HasValue ? LastTimeSpentALotOfTimeGettingAlcoholNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeKeptUsingAlcoholNumber.HasValue ? LastTimeKeptUsingAlcoholNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeUseAlcoholCauseYouToGiveUpNumber.HasValue ? LastTimeUseAlcoholCauseYouToGiveUpNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeHadWithdrawProblemsNumber.HasValue ? LastTimeHadWithdrawProblemsNumber.Value : 0 );
                result = ( result * 397 ) ^ ( SubstanceDisorderScreenerPastMonthScore.HasValue ? SubstanceDisorderScreenerPastMonthScore.Value : 0 );
                result = ( result * 397 ) ^ ( SubstanceDisorderScreenerPastYearScore.HasValue ? SubstanceDisorderScreenerPastYearScore.Value : 0 );
                result = ( result * 397 ) ^ ( SubstanceDisorderScreenerLifetimeScore.HasValue ? SubstanceDisorderScreenerLifetimeScore.Value : 0 );
                return result;
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == ( GainShortScreenerSubstanceDisorder left, GainShortScreenerSubstanceDisorder right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator != ( GainShortScreenerSubstanceDisorder left, GainShortScreenerSubstanceDisorder right )
        {
            return !Equals ( left, right );
        }
    }
}