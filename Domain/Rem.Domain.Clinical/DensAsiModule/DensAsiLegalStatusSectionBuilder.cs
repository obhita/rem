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
    /// DensAsiLegalStatusSectionBuilder provides a fluent interface for creating a Legal Information section.
    /// </summary>
    public class DensAsiLegalStatusSectionBuilder
    {
        private DensAsiNonResponseType<bool?> _admissionPromptedByCriminalJusticeSystemIndicator;
        private string _admissionPromptedByCriminalJusticeSystemIndicatorNote;
        private DensAsiNonResponseType<bool?> _probationOrParoleIndicator;
        private string _probationOrParoleIndicatorNote;
        private DensAsiNonResponseType<int?> _arrestedChargedShopliftingCount;
        private string _arrestedChargedShopliftingCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedProbationParoleViolationCount;
        private string _arrestedChargedProbationParoleViolationCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedDrugChargesCount;
        private string _arrestedChargedDrugChargesCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedForgeryCount;
        private string _arrestedChargedForgeryCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedWeaponsOffenseCount;
        private string _arrestedChargedWeaponsOffenseCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedBurglaryLarcencyCount;
        private string _arrestedChargedBurglaryLarcencyCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedRobberyCount;
        private string _arrestedChargedRobberyCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedAssaultCount;
        private string _arrestedChargedAssaultCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedArsonCount;
        private string _arrestedChargedArsonCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedRapeCount;
        private string _arrestedChargedRapeCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedHomicideManslaughterCount;
        private string _arrestedChargedHomicideManslaughterCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedProstitutionCount;
        private string _arrestedChargedProstitutionCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedContemptOfCountCount;
        private string _arrestedChargedContemptOfCountCountNote;
        private DensAsiNonResponseType<int?> _arrestedChargedOtherCount;
        private string _arrestedChargedOtherDescription;
        private string _arrestedChargedOtherNote;
        private DensAsiNonResponseType<int?> _arrestChargesResultedInConvictionsCount;
        private string _arrestChargesResultedInConvictionsCountNote;
        private DensAsiNonResponseType<int?> _chargedWithDisorderlyConductCount;
        private string _chargedWithDisorderlyConductCountNote;
        private DensAsiNonResponseType<int?> _chargedWithDrivingWhileIntoxicatedCount;
        private string _chargedWithDrivingWhileIntoxicatedCountNote;
        private DensAsiNonResponseType<int?> _chargedWithMajorDrivingViolationsCount;
        private string _chargedWithMajorDrivingViolationsCountNote;
        private DensAsiNonResponseType<int?> _incarcerationInLifeMonthCount;
        private string _incarcerationInLifeMonthCountNote;
        private DensAsiNonResponseType<int?> _lastIncarcerationLengthMonthCount;
        private string _incarcerationLengthMonthCountNote;
        private DensAsiNonResponseType<DensAsiViolationType> _incarcerationForDensAsiViolationType;
        private string _incarcerationForDensAsiViolationTypeNote;
        private DensAsiNonResponseType<bool?> _presentlyAwaitingChargesIndicator;
        private string _presentlyAwaitingChargesIndicatorNote;
        private DensAsiNonResponseType<DensAsiViolationType> _presentlyAwaitingChargesForDensAsiViolationType;
        private string _presentlyAwaitingChargesForNote;
        private DensAsiNonResponseType<int?> _incarceratedInLastThirtyDaysDayCount;
        private string _incarceratedInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<int?> _illegalActivityInLastThirtyDaysDayCount;
        private string _illegalActivityInLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _seriousnessOfLegalProblemsDensAsiPatientRating;
        private string _seriousnessOfLegalProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfLegalProblemCounselingDensAsiPatientRating;
        private string _importanceOfLegalProblemCounselingDensAsiPatientRatingNote;
        private DensAsiInterviewerRating _patientCounselingDensAsiInterviewerRating;
        private string _patientCounselingDensAsiInterviewerRatingNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private string _sectionNote;
        private DensAsiNonResponseType<bool?> _treatmentMandatoryForCriminalJusticeSystemIndicator;
        private string _treatmentMandatoryForCriminalJusticeSystemIndicatorNote;
        private DensAsiNonResponseType<bool?> _treatmentInsteadOfIncarcerationInPrisonIndicator;
        private string _treatmentInsteadOfIncarcerationInPrisonIndicatorNote;


        /// <summary>
        /// Assigns the admission prompted by criminal justice system indicator.
        /// </summary>
        /// <param name="admissionPromptedByCriminalJusticeSystemIndicator">The admission prompted by criminal justice system indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithAdmissionPromptedByCriminalJusticeSystemIndicator(DensAsiNonResponseType<bool?> admissionPromptedByCriminalJusticeSystemIndicator)
        {
            _admissionPromptedByCriminalJusticeSystemIndicator = admissionPromptedByCriminalJusticeSystemIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the admission prompted by criminal justice system indicator note.
        /// </summary>
        /// <param name="admissionPromptedByCriminalJusticeSystemIndicatorNote">The admission prompted by criminal justice system indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithAdmissionPromptedByCriminalJusticeSystemIndicatorNote(string admissionPromptedByCriminalJusticeSystemIndicatorNote)
        {
            _admissionPromptedByCriminalJusticeSystemIndicatorNote = admissionPromptedByCriminalJusticeSystemIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the probation or parole indicator.
        /// </summary>
        /// <param name="probationOrParoleIndicator">The probation or parole indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithProbationOrParoleIndicator(DensAsiNonResponseType<bool?> probationOrParoleIndicator)
        {
            _probationOrParoleIndicator = probationOrParoleIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the probation or parole indicator note.
        /// </summary>
        /// <param name="probationOrParoleIndicatorNote">The probation or parole indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithProbationOrParoleIndicatorNote(string probationOrParoleIndicatorNote)
        {
            _probationOrParoleIndicatorNote = probationOrParoleIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged shoplifting count.
        /// </summary>
        /// <param name="arrestedChargedShopliftingCount">The arrested charged shoplifting count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedShopliftingCount(DensAsiNonResponseType<int?> arrestedChargedShopliftingCount)
        {
            _arrestedChargedShopliftingCount = arrestedChargedShopliftingCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged shoplifting count note.
        /// </summary>
        /// <param name="arrestedChargedShopliftingCountNote">The arrested charged shoplifting count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedShopliftingCountNote(string arrestedChargedShopliftingCountNote)
        {
            _arrestedChargedShopliftingCountNote = arrestedChargedShopliftingCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged probation parole violation count.
        /// </summary>
        /// <param name="arrestedChargedProbationParoleViolationCount">The arrested charged probation parole violation count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedProbationParoleViolationCount(DensAsiNonResponseType<int?> arrestedChargedProbationParoleViolationCount)
        {
            _arrestedChargedProbationParoleViolationCount = arrestedChargedProbationParoleViolationCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged probation parole violation count note.
        /// </summary>
        /// <param name="arrestedChargedProbationParoleViolationCountNote">The arrested charged probation parole violation count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedProbationParoleViolationCountNote(string arrestedChargedProbationParoleViolationCountNote)
        {
            _arrestedChargedProbationParoleViolationCountNote = arrestedChargedProbationParoleViolationCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged drug charges count.
        /// </summary>
        /// <param name="arrestedChargedDrugChargesCount">The arrested charged drug charges count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedDrugChargesCount(DensAsiNonResponseType<int?> arrestedChargedDrugChargesCount)
        {
            _arrestedChargedDrugChargesCount = arrestedChargedDrugChargesCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged drug charges count note.
        /// </summary>
        /// <param name="arrestedChargedDrugChargesCountNote">The arrested charged drug charges count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedDrugChargesCountNote(string arrestedChargedDrugChargesCountNote)
        {
            _arrestedChargedDrugChargesCountNote = arrestedChargedDrugChargesCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged forgery count.
        /// </summary>
        /// <param name="arrestedChargedForgeryCount">The arrested charged forgery count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedForgeryCount(DensAsiNonResponseType<int?> arrestedChargedForgeryCount)
        {
            _arrestedChargedForgeryCount = arrestedChargedForgeryCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged forgery count note.
        /// </summary>
        /// <param name="arrestedChargedForgeryCountNote">The arrested charged forgery count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedForgeryCountNote(string arrestedChargedForgeryCountNote)
        {
            _arrestedChargedForgeryCountNote = arrestedChargedForgeryCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged weapons offense count.
        /// </summary>
        /// <param name="arrestedChargedWeaponsOffenseCount">The arrested charged weapons offense count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedWeaponsOffenseCount(DensAsiNonResponseType<int?> arrestedChargedWeaponsOffenseCount)
        {
            _arrestedChargedWeaponsOffenseCount = arrestedChargedWeaponsOffenseCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged weapons offense count note.
        /// </summary>
        /// <param name="arrestedChargedWeaponsOffenseCountNote">The arrested charged weapons offense count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedWeaponsOffenseCountNote(string arrestedChargedWeaponsOffenseCountNote)
        {
            _arrestedChargedWeaponsOffenseCountNote = arrestedChargedWeaponsOffenseCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged burglary larcency count.
        /// </summary>
        /// <param name="arrestedChargedBurglaryLarcencyCount">The arrested charged burglary larcency count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedBurglaryLarcencyCount(DensAsiNonResponseType<int?> arrestedChargedBurglaryLarcencyCount)
        {
            _arrestedChargedBurglaryLarcencyCount = arrestedChargedBurglaryLarcencyCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged burglary larcency count note.
        /// </summary>
        /// <param name="arrestedChargedBurglaryLarcencyCountNote">The arrested charged burglary larcency count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedBurglaryLarcencyCountNote(string arrestedChargedBurglaryLarcencyCountNote)
        {
            _arrestedChargedBurglaryLarcencyCountNote = arrestedChargedBurglaryLarcencyCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged robbery count.
        /// </summary>
        /// <param name="arrestedChargedRobberyCount">The arrested charged robbery count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedRobberyCount(DensAsiNonResponseType<int?> arrestedChargedRobberyCount)
        {
            _arrestedChargedRobberyCount = arrestedChargedRobberyCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged robbery count note.
        /// </summary>
        /// <param name="arrestedChargedRobberyCountNote">The arrested charged robbery count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedRobberyCountNote(string arrestedChargedRobberyCountNote)
        {
            _arrestedChargedRobberyCountNote = arrestedChargedRobberyCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged assault count.
        /// </summary>
        /// <param name="arrestedChargedAssaultCount">The arrested charged assault count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedAssaultCount(DensAsiNonResponseType<int?> arrestedChargedAssaultCount)
        {
            _arrestedChargedAssaultCount = arrestedChargedAssaultCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged assault count note.
        /// </summary>
        /// <param name="arrestedChargedAssaultCountNote">The arrested charged assault count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedAssaultCountNote(string arrestedChargedAssaultCountNote)
        {
            _arrestedChargedAssaultCountNote = arrestedChargedAssaultCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged arson count.
        /// </summary>
        /// <param name="arrestedChargedArsonCount">The arrested charged arson count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedArsonCount(DensAsiNonResponseType<int?> arrestedChargedArsonCount)
        {
            _arrestedChargedArsonCount = arrestedChargedArsonCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged arson count note.
        /// </summary>
        /// <param name="arrestedChargedArsonCountNote">The arrested charged arson count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedArsonCountNote(string arrestedChargedArsonCountNote)
        {
            _arrestedChargedArsonCountNote = arrestedChargedArsonCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged rape count.
        /// </summary>
        /// <param name="arrestedChargedRapeCount">The arrested charged rape count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedRapeCount(DensAsiNonResponseType<int?> arrestedChargedRapeCount)
        {
            _arrestedChargedRapeCount = arrestedChargedRapeCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged rape count note.
        /// </summary>
        /// <param name="arrestedChargedRapeCountNote">The arrested charged rape count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedRapeCountNote(string arrestedChargedRapeCountNote)
        {
            _arrestedChargedRapeCountNote = arrestedChargedRapeCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged homicide manslaughter count.
        /// </summary>
        /// <param name="arrestedChargedHomicideManslaughterCount">The arrested charged homicide manslaughter count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedHomicideManslaughterCount(DensAsiNonResponseType<int?> arrestedChargedHomicideManslaughterCount)
        {
            _arrestedChargedHomicideManslaughterCount = arrestedChargedHomicideManslaughterCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged homicide manslaughter count note.
        /// </summary>
        /// <param name="arrestedChargedHomicideManslaughterCountNote">The arrested charged homicide manslaughter count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedHomicideManslaughterCountNote(string arrestedChargedHomicideManslaughterCountNote)
        {
            _arrestedChargedHomicideManslaughterCountNote = arrestedChargedHomicideManslaughterCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged prostitution count.
        /// </summary>
        /// <param name="arrestedChargedProstitutionCount">The arrested charged prostitution count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedProstitutionCount(DensAsiNonResponseType<int?> arrestedChargedProstitutionCount)
        {
            _arrestedChargedProstitutionCount = arrestedChargedProstitutionCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged prostitution count note.
        /// </summary>
        /// <param name="arrestedChargedProstitutionCountNote">The arrested charged prostitution count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedProstitutionCountNote(string arrestedChargedProstitutionCountNote)
        {
            _arrestedChargedProstitutionCountNote = arrestedChargedProstitutionCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged contempt of count count.
        /// </summary>
        /// <param name="arrestedChargedContemptOfCountCount">The arrested charged contempt of count count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedContemptOfCountCount(DensAsiNonResponseType<int?> arrestedChargedContemptOfCountCount)
        {
            _arrestedChargedContemptOfCountCount = arrestedChargedContemptOfCountCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged contempt of count count note.
        /// </summary>
        /// <param name="arrestedChargedContemptOfCountCountNote">The arrested charged contempt of count count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedContemptOfCountCountNote(string arrestedChargedContemptOfCountCountNote)
        {
            _arrestedChargedContemptOfCountCountNote = arrestedChargedContemptOfCountCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged other count.
        /// </summary>
        /// <param name="arrestedChargedOtherCount">The arrested charged other count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedOtherCount(DensAsiNonResponseType<int?> arrestedChargedOtherCount)
        {
            _arrestedChargedOtherCount = arrestedChargedOtherCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged other description.
        /// </summary>
        /// <param name="arrestedChargedOtherDescription">The arrested charged other description.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedOtherDescription(string arrestedChargedOtherDescription)
        {
            _arrestedChargedOtherDescription = arrestedChargedOtherDescription;
            return this;
        }

        /// <summary>
        /// Assigns the arrested charged other note.
        /// </summary>
        /// <param name="arrestedChargedOtherNote">The arrested charged other note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestedChargedOtherNote(string arrestedChargedOtherNote)
        {
            _arrestedChargedOtherNote = arrestedChargedOtherNote;
            return this;
        }

        /// <summary>
        /// Assigns the arrest charges resulted in convictions count.
        /// </summary>
        /// <param name="arrestChargesResultedInConvictionsCount">The arrest charges resulted in convictions count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestChargesResultedInConvictionsCount(DensAsiNonResponseType<int?> arrestChargesResultedInConvictionsCount)
        {
            _arrestChargesResultedInConvictionsCount = arrestChargesResultedInConvictionsCount;
            return this;
        }

        /// <summary>
        /// Assigns the arrest charges resulted in convictions count note.
        /// </summary>
        /// <param name="arrestChargesResultedInConvictionsCountNote">The arrest charges resulted in convictions count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithArrestChargesResultedInConvictionsCountNote(string arrestChargesResultedInConvictionsCountNote)
        {
            _arrestChargesResultedInConvictionsCountNote = arrestChargesResultedInConvictionsCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the charged with disorderly conduct count.
        /// </summary>
        /// <param name="chargedWithDisorderlyConductCount">The charged with disorderly conduct count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithChargedWithDisorderlyConductCount(DensAsiNonResponseType<int?> chargedWithDisorderlyConductCount)
        {
            _chargedWithDisorderlyConductCount = chargedWithDisorderlyConductCount;
            return this;
        }

        /// <summary>
        /// Assigns the charged with disorderly conduct count note.
        /// </summary>
        /// <param name="chargedWithDisorderlyConductCountNote">The charged with disorderly conduct count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithChargedWithDisorderlyConductCountNote(string chargedWithDisorderlyConductCountNote)
        {
            _chargedWithDisorderlyConductCountNote = chargedWithDisorderlyConductCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the charged with driving while intoxicated count.
        /// </summary>
        /// <param name="chargedWithDrivingWhileIntoxicatedCount">The charged with driving while intoxicated count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithChargedWithDrivingWhileIntoxicatedCount(DensAsiNonResponseType<int?> chargedWithDrivingWhileIntoxicatedCount)
        {
            _chargedWithDrivingWhileIntoxicatedCount = chargedWithDrivingWhileIntoxicatedCount;
            return this;
        }

        /// <summary>
        /// Assigns the charged with driving while intoxicated count note.
        /// </summary>
        /// <param name="chargedWithDrivingWhileIntoxicatedCountNote">The charged with driving while intoxicated count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithChargedWithDrivingWhileIntoxicatedCountNote(string chargedWithDrivingWhileIntoxicatedCountNote)
        {
            _chargedWithDrivingWhileIntoxicatedCountNote = chargedWithDrivingWhileIntoxicatedCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the charged with major driving violations count.
        /// </summary>
        /// <param name="chargedWithMajorDrivingViolationsCount">The charged with major driving violations count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithChargedWithMajorDrivingViolationsCount(DensAsiNonResponseType<int?> chargedWithMajorDrivingViolationsCount)
        {
            _chargedWithMajorDrivingViolationsCount = chargedWithMajorDrivingViolationsCount;
            return this;
        }

        /// <summary>
        /// Assigns the charged with major driving violations count note.
        /// </summary>
        /// <param name="chargedWithMajorDrivingViolationsCountNote">The charged with major driving violations count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithChargedWithMajorDrivingViolationsCountNote(string chargedWithMajorDrivingViolationsCountNote)
        {
            _chargedWithMajorDrivingViolationsCountNote = chargedWithMajorDrivingViolationsCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the incarceration in life month count.
        /// </summary>
        /// <param name="incarcerationInLifeMonthCount">The incarceration in life month count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIncarcerationInLifeMonthCount(DensAsiNonResponseType<int?> incarcerationInLifeMonthCount)
        {
            _incarcerationInLifeMonthCount = incarcerationInLifeMonthCount;
            return this;
        }

        /// <summary>
        /// Assigns the incarceration in life month count note.
        /// </summary>
        /// <param name="incarcerationInLifeMonthCountNote">The incarceration in life month count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIncarcerationInLifeMonthCountNote(string incarcerationInLifeMonthCountNote)
        {
            _incarcerationInLifeMonthCountNote = incarcerationInLifeMonthCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the last incarceration length month count.
        /// </summary>
        /// <param name="lastIncarcerationLengthMonthCount">The last incarceration length month count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithLastIncarcerationLengthMonthCount(DensAsiNonResponseType<int?> lastIncarcerationLengthMonthCount)
        {
            _lastIncarcerationLengthMonthCount = lastIncarcerationLengthMonthCount;
            return this;
        }

        /// <summary>
        /// Assigns the incarceration length month count note.
        /// </summary>
        /// <param name="incarcerationLengthMonthCountNote">The incarceration length month count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIncarcerationLengthMonthCountNote(string incarcerationLengthMonthCountNote)
        {
            _incarcerationLengthMonthCountNote = incarcerationLengthMonthCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the type of the incarceration for DensAsi violation.
        /// </summary>
        /// <param name="incarcerationForDensAsiViolationType">Type of the incarceration for DensAsi violation.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIncarcerationForDensAsiViolationType(DensAsiNonResponseType<DensAsiViolationType> incarcerationForDensAsiViolationType)
        {
            _incarcerationForDensAsiViolationType = incarcerationForDensAsiViolationType;
            return this;
        }

        /// <summary>
        /// Assigns the incarceration for DensAsi violation type note.
        /// </summary>
        /// <param name="incarcerationForDensAsiViolationTypeNote">The incarceration for DensAsi violation type note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIncarcerationForDensAsiViolationTypeNote(string incarcerationForDensAsiViolationTypeNote)
        {
            _incarcerationForDensAsiViolationTypeNote = incarcerationForDensAsiViolationTypeNote;
            return this;
        }

        /// <summary>
        /// Assigns the presently awaiting charges indicator.
        /// </summary>
        /// <param name="presentlyAwaitingChargesIndicator">The presently awaiting charges indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithPresentlyAwaitingChargesIndicator(DensAsiNonResponseType<bool?> presentlyAwaitingChargesIndicator)
        {
            _presentlyAwaitingChargesIndicator = presentlyAwaitingChargesIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the presently awaiting charges indicator note.
        /// </summary>
        /// <param name="presentlyAwaitingChargesIndicatorNote">The presently awaiting charges indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithPresentlyAwaitingChargesIndicatorNote(string presentlyAwaitingChargesIndicatorNote)
        {
            _presentlyAwaitingChargesIndicatorNote = presentlyAwaitingChargesIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the type of the presently awaiting charges for DensAsi violation.
        /// </summary>
        /// <param name="presentlyAwaitingChargesForDensAsiViolationType">Type of the presently awaiting charges for DensAsi violation.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithPresentlyAwaitingChargesForDensAsiViolationType(DensAsiNonResponseType<DensAsiViolationType> presentlyAwaitingChargesForDensAsiViolationType)
        {
            _presentlyAwaitingChargesForDensAsiViolationType = presentlyAwaitingChargesForDensAsiViolationType;
            return this;
        }

        /// <summary>
        /// Assigns the presently awaiting charges for note.
        /// </summary>
        /// <param name="presentlyAwaitingChargesForNote">The presently awaiting charges for note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithPresentlyAwaitingChargesForNote(string presentlyAwaitingChargesForNote)
        {
            _presentlyAwaitingChargesForNote = presentlyAwaitingChargesForNote;
            return this;
        }

        /// <summary>
        /// Assigns the incarcerated in last thirty days day count.
        /// </summary>
        /// <param name="incarceratedInLastThirtyDaysDayCount">The incarcerated in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIncarceratedInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> incarceratedInLastThirtyDaysDayCount)
        {
            _incarceratedInLastThirtyDaysDayCount = incarceratedInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the incarcerated in last thirty days day count note.
        /// </summary>
        /// <param name="incarceratedInLastThirtyDaysDayCountNote">The incarcerated in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIncarceratedInLastThirtyDaysDayCountNote(string incarceratedInLastThirtyDaysDayCountNote)
        {
            _incarceratedInLastThirtyDaysDayCountNote = incarceratedInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the illegal activity in last thirty days day count.
        /// </summary>
        /// <param name="illegalActivityInLastThirtyDaysDayCount">The illegal activity in last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIllegalActivityInLastThirtyDaysDayCount(DensAsiNonResponseType<int?> illegalActivityInLastThirtyDaysDayCount)
        {
            _illegalActivityInLastThirtyDaysDayCount = illegalActivityInLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the illegal activity in last thirty days day count note.
        /// </summary>
        /// <param name="illegalActivityInLastThirtyDaysDayCountNote">The illegal activity in last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithIllegalActivityInLastThirtyDaysDayCountNote(string illegalActivityInLastThirtyDaysDayCountNote)
        {
            _illegalActivityInLastThirtyDaysDayCountNote = illegalActivityInLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the seriousness of legal problems DensAsi patient rating.
        /// </summary>
        /// <param name="seriousnessOfLegalProblemsDensAsiPatientRating">The seriousness of legal problems DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithSeriousnessOfLegalProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> seriousnessOfLegalProblemsDensAsiPatientRating)
        {
            _seriousnessOfLegalProblemsDensAsiPatientRating = seriousnessOfLegalProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the seriousness of legal problems DensAsi patient rating note.
        /// </summary>
        /// <param name="seriousnessOfLegalProblemsDensAsiPatientRatingNote">The seriousness of legal problems DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithSeriousnessOfLegalProblemsDensAsiPatientRatingNote(string seriousnessOfLegalProblemsDensAsiPatientRatingNote)
        {
            _seriousnessOfLegalProblemsDensAsiPatientRatingNote = seriousnessOfLegalProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of legal problem counseling DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfLegalProblemCounselingDensAsiPatientRating">The importance of legal problem counseling DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithImportanceOfLegalProblemCounselingDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfLegalProblemCounselingDensAsiPatientRating)
        {
            _importanceOfLegalProblemCounselingDensAsiPatientRating = importanceOfLegalProblemCounselingDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of legal problem counseling DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfLegalProblemCounselingDensAsiPatientRatingNote">The importance of legal problem counseling DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithImportanceOfLegalProblemCounselingDensAsiPatientRatingNote(string importanceOfLegalProblemCounselingDensAsiPatientRatingNote)
        {
            _importanceOfLegalProblemCounselingDensAsiPatientRatingNote = importanceOfLegalProblemCounselingDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient counseling DensAsi interviewer rating.
        /// </summary>
        /// <param name="patientCounselingDensAsiInterviewerRating">The patient counseling DensAsi interviewer rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithPatientCounselingDensAsiInterviewerRating(DensAsiInterviewerRating patientCounselingDensAsiInterviewerRating)
        {
            _patientCounselingDensAsiInterviewerRating = patientCounselingDensAsiInterviewerRating;
            return this;
        }

        /// <summary>
        /// Assigns the patient counseling DensAsi interviewer rating note.
        /// </summary>
        /// <param name="patientCounselingDensAsiInterviewerRatingNote">The patient counseling DensAsi interviewer rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithPatientCounselingDensAsiInterviewerRatingNote(string patientCounselingDensAsiInterviewerRatingNote)
        {
            _patientCounselingDensAsiInterviewerRatingNote = patientCounselingDensAsiInterviewerRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicator(bool? confidenceDistortedByPatientMisrepresentationIndicator)
        {
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator note.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicatorNote(string confidenceDistortedByPatientMisrepresentationIndicatorNote)
        {
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator(bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator note.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote(string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the section note.
        /// </summary>
        /// <param name="sectionNote">The section note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithSectionNote(string sectionNote)
        {
            _sectionNote = sectionNote;
            return this;
        }

        /// <summary>
        /// Assigns the treatment mandatory for criminal justice system indicator.
        /// </summary>
        /// <param name="treatmentMandatoryForCriminalJusticeSystemIndicator">The treatment mandatory for criminal justice system indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithTreatmentMandatoryForCriminalJusticeSystemIndicator(DensAsiNonResponseType<bool?> treatmentMandatoryForCriminalJusticeSystemIndicator)
        {
            _treatmentMandatoryForCriminalJusticeSystemIndicator = treatmentMandatoryForCriminalJusticeSystemIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the treatment mandatory for criminal justice system indicator note.
        /// </summary>
        /// <param name="treatmentMandatoryForCriminalJusticeSystemIndicatorNote">The treatment mandatory for criminal justice system indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithTreatmentMandatoryForCriminalJusticeSystemIndicatorNote(string treatmentMandatoryForCriminalJusticeSystemIndicatorNote)
        {
            _treatmentMandatoryForCriminalJusticeSystemIndicatorNote = treatmentMandatoryForCriminalJusticeSystemIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the treatment instead of incarceration in prison indicator.
        /// </summary>
        /// <param name="treatmentInsteadOfIncarcerationInPrisonIndicator">The treatment instead of incarceration in prison indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithTreatmentInsteadOfIncarcerationInPrisonIndicator(DensAsiNonResponseType<bool?> treatmentInsteadOfIncarcerationInPrisonIndicator)
        {
            _treatmentInsteadOfIncarcerationInPrisonIndicator = treatmentInsteadOfIncarcerationInPrisonIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the treatment instead of incarceration in prison indicator note.
        /// </summary>
        /// <param name="treatmentInsteadOfIncarcerationInPrisonIndicatorNote">The treatment instead of incarceration in prison indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSectionBuilder WithTreatmentInsteadOfIncarcerationInPrisonIndicatorNote(string treatmentInsteadOfIncarcerationInPrisonIndicatorNote)
        {
            _treatmentInsteadOfIncarcerationInPrisonIndicatorNote = treatmentInsteadOfIncarcerationInPrisonIndicatorNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiLegalStatusSectionBuilder">A DensAsiLegalStatusSectionBuilder.</see></returns>
        public DensAsiLegalStatusSection Build()
        {
            return new DensAsiLegalStatusSection(
                _admissionPromptedByCriminalJusticeSystemIndicator,
                _admissionPromptedByCriminalJusticeSystemIndicatorNote,
                _probationOrParoleIndicator,
                _probationOrParoleIndicatorNote,
                _arrestedChargedShopliftingCount,
                _arrestedChargedShopliftingCountNote,
                _arrestedChargedProbationParoleViolationCount,
                _arrestedChargedProbationParoleViolationCountNote,
                _arrestedChargedDrugChargesCount,
                _arrestedChargedDrugChargesCountNote,
                _arrestedChargedForgeryCount,
                _arrestedChargedForgeryCountNote,
                _arrestedChargedWeaponsOffenseCount,
                _arrestedChargedWeaponsOffenseCountNote,
                _arrestedChargedBurglaryLarcencyCount,
                _arrestedChargedBurglaryLarcencyCountNote,
                _arrestedChargedRobberyCount,
                _arrestedChargedRobberyCountNote,
                _arrestedChargedAssaultCount,
                _arrestedChargedAssaultCountNote,
                _arrestedChargedArsonCount,
                _arrestedChargedArsonCountNote,
                _arrestedChargedRapeCount,
                _arrestedChargedRapeCountNote,
                _arrestedChargedHomicideManslaughterCount,
                _arrestedChargedHomicideManslaughterCountNote,
                _arrestedChargedProstitutionCount,
                _arrestedChargedProstitutionCountNote,
                _arrestedChargedContemptOfCountCount,
                _arrestedChargedContemptOfCountCountNote,
                _arrestedChargedOtherCount,
                _arrestedChargedOtherDescription,
                _arrestedChargedOtherNote,
                _arrestChargesResultedInConvictionsCount,
                _arrestChargesResultedInConvictionsCountNote,
                _chargedWithDisorderlyConductCount,
                _chargedWithDisorderlyConductCountNote,
                _chargedWithDrivingWhileIntoxicatedCount,
                _chargedWithDrivingWhileIntoxicatedCountNote,
                _chargedWithMajorDrivingViolationsCount,
                _chargedWithMajorDrivingViolationsCountNote,
                _incarcerationInLifeMonthCount,
                _incarcerationInLifeMonthCountNote,
                _lastIncarcerationLengthMonthCount,
                _incarcerationLengthMonthCountNote,
                _incarcerationForDensAsiViolationType,
                _incarcerationForDensAsiViolationTypeNote,
                _presentlyAwaitingChargesIndicator,
                _presentlyAwaitingChargesIndicatorNote,
                _presentlyAwaitingChargesForDensAsiViolationType,
                _presentlyAwaitingChargesForNote,
                _incarceratedInLastThirtyDaysDayCount,
                _incarceratedInLastThirtyDaysDayCountNote,
                _illegalActivityInLastThirtyDaysDayCount,
                _illegalActivityInLastThirtyDaysDayCountNote,
                _seriousnessOfLegalProblemsDensAsiPatientRating,
                _seriousnessOfLegalProblemsDensAsiPatientRatingNote,
                _importanceOfLegalProblemCounselingDensAsiPatientRating,
                _importanceOfLegalProblemCounselingDensAsiPatientRatingNote,
                _patientCounselingDensAsiInterviewerRating,
                _patientCounselingDensAsiInterviewerRatingNote,
                _confidenceDistortedByPatientMisrepresentationIndicator,
                _confidenceDistortedByPatientMisrepresentationIndicatorNote,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                _sectionNote,
                _treatmentMandatoryForCriminalJusticeSystemIndicator,
                _treatmentMandatoryForCriminalJusticeSystemIndicatorNote,
                _treatmentInsteadOfIncarcerationInPrisonIndicator,
                _treatmentInsteadOfIncarcerationInPrisonIndicatorNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiLegalStatusSectionBuilder"/> to <see cref="DensAsiLegalStatusSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiLegalStatusSection(DensAsiLegalStatusSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
