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

using Pillar.Common.InversionOfControl;
using Rem.Domain.Clinical.VisitModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// DAST-10 is an <see cref="Activity">Activity</see> in the form of a
    /// questionnaire, Drug Abuse Screening Test, used to assess potential involvement with drugs
    /// excluding alcohol and tobacco during the past 12 months.
    /// http://www.drugslibrary.stir.ac.uk/documents/dast.pdf
    /// </summary>
    /// <remarks>
    /// The SBIRT Initiative is a 5-year national program funded by the Substance Abuse and Mental Health Services Administration (SAMHSA), 
    /// Center for Substance Abuse Treatment (CSAT) to implement screening, brief intervention, referral to treatment services for adults in 
    /// primary care and community health settings for substance misuse and substance abuse disorders.
    /// </remarks>
    public class Dast10 : Activity
    {
        #region Member Variables

        private const int SeverityScoreThreshold = 6;
        private const int MinConsiderableSeverityScore = 1;
        private const int ResultAsReassesAnnually = 0;
        private const int ResultAsBriefAdvice = 1;
        private const int ResultAsScheduleFollowUp = 2;
        private IActivitySchedulerService _activitySchedulerService;

        private bool? _areYouAbleToStopUsingDrugsIndicator;
        private int? _dast10Result;
        private bool? _doYouAbuseMoreThanOneDrugIndicator;
        private bool? _doYouFeelBadOrGuiltyIndicator;
        private bool? _doesYourSpouseOrParentComplainIndicator;
        private bool? _haveYouEngagedInIllegalActivitiesIndicator;
        private bool? _haveYouEverExperiencedWithdrawalSymptomsIndicator;
        private bool? _haveYouHadBlackoutsOrFlashbacksIndicator;
        private bool? _haveYouHadMedicalProblemsIndicator;
        private bool? _haveYouNeglectedYourFamilyIndicator;
        private bool? _haveYouUsedDrugsIndicator;
        private ILookupValueRepository _lookupValueRepository;
        private int? _severityScore;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Dast10"/> class.
        /// </summary>
        protected internal Dast10 ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dast10"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal Dast10 (
            Visit visit,
            ActivityType activityType )
            : base ( visit, activityType )
        {
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Have you used drugs other than those required for medical reasons?
        /// </summary>
        public virtual bool? HaveYouUsedDrugsIndicator
        {
            get { return _haveYouUsedDrugsIndicator; }
            private set { ApplyPropertyChange ( ref _haveYouUsedDrugsIndicator, () => HaveYouUsedDrugsIndicator, value ); }
        }

        /// <summary>
        ///   Do you abuse more than one drug at a time?
        /// </summary>
        public virtual bool? DoYouAbuseMoreThanOneDrugIndicator
        {
            get { return _doYouAbuseMoreThanOneDrugIndicator; }
            private set { ApplyPropertyChange ( ref _doYouAbuseMoreThanOneDrugIndicator, () => DoYouAbuseMoreThanOneDrugIndicator, value ); }
        }

        /// <summary>
        ///   Are you always able to stop using drugs when you want to?
        /// </summary>
        public virtual bool? AreYouAbleToStopUsingDrugsIndicator
        {
            get { return _areYouAbleToStopUsingDrugsIndicator; }
            private set { ApplyPropertyChange ( ref _areYouAbleToStopUsingDrugsIndicator, () => AreYouAbleToStopUsingDrugsIndicator, value ); }
        }

        /// <summary>
        ///   Have you had “blackouts” or “flashbacks” as a result of drug use?
        /// </summary>
        public virtual bool? HaveYouHadBlackoutsOrFlashbacksIndicator
        {
            get { return _haveYouHadBlackoutsOrFlashbacksIndicator; }
            private set { ApplyPropertyChange ( ref _haveYouHadBlackoutsOrFlashbacksIndicator, () => HaveYouHadBlackoutsOrFlashbacksIndicator, value ); }
        }

        /// <summary>
        ///   Have you had “blackouts” or “flashbacks” as a result of drug use?
        /// </summary>
        public virtual bool? DoYouFeelBadOrGuiltyIndicator
        {
            get { return _doYouFeelBadOrGuiltyIndicator; }
            private set { ApplyPropertyChange ( ref _doYouFeelBadOrGuiltyIndicator, () => DoYouFeelBadOrGuiltyIndicator, value ); }
        }

        /// <summary>
        ///   Does your spouse (or parent) ever complain about your involvement with drugs?
        /// </summary>
        public virtual bool? DoesYourSpouseOrParentComplainIndicator
        {
            get { return _doesYourSpouseOrParentComplainIndicator; }
            private set { ApplyPropertyChange ( ref _doesYourSpouseOrParentComplainIndicator, () => DoesYourSpouseOrParentComplainIndicator, value ); }
        }

        /// <summary>
        ///   Have you neglected your family because of your use of drugs?
        /// </summary>
        public virtual bool? HaveYouNeglectedYourFamilyIndicator
        {
            get { return _haveYouNeglectedYourFamilyIndicator; }
            private set { ApplyPropertyChange ( ref _haveYouNeglectedYourFamilyIndicator, () => HaveYouNeglectedYourFamilyIndicator, value ); }
        }

        /// <summary>
        ///   Have you neglected your family because of your use of drugs?
        /// </summary>
        public virtual bool? HaveYouEngagedInIllegalActivitiesIndicator
        {
            get { return _haveYouEngagedInIllegalActivitiesIndicator; }
            private set { ApplyPropertyChange ( ref _haveYouEngagedInIllegalActivitiesIndicator, () => HaveYouEngagedInIllegalActivitiesIndicator, value ); }
        }

        /// <summary>
        ///   Have you neglected your family because of your use of drugs?
        /// </summary>
        public virtual bool? HaveYouEverExperiencedWithdrawalSymptomsIndicator
        {
            get { return _haveYouEverExperiencedWithdrawalSymptomsIndicator; }
            private set
            {
                ApplyPropertyChange ( ref _haveYouEverExperiencedWithdrawalSymptomsIndicator, () => HaveYouEverExperiencedWithdrawalSymptomsIndicator, value );
            }
        }

        /// <summary>
        ///   Have you had medical problems as a result of your drug use  
        ///   (e.g., memory loss, hepatitis, convulsions, bleeding etc…)
        /// </summary>
        public virtual bool? HaveYouHadMedicalProblemsIndicator
        {
            get { return _haveYouHadMedicalProblemsIndicator; }
            private set { ApplyPropertyChange ( ref _haveYouHadMedicalProblemsIndicator, () => HaveYouHadMedicalProblemsIndicator, value ); }
        }

        /// <summary>
        /// Gets the severity score.
        /// </summary>
        public virtual int? SeverityScore
        {
            get { return _severityScore; }
            private set { ApplyPropertyChange ( ref _severityScore, () => SeverityScore, value ); }
        }

        /// <summary>
        ///   Result = 0 when, SeverityScore = 0.
        ///   Result = 1 when, SeverityScore greater than or equal to 1 and SeverityScore less than or equal to 5 and NidaDrugQuestionnaireIndicator is FALSE.
        ///   Result = 2 when, (SeverityScore greater than or equal to 6) Or (SeverityScore greater than or equal to 1 and SeverityScore less than or equal to 5 and NidaDrugQuestionnaireIndicator is TRUE).
        /// </summary>
        public virtual int? Dast10Result
        {
            get { return _dast10Result; }
            private set { ApplyPropertyChange ( ref _dast10Result, () => Dast10Result, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Revises the and calculate.
        /// </summary>
        /// <param name="haveYouUsedDrugsIndicator">The have you used drugs indicator.</param>
        /// <param name="doYouAbuseMoreThanOneDrugIndicator">The do you abuse more than one drug indicator.</param>
        /// <param name="areYouAbleToStopUsingDrugsIndicator">The are you able to stop using drugs indicator.</param>
        /// <param name="haveYouHadBlackoutsOrFlashbacksIndicator">The have you had blackouts or flashbacks indicator.</param>
        /// <param name="doYouFeelBadOrGuiltyIndicator">The do you feel bad or guilty indicator.</param>
        /// <param name="doesYourSpouseOrParentComplainIndicator">The does your spouse or parent complain indicator.</param>
        /// <param name="haveYouNeglectedYourFamilyIndicator">The have you neglected your family indicator.</param>
        /// <param name="haveYouEngagedInIllegalActivitiesIndicator">The have you engaged in illegal activities indicator.</param>
        /// <param name="haveYouEverExperiencedWithdrawalSymptomsIndicator">The have you ever experienced withdrawal symptoms indicator.</param>
        /// <param name="haveYouHadMedicalProblemsIndicator">The have you had medical problems indicator.</param>
        public virtual void ReviseAndCalculate ( bool? haveYouUsedDrugsIndicator,
                                                 bool? doYouAbuseMoreThanOneDrugIndicator,
                                                 bool? areYouAbleToStopUsingDrugsIndicator,
                                                 bool? haveYouHadBlackoutsOrFlashbacksIndicator,
                                                 bool? doYouFeelBadOrGuiltyIndicator,
                                                 bool? doesYourSpouseOrParentComplainIndicator,
                                                 bool? haveYouNeglectedYourFamilyIndicator,
                                                 bool? haveYouEngagedInIllegalActivitiesIndicator,
                                                 bool? haveYouEverExperiencedWithdrawalSymptomsIndicator,
                                                 bool? haveYouHadMedicalProblemsIndicator )
        {
            HaveYouUsedDrugsIndicator = haveYouUsedDrugsIndicator;
            DoYouAbuseMoreThanOneDrugIndicator = doYouAbuseMoreThanOneDrugIndicator;
            AreYouAbleToStopUsingDrugsIndicator = areYouAbleToStopUsingDrugsIndicator;
            HaveYouHadBlackoutsOrFlashbacksIndicator = haveYouHadBlackoutsOrFlashbacksIndicator;
            DoYouFeelBadOrGuiltyIndicator = doYouFeelBadOrGuiltyIndicator;
            DoesYourSpouseOrParentComplainIndicator = doesYourSpouseOrParentComplainIndicator;
            HaveYouNeglectedYourFamilyIndicator = haveYouNeglectedYourFamilyIndicator;
            HaveYouEngagedInIllegalActivitiesIndicator = haveYouEngagedInIllegalActivitiesIndicator;
            HaveYouEverExperiencedWithdrawalSymptomsIndicator = haveYouEverExperiencedWithdrawalSymptomsIndicator;
            HaveYouHadMedicalProblemsIndicator = haveYouHadMedicalProblemsIndicator;

            SeverityScore = InterpretAnswer ( _haveYouUsedDrugsIndicator )
                            + InterpretAnswer ( _doYouAbuseMoreThanOneDrugIndicator )
                            + InterpretAnswer ( _areYouAbleToStopUsingDrugsIndicator, true )
                            + InterpretAnswer ( _haveYouHadBlackoutsOrFlashbacksIndicator )
                            + InterpretAnswer ( _doYouFeelBadOrGuiltyIndicator )
                            + InterpretAnswer ( _doesYourSpouseOrParentComplainIndicator )
                            + InterpretAnswer ( _haveYouNeglectedYourFamilyIndicator )
                            + InterpretAnswer ( _haveYouEngagedInIllegalActivitiesIndicator )
                            + InterpretAnswer ( _haveYouEverExperiencedWithdrawalSymptomsIndicator )
                            + InterpretAnswer ( _haveYouHadMedicalProblemsIndicator );

            NidaDrugQuestionnaire nidaDrugQuestionnaire = null;

            if ( SeverityScore > 0 )
            {
                nidaDrugQuestionnaire = ScheduleNidaDrugQuestionnaire ();
            }
            else
            {
                var socialHistoryRepository = IoC.CurrentContainer.Resolve<ISocialHistoryRepository> ();
                SocialHistory socialHistory = socialHistoryRepository.GetSocialHistoryInVisit ( Visit.Key );

                if ( socialHistory != null && socialHistory.SocialHistoryDast10.Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber.GetValueOrDefault () > 0 )
                {
                    nidaDrugQuestionnaire = ScheduleNidaDrugQuestionnaire ();
                }
            }

             SetDast10Result(nidaDrugQuestionnaire);
        }

        /// <summary>
        /// Sets the dast10 result.
        /// </summary>
        /// <param name="nidaDrugQuestionnaire">The nida drug questionnaire.</param>
        /// <returns>A boolean value.</returns>
        public virtual bool SetDast10Result ( NidaDrugQuestionnaire nidaDrugQuestionnaire )
        {
            int severityScore = SeverityScore.GetValueOrDefault ();
            int dast10Result = 0;

            if ( SeverityScore.GetValueOrDefault () < MinConsiderableSeverityScore )
            {
                dast10Result = ResultAsReassesAnnually;
            }
            else
            {
                if ( severityScore >= SeverityScoreThreshold )
                {
                    dast10Result = ResultAsScheduleFollowUp;
                }
                else
                {
                    if ( severityScore >= MinConsiderableSeverityScore && severityScore < SeverityScoreThreshold )
                    {
                        if (nidaDrugQuestionnaire != null && nidaDrugQuestionnaire.NidaDrugQuestionnaireIndicator.HasValue)
                        {
                            dast10Result = nidaDrugQuestionnaire.NidaDrugQuestionnaireIndicator.Value ? ResultAsScheduleFollowUp : ResultAsBriefAdvice;
                        }
                    }
                    else
                    {
                        dast10Result = ResultAsReassesAnnually;
                    }
                }
            }

            bool dast10ResultChanged = false;

            if (Dast10Result != dast10Result)
            {
                Dast10Result = dast10Result;
                dast10ResultChanged = true;
            }

            return dast10ResultChanged;
        }

        private NidaDrugQuestionnaire ScheduleNidaDrugQuestionnaire ()
        {
            if ( _activitySchedulerService == null )
            {
                _activitySchedulerService = IoC.CurrentContainer.Resolve<IActivitySchedulerService> ();
            }

            if ( _lookupValueRepository == null )
            {
                _lookupValueRepository = IoC.CurrentContainer.Resolve<ILookupValueRepository> ();
            }

            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType> ( WellKnownNames.VisitModule.ActivityType.NidaDrugQuestionnaire );

            return ( NidaDrugQuestionnaire ) _activitySchedulerService.ScheduleActivity ( Visit.Key, activityType );
        }

        private static int InterpretAnswer ( bool? answer, bool negate = false )
        {
            return negate ? ( answer.HasValue ? ( answer.Value ? 0 : 1 ) : 0 ) : ( answer.HasValue ? ( answer.Value ? 1 : 0 ) : 0 );
        }

        #endregion
    }
}