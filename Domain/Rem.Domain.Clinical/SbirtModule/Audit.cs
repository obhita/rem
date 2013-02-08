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
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// Audit is an <see cref="Activity">Activity</see> in the form of a
    /// questionnaire, Alcohol Use Disorder Identification Test, used for assessment of alcohol abuse
    /// <see cref="Patient">Patient</see> in a given <see
    /// cref="Visit">Visit</see>.
    /// </summary>
    /// <remarks>
    /// The SBIRT Initiative is a 5-year national program funded by the Substance Abuse and Mental Health Services Administration (SAMHSA), 
    /// Center for Substance Abuse Treatment (CSAT) to implement screening, brief intervention, referral to treatment services for adults in 
    /// primary care and community health settings for substance misuse and substance abuse disorders.
    /// </remarks>
    public class Audit : Activity
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Audit"/> class.
        /// </summary>
        protected internal Audit ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Audit"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        public Audit ( Visit visit, ActivityType activityType )
            : base ( visit, activityType )
        {
        }

        /// <summary>
        /// Gets the how often you drink number.
        /// Question Number: 1. How often do you have a drink containing alcohol?
        /// </summary>
        public virtual int? HowOftenYouDrinkNumber
        {
            get { return _howOftenYouDrinkNumber; }
            private set { ApplyPropertyChange ( ref _howOftenYouDrinkNumber, () => HowOftenYouDrinkNumber, value ); }
        }

        /// <summary>
        /// Gets the alcoholic drinks per day number.
        /// Question Number: 2. How many standard drinks containing alcohol do you have on a typical day when drinking?
        /// </summary>
        public virtual int? AlcoholicDrinksPerDayNumber
        {
            get { return _alcoholicDrinksPerDayNumber; }
            private set { ApplyPropertyChange ( ref _alcoholicDrinksPerDayNumber, () => AlcoholicDrinksPerDayNumber, value ); }
        }

        /// <summary>
        /// Gets the how often you have six or more drinks number.
        /// Question Number: 3. How often do you have six or more drinks on one occasion?
        /// </summary>
        public virtual int? HowOftenYouHaveSixOrMoreDrinksNumber
        {
            get { return _howOftenYouHaveSixOrMoreDrinksNumber; }
            private set { ApplyPropertyChange ( ref _howOftenYouHaveSixOrMoreDrinksNumber, () => HowOftenYouHaveSixOrMoreDrinksNumber, value ); }
        }

        /// <summary>
        /// Gets the past year how often you were unable to stop drinking number.
        /// Question Number: 4. During the past year, how often have you found that you were not able to stop drinking once you had started?
        /// </summary>
        public virtual int? PastYearHowOftenYouWereUnableToStopDrinkingNumber
        {
            get { return _pastYearHowOftenYouWereUnableToStopDrinkingNumber; }
            private set { ApplyPropertyChange ( ref _pastYearHowOftenYouWereUnableToStopDrinkingNumber, () => PastYearHowOftenYouWereUnableToStopDrinkingNumber, value ); }
        }

        /// <summary>
        /// Gets the past year how often you failed normal expectation number.
        /// Question Number: 5. During the past year, how often have you failed to do what was normally expected of you because of drinking?
        /// </summary>
        public virtual int? PastYearHowOftenYouFailedNormalExpectationNumber
        {
            get { return _pastYearHowOftenYouFailedNormalExpectationNumber; }
            private set { ApplyPropertyChange ( ref _pastYearHowOftenYouFailedNormalExpectationNumber, () => PastYearHowOftenYouFailedNormalExpectationNumber, value ); }
        }

        /// <summary>
        /// Gets the past year how often you drink in morning number.
        /// Question Number: 6. During the past year, how often have you needed a drink in the morning to get yourself going after a heavy drinking session?
        /// </summary>
        public virtual int? PastYearHowOftenYouDrinkInMorningNumber
        {
            get { return _pastYearHowOftenYouDrinkInMorningNumber; }
            private set { ApplyPropertyChange ( ref _pastYearHowOftenYouDrinkInMorningNumber, () => PastYearHowOftenYouDrinkInMorningNumber, value ); }
        }

        /// <summary>
        /// Gets the past year how often you had guilt after drinking number.
        /// Question Number: 7. During the past year, how often have you had a feeling of guilt or remorse after drinking?
        /// </summary>
        public virtual int? PastYearHowOftenYouHadGuiltAfterDrinkingNumber
        {
            get { return _pastYearHowOftenYouHadGuiltAfterDrinkingNumber; }
            private set { ApplyPropertyChange ( ref _pastYearHowOftenYouHadGuiltAfterDrinkingNumber, () => PastYearHowOftenYouHadGuiltAfterDrinkingNumber, value ); }
        }

        /// <summary>
        /// Gets the past year how often you forgot night before number.
        /// Question Number: 8. During the past year, have you been unable to remember what happened the night before because you had been drinking?
        /// </summary>
        public virtual int? PastYearHowOftenYouForgotNightBeforeNumber
        {
            get { return _pastYearHowOftenYouForgotNightBeforeNumber; }
            private set { ApplyPropertyChange ( ref _pastYearHowOftenYouForgotNightBeforeNumber, () => PastYearHowOftenYouForgotNightBeforeNumber, value ); }
        }

        /// <summary>
        /// Gets you or someone injured due to your drinking number.
        /// Question Number: 9. Have you or someone else been injured as a result of your drinking?
        /// </summary>
        public virtual int? YouOrSomeoneInjuredDueToYourDrinkingNumber
        {
            get { return _youOrSomeoneInjuredDueToYourDrinkingNumber; }
            private set { ApplyPropertyChange ( ref _youOrSomeoneInjuredDueToYourDrinkingNumber, () => YouOrSomeoneInjuredDueToYourDrinkingNumber, value ); }
        }

        /// <summary>
        /// Gets the health worker suggested to cut down drinking number.
        /// Question Number: 10. Has a relative or friend, doctor or other health worker been concerned about your drinking or suggested you cut down?
        /// </summary>
        public virtual int? HealthWorkerSuggestedToCutDownDrinkingNumber
        {
            get { return _healthWorkerSuggestedToCutDownDrinkingNumber; }
            private set { ApplyPropertyChange ( ref _healthWorkerSuggestedToCutDownDrinkingNumber, () => HealthWorkerSuggestedToCutDownDrinkingNumber, value ); }
        }

        /// <summary>
        /// Gets the audit score.
        /// </summary>
        public virtual int? AuditScore
        {
            get { return _auditScore; }
            private set { }
        }

        #region Methods

        /// <summary>
        /// Revises the and calculate.
        /// </summary>
        /// <param name="howOftenYouDrinkNumber">The how often you drink number.</param>
        /// <param name="alcoholicDrinksPerDayNumber">The alcoholic drinks per day number.</param>
        /// <param name="howOftenYouHaveSixOrMoreDrinksNumber">The how often you have six or more drinks number.</param>
        /// <param name="pastYearHowOftenYouWereUnableToStopDrinkingNumber">The past year how often you were unable to stop drinking number.</param>
        /// <param name="pastYearHowOftenYouFailedNormalExpectationNumber">The past year how often you failed normal expectation number.</param>
        /// <param name="pastYearHowOftenYouDrinkInMorningNumber">The past year how often you drink in morning number.</param>
        /// <param name="pastYearHowOftenYouHadGuiltAfterDrinkingNumber">The past year how often you had guilt after drinking number.</param>
        /// <param name="pastYearHowOftenYouForgotNightBeforeNumber">The past year how often you forgot night before number.</param>
        /// <param name="youOrSomeoneInjuredDueToYourDrinkingNumber">You or someone injured due to your drinking number.</param>
        /// <param name="healthWorkerSuggestedToCutDownDrinkingNumber">The health worker suggested to cut down drinking number.</param>
        public virtual void ReviseAndCalculate ( int? howOftenYouDrinkNumber,
                                             int? alcoholicDrinksPerDayNumber,
                                             int? howOftenYouHaveSixOrMoreDrinksNumber,
                                             int? pastYearHowOftenYouWereUnableToStopDrinkingNumber,
                                             int? pastYearHowOftenYouFailedNormalExpectationNumber,
                                             int? pastYearHowOftenYouDrinkInMorningNumber,
                                             int? pastYearHowOftenYouHadGuiltAfterDrinkingNumber,
                                             int? pastYearHowOftenYouForgotNightBeforeNumber,
                                             int? youOrSomeoneInjuredDueToYourDrinkingNumber,
                                             int? healthWorkerSuggestedToCutDownDrinkingNumber )
        {
            Check.IsInRange ( howOftenYouDrinkNumber, 0, 4, () => HowOftenYouDrinkNumber );
            Check.IsInRange ( alcoholicDrinksPerDayNumber, 0, 4, () => AlcoholicDrinksPerDayNumber );
            Check.IsInRange ( howOftenYouHaveSixOrMoreDrinksNumber, 0, 4, () => HowOftenYouHaveSixOrMoreDrinksNumber );
            Check.IsInRange ( pastYearHowOftenYouWereUnableToStopDrinkingNumber, 0, 4, () => PastYearHowOftenYouWereUnableToStopDrinkingNumber );
            Check.IsInRange ( pastYearHowOftenYouFailedNormalExpectationNumber, 0, 4, () => PastYearHowOftenYouFailedNormalExpectationNumber );
            Check.IsInRange ( pastYearHowOftenYouDrinkInMorningNumber, 0, 4, () => PastYearHowOftenYouDrinkInMorningNumber );
            Check.IsInRange ( pastYearHowOftenYouHadGuiltAfterDrinkingNumber, 0, 4, () => PastYearHowOftenYouHadGuiltAfterDrinkingNumber );
            Check.IsInRange ( pastYearHowOftenYouForgotNightBeforeNumber, 0, 4, () => PastYearHowOftenYouForgotNightBeforeNumber );
            Check.IsInList ( youOrSomeoneInjuredDueToYourDrinkingNumber, new[] { 0, 2, 4 }, () => YouOrSomeoneInjuredDueToYourDrinkingNumber );
            Check.IsInList ( healthWorkerSuggestedToCutDownDrinkingNumber, new[] { 0, 2, 4 }, () => HealthWorkerSuggestedToCutDownDrinkingNumber );

            HowOftenYouDrinkNumber = howOftenYouDrinkNumber;

            //NOTE: If the Patient never drinks, then the next answers are always 0.
            if ( howOftenYouDrinkNumber == 0 )
            {
                AlcoholicDrinksPerDayNumber
                    = HowOftenYouHaveSixOrMoreDrinksNumber
                      = PastYearHowOftenYouWereUnableToStopDrinkingNumber
                        = PastYearHowOftenYouFailedNormalExpectationNumber
                          = PastYearHowOftenYouDrinkInMorningNumber
                            = PastYearHowOftenYouHadGuiltAfterDrinkingNumber
                              = PastYearHowOftenYouForgotNightBeforeNumber
                                = YouOrSomeoneInjuredDueToYourDrinkingNumber
                                  = HealthWorkerSuggestedToCutDownDrinkingNumber
                                    = 0;
            }
            else
            {
                AlcoholicDrinksPerDayNumber = alcoholicDrinksPerDayNumber;
                HowOftenYouHaveSixOrMoreDrinksNumber = howOftenYouHaveSixOrMoreDrinksNumber;
                PastYearHowOftenYouWereUnableToStopDrinkingNumber = pastYearHowOftenYouWereUnableToStopDrinkingNumber;
                PastYearHowOftenYouFailedNormalExpectationNumber = pastYearHowOftenYouFailedNormalExpectationNumber;
                PastYearHowOftenYouDrinkInMorningNumber = pastYearHowOftenYouDrinkInMorningNumber;
                PastYearHowOftenYouHadGuiltAfterDrinkingNumber = pastYearHowOftenYouHadGuiltAfterDrinkingNumber;
                PastYearHowOftenYouForgotNightBeforeNumber = pastYearHowOftenYouForgotNightBeforeNumber;
                YouOrSomeoneInjuredDueToYourDrinkingNumber = youOrSomeoneInjuredDueToYourDrinkingNumber;
                HealthWorkerSuggestedToCutDownDrinkingNumber = healthWorkerSuggestedToCutDownDrinkingNumber;
            }

            _auditScore = HowOftenYouDrinkNumber.GetValueOrDefault ()
                          + AlcoholicDrinksPerDayNumber.GetValueOrDefault ()
                          + HowOftenYouHaveSixOrMoreDrinksNumber.GetValueOrDefault ()
                          + PastYearHowOftenYouWereUnableToStopDrinkingNumber.GetValueOrDefault ()
                          + PastYearHowOftenYouFailedNormalExpectationNumber.GetValueOrDefault ()
                          + PastYearHowOftenYouDrinkInMorningNumber.GetValueOrDefault ()
                          + PastYearHowOftenYouHadGuiltAfterDrinkingNumber.GetValueOrDefault ()
                          + PastYearHowOftenYouForgotNightBeforeNumber.GetValueOrDefault ()
                          + YouOrSomeoneInjuredDueToYourDrinkingNumber.GetValueOrDefault ()
                          + HealthWorkerSuggestedToCutDownDrinkingNumber.GetValueOrDefault ();
        }

        #endregion
    }
}