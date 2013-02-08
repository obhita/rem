using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiLegalStatusSection contains patient legal status information from the Legal Information section of the DensAsi. 
    /// <remarks>
    /// Included in each of these sections is the interviewer's severity rating, suggesting the client's need for treatment or additional treatment. 
    /// This is based on the information provided by the client. 
    /// </remarks>
    /// </summary>
    [Component]
    public class DensAsiLegalStatusSection : DensAsiInterviewSectionBase
    {
        private readonly DensAsiNonResponseType<bool?> _admissionPromptedByCriminalJusticeSystemIndicator;
        private readonly string _admissionPromptedByCriminalJusticeSystemIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _arrestChargesResultedInConvictionsCount;
        private readonly string _arrestChargesResultedInConvictionsCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedArsonCount;
        private readonly string _arrestedChargedArsonCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedAssaultCount;
        private readonly string _arrestedChargedAssaultCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedBurglaryLarcencyCount;
        private readonly string _arrestedChargedBurglaryLarcencyCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedContemptOfCountCount;
        private readonly string _arrestedChargedContemptOfCountCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedDrugChargesCount;
        private readonly string _arrestedChargedDrugChargesCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedForgeryCount;
        private readonly string _arrestedChargedForgeryCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedHomicideManslaughterCount;
        private readonly string _arrestedChargedHomicideManslaughterCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedOtherCount;
        private readonly string _arrestedChargedOtherDescription;
        private readonly string _arrestedChargedOtherNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedProbationParoleViolationCount;
        private readonly string _arrestedChargedProbationParoleViolationCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedProstitutionCount;
        private readonly string _arrestedChargedProstitutionCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedRapeCount;
        private readonly string _arrestedChargedRapeCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedRobberyCount;
        private readonly string _arrestedChargedRobberyCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedShopliftingCount;
        private readonly string _arrestedChargedShopliftingCountNote;
        private readonly DensAsiNonResponseType<int?> _arrestedChargedWeaponsOffenseCount;
        private readonly string _arrestedChargedWeaponsOffenseCountNote;
        private readonly DensAsiNonResponseType<int?> _chargedWithDisorderlyConductCount;
        private readonly string _chargedWithDisorderlyConductCountNote;
        private readonly DensAsiNonResponseType<int?> _chargedWithDrivingWhileIntoxicatedCount;
        private readonly string _chargedWithDrivingWhileIntoxicatedCountNote;
        private readonly DensAsiNonResponseType<int?> _chargedWithMajorDrivingViolationsCount;
        private readonly string _chargedWithMajorDrivingViolationsCountNote;
        private readonly bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private readonly string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private readonly bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private readonly string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _illegalActivityInLastThirtyDaysDayCount;
        private readonly string _illegalActivityInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfLegalProblemCounselingDensAsiPatientRating;
        private readonly string _importanceOfLegalProblemCounselingDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<int?> _incarceratedInLastThirtyDaysDayCount;
        private readonly string _incarceratedInLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<DensAsiViolationType> _incarcerationForDensAsiViolationType;
        private readonly string _incarcerationForDensAsiViolationTypeNote;
        private readonly DensAsiNonResponseType<int?> _incarcerationInLifeMonthCount;
        private readonly string _incarcerationInLifeMonthCountNote;
        private readonly string _incarcerationLengthMonthCountNote;
        private readonly DensAsiNonResponseType<int?> _lastIncarcerationLengthMonthCount;
        private readonly DensAsiInterviewerRating _patientCounselingDensAsiInterviewerRating;
        private readonly string _patientCounselingDensAsiInterviewerRatingNote;
        private readonly DensAsiNonResponseType<DensAsiViolationType> _presentlyAwaitingChargesForDensAsiViolationType;
        private readonly string _presentlyAwaitingChargesForNote;
        private readonly DensAsiNonResponseType<bool?> _presentlyAwaitingChargesIndicator;
        private readonly string _presentlyAwaitingChargesIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _probationOrParoleIndicator;
        private readonly string _probationOrParoleIndicatorNote;
        private readonly string _sectionNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _seriousnessOfLegalProblemsDensAsiPatientRating;
        private readonly string _seriousnessOfLegalProblemsDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<bool?> _treatmentInsteadOfIncarcerationInPrisonIndicator;
        private readonly string _treatmentInsteadOfIncarcerationInPrisonIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _treatmentMandatoryForCriminalJusticeSystemIndicator;
        private readonly string _treatmentMandatoryForCriminalJusticeSystemIndicatorNote;

        private DensAsiLegalStatusSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiLegalStatusSection"/> class.
        /// </summary>
        /// <param name="admissionPromptedByCriminalJusticeSystemIndicator">The admission prompted by criminal justice system indicator.</param>
        /// <param name="admissionPromptedByCriminalJusticeSystemIndicatorNote">The admission prompted by criminal justice system indicator note.</param>
        /// <param name="probationOrParoleIndicator">The probation or parole indicator.</param>
        /// <param name="probationOrParoleIndicatorNote">The probation or parole indicator note.</param>
        /// <param name="arrestedChargedShopliftingCount">The arrested charged shoplifting count.</param>
        /// <param name="arrestedChargedShopliftingCountNote">The arrested charged shoplifting count note.</param>
        /// <param name="arrestedChargedProbationParoleViolationCount">The arrested charged probation parole violation count.</param>
        /// <param name="arrestedChargedProbationParoleViolationCountNote">The arrested charged probation parole violation count note.</param>
        /// <param name="arrestedChargedDrugChargesCount">The arrested charged drug charges count.</param>
        /// <param name="arrestedChargedDrugChargesCountNote">The arrested charged drug charges count note.</param>
        /// <param name="arrestedChargedForgeryCount">The arrested charged forgery count.</param>
        /// <param name="arrestedChargedForgeryCountNote">The arrested charged forgery count note.</param>
        /// <param name="arrestedChargedWeaponsOffenseCount">The arrested charged weapons offense count.</param>
        /// <param name="arrestedChargedWeaponsOffenseCountNote">The arrested charged weapons offense count note.</param>
        /// <param name="arrestedChargedBurglaryLarcencyCount">The arrested charged burglary larcency count.</param>
        /// <param name="arrestedChargedBurglaryLarcencyCountNote">The arrested charged burglary larcency count note.</param>
        /// <param name="arrestedChargedRobberyCount">The arrested charged robbery count.</param>
        /// <param name="arrestedChargedRobberyCountNote">The arrested charged robbery count note.</param>
        /// <param name="arrestedChargedAssaultCount">The arrested charged assault count.</param>
        /// <param name="arrestedChargedAssaultCountNote">The arrested charged assault count note.</param>
        /// <param name="arrestedChargedArsonCount">The arrested charged arson count.</param>
        /// <param name="arrestedChargedArsonCountNote">The arrested charged arson count note.</param>
        /// <param name="arrestedChargedRapeCount">The arrested charged rape count.</param>
        /// <param name="arrestedChargedRapeCountNote">The arrested charged rape count note.</param>
        /// <param name="arrestedChargedHomicideManslaughterCount">The arrested charged homicide manslaughter count.</param>
        /// <param name="arrestedChargedHomicideManslaughterCountNote">The arrested charged homicide manslaughter count note.</param>
        /// <param name="arrestedChargedProstitutionCount">The arrested charged prostitution count.</param>
        /// <param name="arrestedChargedProstitutionCountNote">The arrested charged prostitution count note.</param>
        /// <param name="arrestedChargedContemptOfCountCount">The arrested charged contempt of count count.</param>
        /// <param name="arrestedChargedContemptOfCountCountNote">The arrested charged contempt of count count note.</param>
        /// <param name="arrestedChargedOtherCount">The arrested charged other count.</param>
        /// <param name="arrestedChargedOtherDescription">The arrested charged other description.</param>
        /// <param name="arrestedChargedOtherNote">The arrested charged other note.</param>
        /// <param name="arrestChargesResultedInConvictionsCount">The arrest charges resulted in convictions count.</param>
        /// <param name="arrestChargesResultedInConvictionsCountNote">The arrest charges resulted in convictions count note.</param>
        /// <param name="chargedWithDisorderlyConductCount">The charged with disorderly conduct count.</param>
        /// <param name="chargedWithDisorderlyConductCountNote">The charged with disorderly conduct count note.</param>
        /// <param name="chargedWithDrivingWhileIntoxicatedCount">The charged with driving while intoxicated count.</param>
        /// <param name="chargedWithDrivingWhileIntoxicatedCountNote">The charged with driving while intoxicated count note.</param>
        /// <param name="chargedWithMajorDrivingViolationsCount">The charged with major driving violations count.</param>
        /// <param name="chargedWithMajorDrivingViolationsCountNote">The charged with major driving violations count note.</param>
        /// <param name="incarcerationInLifeMonthCount">The incarceration in life month count.</param>
        /// <param name="incarcerationInLifeMonthCountNote">The incarceration in life month count note.</param>
        /// <param name="lastIncarcerationLengthMonthCount">The last incarceration length month count.</param>
        /// <param name="incarcerationLengthMonthCountNote">The incarceration length month count note.</param>
        /// <param name="incarcerationForDensAsiViolationType">Type of the incarceration for dens asi violation.</param>
        /// <param name="incarcerationForDensAsiViolationTypeNote">The incarceration for dens asi violation type note.</param>
        /// <param name="presentlyAwaitingChargesIndicator">The presently awaiting charges indicator.</param>
        /// <param name="presentlyAwaitingChargesIndicatorNote">The presently awaiting charges indicator note.</param>
        /// <param name="presentlyAwaitingChargesForDensAsiViolationType">Type of the presently awaiting charges for dens asi violation.</param>
        /// <param name="presentlyAwaitingChargesForNote">The presently awaiting charges for note.</param>
        /// <param name="incarceratedInLastThirtyDaysDayCount">The incarcerated in last thirty days day count.</param>
        /// <param name="incarceratedInLastThirtyDaysDayCountNote">The incarcerated in last thirty days day count note.</param>
        /// <param name="illegalActivityInLastThirtyDaysDayCount">The illegal activity in last thirty days day count.</param>
        /// <param name="illegalActivityInLastThirtyDaysDayCountNote">The illegal activity in last thirty days day count note.</param>
        /// <param name="seriousnessOfLegalProblemsDensAsiPatientRating">The seriousness of legal problems dens asi patient rating.</param>
        /// <param name="seriousnessOfLegalProblemsDensAsiPatientRatingNote">The seriousness of legal problems dens asi patient rating note.</param>
        /// <param name="importanceOfLegalProblemCounselingDensAsiPatientRating">The importance of legal problem counseling dens asi patient rating.</param>
        /// <param name="importanceOfLegalProblemCounselingDensAsiPatientRatingNote">The importance of legal problem counseling dens asi patient rating note.</param>
        /// <param name="patientCounselingDensAsiInterviewerRating">The patient counseling dens asi interviewer rating.</param>
        /// <param name="patientCounselingDensAsiInterviewerRatingNote">The patient counseling dens asi interviewer rating note.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <param name="sectionNote">The section note.</param>
        /// <param name="treatmentMandatoryForCriminalJusticeSystemIndicator">The treatment mandatory for criminal justice system indicator.</param>
        /// <param name="treatmentMandatoryForCriminalJusticeSystemIndicatorNote">The treatment mandatory for criminal justice system indicator note.</param>
        /// <param name="treatmentInsteadOfIncarcerationInPrisonIndicator">The treatment instead of incarceration in prison indicator.</param>
        /// <param name="treatmentInsteadOfIncarcerationInPrisonIndicatorNote">The treatment instead of incarceration in prison indicator note.</param>
        public DensAsiLegalStatusSection(DensAsiNonResponseType<bool?> admissionPromptedByCriminalJusticeSystemIndicator,
                                             string admissionPromptedByCriminalJusticeSystemIndicatorNote,
                                             DensAsiNonResponseType<bool?> probationOrParoleIndicator,
                                             string probationOrParoleIndicatorNote,
                                             DensAsiNonResponseType<int?> arrestedChargedShopliftingCount,
                                             string arrestedChargedShopliftingCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedProbationParoleViolationCount,
                                             string arrestedChargedProbationParoleViolationCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedDrugChargesCount,
                                             string arrestedChargedDrugChargesCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedForgeryCount,
                                             string arrestedChargedForgeryCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedWeaponsOffenseCount,
                                             string arrestedChargedWeaponsOffenseCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedBurglaryLarcencyCount,
                                             string arrestedChargedBurglaryLarcencyCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedRobberyCount,
                                             string arrestedChargedRobberyCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedAssaultCount,
                                             string arrestedChargedAssaultCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedArsonCount,
                                             string arrestedChargedArsonCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedRapeCount,
                                             string arrestedChargedRapeCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedHomicideManslaughterCount,
                                             string arrestedChargedHomicideManslaughterCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedProstitutionCount,
                                             string arrestedChargedProstitutionCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedContemptOfCountCount,
                                             string arrestedChargedContemptOfCountCountNote,
                                             DensAsiNonResponseType<int?> arrestedChargedOtherCount,
                                             string arrestedChargedOtherDescription,
                                             string arrestedChargedOtherNote,
                                             DensAsiNonResponseType<int?> arrestChargesResultedInConvictionsCount,
                                             string arrestChargesResultedInConvictionsCountNote,
                                             DensAsiNonResponseType<int?> chargedWithDisorderlyConductCount,
                                             string chargedWithDisorderlyConductCountNote,
                                             DensAsiNonResponseType<int?> chargedWithDrivingWhileIntoxicatedCount,
                                             string chargedWithDrivingWhileIntoxicatedCountNote,
                                             DensAsiNonResponseType<int?> chargedWithMajorDrivingViolationsCount,
                                             string chargedWithMajorDrivingViolationsCountNote,
                                             DensAsiNonResponseType<int?> incarcerationInLifeMonthCount,
                                             string incarcerationInLifeMonthCountNote,
                                             DensAsiNonResponseType<int?> lastIncarcerationLengthMonthCount,
                                             string incarcerationLengthMonthCountNote,
                                             DensAsiNonResponseType<DensAsiViolationType> incarcerationForDensAsiViolationType,
                                             string incarcerationForDensAsiViolationTypeNote,
                                             DensAsiNonResponseType<bool?> presentlyAwaitingChargesIndicator,
                                             string presentlyAwaitingChargesIndicatorNote,
                                             DensAsiNonResponseType<DensAsiViolationType> presentlyAwaitingChargesForDensAsiViolationType,
                                             string presentlyAwaitingChargesForNote,
                                             DensAsiNonResponseType<int?> incarceratedInLastThirtyDaysDayCount,
                                             string incarceratedInLastThirtyDaysDayCountNote,
                                             DensAsiNonResponseType<int?> illegalActivityInLastThirtyDaysDayCount,
                                             string illegalActivityInLastThirtyDaysDayCountNote,
                                             DensAsiNonResponseType<DensAsiPatientRating> seriousnessOfLegalProblemsDensAsiPatientRating,
                                             string seriousnessOfLegalProblemsDensAsiPatientRatingNote,
                                             DensAsiNonResponseType<DensAsiPatientRating> importanceOfLegalProblemCounselingDensAsiPatientRating,
                                             string importanceOfLegalProblemCounselingDensAsiPatientRatingNote,
                                             DensAsiInterviewerRating patientCounselingDensAsiInterviewerRating,
                                             string patientCounselingDensAsiInterviewerRatingNote,
                                             bool? confidenceDistortedByPatientMisrepresentationIndicator,
                                             string confidenceDistortedByPatientMisrepresentationIndicatorNote,
                                             bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                                             string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                                             string sectionNote,
                                             DensAsiNonResponseType<bool?> treatmentMandatoryForCriminalJusticeSystemIndicator,
                                             string treatmentMandatoryForCriminalJusticeSystemIndicatorNote,
                                             DensAsiNonResponseType<bool?> treatmentInsteadOfIncarcerationInPrisonIndicator,
                                             string treatmentInsteadOfIncarcerationInPrisonIndicatorNote )
        {
            if ( admissionPromptedByCriminalJusticeSystemIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AdmissionPromptedByCriminalJusticeSystemIndicator ).Contains ( admissionPromptedByCriminalJusticeSystemIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AdmissionPromptedByCriminalJusticeSystemIndicator DensAsiNonResponse value '" + admissionPromptedByCriminalJusticeSystemIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( probationOrParoleIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ProbationOrParoleIndicator ).Contains ( probationOrParoleIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ProbationOrParoleIndicator DensAsiNonResponse value '" + probationOrParoleIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedShopliftingCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedShopliftingCount ).Contains ( arrestedChargedShopliftingCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedShopliftingCount DensAsiNonResponse value '" + arrestedChargedShopliftingCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedProbationParoleViolationCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedProbationParoleViolationCount ).Contains ( arrestedChargedProbationParoleViolationCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedProbationParoleViolationCount DensAsiNonResponse value '" + arrestedChargedProbationParoleViolationCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedDrugChargesCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedDrugChargesCount ).Contains ( arrestedChargedDrugChargesCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedDrugChargesCount DensAsiNonResponse value '" + arrestedChargedDrugChargesCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedForgeryCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedForgeryCount ).Contains ( arrestedChargedForgeryCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedForgeryCount DensAsiNonResponse value '" + arrestedChargedForgeryCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedWeaponsOffenseCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedWeaponsOffenseCount ).Contains ( arrestedChargedWeaponsOffenseCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedWeaponsOffenseCount DensAsiNonResponse value '" + arrestedChargedWeaponsOffenseCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedBurglaryLarcencyCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedBurglaryLarcencyCount ).Contains ( arrestedChargedBurglaryLarcencyCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedBurglaryLarcencyCount DensAsiNonResponse value '" + arrestedChargedBurglaryLarcencyCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedRobberyCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedRobberyCount ).Contains ( arrestedChargedRobberyCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedRobberyCount DensAsiNonResponse value '" + arrestedChargedRobberyCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedAssaultCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedAssaultCount ).Contains ( arrestedChargedAssaultCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedAssaultCount DensAsiNonResponse value '" + arrestedChargedAssaultCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedArsonCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedArsonCount ).Contains ( arrestedChargedArsonCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedArsonCount DensAsiNonResponse value '" + arrestedChargedArsonCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedRapeCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedRapeCount ).Contains ( arrestedChargedRapeCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedRapeCount DensAsiNonResponse value '" + arrestedChargedRapeCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedHomicideManslaughterCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedHomicideManslaughterCount ).Contains ( arrestedChargedHomicideManslaughterCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedHomicideManslaughterCount DensAsiNonResponse value '" + arrestedChargedHomicideManslaughterCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedProstitutionCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedProstitutionCount ).Contains ( arrestedChargedProstitutionCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedProstitutionCount DensAsiNonResponse value '" + arrestedChargedProstitutionCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedContemptOfCountCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedContemptOfCountCount ).Contains ( arrestedChargedContemptOfCountCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedContemptOfCountCount DensAsiNonResponse value '" + arrestedChargedContemptOfCountCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestedChargedOtherCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestedChargedOtherCount ).Contains ( arrestedChargedOtherCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestedChargedOtherCount DensAsiNonResponse value '" + arrestedChargedOtherCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( arrestChargesResultedInConvictionsCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestChargesResultedInConvictionsCount ).Contains ( arrestChargesResultedInConvictionsCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ArrestChargesResultedInConvictionsCount DensAsiNonResponse value '" + arrestChargesResultedInConvictionsCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( chargedWithDisorderlyConductCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ChargedWithDisorderlyConductCount ).Contains ( chargedWithDisorderlyConductCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ChargedWithDisorderlyConductCount DensAsiNonResponse value '" + chargedWithDisorderlyConductCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( chargedWithDrivingWhileIntoxicatedCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ChargedWithDrivingWhileIntoxicatedCount ).Contains ( chargedWithDrivingWhileIntoxicatedCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ChargedWithDrivingWhileIntoxicatedCount DensAsiNonResponse value '" + chargedWithDrivingWhileIntoxicatedCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( chargedWithMajorDrivingViolationsCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ChargedWithMajorDrivingViolationsCount ).Contains ( chargedWithMajorDrivingViolationsCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ChargedWithMajorDrivingViolationsCount DensAsiNonResponse value '" + chargedWithMajorDrivingViolationsCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( incarcerationInLifeMonthCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => IncarcerationInLifeMonthCount ).Contains ( incarcerationInLifeMonthCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "IncarcerationInLifeMonthCount DensAsiNonResponse value '" + incarcerationInLifeMonthCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( lastIncarcerationLengthMonthCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => LastIncarcerationLengthMonthCount ).Contains ( lastIncarcerationLengthMonthCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "LastIncarcerationLengthMonthCount DensAsiNonResponse value '" + lastIncarcerationLengthMonthCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( incarcerationForDensAsiViolationType.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => IncarcerationForDensAsiViolationType ).Contains ( incarcerationForDensAsiViolationType.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "IncarcerationForDensAsiViolationType DensAsiNonResponse value '" + incarcerationForDensAsiViolationType.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( presentlyAwaitingChargesIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PresentlyAwaitingChargesIndicator ).Contains ( presentlyAwaitingChargesIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PresentlyAwaitingChargesIndicator DensAsiNonResponse value '" + presentlyAwaitingChargesIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( presentlyAwaitingChargesForDensAsiViolationType.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PresentlyAwaitingChargesForDensAsiViolationType ).Contains ( presentlyAwaitingChargesForDensAsiViolationType.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PresentlyAwaitingChargesForDensAsiViolationType DensAsiNonResponse value '" + presentlyAwaitingChargesForDensAsiViolationType.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( incarceratedInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => IncarceratedInLastThirtyDaysDayCount ).Contains ( incarceratedInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "IncarceratedInLastThirtyDaysDayCount DensAsiNonResponse value '" + incarceratedInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( illegalActivityInLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => IllegalActivityInLastThirtyDaysDayCount ).Contains ( illegalActivityInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "IllegalActivityInLastThirtyDaysDayCount DensAsiNonResponse value '" + illegalActivityInLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( seriousnessOfLegalProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => SeriousnessOfLegalProblemsDensAsiPatientRating ).Contains ( seriousnessOfLegalProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "SeriousnessOfLegalProblemsDensAsiPatientRating DensAsiNonResponse value '" + seriousnessOfLegalProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfLegalProblemCounselingDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfLegalProblemCounselingDensAsiPatientRating ).Contains ( importanceOfLegalProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfLegalProblemCounselingDensAsiPatientRating DensAsiNonResponse value '" + importanceOfLegalProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( treatmentMandatoryForCriminalJusticeSystemIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TreatmentMandatoryForCriminalJusticeSystemIndicator ).Contains ( treatmentMandatoryForCriminalJusticeSystemIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TreatmentMandatoryForCriminalJusticeSystemIndicator DensAsiNonResponse value '" + treatmentMandatoryForCriminalJusticeSystemIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( treatmentInsteadOfIncarcerationInPrisonIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TreatmentInsteadOfIncarcerationInPrisonIndicator ).Contains ( treatmentInsteadOfIncarcerationInPrisonIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TreatmentInsteadOfIncarcerationInPrisonIndicator DensAsiNonResponse value '" + treatmentInsteadOfIncarcerationInPrisonIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _admissionPromptedByCriminalJusticeSystemIndicator = admissionPromptedByCriminalJusticeSystemIndicator;
            _admissionPromptedByCriminalJusticeSystemIndicatorNote = admissionPromptedByCriminalJusticeSystemIndicatorNote;
            _probationOrParoleIndicator = probationOrParoleIndicator;
            _probationOrParoleIndicatorNote = probationOrParoleIndicatorNote;
            _arrestedChargedShopliftingCount = arrestedChargedShopliftingCount;
            _arrestedChargedShopliftingCountNote = arrestedChargedShopliftingCountNote;
            _arrestedChargedProbationParoleViolationCount = arrestedChargedProbationParoleViolationCount;
            _arrestedChargedProbationParoleViolationCountNote = arrestedChargedProbationParoleViolationCountNote;
            _arrestedChargedDrugChargesCount = arrestedChargedDrugChargesCount;
            _arrestedChargedDrugChargesCountNote = arrestedChargedDrugChargesCountNote;
            _arrestedChargedForgeryCount = arrestedChargedForgeryCount;
            _arrestedChargedForgeryCountNote = arrestedChargedForgeryCountNote;
            _arrestedChargedWeaponsOffenseCount = arrestedChargedWeaponsOffenseCount;
            _arrestedChargedWeaponsOffenseCountNote = arrestedChargedWeaponsOffenseCountNote;
            _arrestedChargedBurglaryLarcencyCount = arrestedChargedBurglaryLarcencyCount;
            _arrestedChargedBurglaryLarcencyCountNote = arrestedChargedBurglaryLarcencyCountNote;
            _arrestedChargedRobberyCount = arrestedChargedRobberyCount;
            _arrestedChargedRobberyCountNote = arrestedChargedRobberyCountNote;
            _arrestedChargedAssaultCount = arrestedChargedAssaultCount;
            _arrestedChargedAssaultCountNote = arrestedChargedAssaultCountNote;
            _arrestedChargedArsonCount = arrestedChargedArsonCount;
            _arrestedChargedArsonCountNote = arrestedChargedArsonCountNote;
            _arrestedChargedRapeCount = arrestedChargedRapeCount;
            _arrestedChargedRapeCountNote = arrestedChargedRapeCountNote;
            _arrestedChargedHomicideManslaughterCount = arrestedChargedHomicideManslaughterCount;
            _arrestedChargedHomicideManslaughterCountNote = arrestedChargedHomicideManslaughterCountNote;
            _arrestedChargedProstitutionCount = arrestedChargedProstitutionCount;
            _arrestedChargedProstitutionCountNote = arrestedChargedProstitutionCountNote;
            _arrestedChargedContemptOfCountCount = arrestedChargedContemptOfCountCount;
            _arrestedChargedContemptOfCountCountNote = arrestedChargedContemptOfCountCountNote;
            _arrestedChargedOtherCount = arrestedChargedOtherCount;
            _arrestedChargedOtherDescription = arrestedChargedOtherDescription;
            _arrestedChargedOtherNote = arrestedChargedOtherNote;
            _arrestChargesResultedInConvictionsCount = arrestChargesResultedInConvictionsCount;
            _arrestChargesResultedInConvictionsCountNote = arrestChargesResultedInConvictionsCountNote;
            _chargedWithDisorderlyConductCount = chargedWithDisorderlyConductCount;
            _chargedWithDisorderlyConductCountNote = chargedWithDisorderlyConductCountNote;
            _chargedWithDrivingWhileIntoxicatedCount = chargedWithDrivingWhileIntoxicatedCount;
            _chargedWithDrivingWhileIntoxicatedCountNote = chargedWithDrivingWhileIntoxicatedCountNote;
            _chargedWithMajorDrivingViolationsCount = chargedWithMajorDrivingViolationsCount;
            _chargedWithMajorDrivingViolationsCountNote = chargedWithMajorDrivingViolationsCountNote;
            _incarcerationInLifeMonthCount = incarcerationInLifeMonthCount;
            _incarcerationInLifeMonthCountNote = incarcerationInLifeMonthCountNote;
            _lastIncarcerationLengthMonthCount = lastIncarcerationLengthMonthCount;
            _incarcerationLengthMonthCountNote = incarcerationLengthMonthCountNote;
            _incarcerationForDensAsiViolationType = incarcerationForDensAsiViolationType;
            _incarcerationForDensAsiViolationTypeNote = incarcerationForDensAsiViolationTypeNote;
            _presentlyAwaitingChargesIndicator = presentlyAwaitingChargesIndicator;
            _presentlyAwaitingChargesIndicatorNote = presentlyAwaitingChargesIndicatorNote;
            _presentlyAwaitingChargesForDensAsiViolationType = presentlyAwaitingChargesForDensAsiViolationType;
            _presentlyAwaitingChargesForNote = presentlyAwaitingChargesForNote;
            _incarceratedInLastThirtyDaysDayCount = incarceratedInLastThirtyDaysDayCount;
            _incarceratedInLastThirtyDaysDayCountNote = incarceratedInLastThirtyDaysDayCountNote;
            _illegalActivityInLastThirtyDaysDayCount = illegalActivityInLastThirtyDaysDayCount;
            _illegalActivityInLastThirtyDaysDayCountNote = illegalActivityInLastThirtyDaysDayCountNote;
            _seriousnessOfLegalProblemsDensAsiPatientRating = seriousnessOfLegalProblemsDensAsiPatientRating;
            _seriousnessOfLegalProblemsDensAsiPatientRatingNote = seriousnessOfLegalProblemsDensAsiPatientRatingNote;
            _importanceOfLegalProblemCounselingDensAsiPatientRating = importanceOfLegalProblemCounselingDensAsiPatientRating;
            _importanceOfLegalProblemCounselingDensAsiPatientRatingNote = importanceOfLegalProblemCounselingDensAsiPatientRatingNote;
            _patientCounselingDensAsiInterviewerRating = patientCounselingDensAsiInterviewerRating;
            _patientCounselingDensAsiInterviewerRatingNote = patientCounselingDensAsiInterviewerRatingNote;
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            _sectionNote = sectionNote;
            _treatmentMandatoryForCriminalJusticeSystemIndicator = treatmentMandatoryForCriminalJusticeSystemIndicator;
            _treatmentMandatoryForCriminalJusticeSystemIndicatorNote = treatmentMandatoryForCriminalJusticeSystemIndicatorNote;
            _treatmentInsteadOfIncarcerationInPrisonIndicator = treatmentInsteadOfIncarcerationInPrisonIndicator;
            _treatmentInsteadOfIncarcerationInPrisonIndicatorNote = treatmentInsteadOfIncarcerationInPrisonIndicatorNote;
        }

        /// <summary>
        /// Gets a boolean value indicating whether admission was prompted by criminal justice system.
        /// Question Number: L1
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AdmissionPromptedByCriminalJusticeSystemIndicator
        {
            get { return _admissionPromptedByCriminalJusticeSystemIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether admission was prompted by criminal justice system note.
        /// Question Number: L1
        /// </summary>
        public virtual string AdmissionPromptedByCriminalJusticeSystemIndicatorNote
        {
            get { return _admissionPromptedByCriminalJusticeSystemIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating probation or parole.
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ProbationOrParoleIndicator
        {
            get { return _probationOrParoleIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating probation or parole note.
        /// Question Number: L2
        /// </summary>
        public virtual string ProbationOrParoleIndicatorNote
        {
            get { return _probationOrParoleIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the the number of patient arrests and charges for shoplifting.
        /// Question Number: L3
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedShopliftingCount
        {
            get { return _arrestedChargedShopliftingCount; }
            private set { }
        }

        /// <summary>
        /// Gets the the number of patient arrests and charges for shoplifting note.
        /// Question Number: L3
        /// </summary>
        public virtual string ArrestedChargedShopliftingCountNote
        {
            get { return _arrestedChargedShopliftingCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of patient arrests for probation and parole violations.
        /// Question Number: L4
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedProbationParoleViolationCount
        {
            get { return _arrestedChargedProbationParoleViolationCount; }
            private set { }
        }

        /// <summary>
        /// Question Number: L4
        /// Gets the number of patient arrests for probation and parole violations note.
        /// </summary>
        public virtual string ArrestedChargedProbationParoleViolationCountNote
        {
            get { return _arrestedChargedProbationParoleViolationCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on drug charges.
        /// Question Number: L5
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedDrugChargesCount
        {
            get { return _arrestedChargedDrugChargesCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on drug charges note.
        /// Question Number: L5
        /// </summary>
        public virtual string ArrestedChargedDrugChargesCountNote
        {
            get { return _arrestedChargedDrugChargesCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on forgery charges.
        /// Question Number: L6
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedForgeryCount
        {
            get { return _arrestedChargedForgeryCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on forgery charges note.
        /// Question Number: L6
        /// </summary>
        public virtual string ArrestedChargedForgeryCountNote
        {
            get { return _arrestedChargedForgeryCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on weapons charges.
        /// Question Number: L7
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedWeaponsOffenseCount
        {
            get { return _arrestedChargedWeaponsOffenseCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on weapons charges note.
        /// Question Number: L7
        /// </summary>
        public virtual string ArrestedChargedWeaponsOffenseCountNote
        {
            get { return _arrestedChargedWeaponsOffenseCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on burglary or larcency charges.
        /// Question Number: L8
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedBurglaryLarcencyCount
        {
            get { return _arrestedChargedBurglaryLarcencyCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on burglary or larcency charges note.
        /// Question Number: L8
        /// </summary>
        public virtual string ArrestedChargedBurglaryLarcencyCountNote
        {
            get { return _arrestedChargedBurglaryLarcencyCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on robbery charges.
        /// Question Number: L9
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedRobberyCount
        {
            get { return _arrestedChargedRobberyCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on robbery charges note.
        /// Question Number: L9
        /// </summary>
        public virtual string ArrestedChargedRobberyCountNote
        {
            get { return _arrestedChargedRobberyCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on assault charges.
        /// Question Number: L10
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedAssaultCount
        {
            get { return _arrestedChargedAssaultCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on assault charges note.
        /// Question Number: L10
        /// </summary>
        public virtual string ArrestedChargedAssaultCountNote
        {
            get { return _arrestedChargedAssaultCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on arson charges.
        /// Question Number: L11
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedArsonCount
        {
            get { return _arrestedChargedArsonCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on arson charges note.
        /// Question Number: L11
        /// </summary>
        public virtual string ArrestedChargedArsonCountNote
        {
            get { return _arrestedChargedArsonCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on rape charges.
        /// Question Number: L12
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedRapeCount
        {
            get { return _arrestedChargedRapeCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on rape charges note.
        /// Question Number: L12
        /// </summary>
        public virtual string ArrestedChargedRapeCountNote
        {
            get { return _arrestedChargedRapeCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on homicide or manslaughter charges.
        /// Question Number: L13
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedHomicideManslaughterCount
        {
            get { return _arrestedChargedHomicideManslaughterCount; }
            private set { }
        }

        /// <summary>
        /// Question Number: L13
        /// Gets the number of arrests on homicide or manslaughter charges note.
        /// </summary>
        public virtual string ArrestedChargedHomicideManslaughterCountNote
        {
            get { return _arrestedChargedHomicideManslaughterCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on prostitution charges.
        /// Question Number: L14
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedProstitutionCount
        {
            get { return _arrestedChargedProstitutionCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on prostitution charges note.
        /// Question Number: L14
        /// </summary>
        public virtual string ArrestedChargedProstitutionCountNote
        {
            get { return _arrestedChargedProstitutionCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on contempt of court charges.
        /// Question Number: L15
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedContemptOfCountCount
        {
            get { return _arrestedChargedContemptOfCountCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on contempt of court charges note.
        /// Question Number: L15
        /// </summary>
        public virtual string ArrestedChargedContemptOfCountCountNote
        {
            get { return _arrestedChargedContemptOfCountCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on other charges.
        /// Question Number: L16
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestedChargedOtherCount
        {
            get { return _arrestedChargedOtherCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on other charges description.
        /// Question Number: L16
        /// </summary>
        public virtual string ArrestedChargedOtherDescription
        {
            get { return _arrestedChargedOtherDescription; }
            private set { }
        }

        /// <summary>
        /// Gets the number of arrests on other charges note.
        /// Question Number: L16
        /// </summary>
        public virtual string ArrestedChargedOtherNote
        {
            get { return _arrestedChargedOtherNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges resulting in convictions.
        /// Question Number: L17
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ArrestChargesResultedInConvictionsCount
        {
            get { return _arrestChargesResultedInConvictionsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges resulting in convictions note.
        /// Question Number: L17
        /// </summary>
        public virtual string ArrestChargesResultedInConvictionsCountNote
        {
            get { return _arrestChargesResultedInConvictionsCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges for disorderly conduct.
        /// Question Number: L18
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ChargedWithDisorderlyConductCount
        {
            get { return _chargedWithDisorderlyConductCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges for disorderly conduct note.
        /// Question Number: L18
        /// </summary>
        public virtual string ChargedWithDisorderlyConductCountNote
        {
            get { return _chargedWithDisorderlyConductCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges for driving while intoxicated.
        /// Question Number: L19
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ChargedWithDrivingWhileIntoxicatedCount
        {
            get { return _chargedWithDrivingWhileIntoxicatedCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges for driving while intoxicated note.
        /// Question Number: L19
        /// </summary>
        public virtual string ChargedWithDrivingWhileIntoxicatedCountNote
        {
            get { return _chargedWithDrivingWhileIntoxicatedCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges for major driving violations.
        /// Question Number: L20
        /// </summary>
        public virtual DensAsiNonResponseType<int?> ChargedWithMajorDrivingViolationsCount
        {
            get { return _chargedWithMajorDrivingViolationsCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of charges for major driving violations note.
        /// Question Number: L20
        /// </summary>
        public virtual string ChargedWithMajorDrivingViolationsCountNote
        {
            get { return _chargedWithMajorDrivingViolationsCountNote; }
            private set { }
        }


        /// <summary>
        /// Gets the months in life month incarcerated.
        /// Question Number: L21
        /// </summary>
        public virtual DensAsiNonResponseType<int?> IncarcerationInLifeMonthCount
        {
            get { return _incarcerationInLifeMonthCount; }
            private set { }
        }

        /// <summary>
        /// Gets the months in life month incarcerated note.
        /// Question Number: L21
        /// </summary>
        public virtual string IncarcerationInLifeMonthCountNote
        {
            get { return _incarcerationInLifeMonthCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the months of last incarceration.
        /// Question Number: L22
        /// </summary>
        public virtual DensAsiNonResponseType<int?> LastIncarcerationLengthMonthCount
        {
            get { return _lastIncarcerationLengthMonthCount; }
            private set { }
        }

        /// <summary>
        /// Gets the months of last incarceration note.
        /// Question Number: L22
        /// </summary>
        public virtual string IncarcerationLengthMonthCountNote
        {
            get { return _incarcerationLengthMonthCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiViolationType">IncarcerationForDensAsiViolationType</see>  
        /// denoting the offense that the patient was incarcerated for.
        /// Question Number: L23
        /// </summary>      
        public virtual DensAsiNonResponseType<DensAsiViolationType> IncarcerationForDensAsiViolationType
        {
            get { return _incarcerationForDensAsiViolationType; }
            private set { }
        }

        /// <summary>
        /// Gets the offense that the patient was incarcerated for.
        /// Question Number: L23
        /// </summary>
        public virtual string IncarcerationForDensAsiViolationTypeNote
        {
            get { return _incarcerationForDensAsiViolationTypeNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient is presently awaiting charges.
        /// Question Number: L24
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PresentlyAwaitingChargesIndicator
        {
            get { return _presentlyAwaitingChargesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether the patient is presently awaiting charges note.
        /// Question Number: L24
        /// </summary>
        public virtual string PresentlyAwaitingChargesIndicatorNote
        {
            get { return _presentlyAwaitingChargesIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiViolationType">PresentlyAwaitingChargesForDensAsiViolationType</see>  
        /// denoting the offense that the patient is presently awaiting charges for.
        /// Question Number: L25
        /// </summary>  
        public virtual DensAsiNonResponseType<DensAsiViolationType> PresentlyAwaitingChargesForDensAsiViolationType
        {
            get { return _presentlyAwaitingChargesForDensAsiViolationType; }
            private set { }
        }

        /// <summary>
        /// Gets the offense that the patient is presently awaiting charges for note.
        /// Question Number: L25
        /// </summary>
        public virtual string PresentlyAwaitingChargesForNote
        {
            get { return _presentlyAwaitingChargesForNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty that the patient was incarcerated.
        /// Question Number: L26
        /// </summary>
        public virtual DensAsiNonResponseType<int?> IncarceratedInLastThirtyDaysDayCount
        {
            get { return _incarceratedInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty that the patient was incarcerated note.
        /// Question Number: L26
        /// </summary>
        public virtual string IncarceratedInLastThirtyDaysDayCountNote
        {
            get { return _incarceratedInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty that the patient engaged in illegal activity.
        /// Question Number: L27
        /// </summary>
        public virtual DensAsiNonResponseType<int?> IllegalActivityInLastThirtyDaysDayCount
        {
            get { return _illegalActivityInLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in the last thirty that the patient engaged in illegal activity note.
        /// Question Number: L27
        /// </summary>
        public virtual string IllegalActivityInLastThirtyDaysDayCountNote
        {
            get { return _illegalActivityInLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">SeriousnessOfLegalProblemsDensAsiPatientRating</see>  
        /// denoting the patient severity rating of the legal problem.
        /// Question Number: L28
        /// </summary>  
        public virtual DensAsiNonResponseType<DensAsiPatientRating> SeriousnessOfLegalProblemsDensAsiPatientRating
        {
            get { return _seriousnessOfLegalProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the patient severity rating of the legal problem note.
        /// Question Number: L28
        /// </summary>
        public virtual string SeriousnessOfLegalProblemsDensAsiPatientRatingNote
        {
            get { return _seriousnessOfLegalProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">ImportanceOfLegalProblemCounselingDensAsiPatientRating</see>  
        /// denoting the patient importance rating of the legal problem.
        /// Question Number: L29
        /// </summary>   
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfLegalProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfLegalProblemCounselingDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the patient importance rating of the legal problem note.
        /// Question Number: L29
        /// </summary>
        public virtual string ImportanceOfLegalProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfLegalProblemCounselingDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">PatientCounselingDensAsiInterviewerRating</see>
        /// denoting the interviewer's importance rating for patient legal services. Question Number: L30
        /// </summary>
        public virtual DensAsiInterviewerRating PatientCounselingDensAsiInterviewerRating
        {
            get { return _patientCounselingDensAsiInterviewerRating; }
            private set { }
        }

        /// <summary>
        /// Gets the interviewer's importance rating for patient legal services note.
        /// Question Number: L30
        /// </summary>
        public virtual string PatientCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientCounselingDensAsiInterviewerRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation.
        /// Question Number: L31
        /// </summary>
        public virtual bool? ConfidenceDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation note.
        /// Question Number: L31
        /// </summary>
        public virtual string ConfidenceDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand.
        /// Question Number: L32
        /// </summary>
        public virtual bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand note.
        /// Question Number: L32
        /// </summary>
        public virtual string ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote; }
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
        /// Gets a boolean value indicating that treatment is mandatory condition for the criminal justice system.
        /// Question Number: L102
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> TreatmentMandatoryForCriminalJusticeSystemIndicator
        {
            get { return _treatmentMandatoryForCriminalJusticeSystemIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating that treatment is mandatory condition for the criminal justice system note.
        /// Question Number: L102
        /// </summary>
        public virtual string TreatmentMandatoryForCriminalJusticeSystemIndicatorNote
        {
            get { return _treatmentMandatoryForCriminalJusticeSystemIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating that the patient is in treatment instead of incarceration in a jail or prison.
        /// Question Number: L103
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> TreatmentInsteadOfIncarcerationInPrisonIndicator
        {
            get { return _treatmentInsteadOfIncarcerationInPrisonIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating that the patient is in treatment instead of incarceration in a jail or prison note.
        /// Question Number: L103
        /// </summary>
        public virtual string TreatmentInsteadOfIncarcerationInPrisonIndicatorNote
        {
            get { return _treatmentInsteadOfIncarcerationInPrisonIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the possible dens asi non response well known names.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see>.
        /// </returns>
        public override IEnumerable<string> GetPossibleDensAsiNonResponseWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            IEnumerable<string> possibleDensAsiNonResponseWellKnownNames;
            string propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => ArrestChargesResultedInConvictionsCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => LastIncarcerationLengthMonthCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => TreatmentMandatoryForCriminalJusticeSystemIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => TreatmentInsteadOfIncarcerationInPrisonIndicator )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => IncarcerationForDensAsiViolationType )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => PresentlyAwaitingChargesForDensAsiViolationType )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => IncarceratedInLastThirtyDaysDayCount )
                 || propertyName == PropertyUtil.ExtractPropertyName ( () => IllegalActivityInLastThirtyDaysDayCount ) )
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
                           { PropertyUtil.ExtractPropertyName ( () => ArrestChargesResultedInConvictionsCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => ArrestChargesResultedInConvictionsCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => LastIncarcerationLengthMonthCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => LastIncarcerationLengthMonthCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => TreatmentMandatoryForCriminalJusticeSystemIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => TreatmentMandatoryForCriminalJusticeSystemIndicator ) },
                           { PropertyUtil.ExtractPropertyName ( () => TreatmentInsteadOfIncarcerationInPrisonIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => TreatmentInsteadOfIncarcerationInPrisonIndicator ) },
                           
                           { PropertyUtil.ExtractPropertyName ( () => IncarcerationForDensAsiViolationType ), GetPossibleDensAsiNonResponseWellKnownNames ( () => IncarcerationForDensAsiViolationType ) },
                           { PropertyUtil.ExtractPropertyName ( () => PresentlyAwaitingChargesForDensAsiViolationType ), GetPossibleDensAsiNonResponseWellKnownNames ( () => PresentlyAwaitingChargesForDensAsiViolationType ) },
                           { PropertyUtil.ExtractPropertyName ( () => IncarceratedInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => IncarceratedInLastThirtyDaysDayCount ) },
                           { PropertyUtil.ExtractPropertyName ( () => IllegalActivityInLastThirtyDaysDayCount ), GetPossibleDensAsiNonResponseWellKnownNames ( () => IllegalActivityInLastThirtyDaysDayCount ) }
                       };
        }
    }
}