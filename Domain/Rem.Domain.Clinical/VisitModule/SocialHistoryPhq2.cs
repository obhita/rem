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
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;
using Rem.Domain.Clinical.SbirtModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// SocialHistoryPhq2 defines elements for managing the scheduling of the <see cref="Phq9">Phq9</see> 
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class SocialHistoryPhq2 : IEquatable<SocialHistoryPhq2>
    {
        private SocialHistoryPhq2 ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialHistoryPhq2"/> class.
        /// </summary>
        /// <param name="phq2LittleInterestInDoingThingsAnswerNumber">The PHQ2 little interest in doing things answer number.</param>
        /// <param name="phq2FeelingDownAnswerNumber">The PHQ2 feeling down answer number.</param>
        /// <param name="phq2Score">The PHQ2 score.</param>
        public SocialHistoryPhq2 (
            int? phq2LittleInterestInDoingThingsAnswerNumber,
            int? phq2FeelingDownAnswerNumber,
            int? phq2Score )
        {
            Phq2LittleInterestInDoingThingsAnswerNumber = phq2LittleInterestInDoingThingsAnswerNumber;
            Phq2FeelingDownAnswerNumber = phq2FeelingDownAnswerNumber;
            Phq2Score = phq2Score;

            CalculatePhq2Score ();
        }

        /// <summary>
        /// Gets the PHQ2 little interest in doing things answer number.
        /// </summary>
        public virtual int? Phq2LittleInterestInDoingThingsAnswerNumber { get; private set; }

        /// <summary>
        /// Gets the PHQ2 feeling down answer number.
        /// </summary>
        public virtual int? Phq2FeelingDownAnswerNumber { get; private set; }

        /// <summary>
        /// Gets the PHQ2 score.
        /// </summary>
        public virtual int? Phq2Score { get; private set; }

        /// <summary>
        /// Gets a boolean value indicating that the PHQ2 score is above PHQ9 threshold.
        /// </summary>
        public virtual bool? IsPhq2ScoreAbovePhq9ThresholdIndicator { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is PHQ9 threshold satisfied.
        /// </summary>
        /// <value><c>true</c> if this instance is PHQ9 threshold satisfied; otherwise, <c>false</c>.</value>
        [IgnoreMapping]
        public virtual bool IsPhq9ThresholdSatisfied
        {
            get
            {
                return IsPhq2ScoreAbovePhq9ThresholdIndicator.HasValue
                       && IsPhq2ScoreAbovePhq9ThresholdIndicator.Value;
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
        public static bool operator ==(SocialHistoryPhq2 left, SocialHistoryPhq2 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(SocialHistoryPhq2 left, SocialHistoryPhq2 right)
        {
            return !Equals(left, right);
        }

        #region IEquatable<SocialHistoryPhq2> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals ( SocialHistoryPhq2 other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.Phq2LittleInterestInDoingThingsAnswerNumber.Equals ( Phq2LittleInterestInDoingThingsAnswerNumber ) && other.Phq2FeelingDownAnswerNumber.Equals ( Phq2FeelingDownAnswerNumber ) && other.Phq2Score.Equals ( Phq2Score ) && other.IsPhq2ScoreAbovePhq9ThresholdIndicator.Equals ( IsPhq2ScoreAbovePhq9ThresholdIndicator );
        }

        #endregion

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
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
            if ( obj.GetType () != typeof ( SocialHistoryPhq2 ) )
            {
                return false;
            }
            return Equals ( ( SocialHistoryPhq2 ) obj );
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
                var result = ( Phq2LittleInterestInDoingThingsAnswerNumber.HasValue ? Phq2LittleInterestInDoingThingsAnswerNumber.Value : 0 );
                result = ( result * 397 ) ^ ( Phq2FeelingDownAnswerNumber.HasValue ? Phq2FeelingDownAnswerNumber.Value : 0 );
                result = ( result * 397 ) ^ ( Phq2Score.HasValue ? Phq2Score.Value : 0 );
                result = ( result * 397 ) ^ ( IsPhq2ScoreAbovePhq9ThresholdIndicator.GetHashCode () );
                return result;
            }
        }

        private void CalculatePhq2Score()
        {
            Phq2Score = (Phq2LittleInterestInDoingThingsAnswerNumber ?? 0) + (Phq2FeelingDownAnswerNumber ?? 0);
            IsPhq2ScoreAbovePhq9ThresholdIndicator = Phq2Score >= 4;
        }
    }
}