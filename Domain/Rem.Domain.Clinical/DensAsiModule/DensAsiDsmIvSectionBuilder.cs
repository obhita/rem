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

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiDsmIvSectionBuilder provides a fluent interface for creating a DesAsiDsmIv section.
    /// </summary>
    public class DensAsiDsmIvSectionBuilder
    {
        private DensAsiNonResponseType<bool?> _alcoholDependenceLessEffectIndicator;
        private DensAsiNonResponseType<bool?> _drugDependenceLessEffectIndicator;
        private string _lessEffectIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholDependencePhysicalDistressOnQuittingIndicator;
        private DensAsiNonResponseType<bool?> _drugDependencePhysicalDistressOnQuittingIndicator;
        private string _physicalDistressOnQuittingNote;
        private DensAsiNonResponseType<bool?> _alcoholDependenceUsedMoreIndicator;
        private DensAsiNonResponseType<bool?> _drugDependenceUsedMoreIndicator;
        private string _usedMoreIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholDependenceWantedToCutDownIndicator;
        private DensAsiNonResponseType<bool?> _drugDependenceWantedToCutDownIndicator;
        private string _wantedToCutDownIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholDependenceTimeSpentObtainingIndicator;
        private DensAsiNonResponseType<bool?> _drugDependenceTimeSpentObtainingIndicator;
        private string _timeSpentObtainingIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator;
        private DensAsiNonResponseType<bool?> _drugDependenceGiveUpWorkFamilyActivitiesIndicator;
        private string _giveUpWorkFamilyActivitiesIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholDependenceContinuedToUseIndicator;
        private DensAsiNonResponseType<bool?> _drugDependenceContinuedToUseIndicator;
        private string _continuedToUseIndicatorNote;
        private string _sectionNote;
        private DensAsiNonResponseType<bool?> _alcoholAbuseInabilityToMeetResponsibilitiesIndicator;
        private DensAsiNonResponseType<bool?> _drugAbuseInabilityToMeetResponsibilitiesIndicator;
        private string _inabilityToMeetResponsibilitiesIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholAbuseVoluntaryPhysicalDangerIndicator;
        private DensAsiNonResponseType<bool?> _drugAbuseVoluntaryPhysicalDangerIndicator;
        private string _voluntaryPhysicalDangerIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholAbuseLegalProblemsIndicator;
        private DensAsiNonResponseType<bool?> _drugAbuseLegalProblemsIndicator;
        private string _legalProblemsIndicatorNote;
        private DensAsiNonResponseType<bool?> _alcoholAbuseSocialProblemsIndicator;
        private DensAsiNonResponseType<bool?> _drugAbuseSocialProblemsIndicator;
        private string _socialProblemsIndicatorNote;


        /// <summary>
        /// Assigns the alcohol dependence less effect indicator.
        /// </summary>
        /// <param name="alcoholDependenceLessEffectIndicator">The alcohol dependence less effect indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholDependenceLessEffectIndicator(DensAsiNonResponseType<bool?> alcoholDependenceLessEffectIndicator)
        {
            _alcoholDependenceLessEffectIndicator = alcoholDependenceLessEffectIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug dependence less effect indicator.
        /// </summary>
        /// <param name="drugDependenceLessEffectIndicator">The drug dependence less effect indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugDependenceLessEffectIndicator(DensAsiNonResponseType<bool?> drugDependenceLessEffectIndicator)
        {
            _drugDependenceLessEffectIndicator = drugDependenceLessEffectIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the less effect indicator note.
        /// </summary>
        /// <param name="lessEffectIndicatorNote">The less effect indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithLessEffectIndicatorNote(string lessEffectIndicatorNote)
        {
            _lessEffectIndicatorNote = lessEffectIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dependence physical distress on quitting indicator.
        /// </summary>
        /// <param name="alcoholDependencePhysicalDistressOnQuittingIndicator">The alcohol dependence physical distress on quitting indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholDependencePhysicalDistressOnQuittingIndicator(DensAsiNonResponseType<bool?> alcoholDependencePhysicalDistressOnQuittingIndicator)
        {
            _alcoholDependencePhysicalDistressOnQuittingIndicator = alcoholDependencePhysicalDistressOnQuittingIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug dependence physical distress on quitting indicator.
        /// </summary>
        /// <param name="drugDependencePhysicalDistressOnQuittingIndicator">The drug dependence physical distress on quitting indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugDependencePhysicalDistressOnQuittingIndicator(DensAsiNonResponseType<bool?> drugDependencePhysicalDistressOnQuittingIndicator)
        {
            _drugDependencePhysicalDistressOnQuittingIndicator = drugDependencePhysicalDistressOnQuittingIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the physical distress on quitting note.
        /// </summary>
        /// <param name="physicalDistressOnQuittingNote">The physical distress on quitting note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithPhysicalDistressOnQuittingNote(string physicalDistressOnQuittingNote)
        {
            _physicalDistressOnQuittingNote = physicalDistressOnQuittingNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dependence used more indicator.
        /// </summary>
        /// <param name="alcoholDependenceUsedMoreIndicator">The alcohol dependence used more indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholDependenceUsedMoreIndicator(DensAsiNonResponseType<bool?> alcoholDependenceUsedMoreIndicator)
        {
            _alcoholDependenceUsedMoreIndicator = alcoholDependenceUsedMoreIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug dependence used more indicator.
        /// </summary>
        /// <param name="drugDependenceUsedMoreIndicator">The drug dependence used more indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugDependenceUsedMoreIndicator(DensAsiNonResponseType<bool?> drugDependenceUsedMoreIndicator)
        {
            _drugDependenceUsedMoreIndicator = drugDependenceUsedMoreIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the used more indicator note.
        /// </summary>
        /// <param name="usedMoreIndicatorNote">The used more indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithUsedMoreIndicatorNote(string usedMoreIndicatorNote)
        {
            _usedMoreIndicatorNote = usedMoreIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dependence wanted to cut down indicator.
        /// </summary>
        /// <param name="alcoholDependenceWantedToCutDownIndicator">The alcohol dependence wanted to cut down indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholDependenceWantedToCutDownIndicator(DensAsiNonResponseType<bool?> alcoholDependenceWantedToCutDownIndicator)
        {
            _alcoholDependenceWantedToCutDownIndicator = alcoholDependenceWantedToCutDownIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug dependence wanted to cut down indicator.
        /// </summary>
        /// <param name="drugDependenceWantedToCutDownIndicator">The drug dependence wanted to cut down indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugDependenceWantedToCutDownIndicator(DensAsiNonResponseType<bool?> drugDependenceWantedToCutDownIndicator)
        {
            _drugDependenceWantedToCutDownIndicator = drugDependenceWantedToCutDownIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the wanted to cut down indicator note.
        /// </summary>
        /// <param name="wantedToCutDownIndicatorNote">The wanted to cut down indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithWantedToCutDownIndicatorNote(string wantedToCutDownIndicatorNote)
        {
            _wantedToCutDownIndicatorNote = wantedToCutDownIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dependence time spent obtaining indicator.
        /// </summary>
        /// <param name="alcoholDependenceTimeSpentObtainingIndicator">The alcohol dependence time spent obtaining indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholDependenceTimeSpentObtainingIndicator(DensAsiNonResponseType<bool?> alcoholDependenceTimeSpentObtainingIndicator)
        {
            _alcoholDependenceTimeSpentObtainingIndicator = alcoholDependenceTimeSpentObtainingIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug dependence time spent obtaining indicator.
        /// </summary>
        /// <param name="drugDependenceTimeSpentObtainingIndicator">The drug dependence time spent obtaining indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugDependenceTimeSpentObtainingIndicator(DensAsiNonResponseType<bool?> drugDependenceTimeSpentObtainingIndicator)
        {
            _drugDependenceTimeSpentObtainingIndicator = drugDependenceTimeSpentObtainingIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the time spent obtaining indicator note.
        /// </summary>
        /// <param name="timeSpentObtainingIndicatorNote">The time spent obtaining indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithTimeSpentObtainingIndicatorNote(string timeSpentObtainingIndicatorNote)
        {
            _timeSpentObtainingIndicatorNote = timeSpentObtainingIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dependence give up work family activities indicator.
        /// </summary>
        /// <param name="alcoholDependenceGiveUpWorkFamilyActivitiesIndicator">The alcohol dependence give up work family activities indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholDependenceGiveUpWorkFamilyActivitiesIndicator(DensAsiNonResponseType<bool?> alcoholDependenceGiveUpWorkFamilyActivitiesIndicator)
        {
            _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator = alcoholDependenceGiveUpWorkFamilyActivitiesIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug dependence give up work family activities indicator.
        /// </summary>
        /// <param name="drugDependenceGiveUpWorkFamilyActivitiesIndicator">The drug dependence give up work family activities indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugDependenceGiveUpWorkFamilyActivitiesIndicator(DensAsiNonResponseType<bool?> drugDependenceGiveUpWorkFamilyActivitiesIndicator)
        {
            _drugDependenceGiveUpWorkFamilyActivitiesIndicator = drugDependenceGiveUpWorkFamilyActivitiesIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the give up work family activities indicator note.
        /// </summary>
        /// <param name="giveUpWorkFamilyActivitiesIndicatorNote">The give up work family activities indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithGiveUpWorkFamilyActivitiesIndicatorNote(string giveUpWorkFamilyActivitiesIndicatorNote)
        {
            _giveUpWorkFamilyActivitiesIndicatorNote = giveUpWorkFamilyActivitiesIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dependence continued to use indicator.
        /// </summary>
        /// <param name="alcoholDependenceContinuedToUseIndicator">The alcohol dependence continued to use indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholDependenceContinuedToUseIndicator(DensAsiNonResponseType<bool?> alcoholDependenceContinuedToUseIndicator)
        {
            _alcoholDependenceContinuedToUseIndicator = alcoholDependenceContinuedToUseIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug dependence continued to use indicator.
        /// </summary>
        /// <param name="drugDependenceContinuedToUseIndicator">The drug dependence continued to use indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugDependenceContinuedToUseIndicator(DensAsiNonResponseType<bool?> drugDependenceContinuedToUseIndicator)
        {
            _drugDependenceContinuedToUseIndicator = drugDependenceContinuedToUseIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the continued to use indicator note.
        /// </summary>
        /// <param name="continuedToUseIndicatorNote">The continued to use indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithContinuedToUseIndicatorNote(string continuedToUseIndicatorNote)
        {
            _continuedToUseIndicatorNote = continuedToUseIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the section note.
        /// </summary>
        /// <param name="sectionNote">The section note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithSectionNote(string sectionNote)
        {
            _sectionNote = sectionNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol abuse inability to meet responsibilities indicator.
        /// </summary>
        /// <param name="alcoholAbuseInabilityToMeetResponsibilitiesIndicator">The alcohol abuse inability to meet responsibilities indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholAbuseInabilityToMeetResponsibilitiesIndicator(DensAsiNonResponseType<bool?> alcoholAbuseInabilityToMeetResponsibilitiesIndicator)
        {
            _alcoholAbuseInabilityToMeetResponsibilitiesIndicator = alcoholAbuseInabilityToMeetResponsibilitiesIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug abuse inability to meet responsibilities indicator.
        /// </summary>
        /// <param name="drugAbuseInabilityToMeetResponsibilitiesIndicator">The drug abuse inability to meet responsibilities indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugAbuseInabilityToMeetResponsibilitiesIndicator(DensAsiNonResponseType<bool?> drugAbuseInabilityToMeetResponsibilitiesIndicator)
        {
            _drugAbuseInabilityToMeetResponsibilitiesIndicator = drugAbuseInabilityToMeetResponsibilitiesIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the inability to meet responsibilities indicator note.
        /// </summary>
        /// <param name="inabilityToMeetResponsibilitiesIndicatorNote">The inability to meet responsibilities indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithInabilityToMeetResponsibilitiesIndicatorNote(string inabilityToMeetResponsibilitiesIndicatorNote)
        {
            _inabilityToMeetResponsibilitiesIndicatorNote = inabilityToMeetResponsibilitiesIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol abuse voluntary physical danger indicator.
        /// </summary>
        /// <param name="alcoholAbuseVoluntaryPhysicalDangerIndicator">The alcohol abuse voluntary physical danger indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholAbuseVoluntaryPhysicalDangerIndicator(DensAsiNonResponseType<bool?> alcoholAbuseVoluntaryPhysicalDangerIndicator)
        {
            _alcoholAbuseVoluntaryPhysicalDangerIndicator = alcoholAbuseVoluntaryPhysicalDangerIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug abuse voluntary physical danger indicator.
        /// </summary>
        /// <param name="drugAbuseVoluntaryPhysicalDangerIndicator">The drug abuse voluntary physical danger indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugAbuseVoluntaryPhysicalDangerIndicator(DensAsiNonResponseType<bool?> drugAbuseVoluntaryPhysicalDangerIndicator)
        {
            _drugAbuseVoluntaryPhysicalDangerIndicator = drugAbuseVoluntaryPhysicalDangerIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the voluntary physical danger indicator note.
        /// </summary>
        /// <param name="voluntaryPhysicalDangerIndicatorNote">The voluntary physical danger indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithVoluntaryPhysicalDangerIndicatorNote(string voluntaryPhysicalDangerIndicatorNote)
        {
            _voluntaryPhysicalDangerIndicatorNote = voluntaryPhysicalDangerIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol abuse legal problems indicator.
        /// </summary>
        /// <param name="alcoholAbuseLegalProblemsIndicator">The alcohol abuse legal problems indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholAbuseLegalProblemsIndicator(DensAsiNonResponseType<bool?> alcoholAbuseLegalProblemsIndicator)
        {
            _alcoholAbuseLegalProblemsIndicator = alcoholAbuseLegalProblemsIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug abuse legal problems indicator.
        /// </summary>
        /// <param name="drugAbuseLegalProblemsIndicator">The drug abuse legal problems indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugAbuseLegalProblemsIndicator(DensAsiNonResponseType<bool?> drugAbuseLegalProblemsIndicator)
        {
            _drugAbuseLegalProblemsIndicator = drugAbuseLegalProblemsIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the legal problems indicator note.
        /// </summary>
        /// <param name="legalProblemsIndicatorNote">The legal problems indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithLegalProblemsIndicatorNote(string legalProblemsIndicatorNote)
        {
            _legalProblemsIndicatorNote = legalProblemsIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol abuse social problems indicator.
        /// </summary>
        /// <param name="alcoholAbuseSocialProblemsIndicator">The alcohol abuse social problems indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithAlcoholAbuseSocialProblemsIndicator(DensAsiNonResponseType<bool?> alcoholAbuseSocialProblemsIndicator)
        {
            _alcoholAbuseSocialProblemsIndicator = alcoholAbuseSocialProblemsIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the drug abuse social problems indicator.
        /// </summary>
        /// <param name="drugAbuseSocialProblemsIndicator">The drug abuse social problems indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithDrugAbuseSocialProblemsIndicator(DensAsiNonResponseType<bool?> drugAbuseSocialProblemsIndicator)
        {
            _drugAbuseSocialProblemsIndicator = drugAbuseSocialProblemsIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the social problems indicator note.
        /// </summary>
        /// <param name="socialProblemsIndicatorNote">The social problems indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSectionBuilder WithSocialProblemsIndicatorNote(string socialProblemsIndicatorNote)
        {
            _socialProblemsIndicatorNote = socialProblemsIndicatorNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDsmIvSectionBuilder">A DensAsiDsmIvSectionBuilder.</see></returns>
        public DensAsiDsmIvSection Build()
        {
            return new DensAsiDsmIvSection(
               _alcoholDependenceLessEffectIndicator,
                _drugDependenceLessEffectIndicator,
                _lessEffectIndicatorNote,
                _alcoholDependencePhysicalDistressOnQuittingIndicator,
                _drugDependencePhysicalDistressOnQuittingIndicator,
                _physicalDistressOnQuittingNote,
                _alcoholDependenceUsedMoreIndicator,
                _drugDependenceUsedMoreIndicator,
                _usedMoreIndicatorNote,
                _alcoholDependenceWantedToCutDownIndicator,
                _drugDependenceWantedToCutDownIndicator,
                _wantedToCutDownIndicatorNote,
                _alcoholDependenceTimeSpentObtainingIndicator,
                _drugDependenceTimeSpentObtainingIndicator,
                _timeSpentObtainingIndicatorNote,
                _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator,
                _drugDependenceGiveUpWorkFamilyActivitiesIndicator,
                _giveUpWorkFamilyActivitiesIndicatorNote,
                _alcoholDependenceContinuedToUseIndicator,
                _drugDependenceContinuedToUseIndicator,
                _continuedToUseIndicatorNote,
                _sectionNote,
                _alcoholAbuseInabilityToMeetResponsibilitiesIndicator,
                _drugAbuseInabilityToMeetResponsibilitiesIndicator,
                _inabilityToMeetResponsibilitiesIndicatorNote,
                _alcoholAbuseVoluntaryPhysicalDangerIndicator,
                _drugAbuseVoluntaryPhysicalDangerIndicator,
                _voluntaryPhysicalDangerIndicatorNote,
                _alcoholAbuseLegalProblemsIndicator,
                _drugAbuseLegalProblemsIndicator,
                _legalProblemsIndicatorNote,
                _alcoholAbuseSocialProblemsIndicator,
                _drugAbuseSocialProblemsIndicator,
                _socialProblemsIndicatorNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiDsmIvSectionBuilder"/> to <see cref="DensAsiDsmIvSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiDsmIvSection(DensAsiDsmIvSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
