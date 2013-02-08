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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiLegalStatus class.
    /// </summary>
    public class DensAsiLegalStatusDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DensAsiNonResponseTypeDto<bool?> _admissionPromptedByCriminalJusticeSystemIndicator;
        private string _admissionPromptedByCriminalJusticeSystemIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _arrestChargesResultedInConvictionsCount;
        private string _arrestChargesResultedInConvictionsCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedArsonCount;
        private string _arrestedChargedArsonCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedAssaultCount;
        private string _arrestedChargedAssaultCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedBurglaryLarcencyCount;
        private string _arrestedChargedBurglaryLarcencyCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedContemptOfCountCount;
        private string _arrestedChargedContemptOfCountCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedDrugChargesCount;
        private string _arrestedChargedDrugChargesCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedForgeryCount;
        private string _arrestedChargedForgeryCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedHomicideManslaughterCount;
        private string _arrestedChargedHomicideManslaughterCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedOtherCount;
        private string _arrestedChargedOtherDescription;
        private string _arrestedChargedOtherNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedProbationParoleViolationCount;
        private string _arrestedChargedProbationParoleViolationCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedProstitutionCount;
        private string _arrestedChargedProstitutionCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedRapeCount;
        private string _arrestedChargedRapeCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedRobberyCount;
        private string _arrestedChargedRobberyCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedShopliftingCount;
        private string _arrestedChargedShopliftingCountNote;
        private DensAsiNonResponseTypeDto<int?> _arrestedChargedWeaponsOffenseCount;
        private string _arrestedChargedWeaponsOffenseCountNote;
        private DensAsiNonResponseTypeDto<int?> _chargedWithDisorderlyConductCount;
        private string _chargedWithDisorderlyConductCountNote;
        private DensAsiNonResponseTypeDto<int?> _chargedWithDrivingWhileIntoxicatedCount;
        private string _chargedWithDrivingWhileIntoxicatedCountNote;
        private DensAsiNonResponseTypeDto<int?> _chargedWithMajorDrivingViolationsCount;
        private string _chargedWithMajorDrivingViolationsCountNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _illegalActivityInLastThirtyDaysDayCount;
        private string _illegalActivityInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfLegalProblemCounselingDensAsiPatientRating;
        private string _importanceOfLegalProblemCounselingDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<int?> _incarceratedInLastThirtyDaysDayCount;
        private string _incarceratedInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _incarcerationForDensAsiViolationType;
        private string _incarcerationForDensAsiViolationTypeNote;
        private DensAsiNonResponseTypeDto<int?> _incarcerationInLifeMonthCount;
        private string _incarcerationInLifeMonthCountNote;
        private string _incarcerationLengthMonthCountNote;
        private DensAsiNonResponseTypeDto<int?> _lastIncarcerationLengthMonthCount;
        private LookupValueDto _patientCounselingDensAsiInterviewerRating;
        private string _patientCounselingDensAsiInterviewerRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _presentlyAwaitingChargesForDensAsiViolationType;
        private string _presentlyAwaitingChargesForNote;
        private DensAsiNonResponseTypeDto<bool?> _presentlyAwaitingChargesIndicator;
        private string _presentlyAwaitingChargesIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _probationOrParoleIndicator;
        private string _probationOrParoleIndicatorNote;
        private string _sectionNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _seriousnessOfLegalProblemsDensAsiPatientRating;
        private string _seriousnessOfLegalProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<bool?> _treatmentInsteadOfIncarcerationInPrisonIndicator;
        private string _treatmentInsteadOfIncarcerationInPrisonIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _treatmentMandatoryForCriminalJusticeSystemIndicator;
        private string _treatmentMandatoryForCriminalJusticeSystemIndicatorNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: L1
        /// </summary>
        /// <value>The admission prompted by criminal justice system indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AdmissionPromptedByCriminalJusticeSystemIndicator
        {
            get { return _admissionPromptedByCriminalJusticeSystemIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _admissionPromptedByCriminalJusticeSystemIndicator, () => AdmissionPromptedByCriminalJusticeSystemIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: L1
        /// </summary>
        /// <value>The admission prompted by criminal justice system indicator note.</value>
        public string AdmissionPromptedByCriminalJusticeSystemIndicatorNote
        {
            get { return _admissionPromptedByCriminalJusticeSystemIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _admissionPromptedByCriminalJusticeSystemIndicatorNote, () => AdmissionPromptedByCriminalJusticeSystemIndicatorNote, value );
            }
        }

        /// <summary>
        /// Question Number: L17
        /// </summary>
        /// <value>The arrest charges resulted in convictions count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestChargesResultedInConvictionsCount
        {
            get { return _arrestChargesResultedInConvictionsCount; }
            set { ApplyPropertyChange ( ref _arrestChargesResultedInConvictionsCount, () => ArrestChargesResultedInConvictionsCount, value ); }
        }

        /// <summary>
        /// Question Number: L17
        /// </summary>
        /// <value>The arrest charges resulted in convictions count note.</value>
        public string ArrestChargesResultedInConvictionsCountNote
        {
            get { return _arrestChargesResultedInConvictionsCountNote; }
            set { ApplyPropertyChange ( ref _arrestChargesResultedInConvictionsCountNote, () => ArrestChargesResultedInConvictionsCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L11
        /// </summary>
        /// <value>The arrested charged arson count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedArsonCount
        {
            get { return _arrestedChargedArsonCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedArsonCount, () => ArrestedChargedArsonCount, value ); }
        }

        /// <summary>
        /// Question Number: L11
        /// </summary>
        /// <value>The arrested charged arson count note.</value>
        public string ArrestedChargedArsonCountNote
        {
            get { return _arrestedChargedArsonCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedArsonCountNote, () => ArrestedChargedArsonCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L10
        /// </summary>
        /// <value>The arrested charged assault count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedAssaultCount
        {
            get { return _arrestedChargedAssaultCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedAssaultCount, () => ArrestedChargedAssaultCount, value ); }
        }

        /// <summary>
        /// Question Number: L10
        /// </summary>
        /// <value>The arrested charged assault count note.</value>
        public string ArrestedChargedAssaultCountNote
        {
            get { return _arrestedChargedAssaultCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedAssaultCountNote, () => ArrestedChargedAssaultCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L8
        /// </summary>
        /// <value>The arrested charged burglary larcency count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedBurglaryLarcencyCount
        {
            get { return _arrestedChargedBurglaryLarcencyCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedBurglaryLarcencyCount, () => ArrestedChargedBurglaryLarcencyCount, value ); }
        }

        /// <summary>
        /// Question Number: L8
        /// </summary>
        /// <value>The arrested charged burglary larcency count note.</value>
        public string ArrestedChargedBurglaryLarcencyCountNote
        {
            get { return _arrestedChargedBurglaryLarcencyCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedBurglaryLarcencyCountNote, () => ArrestedChargedBurglaryLarcencyCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L15
        /// </summary>
        /// <value>The arrested charged contempt of count count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedContemptOfCountCount
        {
            get { return _arrestedChargedContemptOfCountCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedContemptOfCountCount, () => ArrestedChargedContemptOfCountCount, value ); }
        }

        /// <summary>
        /// Question Number: L15
        /// </summary>
        /// <value>The arrested charged contempt of count count note.</value>
        public string ArrestedChargedContemptOfCountCountNote
        {
            get { return _arrestedChargedContemptOfCountCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedContemptOfCountCountNote, () => ArrestedChargedContemptOfCountCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L5
        /// </summary>
        /// <value>The arrested charged drug charges count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedDrugChargesCount
        {
            get { return _arrestedChargedDrugChargesCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedDrugChargesCount, () => ArrestedChargedDrugChargesCount, value ); }
        }

        /// <summary>
        /// Question Number: L5
        /// </summary>
        /// <value>The arrested charged drug charges count note.</value>
        public string ArrestedChargedDrugChargesCountNote
        {
            get { return _arrestedChargedDrugChargesCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedDrugChargesCountNote, () => ArrestedChargedDrugChargesCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L6
        /// </summary>
        /// <value>The arrested charged forgery count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedForgeryCount
        {
            get { return _arrestedChargedForgeryCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedForgeryCount, () => ArrestedChargedForgeryCount, value ); }
        }

        /// <summary>
        /// Question Number: L6
        /// </summary>
        /// <value>The arrested charged forgery count note.</value>
        public string ArrestedChargedForgeryCountNote
        {
            get { return _arrestedChargedForgeryCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedForgeryCountNote, () => ArrestedChargedForgeryCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L13
        /// </summary>
        /// <value>The arrested charged homicide manslaughter count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedHomicideManslaughterCount
        {
            get { return _arrestedChargedHomicideManslaughterCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedHomicideManslaughterCount, () => ArrestedChargedHomicideManslaughterCount, value ); }
        }

        /// <summary>
        /// Question Number: L13
        /// </summary>
        /// <value>The arrested charged homicide manslaughter count note.</value>
        public string ArrestedChargedHomicideManslaughterCountNote
        {
            get { return _arrestedChargedHomicideManslaughterCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedHomicideManslaughterCountNote, () => ArrestedChargedHomicideManslaughterCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L16
        /// </summary>
        /// <value>The arrested charged other count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedOtherCount
        {
            get { return _arrestedChargedOtherCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedOtherCount, () => ArrestedChargedOtherCount, value ); }
        }

        /// <summary>
        /// Question Number: L16
        /// </summary>
        /// <value>The arrested charged other description.</value>
        public string ArrestedChargedOtherDescription
        {
            get { return _arrestedChargedOtherDescription; }
            set { ApplyPropertyChange ( ref _arrestedChargedOtherDescription, () => ArrestedChargedOtherDescription, value ); }
        }

        /// <summary>
        /// Question Number: L16
        /// </summary>
        /// <value>The arrested charged other note.</value>
        public string ArrestedChargedOtherNote
        {
            get { return _arrestedChargedOtherNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedOtherNote, () => ArrestedChargedOtherNote, value ); }
        }

        /// <summary>
        /// Question Number: L4
        /// </summary>
        /// <value>The arrested charged probation parole violation count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedProbationParoleViolationCount
        {
            get { return _arrestedChargedProbationParoleViolationCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedProbationParoleViolationCount, () => ArrestedChargedProbationParoleViolationCount, value ); }
        }

        /// <summary>
        /// Question Number: L4
        /// </summary>
        /// <value>The arrested charged probation parole violation count note.</value>
        public string ArrestedChargedProbationParoleViolationCountNote
        {
            get { return _arrestedChargedProbationParoleViolationCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _arrestedChargedProbationParoleViolationCountNote, () => ArrestedChargedProbationParoleViolationCountNote, value );
            }
        }

        /// <summary>
        /// Question Number: L14
        /// </summary>
        /// <value>The arrested charged prostitution count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedProstitutionCount
        {
            get { return _arrestedChargedProstitutionCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedProstitutionCount, () => ArrestedChargedProstitutionCount, value ); }
        }

        /// <summary>
        /// Question Number: L14
        /// </summary>
        /// <value>The arrested charged prostitution count note.</value>
        public string ArrestedChargedProstitutionCountNote
        {
            get { return _arrestedChargedProstitutionCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedProstitutionCountNote, () => ArrestedChargedProstitutionCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L12
        /// </summary>
        /// <value>The arrested charged rape count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedRapeCount
        {
            get { return _arrestedChargedRapeCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedRapeCount, () => ArrestedChargedRapeCount, value ); }
        }

        /// <summary>
        /// Question Number: L12
        /// </summary>
        /// <value>The arrested charged rape count note.</value>
        public string ArrestedChargedRapeCountNote
        {
            get { return _arrestedChargedRapeCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedRapeCountNote, () => ArrestedChargedRapeCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L9
        /// </summary>
        /// <value>The arrested charged robbery count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedRobberyCount
        {
            get { return _arrestedChargedRobberyCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedRobberyCount, () => ArrestedChargedRobberyCount, value ); }
        }

        /// <summary>
        /// Question Number: L9
        /// </summary>
        /// <value>The arrested charged robbery count note.</value>
        public string ArrestedChargedRobberyCountNote
        {
            get { return _arrestedChargedRobberyCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedRobberyCountNote, () => ArrestedChargedRobberyCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L3
        /// </summary>
        /// <value>The arrested charged shoplifting count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedShopliftingCount
        {
            get { return _arrestedChargedShopliftingCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedShopliftingCount, () => ArrestedChargedShopliftingCount, value ); }
        }

        /// <summary>
        /// Question Number: L3
        /// </summary>
        /// <value>The arrested charged shoplifting count note.</value>
        public string ArrestedChargedShopliftingCountNote
        {
            get { return _arrestedChargedShopliftingCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedShopliftingCountNote, () => ArrestedChargedShopliftingCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L7
        /// </summary>
        /// <value>The arrested charged weapons offense count.</value>
        public DensAsiNonResponseTypeDto<int?> ArrestedChargedWeaponsOffenseCount
        {
            get { return _arrestedChargedWeaponsOffenseCount; }
            set { ApplyPropertyChange ( ref _arrestedChargedWeaponsOffenseCount, () => ArrestedChargedWeaponsOffenseCount, value ); }
        }

        /// <summary>
        /// Question Number: L7
        /// </summary>
        /// <value>The arrested charged weapons offense count note.</value>
        public string ArrestedChargedWeaponsOffenseCountNote
        {
            get { return _arrestedChargedWeaponsOffenseCountNote; }
            set { ApplyPropertyChange ( ref _arrestedChargedWeaponsOffenseCountNote, () => ArrestedChargedWeaponsOffenseCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L18
        /// </summary>
        /// <value>The charged with disorderly conduct count.</value>
        public DensAsiNonResponseTypeDto<int?> ChargedWithDisorderlyConductCount
        {
            get { return _chargedWithDisorderlyConductCount; }
            set { ApplyPropertyChange ( ref _chargedWithDisorderlyConductCount, () => ChargedWithDisorderlyConductCount, value ); }
        }

        /// <summary>
        /// Question Number: L18
        /// </summary>
        /// <value>The charged with disorderly conduct count note.</value>
        public string ChargedWithDisorderlyConductCountNote
        {
            get { return _chargedWithDisorderlyConductCountNote; }
            set { ApplyPropertyChange ( ref _chargedWithDisorderlyConductCountNote, () => ChargedWithDisorderlyConductCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L19
        /// </summary>
        /// <value>The charged with driving while intoxicated count.</value>
        public DensAsiNonResponseTypeDto<int?> ChargedWithDrivingWhileIntoxicatedCount
        {
            get { return _chargedWithDrivingWhileIntoxicatedCount; }
            set { ApplyPropertyChange ( ref _chargedWithDrivingWhileIntoxicatedCount, () => ChargedWithDrivingWhileIntoxicatedCount, value ); }
        }

        /// <summary>
        /// Question Number: L19
        /// </summary>
        /// <value>The charged with driving while intoxicated count note.</value>
        public string ChargedWithDrivingWhileIntoxicatedCountNote
        {
            get { return _chargedWithDrivingWhileIntoxicatedCountNote; }
            set { ApplyPropertyChange ( ref _chargedWithDrivingWhileIntoxicatedCountNote, () => ChargedWithDrivingWhileIntoxicatedCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L20
        /// </summary>
        /// <value>The charged with major driving violations count.</value>
        public DensAsiNonResponseTypeDto<int?> ChargedWithMajorDrivingViolationsCount
        {
            get { return _chargedWithMajorDrivingViolationsCount; }
            set { ApplyPropertyChange ( ref _chargedWithMajorDrivingViolationsCount, () => ChargedWithMajorDrivingViolationsCount, value ); }
        }

        /// <summary>
        /// Question Number: L20
        /// </summary>
        /// <value>The charged with major driving violations count note.</value>
        public string ChargedWithMajorDrivingViolationsCountNote
        {
            get { return _chargedWithMajorDrivingViolationsCountNote; }
            set { ApplyPropertyChange ( ref _chargedWithMajorDrivingViolationsCountNote, () => ChargedWithMajorDrivingViolationsCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L31
        /// </summary>
        /// <value>The confidence distorted by patient misrepresentation indicator.</value>
        public bool? ConfidenceDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceDistortedByPatientMisrepresentationIndicator, () => ConfidenceDistortedByPatientMisrepresentationIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: L31
        /// </summary>
        /// <value>The confidence distorted by patient misrepresentation indicator note.</value>
        public string ConfidenceDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceDistortedByPatientMisrepresentationIndicatorNote,
                    () => ConfidenceDistortedByPatientMisrepresentationIndicatorNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: L32
        /// </summary>
        /// <value>The confidence rate distorted by patient inability to understand indicator.</value>
        public bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                    () => ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                    value );
            }
        }

        /// <summary>
        /// Question Number: L32
        /// </summary>
        /// <value>The confidence rate distorted by patient inability to understand indicator note.</value>
        public string ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                    () => ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: L27
        /// </summary>
        /// <value>The illegal activity in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> IllegalActivityInLastThirtyDaysDayCount
        {
            get { return _illegalActivityInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _illegalActivityInLastThirtyDaysDayCount, () => IllegalActivityInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: L27
        /// </summary>
        /// <value>The illegal activity in last thirty days day count note.</value>
        public string IllegalActivityInLastThirtyDaysDayCountNote
        {
            get { return _illegalActivityInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _illegalActivityInLastThirtyDaysDayCountNote, () => IllegalActivityInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L29
        /// </summary>
        /// <value>The importance of legal problem counseling dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfLegalProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfLegalProblemCounselingDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfLegalProblemCounselingDensAsiPatientRating, () => ImportanceOfLegalProblemCounselingDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: L29
        /// </summary>
        /// <value>The importance of legal problem counseling dens asi patient rating note.</value>
        public string ImportanceOfLegalProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfLegalProblemCounselingDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfLegalProblemCounselingDensAsiPatientRatingNote,
                    () => ImportanceOfLegalProblemCounselingDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: L26
        /// </summary>
        /// <value>The incarcerated in last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> IncarceratedInLastThirtyDaysDayCount
        {
            get { return _incarceratedInLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _incarceratedInLastThirtyDaysDayCount, () => IncarceratedInLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: L26
        /// </summary>
        /// <value>The incarcerated in last thirty days day count note.</value>
        public string IncarceratedInLastThirtyDaysDayCountNote
        {
            get { return _incarceratedInLastThirtyDaysDayCountNote; }
            set { ApplyPropertyChange ( ref _incarceratedInLastThirtyDaysDayCountNote, () => IncarceratedInLastThirtyDaysDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L23
        /// </summary>
        /// <value>The type of the incarceration for dens asi violation.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> IncarcerationForDensAsiViolationType
        {
            get { return _incarcerationForDensAsiViolationType; }
            set { ApplyPropertyChange ( ref _incarcerationForDensAsiViolationType, () => IncarcerationForDensAsiViolationType, value ); }
        }

        /// <summary>
        /// Question Number: L23
        /// </summary>
        /// <value>The incarceration for dens asi violation type note.</value>
        public string IncarcerationForDensAsiViolationTypeNote
        {
            get { return _incarcerationForDensAsiViolationTypeNote; }
            set { ApplyPropertyChange ( ref _incarcerationForDensAsiViolationTypeNote, () => IncarcerationForDensAsiViolationTypeNote, value ); }
        }

        /// <summary>
        /// Question Number: L21
        /// </summary>
        /// <value>The incarceration in life month count.</value>
        public DensAsiNonResponseTypeDto<int?> IncarcerationInLifeMonthCount
        {
            get { return _incarcerationInLifeMonthCount; }
            set { ApplyPropertyChange ( ref _incarcerationInLifeMonthCount, () => IncarcerationInLifeMonthCount, value ); }
        }

        /// <summary>
        /// Question Number: L21
        /// </summary>
        /// <value>The incarceration in life month count note.</value>
        public string IncarcerationInLifeMonthCountNote
        {
            get { return _incarcerationInLifeMonthCountNote; }
            set { ApplyPropertyChange ( ref _incarcerationInLifeMonthCountNote, () => IncarcerationInLifeMonthCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L22
        /// </summary>
        /// <value>The incarceration length month count note.</value>
        public string IncarcerationLengthMonthCountNote
        {
            get { return _incarcerationLengthMonthCountNote; }
            set { ApplyPropertyChange ( ref _incarcerationLengthMonthCountNote, () => IncarcerationLengthMonthCountNote, value ); }
        }

        /// <summary>
        /// Question Number: L22
        /// </summary>
        /// <value>The last incarceration length month count.</value>
        public DensAsiNonResponseTypeDto<int?> LastIncarcerationLengthMonthCount
        {
            get { return _lastIncarcerationLengthMonthCount; }
            set { ApplyPropertyChange ( ref _lastIncarcerationLengthMonthCount, () => LastIncarcerationLengthMonthCount, value ); }
        }

        /// <summary>
        /// Question Number: L30
        /// </summary>
        /// <value>The patient counseling dens asi interviewer rating.</value>
        public LookupValueDto PatientCounselingDensAsiInterviewerRating
        {
            get { return _patientCounselingDensAsiInterviewerRating; }
            set { ApplyPropertyChange ( ref _patientCounselingDensAsiInterviewerRating, () => PatientCounselingDensAsiInterviewerRating, value ); }
        }

        /// <summary>
        /// Question Number: L30
        /// </summary>
        /// <value>The patient counseling dens asi interviewer rating note.</value>
        public string PatientCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientCounselingDensAsiInterviewerRatingNote; }
            set { ApplyPropertyChange ( ref _patientCounselingDensAsiInterviewerRatingNote, () => PatientCounselingDensAsiInterviewerRatingNote, value ); }
        }

        /// <summary>
        /// Question Number: L25
        /// </summary>
        /// <value>The type of the presently awaiting charges for dens asi violation.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> PresentlyAwaitingChargesForDensAsiViolationType
        {
            get { return _presentlyAwaitingChargesForDensAsiViolationType; }
            set
            {
                ApplyPropertyChange (
                    ref _presentlyAwaitingChargesForDensAsiViolationType, () => PresentlyAwaitingChargesForDensAsiViolationType, value );
            }
        }

        /// <summary>
        /// Question Number: L25
        /// </summary>
        /// <value>The presently awaiting charges for note.</value>
        public string PresentlyAwaitingChargesForNote
        {
            get { return _presentlyAwaitingChargesForNote; }
            set { ApplyPropertyChange ( ref _presentlyAwaitingChargesForNote, () => PresentlyAwaitingChargesForNote, value ); }
        }

        /// <summary>
        /// Question Number: L24
        /// </summary>
        /// <value>The presently awaiting charges indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PresentlyAwaitingChargesIndicator
        {
            get { return _presentlyAwaitingChargesIndicator; }
            set { ApplyPropertyChange ( ref _presentlyAwaitingChargesIndicator, () => PresentlyAwaitingChargesIndicator, value ); }
        }

        /// <summary>
        /// Question Number: L24
        /// </summary>
        /// <value>The presently awaiting charges indicator note.</value>
        public string PresentlyAwaitingChargesIndicatorNote
        {
            get { return _presentlyAwaitingChargesIndicatorNote; }
            set { ApplyPropertyChange ( ref _presentlyAwaitingChargesIndicatorNote, () => PresentlyAwaitingChargesIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: L2
        /// </summary>
        /// <value>The probation or parole indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ProbationOrParoleIndicator
        {
            get { return _probationOrParoleIndicator; }
            set { ApplyPropertyChange ( ref _probationOrParoleIndicator, () => ProbationOrParoleIndicator, value ); }
        }

        /// <summary>
        /// Question Number: L2
        /// </summary>
        /// <value>The probation or parole indicator note.</value>
        public string ProbationOrParoleIndicatorNote
        {
            get { return _probationOrParoleIndicatorNote; }
            set { ApplyPropertyChange ( ref _probationOrParoleIndicatorNote, () => ProbationOrParoleIndicatorNote, value ); }
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
        /// Question Number: L28
        /// </summary>
        /// <value>The seriousness of legal problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> SeriousnessOfLegalProblemsDensAsiPatientRating
        {
            get { return _seriousnessOfLegalProblemsDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _seriousnessOfLegalProblemsDensAsiPatientRating, () => SeriousnessOfLegalProblemsDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: L28
        /// </summary>
        /// <value>The seriousness of legal problems dens asi patient rating note.</value>
        public string SeriousnessOfLegalProblemsDensAsiPatientRatingNote
        {
            get { return _seriousnessOfLegalProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _seriousnessOfLegalProblemsDensAsiPatientRatingNote, () => SeriousnessOfLegalProblemsDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: L103
        /// </summary>
        /// <value>The treatment instead of incarceration in prison indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> TreatmentInsteadOfIncarcerationInPrisonIndicator
        {
            get { return _treatmentInsteadOfIncarcerationInPrisonIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _treatmentInsteadOfIncarcerationInPrisonIndicator, () => TreatmentInsteadOfIncarcerationInPrisonIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: L103
        /// </summary>
        /// <value>The treatment instead of incarceration in prison indicator note.</value>
        public string TreatmentInsteadOfIncarcerationInPrisonIndicatorNote
        {
            get { return _treatmentInsteadOfIncarcerationInPrisonIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _treatmentInsteadOfIncarcerationInPrisonIndicatorNote, () => TreatmentInsteadOfIncarcerationInPrisonIndicatorNote, value );
            }
        }

        /// <summary>
        /// Question Number: L102
        /// </summary>
        /// <value>The treatment mandatory for criminal justice system indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> TreatmentMandatoryForCriminalJusticeSystemIndicator
        {
            get { return _treatmentMandatoryForCriminalJusticeSystemIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _treatmentMandatoryForCriminalJusticeSystemIndicator, () => TreatmentMandatoryForCriminalJusticeSystemIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: L102
        /// </summary>
        /// <value>The treatment mandatory for criminal justice system indicator note.</value>
        public string TreatmentMandatoryForCriminalJusticeSystemIndicatorNote
        {
            get { return _treatmentMandatoryForCriminalJusticeSystemIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _treatmentMandatoryForCriminalJusticeSystemIndicatorNote, () => TreatmentMandatoryForCriminalJusticeSystemIndicatorNote, value );
            }
        }

        #endregion
    }
}
