namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiDrugAlcoholUseSectionBuilder provides a fluent interface for creating a DesAsiDrugAlcohol section.
    /// </summary>
    public class DensAsiDrugAlcoholUseSectionBuilder
    {
        private DensAsiNonResponseType<int?> _anyAlcoholUseInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _anyAlcoholUseInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _anyAlcoholDensAsiDrugAlcoholAdministrationRoute;
        private string _anyAlcoholUseNote;
        private DensAsiNonResponseType<int?> _alcoholIntoxicationInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _alcoholIntoxicationUseInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute;
        private string _alcoholIntoxicationNote;
        private DensAsiNonResponseType<int?> _heroinInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _heroinInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _heroinDensAsiDrugAlcoholAdministrationRoute;
        private string _heroinNote;
        private DensAsiNonResponseType<int?> _methadoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _methadoneInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _methadoneDensAsiDrugAlcoholAdministrationRoute;
        private string _methadoneNote;
        private DensAsiNonResponseType<int?> _otherOpiatesInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _otherOpiatesInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _otherOpiatesDensAsiDrugAlcoholAdministrationRoute;
        private string _otherOpiatesNote;
        private DensAsiNonResponseType<int?> _barbituratesInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _barbituratesInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _barbituratesDensAsiDrugAlcoholAdministrationRoute;
        private string _barbituratesNote;
        private DensAsiNonResponseType<int?> _otherSedativesInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _otherSedativesInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _otherSedativesDensAsiDrugAlcoholAdministrationRoute;
        private string _otherSedativesNote;
        private DensAsiNonResponseType<int?> _cocaineInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _cocaineInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _cocaineDensAsiDrugAlcoholAdministrationRoute;
        private string _cocaineNote;
        private DensAsiNonResponseType<int?> _amphetaminesInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _amphetaminesInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _amphetaminesDensAsiDrugAlcoholAdministrationRoute;
        private string _amphetaminesNote;
        private DensAsiNonResponseType<int?> _cannabisInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _cannabisInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _cannabisDensAsiDrugAlcoholAdministrationRoute;
        private string _cannabisNote;
        private DensAsiNonResponseType<int?> _hallucinogensInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _hallucinogensInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _hallucinogensDensAsiDrugAlcoholAdministrationRoute;
        private string _hallucinogensNote;
        private DensAsiNonResponseType<int?> _inhalantsInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _inhalantsInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _inhalantsDensAsiDrugAlcoholAdministrationRoute;
        private string _inhalantsNote;
        private DensAsiNonResponseType<int?> _moreThanOneSubstancePerDayInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _moreThanOneSubstancePerDayInLifetimeYearCount;
        private string _moreThanOneSubstancePerDayNote;
        private DensAsiNonResponseType<DensAsiProblematicSubstance> _majorDensAsiProblematicSubstance;
        private string _majorDensAsiProblematicSubstanceNote;
        private DensAsiNonResponseType<int?> _voluntaryAbstinenceFromProblematicSubstanceMonthCount;
        private string _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote;
        private DensAsiNonResponseType<int?> _endOfProblematicSubstanceAbstinenceMonthCount;
        private string _endOfProblematicSubstanceAbstinenceMonthCountNote;
        private DensAsiNonResponseType<int?> _alcoholDtCount;
        private string _alcoholDtCountNote;
        private DensAsiNonResponseType<int?> _overdosedOnDrugsCount;
        private string _overdosedOnDrugsCountNote;
        private DensAsiNonResponseType<int?> _alcoholAbuseTreatmentCount;
        private string _alcoholAbuseTreatmentCountNote;
        private DensAsiNonResponseType<int?> _alcoholDetoxTreatmentOnlyCount;
        private string _alcoholDetoxTreatmentOnlyCountNote;
        private DensAsiNonResponseType<int?> _moneySpentOnAlcoholInLastThirtyDaysAmount;
        private string _moneySpentOnAlcoholInLastThirtyDaysAmountNote;
        private DensAsiNonResponseType<int?> _drugAbuseTreatmentCount;
        private string _drugAbuseTreatmentCountNote;
        private DensAsiNonResponseType<int?> _drugDetoxTreatmentOnlyCount;
        private string _drugDetoxTreatmentOnlyCountNote;
        private DensAsiNonResponseType<int?> _moneySpentOnDrugsInLastThirtyDaysAmount;
        private string _moneySpentOnDrugsInLastThirtyDaysAmountNote;
        private DensAsiNonResponseType<int?> _outpatientTreatmentInLastThirtyDaysDayCount;
        private string _outpatientTreatmentInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<int?> _alcoholProblemInLastThirtyDaysDayCount;
        private string _alcoholProblemInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _troubledByAlcoholProblemsDensAsiPatientRating;
        private string _troubledByAlcoholProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfAlcoholProblemTreatmentDensAsiPatientRating;
        private string _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote;
        private DensAsiNonResponseType<int?> _drugProblemInLastThirtyDaysDayCount;
        private string _drugProblemInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _troubledByDrugProblemsDensAsiPatientRating;
        private string _troubledByDrugProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfDrugProblemTreatmentDensAsiPatientRating;
        private string _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote;
        private DensAsiInterviewerRating _patientAlcoholTreatmentDensAsiInterviewerRating;
        private string _patientAlcoholTreatmentDensAsiInterviewerRatingNote;
        private DensAsiInterviewerRating _patientDrugTreatmentDensAsiInterviewerRating;
        private string _patientDrugTreatmentDensAsiInterviewerRatingNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private DensAsiNonResponseType<int?> _hydromorphoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _hydromorphoneInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _hydromorphoneDensAsiDrugAlcoholAdministrationRoute;
        private string _hydromorphoneNote;
        private DensAsiNonResponseType<int?> _oxycodoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _oxycodoneInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _oxycodoneDensAsiDrugAlcoholAdministrationRoute;
        private string _oxycodoneNote;
        private DensAsiNonResponseType<int?> _hydrocodoneInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _hydrocodoneInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _hydrocodoneDensAsiDrugAlcoholAdministrationRoute;
        private string _hydrocodoneNote;
        private DensAsiNonResponseType<int?> _buprenorphineInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _buprenorphineInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _buprenorphineDensAsiDrugAlcoholAdministrationRoute;
        private string _buprenorphineNote;
        private DensAsiNonResponseType<int?> _oxyContinInLastThirtyDaysDayCount;
        private DensAsiNonResponseType<int?> _oxyContinInLifetimeYearCount;
        private DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _oxyContinDensAsiDrugAlcoholAdministrationRoute;
        private string _oxyContinNote;
        private DensAsiNonResponseType<bool?> _oxyContinPrescribedForMedicalReasonIndicator;
        private string _oxyContinPrescribedForMedicalReasonIndicatorNote;
        private DensAsiNonResponseType<bool?> _oxyContinUseToGetHighIndicator;
        private string _oxyContinUseToGetHighIndicatorNote;
        private DensAsiNonResponseType<bool?> _oxyContinTakenWithOtherOpiatesIndicator;
        private string _oxyContinTakenWithOtherOpiatesIndicatorNote;
        private DensAsiNonResponseType<int?> _afterOxyContinFirstUseMonthCount;
        private string _afterOxyContinFirstUseMonthCountNote;
        private DensAsiNonResponseType<bool?> _oxyContinFromFriendFamilyStreetIndicator;
        private string _oxyContinFromFriendFamilyStreetIndicatorNote;
        private string _sectionNote;


        /// <summary>
        /// Assigns any alcohol use in last thirty days day count.
        /// </summary>
        /// <param name="anyAlcoholUseInLastThirtyDaysDayCount">Any alcohol use in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAnyAlcoholUseInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> anyAlcoholUseInLastThirtyDaysDayCount)
        {
            _anyAlcoholUseInLastThirtyDaysDayCount = anyAlcoholUseInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns any alcohol use in lifetime year count.
        /// </summary>
        /// <param name="anyAlcoholUseInLifetimeYearCount">Any alcohol use in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAnyAlcoholUseInLifetimeYearCount(DensAsiNonResponseType<int?> anyAlcoholUseInLifetimeYearCount)
        {
            _anyAlcoholUseInLifetimeYearCount = anyAlcoholUseInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns any alcohol DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="anyAlcoholDensAsiDrugAlcoholAdministrationRoute">Any alcohol DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAnyAlcoholDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> anyAlcoholDensAsiDrugAlcoholAdministrationRoute)
        {
            _anyAlcoholDensAsiDrugAlcoholAdministrationRoute = anyAlcoholDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns any alcohol use note.
        /// </summary>
        /// <param name="anyAlcoholUseNote">Any alcohol use note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAnyAlcoholUseNote(string anyAlcoholUseNote)
        {
            _anyAlcoholUseNote = anyAlcoholUseNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol intoxication in last thirty days day count.
        /// </summary>
        /// <param name="alcoholIntoxicationInLastThirtyDaysDayCount">The alcohol intoxication in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholIntoxicationInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> alcoholIntoxicationInLastThirtyDaysDayCount)
        {
            _alcoholIntoxicationInLastThirtyDaysDayCount = alcoholIntoxicationInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol intoxication use in lifetime year count.
        /// </summary>
        /// <param name="alcoholIntoxicationUseInLifetimeYearCount">The alcohol intoxication use in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholIntoxicationUseInLifetimeYearCount(DensAsiNonResponseType<int?> alcoholIntoxicationUseInLifetimeYearCount)
        {
            _alcoholIntoxicationUseInLifetimeYearCount = alcoholIntoxicationUseInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol intoxication DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute">The alcohol intoxication DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute)
        {
            _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute = alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol intoxication note.
        /// </summary>
        /// <param name="alcoholIntoxicationNote">The alcohol intoxication note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholIntoxicationNote(string alcoholIntoxicationNote)
        {
            _alcoholIntoxicationNote = alcoholIntoxicationNote;
            return this;
        }

        /// <summary>
        /// Assigns the heroin in last thirty days day count.
        /// </summary>
        /// <param name="heroinInLastThirtyDaysDayCount">The heroin in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHeroinInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> heroinInLastThirtyDaysDayCount)
        {
            _heroinInLastThirtyDaysDayCount = heroinInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the heroin in lifetime year count.
        /// </summary>
        /// <param name="heroinInLifetimeYearCount">The heroin in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHeroinInLifetimeYearCount(DensAsiNonResponseType<int?> heroinInLifetimeYearCount)
        {
            _heroinInLifetimeYearCount = heroinInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the heroin DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="heroinDensAsiDrugAlcoholAdministrationRoute">The heroin DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHeroinDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> heroinDensAsiDrugAlcoholAdministrationRoute)
        {
            _heroinDensAsiDrugAlcoholAdministrationRoute = heroinDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the heroin note.
        /// </summary>
        /// <param name="heroinNote">The heroin note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHeroinNote(string heroinNote)
        {
            _heroinNote = heroinNote;
            return this;
        }

        /// <summary>
        /// Assigns the methadone in last thirty days day count.
        /// </summary>
        /// <param name="methadoneInLastThirtyDaysDayCount">The methadone in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMethadoneInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> methadoneInLastThirtyDaysDayCount)
        {
            _methadoneInLastThirtyDaysDayCount = methadoneInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the methadone in lifetime year count.
        /// </summary>
        /// <param name="methadoneInLifetimeYearCount">The methadone in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMethadoneInLifetimeYearCount(DensAsiNonResponseType<int?> methadoneInLifetimeYearCount)
        {
            _methadoneInLifetimeYearCount = methadoneInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the methadone DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="methadoneDensAsiDrugAlcoholAdministrationRoute">The methadone DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMethadoneDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> methadoneDensAsiDrugAlcoholAdministrationRoute)
        {
            _methadoneDensAsiDrugAlcoholAdministrationRoute = methadoneDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the methadone note.
        /// </summary>
        /// <param name="methadoneNote">The methadone note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMethadoneNote(string methadoneNote)
        {
            _methadoneNote = methadoneNote;
            return this;
        }

        /// <summary>
        /// Assigns the other opiates in last thirty days day count.
        /// </summary>
        /// <param name="otherOpiatesInLastThirtyDaysDayCount">The other opiates in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherOpiatesInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> otherOpiatesInLastThirtyDaysDayCount)
        {
            _otherOpiatesInLastThirtyDaysDayCount = otherOpiatesInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the other opiates in lifetime year count.
        /// </summary>
        /// <param name="otherOpiatesInLifetimeYearCount">The other opiates in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherOpiatesInLifetimeYearCount(DensAsiNonResponseType<int?> otherOpiatesInLifetimeYearCount)
        {
            _otherOpiatesInLifetimeYearCount = otherOpiatesInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the other opiates DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="otherOpiatesDensAsiDrugAlcoholAdministrationRoute">The other opiates DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherOpiatesDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> otherOpiatesDensAsiDrugAlcoholAdministrationRoute)
        {
            _otherOpiatesDensAsiDrugAlcoholAdministrationRoute = otherOpiatesDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the other opiates note.
        /// </summary>
        /// <param name="otherOpiatesNote">The other opiates note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherOpiatesNote(string otherOpiatesNote)
        {
            _otherOpiatesNote = otherOpiatesNote;
            return this;
        }

        /// <summary>
        /// Assigns the barbiturates in last thirty days day count.
        /// </summary>
        /// <param name="barbituratesInLastThirtyDaysDayCount">The barbiturates in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBarbituratesInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> barbituratesInLastThirtyDaysDayCount)
        {
            _barbituratesInLastThirtyDaysDayCount = barbituratesInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the barbiturates in lifetime year count.
        /// </summary>
        /// <param name="barbituratesInLifetimeYearCount">The barbiturates in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBarbituratesInLifetimeYearCount(DensAsiNonResponseType<int?> barbituratesInLifetimeYearCount)
        {
            _barbituratesInLifetimeYearCount = barbituratesInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the barbiturates DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="barbituratesDensAsiDrugAlcoholAdministrationRoute">The barbiturates DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBarbituratesDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> barbituratesDensAsiDrugAlcoholAdministrationRoute)
        {
            _barbituratesDensAsiDrugAlcoholAdministrationRoute = barbituratesDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the barbiturates note.
        /// </summary>
        /// <param name="barbituratesNote">The barbiturates note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBarbituratesNote(string barbituratesNote)
        {
            _barbituratesNote = barbituratesNote;
            return this;
        }

        /// <summary>
        /// Assigns the other sedatives in last thirty days day count.
        /// </summary>
        /// <param name="otherSedativesInLastThirtyDaysDayCount">The other sedatives in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherSedativesInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> otherSedativesInLastThirtyDaysDayCount)
        {
            _otherSedativesInLastThirtyDaysDayCount = otherSedativesInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the other sedatives in lifetime year count.
        /// </summary>
        /// <param name="otherSedativesInLifetimeYearCount">The other sedatives in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherSedativesInLifetimeYearCount(DensAsiNonResponseType<int?> otherSedativesInLifetimeYearCount)
        {
            _otherSedativesInLifetimeYearCount = otherSedativesInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the other sedatives DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="otherSedativesDensAsiDrugAlcoholAdministrationRoute">The other sedatives DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherSedativesDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> otherSedativesDensAsiDrugAlcoholAdministrationRoute)
        {
            _otherSedativesDensAsiDrugAlcoholAdministrationRoute = otherSedativesDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the other sedatives note.
        /// </summary>
        /// <param name="otherSedativesNote">The other sedatives note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOtherSedativesNote(string otherSedativesNote)
        {
            _otherSedativesNote = otherSedativesNote;
            return this;
        }

        /// <summary>
        /// Assigns the cocaine in last thirty days day count.
        /// </summary>
        /// <param name="cocaineInLastThirtyDaysDayCount">The cocaine in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCocaineInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> cocaineInLastThirtyDaysDayCount)
        {
            _cocaineInLastThirtyDaysDayCount = cocaineInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the cocaine in lifetime year count.
        /// </summary>
        /// <param name="cocaineInLifetimeYearCount">The cocaine in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCocaineInLifetimeYearCount(DensAsiNonResponseType<int?> cocaineInLifetimeYearCount)
        {
            _cocaineInLifetimeYearCount = cocaineInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the cocaine DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="cocaineDensAsiDrugAlcoholAdministrationRoute">The cocaine DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCocaineDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> cocaineDensAsiDrugAlcoholAdministrationRoute)
        {
            _cocaineDensAsiDrugAlcoholAdministrationRoute = cocaineDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the cocaine note.
        /// </summary>
        /// <param name="cocaineNote">The cocaine note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCocaineNote(string cocaineNote)
        {
            _cocaineNote = cocaineNote;
            return this;
        }

        /// <summary>
        /// Assigns the amphetamines in last thirty days day count.
        /// </summary>
        /// <param name="amphetaminesInLastThirtyDaysDayCount">The amphetamines in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAmphetaminesInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> amphetaminesInLastThirtyDaysDayCount)
        {
            _amphetaminesInLastThirtyDaysDayCount = amphetaminesInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the amphetamines in lifetime year count.
        /// </summary>
        /// <param name="amphetaminesInLifetimeYearCount">The amphetamines in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAmphetaminesInLifetimeYearCount(DensAsiNonResponseType<int?> amphetaminesInLifetimeYearCount)
        {
            _amphetaminesInLifetimeYearCount = amphetaminesInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the amphetamines DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="amphetaminesDensAsiDrugAlcoholAdministrationRoute">The amphetamines DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAmphetaminesDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> amphetaminesDensAsiDrugAlcoholAdministrationRoute)
        {
            _amphetaminesDensAsiDrugAlcoholAdministrationRoute = amphetaminesDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the amphetamines note.
        /// </summary>
        /// <param name="amphetaminesNote">The amphetamines note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAmphetaminesNote(string amphetaminesNote)
        {
            _amphetaminesNote = amphetaminesNote;
            return this;
        }

        /// <summary>
        /// Assigns the cannabis in last thirty days day count.
        /// </summary>
        /// <param name="cannabisInLastThirtyDaysDayCount">The cannabis in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCannabisInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> cannabisInLastThirtyDaysDayCount)
        {
            _cannabisInLastThirtyDaysDayCount = cannabisInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the cannabis in lifetime year count.
        /// </summary>
        /// <param name="cannabisInLifetimeYearCount">The cannabis in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCannabisInLifetimeYearCount(DensAsiNonResponseType<int?> cannabisInLifetimeYearCount)
        {
            _cannabisInLifetimeYearCount = cannabisInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the cannabis DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="cannabisDensAsiDrugAlcoholAdministrationRoute">The cannabis DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCannabisDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> cannabisDensAsiDrugAlcoholAdministrationRoute)
        {
            _cannabisDensAsiDrugAlcoholAdministrationRoute = cannabisDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the cannabis note.
        /// </summary>
        /// <param name="cannabisNote">The cannabis note.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithCannabisNote(string cannabisNote)
        {
            _cannabisNote = cannabisNote;
            return this;
        }

        /// <summary>
        /// Assigns the hallucinogens in last thirty days day count.
        /// </summary>
        /// <param name="hallucinogensInLastThirtyDaysDayCount">The hallucinogens in last thirty days day count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHallucinogensInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> hallucinogensInLastThirtyDaysDayCount)
        {
            _hallucinogensInLastThirtyDaysDayCount = hallucinogensInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the hallucinogens in lifetime year count.
        /// </summary>
        /// <param name="hallucinogensInLifetimeYearCount">The hallucinogens in lifetime year count.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHallucinogensInLifetimeYearCount(DensAsiNonResponseType<int?> hallucinogensInLifetimeYearCount)
        {
            _hallucinogensInLifetimeYearCount = hallucinogensInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the hallucinogens DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="hallucinogensDensAsiDrugAlcoholAdministrationRoute">The hallucinogens DensAsi drug alcohol administration route.</param>
        /// <returns><see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholUseSectionBuilder">A DensAsiClosureSectionBuilder.</see></returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHallucinogensDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> hallucinogensDensAsiDrugAlcoholAdministrationRoute)
        {
            _hallucinogensDensAsiDrugAlcoholAdministrationRoute = hallucinogensDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the hallucinogens note.
        /// </summary>
        /// <param name="hallucinogensNote">The hallucinogens note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHallucinogensNote(string hallucinogensNote)
        {
            _hallucinogensNote = hallucinogensNote;
            return this;
        }

        /// <summary>
        /// Assigns the inhalants in last thirty days day count.
        /// </summary>
        /// <param name="inhalantsInLastThirtyDaysDayCount">The inhalants in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithInhalantsInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> inhalantsInLastThirtyDaysDayCount)
        {
            _inhalantsInLastThirtyDaysDayCount = inhalantsInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the inhalants in lifetime year count.
        /// </summary>
        /// <param name="inhalantsInLifetimeYearCount">The inhalants in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithInhalantsInLifetimeYearCount(DensAsiNonResponseType<int?> inhalantsInLifetimeYearCount)
        {
            _inhalantsInLifetimeYearCount = inhalantsInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the inhalants DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="inhalantsDensAsiDrugAlcoholAdministrationRoute">The inhalants DensAsi drug alcohol administration route.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithInhalantsDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> inhalantsDensAsiDrugAlcoholAdministrationRoute)
        {
            _inhalantsDensAsiDrugAlcoholAdministrationRoute = inhalantsDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the inhalants note.
        /// </summary>
        /// <param name="inhalantsNote">The inhalants note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithInhalantsNote(string inhalantsNote)
        {
            _inhalantsNote = inhalantsNote;
            return this;
        }

        /// <summary>
        /// Assigns the more than one substance per day in last thirty days day count.
        /// </summary>
        /// <param name="moreThanOneSubstancePerDayInLastThirtyDaysDayCount">The more than one substance per day in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMoreThanOneSubstancePerDayInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> moreThanOneSubstancePerDayInLastThirtyDaysDayCount)
        {
            _moreThanOneSubstancePerDayInLastThirtyDaysDayCount = moreThanOneSubstancePerDayInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the more than one substance per day in lifetime year count.
        /// </summary>
        /// <param name="moreThanOneSubstancePerDayInLifetimeYearCount">The more than one substance per day in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMoreThanOneSubstancePerDayInLifetimeYearCount(DensAsiNonResponseType<int?> moreThanOneSubstancePerDayInLifetimeYearCount)
        {
            _moreThanOneSubstancePerDayInLifetimeYearCount = moreThanOneSubstancePerDayInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the more than one substance per day note.
        /// </summary>
        /// <param name="moreThanOneSubstancePerDayNote">The more than one substance per day note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMoreThanOneSubstancePerDayNote(string moreThanOneSubstancePerDayNote)
        {
            _moreThanOneSubstancePerDayNote = moreThanOneSubstancePerDayNote;
            return this;
        }

        /// <summary>
        /// Assigns the major DensAsi problematic substance.
        /// </summary>
        /// <param name="majorDensAsiProblematicSubstance">The major DensAsi problematic substance.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMajorDensAsiProblematicSubstance(DensAsiNonResponseType<DensAsiProblematicSubstance> majorDensAsiProblematicSubstance)
        {
            _majorDensAsiProblematicSubstance = majorDensAsiProblematicSubstance;
            return this;
        }

        /// <summary>
        /// Assigns the major DensAsi problematic substance note.
        /// </summary>
        /// <param name="majorDensAsiProblematicSubstanceNote">The major DensAsi problematic substance note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMajorDensAsiProblematicSubstanceNote(string majorDensAsiProblematicSubstanceNote)
        {
            _majorDensAsiProblematicSubstanceNote = majorDensAsiProblematicSubstanceNote;
            return this;
        }

        /// <summary>
        /// Assigns the voluntary abstinence from problematic substance month count.
        /// </summary>
        /// <param name="voluntaryAbstinenceFromProblematicSubstanceMonthCount">The voluntary abstinence from problematic substance month count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithVoluntaryAbstinenceFromProblematicSubstanceMonthCount(DensAsiNonResponseType<int?> voluntaryAbstinenceFromProblematicSubstanceMonthCount)
        {
            _voluntaryAbstinenceFromProblematicSubstanceMonthCount = voluntaryAbstinenceFromProblematicSubstanceMonthCount;
            return this;
        }

        /// <summary>
        /// Assigns the voluntary abstinence from problematic substance month count note.
        /// </summary>
        /// <param name="voluntaryAbstinenceFromProblematicSubstanceMonthCountNote">The voluntary abstinence from problematic substance month count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithVoluntaryAbstinenceFromProblematicSubstanceMonthCountNote(string voluntaryAbstinenceFromProblematicSubstanceMonthCountNote)
        {
            _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote = voluntaryAbstinenceFromProblematicSubstanceMonthCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the end of problematic substance abstinence month count.
        /// </summary>
        /// <param name="endOfProblematicSubstanceAbstinenceMonthCount">The end of problematic substance abstinence month count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithEndOfProblematicSubstanceAbstinenceMonthCount(DensAsiNonResponseType<int?> endOfProblematicSubstanceAbstinenceMonthCount)
        {
            _endOfProblematicSubstanceAbstinenceMonthCount = endOfProblematicSubstanceAbstinenceMonthCount;
            return this;
        }

        /// <summary>
        /// Assigns the end of problematic substance abstinence month count note.
        /// </summary>
        /// <param name="endOfProblematicSubstanceAbstinenceMonthCountNote">The end of problematic substance abstinence month count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithEndOfProblematicSubstanceAbstinenceMonthCountNote(string endOfProblematicSubstanceAbstinenceMonthCountNote)
        {
            _endOfProblematicSubstanceAbstinenceMonthCountNote = endOfProblematicSubstanceAbstinenceMonthCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dt count.
        /// </summary>
        /// <param name="alcoholDtCount">The alcohol dt count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholDtCount(DensAsiNonResponseType<int?> alcoholDtCount)
        {
            _alcoholDtCount = alcoholDtCount;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol dt count note.
        /// </summary>
        /// <param name="alcoholDtCountNote">The alcohol dt count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholDtCountNote(string alcoholDtCountNote)
        {
            _alcoholDtCountNote = alcoholDtCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the overdosed on drugs count.
        /// </summary>
        /// <param name="overdosedOnDrugsCount">The overdosed on drugs count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOverdosedOnDrugsCount(DensAsiNonResponseType<int?> overdosedOnDrugsCount)
        {
            _overdosedOnDrugsCount = overdosedOnDrugsCount;
            return this;
        }

        /// <summary>
        /// Assigns the overdosed on drugs count note.
        /// </summary>
        /// <param name="overdosedOnDrugsCountNote">The overdosed on drugs count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOverdosedOnDrugsCountNote(string overdosedOnDrugsCountNote)
        {
            _overdosedOnDrugsCountNote = overdosedOnDrugsCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol abuse treatment count.
        /// </summary>
        /// <param name="alcoholAbuseTreatmentCount">The alcohol abuse treatment count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholAbuseTreatmentCount(DensAsiNonResponseType<int?> alcoholAbuseTreatmentCount)
        {
            _alcoholAbuseTreatmentCount = alcoholAbuseTreatmentCount;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol abuse treatment count note.
        /// </summary>
        /// <param name="alcoholAbuseTreatmentCountNote">The alcohol abuse treatment count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholAbuseTreatmentCountNote(string alcoholAbuseTreatmentCountNote)
        {
            _alcoholAbuseTreatmentCountNote = alcoholAbuseTreatmentCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol detox treatment only count.
        /// </summary>
        /// <param name="alcoholDetoxTreatmentOnlyCount">The alcohol detox treatment only count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholDetoxTreatmentOnlyCount(DensAsiNonResponseType<int?> alcoholDetoxTreatmentOnlyCount)
        {
            _alcoholDetoxTreatmentOnlyCount = alcoholDetoxTreatmentOnlyCount;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol detox treatment only count note.
        /// </summary>
        /// <param name="alcoholDetoxTreatmentOnlyCountNote">The alcohol detox treatment only count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholDetoxTreatmentOnlyCountNote(string alcoholDetoxTreatmentOnlyCountNote)
        {
            _alcoholDetoxTreatmentOnlyCountNote = alcoholDetoxTreatmentOnlyCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the money spent on alcohol in last thirty days amount.
        /// </summary>
        /// <param name="moneySpentOnAlcoholInLastThirtyDaysAmount">The money spent on alcohol in last thirty days amount.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMoneySpentOnAlcoholInLastThirtyDaysAmount(DensAsiNonResponseType<int?> moneySpentOnAlcoholInLastThirtyDaysAmount)
        {
            _moneySpentOnAlcoholInLastThirtyDaysAmount = moneySpentOnAlcoholInLastThirtyDaysAmount;
            return this;
        }

        /// <summary>
        /// Assigns the money spent on alcohol in last thirty days amount note.
        /// </summary>
        /// <param name="moneySpentOnAlcoholInLastThirtyDaysAmountNote">The money spent on alcohol in last thirty days amount note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMoneySpentOnAlcoholInLastThirtyDaysAmountNote(string moneySpentOnAlcoholInLastThirtyDaysAmountNote)
        {
            _moneySpentOnAlcoholInLastThirtyDaysAmountNote = moneySpentOnAlcoholInLastThirtyDaysAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the drug abuse treatment count.
        /// </summary>
        /// <param name="drugAbuseTreatmentCount">The drug abuse treatment count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithDrugAbuseTreatmentCount(DensAsiNonResponseType<int?> drugAbuseTreatmentCount)
        {
            _drugAbuseTreatmentCount = drugAbuseTreatmentCount;
            return this;
        }

        /// <summary>
        /// Assigns the drug abuse treatment count note.
        /// </summary>
        /// <param name="drugAbuseTreatmentCountNote">The drug abuse treatment count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithDrugAbuseTreatmentCountNote(string drugAbuseTreatmentCountNote)
        {
            _drugAbuseTreatmentCountNote = drugAbuseTreatmentCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the drug detox treatment only count.
        /// </summary>
        /// <param name="drugDetoxTreatmentOnlyCount">The drug detox treatment only count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithDrugDetoxTreatmentOnlyCount(DensAsiNonResponseType<int?> drugDetoxTreatmentOnlyCount)
        {
            _drugDetoxTreatmentOnlyCount = drugDetoxTreatmentOnlyCount;
            return this;
        }

        /// <summary>
        /// Assigns the drug detox treatment only count note.
        /// </summary>
        /// <param name="drugDetoxTreatmentOnlyCountNote">The drug detox treatment only count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithDrugDetoxTreatmentOnlyCountNote(string drugDetoxTreatmentOnlyCountNote)
        {
            _drugDetoxTreatmentOnlyCountNote = drugDetoxTreatmentOnlyCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the money spent on drugs in last thirty days amount.
        /// </summary>
        /// <param name="moneySpentOnDrugsInLastThirtyDaysAmount">The money spent on drugs in last thirty days amount.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMoneySpentOnDrugsInLastThirtyDaysAmount(DensAsiNonResponseType<int?> moneySpentOnDrugsInLastThirtyDaysAmount)
        {
            _moneySpentOnDrugsInLastThirtyDaysAmount = moneySpentOnDrugsInLastThirtyDaysAmount;
            return this;
        }

        /// <summary>
        /// Assigns the money spent on drugs in last thirty days amount note.
        /// </summary>
        /// <param name="moneySpentOnDrugsInLastThirtyDaysAmountNote">The money spent on drugs in last thirty days amount note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithMoneySpentOnDrugsInLastThirtyDaysAmountNote(string moneySpentOnDrugsInLastThirtyDaysAmountNote)
        {
            _moneySpentOnDrugsInLastThirtyDaysAmountNote = moneySpentOnDrugsInLastThirtyDaysAmountNote;
            return this;
        }

        /// <summary>
        /// Assigns the outpatient treatment in last thirty days day count.
        /// </summary>
        /// <param name="outpatientTreatmentInLastThirtyDaysDayCount">The outpatient treatment in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOutpatientTreatmentInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> outpatientTreatmentInLastThirtyDaysDayCount)
        {
            _outpatientTreatmentInLastThirtyDaysDayCount = outpatientTreatmentInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the outpatient treatment in last thirty days day count note.
        /// </summary>
        /// <param name="outpatientTreatmentInLastThirtyDaysDayCountNote">The outpatient treatment in last thirty days day count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOutpatientTreatmentInLastThirtyDaysDayCountNote(string outpatientTreatmentInLastThirtyDaysDayCountNote)
        {
            _outpatientTreatmentInLastThirtyDaysDayCountNote = outpatientTreatmentInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol problem in last thirty days day count.
        /// </summary>
        /// <param name="alcoholProblemInLastThirtyDaysDayCount">The alcohol problem in last Thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholProblemInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> alcoholProblemInLastThirtyDaysDayCount)
        {
            _alcoholProblemInLastThirtyDaysDayCount = alcoholProblemInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the alcohol problem in last thirty days day count note.
        /// </summary>
        /// <param name="alcoholProblemInLastThirtyDaysDayCountNote">The alcohol problem in last Thirty days day count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAlcoholProblemInLastThirtyDaysDayCountNote(string alcoholProblemInLastThirtyDaysDayCountNote)
        {
            _alcoholProblemInLastThirtyDaysDayCountNote = alcoholProblemInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by alcohol problems DensAsi patient rating.
        /// </summary>
        /// <param name="troubledByAlcoholProblemsDensAsiPatientRating">The troubled by alcohol problems DensAsi patient rating.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithTroubledByAlcoholProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> troubledByAlcoholProblemsDensAsiPatientRating)
        {
            _troubledByAlcoholProblemsDensAsiPatientRating = troubledByAlcoholProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by alcohol problems DensAsi patient rating note.
        /// </summary>
        /// <param name="troubledByAlcoholProblemsDensAsiPatientRatingNote">The troubled by alcohol problems DensAsi patient rating note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithTroubledByAlcoholProblemsDensAsiPatientRatingNote(string troubledByAlcoholProblemsDensAsiPatientRatingNote)
        {
            _troubledByAlcoholProblemsDensAsiPatientRatingNote = troubledByAlcoholProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of alcohol problem treatment DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfAlcoholProblemTreatmentDensAsiPatientRating">The importance of alcohol problem treatment DensAsi patient rating.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithImportanceOfAlcoholProblemTreatmentDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfAlcoholProblemTreatmentDensAsiPatientRating)
        {
            _importanceOfAlcoholProblemTreatmentDensAsiPatientRating = importanceOfAlcoholProblemTreatmentDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of alcohol problem treatment DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote">The importance of alcohol problem treatment DensAsi patient rating note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithImportanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote(string importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote)
        {
            _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote = importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the drug problem in last thirty days day count.
        /// </summary>
        /// <param name="drugProblemInLastThirtyDaysDayCount">The drug problem in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithDrugProblemInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> drugProblemInLastThirtyDaysDayCount)
        {
            _drugProblemInLastThirtyDaysDayCount = drugProblemInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the drug problem in last thirty days day count note.
        /// </summary>
        /// <param name="drugProblemInLastThirtyDaysDayCountNote">The drug problem in last thirty days day count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithDrugProblemInLastThirtyDaysDayCountNote(string drugProblemInLastThirtyDaysDayCountNote)
        {
            _drugProblemInLastThirtyDaysDayCountNote = drugProblemInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by drug problems DensAsi patient rating.
        /// </summary>
        /// <param name="troubledByDrugProblemsDensAsiPatientRating">The troubled by drug problems DensAsi patient rating.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithTroubledByDrugProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> troubledByDrugProblemsDensAsiPatientRating)
        {
            _troubledByDrugProblemsDensAsiPatientRating = troubledByDrugProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by drug problems DensAsi patient rating note.
        /// </summary>
        /// <param name="troubledByDrugProblemsDensAsiPatientRatingNote">The troubled by drug problems DensAsi patient rating note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithTroubledByDrugProblemsDensAsiPatientRatingNote(string troubledByDrugProblemsDensAsiPatientRatingNote)
        {
            _troubledByDrugProblemsDensAsiPatientRatingNote = troubledByDrugProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of drug problem treatment DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfDrugProblemTreatmentDensAsiPatientRating">The importance of drug problem treatment DensAsi patient rating.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithImportanceOfDrugProblemTreatmentDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfDrugProblemTreatmentDensAsiPatientRating)
        {
            _importanceOfDrugProblemTreatmentDensAsiPatientRating = importanceOfDrugProblemTreatmentDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of drug problem treatment DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfDrugProblemTreatmentDensAsiPatientRatingNote">The importance of drug problem treatment DensAsi patient rating note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithImportanceOfDrugProblemTreatmentDensAsiPatientRatingNote(string importanceOfDrugProblemTreatmentDensAsiPatientRatingNote)
        {
            _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote = importanceOfDrugProblemTreatmentDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient alcohol treatment DensAsi interviewer rating.
        /// </summary>
        /// <param name="patientAlcoholTreatmentDensAsiInterviewerRating">The patient alcohol treatment DensAsi interviewer rating.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithPatientAlcoholTreatmentDensAsiInterviewerRating(DensAsiInterviewerRating patientAlcoholTreatmentDensAsiInterviewerRating)
        {
            _patientAlcoholTreatmentDensAsiInterviewerRating = patientAlcoholTreatmentDensAsiInterviewerRating;
            return this;
        }

        /// <summary>
        /// Assigns the patient alcohol treatment DensAsi interviewer rating note.
        /// </summary>
        /// <param name="patientAlcoholTreatmentDensAsiInterviewerRatingNote">The patient alcohol treatment DensAsi interviewer rating note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithPatientAlcoholTreatmentDensAsiInterviewerRatingNote(string patientAlcoholTreatmentDensAsiInterviewerRatingNote)
        {
            _patientAlcoholTreatmentDensAsiInterviewerRatingNote = patientAlcoholTreatmentDensAsiInterviewerRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient drug treatment DensAsi interviewer rating.
        /// </summary>
        /// <param name="patientDrugTreatmentDensAsiInterviewerRating">The patient drug treatment DensAsi interviewer rating.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithPatientDrugTreatmentDensAsiInterviewerRating(DensAsiInterviewerRating patientDrugTreatmentDensAsiInterviewerRating)
        {
            _patientDrugTreatmentDensAsiInterviewerRating = patientDrugTreatmentDensAsiInterviewerRating;
            return this;
        }

        /// <summary>
        /// Assigns the patient drug treatment DensAsi interviewer rating note.
        /// </summary>
        /// <param name="patientDrugTreatmentDensAsiInterviewerRatingNote">The patient drug treatment DensAsi interviewer rating note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithPatientDrugTreatmentDensAsiInterviewerRatingNote(string patientDrugTreatmentDensAsiInterviewerRatingNote)
        {
            _patientDrugTreatmentDensAsiInterviewerRatingNote = patientDrugTreatmentDensAsiInterviewerRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicator(bool? confidenceDistortedByPatientMisrepresentationIndicator)
        {
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator note.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicatorNote(string confidenceDistortedByPatientMisrepresentationIndicatorNote)
        {
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator(bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator note.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote(string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the hydromorphone in last thirty days day count.
        /// </summary>
        /// <param name="hydromorphoneInLastThirtyDaysDayCount">The hydromorphone in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydromorphoneInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> hydromorphoneInLastThirtyDaysDayCount)
        {
            _hydromorphoneInLastThirtyDaysDayCount = hydromorphoneInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the hydromorphone in lifetime year count.
        /// </summary>
        /// <param name="hydromorphoneInLifetimeYearCount">The hydromorphone in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydromorphoneInLifetimeYearCount(DensAsiNonResponseType<int?> hydromorphoneInLifetimeYearCount)
        {
            _hydromorphoneInLifetimeYearCount = hydromorphoneInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the hydromorphone DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="hydromorphoneDensAsiDrugAlcoholAdministrationRoute">The hydromorphone DensAsi drug alcohol administration route.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydromorphoneDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> hydromorphoneDensAsiDrugAlcoholAdministrationRoute)
        {
            _hydromorphoneDensAsiDrugAlcoholAdministrationRoute = hydromorphoneDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the hydromorphone note.
        /// </summary>
        /// <param name="hydromorphoneNote">The hydromorphone note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydromorphoneNote(string hydromorphoneNote)
        {
            _hydromorphoneNote = hydromorphoneNote;
            return this;
        }

        /// <summary>
        /// Assigns the oxycodone in last thirty days day count.
        /// </summary>
        /// <param name="oxycodoneInLastThirtyDaysDayCount">The oxycodone in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxycodoneInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> oxycodoneInLastThirtyDaysDayCount)
        {
            _oxycodoneInLastThirtyDaysDayCount = oxycodoneInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the oxycodone in lifetime year count.
        /// </summary>
        /// <param name="oxycodoneInLifetimeYearCount">The oxycodone in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxycodoneInLifetimeYearCount(DensAsiNonResponseType<int?> oxycodoneInLifetimeYearCount)
        {
            _oxycodoneInLifetimeYearCount = oxycodoneInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the oxycodone DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="oxycodoneDensAsiDrugAlcoholAdministrationRoute">The oxycodone DensAsi drug alcohol administration route.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxycodoneDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> oxycodoneDensAsiDrugAlcoholAdministrationRoute)
        {
            _oxycodoneDensAsiDrugAlcoholAdministrationRoute = oxycodoneDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the oxycodone note.
        /// </summary>
        /// <param name="oxycodoneNote">The oxycodone note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxycodoneNote(string oxycodoneNote)
        {
            _oxycodoneNote = oxycodoneNote;
            return this;
        }

        /// <summary>
        /// Assigns the hydrocodone in last thirty days day count.
        /// </summary>
        /// <param name="hydrocodoneInLastThirtyDaysDayCount">The hydrocodone in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydrocodoneInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> hydrocodoneInLastThirtyDaysDayCount)
        {
            _hydrocodoneInLastThirtyDaysDayCount = hydrocodoneInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the hydrocodone in lifetime year count.
        /// </summary>
        /// <param name="hydrocodoneInLifetimeYearCount">The hydrocodone in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydrocodoneInLifetimeYearCount(DensAsiNonResponseType<int?> hydrocodoneInLifetimeYearCount)
        {
            _hydrocodoneInLifetimeYearCount = hydrocodoneInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the hydrocodone DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="hydrocodoneDensAsiDrugAlcoholAdministrationRoute">The hydrocodone DensAsi drug alcohol administration route.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydrocodoneDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> hydrocodoneDensAsiDrugAlcoholAdministrationRoute)
        {
            _hydrocodoneDensAsiDrugAlcoholAdministrationRoute = hydrocodoneDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the hydrocodone note.
        /// </summary>
        /// <param name="hydrocodoneNote">The hydrocodone note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithHydrocodoneNote(string hydrocodoneNote)
        {
            _hydrocodoneNote = hydrocodoneNote;
            return this;
        }

        /// <summary>
        /// Assigns the buprenorphine in last thirty days day count.
        /// </summary>
        /// <param name="buprenorphineInLastThirtyDaysDayCount">The buprenorphine in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBuprenorphineInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> buprenorphineInLastThirtyDaysDayCount)
        {
            _buprenorphineInLastThirtyDaysDayCount = buprenorphineInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the buprenorphine in lifetime year count.
        /// </summary>
        /// <param name="buprenorphineInLifetimeYearCount">The buprenorphine in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBuprenorphineInLifetimeYearCount(DensAsiNonResponseType<int?> buprenorphineInLifetimeYearCount)
        {
            _buprenorphineInLifetimeYearCount = buprenorphineInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the buprenorphine DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="buprenorphineDensAsiDrugAlcoholAdministrationRoute">The buprenorphine DensAsi drug alcohol administration route.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBuprenorphineDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> buprenorphineDensAsiDrugAlcoholAdministrationRoute)
        {
            _buprenorphineDensAsiDrugAlcoholAdministrationRoute = buprenorphineDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the buprenorphine note.
        /// </summary>
        /// <param name="buprenorphineNote">The buprenorphine note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithBuprenorphineNote(string buprenorphineNote)
        {
            _buprenorphineNote = buprenorphineNote;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin in last thirty days day count.
        /// </summary>
        /// <param name="oxyContinInLastThirtyDaysDayCount">The oxycontin in last thirty days day count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> oxyContinInLastThirtyDaysDayCount)
        {
            _oxyContinInLastThirtyDaysDayCount = oxyContinInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin in lifetime year count.
        /// </summary>
        /// <param name="oxyContinInLifetimeYearCount">The oxycontin in lifetime year count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinInLifetimeYearCount(DensAsiNonResponseType<int?> oxyContinInLifetimeYearCount)
        {
            _oxyContinInLifetimeYearCount = oxyContinInLifetimeYearCount;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin DensAsi drug alcohol administration route.
        /// </summary>
        /// <param name="oxyContinDensAsiDrugAlcoholAdministrationRoute">The oxycontin DensAsi drug alcohol administration route.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinDensAsiDrugAlcoholAdministrationRoute(DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> oxyContinDensAsiDrugAlcoholAdministrationRoute)
        {
            _oxyContinDensAsiDrugAlcoholAdministrationRoute = oxyContinDensAsiDrugAlcoholAdministrationRoute;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin note.
        /// </summary>
        /// <param name="oxyContinNote">The oxycontin note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinNote(string oxyContinNote)
        {
            _oxyContinNote = oxyContinNote;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin prescribed for medical reason indicator.
        /// </summary>
        /// <param name="oxyContinPrescribedForMedicalReasonIndicator">The oxycontin prescribed for medical reason indicator.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinPrescribedForMedicalReasonIndicator(DensAsiNonResponseType<bool?> oxyContinPrescribedForMedicalReasonIndicator)
        {
            _oxyContinPrescribedForMedicalReasonIndicator = oxyContinPrescribedForMedicalReasonIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin prescribed for medical reason indicator note.
        /// </summary>
        /// <param name="oxyContinPrescribedForMedicalReasonIndicatorNote">The oxycontin prescribed for medical reason indicator note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinPrescribedForMedicalReasonIndicatorNote(string oxyContinPrescribedForMedicalReasonIndicatorNote)
        {
            _oxyContinPrescribedForMedicalReasonIndicatorNote = oxyContinPrescribedForMedicalReasonIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin use to get high indicator.
        /// </summary>
        /// <param name="oxyContinUseToGetHighIndicator">The oxycontin use to get high indicator.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinUseToGetHighIndicator(DensAsiNonResponseType<bool?> oxyContinUseToGetHighIndicator)
        {
            _oxyContinUseToGetHighIndicator = oxyContinUseToGetHighIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin use to get high indicator note.
        /// </summary>
        /// <param name="oxyContinUseToGetHighIndicatorNote">The oxycontin use to get high indicator note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinUseToGetHighIndicatorNote(string oxyContinUseToGetHighIndicatorNote)
        {
            _oxyContinUseToGetHighIndicatorNote = oxyContinUseToGetHighIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin taken with other opiates indicator.
        /// </summary>
        /// <param name="oxyContinTakenWithOtherOpiatesIndicator">The oxycontin taken with other opiates indicator.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinTakenWithOtherOpiatesIndicator(DensAsiNonResponseType<bool?> oxyContinTakenWithOtherOpiatesIndicator)
        {
            _oxyContinTakenWithOtherOpiatesIndicator = oxyContinTakenWithOtherOpiatesIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin taken with other opiates indicator note.
        /// </summary>
        /// <param name="oxyContinTakenWithOtherOpiatesIndicatorNote">The oxycontin taken with other opiates indicator note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinTakenWithOtherOpiatesIndicatorNote(string oxyContinTakenWithOtherOpiatesIndicatorNote)
        {
            _oxyContinTakenWithOtherOpiatesIndicatorNote = oxyContinTakenWithOtherOpiatesIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the after oxycontin first use month count.
        /// </summary>
        /// <param name="afterOxyContinFirstUseMonthCount">The after oxycontin first use month count.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAfterOxyContinFirstUseMonthCount(DensAsiNonResponseType<int?> afterOxyContinFirstUseMonthCount)
        {
            _afterOxyContinFirstUseMonthCount = afterOxyContinFirstUseMonthCount;
            return this;
        }

        /// <summary>
        /// Assigns the after oxycontin first use month count note.
        /// </summary>
        /// <param name="afterOxyContinFirstUseMonthCountNote">The after oxycontin first use month count note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithAfterOxyContinFirstUseMonthCountNote(string afterOxyContinFirstUseMonthCountNote)
        {
            _afterOxyContinFirstUseMonthCountNote = afterOxyContinFirstUseMonthCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin from friend family street indicator.
        /// </summary>
        /// <param name="oxyContinFromFriendFamilyStreetIndicator">The oxycontin from friend family street indicator.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinFromFriendFamilyStreetIndicator(DensAsiNonResponseType<bool?> oxyContinFromFriendFamilyStreetIndicator)
        {
            _oxyContinFromFriendFamilyStreetIndicator = oxyContinFromFriendFamilyStreetIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the oxycontin from friend family street indicator note.
        /// </summary>
        /// <param name="oxyContinFromFriendFamilyStreetIndicatorNote">The oxycontin from friend family street indicator note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithOxyContinFromFriendFamilyStreetIndicatorNote(string oxyContinFromFriendFamilyStreetIndicatorNote)
        {
            _oxyContinFromFriendFamilyStreetIndicatorNote = oxyContinFromFriendFamilyStreetIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the section note.
        /// </summary>
        /// <param name="sectionNote">The section note.</param>
        /// <returns>A DensAsiDrugAlcoholUseSectionBuilder.</returns>
        public DensAsiDrugAlcoholUseSectionBuilder WithSectionNote(string sectionNote)
        {
            _sectionNote = sectionNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A DensAsiDrugAlcoholUseSection.</returns>
        public DensAsiDrugAlcoholUseSection Build()
        {
            return new DensAsiDrugAlcoholUseSection(
                _anyAlcoholUseInLastThirtyDaysDayCount,
                _anyAlcoholUseInLifetimeYearCount,
                _anyAlcoholDensAsiDrugAlcoholAdministrationRoute,
                _anyAlcoholUseNote,
                _alcoholIntoxicationInLastThirtyDaysDayCount,
                _alcoholIntoxicationUseInLifetimeYearCount,
                _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute,
                _alcoholIntoxicationNote,
                _heroinInLastThirtyDaysDayCount,
                _heroinInLifetimeYearCount,
                _heroinDensAsiDrugAlcoholAdministrationRoute,
                _heroinNote,
                _methadoneInLastThirtyDaysDayCount,
                _methadoneInLifetimeYearCount,
                _methadoneDensAsiDrugAlcoholAdministrationRoute,
                _methadoneNote,
                _otherOpiatesInLastThirtyDaysDayCount,
                _otherOpiatesInLifetimeYearCount,
                _otherOpiatesDensAsiDrugAlcoholAdministrationRoute,
                _otherOpiatesNote,
                _barbituratesInLastThirtyDaysDayCount,
                _barbituratesInLifetimeYearCount,
                _barbituratesDensAsiDrugAlcoholAdministrationRoute,
                _barbituratesNote,
                _otherSedativesInLastThirtyDaysDayCount,
                _otherSedativesInLifetimeYearCount,
                _otherSedativesDensAsiDrugAlcoholAdministrationRoute,
                _otherSedativesNote,
                _cocaineInLastThirtyDaysDayCount,
                _cocaineInLifetimeYearCount,
                _cocaineDensAsiDrugAlcoholAdministrationRoute,
                _cocaineNote,
                _amphetaminesInLastThirtyDaysDayCount,
                _amphetaminesInLifetimeYearCount,
                _amphetaminesDensAsiDrugAlcoholAdministrationRoute,
                _amphetaminesNote,
                _cannabisInLastThirtyDaysDayCount,
                _cannabisInLifetimeYearCount,
                _cannabisDensAsiDrugAlcoholAdministrationRoute,
                _cannabisNote,
                _hallucinogensInLastThirtyDaysDayCount,
                _hallucinogensInLifetimeYearCount,
                _hallucinogensDensAsiDrugAlcoholAdministrationRoute,
                _hallucinogensNote,
                _inhalantsInLastThirtyDaysDayCount,
                _inhalantsInLifetimeYearCount,
                _inhalantsDensAsiDrugAlcoholAdministrationRoute,
                _inhalantsNote,
                _moreThanOneSubstancePerDayInLastThirtyDaysDayCount,
                _moreThanOneSubstancePerDayInLifetimeYearCount,
                _moreThanOneSubstancePerDayNote,
                _majorDensAsiProblematicSubstance,
                _majorDensAsiProblematicSubstanceNote,
                _voluntaryAbstinenceFromProblematicSubstanceMonthCount,
                _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote,
                _endOfProblematicSubstanceAbstinenceMonthCount,
                _endOfProblematicSubstanceAbstinenceMonthCountNote,
                _alcoholDtCount,
                _alcoholDtCountNote,
                _overdosedOnDrugsCount,
                _overdosedOnDrugsCountNote,
                _alcoholAbuseTreatmentCount,
                _alcoholAbuseTreatmentCountNote,
                _alcoholDetoxTreatmentOnlyCount,
                _alcoholDetoxTreatmentOnlyCountNote,
                _moneySpentOnAlcoholInLastThirtyDaysAmount,
                _moneySpentOnAlcoholInLastThirtyDaysAmountNote,
                _drugAbuseTreatmentCount,
                _drugAbuseTreatmentCountNote,
                _drugDetoxTreatmentOnlyCount,
                _drugDetoxTreatmentOnlyCountNote,
                _moneySpentOnDrugsInLastThirtyDaysAmount,
                _moneySpentOnDrugsInLastThirtyDaysAmountNote,
                _outpatientTreatmentInLastThirtyDaysDayCount,
                _outpatientTreatmentInLastThirtyDaysDayCountNote,
                _alcoholProblemInLastThirtyDaysDayCount,
                _alcoholProblemInLastThirtyDaysDayCountNote,
                _troubledByAlcoholProblemsDensAsiPatientRating,
                _troubledByAlcoholProblemsDensAsiPatientRatingNote,
                _importanceOfAlcoholProblemTreatmentDensAsiPatientRating,
                _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote,
                _drugProblemInLastThirtyDaysDayCount,
                _drugProblemInLastThirtyDaysDayCountNote,
                _troubledByDrugProblemsDensAsiPatientRating,
                _troubledByDrugProblemsDensAsiPatientRatingNote,
                _importanceOfDrugProblemTreatmentDensAsiPatientRating,
                _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote,
                _patientAlcoholTreatmentDensAsiInterviewerRating,
                _patientAlcoholTreatmentDensAsiInterviewerRatingNote,
                _patientDrugTreatmentDensAsiInterviewerRating,
                _patientDrugTreatmentDensAsiInterviewerRatingNote,
                _confidenceDistortedByPatientMisrepresentationIndicator,
                _confidenceDistortedByPatientMisrepresentationIndicatorNote,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                _hydromorphoneInLastThirtyDaysDayCount,
                _hydromorphoneInLifetimeYearCount,
                _hydromorphoneDensAsiDrugAlcoholAdministrationRoute,
                _hydromorphoneNote,
                _oxycodoneInLastThirtyDaysDayCount,
                _oxycodoneInLifetimeYearCount,
                _oxycodoneDensAsiDrugAlcoholAdministrationRoute,
                _oxycodoneNote,
                _hydrocodoneInLastThirtyDaysDayCount,
                _hydrocodoneInLifetimeYearCount,
                _hydrocodoneDensAsiDrugAlcoholAdministrationRoute,
                _hydrocodoneNote,
                _buprenorphineInLastThirtyDaysDayCount,
                _buprenorphineInLifetimeYearCount,
                _buprenorphineDensAsiDrugAlcoholAdministrationRoute,
                _buprenorphineNote,
                _oxyContinInLastThirtyDaysDayCount,
                _oxyContinInLifetimeYearCount,
                _oxyContinDensAsiDrugAlcoholAdministrationRoute,
                _oxyContinNote,
                _oxyContinPrescribedForMedicalReasonIndicator,
                _oxyContinPrescribedForMedicalReasonIndicatorNote,
                _oxyContinUseToGetHighIndicator,
                _oxyContinUseToGetHighIndicatorNote,
                _oxyContinTakenWithOtherOpiatesIndicator,
                _oxyContinTakenWithOtherOpiatesIndicatorNote,
                _afterOxyContinFirstUseMonthCount,
                _afterOxyContinFirstUseMonthCountNote,
                _oxyContinFromFriendFamilyStreetIndicator,
                _oxyContinFromFriendFamilyStreetIndicatorNote,
                _sectionNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiDrugAlcoholUseSectionBuilder"/> to <see cref="DensAsiDrugAlcoholUseSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiDrugAlcoholUseSection(DensAsiDrugAlcoholUseSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
