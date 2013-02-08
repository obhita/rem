using System;
using System.Linq;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiDsmIvSection contains patient DSM-IV dependence questions from the DSM-IV section of the DensAsi. 
    /// </summary>
    [Component]
    public class DensAsiDsmIvSection : DensAsiInterviewSectionBase
    {
        private readonly DensAsiNonResponseType<bool?> _alcoholAbuseInabilityToMeetResponsibilitiesIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholAbuseLegalProblemsIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholAbuseSocialProblemsIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholAbuseVoluntaryPhysicalDangerIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholDependenceContinuedToUseIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholDependenceLessEffectIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholDependencePhysicalDistressOnQuittingIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholDependenceTimeSpentObtainingIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholDependenceUsedMoreIndicator;
        private readonly DensAsiNonResponseType<bool?> _alcoholDependenceWantedToCutDownIndicator;
        private readonly string _continuedToUseIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _drugAbuseInabilityToMeetResponsibilitiesIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugAbuseLegalProblemsIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugAbuseSocialProblemsIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugAbuseVoluntaryPhysicalDangerIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugDependenceContinuedToUseIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugDependenceGiveUpWorkFamilyActivitiesIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugDependenceLessEffectIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugDependencePhysicalDistressOnQuittingIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugDependenceTimeSpentObtainingIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugDependenceUsedMoreIndicator;
        private readonly DensAsiNonResponseType<bool?> _drugDependenceWantedToCutDownIndicator;
        private readonly string _giveUpWorkFamilyActivitiesIndicatorNote;
        private readonly string _inabilityToMeetResponsibilitiesIndicatorNote;
        private readonly string _legalProblemsIndicatorNote;
        private readonly string _lessEffectIndicatorNote;
        private readonly string _physicalDistressOnQuittingNote;
        private readonly string _sectionNote;
        private readonly string _socialProblemsIndicatorNote;
        private readonly string _timeSpentObtainingIndicatorNote;
        private readonly string _usedMoreIndicatorNote;
        private readonly string _voluntaryPhysicalDangerIndicatorNote;
        private readonly string _wantedToCutDownIndicatorNote;

        private DensAsiDsmIvSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiDsmIvSection"/> class.
        /// </summary>
        /// <param name="alcoholDependenceLessEffectIndicator">The alcohol dependence less effect indicator.</param>
        /// <param name="drugDependenceLessEffectIndicator">The drug dependence less effect indicator.</param>
        /// <param name="lessEffectIndicatorNote">The less effect indicator note.</param>
        /// <param name="alcoholDependencePhysicalDistressOnQuittingIndicator">The alcohol dependence physical distress on quitting indicator.</param>
        /// <param name="drugDependencePhysicalDistressOnQuittingIndicator">The drug dependence physical distress on quitting indicator.</param>
        /// <param name="physicalDistressOnQuittingNote">The physical distress on quitting note.</param>
        /// <param name="alcoholDependenceUsedMoreIndicator">The alcohol dependence used more indicator.</param>
        /// <param name="drugDependenceUsedMoreIndicator">The drug dependence used more indicator.</param>
        /// <param name="usedMoreIndicatorNote">The used more indicator note.</param>
        /// <param name="alcoholDependenceWantedToCutDownIndicator">The alcohol dependence wanted to cut down indicator.</param>
        /// <param name="drugDependenceWantedToCutDownIndicator">The drug dependence wanted to cut down indicator.</param>
        /// <param name="wantedToCutDownIndicatorNote">The wanted to cut down indicator note.</param>
        /// <param name="alcoholDependenceTimeSpentObtainingIndicator">The alcohol dependence time spent obtaining indicator.</param>
        /// <param name="drugDependenceTimeSpentObtainingIndicator">The drug dependence time spent obtaining indicator.</param>
        /// <param name="timeSpentObtainingIndicatorNote">The time spent obtaining indicator note.</param>
        /// <param name="alcoholDependenceGiveUpWorkFamilyActivitiesIndicator">The alcohol dependence give up work family activities indicator.</param>
        /// <param name="drugDependenceGiveUpWorkFamilyActivitiesIndicator">The drug dependence give up work family activities indicator.</param>
        /// <param name="giveUpWorkFamilyActivitiesIndicatorNote">The give up work family activities indicator note.</param>
        /// <param name="alcoholDependenceContinuedToUseIndicator">The alcohol dependence continued to use indicator.</param>
        /// <param name="drugDependenceContinuedToUseIndicator">The drug dependence continued to use indicator.</param>
        /// <param name="continuedToUseIndicatorNote">The continued to use indicator note.</param>
        /// <param name="sectionNote">The section note.</param>
        /// <param name="alcoholAbuseInabilityToMeetResponsibilitiesIndicator">The alcohol abuse inability to meet responsibilities indicator.</param>
        /// <param name="drugAbuseInabilityToMeetResponsibilitiesIndicator">The drug abuse inability to meet responsibilities indicator.</param>
        /// <param name="inabilityToMeetResponsibilitiesIndicatorNote">The inability to meet responsibilities indicator note.</param>
        /// <param name="alcoholAbuseVoluntaryPhysicalDangerIndicator">The alcohol abuse voluntary physical danger indicator.</param>
        /// <param name="drugAbuseVoluntaryPhysicalDangerIndicator">The drug abuse voluntary physical danger indicator.</param>
        /// <param name="voluntaryPhysicalDangerIndicatorNote">The voluntary physical danger indicator note.</param>
        /// <param name="alcoholAbuseLegalProblemsIndicator">The alcohol abuse legal problems indicator.</param>
        /// <param name="drugAbuseLegalProblemsIndicator">The drug abuse legal problems indicator.</param>
        /// <param name="legalProblemsIndicatorNote">The legal problems indicator note.</param>
        /// <param name="alcoholAbuseSocialProblemsIndicator">The alcohol abuse social problems indicator.</param>
        /// <param name="drugAbuseSocialProblemsIndicator">The drug abuse social problems indicator.</param>
        /// <param name="socialProblemsIndicatorNote">The social problems indicator note.</param>
        public DensAsiDsmIvSection(DensAsiNonResponseType<bool?> alcoholDependenceLessEffectIndicator,
                                       DensAsiNonResponseType<bool?> drugDependenceLessEffectIndicator,
                                       string lessEffectIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholDependencePhysicalDistressOnQuittingIndicator,
                                       DensAsiNonResponseType<bool?> drugDependencePhysicalDistressOnQuittingIndicator,
                                       string physicalDistressOnQuittingNote,
                                       DensAsiNonResponseType<bool?> alcoholDependenceUsedMoreIndicator,
                                       DensAsiNonResponseType<bool?> drugDependenceUsedMoreIndicator,
                                       string usedMoreIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholDependenceWantedToCutDownIndicator,
                                       DensAsiNonResponseType<bool?> drugDependenceWantedToCutDownIndicator,
                                       string wantedToCutDownIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholDependenceTimeSpentObtainingIndicator,
                                       DensAsiNonResponseType<bool?> drugDependenceTimeSpentObtainingIndicator,
                                       string timeSpentObtainingIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholDependenceGiveUpWorkFamilyActivitiesIndicator,
                                       DensAsiNonResponseType<bool?> drugDependenceGiveUpWorkFamilyActivitiesIndicator,
                                       string giveUpWorkFamilyActivitiesIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholDependenceContinuedToUseIndicator,
                                       DensAsiNonResponseType<bool?> drugDependenceContinuedToUseIndicator,
                                       string continuedToUseIndicatorNote,
                                       string sectionNote,
                                       DensAsiNonResponseType<bool?> alcoholAbuseInabilityToMeetResponsibilitiesIndicator,
                                       DensAsiNonResponseType<bool?> drugAbuseInabilityToMeetResponsibilitiesIndicator,
                                       string inabilityToMeetResponsibilitiesIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholAbuseVoluntaryPhysicalDangerIndicator,
                                       DensAsiNonResponseType<bool?> drugAbuseVoluntaryPhysicalDangerIndicator,
                                       string voluntaryPhysicalDangerIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholAbuseLegalProblemsIndicator,
                                       DensAsiNonResponseType<bool?> drugAbuseLegalProblemsIndicator,
                                       string legalProblemsIndicatorNote,
                                       DensAsiNonResponseType<bool?> alcoholAbuseSocialProblemsIndicator,
                                       DensAsiNonResponseType<bool?> drugAbuseSocialProblemsIndicator,
                                       string socialProblemsIndicatorNote )
        {
            if ( alcoholDependenceLessEffectIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDependenceLessEffectIndicator ).Contains ( alcoholDependenceLessEffectIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDependenceLessEffectIndicator DensAsiNonResponse value '" + alcoholDependenceLessEffectIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDependenceLessEffectIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDependenceLessEffectIndicator ).Contains ( drugDependenceLessEffectIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDependenceLessEffectIndicator DensAsiNonResponse value '" + drugDependenceLessEffectIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDependencePhysicalDistressOnQuittingIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDependencePhysicalDistressOnQuittingIndicator ).Contains ( alcoholDependencePhysicalDistressOnQuittingIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDependencePhysicalDistressOnQuittingIndicator DensAsiNonResponse value '" + alcoholDependencePhysicalDistressOnQuittingIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDependencePhysicalDistressOnQuittingIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDependencePhysicalDistressOnQuittingIndicator ).Contains ( drugDependencePhysicalDistressOnQuittingIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDependencePhysicalDistressOnQuittingIndicator DensAsiNonResponse value '" + drugDependencePhysicalDistressOnQuittingIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDependenceUsedMoreIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDependenceUsedMoreIndicator ).Contains ( alcoholDependenceUsedMoreIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDependenceUsedMoreIndicator DensAsiNonResponse value '" + alcoholDependenceUsedMoreIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDependenceUsedMoreIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDependenceUsedMoreIndicator ).Contains ( drugDependenceUsedMoreIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDependenceUsedMoreIndicator DensAsiNonResponse value '" + drugDependenceUsedMoreIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDependenceWantedToCutDownIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDependenceWantedToCutDownIndicator ).Contains ( alcoholDependenceWantedToCutDownIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDependenceWantedToCutDownIndicator DensAsiNonResponse value '" + alcoholDependenceWantedToCutDownIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDependenceWantedToCutDownIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDependenceWantedToCutDownIndicator ).Contains ( drugDependenceWantedToCutDownIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDependenceWantedToCutDownIndicator DensAsiNonResponse value '" + drugDependenceWantedToCutDownIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDependenceTimeSpentObtainingIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDependenceTimeSpentObtainingIndicator ).Contains ( alcoholDependenceTimeSpentObtainingIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDependenceTimeSpentObtainingIndicator DensAsiNonResponse value '" + alcoholDependenceTimeSpentObtainingIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDependenceTimeSpentObtainingIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDependenceTimeSpentObtainingIndicator ).Contains ( drugDependenceTimeSpentObtainingIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDependenceTimeSpentObtainingIndicator DensAsiNonResponse value '" + drugDependenceTimeSpentObtainingIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDependenceGiveUpWorkFamilyActivitiesIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDependenceGiveUpWorkFamilyActivitiesIndicator ).Contains ( alcoholDependenceGiveUpWorkFamilyActivitiesIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDependenceGiveUpWorkFamilyActivitiesIndicator DensAsiNonResponse value '" + alcoholDependenceGiveUpWorkFamilyActivitiesIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDependenceGiveUpWorkFamilyActivitiesIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDependenceGiveUpWorkFamilyActivitiesIndicator ).Contains ( drugDependenceGiveUpWorkFamilyActivitiesIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDependenceGiveUpWorkFamilyActivitiesIndicator DensAsiNonResponse value '" + drugDependenceGiveUpWorkFamilyActivitiesIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDependenceContinuedToUseIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDependenceContinuedToUseIndicator ).Contains ( alcoholDependenceContinuedToUseIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDependenceContinuedToUseIndicator DensAsiNonResponse value '" + alcoholDependenceContinuedToUseIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDependenceContinuedToUseIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDependenceContinuedToUseIndicator ).Contains ( drugDependenceContinuedToUseIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDependenceContinuedToUseIndicator DensAsiNonResponse value '" + drugDependenceContinuedToUseIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholAbuseInabilityToMeetResponsibilitiesIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholAbuseInabilityToMeetResponsibilitiesIndicator ).Contains ( alcoholAbuseInabilityToMeetResponsibilitiesIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholAbuseInabilityToMeetResponsibilitiesIndicator DensAsiNonResponse value '" + alcoholAbuseInabilityToMeetResponsibilitiesIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugAbuseInabilityToMeetResponsibilitiesIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugAbuseInabilityToMeetResponsibilitiesIndicator ).Contains ( drugAbuseInabilityToMeetResponsibilitiesIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugAbuseInabilityToMeetResponsibilitiesIndicator DensAsiNonResponse value '" + drugAbuseInabilityToMeetResponsibilitiesIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholAbuseVoluntaryPhysicalDangerIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholAbuseVoluntaryPhysicalDangerIndicator ).Contains ( alcoholAbuseVoluntaryPhysicalDangerIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholAbuseVoluntaryPhysicalDangerIndicator DensAsiNonResponse value '" + alcoholAbuseVoluntaryPhysicalDangerIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugAbuseVoluntaryPhysicalDangerIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugAbuseVoluntaryPhysicalDangerIndicator ).Contains ( drugAbuseVoluntaryPhysicalDangerIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugAbuseVoluntaryPhysicalDangerIndicator DensAsiNonResponse value '" + drugAbuseVoluntaryPhysicalDangerIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholAbuseLegalProblemsIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholAbuseLegalProblemsIndicator ).Contains ( alcoholAbuseLegalProblemsIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholAbuseLegalProblemsIndicator DensAsiNonResponse value '" + alcoholAbuseLegalProblemsIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugAbuseLegalProblemsIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugAbuseLegalProblemsIndicator ).Contains ( drugAbuseLegalProblemsIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugAbuseLegalProblemsIndicator DensAsiNonResponse value '" + drugAbuseLegalProblemsIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholAbuseSocialProblemsIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholAbuseSocialProblemsIndicator ).Contains ( alcoholAbuseSocialProblemsIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholAbuseSocialProblemsIndicator DensAsiNonResponse value '" + alcoholAbuseSocialProblemsIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugAbuseSocialProblemsIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugAbuseSocialProblemsIndicator ).Contains ( drugAbuseSocialProblemsIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugAbuseSocialProblemsIndicator DensAsiNonResponse value '" + drugAbuseSocialProblemsIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _alcoholDependenceLessEffectIndicator = alcoholDependenceLessEffectIndicator;
            _drugDependenceLessEffectIndicator = drugDependenceLessEffectIndicator;
            _lessEffectIndicatorNote = lessEffectIndicatorNote;
            _alcoholDependencePhysicalDistressOnQuittingIndicator = alcoholDependencePhysicalDistressOnQuittingIndicator;
            _drugDependencePhysicalDistressOnQuittingIndicator = drugDependencePhysicalDistressOnQuittingIndicator;
            _physicalDistressOnQuittingNote = physicalDistressOnQuittingNote;
            _alcoholDependenceUsedMoreIndicator = alcoholDependenceUsedMoreIndicator;
            _drugDependenceUsedMoreIndicator = drugDependenceUsedMoreIndicator;
            _usedMoreIndicatorNote = usedMoreIndicatorNote;
            _alcoholDependenceWantedToCutDownIndicator = alcoholDependenceWantedToCutDownIndicator;
            _drugDependenceWantedToCutDownIndicator = drugDependenceWantedToCutDownIndicator;
            _wantedToCutDownIndicatorNote = wantedToCutDownIndicatorNote;
            _alcoholDependenceTimeSpentObtainingIndicator = alcoholDependenceTimeSpentObtainingIndicator;
            _drugDependenceTimeSpentObtainingIndicator = drugDependenceTimeSpentObtainingIndicator;
            _timeSpentObtainingIndicatorNote = timeSpentObtainingIndicatorNote;
            _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator = alcoholDependenceGiveUpWorkFamilyActivitiesIndicator;
            _drugDependenceGiveUpWorkFamilyActivitiesIndicator = drugDependenceGiveUpWorkFamilyActivitiesIndicator;
            _giveUpWorkFamilyActivitiesIndicatorNote = giveUpWorkFamilyActivitiesIndicatorNote;
            _alcoholDependenceContinuedToUseIndicator = alcoholDependenceContinuedToUseIndicator;
            _drugDependenceContinuedToUseIndicator = drugDependenceContinuedToUseIndicator;
            _continuedToUseIndicatorNote = continuedToUseIndicatorNote;
            _sectionNote = sectionNote;
            _alcoholAbuseInabilityToMeetResponsibilitiesIndicator = alcoholAbuseInabilityToMeetResponsibilitiesIndicator;
            _drugAbuseInabilityToMeetResponsibilitiesIndicator = drugAbuseInabilityToMeetResponsibilitiesIndicator;
            _inabilityToMeetResponsibilitiesIndicatorNote = inabilityToMeetResponsibilitiesIndicatorNote;
            _alcoholAbuseVoluntaryPhysicalDangerIndicator = alcoholAbuseVoluntaryPhysicalDangerIndicator;
            _drugAbuseVoluntaryPhysicalDangerIndicator = drugAbuseVoluntaryPhysicalDangerIndicator;
            _voluntaryPhysicalDangerIndicatorNote = voluntaryPhysicalDangerIndicatorNote;
            _alcoholAbuseLegalProblemsIndicator = alcoholAbuseLegalProblemsIndicator;
            _drugAbuseLegalProblemsIndicator = drugAbuseLegalProblemsIndicator;
            _legalProblemsIndicatorNote = legalProblemsIndicatorNote;
            _alcoholAbuseSocialProblemsIndicator = alcoholAbuseSocialProblemsIndicator;
            _drugAbuseSocialProblemsIndicator = drugAbuseSocialProblemsIndicator;
            _socialProblemsIndicatorNote = socialProblemsIndicatorNote;
        }

        /// <summary>
        /// Gets  a boolean value indicating alcohol dependence less effect.
        /// Question Number: 1.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholDependenceLessEffectIndicator
        {
            get { return _alcoholDependenceLessEffectIndicator; }
            private set { } 
        }

        /// <summary>
        /// Gets a boolean value indicating drug dependence less effect.
        /// Question Number: 1.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugDependenceLessEffectIndicator
        {
            get { return _drugDependenceLessEffectIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating less effect indicator note.
        /// Question Number: 1.
        /// </summary>
        public virtual string LessEffectIndicatorNote
        {
            get { return _lessEffectIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol dependence physical distress on quitting.
        /// Question Number: 2.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholDependencePhysicalDistressOnQuittingIndicator
        {
            get { return _alcoholDependencePhysicalDistressOnQuittingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug dependence physical distress on quitting.
        /// Question Number: 2.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugDependencePhysicalDistressOnQuittingIndicator
        {
            get { return _drugDependencePhysicalDistressOnQuittingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the physical distress on quitting note.
        /// Question Number: 2.
        /// </summary>
        public virtual string PhysicalDistressOnQuittingNote
        {
            get { return _physicalDistressOnQuittingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol dependence used more.
        /// Question Number: 3.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholDependenceUsedMoreIndicator
        {
            get { return _alcoholDependenceUsedMoreIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug dependence used more.
        /// Question Number: 3.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugDependenceUsedMoreIndicator
        {
            get { return _drugDependenceUsedMoreIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating used more note.
        /// Question Number : 3.
        /// </summary>
        public virtual string UsedMoreIndicatorNote
        {
            get { return _usedMoreIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol dependence wanted to cut down.
        /// Question Number: 4.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholDependenceWantedToCutDownIndicator
        {
            get { return _alcoholDependenceWantedToCutDownIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug dependence wanted to cut down.
        /// Question Number: 4.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugDependenceWantedToCutDownIndicator
        {
            get { return _drugDependenceWantedToCutDownIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating wanted to cut down note.
        /// Question Number : 4.
        /// </summary>
        public virtual string WantedToCutDownIndicatorNote
        {
            get { return _wantedToCutDownIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol dependence time spent obtaining.
        /// Question Number: 5.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholDependenceTimeSpentObtainingIndicator
        {
            get { return _alcoholDependenceTimeSpentObtainingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug dependence time spent obtaining.
        /// Question Number: 5.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugDependenceTimeSpentObtainingIndicator
        {
            get { return _drugDependenceTimeSpentObtainingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating time spent obtaining note.
        /// Question Number : 5.
        /// </summary>
        public virtual string TimeSpentObtainingIndicatorNote
        {
            get { return _timeSpentObtainingIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol dependence give up work family activities.
        /// Question Number: 6.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholDependenceGiveUpWorkFamilyActivitiesIndicator
        {
            get { return _alcoholDependenceGiveUpWorkFamilyActivitiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug dependence give up work family activities.
        /// Question Number: 6.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugDependenceGiveUpWorkFamilyActivitiesIndicator
        {
            get { return _drugDependenceGiveUpWorkFamilyActivitiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating give up work family activities note.
        /// Question Number : 6.
        /// </summary>
        public virtual string GiveUpWorkFamilyActivitiesIndicatorNote
        {
            get { return _giveUpWorkFamilyActivitiesIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol dependence continued to use.
        /// Question Number: 7.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholDependenceContinuedToUseIndicator
        {
            get { return _alcoholDependenceContinuedToUseIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug dependence continued to use.
        /// Question Number: 7.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugDependenceContinuedToUseIndicator
        {
            get { return _drugDependenceContinuedToUseIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating continued to use note.
        /// Question Number : 7.
        /// </summary>
        public virtual string ContinuedToUseIndicatorNote
        {
            get { return _continuedToUseIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the section note.
        /// </summary>
        public virtual string SectionNote
        {
            get { return _sectionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol abuse inability to meet responsibilities.
        /// Question Number: 1 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholAbuseInabilityToMeetResponsibilitiesIndicator
        {
            get { return _alcoholAbuseInabilityToMeetResponsibilitiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug abuse inability to meet responsibilities.
        /// Question Number: 1 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugAbuseInabilityToMeetResponsibilitiesIndicator
        {
            get { return _drugAbuseInabilityToMeetResponsibilitiesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating inability to meet responsibilities note.
        /// Question Number : 1 (Additional).
        /// </summary>
        public virtual string InabilityToMeetResponsibilitiesIndicatorNote
        {
            get { return _inabilityToMeetResponsibilitiesIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol abuse voluntary physical danger.
        /// Question Number: 2 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholAbuseVoluntaryPhysicalDangerIndicator
        {
            get { return _alcoholAbuseVoluntaryPhysicalDangerIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug abuse voluntary physical danger.
        /// Question Number: 2 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugAbuseVoluntaryPhysicalDangerIndicator
        {
            get { return _drugAbuseVoluntaryPhysicalDangerIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating voluntary physical danger note.
        /// Question Number : 2 (Additional).
        /// </summary>
        public virtual string VoluntaryPhysicalDangerIndicatorNote
        {
            get { return _voluntaryPhysicalDangerIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol abuse legal problems.
        /// Question Number: 3 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholAbuseLegalProblemsIndicator
        {
            get { return _alcoholAbuseLegalProblemsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug abuse legal problems.
        /// Question Number: 3 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugAbuseLegalProblemsIndicator
        {
            get { return _drugAbuseLegalProblemsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating legal problems note.
        /// Question Number : 3 (Additional).
        /// </summary>
        public virtual string LegalProblemsIndicatorNote
        {
            get { return _legalProblemsIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating alcohol abuse social problems.
        /// Question Number: 4 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AlcoholAbuseSocialProblemsIndicator
        {
            get { return _alcoholAbuseSocialProblemsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating drug abuse social problems.
        /// Question Number: 4 (Additional).
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DrugAbuseSocialProblemsIndicator
        {
            get { return _drugAbuseSocialProblemsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating social problems note.
        /// Question Number : 4 (Additional).
        /// </summary>
        public virtual string SocialProblemsIndicatorNote
        {
            get { return _socialProblemsIndicatorNote; }
            private set { }
        }
    }
}