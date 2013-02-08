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
    /// Data transfer object for Dast10 class.
    /// </summary>
    public class Dast10Dto : ActivityDto
    {
        #region Constants and Fields

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
        private bool _isNidaDrugQuestionnaireCreated;
        private int? _severityScore;

        #endregion

        #region Public Properties

        /// <summary>
        /// Are you always able to stop using drugs when you want to?
        /// </summary>
        /// <value>The are you able to stop using drugs indicator.</value>
        [DataMember]
        public virtual bool? AreYouAbleToStopUsingDrugsIndicator
        {
            get { return _areYouAbleToStopUsingDrugsIndicator; }
            set { ApplyPropertyChange ( ref _areYouAbleToStopUsingDrugsIndicator, () => AreYouAbleToStopUsingDrugsIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the dast10 result.
        /// </summary>
        /// <value>The dast10 result.</value>
        [DataMember]
        public int? Dast10Result
        {
            get { return _dast10Result; }
            set { ApplyPropertyChange ( ref _dast10Result, () => Dast10Result, value ); }
        }

        /// <summary>
        /// Do you abuse more than one drug at a time?
        /// </summary>
        /// <value>The do you abuse more than one drug indicator.</value>
        [DataMember]
        public virtual bool? DoYouAbuseMoreThanOneDrugIndicator
        {
            get { return _doYouAbuseMoreThanOneDrugIndicator; }
            set { ApplyPropertyChange ( ref _doYouAbuseMoreThanOneDrugIndicator, () => DoYouAbuseMoreThanOneDrugIndicator, value ); }
        }

        /// <summary>
        /// Have you had “blackouts” or “flashbacks” as a result of drug use?
        /// </summary>
        /// <value>The do you feel bad or guilty indicator.</value>
        [DataMember]
        public virtual bool? DoYouFeelBadOrGuiltyIndicator
        {
            get { return _doYouFeelBadOrGuiltyIndicator; }
            set { ApplyPropertyChange ( ref _doYouFeelBadOrGuiltyIndicator, () => DoYouFeelBadOrGuiltyIndicator, value ); }
        }

        /// <summary>
        /// Does your spouse (or parent) ever complain about your involvement with drugs?
        /// </summary>
        /// <value>The does your spouse or parent complain indicator.</value>
        [DataMember]
        public virtual bool? DoesYourSpouseOrParentComplainIndicator
        {
            get { return _doesYourSpouseOrParentComplainIndicator; }
            set { ApplyPropertyChange ( ref _doesYourSpouseOrParentComplainIndicator, () => DoesYourSpouseOrParentComplainIndicator, value ); }
        }

        /// <summary>
        /// Have you neglected your family because of your use of drugs?
        /// </summary>
        /// <value>The have you engaged in illegal activities indicator.</value>
        [DataMember]
        public virtual bool? HaveYouEngagedInIllegalActivitiesIndicator
        {
            get { return _haveYouEngagedInIllegalActivitiesIndicator; }
            set { ApplyPropertyChange ( ref _haveYouEngagedInIllegalActivitiesIndicator, () => HaveYouEngagedInIllegalActivitiesIndicator, value ); }
        }

        /// <summary>
        /// Have you neglected your family because of your use of drugs?
        /// </summary>
        /// <value>The have you ever experienced withdrawal symptoms indicator.</value>
        [DataMember]
        public virtual bool? HaveYouEverExperiencedWithdrawalSymptomsIndicator
        {
            get { return _haveYouEverExperiencedWithdrawalSymptomsIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _haveYouEverExperiencedWithdrawalSymptomsIndicator,
                    () => HaveYouEverExperiencedWithdrawalSymptomsIndicator,
                    value );
            }
        }

        /// <summary>
        /// Have you had “blackouts” or “flashbacks” as a result of drug use?
        /// </summary>
        /// <value>The have you had blackouts or flashbacks indicator.</value>
        [DataMember]
        public virtual bool? HaveYouHadBlackoutsOrFlashbacksIndicator
        {
            get { return _haveYouHadBlackoutsOrFlashbacksIndicator; }
            set { ApplyPropertyChange ( ref _haveYouHadBlackoutsOrFlashbacksIndicator, () => HaveYouHadBlackoutsOrFlashbacksIndicator, value ); }
        }

        /// <summary>
        /// Have you had medical problems as a result of your drug use
        /// (e.g., memory loss, hepatitis, convulsions, bleeding etc…)
        /// </summary>
        /// <value>The have you had medical problems indicator.</value>
        [DataMember]
        public virtual bool? HaveYouHadMedicalProblemsIndicator
        {
            get { return _haveYouHadMedicalProblemsIndicator; }
            set { ApplyPropertyChange ( ref _haveYouHadMedicalProblemsIndicator, () => HaveYouHadMedicalProblemsIndicator, value ); }
        }

        /// <summary>
        /// Have you neglected your family because of your use of drugs?
        /// </summary>
        /// <value>The have you neglected your family indicator.</value>
        [DataMember]
        public virtual bool? HaveYouNeglectedYourFamilyIndicator
        {
            get { return _haveYouNeglectedYourFamilyIndicator; }
            set { ApplyPropertyChange ( ref _haveYouNeglectedYourFamilyIndicator, () => HaveYouNeglectedYourFamilyIndicator, value ); }
        }

        /// <summary>
        /// Have you used drugs other than those required for medical reasons?
        /// </summary>
        /// <value>The have you used drugs indicator.</value>
        [DataMember]
        public virtual bool? HaveYouUsedDrugsIndicator
        {
            get { return _haveYouUsedDrugsIndicator; }
            set { ApplyPropertyChange ( ref _haveYouUsedDrugsIndicator, () => HaveYouUsedDrugsIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is nida drug questionnaire created.
        /// </summary>
        /// <value><c>true</c> if this instance is nida drug questionnaire created; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsNidaDrugQuestionnaireCreated
        {
            get { return _isNidaDrugQuestionnaireCreated; }
            set { ApplyPropertyChange ( ref _isNidaDrugQuestionnaireCreated, () => IsNidaDrugQuestionnaireCreated, value ); }
        }

        /// <summary>
        /// Gets or sets the severity score.
        /// </summary>
        /// <value>The severity score.</value>
        [DataMember]
        public virtual int? SeverityScore
        {
            get { return _severityScore; }
            set { ApplyPropertyChange ( ref _severityScore, () => SeverityScore, value ); }
        }

        #endregion
    }
}
