namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// DensAsiPsychiatricStatusSectionBuilder provides a fluent interface for creating a Psychiatric Status section.
    /// </summary>
    public class DensAsiPsychiatricStatusSectionBuilder
    {
        private DensAsiNonResponseType<int?> _psychologicalTreatmentInHospitalCount;
        private string _psychologicalTreatmentInHospitalCountNote;
        private DensAsiNonResponseType<int?> _psychologicalTreatmentAsOutpatientCount;
        private string _psychologicalTreatmentAsOutpatientCountNote;
        private DensAsiNonResponseType<bool?> _psychiatricDisabilityPensionIndicator;
        private string _psychiatricDisabilityPensionIndicatorNote;
        private DensAsiNonResponseType<bool?> _depressionLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _depressionLifetimeIndicator;
        private string _depressionNote;
        private DensAsiNonResponseType<bool?> _anxietyLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _anxietyLifetimeIndicator;
        private string _anxietyNote;
        private DensAsiNonResponseType<bool?> _hallucinationLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _hallucinationLifetimeIndicator;
        private string _hallucinationNote;
        private DensAsiNonResponseType<bool?> _troubleConcentratingLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _troubleConcentratingLifetimeIndicator;
        private string _troubleConcentratingNote;
        private DensAsiNonResponseType<bool?> _troubleControllingRageLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _troubleControllingRageLifetimeIndicator;
        private string _troubleControllingRageNote;
        private DensAsiNonResponseType<bool?> _thoughtsOfSuicideLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _thoughtsOfSuicideLifetimeIndicator;
        private string _thoughtsOfSuicideNote;
        private DensAsiNonResponseType<bool?> _attemptedSuicideLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _attemptedSuicideLifetimeIndicator;
        private string _attemptedSuicideNote;
        private DensAsiNonResponseType<bool?> _prescribedMedicationsLastThirtyDaysIndicator;
        private DensAsiNonResponseType<bool?> _prescribedMedicationsLifetimeIndicator;
        private string _prescribedMedicationsNote;
        private DensAsiNonResponseType<int?> _psychologicalProblemsLastThirtyDaysDayCount;
        private string _psychologicalProblemsLastThirtyDaysDayCountNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _troubledByPsychologicalProblemsDensAsiPatientRating;
        private string _troubledByPsychologicalProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _importanceOfPsychologicalProblemCounselingDensAsiPatientRating;
        private string _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote;
        private DensAsiNonResponseType<bool?> _patientDepressedIndicator;
        private string _patientDepressedIndicatorNote;
        private DensAsiNonResponseType<bool?> _patientHostileIndicator;
        private string _patientHostileIndicatorNote;
        private DensAsiNonResponseType<bool?> _patientAnxiousIndicator;
        private string _patientAnxiousIndicatorNote;
        private DensAsiNonResponseType<bool?> _patientParanoidIndicator;
        private string _patientParanoidIndicatorNote;
        private DensAsiNonResponseType<bool?> _patientTroubleConcentratingIndicator;
        private string _patientTroubleConcentratingIndicatorNote;
        private DensAsiNonResponseType<bool?> _patientThoughtsOfSuicideIndicator;
        private string _patientThoughtsOfSuicideIndicatorNote;
        private DensAsiInterviewerRating _patientCounselingDensAsiInterviewerRating;
        private string _patientCounselingDensAsiInterviewerRatingNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private string _sectionNote;
        private DensAsiNonResponseType<bool?> _horribleExperiencesIndicator;
        private string _horribleExperiencesIndicatorNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _nightmaresLastThirtyDaysDensAsiPatientRating;
        private string _nightmaresLastThirtyDaysDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating;
        private string _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _onGuardLastThirtyDaysDensAsiPatientRating;
        private string _onGuardLastThirtyDaysDensAsiPatientRatingNote;
        private DensAsiNonResponseType<DensAsiPatientRating> _feltNumbLastThirtyDaysDensAsiPatientRating;
        private string _feltNumbLastThirtyDaysDensAsiPatientRatingNote;


        /// <summary>
        /// Assigns the psychological treatment in hospital count.
        /// </summary>
        /// <param name="psychologicalTreatmentInHospitalCount">The psychological treatment in hospital count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychologicalTreatmentInHospitalCount(DensAsiNonResponseType<int?> psychologicalTreatmentInHospitalCount)
        {
            _psychologicalTreatmentInHospitalCount = psychologicalTreatmentInHospitalCount;
            return this;
        }

        /// <summary>
        /// Assigns the psychological treatment in hospital count note.
        /// </summary>
        /// <param name="psychologicalTreatmentInHospitalCountNote">The psychological treatment in hospital count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychologicalTreatmentInHospitalCountNote(string psychologicalTreatmentInHospitalCountNote)
        {
            _psychologicalTreatmentInHospitalCountNote = psychologicalTreatmentInHospitalCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the psychological treatment as outpatient count.
        /// </summary>
        /// <param name="psychologicalTreatmentAsOutpatientCount">The psychological treatment as outpatient count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychologicalTreatmentAsOutpatientCount(DensAsiNonResponseType<int?> psychologicalTreatmentAsOutpatientCount)
        {
            _psychologicalTreatmentAsOutpatientCount = psychologicalTreatmentAsOutpatientCount;
            return this;
        }

        /// <summary>
        /// Assigns the psychological treatment as outpatient count note.
        /// </summary>
        /// <param name="psychologicalTreatmentAsOutpatientCountNote">The psychological treatment as outpatient count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychologicalTreatmentAsOutpatientCountNote(string psychologicalTreatmentAsOutpatientCountNote)
        {
            _psychologicalTreatmentAsOutpatientCountNote = psychologicalTreatmentAsOutpatientCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the psychiatric disability pension indicator.
        /// </summary>
        /// <param name="psychiatricDisabilityPensionIndicator">The psychiatric disability pension indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychiatricDisabilityPensionIndicator(DensAsiNonResponseType<bool?> psychiatricDisabilityPensionIndicator)
        {
            _psychiatricDisabilityPensionIndicator = psychiatricDisabilityPensionIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the psychiatric disability pension indicator note.
        /// </summary>
        /// <param name="psychiatricDisabilityPensionIndicatorNote">The psychiatric disability pension indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychiatricDisabilityPensionIndicatorNote(string psychiatricDisabilityPensionIndicatorNote)
        {
            _psychiatricDisabilityPensionIndicatorNote = psychiatricDisabilityPensionIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the depression last thirty days indicator.
        /// </summary>
        /// <param name="depressionLastThirtyDaysIndicator">The depression last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithDepressionLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> depressionLastThirtyDaysIndicator)
        {
            _depressionLastThirtyDaysIndicator = depressionLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the depression lifetime indicator.
        /// </summary>
        /// <param name="depressionLifetimeIndicator">The depression lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithDepressionLifetimeIndicator(DensAsiNonResponseType<bool?> depressionLifetimeIndicator)
        {
            _depressionLifetimeIndicator = depressionLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the depression note.
        /// </summary>
        /// <param name="depressionNote">The depression note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithDepressionNote(string depressionNote)
        {
            _depressionNote = depressionNote;
            return this;
        }

        /// <summary>
        /// Assigns the anxiety last thirty days indicator.
        /// </summary>
        /// <param name="anxietyLastThirtyDaysIndicator">The anxiety last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithAnxietyLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> anxietyLastThirtyDaysIndicator)
        {
            _anxietyLastThirtyDaysIndicator = anxietyLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the anxiety lifetime indicator.
        /// </summary>
        /// <param name="anxietyLifetimeIndicator">The anxiety lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithAnxietyLifetimeIndicator(DensAsiNonResponseType<bool?> anxietyLifetimeIndicator)
        {
            _anxietyLifetimeIndicator = anxietyLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the anxiety note.
        /// </summary>
        /// <param name="anxietyNote">The anxiety note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithAnxietyNote(string anxietyNote)
        {
            _anxietyNote = anxietyNote;
            return this;
        }

        /// <summary>
        /// Assigns the hallucination last thirty days indicator.
        /// </summary>
        /// <param name="hallucinationLastThirtyDaysIndicator">The hallucination last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithHallucinationLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> hallucinationLastThirtyDaysIndicator)
        {
            _hallucinationLastThirtyDaysIndicator = hallucinationLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the hallucination lifetime indicator.
        /// </summary>
        /// <param name="hallucinationLifetimeIndicator">The hallucination lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithHallucinationLifetimeIndicator(DensAsiNonResponseType<bool?> hallucinationLifetimeIndicator)
        {
            _hallucinationLifetimeIndicator = hallucinationLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the hallucination note.
        /// </summary>
        /// <param name="hallucinationNote">The hallucination note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithHallucinationNote(string hallucinationNote)
        {
            _hallucinationNote = hallucinationNote;
            return this;
        }

        /// <summary>
        /// Assigns the trouble concentrating last thirty days indicator.
        /// </summary>
        /// <param name="troubleConcentratingLastThirtyDaysIndicator">The trouble concentrating last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubleConcentratingLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> troubleConcentratingLastThirtyDaysIndicator)
        {
            _troubleConcentratingLastThirtyDaysIndicator = troubleConcentratingLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the trouble concentrating lifetime indicator.
        /// </summary>
        /// <param name="troubleConcentratingLifetimeIndicator">The trouble concentrating lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubleConcentratingLifetimeIndicator(DensAsiNonResponseType<bool?> troubleConcentratingLifetimeIndicator)
        {
            _troubleConcentratingLifetimeIndicator = troubleConcentratingLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the trouble concentrating note.
        /// </summary>
        /// <param name="troubleConcentratingNote">The trouble concentrating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubleConcentratingNote(string troubleConcentratingNote)
        {
            _troubleConcentratingNote = troubleConcentratingNote;
            return this;
        }

        /// <summary>
        /// Assigns the trouble controlling rage last thirty days indicator.
        /// </summary>
        /// <param name="troubleControllingRageLastThirtyDaysIndicator">The trouble controlling rage last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubleControllingRageLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> troubleControllingRageLastThirtyDaysIndicator)
        {
            _troubleControllingRageLastThirtyDaysIndicator = troubleControllingRageLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the trouble controlling rage lifetime indicator.
        /// </summary>
        /// <param name="troubleControllingRageLifetimeIndicator">The trouble controlling rage lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubleControllingRageLifetimeIndicator(DensAsiNonResponseType<bool?> troubleControllingRageLifetimeIndicator)
        {
            _troubleControllingRageLifetimeIndicator = troubleControllingRageLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the trouble controlling rage note.
        /// </summary>
        /// <param name="troubleControllingRageNote">The trouble controlling rage note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubleControllingRageNote(string troubleControllingRageNote)
        {
            _troubleControllingRageNote = troubleControllingRageNote;
            return this;
        }

        /// <summary>
        /// Assigns the thoughts of suicide last thirty days indicator.
        /// </summary>
        /// <param name="thoughtsOfSuicideLastThirtyDaysIndicator">The thoughts of suicide last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithThoughtsOfSuicideLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> thoughtsOfSuicideLastThirtyDaysIndicator)
        {
            _thoughtsOfSuicideLastThirtyDaysIndicator = thoughtsOfSuicideLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the thoughts of suicide lifetime indicator.
        /// </summary>
        /// <param name="thoughtsOfSuicideLifetimeIndicator">The thoughts of suicide lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithThoughtsOfSuicideLifetimeIndicator(DensAsiNonResponseType<bool?> thoughtsOfSuicideLifetimeIndicator)
        {
            _thoughtsOfSuicideLifetimeIndicator = thoughtsOfSuicideLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the thoughts of suicide note.
        /// </summary>
        /// <param name="thoughtsOfSuicideNote">The thoughts of suicide note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithThoughtsOfSuicideNote(string thoughtsOfSuicideNote)
        {
            _thoughtsOfSuicideNote = thoughtsOfSuicideNote;
            return this;
        }

        /// <summary>
        /// Assigns the attempted suicide last thirty days indicator.
        /// </summary>
        /// <param name="attemptedSuicideLastThirtyDaysIndicator">The attempted suicide last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithAttemptedSuicideLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> attemptedSuicideLastThirtyDaysIndicator)
        {
            _attemptedSuicideLastThirtyDaysIndicator = attemptedSuicideLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the attempted suicide lifetime indicator.
        /// </summary>
        /// <param name="attemptedSuicideLifetimeIndicator">The attempted suicide lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithAttemptedSuicideLifetimeIndicator(DensAsiNonResponseType<bool?> attemptedSuicideLifetimeIndicator)
        {
            _attemptedSuicideLifetimeIndicator = attemptedSuicideLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the attempted suicide note.
        /// </summary>
        /// <param name="attemptedSuicideNote">The attempted suicide note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithAttemptedSuicideNote(string attemptedSuicideNote)
        {
            _attemptedSuicideNote = attemptedSuicideNote;
            return this;
        }

        /// <summary>
        /// Assigns the prescribed medications last thirty days indicator.
        /// </summary>
        /// <param name="prescribedMedicationsLastThirtyDaysIndicator">The prescribed medications last thirty days indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPrescribedMedicationsLastThirtyDaysIndicator(DensAsiNonResponseType<bool?> prescribedMedicationsLastThirtyDaysIndicator)
        {
            _prescribedMedicationsLastThirtyDaysIndicator = prescribedMedicationsLastThirtyDaysIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the prescribed medications lifetime indicator.
        /// </summary>
        /// <param name="prescribedMedicationsLifetimeIndicator">The prescribed medications lifetime indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPrescribedMedicationsLifetimeIndicator(DensAsiNonResponseType<bool?> prescribedMedicationsLifetimeIndicator)
        {
            _prescribedMedicationsLifetimeIndicator = prescribedMedicationsLifetimeIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the prescribed medications note.
        /// </summary>
        /// <param name="prescribedMedicationsNote">The prescribed medications note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPrescribedMedicationsNote(string prescribedMedicationsNote)
        {
            _prescribedMedicationsNote = prescribedMedicationsNote;
            return this;
        }

        /// <summary>
        /// Assigns the psychological problems last thirty days day count.
        /// </summary>
        /// <param name="psychologicalProblemsLastThirtyDaysDayCount">The psychological problems last thirty days day count.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychologicalProblemsLastThirtyDaysDayCount(DensAsiNonResponseType<int?> psychologicalProblemsLastThirtyDaysDayCount)
        {
            _psychologicalProblemsLastThirtyDaysDayCount = psychologicalProblemsLastThirtyDaysDayCount;
            return this;
        }

        /// <summary>
        /// Assigns the psychological problems last thirty days day count note.
        /// </summary>
        /// <param name="psychologicalProblemsLastThirtyDaysDayCountNote">The psychological problems last thirty days day count note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPsychologicalProblemsLastThirtyDaysDayCountNote(string psychologicalProblemsLastThirtyDaysDayCountNote)
        {
            _psychologicalProblemsLastThirtyDaysDayCountNote = psychologicalProblemsLastThirtyDaysDayCountNote;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by psychological problems DensAsi patient rating.
        /// </summary>
        /// <param name="troubledByPsychologicalProblemsDensAsiPatientRating">The troubled by psychological problems DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubledByPsychologicalProblemsDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> troubledByPsychologicalProblemsDensAsiPatientRating)
        {
            _troubledByPsychologicalProblemsDensAsiPatientRating = troubledByPsychologicalProblemsDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the troubled by psychological problems DensAsi patient rating note.
        /// </summary>
        /// <param name="troubledByPsychologicalProblemsDensAsiPatientRatingNote">The troubled by psychological problems DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTroubledByPsychologicalProblemsDensAsiPatientRatingNote(string troubledByPsychologicalProblemsDensAsiPatientRatingNote)
        {
            _troubledByPsychologicalProblemsDensAsiPatientRatingNote = troubledByPsychologicalProblemsDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the importance of psychological problem counseling DensAsi patient rating.
        /// </summary>
        /// <param name="importanceOfPsychologicalProblemCounselingDensAsiPatientRating">The importance of psychological problem counseling DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithImportanceOfPsychologicalProblemCounselingDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> importanceOfPsychologicalProblemCounselingDensAsiPatientRating)
        {
            _importanceOfPsychologicalProblemCounselingDensAsiPatientRating = importanceOfPsychologicalProblemCounselingDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the importance of psychological problem counseling DensAsi patient rating note.
        /// </summary>
        /// <param name="importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote">The importance of psychological problem counseling DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithImportanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote(string importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote)
        {
            _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote = importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient depressed indicator.
        /// </summary>
        /// <param name="patientDepressedIndicator">The patient depressed indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientDepressedIndicator(DensAsiNonResponseType<bool?> patientDepressedIndicator)
        {
            _patientDepressedIndicator = patientDepressedIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the patient depressed indicator note.
        /// </summary>
        /// <param name="patientDepressedIndicatorNote">The patient depressed indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientDepressedIndicatorNote(string patientDepressedIndicatorNote)
        {
            _patientDepressedIndicatorNote = patientDepressedIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient hostile indicator.
        /// </summary>
        /// <param name="patientHostileIndicator">The patient hostile indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientHostileIndicator(DensAsiNonResponseType<bool?> patientHostileIndicator)
        {
            _patientHostileIndicator = patientHostileIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the patient hostile indicator note.
        /// </summary>
        /// <param name="patientHostileIndicatorNote">The patient hostile indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientHostileIndicatorNote(string patientHostileIndicatorNote)
        {
            _patientHostileIndicatorNote = patientHostileIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient anxious indicator.
        /// </summary>
        /// <param name="patientAnxiousIndicator">The patient anxious indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientAnxiousIndicator(DensAsiNonResponseType<bool?> patientAnxiousIndicator)
        {
            _patientAnxiousIndicator = patientAnxiousIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the patient anxious indicator note.
        /// </summary>
        /// <param name="patientAnxiousIndicatorNote">The patient anxious indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientAnxiousIndicatorNote(string patientAnxiousIndicatorNote)
        {
            _patientAnxiousIndicatorNote = patientAnxiousIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient paranoid indicator.
        /// </summary>
        /// <param name="patientParanoidIndicator">The patient paranoid indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientParanoidIndicator(DensAsiNonResponseType<bool?> patientParanoidIndicator)
        {
            _patientParanoidIndicator = patientParanoidIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the patient paranoid indicator note.
        /// </summary>
        /// <param name="patientParanoidIndicatorNote">The patient paranoid indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientParanoidIndicatorNote(string patientParanoidIndicatorNote)
        {
            _patientParanoidIndicatorNote = patientParanoidIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient trouble concentrating indicator.
        /// </summary>
        /// <param name="patientTroubleConcentratingIndicator">The patient trouble concentrating indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientTroubleConcentratingIndicator(DensAsiNonResponseType<bool?> patientTroubleConcentratingIndicator)
        {
            _patientTroubleConcentratingIndicator = patientTroubleConcentratingIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the patient trouble concentrating indicator note.
        /// </summary>
        /// <param name="patientTroubleConcentratingIndicatorNote">The patient trouble concentrating indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientTroubleConcentratingIndicatorNote(string patientTroubleConcentratingIndicatorNote)
        {
            _patientTroubleConcentratingIndicatorNote = patientTroubleConcentratingIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient thoughts of suicide indicator.
        /// </summary>
        /// <param name="patientThoughtsOfSuicideIndicator">The patient thoughts of suicide indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientThoughtsOfSuicideIndicator(DensAsiNonResponseType<bool?> patientThoughtsOfSuicideIndicator)
        {
            _patientThoughtsOfSuicideIndicator = patientThoughtsOfSuicideIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the patient thoughts of suicide indicator note.
        /// </summary>
        /// <param name="patientThoughtsOfSuicideIndicatorNote">The patient thoughts of suicide indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientThoughtsOfSuicideIndicatorNote(string patientThoughtsOfSuicideIndicatorNote)
        {
            _patientThoughtsOfSuicideIndicatorNote = patientThoughtsOfSuicideIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the patient counseling DensAsi interviewer rating.
        /// </summary>
        /// <param name="patientCounselingDensAsiInterviewerRating">The patient counseling DensAsi interviewer rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientCounselingDensAsiInterviewerRating(DensAsiInterviewerRating patientCounselingDensAsiInterviewerRating)
        {
            _patientCounselingDensAsiInterviewerRating = patientCounselingDensAsiInterviewerRating;
            return this;
        }

        /// <summary>
        /// Assigns the patient counseling DensAsi interviewer rating note.
        /// </summary>
        /// <param name="patientCounselingDensAsiInterviewerRatingNote">The patient counseling DensAsi interviewer rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithPatientCounselingDensAsiInterviewerRatingNote(string patientCounselingDensAsiInterviewerRatingNote)
        {
            _patientCounselingDensAsiInterviewerRatingNote = patientCounselingDensAsiInterviewerRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicator(bool? confidenceDistortedByPatientMisrepresentationIndicator)
        {
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence distorted by patient misrepresentation indicator note.
        /// </summary>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithConfidenceDistortedByPatientMisrepresentationIndicatorNote(string confidenceDistortedByPatientMisrepresentationIndicatorNote)
        {
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicator(bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the confidence rate distorted by patient inability to understand indicator note.
        /// </summary>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote(string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote)
        {
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the section note.
        /// </summary>
        /// <param name="sectionNote">The section note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithSectionNote(string sectionNote)
        {
            _sectionNote = sectionNote;
            return this;
        }

        /// <summary>
        /// Assigns the horrible experiences indicator.
        /// </summary>
        /// <param name="horribleExperiencesIndicator">The horrible experiences indicator.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithHorribleExperiencesIndicator(DensAsiNonResponseType<bool?> horribleExperiencesIndicator)
        {
            _horribleExperiencesIndicator = horribleExperiencesIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the horrible experiences indicator note.
        /// </summary>
        /// <param name="horribleExperiencesIndicatorNote">The horrible experiences indicator note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithHorribleExperiencesIndicatorNote(string horribleExperiencesIndicatorNote)
        {
            _horribleExperiencesIndicatorNote = horribleExperiencesIndicatorNote;
            return this;
        }

        /// <summary>
        /// Assigns the nightmares last thirty days DensAsi patient rating.
        /// </summary>
        /// <param name="nightmaresLastThirtyDaysDensAsiPatientRating">The nightmares last thirty days DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithNightmaresLastThirtyDaysDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> nightmaresLastThirtyDaysDensAsiPatientRating)
        {
            _nightmaresLastThirtyDaysDensAsiPatientRating = nightmaresLastThirtyDaysDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the nightmares last thirty days DensAsi patient rating note.
        /// </summary>
        /// <param name="nightmaresLastThirtyDaysDensAsiPatientRatingNote">The nightmares last thirty days DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithNightmaresLastThirtyDaysDensAsiPatientRatingNote(string nightmaresLastThirtyDaysDensAsiPatientRatingNote)
        {
            _nightmaresLastThirtyDaysDensAsiPatientRatingNote = nightmaresLastThirtyDaysDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the traumatic event thoughts last thirty days DensAsi patient rating.
        /// </summary>
        /// <param name="traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating">The traumatic event thoughts last thirty days DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating)
        {
            _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating = traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the traumatic event thoughts last thirty days DensAsi patient rating note.
        /// </summary>
        /// <param name="traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote">The traumatic event thoughts last thirty days DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithTraumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote(string traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote)
        {
            _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote = traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the on guard last thirty days DensAsi patient rating.
        /// </summary>
        /// <param name="onGuardLastThirtyDaysDensAsiPatientRating">The on guard last thirty days DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithOnGuardLastThirtyDaysDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> onGuardLastThirtyDaysDensAsiPatientRating)
        {
            _onGuardLastThirtyDaysDensAsiPatientRating = onGuardLastThirtyDaysDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the on guard last thirty days DensAsi patient rating note.
        /// </summary>
        /// <param name="onGuardLastThirtyDaysDensAsiPatientRatingNote">The on guard last thirty days DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithOnGuardLastThirtyDaysDensAsiPatientRatingNote(string onGuardLastThirtyDaysDensAsiPatientRatingNote)
        {
            _onGuardLastThirtyDaysDensAsiPatientRatingNote = onGuardLastThirtyDaysDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Assigns the felt numb last thirty days DensAsi patient rating.
        /// </summary>
        /// <param name="feltNumbLastThirtyDaysDensAsiPatientRating">The felt numb last thirty days DensAsi patient rating.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithFeltNumbLastThirtyDaysDensAsiPatientRating(DensAsiNonResponseType<DensAsiPatientRating> feltNumbLastThirtyDaysDensAsiPatientRating)
        {
            _feltNumbLastThirtyDaysDensAsiPatientRating = feltNumbLastThirtyDaysDensAsiPatientRating;
            return this;
        }

        /// <summary>
        /// Assigns the felt numb last thirty days DensAsi patient rating note.
        /// </summary>
        /// <param name="feltNumbLastThirtyDaysDensAsiPatientRatingNote">The felt numb last thirty days DensAsi patient rating note.</param>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSectionBuilder WithFeltNumbLastThirtyDaysDensAsiPatientRatingNote(string feltNumbLastThirtyDaysDensAsiPatientRatingNote)
        {
            _feltNumbLastThirtyDaysDensAsiPatientRatingNote = feltNumbLastThirtyDaysDensAsiPatientRatingNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns><see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPsychiatricStatusSectionBuilder">A DensAsiPsychiatricStatusSectionBuilder.</see></returns>
        public DensAsiPsychiatricStatusSection Build()
        {
            return new DensAsiPsychiatricStatusSection(
                _psychologicalTreatmentInHospitalCount,
                _psychologicalTreatmentInHospitalCountNote,
                _psychologicalTreatmentAsOutpatientCount,
                _psychologicalTreatmentAsOutpatientCountNote,
                _psychiatricDisabilityPensionIndicator,
                _psychiatricDisabilityPensionIndicatorNote,
                _depressionLastThirtyDaysIndicator,
                _depressionLifetimeIndicator,
                _depressionNote,
                _anxietyLastThirtyDaysIndicator,
                _anxietyLifetimeIndicator,
                _anxietyNote,
                _hallucinationLastThirtyDaysIndicator,
                _hallucinationLifetimeIndicator,
                _hallucinationNote,
                _troubleConcentratingLastThirtyDaysIndicator,
                _troubleConcentratingLifetimeIndicator,
                _troubleConcentratingNote,
                _troubleControllingRageLastThirtyDaysIndicator,
                _troubleControllingRageLifetimeIndicator,
                _troubleControllingRageNote,
                _thoughtsOfSuicideLastThirtyDaysIndicator,
                _thoughtsOfSuicideLifetimeIndicator,
                _thoughtsOfSuicideNote,
                _attemptedSuicideLastThirtyDaysIndicator,
                _attemptedSuicideLifetimeIndicator,
                _attemptedSuicideNote,
                _prescribedMedicationsLastThirtyDaysIndicator,
                _prescribedMedicationsLifetimeIndicator,
                _prescribedMedicationsNote,
                _psychologicalProblemsLastThirtyDaysDayCount,
                _psychologicalProblemsLastThirtyDaysDayCountNote,
                _troubledByPsychologicalProblemsDensAsiPatientRating,
                _troubledByPsychologicalProblemsDensAsiPatientRatingNote,
                _importanceOfPsychologicalProblemCounselingDensAsiPatientRating,
                _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote,
                _patientDepressedIndicator,
                _patientDepressedIndicatorNote,
                _patientHostileIndicator,
                _patientHostileIndicatorNote,
                _patientAnxiousIndicator,
                _patientAnxiousIndicatorNote,
                _patientParanoidIndicator,
                _patientParanoidIndicatorNote,
                _patientTroubleConcentratingIndicator,
                _patientTroubleConcentratingIndicatorNote,
                _patientThoughtsOfSuicideIndicator,
                _patientThoughtsOfSuicideIndicatorNote,
                _patientCounselingDensAsiInterviewerRating,
                _patientCounselingDensAsiInterviewerRatingNote,
                _confidenceDistortedByPatientMisrepresentationIndicator,
                _confidenceDistortedByPatientMisrepresentationIndicatorNote,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                _sectionNote,
                _horribleExperiencesIndicator,
                _horribleExperiencesIndicatorNote,
                _nightmaresLastThirtyDaysDensAsiPatientRating,
                _nightmaresLastThirtyDaysDensAsiPatientRatingNote,
                _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating,
                _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote,
                _onGuardLastThirtyDaysDensAsiPatientRating,
                _onGuardLastThirtyDaysDensAsiPatientRatingNote,
                _feltNumbLastThirtyDaysDensAsiPatientRating,
                _feltNumbLastThirtyDaysDensAsiPatientRatingNote
                );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="DensAsiPsychiatricStatusSectionBuilder"/> to <see cref="DensAsiPsychiatricStatusSection"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator DensAsiPsychiatricStatusSection(DensAsiPsychiatricStatusSectionBuilder builder)
        {
            return builder.Build();
        }
    }
}
