using System;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiMedicalStatusSectionBuilder provides a fluent interface for creating a Medical Status section.
    /// </summary>
    public class DensAsiMedicalStatusSectionBuilder
    {
        private DensAsiNonResponseType<int?> _hopitalizedForMedicalProblemsCount;
        private string _hopitalizedForMedicalProblemsCountNote;
        private DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan;
        private string _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote;
        private DensAsiNonResponseType<bool?> _chronicMedicalProblemThatInterferesWithLifeIndicator;
        private string _chronicMedicalProblemThatInterferesWithLifeDescription;
        private string _chronicMedicalProblemThatInterferesWithLifeNote;
        private DensAsiNonResponseType<bool?> _takingPrescribedMedicationsForPhysicalProblemIndicator;
        private string _takingPrescribedMedicationsForPhysicalProblemDescription;
        private string _takingPrescribedMedicationsForPhysicalProblemNote;
        private DensAsiNonResponseType<bool?> _receivePensionForPhysicalDisabilityIndicator;
        private string _receivePensionForPhysicalDisabilityDescription;
        private string _receivePensionForPhysicalDisabilityNote;
        private DensAsiNonResponseType<int?> _medicalProblemsDayCount;
        private string _medicalProblemsDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _troubledByMedicalProblemsDensAsiPatientRating;
        private string _troubledByMedicalProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfMedicalProblemTreatmentDensAsiPatientRating;
        private string _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote;
        private DensAsiInterviewerRating _patientTreatmentDensAsiInterviewerRating;
        private string _patientTreatmentDensAsiInterviewerRatingNote;
        private bool? _confidenceRateDistortedByPatientMisrepresentationIndicator;
        private string _confidenceRateDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private string _sectionNote;


        /// <summary>
        /// Assigns the hopitalized for medical problems count.
        /// </summary>
        /// <param name="hopitalizedForMedicalProblemsCount">The hopitalized for medical problems count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithHopitalizedForMedicalProblemsCount(DensAsiNonResponseType<int?> hopitalizedForMedicalProblemsCount)
        {
            _hopitalizedForMedicalProblemsCount = hopitalizedForMedicalProblemsCount;
            return this;
        }

        /// <summary>
        /// Assigns the hopitalized for medical problems count note.
        /// </summary>
        /// <param name="hopitalizedForMedicalProblemsCountNote">The hopitalized for medical problems count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithHopitalizedForMedicalProblemsCountNote(string hopitalizedForMedicalProblemsCountNote)
        {
            _hopitalizedForMedicalProblemsCountNote = hopitalizedForMedicalProblemsCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the years and months after last hospitalization for physical problem time span.
        /// </summary>
        /// <param name="yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan">The years and months after last hospitalization for physical problem time span.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithYearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan(DensAsiNonResponseType<TimeSpan?> yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan)
        {
            _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan = yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan;
            return this;
        }

        /// <summary>
        /// Assigns the years and months after last hospitalization for physical problem time span note.
        /// </summary>
        /// <param name="yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote">The years and months after last hospitalization for physical problem time span note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithYearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote(string yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote)
        {
            _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote = yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote;
            return this;
        }

        /// <summary>
        /// Assigns the chronic medical problem that interferes with life indicator.
        /// </summary>
        /// <param name="chronicMedicalProblemThatInterferesWithLifeIndicator">The chronic medical problem that interferes with life indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithChronicMedicalProblemThatInterferesWithLifeIndicator(DensAsiNonResponseType<bool?> chronicMedicalProblemThatInterferesWithLifeIndicator)
        {
            _chronicMedicalProblemThatInterferesWithLifeIndicator = chronicMedicalProblemThatInterferesWithLifeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the chronic medical problem that interferes with life description.
        /// </summary>
        /// <param name="chronicMedicalProblemThatInterferesWithLifeDescription">The chronic medical problem that interferes with life description.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithChronicMedicalProblemThatInterferesWithLifeDescription(string chronicMedicalProblemThatInterferesWithLifeDescription)
        {
            _chronicMedicalProblemThatInterferesWithLifeDescription = chronicMedicalProblemThatInterferesWithLifeDescription;
            return this;
        }

        /// <summary>
        /// Assigns the chronic medical problem that interferes with life note.
        /// </summary>
        /// <param name="chronicMedicalProblemThatInterferesWithLifeNote">The chronic medical problem that interferes with life note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithChronicMedicalProblemThatInterferesWithLifeNote(string chronicMedicalProblemThatInterferesWithLifeNote)
        {
            _chronicMedicalProblemThatInterferesWithLifeNote = chronicMedicalProblemThatInterferesWithLifeNote;
            return this;
        }

        /// <summary>
        /// Assigns the taking prescribed medications for physical problem indicator.
        /// </summary>
        /// <param name="takingPrescribedMedicationsForPhysicalProblemIndicator">The taking prescribed medications for physical problem indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithTakingPrescribedMedicationsForPhysicalProblemIndicator(DensAsiNonResponseType<bool?> takingPrescribedMedicationsForPhysicalProblemIndicator)
        {
            _takingPrescribedMedicationsForPhysicalProblemIndicator = takingPrescribedMedicationsForPhysicalProblemIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the taking prescribed medications for physical problem description.
        /// </summary>
        /// <param name="takingPrescribedMedicationsForPhysicalProblemDescription">The taking prescribed medications for physical problem description.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithTakingPrescribedMedicationsForPhysicalProblemDescription(string takingPrescribedMedicationsForPhysicalProblemDescription)
        {
            _takingPrescribedMedicationsForPhysicalProblemDescription = takingPrescribedMedicationsForPhysicalProblemDescription;
            return this;
        }

        /// <summary>
        /// Assigns the taking prescribed medications for physical problem note.
        /// </summary>
        /// <param name="takingPrescribedMedicationsForPhysicalProblemNote">The taking prescribed medications for physical problem note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithTakingPrescribedMedicationsForPhysicalProblemNote(string takingPrescribedMedicationsForPhysicalProblemNote)
        {
            _takingPrescribedMedicationsForPhysicalProblemNote = takingPrescribedMedicationsForPhysicalProblemNote;
            return this;
        }

        /// <summary>
        /// Assigns the receive pension for physical disability indicator.
        /// </summary>
        /// <param name="receivePensionForPhysicalDisabilityIndicator">The receive pension for physical disability indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithReceivePensionForPhysicalDisabilityIndicator(DensAsiNonResponseType<bool?> receivePensionForPhysicalDisabilityIndicator)
        {
            _receivePensionForPhysicalDisabilityIndicator = receivePensionForPhysicalDisabilityIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the receive pension for physical disability description.
        /// </summary>
        /// <param name="receivePensionForPhysicalDisabilityDescription">The receive pension for physical disability description.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithReceivePensionForPhysicalDisabilityDescription(string receivePensionForPhysicalDisabilityDescription)
        {
            _receivePensionForPhysicalDisabilityDescription = receivePensionForPhysicalDisabilityDescription;
            return this;
        }

        /// <summary>
        /// Assigns the receive pension for physical disability note.
        /// </summary>
        /// <param name="receivePensionForPhysicalDisabilityNote">The receive pension for physical disability note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithReceivePensionForPhysicalDisabilityNote(string receivePensionForPhysicalDisabilityNote)
        {
            _receivePensionForPhysicalDisabilityNote = receivePensionForPhysicalDisabilityNote;
            return this;
        }

        /// <summary>
        /// Assigns the medical problems day count.
        /// </summary>
        /// <param name="medicalProblemsDayCount">The medical problems day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithMedicalProblemsDayCount(DensAsiNonResponseType<int?> medicalProblemsDayCount)
        {
            _medicalProblemsDayCount = medicalProblemsDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the medical problems day count note.
        /// </summary>
        /// <param name="medicalProblemsDayCountNote">The medical problems day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithMedicalProblemsDayCountNote(string medicalProblemsDayCountNote)
        {
            _medicalProblemsDayCountNote = medicalProblemsDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by medical problems DensAsi patient rating.
        /// </summary>
        /// <param name="troubledByMedicalProblemsDensAsiPatientRating">The troubled by medical problems DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithTroubledByMedicalProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> troubledByMedicalProblemsDensAsiPatientRating)
        {
            _troubledByMedicalProblemsDensAsiPatientRating = troubledByMedicalProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by medical problems DensAsi patient rating note.
        /// </summary>
        /// <param name="troubledByMedicalProblemsDensAsiPatientRatingNote">The troubled by medical problems DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithTroubledByMedicalProblemsDensAsiPatientRatingNote(string troubledByMedicalProblemsDensAsiPatientRatingNote)
        {
            _troubledByMedicalProblemsDensAsiPatientRatingNote = troubledByMedicalProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of medical problem treatment DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfMedicalProblemTreatmentDensAsiPatientRating">The importance of medical problem treatment DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithImportanceOfMedicalProblemTreatmentDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfMedicalProblemTreatmentDensAsiPatientRating)
        {
            _importanceOfMedicalProblemTreatmentDensAsiPatientRating = importanceOfMedicalProblemTreatmentDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of medical problem treatment DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote">The importance of medical problem treatment DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithImportanceOfMedicalProblemTreatmentDensAsiPatientRatingNote(string importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote)
        {
            _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote = importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient treatment DensAsi interviewer rating.
        /// </summary>
        /// <param name="patientTreatmentDensAsiInterviewerRating">The patient treatment DensAsi interviewer rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithPatientTreatmentDensAsiInterviewerRating(DensAsiInterviewerRating patientTreatmentDensAsiInterviewerRating)
        {
            _patientTreatmentDensAsiInterviewerRating = patientTreatmentDensAsiInterviewerRating;
            return this;
        }

        /// <summary>
        /// Assigns the patient treatment DensAsi interviewer rating note.
        /// </summary>
        /// <param name="patientTreatmentDensAsiInterviewerRatingNote">The patient treatment DensAsi interviewer rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithPatientTreatmentDensAsiInterviewerRatingNote(string patientTreatmentDensAsiInterviewerRatingNote)
        {
            _patientTreatmentDensAsiInterviewerRatingNote = patientTreatmentDensAsiInterviewerRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient misrepresentation indicator.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientMisrepresentationIndicator">The confidence rate distorted by patient misrepresentation indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithConfidenceRateDistortedByPatientMisrepresentationIndicator(bool? confidenceRateDistortedByPatientMisrepresentationIndicator)
        {
            _confidenceRateDistortedByPatientMisrepresentationIndicator = confidenceRateDistortedByPatientMisrepresentationIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient misrepresentation indicator note.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientMisrepresentationIndicatorNote">The confidence rate distorted by patient misrepresentation indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithConfidenceRateDistortedByPatientMisrepresentationIndicatorNote(string confidenceRateDistortedByPatientMisrepresentationIndicatorNote)
        {
            _confidenceRateDistortedByPatientMisrepresentationIndicatorNote = confidenceRateDistortedByPatientMisrepresentationIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator(bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator note.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote(string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the section note.
        /// </summary>
        /// <param name="sectionNote">The section note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSectionBuilder WithSectionNote(string sectionNote)
        {
            _sectionNote = sectionNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiMedicalStatusSectionBuilder">A DensAsiMedicalStatusSectionBuilder.</see></returns>
        public DensAsiMedicalStatusSection Build()
        {
            return new DensAsiMedicalStatusSection(
                _hopitalizedForMedicalProblemsCount,
                _hopitalizedForMedicalProblemsCountNote,
                _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan,
                _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote,
                _chronicMedicalProblemThatInterferesWithLifeIndicator,
                _chronicMedicalProblemThatInterferesWithLifeDescription,
                _chronicMedicalProblemThatInterferesWithLifeNote,
                _takingPrescribedMedicationsForPhysicalProblemIndicator,
                _takingPrescribedMedicationsForPhysicalProblemDescription,
                _takingPrescribedMedicationsForPhysicalProblemNote,
                _receivePensionForPhysicalDisabilityIndicator,
                _receivePensionForPhysicalDisabilityDescription,
                _receivePensionForPhysicalDisabilityNote,
                _medicalProblemsDayCount,
                _medicalProblemsDayCountNote,
                _troubledByMedicalProblemsDensAsiPatientRating,
                _troubledByMedicalProblemsDensAsiPatientRatingNote,
                _importanceOfMedicalProblemTreatmentDensAsiPatientRating,
                _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote,
                _patientTreatmentDensAsiInterviewerRating,
                _patientTreatmentDensAsiInterviewerRatingNote,
                _confidenceRateDistortedByPatientMisrepresentationIndicator,
                _confidenceRateDistortedByPatientMisrepresentationIndicatorNote,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                _sectionNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiMedicalStatusSectionBuilder"/> to <see cref="DensAsiMedicalStatusSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiMedicalStatusSection(DensAsiMedicalStatusSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
