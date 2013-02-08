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
    /// The GainShortScreenerCrimeViolence section of the screener.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof ( PropertyNameAsColumnNameNamingStrategy ) )]
    public class GainShortScreenerCrimeViolence : IEquatable<GainShortScreenerCrimeViolence>
    {
        private GainShortScreenerCrimeViolence ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GainShortScreenerCrimeViolence"/> class.
        /// </summary>
        /// <param name="lastTimeYouHadDisagreementNumber">The last time you had disagreement number.</param>
        /// <param name="lastTimeYouTookSomethingNumber">The last time you took something number.</param>
        /// <param name="lastTimeYouSoldIllegalDrugsNumber">The last time you sold illegal drugs number.</param>
        /// <param name="lastTimeYouDroveUnderTheInfluenceNumber">The last time you drove under the influence number.</param>
        /// <param name="lastTimeYouPurposelyDamagedPropertyNumber">The last time you purposely damaged property number.</param>
        /// <param name="significantProblemsSeekingTreatmentIndicator">The significant problems seeking treatment indicator.</param>
        /// <param name="significantProblemsSeekingTreatmentNote">The significant problems seeking treatment note.</param>
        public GainShortScreenerCrimeViolence ( int? lastTimeYouHadDisagreementNumber,
                                                int? lastTimeYouTookSomethingNumber,
                                                int? lastTimeYouSoldIllegalDrugsNumber,
                                                int? lastTimeYouDroveUnderTheInfluenceNumber,
                                                int? lastTimeYouPurposelyDamagedPropertyNumber,
                                                bool? significantProblemsSeekingTreatmentIndicator,
                                                string significantProblemsSeekingTreatmentNote )
        {
            Check.IsInRange ( lastTimeYouHadDisagreementNumber, 0, 3, () => LastTimeYouHadDisagreementNumber );
            Check.IsInRange ( lastTimeYouTookSomethingNumber, 0, 3, () => LastTimeYouTookSomethingNumber );
            Check.IsInRange ( lastTimeYouSoldIllegalDrugsNumber, 0, 3, () => LastTimeYouSoldIllegalDrugsNumber );
            Check.IsInRange ( lastTimeYouDroveUnderTheInfluenceNumber, 0, 3, () => LastTimeYouDroveUnderTheInfluenceNumber );
            Check.IsInRange ( lastTimeYouPurposelyDamagedPropertyNumber, 0, 3, () => LastTimeYouPurposelyDamagedPropertyNumber );

            LastTimeYouHadDisagreementNumber = lastTimeYouHadDisagreementNumber;
            LastTimeYouTookSomethingNumber = lastTimeYouTookSomethingNumber;
            LastTimeYouSoldIllegalDrugsNumber = lastTimeYouSoldIllegalDrugsNumber;
            LastTimeYouDroveUnderTheInfluenceNumber = lastTimeYouDroveUnderTheInfluenceNumber;
            LastTimeYouPurposelyDamagedPropertyNumber = lastTimeYouPurposelyDamagedPropertyNumber;
            SignificantProblemsSeekingTreatmentIndicator = significantProblemsSeekingTreatmentIndicator;
            SignificantProblemsSeekingTreatmentNote = significantProblemsSeekingTreatmentNote;
            CalculateCrimeViolenceScores ();
        }

        /// <summary>
        /// Gets the respondant last time you had disagreement number.
        /// Question Number: 4.a
        /// </summary> 
        public virtual int? LastTimeYouHadDisagreementNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time you took something number
        /// Question Number: 4.b
        /// </summary>
        public virtual int? LastTimeYouTookSomethingNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time you sold illegal drugs number.
        /// Question Number: 4.c
        /// </summary>
        public virtual int? LastTimeYouSoldIllegalDrugsNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time you drove under the influence number.
        /// Question Number: 4.d
        /// </summary>
        public virtual int? LastTimeYouDroveUnderTheInfluenceNumber { get; private set; }

        /// <summary>
        /// Gets the respondant last time you purposely damaged property number.
        /// Question Number: 4.e
        /// </summary>
        public virtual int? LastTimeYouPurposelyDamagedPropertyNumber { get; private set; }

        /// <summary>
        /// Gets the respondant significant problems seeking treatment indicator.
        /// Question Number: 5
        /// </summary>
        public virtual bool? SignificantProblemsSeekingTreatmentIndicator { get; private set; }

        /// <summary>
        /// Gets the respondant significant problems seeking treatment note.
        /// Question Number: 5.v
        /// </summary>
        public virtual string SignificantProblemsSeekingTreatmentNote { get; private set; }

        /// <summary>
        /// Gets the respondant crime violence screener past month score.
        /// </summary>
        public virtual int? CrimeViolenceScreenerPastMonthScore { get; private set; }

        /// <summary>
        /// Gets the respondant crime violence screener past year score.
        /// </summary>
        public virtual int? CrimeViolenceScreenerPastYearScore { get; private set; }

        /// <summary>
        /// Gets the respondant crime violence screener lifetime score.
        /// </summary>
        public virtual int? CrimeViolenceScreenerLifetimeScore { get; private set; }

        /// <summary>
        /// Calculates the crime violence total scores.
        /// </summary>
        private void CalculateCrimeViolenceScores()
        {
            var gainShortScreenerCalculationService = new GainShortScreenerCalculationService();
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeYouHadDisagreementNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeYouTookSomethingNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeYouSoldIllegalDrugsNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeYouDroveUnderTheInfluenceNumber);
            gainShortScreenerCalculationService.ProcessQuestionValue(LastTimeYouPurposelyDamagedPropertyNumber);

            CrimeViolenceScreenerPastMonthScore = gainShortScreenerCalculationService.ScreenerPastMonthScore;
            CrimeViolenceScreenerPastYearScore = gainShortScreenerCalculationService.ScreenerPastYearScore;
            CrimeViolenceScreenerLifetimeScore = gainShortScreenerCalculationService.ScreenerLifetimeScore;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals ( GainShortScreenerCrimeViolence other )
        {
            if ( ReferenceEquals ( null, other ) ) 
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.LastTimeYouHadDisagreementNumber.Equals ( LastTimeYouHadDisagreementNumber ) && other.LastTimeYouTookSomethingNumber.Equals ( LastTimeYouTookSomethingNumber ) && other.LastTimeYouSoldIllegalDrugsNumber.Equals ( LastTimeYouSoldIllegalDrugsNumber ) && other.LastTimeYouDroveUnderTheInfluenceNumber.Equals ( LastTimeYouDroveUnderTheInfluenceNumber ) && other.LastTimeYouPurposelyDamagedPropertyNumber.Equals ( LastTimeYouPurposelyDamagedPropertyNumber ) && other.SignificantProblemsSeekingTreatmentIndicator.Equals ( SignificantProblemsSeekingTreatmentIndicator ) && Equals ( other.SignificantProblemsSeekingTreatmentNote, SignificantProblemsSeekingTreatmentNote ) && other.CrimeViolenceScreenerPastMonthScore.Equals ( CrimeViolenceScreenerPastMonthScore ) && other.CrimeViolenceScreenerPastYearScore.Equals ( CrimeViolenceScreenerPastYearScore ) && other.CrimeViolenceScreenerLifetimeScore.Equals ( CrimeViolenceScreenerLifetimeScore );
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
            if ( obj.GetType () != typeof ( GainShortScreenerCrimeViolence ) )
            {
                return false;
            }
            return Equals ( ( GainShortScreenerCrimeViolence ) obj );
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
                int result = ( LastTimeYouHadDisagreementNumber.HasValue ? LastTimeYouHadDisagreementNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeYouTookSomethingNumber.HasValue ? LastTimeYouTookSomethingNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeYouSoldIllegalDrugsNumber.HasValue ? LastTimeYouSoldIllegalDrugsNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeYouDroveUnderTheInfluenceNumber.HasValue ? LastTimeYouDroveUnderTheInfluenceNumber.Value : 0 );
                result = ( result * 397 ) ^ ( LastTimeYouPurposelyDamagedPropertyNumber.HasValue ? LastTimeYouPurposelyDamagedPropertyNumber.Value : 0 );
                result = ( result * 397 ) ^ ( SignificantProblemsSeekingTreatmentIndicator.HasValue ? SignificantProblemsSeekingTreatmentIndicator.Value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( SignificantProblemsSeekingTreatmentNote != null ? SignificantProblemsSeekingTreatmentNote.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( CrimeViolenceScreenerPastMonthScore.HasValue ? CrimeViolenceScreenerPastMonthScore.Value : 0 );
                result = ( result * 397 ) ^ ( CrimeViolenceScreenerPastYearScore.HasValue ? CrimeViolenceScreenerPastYearScore.Value : 0 );
                result = ( result * 397 ) ^ ( CrimeViolenceScreenerLifetimeScore.HasValue ? CrimeViolenceScreenerLifetimeScore.Value : 0 );
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
        public static bool operator == ( GainShortScreenerCrimeViolence left, GainShortScreenerCrimeViolence right )
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
        public static bool operator != ( GainShortScreenerCrimeViolence left, GainShortScreenerCrimeViolence right )
        {
            return !Equals ( left, right );
        }
    }
}