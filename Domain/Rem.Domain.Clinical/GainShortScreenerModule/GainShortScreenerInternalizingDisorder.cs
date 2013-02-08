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
    /// The GainShortScreenerInternalizingDisorder section of the screener.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof ( PropertyNameAsColumnNameNamingStrategy ) )]
    public class GainShortScreenerInternalizingDisorder : IEquatable<GainShortScreenerInternalizingDisorder>
    {
        private GainShortScreenerInternalizingDisorder ()
        {          
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreenerInternalizingDisorder"/> class.
        /// </summary>
        /// <param name="problemFeelingDepressedNumber">The problem feeling depressed number.</param>
        /// <param name="problemSleepingNumber">The problem sleeping number.</param>
        /// <param name="problemFeelingAnxiousNumber">The problem feeling anxious number.</param>
        /// <param name="problemBecomingDistressedNumber">The problem becoming distressed number.</param>
        /// <param name="problemCommittingSuicideNumber">The problem committing suicide number.</param>
        public GainShortScreenerInternalizingDisorder ( int? problemFeelingDepressedNumber, 
            int? problemSleepingNumber, 
            int? problemFeelingAnxiousNumber, 
            int? problemBecomingDistressedNumber, 
            int? problemCommittingSuicideNumber )
        {
            Check.IsInRange( problemFeelingDepressedNumber, 0, 3, () => ProblemFeelingDepressedNumber);
            Check.IsInRange( problemSleepingNumber, 0, 3, () => ProblemSleepingNumber);
            Check.IsInRange( problemFeelingAnxiousNumber, 0, 3, () => ProblemFeelingAnxiousNumber);
            Check.IsInRange( problemBecomingDistressedNumber, 0, 3, () => ProblemBecomingDistressedNumber);
            Check.IsInRange( problemCommittingSuicideNumber, 0, 3, () => ProblemCommittingSuicideNumber);

            ProblemFeelingDepressedNumber = problemFeelingDepressedNumber;
            ProblemSleepingNumber = problemSleepingNumber;
            ProblemFeelingAnxiousNumber = problemFeelingAnxiousNumber;
            ProblemBecomingDistressedNumber = problemBecomingDistressedNumber;
            ProblemCommittingSuicideNumber = problemCommittingSuicideNumber;
            CalculateInternalizingDisorderScores ();
        }

        /// <summary>
        /// Gets the respondant problem feeling depressed number.
        /// Question Number: 1.a
        /// </summary>
        public virtual int? ProblemFeelingDepressedNumber { get; private set; }

        /// <summary>
        /// Gets the respondant problem sleeping number.
        /// Question Number: 1.b
        /// </summary>
        public virtual int? ProblemSleepingNumber { get; private set; }

        /// <summary>
        /// Gets the respondant problem feeling anxious number.
        /// Question Number: 1.c
        /// </summary>
        public virtual int? ProblemFeelingAnxiousNumber { get; private set; }

        /// <summary>
        /// Gets the respondant problem becoming distressed number.
        /// Question Number: 1.d
        /// </summary>
        public virtual int? ProblemBecomingDistressedNumber { get; private set; }

        /// <summary>
        /// Gets the respondant problem committing suicide number.
        /// Question Number: 1.e
        /// </summary>
        public virtual int? ProblemCommittingSuicideNumber { get; private set; }

        /// <summary>
        /// Gets the internalizing disorder screener past month score.
        /// </summary>
        public virtual int? InternalizingDisorderScreenerPastMonthScore { get; private set; }

        /// <summary>
        /// Gets the internalizing disorder screener past year score.
        /// </summary>
        public virtual int? InternalizingDisorderScreenerPastYearScore { get; private set; }

        /// <summary>
        /// Gets the internalizing disorder screener lifetime score.
        /// </summary>
        public virtual int? InternalizingDisorderScreenerLifetimeScore { get; private set; }

        /// <summary>
        /// Calculates the internalizing disorder scores.
        /// </summary>
        private void CalculateInternalizingDisorderScores()
        {
            var gainShortScreenerCalculationService = new GainShortScreenerCalculationService ();
            gainShortScreenerCalculationService.ProcessQuestionValue(ProblemFeelingDepressedNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(ProblemSleepingNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(ProblemFeelingAnxiousNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(ProblemBecomingDistressedNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(ProblemCommittingSuicideNumber);

            InternalizingDisorderScreenerPastMonthScore = gainShortScreenerCalculationService.ScreenerPastMonthScore;
            InternalizingDisorderScreenerPastYearScore = gainShortScreenerCalculationService.ScreenerPastYearScore;
            InternalizingDisorderScreenerLifetimeScore = gainShortScreenerCalculationService.ScreenerLifetimeScore;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals ( GainShortScreenerInternalizingDisorder other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.ProblemFeelingDepressedNumber.Equals ( ProblemFeelingDepressedNumber ) && other.ProblemSleepingNumber.Equals ( ProblemSleepingNumber ) && other.ProblemFeelingAnxiousNumber.Equals ( ProblemFeelingAnxiousNumber ) && other.ProblemBecomingDistressedNumber.Equals ( ProblemBecomingDistressedNumber ) && other.ProblemCommittingSuicideNumber.Equals ( ProblemCommittingSuicideNumber ) && other.InternalizingDisorderScreenerPastMonthScore.Equals ( InternalizingDisorderScreenerPastMonthScore ) && other.InternalizingDisorderScreenerPastYearScore.Equals ( InternalizingDisorderScreenerPastYearScore ) && other.InternalizingDisorderScreenerLifetimeScore.Equals ( InternalizingDisorderScreenerLifetimeScore );
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
            if ( obj.GetType () != typeof ( GainShortScreenerInternalizingDisorder ) )
            {
                return false;
            }
            return Equals ( ( GainShortScreenerInternalizingDisorder ) obj );
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
                int result = ( ProblemFeelingDepressedNumber.HasValue ? ProblemFeelingDepressedNumber.Value : 0 );
                result = ( result * 397 ) ^ ( ProblemSleepingNumber.HasValue ? ProblemSleepingNumber.Value : 0 );
                result = ( result * 397 ) ^ ( ProblemFeelingAnxiousNumber.HasValue ? ProblemFeelingAnxiousNumber.Value : 0 );
                result = ( result * 397 ) ^ ( ProblemBecomingDistressedNumber.HasValue ? ProblemBecomingDistressedNumber.Value : 0 );
                result = ( result * 397 ) ^ ( ProblemCommittingSuicideNumber.HasValue ? ProblemCommittingSuicideNumber.Value : 0 );
                result = ( result * 397 ) ^ ( InternalizingDisorderScreenerPastMonthScore.HasValue ? InternalizingDisorderScreenerPastMonthScore.Value : 0 );
                result = ( result * 397 ) ^ ( InternalizingDisorderScreenerPastYearScore.HasValue ? InternalizingDisorderScreenerPastYearScore.Value : 0 );
                result = ( result * 397 ) ^ ( InternalizingDisorderScreenerLifetimeScore.HasValue ? InternalizingDisorderScreenerLifetimeScore.Value : 0 );
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
        public static bool operator == ( GainShortScreenerInternalizingDisorder left, GainShortScreenerInternalizingDisorder right )
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
        public static bool operator != ( GainShortScreenerInternalizingDisorder left, GainShortScreenerInternalizingDisorder right )
        {
            return !Equals ( left, right );
        }
    }
}