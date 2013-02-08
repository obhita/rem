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
    /// The GainShortScreenerExternalizingDisorder section of the screener.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof ( PropertyNameAsColumnNameNamingStrategy ) )]
    public class GainShortScreenerExternalizingDisorder : IEquatable<GainShortScreenerExternalizingDisorder>
    {
        private GainShortScreenerExternalizingDisorder ()
        {    
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreenerExternalizingDisorder"/> class.
        /// </summary>
        /// <param name="twoOrMoreLiedNumber">The two or more lied number.</param>
        /// <param name="twoOrMoreHardTimePayingAttentionNumber">The two or more hard time paying attention number.</param>
        /// <param name="twoOrMoreHardTimeListeningNumber">The two or more hard time listening number.</param>
        /// <param name="twoOrMoreThreatenedOthersNumber">The two or more threatened others number.</param>
        /// <param name="twoOrMoreStartedFightNumber">The two or more started fight number.</param>
        public GainShortScreenerExternalizingDisorder( int? twoOrMoreLiedNumber, 
            int? twoOrMoreHardTimePayingAttentionNumber, 
            int? twoOrMoreHardTimeListeningNumber,
            int? twoOrMoreThreatenedOthersNumber,
            int? twoOrMoreStartedFightNumber)
        {
            Check.IsInRange( twoOrMoreLiedNumber, 0, 3, () => TwoOrMoreLiedNumber);
            Check.IsInRange( twoOrMoreHardTimePayingAttentionNumber, 0, 3, () => TwoOrMoreHardTimePayingAttentionNumber);
            Check.IsInRange( twoOrMoreHardTimeListeningNumber, 0, 3, () => TwoOrMoreHardTimeListeningNumber);
            Check.IsInRange( twoOrMoreThreatenedOthersNumber, 0, 3, () => TwoOrMoreThreatenedOthersNumber);
            Check.IsInRange( twoOrMoreStartedFightNumber, 0, 3, () => TwoOrMoreStartedFightNumber);

            TwoOrMoreLiedNumber = twoOrMoreLiedNumber;
            TwoOrMoreHardTimePayingAttentionNumber = twoOrMoreHardTimePayingAttentionNumber;
            TwoOrMoreHardTimeListeningNumber = twoOrMoreHardTimeListeningNumber;
            TwoOrMoreThreatenedOthersNumber = twoOrMoreThreatenedOthersNumber;
            TwoOrMoreStartedFightNumber = twoOrMoreStartedFightNumber;
            CalculateExternalizingDisorderScores ();
        }

        /// <summary>
        /// Gets the respondant two or more times lied number.
        /// Question Number: 2.a
        /// </summary>
        public virtual int? TwoOrMoreLiedNumber { get; private set; }

        /// <summary>
        /// Gets the respondant two or more hard time paying attention number.
        /// Question Number: 2.b
        /// </summary>
        public virtual int? TwoOrMoreHardTimePayingAttentionNumber { get; private set; }

        /// <summary>
        /// Gets the respondant two or more hard time listening number.
        /// Question Number: 2.c
        /// </summary>
        public virtual int? TwoOrMoreHardTimeListeningNumber { get; private set; }

        /// <summary>
        /// Gets the respondant two or more threatened others number.
        /// Question Number: 2.d
        /// </summary>
        public virtual int? TwoOrMoreThreatenedOthersNumber { get; private set; }

        /// <summary>
        /// Gets the respondant two or more started fight number.
        /// Question Number: 2.e
        /// </summary>
        public virtual int? TwoOrMoreStartedFightNumber { get; private set; }

        /// <summary>
        /// Gets the externalizing disorder screener past month score.
        /// </summary>
        public virtual int? ExternalizingDisorderScreenerPastMonthScore { get; private set; }

        /// <summary>
        /// Gets the externalizing disorder screener past year score.
        /// </summary>
        public virtual int? ExternalizingDisorderScreenerPastYearScore { get; private set; }

        /// <summary>
        /// Gets the externalizing disorder screener lifetime score.
        /// </summary>
        public virtual int? ExternalizingDisorderScreenerLifetimeScore { get; private set; }

        /// <summary>
        /// Calculates the externalizing disorder scores.
        /// </summary>
        private void CalculateExternalizingDisorderScores()
        {
            var gainShortScreenerCalculationService = new GainShortScreenerCalculationService();
            gainShortScreenerCalculationService.ProcessQuestionValue(TwoOrMoreLiedNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(TwoOrMoreHardTimePayingAttentionNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(TwoOrMoreHardTimeListeningNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(TwoOrMoreThreatenedOthersNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(TwoOrMoreStartedFightNumber);

            ExternalizingDisorderScreenerPastMonthScore = gainShortScreenerCalculationService.ScreenerPastMonthScore;
            ExternalizingDisorderScreenerPastYearScore = gainShortScreenerCalculationService.ScreenerPastYearScore;
            ExternalizingDisorderScreenerLifetimeScore = gainShortScreenerCalculationService.ScreenerLifetimeScore;
        }

        #region IEquatable<GainShortScreenerExternalizingDisorder> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals ( GainShortScreenerExternalizingDisorder other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.TwoOrMoreLiedNumber.Equals ( TwoOrMoreLiedNumber ) && other.TwoOrMoreHardTimePayingAttentionNumber.Equals ( TwoOrMoreHardTimePayingAttentionNumber ) && other.TwoOrMoreThreatenedOthersNumber.Equals ( TwoOrMoreThreatenedOthersNumber ) && other.TwoOrMoreHardTimeListeningNumber.Equals ( TwoOrMoreHardTimeListeningNumber ) && other.TwoOrMoreStartedFightNumber.Equals ( TwoOrMoreStartedFightNumber );
        }

        #endregion

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
            if ( obj.GetType () != typeof ( GainShortScreenerExternalizingDisorder ) )
            {
                return false;
            }
            return Equals ( ( GainShortScreenerExternalizingDisorder ) obj );
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
                int result = ( TwoOrMoreLiedNumber.HasValue ? TwoOrMoreLiedNumber.Value : 0 );
                result = ( result * 397 ) ^ ( TwoOrMoreHardTimePayingAttentionNumber.HasValue ? TwoOrMoreHardTimePayingAttentionNumber.Value : 0 );
                result = ( result * 397 ) ^ ( TwoOrMoreThreatenedOthersNumber.HasValue ? TwoOrMoreThreatenedOthersNumber.Value : 0 );
                result = ( result * 397 ) ^ ( TwoOrMoreHardTimeListeningNumber.HasValue ? TwoOrMoreHardTimeListeningNumber.Value : 0 );
                result = ( result * 397 ) ^ ( TwoOrMoreStartedFightNumber.HasValue ? TwoOrMoreStartedFightNumber.Value : 0 );
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
        public static bool operator == ( GainShortScreenerExternalizingDisorder left, GainShortScreenerExternalizingDisorder right )
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
        public static bool operator != ( GainShortScreenerExternalizingDisorder left, GainShortScreenerExternalizingDisorder right )
        {
            return !Equals ( left, right );
        }
    }
}