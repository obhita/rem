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

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiDsmIv class.
    /// </summary>
    public class DensAsiDsmIvDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DensAsiNonResponseTypeDto<bool?> _alcoholAbuseInabilityToMeetResponsibilitiesIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholAbuseLegalProblemsIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholAbuseSocialProblemsIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholAbuseVoluntaryPhysicalDangerIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholDependenceContinuedToUseIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholDependenceLessEffectIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholDependencePhysicalDistressOnQuittingIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholDependenceTimeSpentObtainingIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholDependenceUsedMoreIndicator;
        private DensAsiNonResponseTypeDto<bool?> _alcoholDependenceWantedToCutDownIndicator;
        private string _continuedToUseIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _drugAbuseInabilityToMeetResponsibilitiesIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugAbuseLegalProblemsIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugAbuseSocialProblemsIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugAbuseVoluntaryPhysicalDangerIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugDependenceContinuedToUseIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugDependenceGiveUpWorkFamilyActivitiesIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugDependenceLessEffectIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugDependencePhysicalDistressOnQuittingIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugDependenceTimeSpentObtainingIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugDependenceUsedMoreIndicator;
        private DensAsiNonResponseTypeDto<bool?> _drugDependenceWantedToCutDownIndicator;
        private string _giveUpWorkFamilyActivitiesIndicatorNote;
        private string _inabilityToMeetResponsibilitiesIndicatorNote;
        private string _legalProblemsIndicatorNote;
        private string _lessEffectIndicatorNote;
        private string _physicalDistressOnQuittingNote;
        private string _sectionNote;
        private string _socialProblemsIndicatorNote;
        private string _timeSpentObtainingIndicatorNote;
        private string _usedMoreIndicatorNote;
        private string _voluntaryPhysicalDangerIndicatorNote;
        private string _wantedToCutDownIndicatorNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: 1 (Additional)
        /// </summary>
        /// <value>The alcohol abuse inability to meet responsibilities indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholAbuseInabilityToMeetResponsibilitiesIndicator
        {
            get { return _alcoholAbuseInabilityToMeetResponsibilitiesIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _alcoholAbuseInabilityToMeetResponsibilitiesIndicator, () => AlcoholAbuseInabilityToMeetResponsibilitiesIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: 3 (Additional)
        /// </summary>
        /// <value>The alcohol abuse legal problems indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholAbuseLegalProblemsIndicator
        {
            get { return _alcoholAbuseLegalProblemsIndicator; }
            set { ApplyPropertyChange ( ref _alcoholAbuseLegalProblemsIndicator, () => AlcoholAbuseLegalProblemsIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 4 (Additional)
        /// </summary>
        /// <value>The alcohol abuse social problems indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholAbuseSocialProblemsIndicator
        {
            get { return _alcoholAbuseSocialProblemsIndicator; }
            set { ApplyPropertyChange ( ref _alcoholAbuseSocialProblemsIndicator, () => AlcoholAbuseSocialProblemsIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 2 (Additional)
        /// </summary>
        /// <value>The alcohol abuse voluntary physical danger indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholAbuseVoluntaryPhysicalDangerIndicator
        {
            get { return _alcoholAbuseVoluntaryPhysicalDangerIndicator; }
            set { ApplyPropertyChange ( ref _alcoholAbuseVoluntaryPhysicalDangerIndicator, () => AlcoholAbuseVoluntaryPhysicalDangerIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 7
        /// </summary>
        /// <value>The alcohol dependence continued to use indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholDependenceContinuedToUseIndicator
        {
            get { return _alcoholDependenceContinuedToUseIndicator; }
            set { ApplyPropertyChange ( ref _alcoholDependenceContinuedToUseIndicator, () => AlcoholDependenceContinuedToUseIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 6
        /// </summary>
        /// <value>The alcohol dependence give up work family activities indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholDependenceGiveUpWorkFamilyActivitiesIndicator
        {
            get { return _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator, () => AlcoholDependenceGiveUpWorkFamilyActivitiesIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: 1
        /// </summary>
        /// <value>The alcohol dependence less effect indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholDependenceLessEffectIndicator
        {
            get { return _alcoholDependenceLessEffectIndicator; }
            set { ApplyPropertyChange ( ref _alcoholDependenceLessEffectIndicator, () => AlcoholDependenceLessEffectIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 2
        /// </summary>
        /// <value>The alcohol dependence physical distress on quitting indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholDependencePhysicalDistressOnQuittingIndicator
        {
            get { return _alcoholDependencePhysicalDistressOnQuittingIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _alcoholDependencePhysicalDistressOnQuittingIndicator, () => AlcoholDependencePhysicalDistressOnQuittingIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: 5
        /// </summary>
        /// <value>The alcohol dependence time spent obtaining indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholDependenceTimeSpentObtainingIndicator
        {
            get { return _alcoholDependenceTimeSpentObtainingIndicator; }
            set { ApplyPropertyChange ( ref _alcoholDependenceTimeSpentObtainingIndicator, () => AlcoholDependenceTimeSpentObtainingIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 3
        /// </summary>
        /// <value>The alcohol dependence used more indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholDependenceUsedMoreIndicator
        {
            get { return _alcoholDependenceUsedMoreIndicator; }
            set { ApplyPropertyChange ( ref _alcoholDependenceUsedMoreIndicator, () => AlcoholDependenceUsedMoreIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 4
        /// </summary>
        /// <value>The alcohol dependence wanted to cut down indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AlcoholDependenceWantedToCutDownIndicator
        {
            get { return _alcoholDependenceWantedToCutDownIndicator; }
            set { ApplyPropertyChange ( ref _alcoholDependenceWantedToCutDownIndicator, () => AlcoholDependenceWantedToCutDownIndicator, value ); }
        }

        /// <summary>
        /// Question Number : 7
        /// </summary>
        /// <value>The continued to use indicator note.</value>
        public string ContinuedToUseIndicatorNote
        {
            get { return _continuedToUseIndicatorNote; }
            set { ApplyPropertyChange ( ref _continuedToUseIndicatorNote, () => ContinuedToUseIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: 1 (Additional)
        /// </summary>
        /// <value>The drug abuse inability to meet responsibilities indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugAbuseInabilityToMeetResponsibilitiesIndicator
        {
            get { return _drugAbuseInabilityToMeetResponsibilitiesIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _drugAbuseInabilityToMeetResponsibilitiesIndicator, () => DrugAbuseInabilityToMeetResponsibilitiesIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: 3 (Additional)
        /// </summary>
        /// <value>The drug abuse legal problems indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugAbuseLegalProblemsIndicator
        {
            get { return _drugAbuseLegalProblemsIndicator; }
            set { ApplyPropertyChange ( ref _drugAbuseLegalProblemsIndicator, () => DrugAbuseLegalProblemsIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 4 (Additional)
        /// </summary>
        /// <value>The drug abuse social problems indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugAbuseSocialProblemsIndicator
        {
            get { return _drugAbuseSocialProblemsIndicator; }
            set { ApplyPropertyChange ( ref _drugAbuseSocialProblemsIndicator, () => DrugAbuseSocialProblemsIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 2 (Additional)
        /// </summary>
        /// <value>The drug abuse voluntary physical danger indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugAbuseVoluntaryPhysicalDangerIndicator
        {
            get { return _drugAbuseVoluntaryPhysicalDangerIndicator; }
            set { ApplyPropertyChange ( ref _drugAbuseVoluntaryPhysicalDangerIndicator, () => DrugAbuseVoluntaryPhysicalDangerIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 7
        /// </summary>
        /// <value>The drug dependence continued to use indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugDependenceContinuedToUseIndicator
        {
            get { return _drugDependenceContinuedToUseIndicator; }
            set { ApplyPropertyChange ( ref _drugDependenceContinuedToUseIndicator, () => DrugDependenceContinuedToUseIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 6
        /// </summary>
        /// <value>The drug dependence give up work family activities indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugDependenceGiveUpWorkFamilyActivitiesIndicator
        {
            get { return _drugDependenceGiveUpWorkFamilyActivitiesIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _drugDependenceGiveUpWorkFamilyActivitiesIndicator, () => DrugDependenceGiveUpWorkFamilyActivitiesIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: 1
        /// </summary>
        /// <value>The drug dependence less effect indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugDependenceLessEffectIndicator
        {
            get { return _drugDependenceLessEffectIndicator; }
            set { ApplyPropertyChange ( ref _drugDependenceLessEffectIndicator, () => DrugDependenceLessEffectIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 2
        /// </summary>
        /// <value>The drug dependence physical distress on quitting indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugDependencePhysicalDistressOnQuittingIndicator
        {
            get { return _drugDependencePhysicalDistressOnQuittingIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _drugDependencePhysicalDistressOnQuittingIndicator, () => DrugDependencePhysicalDistressOnQuittingIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: 5
        /// </summary>
        /// <value>The drug dependence time spent obtaining indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugDependenceTimeSpentObtainingIndicator
        {
            get { return _drugDependenceTimeSpentObtainingIndicator; }
            set { ApplyPropertyChange ( ref _drugDependenceTimeSpentObtainingIndicator, () => DrugDependenceTimeSpentObtainingIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 3
        /// </summary>
        /// <value>The drug dependence used more indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugDependenceUsedMoreIndicator
        {
            get { return _drugDependenceUsedMoreIndicator; }
            set { ApplyPropertyChange ( ref _drugDependenceUsedMoreIndicator, () => DrugDependenceUsedMoreIndicator, value ); }
        }

        /// <summary>
        /// Question Number: 4
        /// </summary>
        /// <value>The drug dependence wanted to cut down indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DrugDependenceWantedToCutDownIndicator
        {
            get { return _drugDependenceWantedToCutDownIndicator; }
            set { ApplyPropertyChange ( ref _drugDependenceWantedToCutDownIndicator, () => DrugDependenceWantedToCutDownIndicator, value ); }
        }

        /// <summary>
        /// Question Number : 6
        /// </summary>
        /// <value>The give up work family activities indicator note.</value>
        public string GiveUpWorkFamilyActivitiesIndicatorNote
        {
            get { return _giveUpWorkFamilyActivitiesIndicatorNote; }
            set { ApplyPropertyChange ( ref _giveUpWorkFamilyActivitiesIndicatorNote, () => GiveUpWorkFamilyActivitiesIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number : 1 (Additional)
        /// </summary>
        /// <value>The inability to meet responsibilities indicator note.</value>
        public string InabilityToMeetResponsibilitiesIndicatorNote
        {
            get { return _inabilityToMeetResponsibilitiesIndicatorNote; }
            set { ApplyPropertyChange ( ref _inabilityToMeetResponsibilitiesIndicatorNote, () => InabilityToMeetResponsibilitiesIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number : 3 (Additional)
        /// </summary>
        /// <value>The legal problems indicator note.</value>
        public string LegalProblemsIndicatorNote
        {
            get { return _legalProblemsIndicatorNote; }
            set { ApplyPropertyChange ( ref _legalProblemsIndicatorNote, () => LegalProblemsIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: 1
        /// </summary>
        /// <value>The less effect indicator note.</value>
        public string LessEffectIndicatorNote
        {
            get { return _lessEffectIndicatorNote; }
            set { ApplyPropertyChange ( ref _lessEffectIndicatorNote, () => LessEffectIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: 2
        /// </summary>
        /// <value>The physical distress on quitting note.</value>
        public string PhysicalDistressOnQuittingNote
        {
            get { return _physicalDistressOnQuittingNote; }
            set { ApplyPropertyChange ( ref _physicalDistressOnQuittingNote, () => PhysicalDistressOnQuittingNote, value ); }
        }

        /// <summary>
        /// Gets or sets the section note.
        /// </summary>
        /// <value>The section note.</value>
        public string SectionNote
        {
            get { return _sectionNote; }
            set { ApplyPropertyChange ( ref _sectionNote, () => SectionNote, value ); }
        }

        /// <summary>
        /// Question Number : 4 (Additional)
        /// </summary>
        /// <value>The social problems indicator note.</value>
        public string SocialProblemsIndicatorNote
        {
            get { return _socialProblemsIndicatorNote; }
            set { ApplyPropertyChange ( ref _socialProblemsIndicatorNote, () => SocialProblemsIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number : 5
        /// </summary>
        /// <value>The time spent obtaining indicator note.</value>
        public string TimeSpentObtainingIndicatorNote
        {
            get { return _timeSpentObtainingIndicatorNote; }
            set { ApplyPropertyChange ( ref _timeSpentObtainingIndicatorNote, () => TimeSpentObtainingIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number : 3
        /// </summary>
        /// <value>The used more indicator note.</value>
        public string UsedMoreIndicatorNote
        {
            get { return _usedMoreIndicatorNote; }
            set { ApplyPropertyChange ( ref _usedMoreIndicatorNote, () => UsedMoreIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number : 2 (Additional)
        /// </summary>
        /// <value>The voluntary physical danger indicator note.</value>
        public string VoluntaryPhysicalDangerIndicatorNote
        {
            get { return _voluntaryPhysicalDangerIndicatorNote; }
            set { ApplyPropertyChange ( ref _voluntaryPhysicalDangerIndicatorNote, () => VoluntaryPhysicalDangerIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number : 4
        /// </summary>
        /// <value>The wanted to cut down indicator note.</value>
        public string WantedToCutDownIndicatorNote
        {
            get { return _wantedToCutDownIndicatorNote; }
            set { ApplyPropertyChange ( ref _wantedToCutDownIndicatorNote, () => WantedToCutDownIndicatorNote, value ); }
        }

        #endregion
    }
}
