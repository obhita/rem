using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiDrugAlcoholUseSection contains patient drug and alcohol information from the Drug and Alcohol Information section of the DensAsi. 
    /// <remarks>
    /// Included in each of these sections is the interviewer's severity rating, suggesting the client's need for treatment or additional treatment. 
    /// This is based on the information provided by the client. 
    /// </remarks>
    /// </summary>
    [Component]
    public class DensAsiDrugAlcoholUseSection : DensAsiInterviewSectionBase
    {
        private readonly DensAsiNonResponseType<int?> _afterOxyContinFirstUseMonthCount;
        private readonly string _afterOxyContinFirstUseMonthCountNote;
        private readonly DensAsiNonResponseType<int?> _alcoholAbuseTreatmentCount;
        private readonly string _alcoholAbuseTreatmentCountNote;
        private readonly DensAsiNonResponseType<int?> _alcoholDetoxTreatmentOnlyCount;
        private readonly string _alcoholDetoxTreatmentOnlyCountNote;
        private readonly DensAsiNonResponseType<int?> _alcoholDtCount;
        private readonly string _alcoholDtCountNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _alcoholIntoxicationInLastThirtyDaysDayCount;
        private readonly string _alcoholIntoxicationNote;
        private readonly DensAsiNonResponseType<int?> _alcoholIntoxicationUseInLifetimeYearCount;
        private readonly DensAsiNonResponseType<int?> _alcoholProblemInLastThirtyDaysDayCount;
        private readonly string _alcoholProblemInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _amphetaminesDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _amphetaminesInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _amphetaminesInLifetimeYearCount;
        private readonly string _amphetaminesNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _anyAlcoholDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _anyAlcoholUseInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _anyAlcoholUseInLifetimeYearCount;
        private readonly string _anyAlcoholUseNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _barbituratesDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _barbituratesInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _barbituratesInLifetimeYearCount;
        private readonly string _barbituratesNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _buprenorphineDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _buprenorphineInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _buprenorphineInLifetimeYearCount;
        private readonly string _buprenorphineNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _cannabisDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _cannabisInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _cannabisInLifetimeYearCount;
        private readonly string _cannabisNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _cocaineDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _cocaineInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _cocaineInLifetimeYearCount;
        private readonly string _cocaineNote;
        private readonly bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private readonly string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private readonly bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private readonly string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _drugAbuseTreatmentCount;
        private readonly string _drugAbuseTreatmentCountNote;
        private readonly DensAsiNonResponseType<int?> _drugDetoxTreatmentOnlyCount;
        private readonly string _drugDetoxTreatmentOnlyCountNote;
        private readonly DensAsiNonResponseType<int?> _drugProblemInLastThirtyDaysDayCount;
        private readonly string _drugProblemInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<int?> _endOfProblematicSubstanceAbstinenceMonthCount;
        private readonly string _endOfProblematicSubstanceAbstinenceMonthCountNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _hallucinogensDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _hallucinogensInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _hallucinogensInLifetimeYearCount;
        private readonly string _hallucinogensNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _heroinDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _heroinInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _heroinInLifetimeYearCount;
        private readonly string _heroinNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _hydrocodoneDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _hydrocodoneInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _hydrocodoneInLifetimeYearCount;
        private readonly string _hydrocodoneNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _hydromorphoneDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _hydromorphoneInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _hydromorphoneInLifetimeYearCount;
        private readonly string _hydromorphoneNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfAlcoholProblemTreatmentDensAsiPatientRating;
        private readonly string _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfDrugProblemTreatmentDensAsiPatientRating;
        private readonly string _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _inhalantsDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _inhalantsInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _inhalantsInLifetimeYearCount;
        private readonly string _inhalantsNote;
        private readonly DensAsiNonResponseType<DensAsiProblematicSubstance> _majorDensAsiProblematicSubstance;
        private readonly string _majorDensAsiProblematicSubstanceNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _methadoneDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _methadoneInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _methadoneInLifetimeYearCount;
        private readonly string _methadoneNote;
        private readonly DensAsiNonResponseType<int?> _moneySpentOnAlcoholInLastThirtyDaysAmount;
        private readonly string _moneySpentOnAlcoholInLastThirtyDaysAmountNote;
        private readonly DensAsiNonResponseType<int?> _moneySpentOnDrugsInLastThirtyDaysAmount;
        private readonly string _moneySpentOnDrugsInLastThirtyDaysAmountNote;
        private readonly DensAsiNonResponseType<int?> _moreThanOneSubstancePerDayInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _moreThanOneSubstancePerDayInLifetimeYearCount;
        private readonly string _moreThanOneSubstancePerDayNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _otherOpiatesDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _otherOpiatesInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _otherOpiatesInLifetimeYearCount;
        private readonly string _otherOpiatesNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _otherSedativesDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _otherSedativesInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _otherSedativesInLifetimeYearCount;
        private readonly string _otherSedativesNote;
        private readonly DensAsiNonResponseType<int?> _outpatientTreatmentInLastThirtyDaysDayCount;
        private readonly string _outpatientTreatmentInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<int?> _overdosedOnDrugsCount;
        private readonly string _overdosedOnDrugsCountNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _oxyContinDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<bool?> _oxyContinFromFriendFamilyStreetIndicator;
        private readonly string _oxyContinFromFriendFamilyStreetIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _oxyContinInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _oxyContinInLifetimeYearCount;
        private readonly string _oxyContinNote;
        private readonly DensAsiNonResponseType<bool?> _oxyContinPrescribedForMedicalReasonIndicator;
        private readonly string _oxyContinPrescribedForMedicalReasonIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _oxyContinTakenWithOtherOpiatesIndicator;
        private readonly string _oxyContinTakenWithOtherOpiatesIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _oxyContinUseToGetHighIndicator;
        private readonly string _oxyContinUseToGetHighIndicatorNote;
        private readonly DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> _oxycodoneDensAsiDrugAlcoholAdministrationRoute;
        private readonly DensAsiNonResponseType<int?> _oxycodoneInLastThirtyDaysDayCount;
        private readonly DensAsiNonResponseType<int?> _oxycodoneInLifetimeYearCount;
        private readonly string _oxycodoneNote;
        private readonly DensAsiInterviewerRating _patientAlcoholTreatmentDensAsiInterviewerRating;
        private readonly string _patientAlcoholTreatmentDensAsiInterviewerRatingNote;
        private readonly DensAsiInterviewerRating _patientDrugTreatmentDensAsiInterviewerRating;
        private readonly string _patientDrugTreatmentDensAsiInterviewerRatingNote;
        private readonly string _sectionNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _troubledByAlcoholProblemsDensAsiPatientRating;
        private readonly string _troubledByAlcoholProblemsDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _troubledByDrugProblemsDensAsiPatientRating;
        private readonly string _troubledByDrugProblemsDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<int?> _voluntaryAbstinenceFromProblematicSubstanceMonthCount;
        private readonly string _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote;

        private DensAsiDrugAlcoholUseSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiDrugAlcoholUseSection"/> class.
        /// </summary>
        /// <param name="anyAlcoholUseInLastThirtyDaysDayCount">Any alcohol use in last thirty days day count.</param>
        /// <param name="anyAlcoholUseInLifetimeYearCount">Any alcohol use in lifetime year count.</param>
        /// <param name="anyAlcoholDensAsiDrugAlcoholAdministrationRoute">Any alcohol drug alcohol administration route.</param>
        /// <param name="anyAlcoholUseNote">Any alcohol use note.</param>
        /// <param name="alcoholIntoxicationInLastThirtyDaysDayCount">The alcohol intoxication in last thirty days day count.</param>
        /// <param name="alcoholIntoxicationUseInLifetimeYearCount">The alcohol intoxication use in lifetime year count.</param>
        /// <param name="alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute">The alcohol intoxication drug alcohol administration route.</param>
        /// <param name="alcoholIntoxicationNote">The alcohol intoxication note.</param>
        /// <param name="heroinInLastThirtyDaysDayCount">The heroin in last thirty days day count.</param>
        /// <param name="heroinInLifetimeYearCount">The heroin in lifetime year count.</param>
        /// <param name="heroinDensAsiDrugAlcoholAdministrationRoute">The heroin drug alcohol administration route.</param>
        /// <param name="heroinNote">The heroin note.</param>
        /// <param name="methadoneInLastThirtyDaysDayCount">The methadone in last thirty days day count.</param>
        /// <param name="methadoneInLifetimeYearCount">The methadone in lifetime year count.</param>
        /// <param name="methadoneDensAsiDrugAlcoholAdministrationRoute">The methadone drug alcohol administration route.</param>
        /// <param name="methadoneNote">The methadone note.</param>
        /// <param name="otherOpiatesInLastThirtyDaysDayCount">The other opiates in last thirty days day count.</param>
        /// <param name="otherOpiatesInLifetimeYearCount">The other opiates in lifetime year count.</param>
        /// <param name="otherOpiatesDensAsiDrugAlcoholAdministrationRoute">The other opiates drug alcohol administration route.</param>
        /// <param name="otherOpiatesNote">The other opiates note.</param>
        /// <param name="barbituratesInLastThirtyDaysDayCount">The barbiturates in last thirty days day count.</param>
        /// <param name="barbituratesInLifetimeYearCount">The barbiturates in lifetime year count.</param>
        /// <param name="barbituratesDensAsiDrugAlcoholAdministrationRoute">The barbiturates drug alcohol administration route.</param>
        /// <param name="barbituratesNote">The barbiturates note.</param>
        /// <param name="otherSedativesInLastThirtyDaysDayCount">The other sedatives in last thirty days day count.</param>
        /// <param name="otherSedativesInLifetimeYearCount">The other sedatives in lifetime year count.</param>
        /// <param name="otherSedativesDensAsiDrugAlcoholAdministrationRoute">The other sedatives drug alcohol administration route.</param>
        /// <param name="otherSedativesNote">The other sedatives note.</param>
        /// <param name="cocaineInLastThirtyDaysDayCount">The cocaine in last thirty days day count.</param>
        /// <param name="cocaineInLifetimeYearCount">The cocaine in lifetime year count.</param>
        /// <param name="cocaineDensAsiDrugAlcoholAdministrationRoute">The cocaine drug alcohol administration route.</param>
        /// <param name="cocaineNote">The cocaine note.</param>
        /// <param name="amphetaminesInLastThirtyDaysDayCount">The amphetamines in last thirty days day count.</param>
        /// <param name="amphetaminesInLifetimeYearCount">The amphetamines in lifetime year count.</param>
        /// <param name="amphetaminesDensAsiDrugAlcoholAdministrationRoute">The amphetamines drug alcohol administration route.</param>
        /// <param name="amphetaminesNote">The amphetamines note.</param>
        /// <param name="cannabisInLastThirtyDaysDayCount">The cannabis in last thirty days day count.</param>
        /// <param name="cannabisInLifetimeYearCount">The cannabis in lifetime year count.</param>
        /// <param name="cannabisDensAsiDrugAlcoholAdministrationRoute">The cannabis drug alcohol administration route.</param>
        /// <param name="cannabisNote">The cannabis note.</param>
        /// <param name="hallucinogensInLastThirtyDaysDayCount">The hallucinogens in last thirty days day count.</param>
        /// <param name="hallucinogensInLifetimeYearCount">The hallucinogens in lifetime year count.</param>
        /// <param name="hallucinogensDensAsiDrugAlcoholAdministrationRoute">The hallucinogens  drug alcohol administration route.</param>
        /// <param name="hallucinogensNote">The hallucinogens note.</param>
        /// <param name="inhalantsInLastThirtyDaysDayCount">The inhalants in last thirty days day count.</param>
        /// <param name="inhalantsInLifetimeYearCount">The inhalants in lifetime year count.</param>
        /// <param name="inhalantsDensAsiDrugAlcoholAdministrationRoute">The inhalants drug alcohol administration route.</param>
        /// <param name="inhalantsNote">The inhalants note.</param>
        /// <param name="moreThanOneSubstancePerDayInLastThirtyDaysDayCount">The more than one substance per day in last thirty days day count.</param>
        /// <param name="moreThanOneSubstancePerDayInLifetimeYearCount">The more than one substance per day in lifetime year count.</param>
        /// <param name="moreThanOneSubstancePerDayNote">The more than one substance per day note.</param>
        /// <param name="majorDensAsiProblematicSubstance">The major problematic substance.</param>
        /// <param name="majorDensAsiProblematicSubstanceNote">The major problematic substance note.</param>
        /// <param name="voluntaryAbstinenceFromProblematicSubstanceMonthCount">The voluntary abstinence from problematic substance month count.</param>
        /// <param name="voluntaryAbstinenceFromProblematicSubstanceMonthCountNote">The voluntary abstinence from problematic substance month count note.</param>
        /// <param name="endOfProblematicSubstanceAbstinenceMonthCount">The end of problematic substance abstinence month count.</param>
        /// <param name="endOfProblematicSubstanceAbstinenceMonthCountNote">The end of problematic substance abstinence month count note.</param>
        /// <param name="alcoholDtCount">The alcohol dt count.</param>
        /// <param name="alcoholDtCountNote">The alcohol dt count note.</param>
        /// <param name="overdosedOnDrugsCount">The overdosed on drugs count.</param>
        /// <param name="overdosedOnDrugsCountNote">The overdosed on drugs count note.</param>
        /// <param name="alcoholAbuseTreatmentCount">The alcohol abuse treatment count.</param>
        /// <param name="alcoholAbuseTreatmentCountNote">The alcohol abuse treatment count note.</param>
        /// <param name="alcoholDetoxTreatmentOnlyCount">The alcohol detox treatment only count.</param>
        /// <param name="alcoholDetoxTreatmentOnlyCountNote">The alcohol detox treatment only count note.</param>
        /// <param name="moneySpentOnAlcoholInLastThirtyDaysAmount">The money spent on alcohol in last thirty days amount.</param>
        /// <param name="moneySpentOnAlcoholInLastThirtyDaysAmountNote">The money spent on alcohol in last thirty days amount note.</param>
        /// <param name="drugAbuseTreatmentCount">The drug abuse treatment count.</param>
        /// <param name="drugAbuseTreatmentCountNote">The drug abuse treatment count note.</param>
        /// <param name="drugDetoxTreatmentOnlyCount">The drug detox treatment only count.</param>
        /// <param name="drugDetoxTreatmentOnlyCountNote">The drug detox treatment only count note.</param>
        /// <param name="moneySpentOnDrugsInLastThirtyDaysAmount">The money spent on drugs in last thirty days amount.</param>
        /// <param name="moneySpentOnDrugsInLastThirtyDaysAmountNote">The money spent on drugs in last thirty days amount note.</param>
        /// <param name="outpatientTreatmentInLastThirtyDaysDayCount">The outpatient treatment in last thirty days day count.</param>
        /// <param name="outpatientTreatmentInLastThirtyDaysDayCountNote">The outpatient treatment in last thirty days day count note.</param>
        /// <param name="alcoholProblemInLastThirtyDaysDayCount">The alcohol problem in last thirty days day count.</param>
        /// <param name="alcoholProblemInLastThirtyDaysDayCountNote">The alcohol problem in last thirty days day count note.</param>
        /// <param name="troubledByAlcoholProblemsDensAsiPatientRating">The troubled by alcohol problems patient rating.</param>
        /// <param name="troubledByAlcoholProblemsDensAsiPatientRatingNote">The troubled by alcohol problems patient rating note.</param>
        /// <param name="importanceOfAlcoholProblemTreatmentDensAsiPatientRating">The importance of alcohol problem treatment patient rating.</param>
        /// <param name="importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote">The importance of alcohol problem treatment patient rating note.</param>
        /// <param name="drugProblemInLastThirtyDaysDayCount">The drug problem in last thirty days day count.</param>
        /// <param name="drugProblemInLastThirtyDaysDayCountNote">The drug problem in last thirty days day count note.</param>
        /// <param name="troubledByDrugProblemsDensAsiPatientRating">The troubled by drug problems patient rating.</param>
        /// <param name="troubledByDrugProblemsDensAsiPatientRatingNote">The troubled by drug problems patient rating note.</param>
        /// <param name="importanceOfDrugProblemTreatmentDensAsiPatientRating">The importance of drug problem treatment patient rating.</param>
        /// <param name="importanceOfDrugProblemTreatmentDensAsiPatientRatingNote">The importance of drug problem treatment patient rating note.</param>
        /// <param name="patientAlcoholTreatmentDensAsiInterviewerRating">The patient alcohol treatment interviewer rating.</param>
        /// <param name="patientAlcoholTreatmentDensAsiInterviewerRatingNote">The patient alcohol treatment interviewer rating note.</param>
        /// <param name="patientDrugTreatmentDensAsiInterviewerRating">The patient drug treatment interviewer rating.</param>
        /// <param name="patientDrugTreatmentDensAsiInterviewerRatingNote">The patient drug treatment interviewer rating note.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <param name="hydromorphoneInLastThirtyDaysDayCount">The hydromorphone in last thirty days day count.</param>
        /// <param name="hydromorphoneInLifetimeYearCount">The hydromorphone in lifetime year count.</param>
        /// <param name="hydromorphoneDensAsiDrugAlcoholAdministrationRoute">The hydromorphone drug alcohol administration route.</param>
        /// <param name="hydromorphoneNote">The hydromorphone note.</param>
        /// <param name="oxycodoneInLastThirtyDaysDayCount">The oxycodone in last thirty days day count.</param>
        /// <param name="oxycodoneInLifetimeYearCount">The oxycodone in lifetime year count.</param>
        /// <param name="oxycodoneDensAsiDrugAlcoholAdministrationRoute">The oxycodone drug alcohol administration route.</param>
        /// <param name="oxycodoneNote">The oxycodone note.</param>
        /// <param name="hydrocodoneInLastThirtyDaysDayCount">The hydrocodone in last thirty days day count.</param>
        /// <param name="hydrocodoneInLifetimeYearCount">The hydrocodone in lifetime year count.</param>
        /// <param name="hydrocodoneDensAsiDrugAlcoholAdministrationRoute">The hydrocodone drug alcohol administration route.</param>
        /// <param name="hydrocodoneNote">The hydrocodone note.</param>
        /// <param name="buprenorphineInLastThirtyDaysDayCount">The buprenorphine in last thirty days day count.</param>
        /// <param name="buprenorphineInLifetimeYearCount">The buprenorphine in lifetime year count.</param>
        /// <param name="buprenorphineDensAsiDrugAlcoholAdministrationRoute">The buprenorphine drug alcohol administration route.</param>
        /// <param name="buprenorphineNote">The buprenorphine note.</param>
        /// <param name="oxyContinInLastThirtyDaysDayCount">The oxycontin in last thirty days day count.</param>
        /// <param name="oxyContinInLifetimeYearCount">The oxycontin in lifetime year count.</param>
        /// <param name="oxyContinDensAsiDrugAlcoholAdministrationRoute">The oxycontin drug alcohol administration route.</param>
        /// <param name="oxyContinNote">The oxy contin note.</param>
        /// <param name="oxyContinPrescribedForMedicalReasonIndicator">The oxycontin prescribed for medical reason indicator.</param>
        /// <param name="oxyContinPrescribedForMedicalReasonIndicatorNote">The oxycontin prescribed for medical reason indicator note.</param>
        /// <param name="oxyContinUseToGetHighIndicator">The oxycontin use to get high indicator.</param>
        /// <param name="oxyContinUseToGetHighIndicatorNote">The oxycontin use to get high indicator note.</param>
        /// <param name="oxyContinTakenWithOtherOpiatesIndicator">The oxycontin taken with other opiates indicator.</param>
        /// <param name="oxyContinTakenWithOtherOpiatesIndicatorNote">The oxycontin taken with other opiates indicator note.</param>
        /// <param name="afterOxyContinFirstUseMonthCount">The after oxycontin first use month count.</param>
        /// <param name="afterOxyContinFirstUseMonthCountNote">The after oxycontin first use month count note.</param>
        /// <param name="oxyContinFromFriendFamilyStreetIndicator">The oxycontin from friend family street indicator.</param>
        /// <param name="oxyContinFromFriendFamilyStreetIndicatorNote">The oxycontin from friend family street indicator note.</param>
        /// <param name="sectionNote">The section note.</param>
        public DensAsiDrugAlcoholUseSection(DensAsiNonResponseType<int?> anyAlcoholUseInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> anyAlcoholUseInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> anyAlcoholDensAsiDrugAlcoholAdministrationRoute,
                                                string anyAlcoholUseNote,
                                                DensAsiNonResponseType<int?> alcoholIntoxicationInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> alcoholIntoxicationUseInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute,
                                                string alcoholIntoxicationNote,
                                                DensAsiNonResponseType<int?> heroinInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> heroinInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> heroinDensAsiDrugAlcoholAdministrationRoute,
                                                string heroinNote,
                                                DensAsiNonResponseType<int?> methadoneInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> methadoneInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> methadoneDensAsiDrugAlcoholAdministrationRoute,
                                                string methadoneNote,
                                                DensAsiNonResponseType<int?> otherOpiatesInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> otherOpiatesInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> otherOpiatesDensAsiDrugAlcoholAdministrationRoute,
                                                string otherOpiatesNote,
                                                DensAsiNonResponseType<int?> barbituratesInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> barbituratesInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> barbituratesDensAsiDrugAlcoholAdministrationRoute,
                                                string barbituratesNote,
                                                DensAsiNonResponseType<int?> otherSedativesInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> otherSedativesInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> otherSedativesDensAsiDrugAlcoholAdministrationRoute,
                                                string otherSedativesNote,
                                                DensAsiNonResponseType<int?> cocaineInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> cocaineInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> cocaineDensAsiDrugAlcoholAdministrationRoute,
                                                string cocaineNote,
                                                DensAsiNonResponseType<int?> amphetaminesInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> amphetaminesInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> amphetaminesDensAsiDrugAlcoholAdministrationRoute,
                                                string amphetaminesNote,
                                                DensAsiNonResponseType<int?> cannabisInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> cannabisInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> cannabisDensAsiDrugAlcoholAdministrationRoute,
                                                string cannabisNote,
                                                DensAsiNonResponseType<int?> hallucinogensInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> hallucinogensInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> hallucinogensDensAsiDrugAlcoholAdministrationRoute,
                                                string hallucinogensNote,
                                                DensAsiNonResponseType<int?> inhalantsInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> inhalantsInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> inhalantsDensAsiDrugAlcoholAdministrationRoute,
                                                string inhalantsNote,
                                                DensAsiNonResponseType<int?> moreThanOneSubstancePerDayInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> moreThanOneSubstancePerDayInLifetimeYearCount,
                                                string moreThanOneSubstancePerDayNote,
                                                DensAsiNonResponseType<DensAsiProblematicSubstance> majorDensAsiProblematicSubstance,
                                                string majorDensAsiProblematicSubstanceNote,
                                                DensAsiNonResponseType<int?> voluntaryAbstinenceFromProblematicSubstanceMonthCount,
                                                string voluntaryAbstinenceFromProblematicSubstanceMonthCountNote,
                                                DensAsiNonResponseType<int?> endOfProblematicSubstanceAbstinenceMonthCount,
                                                string endOfProblematicSubstanceAbstinenceMonthCountNote,
                                                DensAsiNonResponseType<int?> alcoholDtCount,
                                                string alcoholDtCountNote,
                                                DensAsiNonResponseType<int?> overdosedOnDrugsCount,
                                                string overdosedOnDrugsCountNote,
                                                DensAsiNonResponseType<int?> alcoholAbuseTreatmentCount,
                                                string alcoholAbuseTreatmentCountNote,
                                                DensAsiNonResponseType<int?> alcoholDetoxTreatmentOnlyCount,
                                                string alcoholDetoxTreatmentOnlyCountNote,
                                                DensAsiNonResponseType<int?> moneySpentOnAlcoholInLastThirtyDaysAmount,
                                                string moneySpentOnAlcoholInLastThirtyDaysAmountNote,
                                                DensAsiNonResponseType<int?> drugAbuseTreatmentCount,
                                                string drugAbuseTreatmentCountNote,
                                                DensAsiNonResponseType<int?> drugDetoxTreatmentOnlyCount,
                                                string drugDetoxTreatmentOnlyCountNote,
                                                DensAsiNonResponseType<int?> moneySpentOnDrugsInLastThirtyDaysAmount,
                                                string moneySpentOnDrugsInLastThirtyDaysAmountNote,
                                                DensAsiNonResponseType<int?> outpatientTreatmentInLastThirtyDaysDayCount,
                                                string outpatientTreatmentInLastThirtyDaysDayCountNote,
                                                DensAsiNonResponseType<int?> alcoholProblemInLastThirtyDaysDayCount,
                                                string alcoholProblemInLastThirtyDaysDayCountNote,
                                                DensAsiNonResponseType<DensAsiPatientRating> troubledByAlcoholProblemsDensAsiPatientRating,
                                                string troubledByAlcoholProblemsDensAsiPatientRatingNote,
                                                DensAsiNonResponseType<DensAsiPatientRating> importanceOfAlcoholProblemTreatmentDensAsiPatientRating,
                                                string importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote,
                                                DensAsiNonResponseType<int?> drugProblemInLastThirtyDaysDayCount,
                                                string drugProblemInLastThirtyDaysDayCountNote,
                                                DensAsiNonResponseType<DensAsiPatientRating> troubledByDrugProblemsDensAsiPatientRating,
                                                string troubledByDrugProblemsDensAsiPatientRatingNote,
                                                DensAsiNonResponseType<DensAsiPatientRating> importanceOfDrugProblemTreatmentDensAsiPatientRating,
                                                string importanceOfDrugProblemTreatmentDensAsiPatientRatingNote,
                                                DensAsiInterviewerRating patientAlcoholTreatmentDensAsiInterviewerRating,
                                                string patientAlcoholTreatmentDensAsiInterviewerRatingNote,
                                                DensAsiInterviewerRating patientDrugTreatmentDensAsiInterviewerRating,
                                                string patientDrugTreatmentDensAsiInterviewerRatingNote,
                                                bool? confidenceDistortedByPatientMisrepresentationIndicator,
                                                string confidenceDistortedByPatientMisrepresentationIndicatorNote,
                                                bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                                                string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                                                DensAsiNonResponseType<int?> hydromorphoneInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> hydromorphoneInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> hydromorphoneDensAsiDrugAlcoholAdministrationRoute,
                                                string hydromorphoneNote,
                                                DensAsiNonResponseType<int?> oxycodoneInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> oxycodoneInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> oxycodoneDensAsiDrugAlcoholAdministrationRoute,
                                                string oxycodoneNote,
                                                DensAsiNonResponseType<int?> hydrocodoneInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> hydrocodoneInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> hydrocodoneDensAsiDrugAlcoholAdministrationRoute,
                                                string hydrocodoneNote,
                                                DensAsiNonResponseType<int?> buprenorphineInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> buprenorphineInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> buprenorphineDensAsiDrugAlcoholAdministrationRoute,
                                                string buprenorphineNote,
                                                DensAsiNonResponseType<int?> oxyContinInLastThirtyDaysDayCount,
                                                DensAsiNonResponseType<int?> oxyContinInLifetimeYearCount,
                                                DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> oxyContinDensAsiDrugAlcoholAdministrationRoute,
                                                string oxyContinNote,
                                                DensAsiNonResponseType<bool?> oxyContinPrescribedForMedicalReasonIndicator,
                                                string oxyContinPrescribedForMedicalReasonIndicatorNote,
                                                DensAsiNonResponseType<bool?> oxyContinUseToGetHighIndicator,
                                                string oxyContinUseToGetHighIndicatorNote,
                                                DensAsiNonResponseType<bool?> oxyContinTakenWithOtherOpiatesIndicator,
                                                string oxyContinTakenWithOtherOpiatesIndicatorNote,
                                                DensAsiNonResponseType<int?> afterOxyContinFirstUseMonthCount,
                                                string afterOxyContinFirstUseMonthCountNote,
                                                DensAsiNonResponseType<bool?> oxyContinFromFriendFamilyStreetIndicator,
                                                string oxyContinFromFriendFamilyStreetIndicatorNote,
                                                string sectionNote )
        {
            if ( anyAlcoholUseInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AnyAlcoholUseInLastThirtyDaysDayCount ).Contains ( anyAlcoholUseInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AnyAlcoholUseInLastThirtyDaysDayCount DensAsiNonResponse value '" + anyAlcoholUseInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( anyAlcoholUseInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AnyAlcoholUseInLifetimeYearCount ).Contains ( anyAlcoholUseInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AnyAlcoholUseInLifetimeYearCount DensAsiNonResponse value '" + anyAlcoholUseInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( anyAlcoholDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AnyAlcoholDensAsiDrugAlcoholAdministrationRoute ).Contains ( anyAlcoholDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AnyAlcoholDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + anyAlcoholDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholIntoxicationInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholIntoxicationInLastThirtyDaysDayCount ).Contains ( alcoholIntoxicationInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholIntoxicationInLastThirtyDaysDayCount DensAsiNonResponse value '" + alcoholIntoxicationInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholIntoxicationUseInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholIntoxicationUseInLifetimeYearCount ).Contains ( alcoholIntoxicationUseInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholIntoxicationUseInLifetimeYearCount DensAsiNonResponse value '" + alcoholIntoxicationUseInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute ).Contains ( alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( heroinInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HeroinInLastThirtyDaysDayCount ).Contains ( heroinInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HeroinInLastThirtyDaysDayCount DensAsiNonResponse value '" + heroinInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( heroinInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HeroinInLifetimeYearCount ).Contains ( heroinInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HeroinInLifetimeYearCount DensAsiNonResponse value '" + heroinInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( heroinDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HeroinDensAsiDrugAlcoholAdministrationRoute ).Contains ( heroinDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HeroinDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + heroinDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( methadoneInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MethadoneInLastThirtyDaysDayCount ).Contains ( methadoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MethadoneInLastThirtyDaysDayCount DensAsiNonResponse value '" + methadoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( methadoneInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MethadoneInLifetimeYearCount ).Contains ( methadoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MethadoneInLifetimeYearCount DensAsiNonResponse value '" + methadoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( methadoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MethadoneDensAsiDrugAlcoholAdministrationRoute ).Contains ( methadoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MethadoneDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + methadoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( otherOpiatesInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherOpiatesInLastThirtyDaysDayCount ).Contains ( otherOpiatesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OtherOpiatesInLastThirtyDaysDayCount DensAsiNonResponse value '" + otherOpiatesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( otherOpiatesInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherOpiatesInLifetimeYearCount ).Contains ( otherOpiatesInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OtherOpiatesInLifetimeYearCount DensAsiNonResponse value '" + otherOpiatesInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( otherOpiatesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherOpiatesDensAsiDrugAlcoholAdministrationRoute ).Contains ( otherOpiatesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OtherOpiatesDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + otherOpiatesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( barbituratesInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => BarbituratesInLastThirtyDaysDayCount ).Contains ( barbituratesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "BarbituratesInLastThirtyDaysDayCount DensAsiNonResponse value '" + barbituratesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( barbituratesInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => BarbituratesInLifetimeYearCount ).Contains ( barbituratesInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "BarbituratesInLifetimeYearCount DensAsiNonResponse value '" + barbituratesInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( barbituratesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => BarbituratesDensAsiDrugAlcoholAdministrationRoute ).Contains ( barbituratesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "BarbituratesDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + barbituratesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( otherSedativesInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherSedativesInLastThirtyDaysDayCount ).Contains ( otherSedativesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OtherSedativesInLastThirtyDaysDayCount DensAsiNonResponse value '" + otherSedativesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( otherSedativesInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherSedativesInLifetimeYearCount ).Contains ( otherSedativesInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OtherSedativesInLifetimeYearCount DensAsiNonResponse value '" + otherSedativesInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( otherSedativesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherSedativesDensAsiDrugAlcoholAdministrationRoute ).Contains ( otherSedativesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OtherSedativesDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + otherSedativesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( cocaineInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => CocaineInLastThirtyDaysDayCount ).Contains ( cocaineInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "CocaineInLastThirtyDaysDayCount DensAsiNonResponse value '" + cocaineInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( cocaineInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => CocaineInLifetimeYearCount ).Contains ( cocaineInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "CocaineInLifetimeYearCount DensAsiNonResponse value '" + cocaineInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( cocaineDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => CocaineDensAsiDrugAlcoholAdministrationRoute ).Contains ( cocaineDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "CocaineDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + cocaineDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( amphetaminesInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AmphetaminesInLastThirtyDaysDayCount ).Contains ( amphetaminesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AmphetaminesInLastThirtyDaysDayCount DensAsiNonResponse value '" + amphetaminesInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( amphetaminesInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AmphetaminesInLifetimeYearCount ).Contains ( amphetaminesInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AmphetaminesInLifetimeYearCount DensAsiNonResponse value '" + amphetaminesInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( amphetaminesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AmphetaminesDensAsiDrugAlcoholAdministrationRoute ).Contains ( amphetaminesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AmphetaminesDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + amphetaminesDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( cannabisInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => CannabisInLastThirtyDaysDayCount ).Contains ( cannabisInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "CannabisInLastThirtyDaysDayCount DensAsiNonResponse value '" + cannabisInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( cannabisInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => CannabisInLifetimeYearCount ).Contains ( cannabisInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "CannabisInLifetimeYearCount DensAsiNonResponse value '" + cannabisInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( cannabisDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => CannabisDensAsiDrugAlcoholAdministrationRoute ).Contains ( cannabisDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "CannabisDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + cannabisDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hallucinogensInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HallucinogensInLastThirtyDaysDayCount ).Contains ( hallucinogensInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HallucinogensInLastThirtyDaysDayCount DensAsiNonResponse value '" + hallucinogensInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hallucinogensInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HallucinogensInLifetimeYearCount ).Contains ( hallucinogensInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HallucinogensInLifetimeYearCount DensAsiNonResponse value '" + hallucinogensInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hallucinogensDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HallucinogensDensAsiDrugAlcoholAdministrationRoute ).Contains ( hallucinogensDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HallucinogensDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + hallucinogensDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( inhalantsInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => InhalantsInLastThirtyDaysDayCount ).Contains ( inhalantsInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "InhalantsInLastThirtyDaysDayCount DensAsiNonResponse value '" + inhalantsInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( inhalantsInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => InhalantsInLifetimeYearCount ).Contains ( inhalantsInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "InhalantsInLifetimeYearCount DensAsiNonResponse value '" + inhalantsInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( inhalantsDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => InhalantsDensAsiDrugAlcoholAdministrationRoute ).Contains ( inhalantsDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "InhalantsDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + inhalantsDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( moreThanOneSubstancePerDayInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MoreThanOneSubstancePerDayInLastThirtyDaysDayCount ).Contains ( moreThanOneSubstancePerDayInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MoreThanOneSubstancePerDayInLastThirtyDaysDayCount DensAsiNonResponse value '" + moreThanOneSubstancePerDayInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( moreThanOneSubstancePerDayInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MoreThanOneSubstancePerDayInLifetimeYearCount ).Contains ( moreThanOneSubstancePerDayInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MoreThanOneSubstancePerDayInLifetimeYearCount DensAsiNonResponse value '" + moreThanOneSubstancePerDayInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( majorDensAsiProblematicSubstance.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MajorDensAsiProblematicSubstance ).Contains ( majorDensAsiProblematicSubstance.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MajorDensAsiProblematicSubstance DensAsiNonResponse value '" + majorDensAsiProblematicSubstance.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( voluntaryAbstinenceFromProblematicSubstanceMonthCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => VoluntaryAbstinenceFromProblematicSubstanceMonthCount ).Contains ( voluntaryAbstinenceFromProblematicSubstanceMonthCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "VoluntaryAbstinenceFromProblematicSubstanceMonthCount DensAsiNonResponse value '" + voluntaryAbstinenceFromProblematicSubstanceMonthCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( endOfProblematicSubstanceAbstinenceMonthCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => EndOfProblematicSubstanceAbstinenceMonthCount ).Contains ( endOfProblematicSubstanceAbstinenceMonthCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "EndOfProblematicSubstanceAbstinenceMonthCount DensAsiNonResponse value '" + endOfProblematicSubstanceAbstinenceMonthCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDtCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDtCount ).Contains ( alcoholDtCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDtCount DensAsiNonResponse value '" + alcoholDtCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( overdosedOnDrugsCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OverdosedOnDrugsCount ).Contains ( overdosedOnDrugsCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OverdosedOnDrugsCount DensAsiNonResponse value '" + overdosedOnDrugsCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholAbuseTreatmentCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholAbuseTreatmentCount ).Contains ( alcoholAbuseTreatmentCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholAbuseTreatmentCount DensAsiNonResponse value '" + alcoholAbuseTreatmentCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholDetoxTreatmentOnlyCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDetoxTreatmentOnlyCount ).Contains ( alcoholDetoxTreatmentOnlyCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholDetoxTreatmentOnlyCount DensAsiNonResponse value '" + alcoholDetoxTreatmentOnlyCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( moneySpentOnAlcoholInLastThirtyDaysAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MoneySpentOnAlcoholInLastThirtyDaysAmount ).Contains ( moneySpentOnAlcoholInLastThirtyDaysAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MoneySpentOnAlcoholInLastThirtyDaysAmount DensAsiNonResponse value '" + moneySpentOnAlcoholInLastThirtyDaysAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugAbuseTreatmentCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugAbuseTreatmentCount ).Contains ( drugAbuseTreatmentCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugAbuseTreatmentCount DensAsiNonResponse value '" + drugAbuseTreatmentCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugDetoxTreatmentOnlyCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDetoxTreatmentOnlyCount ).Contains ( drugDetoxTreatmentOnlyCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugDetoxTreatmentOnlyCount DensAsiNonResponse value '" + drugDetoxTreatmentOnlyCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( moneySpentOnDrugsInLastThirtyDaysAmount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MoneySpentOnDrugsInLastThirtyDaysAmount ).Contains ( moneySpentOnDrugsInLastThirtyDaysAmount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MoneySpentOnDrugsInLastThirtyDaysAmount DensAsiNonResponse value '" + moneySpentOnDrugsInLastThirtyDaysAmount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( outpatientTreatmentInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OutpatientTreatmentInLastThirtyDaysDayCount ).Contains ( outpatientTreatmentInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OutpatientTreatmentInLastThirtyDaysDayCount DensAsiNonResponse value '" + outpatientTreatmentInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( alcoholProblemInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholProblemInLastThirtyDaysDayCount ).Contains ( alcoholProblemInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AlcoholProblemInLastThirtyDaysDayCount DensAsiNonResponse value '" + alcoholProblemInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubledByAlcoholProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubledByAlcoholProblemsDensAsiPatientRating ).Contains ( troubledByAlcoholProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubledByAlcoholProblemsDensAsiPatientRating DensAsiNonResponse value '" + troubledByAlcoholProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfAlcoholProblemTreatmentDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating ).Contains ( importanceOfAlcoholProblemTreatmentDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating DensAsiNonResponse value '" + importanceOfAlcoholProblemTreatmentDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( drugProblemInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => this.DrugProblemInLastThirtyDaysDayCount ).Contains ( drugProblemInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DrugProblemInLastThirtyDaysDayCount DensAsiNonResponse value '" + drugProblemInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubledByDrugProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubledByDrugProblemsDensAsiPatientRating ).Contains ( troubledByDrugProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubledByDrugProblemsDensAsiPatientRating DensAsiNonResponse value '" + troubledByDrugProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfDrugProblemTreatmentDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfDrugProblemTreatmentDensAsiPatientRating ).Contains ( importanceOfDrugProblemTreatmentDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfDrugProblemTreatmentDensAsiPatientRating DensAsiNonResponse value '" + importanceOfDrugProblemTreatmentDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hydromorphoneInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HydromorphoneInLastThirtyDaysDayCount ).Contains ( hydromorphoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HydromorphoneInLastThirtyDaysDayCount DensAsiNonResponse value '" + hydromorphoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hydromorphoneInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HydromorphoneInLifetimeYearCount ).Contains ( hydromorphoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HydromorphoneInLifetimeYearCount DensAsiNonResponse value '" + hydromorphoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hydromorphoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HydromorphoneDensAsiDrugAlcoholAdministrationRoute ).Contains ( hydromorphoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HydromorphoneDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + hydromorphoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxycodoneInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxycodoneInLastThirtyDaysDayCount ).Contains ( oxycodoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxycodoneInLastThirtyDaysDayCount DensAsiNonResponse value '" + oxycodoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxycodoneInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxycodoneInLifetimeYearCount ).Contains ( oxycodoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxycodoneInLifetimeYearCount DensAsiNonResponse value '" + oxycodoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxycodoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxycodoneDensAsiDrugAlcoholAdministrationRoute ).Contains ( oxycodoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxycodoneDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + oxycodoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hydrocodoneInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HydrocodoneInLastThirtyDaysDayCount ).Contains ( hydrocodoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HydrocodoneInLastThirtyDaysDayCount DensAsiNonResponse value '" + hydrocodoneInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hydrocodoneInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HydrocodoneInLifetimeYearCount ).Contains ( hydrocodoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HydrocodoneInLifetimeYearCount DensAsiNonResponse value '" + hydrocodoneInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hydrocodoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HydrocodoneDensAsiDrugAlcoholAdministrationRoute ).Contains ( hydrocodoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HydrocodoneDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + hydrocodoneDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( buprenorphineInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => BuprenorphineInLastThirtyDaysDayCount ).Contains ( buprenorphineInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "BuprenorphineInLastThirtyDaysDayCount DensAsiNonResponse value '" + buprenorphineInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( buprenorphineInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => BuprenorphineInLifetimeYearCount ).Contains ( buprenorphineInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "BuprenorphineInLifetimeYearCount DensAsiNonResponse value '" + buprenorphineInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( buprenorphineDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => BuprenorphineDensAsiDrugAlcoholAdministrationRoute ).Contains ( buprenorphineDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "BuprenorphineDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + buprenorphineDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxyContinInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinInLastThirtyDaysDayCount ).Contains ( oxyContinInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxyContinInLastThirtyDaysDayCount DensAsiNonResponse value '" + oxyContinInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxyContinInLifetimeYearCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinInLifetimeYearCount ).Contains ( oxyContinInLifetimeYearCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxyContinInLifetimeYearCount DensAsiNonResponse value '" + oxyContinInLifetimeYearCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxyContinDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinDensAsiDrugAlcoholAdministrationRoute ).Contains ( oxyContinDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxyContinDensAsiDrugAlcoholAdministrationRoute DensAsiNonResponse value '" + oxyContinDensAsiDrugAlcoholAdministrationRoute.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxyContinPrescribedForMedicalReasonIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinPrescribedForMedicalReasonIndicator ).Contains ( oxyContinPrescribedForMedicalReasonIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxyContinPrescribedForMedicalReasonIndicator DensAsiNonResponse value '" + oxyContinPrescribedForMedicalReasonIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxyContinUseToGetHighIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinUseToGetHighIndicator ).Contains ( oxyContinUseToGetHighIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxyContinUseToGetHighIndicator DensAsiNonResponse value '" + oxyContinUseToGetHighIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxyContinTakenWithOtherOpiatesIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinTakenWithOtherOpiatesIndicator ).Contains ( oxyContinTakenWithOtherOpiatesIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxyContinTakenWithOtherOpiatesIndicator DensAsiNonResponse value '" + oxyContinTakenWithOtherOpiatesIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( afterOxyContinFirstUseMonthCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AfterOxyContinFirstUseMonthCount ).Contains ( afterOxyContinFirstUseMonthCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AfterOxyContinFirstUseMonthCount DensAsiNonResponse value '" + afterOxyContinFirstUseMonthCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( oxyContinFromFriendFamilyStreetIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinFromFriendFamilyStreetIndicator ).Contains ( oxyContinFromFriendFamilyStreetIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OxyContinFromFriendFamilyStreetIndicator DensAsiNonResponse value '" + oxyContinFromFriendFamilyStreetIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _anyAlcoholUseInLastThirtyDaysDayCount = anyAlcoholUseInLastThirtyDaysDayCount;
            _anyAlcoholUseInLifetimeYearCount = anyAlcoholUseInLifetimeYearCount;
            _anyAlcoholDensAsiDrugAlcoholAdministrationRoute = anyAlcoholDensAsiDrugAlcoholAdministrationRoute;
            _anyAlcoholUseNote = anyAlcoholUseNote;
            _alcoholIntoxicationInLastThirtyDaysDayCount = alcoholIntoxicationInLastThirtyDaysDayCount;
            _alcoholIntoxicationUseInLifetimeYearCount = alcoholIntoxicationUseInLifetimeYearCount;
            _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute = alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute;
            _alcoholIntoxicationNote = alcoholIntoxicationNote;
            _heroinInLastThirtyDaysDayCount = heroinInLastThirtyDaysDayCount;
            _heroinInLifetimeYearCount = heroinInLifetimeYearCount;
            _heroinDensAsiDrugAlcoholAdministrationRoute = heroinDensAsiDrugAlcoholAdministrationRoute;
            _heroinNote = heroinNote;
            _methadoneInLastThirtyDaysDayCount = methadoneInLastThirtyDaysDayCount;
            _methadoneInLifetimeYearCount = methadoneInLifetimeYearCount;
            _methadoneDensAsiDrugAlcoholAdministrationRoute = methadoneDensAsiDrugAlcoholAdministrationRoute;
            _methadoneNote = methadoneNote;
            _otherOpiatesInLastThirtyDaysDayCount = otherOpiatesInLastThirtyDaysDayCount;
            _otherOpiatesInLifetimeYearCount = otherOpiatesInLifetimeYearCount;
            _otherOpiatesDensAsiDrugAlcoholAdministrationRoute = otherOpiatesDensAsiDrugAlcoholAdministrationRoute;
            _otherOpiatesNote = otherOpiatesNote;
            _barbituratesInLastThirtyDaysDayCount = barbituratesInLastThirtyDaysDayCount;
            _barbituratesInLifetimeYearCount = barbituratesInLifetimeYearCount;
            _barbituratesDensAsiDrugAlcoholAdministrationRoute = barbituratesDensAsiDrugAlcoholAdministrationRoute;
            _barbituratesNote = barbituratesNote;
            _otherSedativesInLastThirtyDaysDayCount = otherSedativesInLastThirtyDaysDayCount;
            _otherSedativesInLifetimeYearCount = otherSedativesInLifetimeYearCount;
            _otherSedativesDensAsiDrugAlcoholAdministrationRoute = otherSedativesDensAsiDrugAlcoholAdministrationRoute;
            _otherSedativesNote = otherSedativesNote;
            _cocaineInLastThirtyDaysDayCount = cocaineInLastThirtyDaysDayCount;
            _cocaineInLifetimeYearCount = cocaineInLifetimeYearCount;
            _cocaineDensAsiDrugAlcoholAdministrationRoute = cocaineDensAsiDrugAlcoholAdministrationRoute;
            _cocaineNote = cocaineNote;
            _amphetaminesInLastThirtyDaysDayCount = amphetaminesInLastThirtyDaysDayCount;
            _amphetaminesInLifetimeYearCount = amphetaminesInLifetimeYearCount;
            _amphetaminesDensAsiDrugAlcoholAdministrationRoute = amphetaminesDensAsiDrugAlcoholAdministrationRoute;
            _amphetaminesNote = amphetaminesNote;
            _cannabisInLastThirtyDaysDayCount = cannabisInLastThirtyDaysDayCount;
            _cannabisInLifetimeYearCount = cannabisInLifetimeYearCount;
            _cannabisDensAsiDrugAlcoholAdministrationRoute = cannabisDensAsiDrugAlcoholAdministrationRoute;
            _cannabisNote = cannabisNote;
            _hallucinogensInLastThirtyDaysDayCount = hallucinogensInLastThirtyDaysDayCount;
            _hallucinogensInLifetimeYearCount = hallucinogensInLifetimeYearCount;
            _hallucinogensDensAsiDrugAlcoholAdministrationRoute = hallucinogensDensAsiDrugAlcoholAdministrationRoute;
            _hallucinogensNote = hallucinogensNote;
            _inhalantsInLastThirtyDaysDayCount = inhalantsInLastThirtyDaysDayCount;
            _inhalantsInLifetimeYearCount = inhalantsInLifetimeYearCount;
            _inhalantsDensAsiDrugAlcoholAdministrationRoute = inhalantsDensAsiDrugAlcoholAdministrationRoute;
            _inhalantsNote = inhalantsNote;
            _moreThanOneSubstancePerDayInLastThirtyDaysDayCount = moreThanOneSubstancePerDayInLastThirtyDaysDayCount;
            _moreThanOneSubstancePerDayInLifetimeYearCount = moreThanOneSubstancePerDayInLifetimeYearCount;
            _moreThanOneSubstancePerDayNote = moreThanOneSubstancePerDayNote;
            _majorDensAsiProblematicSubstance = majorDensAsiProblematicSubstance;
            _majorDensAsiProblematicSubstanceNote = majorDensAsiProblematicSubstanceNote;
            _voluntaryAbstinenceFromProblematicSubstanceMonthCount = voluntaryAbstinenceFromProblematicSubstanceMonthCount;
            _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote = voluntaryAbstinenceFromProblematicSubstanceMonthCountNote;
            _endOfProblematicSubstanceAbstinenceMonthCount = endOfProblematicSubstanceAbstinenceMonthCount;
            _endOfProblematicSubstanceAbstinenceMonthCountNote = endOfProblematicSubstanceAbstinenceMonthCountNote;
            _alcoholDtCount = alcoholDtCount;
            _alcoholDtCountNote = alcoholDtCountNote;
            _overdosedOnDrugsCount = overdosedOnDrugsCount;
            _overdosedOnDrugsCountNote = overdosedOnDrugsCountNote;
            _alcoholAbuseTreatmentCount = alcoholAbuseTreatmentCount;
            _alcoholAbuseTreatmentCountNote = alcoholAbuseTreatmentCountNote;
            _alcoholDetoxTreatmentOnlyCount = alcoholDetoxTreatmentOnlyCount;
            _alcoholDetoxTreatmentOnlyCountNote = alcoholDetoxTreatmentOnlyCountNote;
            _moneySpentOnAlcoholInLastThirtyDaysAmount = moneySpentOnAlcoholInLastThirtyDaysAmount;
            _moneySpentOnAlcoholInLastThirtyDaysAmountNote = moneySpentOnAlcoholInLastThirtyDaysAmountNote;
            _drugAbuseTreatmentCount = drugAbuseTreatmentCount;
            _drugAbuseTreatmentCountNote = drugAbuseTreatmentCountNote;
            _drugDetoxTreatmentOnlyCount = drugDetoxTreatmentOnlyCount;
            _drugDetoxTreatmentOnlyCountNote = drugDetoxTreatmentOnlyCountNote;
            _moneySpentOnDrugsInLastThirtyDaysAmount = moneySpentOnDrugsInLastThirtyDaysAmount;
            _moneySpentOnDrugsInLastThirtyDaysAmountNote = moneySpentOnDrugsInLastThirtyDaysAmountNote;
            _outpatientTreatmentInLastThirtyDaysDayCount = outpatientTreatmentInLastThirtyDaysDayCount;
            _outpatientTreatmentInLastThirtyDaysDayCountNote = outpatientTreatmentInLastThirtyDaysDayCountNote;
            _alcoholProblemInLastThirtyDaysDayCount = alcoholProblemInLastThirtyDaysDayCount;
            _alcoholProblemInLastThirtyDaysDayCountNote = alcoholProblemInLastThirtyDaysDayCountNote;
            _troubledByAlcoholProblemsDensAsiPatientRating = troubledByAlcoholProblemsDensAsiPatientRating;
            _troubledByAlcoholProblemsDensAsiPatientRatingNote = troubledByAlcoholProblemsDensAsiPatientRatingNote;
            _importanceOfAlcoholProblemTreatmentDensAsiPatientRating = importanceOfAlcoholProblemTreatmentDensAsiPatientRating;
            _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote = importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote;
            this._drugProblemInLastThirtyDaysDayCount = drugProblemInLastThirtyDaysDayCount;
            _drugProblemInLastThirtyDaysDayCountNote = drugProblemInLastThirtyDaysDayCountNote;
            _troubledByDrugProblemsDensAsiPatientRating = troubledByDrugProblemsDensAsiPatientRating;
            _troubledByDrugProblemsDensAsiPatientRatingNote = troubledByDrugProblemsDensAsiPatientRatingNote;
            _importanceOfDrugProblemTreatmentDensAsiPatientRating = importanceOfDrugProblemTreatmentDensAsiPatientRating;
            _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote = importanceOfDrugProblemTreatmentDensAsiPatientRatingNote;
            _patientAlcoholTreatmentDensAsiInterviewerRating = patientAlcoholTreatmentDensAsiInterviewerRating;
            _patientAlcoholTreatmentDensAsiInterviewerRatingNote = patientAlcoholTreatmentDensAsiInterviewerRatingNote;
            _patientDrugTreatmentDensAsiInterviewerRating = patientDrugTreatmentDensAsiInterviewerRating;
            _patientDrugTreatmentDensAsiInterviewerRatingNote = patientDrugTreatmentDensAsiInterviewerRatingNote;
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            _hydromorphoneInLastThirtyDaysDayCount = hydromorphoneInLastThirtyDaysDayCount;
            _hydromorphoneInLifetimeYearCount = hydromorphoneInLifetimeYearCount;
            _hydromorphoneDensAsiDrugAlcoholAdministrationRoute = hydromorphoneDensAsiDrugAlcoholAdministrationRoute;
            _hydromorphoneNote = hydromorphoneNote;
            _oxycodoneInLastThirtyDaysDayCount = oxycodoneInLastThirtyDaysDayCount;
            _oxycodoneInLifetimeYearCount = oxycodoneInLifetimeYearCount;
            _oxycodoneDensAsiDrugAlcoholAdministrationRoute = oxycodoneDensAsiDrugAlcoholAdministrationRoute;
            _oxycodoneNote = oxycodoneNote;
            _hydrocodoneInLastThirtyDaysDayCount = hydrocodoneInLastThirtyDaysDayCount;
            _hydrocodoneInLifetimeYearCount = hydrocodoneInLifetimeYearCount;
            _hydrocodoneDensAsiDrugAlcoholAdministrationRoute = hydrocodoneDensAsiDrugAlcoholAdministrationRoute;
            _hydrocodoneNote = hydrocodoneNote;
            _buprenorphineInLastThirtyDaysDayCount = buprenorphineInLastThirtyDaysDayCount;
            _buprenorphineInLifetimeYearCount = buprenorphineInLifetimeYearCount;
            _buprenorphineDensAsiDrugAlcoholAdministrationRoute = buprenorphineDensAsiDrugAlcoholAdministrationRoute;
            _buprenorphineNote = buprenorphineNote;
            _oxyContinInLastThirtyDaysDayCount = oxyContinInLastThirtyDaysDayCount;
            _oxyContinInLifetimeYearCount = oxyContinInLifetimeYearCount;
            _oxyContinDensAsiDrugAlcoholAdministrationRoute = oxyContinDensAsiDrugAlcoholAdministrationRoute;
            _oxyContinNote = oxyContinNote;
            _oxyContinPrescribedForMedicalReasonIndicator = oxyContinPrescribedForMedicalReasonIndicator;
            _oxyContinPrescribedForMedicalReasonIndicatorNote = oxyContinPrescribedForMedicalReasonIndicatorNote;
            _oxyContinUseToGetHighIndicator = oxyContinUseToGetHighIndicator;
            _oxyContinUseToGetHighIndicatorNote = oxyContinUseToGetHighIndicatorNote;
            _oxyContinTakenWithOtherOpiatesIndicator = oxyContinTakenWithOtherOpiatesIndicator;
            _oxyContinTakenWithOtherOpiatesIndicatorNote = oxyContinTakenWithOtherOpiatesIndicatorNote;
            _afterOxyContinFirstUseMonthCount = afterOxyContinFirstUseMonthCount;
            _afterOxyContinFirstUseMonthCountNote = afterOxyContinFirstUseMonthCountNote;
            _oxyContinFromFriendFamilyStreetIndicator = oxyContinFromFriendFamilyStreetIndicator;
            _oxyContinFromFriendFamilyStreetIndicatorNote = oxyContinFromFriendFamilyStreetIndicatorNote;
            _sectionNote = sectionNote;
        }


        /// <summary>
        /// Gets the alcohol use occurances in the last thirty days.
        /// Question Number: D1
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AnyAlcoholUseInLastThirtyDaysDayCount
        {
            get { return _anyAlcoholUseInLastThirtyDaysDayCount; }
            private set {}
        }

        /// <summary>
        /// Gets the alcohol years of regular use.
        /// Question Number: D1
        /// </summary>
        /// <value>DensAsiNonResponseType&lt;int?&gt; containing the value.</value>
        public virtual DensAsiNonResponseType<int?> AnyAlcoholUseInLifetimeYearCount
        {
            get { return _anyAlcoholUseInLifetimeYearCount; }
            private set {}
        }

        /// <summary>
        /// Gets the alcohol drug administration route.
        /// Question Number: D1
        /// </summary>
        /// <value>DensAsiNonResponseType&lt;DensAsiDrugAlcoholAdministrationRoute&gt; containing the value.</value>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> AnyAlcoholDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _anyAlcoholDensAsiDrugAlcoholAdministrationRoute; }
            private set {}
        }

        /// <summary>
        /// Gets the alcohol usage note.
        /// Question Number: D1
        /// </summary>
        public virtual string AnyAlcoholUseNote
        {
            get { return _anyAlcoholUseNote; }
            private set {}
        }

        /// <summary>
        /// Gets the alcohol intoxication occurances in last thirty days days.
        /// Question Number: D2
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AlcoholIntoxicationInLastThirtyDaysDayCount
        {
            get { return _alcoholIntoxicationInLastThirtyDaysDayCount; }
            private set {}
        }

        /// <summary>
        /// Gets the alcohol intoxication years of regular use.
        /// Question Number: D2
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AlcoholIntoxicationUseInLifetimeYearCount
        {
            get { return _alcoholIntoxicationUseInLifetimeYearCount; }
            private set {}
        }

        /// <summary>
        /// Gets the alcohol intoxication administration route.
        /// Question Number: D2
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _alcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the alcohol intoxication note.
        /// Question Number: D2
        /// </summary>
        public virtual string AlcoholIntoxicationNote
        {
            get { return _alcoholIntoxicationNote; }
            private set { }
        }

        /// <summary>
        /// Gets the heroin use in last thirty days days.
        /// Question Number: D3
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HeroinInLastThirtyDaysDayCount
        {
            get { return _heroinInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the use a heroin in lifetime years.
        /// Question Number: D3
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HeroinInLifetimeYearCount
        {
            get { return _heroinInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the heroin, drug or alcohol most severe administration route.
        /// Question Number: D3
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> HeroinDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _heroinDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the heroin note.
        /// Question Number: D3
        /// </summary>
        public virtual string HeroinNote
        {
            get { return _heroinNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of methadone uses in last thirty days days.
        /// Question Number: D4
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MethadoneInLastThirtyDaysDayCount
        {
            get { return _methadoneInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years use of methadone in lifetime.
        /// Question Number: D4
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MethadoneInLifetimeYearCount
        {
            get { return _methadoneInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the methadone drug or alcohol most severe administration route.
        /// Question Number: D4
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> MethadoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _methadoneDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the methadone note.
        /// Question Number: D4
        /// </summary>
        public virtual string MethadoneNote
        {
            get { return _methadoneNote; }
            private set { }
        }

        /// <summary>
        /// Gets the usages of other opiates in last thirty days days.
        /// Question Number: D5
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OtherOpiatesInLastThirtyDaysDayCount
        {
            get { return _otherOpiatesInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the years of usages of other opiates in lifetime.
        /// Question Number: D5
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OtherOpiatesInLifetimeYearCount
        {
            get { return _otherOpiatesInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the other opiates, drug and alcohol most severe administration route.
        /// Question Number: D5
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> OtherOpiatesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _otherOpiatesDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the other opiates note.
        /// Question Number: D5
        /// </summary>
        public virtual string OtherOpiatesNote
        {
            get { return _otherOpiatesNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of usages of barbiturates in last thirty days days.
        /// Question Number: D6
        /// </summary>
        public virtual DensAsiNonResponseType<int?> BarbituratesInLastThirtyDaysDayCount
        {
            get { return _barbituratesInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of barbiturates use in lifetime.
        /// Question Number: D6
        /// </summary>
        public virtual DensAsiNonResponseType<int?> BarbituratesInLifetimeYearCount
        {
            get { return _barbituratesInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the barbiturates, drug or alcohol most severe administration route.
        /// Question Number: D6
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> BarbituratesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _barbituratesDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the barbiturates note.
        /// Question Number: D6
        /// </summary>
        public virtual string BarbituratesNote
        {
            get { return _barbituratesNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of usages of other sedatives in last thirty days.
        /// Question Number: D7
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OtherSedativesInLastThirtyDaysDayCount
        {
            get { return _otherSedativesInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of other sedatives usages in last thirty days.
        /// Question Number: D7
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OtherSedativesInLifetimeYearCount
        {
            get { return _otherSedativesInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the other sedatives, drug or alcohol most severe administration route.
        /// Question Number: D7
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> OtherSedativesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _otherSedativesDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the other sedatives note.
        /// Question Number: D7
        /// </summary>
        public virtual string OtherSedativesNote
        {
            get { return _otherSedativesNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of usgaes of cocaine in last thirty days.
        /// Question Number: D8
        /// </summary>
        public virtual DensAsiNonResponseType<int?> CocaineInLastThirtyDaysDayCount
        {
            get { return _cocaineInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of cocaine usage in lifetime.
        /// Question Number: D8
        /// </summary>
        public virtual DensAsiNonResponseType<int?> CocaineInLifetimeYearCount
        {
            get { return _cocaineInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the cocaine, drug or alcohol most severe administration route.
        /// Question Number: D8
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> CocaineDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _cocaineDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the cocaine note.
        /// Question Number: D8
        /// </summary>
        public virtual string CocaineNote
        {
            get { return _cocaineNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of usages of amphetamines in last thirty days days.
        /// Question Number: D9
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AmphetaminesInLastThirtyDaysDayCount
        {
            get { return _amphetaminesInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of amphetamines usgae in lifetime.
        /// Question Number: D9
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AmphetaminesInLifetimeYearCount
        {
            get { return _amphetaminesInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the amphetamines, drug or alcohol most severe administration route.
        /// Question Number: D9
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> AmphetaminesDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _amphetaminesDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the amphetamines note.
        /// Question Number: D9
        /// </summary>
        public virtual string AmphetaminesNote
        {
            get { return _amphetaminesNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of usages of cannabis in last thirty days.
        /// Question Number: D10
        /// </summary>
        public virtual DensAsiNonResponseType<int?> CannabisInLastThirtyDaysDayCount
        {
            get { return _cannabisInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of cannabis use in lifetime.
        /// Question Number: D10
        /// </summary>
        public virtual DensAsiNonResponseType<int?> CannabisInLifetimeYearCount
        {
            get { return _cannabisInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the cannabis, drug or alcohol most severe administration route.
        /// Question Number: D10
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> CannabisDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _cannabisDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the cannabis note.
        /// Question Number: D10
        /// </summary>
        public virtual string CannabisNote
        {
            get { return _cannabisNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of usages of hallucinogens in last thirty days.
        /// Question Number: D11
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HallucinogensInLastThirtyDaysDayCount
        {
            get { return _hallucinogensInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of use of hallucinogens in lifetime.
        /// Question Number: D11
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HallucinogensInLifetimeYearCount
        {
            get { return _hallucinogensInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the hallucinogens, drug or alcohol most severe administration route.
        /// Question Number: D11
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> HallucinogensDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _hallucinogensDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the hallucinogens note.
        /// Question Number: D11
        /// </summary>
        public virtual string HallucinogensNote
        {
            get { return _hallucinogensNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of usages of inhalants in the last thirty days.
        /// Question Number: D12
        /// </summary>
        public virtual DensAsiNonResponseType<int?> InhalantsInLastThirtyDaysDayCount
        {
            get { return _inhalantsInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of use of inhalants in lifetime.
        /// Question Number: D12
        /// </summary>
        public virtual DensAsiNonResponseType<int?> InhalantsInLifetimeYearCount
        {
            get { return _inhalantsInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the inhalants, drug or alcohol most severe administration route.
        /// Question Number: D12
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> InhalantsDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _inhalantsDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the inhalants note.
        /// Question Number: D12
        /// </summary>
        public virtual string InhalantsNote
        {
            get { return _inhalantsNote; }
            private set { }
        }

        /// <summary>
        /// Gets the the occurances of use of more than one substance per day in the last thirty days.
        /// Question Number: D13
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MoreThanOneSubstancePerDayInLastThirtyDaysDayCount
        {
            get { return _moreThanOneSubstancePerDayInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the the number of years of use of more than one substance per day in lifetime.
        /// Question Number: D13
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MoreThanOneSubstancePerDayInLifetimeYearCount
        {
            get { return _moreThanOneSubstancePerDayInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the use of more than one substance per day note.
        /// Question Number: D13
        /// </summary>
        public virtual string MoreThanOneSubstancePerDayNote
        {
            get { return _moreThanOneSubstancePerDayNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiProblematicSubstance">MajorDensAsiProblematicSubstance</see>  
        /// denoting the patient's main problematic substance.
        /// Question Number: D14
        /// </summary>        
        public virtual DensAsiNonResponseType<DensAsiProblematicSubstance> MajorDensAsiProblematicSubstance
        {
            get { return _majorDensAsiProblematicSubstance; }
            private set { }
        }

        /// <summary>
        /// Gets the patient's major problematic substance note.
        /// Question Number: D14
        /// </summary>
        public virtual string MajorDensAsiProblematicSubstanceNote
        {
            get { return _majorDensAsiProblematicSubstanceNote; }
            private set { }
        }

        /// <summary>
        /// Gets how long the last period of at least 1 month voluntary abstinence lasted.
        /// Question Number: D15
        /// </summary>
        public virtual DensAsiNonResponseType<int?> VoluntaryAbstinenceFromProblematicSubstanceMonthCount
        {
            get { return _voluntaryAbstinenceFromProblematicSubstanceMonthCount; }
            private set { }
        }

        /// <summary>
        /// Gets how long the last period of at least 1 month voluntary abstinence lasted note.
        /// Question Number: D15
        /// </summary>
        public virtual string VoluntaryAbstinenceFromProblematicSubstanceMonthCountNote
        {
            get { return _voluntaryAbstinenceFromProblematicSubstanceMonthCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of months ago that the abstinence ended.
        /// Question Number: D16
        /// </summary>
        public virtual DensAsiNonResponseType<int?> EndOfProblematicSubstanceAbstinenceMonthCount
        {
            get { return _endOfProblematicSubstanceAbstinenceMonthCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of months ago that the abstinence ended note.
        /// Question Number: D16
        /// </summary>
        public virtual string EndOfProblematicSubstanceAbstinenceMonthCountNote
        {
            get { return _endOfProblematicSubstanceAbstinenceMonthCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the alcohol delirium tremens count.
        /// Question Number: D17
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AlcoholDtCount
        {
            get { return _alcoholDtCount; }
            private set { }
        }

        /// <summary>
        /// Gets the alcohol delirium tremens note.
        /// Question Number: D17
        /// </summary>
        public virtual string AlcoholDtCountNote
        {
            get { return _alcoholDtCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of times the patient has overdosed on drugs.
        /// Question Number: D18
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OverdosedOnDrugsCount
        {
            get { return _overdosedOnDrugsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of times the patient has overdosed on drugs note.
        /// Question Number: D18
        /// </summary>
        public virtual string OverdosedOnDrugsCountNote
        {
            get { return _overdosedOnDrugsCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient alcohol abuse treatments.
        /// Question Number: D19
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AlcoholAbuseTreatmentCount
        {
            get { return _alcoholAbuseTreatmentCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient alcohol abuse treatments note.
        /// Question Number: D19
        /// </summary>
        public virtual string AlcoholAbuseTreatmentCountNote
        {
            get { return _alcoholAbuseTreatmentCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient alcohol detox treatments only.
        /// Question Number: D21
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AlcoholDetoxTreatmentOnlyCount
        {
            get { return _alcoholDetoxTreatmentOnlyCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient alcohol detox treatments only note.
        /// Question Number: D21
        /// </summary>
        public virtual string AlcoholDetoxTreatmentOnlyCountNote
        {
            get { return _alcoholDetoxTreatmentOnlyCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the money spent on alcohol in last thirty days amount.
        /// Question Number: D23
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MoneySpentOnAlcoholInLastThirtyDaysAmount
        {
            get { return _moneySpentOnAlcoholInLastThirtyDaysAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the money spent on alcohol in last thirty days amount note.
        /// Question Number: D23
        /// </summary>
        public virtual string MoneySpentOnAlcoholInLastThirtyDaysAmountNote
        {
            get { return _moneySpentOnAlcoholInLastThirtyDaysAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of drug abuse treatments.
        /// Question Number: D20
        /// </summary>
        public virtual DensAsiNonResponseType<int?> DrugAbuseTreatmentCount
        {
            get { return _drugAbuseTreatmentCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of drug abuse treatments note.
        /// Question Number: D20
        /// </summary>
        public virtual string DrugAbuseTreatmentCountNote
        {
            get { return _drugAbuseTreatmentCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of drug detox treatments only.
        /// Question Number: D22
        /// </summary>
        public virtual DensAsiNonResponseType<int?> DrugDetoxTreatmentOnlyCount
        {
            get { return _drugDetoxTreatmentOnlyCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of drug detox treatments only note.
        /// Question Number: D22
        /// </summary>
        public virtual string DrugDetoxTreatmentOnlyCountNote
        {
            get { return _drugDetoxTreatmentOnlyCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the amount of money spent on drugs in last thirty days.
        /// Question Number: D24
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MoneySpentOnDrugsInLastThirtyDaysAmount
        {
            get { return _moneySpentOnDrugsInLastThirtyDaysAmount; }
            private set { }
        }

        /// <summary>
        /// Gets the amount of money spent on drugs in last thirty days note.
        /// Question Number: D24
        /// </summary>
        public virtual string MoneySpentOnDrugsInLastThirtyDaysAmountNote
        {
            get { return _moneySpentOnDrugsInLastThirtyDaysAmountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty of outpatient treatment.
        /// Question Number: D25
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OutpatientTreatmentInLastThirtyDaysDayCount
        {
            get { return _outpatientTreatmentInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty of outpatient treatment note.
        /// Question Number: D25
        /// </summary>
        public virtual string OutpatientTreatmentInLastThirtyDaysDayCountNote
        {
            get { return _outpatientTreatmentInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty of alcohol problems.
        /// Question Number: D26
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AlcoholProblemInLastThirtyDaysDayCount
        {
            get { return _alcoholProblemInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty of alcohol problems note.
        /// Question Number: D26
        /// </summary>
        public virtual string AlcoholProblemInLastThirtyDaysDayCountNote
        {
            get { return _alcoholProblemInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledByAlcoholProblemsDensAsiPatientRating</see>
        /// denoting whether the patient is troubled by alcohol problems. Question Number: D28
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TroubledByAlcoholProblemsDensAsiPatientRating
        {
            get { return _troubledByAlcoholProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the rating denoting whether the patient is troubled by alcohol problems note.
        /// Question Number: D28
        /// </summary>
        public virtual string TroubledByAlcoholProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByAlcoholProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating</see>
        /// denoting the importance of treatment for alcohol problems. Question Number: D30
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfAlcoholProblemTreatmentDensAsiPatientRating
        {
            get { return _importanceOfAlcoholProblemTreatmentDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the rating denoting the importance of treatment for alcohol problems note.
        /// Question Number: D30
        /// </summary>
        public virtual string ImportanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote
        {
            get { return _importanceOfAlcoholProblemTreatmentDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty that the patient has experienced drug problems.
        /// Question Number: D27
        /// </summary>
        public virtual DensAsiNonResponseType<int?> DrugProblemInLastThirtyDaysDayCount
        {
            get { return _drugProblemInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty that the patient has experienced drug problems note.
        /// Question Number: D27
        /// </summary>
        public virtual string DrugProblemInLastThirtyDaysDayCountNote
        {
            get { return _drugProblemInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledByDrugProblemsDensAsiPatientRating</see>
        /// denoting how troubled the patient been in the last thirty days by drug problems. Question Number: D29
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TroubledByDrugProblemsDensAsiPatientRating
        {
            get { return _troubledByDrugProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets how troubled the patient been in the last thirty days by drug problems note.
        /// Question Number: D29
        /// </summary>
        public virtual string TroubledByDrugProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByDrugProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledByDrugProblemsDensAsiPatientRating</see>
        /// denoting the current importance of drug treatment to the patient. Question Number: D31
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfDrugProblemTreatmentDensAsiPatientRating
        {
            get { return _importanceOfDrugProblemTreatmentDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the current importance of drug treatment to the patient.
        /// Question Number: D31
        /// </summary>
        public virtual string ImportanceOfDrugProblemTreatmentDensAsiPatientRatingNote
        {
            get { return _importanceOfDrugProblemTreatmentDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">PatientAlcoholTreatmentDensAsiInterviewerRating</see>
        /// denoting the interviewer's importance rating for alcohol treatment. Question Number: D32
        /// </summary>
        public virtual DensAsiInterviewerRating PatientAlcoholTreatmentDensAsiInterviewerRating
        {
            get { return _patientAlcoholTreatmentDensAsiInterviewerRating; }
            private set { }
        }

        /// <summary>
        /// Gets the interviewer's importance rating for alcohol treatment.
        /// Question Number: D32
        /// </summary>
        public virtual string PatientAlcoholTreatmentDensAsiInterviewerRatingNote
        {
            get { return _patientAlcoholTreatmentDensAsiInterviewerRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">PatientDrugTreatmentDensAsiInterviewerRating</see>
        /// denoting the interviewer's importance rating for drug treatment. Question Number: D33
        /// </summary>
        public virtual DensAsiInterviewerRating PatientDrugTreatmentDensAsiInterviewerRating
        {
            get { return _patientDrugTreatmentDensAsiInterviewerRating; }
            private set { }
        }

        /// <summary>
        /// Question Number: D33
        /// Gets the interviewer's importance rating for drug treatment note.
        /// </summary>
        public virtual string PatientDrugTreatmentDensAsiInterviewerRatingNote
        {
            get { return _patientDrugTreatmentDensAsiInterviewerRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation.
        /// Question Number: D34
        /// </summary>
        public virtual bool? ConfidenceDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation note.
        /// Question Number: D34
        /// </summary>
        public virtual string ConfidenceDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand.
        /// Question Number: D35
        /// </summary>
        public virtual bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand note.
        /// Question Number: D35
        /// </summary>
        public virtual string ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of times that the patient used hydromorphone in last thirty days.
        /// Question Number: DRG12
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HydromorphoneInLastThirtyDaysDayCount
        {
            get { return _hydromorphoneInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years that the patient used hydromorphone in lifetime.
        /// Question Number: DRG12
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HydromorphoneInLifetimeYearCount
        {
            get { return _hydromorphoneInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the most severe hydromorphone or alcohol administration route.
        /// Question Number: DRG12
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> HydromorphoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _hydromorphoneDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the hydromorphone note.
        /// Question Number: DRG12
        /// </summary>
        public virtual string HydromorphoneNote
        {
            get { return _hydromorphoneNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of times that the patient used oxycodone in last thirty days.
        /// Question Number: DRG13
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OxycodoneInLastThirtyDaysDayCount
        {
            get { return _oxycodoneInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years that the patient used oxycodone in lifetime.
        /// Question Number: DRG13
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OxycodoneInLifetimeYearCount
        {
            get { return _oxycodoneInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the most severe oxycodone, drug or alcohol administration route.
        /// Question Number: DRG13
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> OxycodoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _oxycodoneDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the oxycodone note.
        /// Question Number: DRG13
        /// </summary>
        public virtual string OxycodoneNote
        {
            get { return _oxycodoneNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient uses of hydrocodone in last thirty days.
        /// Question Number: DRG14
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HydrocodoneInLastThirtyDaysDayCount
        {
            get { return _hydrocodoneInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of patient uses of hydrocodone.
        /// Question Number: DRG14
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HydrocodoneInLifetimeYearCount
        {
            get { return _hydrocodoneInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholAdministrationRoute">HydrocodoneDensAsiDrugAlcoholAdministrationRoute</see>
        /// denoting the most severe hydrocodone, drug or alcohol administration route. Question Number: DRG14
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> HydrocodoneDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _hydrocodoneDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the hydrocodone note.
        /// Question Number: DRG14
        /// </summary>
        public virtual string HydrocodoneNote
        {
            get { return _hydrocodoneNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient uses of buprenorphine in last thirty days.
        /// Question Number: DRG17
        /// </summary>
        public virtual DensAsiNonResponseType<int?> BuprenorphineInLastThirtyDaysDayCount
        {
            get { return _buprenorphineInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years of patient uses of buprenorphine in lifetime.
        /// Question Number: DRG17
        /// </summary>
        public virtual DensAsiNonResponseType<int?> BuprenorphineInLifetimeYearCount
        {
            get { return _buprenorphineInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholAdministrationRoute">BuprenorphineDensAsiDrugAlcoholAdministrationRoute</see>
        /// denoting the most severe buprenorphine, drug or alcohol administration route. Question Number: DRG17
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> BuprenorphineDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _buprenorphineDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the buprenorphine note.
        /// Question Number: DRG17
        /// </summary>
        public virtual string BuprenorphineNote
        {
            get { return _buprenorphineNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient uses of oxycontin in last thirty days.
        /// Question Number: DRG16
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OxyContinInLastThirtyDaysDayCount
        {
            get { return _oxyContinInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of years that the patient used oxycontin in lifetime.
        /// Question Number: DRG16
        /// </summary>
        public virtual DensAsiNonResponseType<int?> OxyContinInLifetimeYearCount
        {
            get { return _oxyContinInLifetimeYearCount; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiDrugAlcoholAdministrationRoute">OxyContinDensAsiDrugAlcoholAdministrationRoute</see>
        /// denoting the most severe oxyContin, drug or alcohol administration route. Question Number: DRG16
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiDrugAlcoholAdministrationRoute> OxyContinDensAsiDrugAlcoholAdministrationRoute
        {
            get { return _oxyContinDensAsiDrugAlcoholAdministrationRoute; }
            private set { }
        }

        /// <summary>
        /// Gets the oxy contin note.
        /// Question Number: DRG16
        /// </summary>
        public virtual string OxyContinNote
        {
            get { return _oxyContinNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether oxycontin was prescribed for medical reason.
        /// Question Number: D106
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> OxyContinPrescribedForMedicalReasonIndicator
        {
            get { return _oxyContinPrescribedForMedicalReasonIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the oxycontin prescribed for medical reason note.
        /// Question Number: D106
        /// </summary>
        public virtual string OxyContinPrescribedForMedicalReasonIndicatorNote
        {
            get { return _oxyContinPrescribedForMedicalReasonIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating oxycontin use to get high.
        /// Question Number: D107
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> OxyContinUseToGetHighIndicator
        {
            get { return _oxyContinUseToGetHighIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the oxycontin use to get high note.
        /// Question Number: D107
        /// </summary>
        public virtual string OxyContinUseToGetHighIndicatorNote
        {
            get { return _oxyContinUseToGetHighIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating that oxycontin was taken with other opiates.
        /// Question Number: D108
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> OxyContinTakenWithOtherOpiatesIndicator
        {
            get { return _oxyContinTakenWithOtherOpiatesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the oxycontin was taken with other opiates note.
        /// Question Number: D108
        /// </summary>
        public virtual string OxyContinTakenWithOtherOpiatesIndicatorNote
        {
            get { return _oxyContinTakenWithOtherOpiatesIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of months ago the patient first used oxycontin.
        /// Question Number: D109
        /// </summary>
        public virtual DensAsiNonResponseType<int?> AfterOxyContinFirstUseMonthCount
        {
            get { return _afterOxyContinFirstUseMonthCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of months ago the patient first used oxycontin note. 
        /// Question Number: D109
        /// </summary>
        public virtual string AfterOxyContinFirstUseMonthCountNote
        {
            get { return _afterOxyContinFirstUseMonthCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient obtained OxyContin from a friend, family member or on the street.
        /// Question Number: D110
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> OxyContinFromFriendFamilyStreetIndicator
        {
            get { return _oxyContinFromFriendFamilyStreetIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets an indication of whether the patient obtained OxyContin from a friend, family member or on the street note.
        /// Question Number: D110
        /// </summary>
        public virtual string OxyContinFromFriendFamilyStreetIndicatorNote
        {
            get { return _oxyContinFromFriendFamilyStreetIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the drug alcohol section note.
        /// </summary>
        public virtual string SectionNote
        {
            get { return _sectionNote; }
            private set { }
        }


        /// <summary>
        /// Gets the possible DensAsi non response well known names for this interview section.
        /// <remarks>NotAnswered is included in this base class because it is used in most Nonresponse lists.</remarks>
        /// </summary>
        /// <typeparam name="TProperty">The property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see> list of WellKNownNames.
        /// </returns>
        public override IEnumerable<string> GetPossibleDensAsiNonResponseWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            IEnumerable<string> possibleDensAsiNonResponseWellKnownNames;
            string propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => AnyAlcoholDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => HeroinDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => MethadoneDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => OtherOpiatesDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => BarbituratesDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => OtherSedativesDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => CocaineDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => AmphetaminesDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => CannabisDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => HallucinogensDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => InhalantsDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => EndOfProblematicSubstanceAbstinenceMonthCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => DrugDetoxTreatmentOnlyCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => AlcoholDetoxTreatmentOnlyCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => HydromorphoneDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => OxycodoneDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => HydrocodoneDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => BuprenorphineDensAsiDrugAlcoholAdministrationRoute )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => OxyContinDensAsiDrugAlcoholAdministrationRoute )
                )
            {
                possibleDensAsiNonResponseWellKnownNames = new List<string>
                                                               {
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotAnswered,
                                                                   WellKnownNames.DensAsiModule.DensAsiNonResponse.NotApplicable
                                                               };
            }
            else
            {
                possibleDensAsiNonResponseWellKnownNames = base.GetPossibleDensAsiNonResponseWellKnownNames ( propertyExpression );
            }

            return possibleDensAsiNonResponseWellKnownNames;
        }

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public override Dictionary<string, IEnumerable<string>> GetFiltersDictionary ()
        {
            return new Dictionary<string, IEnumerable<string>>
                       {
                           { PropertyUtil.ExtractPropertyName ( () => AnyAlcoholDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => AnyAlcoholDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholIntoxicationDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => HeroinDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HeroinDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => MethadoneDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => MethadoneDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => OtherOpiatesDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherOpiatesDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => BarbituratesDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => BarbituratesDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => OtherSedativesDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OtherSedativesDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => CocaineDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => CocaineDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => AmphetaminesDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => AmphetaminesDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => CannabisDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => CannabisDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => HallucinogensDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HallucinogensDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => InhalantsDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => InhalantsDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => EndOfProblematicSubstanceAbstinenceMonthCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => EndOfProblematicSubstanceAbstinenceMonthCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => DrugDetoxTreatmentOnlyCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => DrugDetoxTreatmentOnlyCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => AlcoholDetoxTreatmentOnlyCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => AlcoholDetoxTreatmentOnlyCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => HydromorphoneInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HydromorphoneInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => HydromorphoneInLifetimeYearCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HydromorphoneInLifetimeYearCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => HydromorphoneDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HydromorphoneDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxycodoneInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxycodoneInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxycodoneInLifetimeYearCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxycodoneInLifetimeYearCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxycodoneDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxycodoneDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => HydrocodoneInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HydrocodoneInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => HydrocodoneInLifetimeYearCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HydrocodoneInLifetimeYearCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => HydrocodoneDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HydrocodoneDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => BuprenorphineInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => BuprenorphineInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => BuprenorphineInLifetimeYearCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => BuprenorphineInLifetimeYearCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => BuprenorphineDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => BuprenorphineDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxyContinInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxyContinInLifetimeYearCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinInLifetimeYearCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxyContinDensAsiDrugAlcoholAdministrationRoute ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinDensAsiDrugAlcoholAdministrationRoute ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxyContinPrescribedForMedicalReasonIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinPrescribedForMedicalReasonIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxyContinUseToGetHighIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinUseToGetHighIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxyContinTakenWithOtherOpiatesIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinTakenWithOtherOpiatesIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => AfterOxyContinFirstUseMonthCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => AfterOxyContinFirstUseMonthCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => OxyContinFromFriendFamilyStreetIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => OxyContinFromFriendFamilyStreetIndicator ) }
                       };
        }
    }
}