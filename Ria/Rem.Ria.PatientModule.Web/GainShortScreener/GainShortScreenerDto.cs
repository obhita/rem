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

using System.Runtime.Serialization;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.GainShortScreener
{
    /// <summary>
    /// Data transfer object for GainShortScreener class.
    /// </summary>
    public class GainShortScreenerDto : ActivityDto
    {
        #region Constants and Fields

        private int? _crimeViolenceScreenerLifetimeScore;
        private int? _crimeViolenceScreenerPastMonthScore;
        private int? _crimeViolenceScreenerPastYearScore;
        private int? _externalizingDisorderScreenerLifetimeScore;
        private int? _externalizingDisorderScreenerPastMonthScore;
        private int? _externalizingDisorderScreenerPastYearScore;
        private int? _internalizingDisorderScreenerLifetimeScore;
        private int? _internalizingDisorderScreenerPastMonthScore;
        private int? _internalizingDisorderScreenerPastYearScore;
        private int? _lastTimeHadWithdrawProblemsNumber;
        private int? _lastTimeKeptUsingAlcoholNumber;
        private int? _lastTimeSpentALotOfTimeGettingAlcoholNumber;
        private int? _lastTimeUseAlcoholCauseYouToGiveUpNumber;
        private int? _lastTimeUsedAlcoholDrugsNumber;
        private int? _lastTimeYouDroveUnderTheInfluenceNumber;

        private int? _lastTimeYouHadDisagreementNumber;
        private int? _lastTimeYouPurposelyDamagedPropertyNumber;
        private int? _lastTimeYouSoldIllegalDrugsNumber;
        private int? _lastTimeYouTookSomethingNumber;
        private int? _problemBecomingDistressedNumber;
        private int? _problemCommittingSuicideNumber;
        private int? _problemFeelingAnxiousNumber;
        private int? _problemFeelingDepressedNumber;
        private int? _problemSleepingNumber;
        private bool? _significantProblemsSeekingTreatmentIndicator;
        private string _significantProblemsSeekingTreatmentNote;
        private int? _substanceDisorderScreenerLifetimeScore;
        private int? _substanceDisorderScreenerPastMonthScore;
        private int? _substanceDisorderScreenerPastYearScore;
        private int? _totalScreenerLifetimeScore;

        private int? _totalScreenerPastMonthScore;
        private int? _totalScreenerPastYearScore;
        private int? _twoOrMoreHardTimeListeningNumber;
        private int? _twoOrMoreHardTimePayingAttentionNumber;
        private int? _twoOrMoreLiedNumber;
        private int? _twoOrMoreStartedFightNumber;
        private int? _twoOrMoreThreatenedOthersNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the crime violence screener lifetime score.
        /// </summary>
        /// <value>The crime violence screener lifetime score.</value>
        [DataMember]
        public virtual int? CrimeViolenceScreenerLifetimeScore
        {
            get { return _crimeViolenceScreenerLifetimeScore; }
            set { ApplyPropertyChange ( ref _crimeViolenceScreenerLifetimeScore, () => CrimeViolenceScreenerLifetimeScore, value ); }
        }

        /// <summary>
        /// Gets or sets the crime violence screener past month score.
        /// </summary>
        /// <value>The crime violence screener past month score.</value>
        [DataMember]
        public virtual int? CrimeViolenceScreenerPastMonthScore
        {
            get { return _crimeViolenceScreenerPastMonthScore; }
            set { ApplyPropertyChange ( ref _crimeViolenceScreenerPastMonthScore, () => CrimeViolenceScreenerPastMonthScore, value ); }
        }

        /// <summary>
        /// Gets or sets the crime violence screener past year score.
        /// </summary>
        /// <value>The crime violence screener past year score.</value>
        [DataMember]
        public virtual int? CrimeViolenceScreenerPastYearScore
        {
            get { return _crimeViolenceScreenerPastYearScore; }
            set { ApplyPropertyChange ( ref _crimeViolenceScreenerPastYearScore, () => CrimeViolenceScreenerPastYearScore, value ); }
        }

        /// <summary>
        /// Gets or sets the externalizing disorder screener lifetime score.
        /// </summary>
        /// <value>The externalizing disorder screener lifetime score.</value>
        [DataMember]
        public virtual int? ExternalizingDisorderScreenerLifetimeScore
        {
            get { return _externalizingDisorderScreenerLifetimeScore; }
            set { ApplyPropertyChange ( ref _externalizingDisorderScreenerLifetimeScore, () => ExternalizingDisorderScreenerLifetimeScore, value ); }
        }

        /// <summary>
        /// Gets or sets the externalizing disorder screener past month score.
        /// </summary>
        /// <value>The externalizing disorder screener past month score.</value>
        [DataMember]
        public virtual int? ExternalizingDisorderScreenerPastMonthScore
        {
            get { return _externalizingDisorderScreenerPastMonthScore; }
            set { ApplyPropertyChange ( ref _externalizingDisorderScreenerPastMonthScore, () => ExternalizingDisorderScreenerPastMonthScore, value ); }
        }

        /// <summary>
        /// Gets or sets the externalizing disorder screener past year score.
        /// </summary>
        /// <value>The externalizing disorder screener past year score.</value>
        [DataMember]
        public virtual int? ExternalizingDisorderScreenerPastYearScore
        {
            get { return _externalizingDisorderScreenerPastYearScore; }
            set { ApplyPropertyChange ( ref _externalizingDisorderScreenerPastYearScore, () => ExternalizingDisorderScreenerPastYearScore, value ); }
        }

        /// <summary>
        /// Gets or sets the internalizing disorder screener lifetime score.
        /// </summary>
        /// <value>The internalizing disorder screener lifetime score.</value>
        [DataMember]
        public virtual int? InternalizingDisorderScreenerLifetimeScore
        {
            get { return _internalizingDisorderScreenerLifetimeScore; }
            set { ApplyPropertyChange ( ref _internalizingDisorderScreenerLifetimeScore, () => InternalizingDisorderScreenerLifetimeScore, value ); }
        }

        /// <summary>
        /// Gets or sets the internalizing disorder screener past month score.
        /// </summary>
        /// <value>The internalizing disorder screener past month score.</value>
        [DataMember]
        public virtual int? InternalizingDisorderScreenerPastMonthScore
        {
            get { return _internalizingDisorderScreenerPastMonthScore; }
            set { ApplyPropertyChange ( ref _internalizingDisorderScreenerPastMonthScore, () => InternalizingDisorderScreenerPastMonthScore, value ); }
        }

        /// <summary>
        /// Gets or sets the internalizing disorder screener past year score.
        /// </summary>
        /// <value>The internalizing disorder screener past year score.</value>
        [DataMember]
        public virtual int? InternalizingDisorderScreenerPastYearScore
        {
            get { return _internalizingDisorderScreenerPastYearScore; }
            set { ApplyPropertyChange ( ref _internalizingDisorderScreenerPastYearScore, () => InternalizingDisorderScreenerPastYearScore, value ); }
        }

        /// <summary>
        /// Gets or sets the last time had withdraw problems number.
        /// </summary>
        /// <value>The last time had withdraw problems number.</value>
        [DataMember]
        public virtual int? LastTimeHadWithdrawProblemsNumber
        {
            get { return _lastTimeHadWithdrawProblemsNumber; }
            set { ApplyPropertyChange ( ref _lastTimeHadWithdrawProblemsNumber, () => LastTimeHadWithdrawProblemsNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time kept using alcohol number.
        /// 3.c
        /// </summary>
        /// <value>The last time kept using alcohol number.</value>
        [DataMember]
        public virtual int? LastTimeKeptUsingAlcoholNumber
        {
            get { return _lastTimeKeptUsingAlcoholNumber; }
            set { ApplyPropertyChange ( ref _lastTimeKeptUsingAlcoholNumber, () => LastTimeKeptUsingAlcoholNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time spent A lot of time getting alcohol number.
        /// </summary>
        /// <value>The last time spent A lot of time getting alcohol number.</value>
        [DataMember]
        public virtual int? LastTimeSpentALotOfTimeGettingAlcoholNumber
        {
            get { return _lastTimeSpentALotOfTimeGettingAlcoholNumber; }
            set { ApplyPropertyChange ( ref _lastTimeSpentALotOfTimeGettingAlcoholNumber, () => LastTimeSpentALotOfTimeGettingAlcoholNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time use alcohol cause you to give up number.
        /// 3.d
        /// </summary>
        /// <value>The last time use alcohol cause you to give up number.</value>
        [DataMember]
        public virtual int? LastTimeUseAlcoholCauseYouToGiveUpNumber
        {
            get { return _lastTimeUseAlcoholCauseYouToGiveUpNumber; }
            set { ApplyPropertyChange ( ref _lastTimeUseAlcoholCauseYouToGiveUpNumber, () => LastTimeUseAlcoholCauseYouToGiveUpNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time used alcohol drugs number.
        /// </summary>
        /// <value>The last time used alcohol drugs number.</value>
        [DataMember]
        public virtual int? LastTimeUsedAlcoholDrugsNumber
        {
            get { return _lastTimeUsedAlcoholDrugsNumber; }
            set { ApplyPropertyChange ( ref _lastTimeUsedAlcoholDrugsNumber, () => LastTimeUsedAlcoholDrugsNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time you drove under the influence number.
        /// 4.d
        /// </summary>
        /// <value>The last time you drove under the influence number.</value>
        [DataMember]
        public virtual int? LastTimeYouDroveUnderTheInfluenceNumber
        {
            get { return _lastTimeYouDroveUnderTheInfluenceNumber; }
            set { ApplyPropertyChange ( ref _lastTimeYouDroveUnderTheInfluenceNumber, () => LastTimeYouDroveUnderTheInfluenceNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time you had disagreement number.
        /// </summary>
        /// <value>The last time you had disagreement number.</value>
        [DataMember]
        public virtual int? LastTimeYouHadDisagreementNumber
        {
            get { return _lastTimeYouHadDisagreementNumber; }
            set { ApplyPropertyChange ( ref _lastTimeYouHadDisagreementNumber, () => LastTimeYouHadDisagreementNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time you purposely damaged property number.
        /// 4.e
        /// </summary>
        /// <value>The last time you purposely damaged property number.</value>
        [DataMember]
        public virtual int? LastTimeYouPurposelyDamagedPropertyNumber
        {
            get { return _lastTimeYouPurposelyDamagedPropertyNumber; }
            set { ApplyPropertyChange ( ref _lastTimeYouPurposelyDamagedPropertyNumber, () => LastTimeYouPurposelyDamagedPropertyNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time you sold illegal drugs number.
        /// </summary>
        /// <value>The last time you sold illegal drugs number.</value>
        [DataMember]
        public virtual int? LastTimeYouSoldIllegalDrugsNumber
        {
            get { return _lastTimeYouSoldIllegalDrugsNumber; }
            set { ApplyPropertyChange ( ref _lastTimeYouSoldIllegalDrugsNumber, () => LastTimeYouSoldIllegalDrugsNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the last time you took something number.
        /// </summary>
        /// <value>The last time you took something number.</value>
        [DataMember]
        public virtual int? LastTimeYouTookSomethingNumber
        {
            get { return _lastTimeYouTookSomethingNumber; }
            set { ApplyPropertyChange ( ref _lastTimeYouTookSomethingNumber, () => LastTimeYouTookSomethingNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the problem becoming distressed number.
        /// </summary>
        /// <value>The problem becoming distressed number.</value>
        [DataMember]
        public virtual int? ProblemBecomingDistressedNumber
        {
            get { return _problemBecomingDistressedNumber; }
            set { ApplyPropertyChange ( ref _problemBecomingDistressedNumber, () => ProblemBecomingDistressedNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the problem committing suicide number.
        /// 1.e
        /// </summary>
        /// <value>The problem committing suicide number.</value>
        [DataMember]
        public virtual int? ProblemCommittingSuicideNumber
        {
            get { return _problemCommittingSuicideNumber; }
            set { ApplyPropertyChange ( ref _problemCommittingSuicideNumber, () => ProblemCommittingSuicideNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the problem feeling anxious number.
        /// </summary>
        /// <value>The problem feeling anxious number.</value>
        [DataMember]
        public virtual int? ProblemFeelingAnxiousNumber
        {
            get { return _problemFeelingAnxiousNumber; }
            set { ApplyPropertyChange ( ref _problemFeelingAnxiousNumber, () => ProblemFeelingAnxiousNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the problem feeling depressed number.
        /// </summary>
        /// <value>The problem feeling depressed number.</value>
        [DataMember]
        public virtual int? ProblemFeelingDepressedNumber
        {
            get { return _problemFeelingDepressedNumber; }
            set { ApplyPropertyChange ( ref _problemFeelingDepressedNumber, () => ProblemFeelingDepressedNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the problem sleeping number.
        /// 1.b
        /// </summary>
        /// <value>The problem sleeping number.</value>
        [DataMember]
        public virtual int? ProblemSleepingNumber
        {
            get { return _problemSleepingNumber; }
            set { ApplyPropertyChange ( ref _problemSleepingNumber, () => ProblemSleepingNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the significant problems seeking treatment indicator.
        /// 5.
        /// </summary>
        /// <value>The significant problems seeking treatment indicator.</value>
        [DataMember]
        public virtual bool? SignificantProblemsSeekingTreatmentIndicator
        {
            get { return _significantProblemsSeekingTreatmentIndicator; }
            set { ApplyPropertyChange ( ref _significantProblemsSeekingTreatmentIndicator, () => SignificantProblemsSeekingTreatmentIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the significant problems seeking treatment note.
        /// 5.v
        /// </summary>
        /// <value>The significant problems seeking treatment note.</value>
        [DataMember]
        public virtual string SignificantProblemsSeekingTreatmentNote
        {
            get { return _significantProblemsSeekingTreatmentNote; }
            set { ApplyPropertyChange ( ref _significantProblemsSeekingTreatmentNote, () => SignificantProblemsSeekingTreatmentNote, value ); }
        }

        /// <summary>
        /// Gets or sets the substance disorder screener lifetime score.
        /// </summary>
        /// <value>The substance disorder screener lifetime score.</value>
        [DataMember]
        public virtual int? SubstanceDisorderScreenerLifetimeScore
        {
            get { return _substanceDisorderScreenerLifetimeScore; }
            set { ApplyPropertyChange ( ref _substanceDisorderScreenerLifetimeScore, () => SubstanceDisorderScreenerLifetimeScore, value ); }
        }

        /// <summary>
        /// Gets or sets the substance disorder screener past month score.
        /// </summary>
        /// <value>The substance disorder screener past month score.</value>
        [DataMember]
        public virtual int? SubstanceDisorderScreenerPastMonthScore
        {
            get { return _substanceDisorderScreenerPastMonthScore; }
            set { ApplyPropertyChange ( ref _substanceDisorderScreenerPastMonthScore, () => SubstanceDisorderScreenerPastMonthScore, value ); }
        }

        /// <summary>
        /// Gets or sets the substance disorder screener past year score.
        /// </summary>
        /// <value>The substance disorder screener past year score.</value>
        [DataMember]
        public virtual int? SubstanceDisorderScreenerPastYearScore
        {
            get { return _substanceDisorderScreenerPastYearScore; }
            set { ApplyPropertyChange ( ref _substanceDisorderScreenerPastYearScore, () => SubstanceDisorderScreenerPastYearScore, value ); }
        }

        /// <summary>
        /// Gets or sets the total screener lifetime score.
        /// </summary>
        /// <value>The total screener lifetime score.</value>
        [DataMember]
        public int? TotalScreenerLifetimeScore
        {
            get { return _totalScreenerLifetimeScore; }
            set { ApplyPropertyChange ( ref _totalScreenerLifetimeScore, () => TotalScreenerLifetimeScore, value ); }
        }

        //// 6.
        //[DataMember]
        //public virtual LookupValueDto GainShortScreenerCrimeViolencePatientGender
        //{
        //    get { return _gainShortScreenerCrimeViolencePatientGender; }
        //    set { ApplyPropertyChange(ref _gainShortScreenerCrimeViolencePatientGender, () => GainShortScreenerCrimeViolencePatientGender, value); }
        //}

        //// 6.v
        //[DataMember]
        //public virtual string OtherPatientGenderNote
        //{
        //    get { return _otherPatientGenderNote; }
        //    set { ApplyPropertyChange(ref _otherPatientGenderNote, () => OtherPatientGenderNote, value); }
        //}

        //// 7.
        //[DataMember]
        //public virtual int? GainPatientAgeNumber
        //{
        //    get { return _gainPatientAgeNumber; }
        //    set { ApplyPropertyChange(ref _gainPatientAgeNumber, () => GainPatientAgeNumber, value); }
        //}

        /// <summary>
        /// Gets or sets the total screener past month score.
        /// </summary>
        /// <value>The total screener past month score.</value>
        [DataMember]
        public int? TotalScreenerPastMonthScore
        {
            get { return _totalScreenerPastMonthScore; }
            set { ApplyPropertyChange ( ref _totalScreenerPastMonthScore, () => TotalScreenerPastMonthScore, value ); }
        }

        /// <summary>
        /// Gets or sets the total screener past year score.
        /// </summary>
        /// <value>The total screener past year score.</value>
        [DataMember]
        public int? TotalScreenerPastYearScore
        {
            get { return _totalScreenerPastYearScore; }
            set { ApplyPropertyChange ( ref _totalScreenerPastYearScore, () => TotalScreenerPastYearScore, value ); }
        }

        /// <summary>
        /// Gets or sets the two or more hard time listening number.
        /// </summary>
        /// <value>The two or more hard time listening number.</value>
        [DataMember]
        public virtual int? TwoOrMoreHardTimeListeningNumber
        {
            get { return _twoOrMoreHardTimeListeningNumber; }
            set { ApplyPropertyChange ( ref _twoOrMoreHardTimeListeningNumber, () => TwoOrMoreHardTimeListeningNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the two or more hard time paying attention number.
        /// </summary>
        /// <value>The two or more hard time paying attention number.</value>
        [DataMember]
        public virtual int? TwoOrMoreHardTimePayingAttentionNumber
        {
            get { return _twoOrMoreHardTimePayingAttentionNumber; }
            set { ApplyPropertyChange ( ref _twoOrMoreHardTimePayingAttentionNumber, () => TwoOrMoreHardTimePayingAttentionNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the two or more lied number.
        /// </summary>
        /// <value>The two or more lied number.</value>
        [DataMember]
        public virtual int? TwoOrMoreLiedNumber
        {
            get { return _twoOrMoreLiedNumber; }
            set { ApplyPropertyChange ( ref _twoOrMoreLiedNumber, () => TwoOrMoreLiedNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the two or more started fight number.
        /// </summary>
        /// <value>The two or more started fight number.</value>
        [DataMember]
        public virtual int? TwoOrMoreStartedFightNumber
        {
            get { return _twoOrMoreStartedFightNumber; }
            set { ApplyPropertyChange ( ref _twoOrMoreStartedFightNumber, () => TwoOrMoreStartedFightNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the two or more threatened others number.
        /// </summary>
        /// <value>The two or more threatened others number.</value>
        [DataMember]
        public virtual int? TwoOrMoreThreatenedOthersNumber
        {
            get { return _twoOrMoreThreatenedOthersNumber; }
            set { ApplyPropertyChange ( ref _twoOrMoreThreatenedOthersNumber, () => TwoOrMoreThreatenedOthersNumber, value ); }
        }

        #endregion
    }
}
