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
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// Phq9 is an <see cref="Activity">Activity</see> in the form of a
    /// questionnaire, Patient Health Questionnaire, is a mulit-purpose instrument for screening, diagnosing, monitoring and 
    /// measuring the severity of depression.
    /// </summary>
    /// <remarks>
    /// The SBIRT Initiative is a 5-year national program funded by the Substance Abuse and Mental Health Services Administration (SAMHSA), 
    /// Center for Substance Abuse Treatment (CSAT) to implement screening, brief intervention, referral to treatment services for adults in 
    /// primary care and community health settings for substance misuse and substance abuse disorders.
    /// </remarks>
    public class Phq9 : Activity
    {
        #region Member Variables

        private int? _littleInterestInDoingThingsAnswerNumber;
        private int? _feelingDownAnswerNumber;
        private int? _troubleSleepingAnswerNumber;
        private int? _feelingTiredAnswerNumber;
        private int? _poorAppetiteAnswerNumber;
        private int? _feelingBadAboutSelfAnswerNumber;
        private int? _troubleConcentratingAnswerNumber;
        private int? _actingSluggishOrFidgityAnswerNumber;
        private int? _thoughtsOfHurtingSelfAnswerNumber;
        private int? _haveTheseProblemsAffectedYouAnswerNumber;
        private int? _severityScore;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Phq9"/> class.
        /// </summary>
        protected internal Phq9()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Phq9"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal Phq9(Visit visit, ActivityType activityType)
            : base(visit, activityType)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the little interest in doing things answer number.
        /// </summary>
        public virtual int? LittleInterestInDoingThingsAnswerNumber
        {
            get { return _littleInterestInDoingThingsAnswerNumber; }
            private set { ApplyPropertyChange(ref _littleInterestInDoingThingsAnswerNumber, () => LittleInterestInDoingThingsAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the feeling down answer number.
        /// </summary>
        public virtual int? FeelingDownAnswerNumber
        {
            get { return _feelingDownAnswerNumber; }
            private set { ApplyPropertyChange(ref _feelingDownAnswerNumber, () => FeelingDownAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the trouble sleeping answer number.
        /// </summary>
        public virtual int? TroubleSleepingAnswerNumber
        {
            get { return _troubleSleepingAnswerNumber; }
            private set { ApplyPropertyChange(ref _troubleSleepingAnswerNumber, () => TroubleSleepingAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the feeling tired answer number.
        /// </summary>
        public virtual int? FeelingTiredAnswerNumber
        {
            get { return _feelingTiredAnswerNumber; }
            private set { ApplyPropertyChange(ref _feelingTiredAnswerNumber, () => FeelingTiredAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the poor appetite answer number.
        /// </summary>
        public virtual int? PoorAppetiteAnswerNumber
        {
            get { return _poorAppetiteAnswerNumber; }
            private set { ApplyPropertyChange(ref _poorAppetiteAnswerNumber, () => PoorAppetiteAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the feeling bad about self answer number.
        /// </summary>
        public virtual int? FeelingBadAboutSelfAnswerNumber
        {
            get { return _feelingBadAboutSelfAnswerNumber; }
            private set { ApplyPropertyChange(ref _feelingBadAboutSelfAnswerNumber, () => FeelingBadAboutSelfAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the trouble concentrating answer number.
        /// </summary>
        public virtual int? TroubleConcentratingAnswerNumber
        {
            get { return _troubleConcentratingAnswerNumber; }
            private set { ApplyPropertyChange(ref _troubleConcentratingAnswerNumber, () => TroubleConcentratingAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the acting sluggish or fidgity answer number.
        /// </summary>
        public virtual int? ActingSluggishOrFidgityAnswerNumber
        {
            get { return _actingSluggishOrFidgityAnswerNumber; }
            private set { ApplyPropertyChange(ref _actingSluggishOrFidgityAnswerNumber, () => ActingSluggishOrFidgityAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the thoughts of hurting self answer number.
        /// </summary>
        public virtual int? ThoughtsOfHurtingSelfAnswerNumber
        {
            get { return _thoughtsOfHurtingSelfAnswerNumber; }
            private set { ApplyPropertyChange(ref _thoughtsOfHurtingSelfAnswerNumber, () => ThoughtsOfHurtingSelfAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the have these problems affected you answer number.
        /// </summary>
        public virtual int? HaveTheseProblemsAffectedYouAnswerNumber
        {
            get { return _haveTheseProblemsAffectedYouAnswerNumber; }
            private set { ApplyPropertyChange(ref _haveTheseProblemsAffectedYouAnswerNumber, () => HaveTheseProblemsAffectedYouAnswerNumber, value); }
        }

        /// <summary>
        /// Gets the severity score.
        /// </summary>
        public virtual int? SeverityScore
        {
            get { return _severityScore; }
            private set { }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Revises the question values and calculates.
        /// </summary>
        /// <param name="littleInterestInDoingThingsAnswerNumber">The little interest in doing things answer number.</param>
        /// <param name="feelingDownAnswerNumber">The feeling down answer number.</param>
        /// <param name="troubleSleepingAnswerNumber">The trouble sleeping answer number.</param>
        /// <param name="feelingTiredAnswerNumber">The feeling tired answer number.</param>
        /// <param name="poorAppetiteAnswerNumber">The poor appetite answer number.</param>
        /// <param name="feelingBadAboutSelfAnswerNumber">The feeling bad about self answer number.</param>
        /// <param name="troubleConcentratingAnswerNumber">The trouble concentrating answer number.</param>
        /// <param name="actingSluggishOrFidgityAnswerNumber">The acting sluggish or fidgity answer number.</param>
        /// <param name="thoughtsOfHurtingSelfAnswerNumber">The thoughts of hurting self answer number.</param>
        /// <param name="haveTheseProblemsAffectedYouAnswerNumber">The have these problems affected you answer number.</param>
        public virtual void ReviseAndCalculate(int? littleInterestInDoingThingsAnswerNumber,
            int? feelingDownAnswerNumber,
            int? troubleSleepingAnswerNumber,
            int? feelingTiredAnswerNumber,
            int? poorAppetiteAnswerNumber,
            int? feelingBadAboutSelfAnswerNumber,
            int? troubleConcentratingAnswerNumber,
            int? actingSluggishOrFidgityAnswerNumber,
            int? thoughtsOfHurtingSelfAnswerNumber,
            int? haveTheseProblemsAffectedYouAnswerNumber)
        {
            Check.IsInRange(littleInterestInDoingThingsAnswerNumber, 0, 3, () => LittleInterestInDoingThingsAnswerNumber);
            Check.IsInRange(feelingDownAnswerNumber, 0, 3, () => FeelingDownAnswerNumber);
            Check.IsInRange(troubleSleepingAnswerNumber, 0, 3, () => TroubleSleepingAnswerNumber);
            Check.IsInRange(feelingTiredAnswerNumber, 0, 3, () => FeelingTiredAnswerNumber);
            Check.IsInRange(poorAppetiteAnswerNumber, 0, 3, () => PoorAppetiteAnswerNumber);
            Check.IsInRange(feelingBadAboutSelfAnswerNumber, 0, 3, () => FeelingBadAboutSelfAnswerNumber);
            Check.IsInRange(troubleConcentratingAnswerNumber, 0, 3, () => TroubleConcentratingAnswerNumber);
            Check.IsInRange(actingSluggishOrFidgityAnswerNumber, 0, 3, () => ActingSluggishOrFidgityAnswerNumber);
            Check.IsInRange(thoughtsOfHurtingSelfAnswerNumber, 0, 3, () => ThoughtsOfHurtingSelfAnswerNumber);
            Check.IsInRange(haveTheseProblemsAffectedYouAnswerNumber, 0, 3, () => HaveTheseProblemsAffectedYouAnswerNumber);

            LittleInterestInDoingThingsAnswerNumber = littleInterestInDoingThingsAnswerNumber;
            FeelingDownAnswerNumber = feelingDownAnswerNumber;
            TroubleSleepingAnswerNumber = troubleSleepingAnswerNumber;
            FeelingTiredAnswerNumber = feelingTiredAnswerNumber;
            PoorAppetiteAnswerNumber = poorAppetiteAnswerNumber;
            FeelingBadAboutSelfAnswerNumber = feelingBadAboutSelfAnswerNumber;
            TroubleConcentratingAnswerNumber = troubleConcentratingAnswerNumber;
            ActingSluggishOrFidgityAnswerNumber = actingSluggishOrFidgityAnswerNumber;
            ThoughtsOfHurtingSelfAnswerNumber = thoughtsOfHurtingSelfAnswerNumber;
            HaveTheseProblemsAffectedYouAnswerNumber = haveTheseProblemsAffectedYouAnswerNumber;

            _severityScore = LittleInterestInDoingThingsAnswerNumber.GetValueOrDefault()
                + FeelingDownAnswerNumber.GetValueOrDefault()
                + TroubleSleepingAnswerNumber.GetValueOrDefault()
                + FeelingTiredAnswerNumber.GetValueOrDefault()
                + PoorAppetiteAnswerNumber.GetValueOrDefault()
                + FeelingBadAboutSelfAnswerNumber.GetValueOrDefault()
                + TroubleConcentratingAnswerNumber.GetValueOrDefault()
                + ActingSluggishOrFidgityAnswerNumber.GetValueOrDefault()
                + ThoughtsOfHurtingSelfAnswerNumber.GetValueOrDefault();
        }

        #endregion
    }
}
