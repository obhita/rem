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

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for Audit class.
    /// </summary>
    public class AuditDto : ActivityDto
    {
        #region Constants and Fields

        private int? _alcoholicDrinksPerDayNumber;
        private int? _auditScore;
        private int? _healthWorkerSuggestedToCutDownDrinkingNumber;
        private int? _howOftenYouDrinkNumber;
        private int? _howOftenYouHaveSixOrMoreDrinksNumber;
        private int? _pastYearHowOftenYouDrinkInMorningNumber;
        private int? _pastYearHowOftenYouFailedNormalExpectationNumber;
        private int? _pastYearHowOftenYouForgotNightBeforeNumber;
        private int? _pastYearHowOftenYouHadGuiltAfterDrinkingNumber;
        private int? _pastYearHowOftenYouWereUnableToStopDrinkingNumber;
        private int? _youOrSomeoneInjuredDueToYourDrinkingNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: 2. How many standard drinks containing alcohol do you have on a typical day when drinking?
        /// </summary>
        /// <value>The alcoholic drinks per day number.</value>
        [DataMember]
        public int? AlcoholicDrinksPerDayNumber
        {
            get { return _alcoholicDrinksPerDayNumber; }
            set { ApplyPropertyChange ( ref _alcoholicDrinksPerDayNumber, () => AlcoholicDrinksPerDayNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the audit score.
        /// </summary>
        /// <value>The audit score.</value>
        public int? AuditScore
        {
            get { return _auditScore; }
            set { ApplyPropertyChange ( ref _auditScore, () => AuditScore, value ); }
        }

        /// <summary>
        /// Question Number: 10. Has a relative or friend, doctor or other health worker been concerned about your drinking or suggested you cut down?
        /// </summary>
        /// <value>The health worker suggested to cut down drinking number.</value>
        [DataMember]
        public int? HealthWorkerSuggestedToCutDownDrinkingNumber
        {
            get { return _healthWorkerSuggestedToCutDownDrinkingNumber; }
            set { ApplyPropertyChange ( ref _healthWorkerSuggestedToCutDownDrinkingNumber, () => HealthWorkerSuggestedToCutDownDrinkingNumber, value ); }
        }

        /// <summary>
        /// Question Number: 1. How often do you have a drink containing alcohol?
        /// </summary>
        /// <value>The how often you drink number.</value>
        [DataMember]
        public int? HowOftenYouDrinkNumber
        {
            get { return _howOftenYouDrinkNumber; }
            set { ApplyPropertyChange ( ref _howOftenYouDrinkNumber, () => HowOftenYouDrinkNumber, value ); }
        }

        /// <summary>
        /// Question Number: 3. How often do you have six or more drinks on one occasion?
        /// </summary>
        /// <value>The how often you have six or more drinks number.</value>
        [DataMember]
        public int? HowOftenYouHaveSixOrMoreDrinksNumber
        {
            get { return _howOftenYouHaveSixOrMoreDrinksNumber; }
            set { ApplyPropertyChange ( ref _howOftenYouHaveSixOrMoreDrinksNumber, () => HowOftenYouHaveSixOrMoreDrinksNumber, value ); }
        }

        /// <summary>
        /// Question Number: 6. During the past year, how often have you needed a drink in the morning to get yourself going after a heavy drinking session?
        /// </summary>
        /// <value>The past year how often you drink in morning number.</value>
        [DataMember]
        public int? PastYearHowOftenYouDrinkInMorningNumber
        {
            get { return _pastYearHowOftenYouDrinkInMorningNumber; }
            set { ApplyPropertyChange ( ref _pastYearHowOftenYouDrinkInMorningNumber, () => PastYearHowOftenYouDrinkInMorningNumber, value ); }
        }

        /// <summary>
        /// Question Number: 5. During the past year, how often have you failed to do what was normally expected of you because of drinking?
        /// </summary>
        /// <value>The past year how often you failed normal expectation number.</value>
        [DataMember]
        public int? PastYearHowOftenYouFailedNormalExpectationNumber
        {
            get { return _pastYearHowOftenYouFailedNormalExpectationNumber; }
            set
            {
                ApplyPropertyChange (
                    ref _pastYearHowOftenYouFailedNormalExpectationNumber, () => PastYearHowOftenYouFailedNormalExpectationNumber, value );
            }
        }

        /// <summary>
        /// Question Number: 8. During the past year, have you been unable to remember what happened the night before because you had been drinking?
        /// </summary>
        /// <value>The past year how often you forgot night before number.</value>
        [DataMember]
        public int? PastYearHowOftenYouForgotNightBeforeNumber
        {
            get { return _pastYearHowOftenYouForgotNightBeforeNumber; }
            set { ApplyPropertyChange ( ref _pastYearHowOftenYouForgotNightBeforeNumber, () => PastYearHowOftenYouForgotNightBeforeNumber, value ); }
        }

        /// <summary>
        /// Question Number: 7. During the past year, how often have you had a feeling of guilt or remorse after drinking?
        /// </summary>
        /// <value>The past year how often you had guilt after drinking number.</value>
        [DataMember]
        public int? PastYearHowOftenYouHadGuiltAfterDrinkingNumber
        {
            get { return _pastYearHowOftenYouHadGuiltAfterDrinkingNumber; }
            set
            {
                ApplyPropertyChange (
                    ref _pastYearHowOftenYouHadGuiltAfterDrinkingNumber, () => PastYearHowOftenYouHadGuiltAfterDrinkingNumber, value );
            }
        }

        /// <summary>
        /// Question Number: 4. During the past year, how often have you found that you were not able to stop drinking once you had started?
        /// </summary>
        /// <value>The past year how often you were unable to stop drinking number.</value>
        [DataMember]
        public int? PastYearHowOftenYouWereUnableToStopDrinkingNumber
        {
            get { return _pastYearHowOftenYouWereUnableToStopDrinkingNumber; }
            set
            {
                ApplyPropertyChange (
                    ref _pastYearHowOftenYouWereUnableToStopDrinkingNumber, () => PastYearHowOftenYouWereUnableToStopDrinkingNumber, value );
            }
        }

        /// <summary>
        /// Question Number: 9. Have you or someone else been injured as a result of your drinking?
        /// </summary>
        /// <value>You or someone injured due to your drinking number.</value>
        [DataMember]
        public int? YouOrSomeoneInjuredDueToYourDrinkingNumber
        {
            get { return _youOrSomeoneInjuredDueToYourDrinkingNumber; }
            set { ApplyPropertyChange ( ref _youOrSomeoneInjuredDueToYourDrinkingNumber, () => YouOrSomeoneInjuredDueToYourDrinkingNumber, value ); }
        }

        #endregion
    }
}
