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
    /// Data transfer object for DensAsiPsychiatricStatus class.
    /// </summary>
    public class DensAsiPsychiatricStatusDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private DensAsiNonResponseTypeDto<bool?> _anxietyLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _anxietyLifetimeIndicator;
        private string _anxietyNote;
        private DensAsiNonResponseTypeDto<bool?> _attemptedSuicideLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _attemptedSuicideLifetimeIndicator;
        private string _attemptedSuicideNote;
        private bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _depressionLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _depressionLifetimeIndicator;
        private string _depressionNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _feltNumbLastThirtyDaysDensAsiPatientRating;
        private string _feltNumbLastThirtyDaysDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<bool?> _hallucinationLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _hallucinationLifetimeIndicator;
        private string _hallucinationNote;
        private DensAsiNonResponseTypeDto<bool?> _horribleExperiencesIndicator;
        private string _horribleExperiencesIndicatorNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfPsychologicalProblemCounselingDensAsiPatientRating;
        private string _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _nightmaresLastThirtyDaysDensAsiPatientRating;
        private string _nightmaresLastThirtyDaysDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _onGuardLastThirtyDaysDensAsiPatientRating;
        private string _onGuardLastThirtyDaysDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<bool?> _patientAnxiousIndicator;
        private string _patientAnxiousIndicatorNote;
        private LookupValueDto _patientCounselingDensAsiInterviewerRating;
        private string _patientCounselingDensAsiInterviewerRatingNote;
        private DensAsiNonResponseTypeDto<bool?> _patientDepressedIndicator;
        private string _patientDepressedIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _patientHostileIndicator;
        private string _patientHostileIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _patientParanoidIndicator;
        private string _patientParanoidIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _patientThoughtsOfSuicideIndicator;
        private string _patientThoughtsOfSuicideIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _patientTroubleConcentratingIndicator;
        private string _patientTroubleConcentratingIndicatorNote;
        private DensAsiNonResponseTypeDto<bool?> _prescribedMedicationsLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _prescribedMedicationsLifetimeIndicator;
        private string _prescribedMedicationsNote;
        private DensAsiNonResponseTypeDto<bool?> _psychiatricDisabilityPensionIndicator;
        private string _psychiatricDisabilityPensionIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _psychologicalProblemsLastThirtyDaysDayCount;
        private string _psychologicalProblemsLastThirtyDaysDayCountNote;
        private DensAsiNonResponseTypeDto<int?> _psychologicalTreatmentAsOutpatientCount;
        private string _psychologicalTreatmentAsOutpatientCountNote;
        private DensAsiNonResponseTypeDto<int?> _psychologicalTreatmentInHospitalCount;
        private string _psychologicalTreatmentInHospitalCountNote;
        private string _sectionNote;
        private DensAsiNonResponseTypeDto<bool?> _thoughtsOfSuicideLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _thoughtsOfSuicideLifetimeIndicator;
        private string _thoughtsOfSuicideNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating;
        private string _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<bool?> _troubleConcentratingLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _troubleConcentratingLifetimeIndicator;
        private string _troubleConcentratingNote;
        private DensAsiNonResponseTypeDto<bool?> _troubleControllingRageLastThirtyDaysIndicator;
        private DensAsiNonResponseTypeDto<bool?> _troubleControllingRageLifetimeIndicator;
        private string _troubleControllingRageNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _troubledByPsychologicalProblemsDensAsiPatientRating;
        private string _troubledByPsychologicalProblemsDensAsiPatientRatingNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: P5
        /// </summary>
        /// <value>The anxiety last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AnxietyLastThirtyDaysIndicator
        {
            get { return _anxietyLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _anxietyLastThirtyDaysIndicator, () => AnxietyLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P5
        /// </summary>
        /// <value>The anxiety lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AnxietyLifetimeIndicator
        {
            get { return _anxietyLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _anxietyLifetimeIndicator, () => AnxietyLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P5
        /// </summary>
        /// <value>The anxiety note.</value>
        public string AnxietyNote
        {
            get { return _anxietyNote; }
            set { ApplyPropertyChange ( ref _anxietyNote, () => AnxietyNote, value ); }
        }

        /// <summary>
        /// Question Number: P10
        /// </summary>
        /// <value>The attempted suicide last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AttemptedSuicideLastThirtyDaysIndicator
        {
            get { return _attemptedSuicideLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _attemptedSuicideLastThirtyDaysIndicator, () => AttemptedSuicideLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P10
        /// </summary>
        /// <value>The attempted suicide lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> AttemptedSuicideLifetimeIndicator
        {
            get { return _attemptedSuicideLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _attemptedSuicideLifetimeIndicator, () => AttemptedSuicideLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P10
        /// </summary>
        /// <value>The attempted suicide note.</value>
        public string AttemptedSuicideNote
        {
            get { return _attemptedSuicideNote; }
            set { ApplyPropertyChange ( ref _attemptedSuicideNote, () => AttemptedSuicideNote, value ); }
        }

        /// <summary>
        /// Question Number: P22
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
        /// Question Number: P22
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
        /// Question Number: P23
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
        /// Question Number: P23
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
        /// Question Number: P4
        /// </summary>
        /// <value>The depression last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DepressionLastThirtyDaysIndicator
        {
            get { return _depressionLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _depressionLastThirtyDaysIndicator, () => DepressionLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P4
        /// </summary>
        /// <value>The depression lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> DepressionLifetimeIndicator
        {
            get { return _depressionLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _depressionLifetimeIndicator, () => DepressionLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P4
        /// </summary>
        /// <value>The depression note.</value>
        public string DepressionNote
        {
            get { return _depressionNote; }
            set { ApplyPropertyChange ( ref _depressionNote, () => DepressionNote, value ); }
        }

        /// <summary>
        /// Question Number: P112
        /// </summary>
        /// <value>The felt numb last thirty days dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> FeltNumbLastThirtyDaysDensAsiPatientRating
        {
            get { return _feltNumbLastThirtyDaysDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _feltNumbLastThirtyDaysDensAsiPatientRating, () => FeltNumbLastThirtyDaysDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: P112
        /// </summary>
        /// <value>The felt numb last thirty days dens asi patient rating note.</value>
        public string FeltNumbLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _feltNumbLastThirtyDaysDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _feltNumbLastThirtyDaysDensAsiPatientRatingNote, () => FeltNumbLastThirtyDaysDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: P6
        /// </summary>
        /// <value>The hallucination last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> HallucinationLastThirtyDaysIndicator
        {
            get { return _hallucinationLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _hallucinationLastThirtyDaysIndicator, () => HallucinationLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P6
        /// </summary>
        /// <value>The hallucination lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> HallucinationLifetimeIndicator
        {
            get { return _hallucinationLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _hallucinationLifetimeIndicator, () => HallucinationLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P6
        /// </summary>
        /// <value>The hallucination note.</value>
        public string HallucinationNote
        {
            get { return _hallucinationNote; }
            set { ApplyPropertyChange ( ref _hallucinationNote, () => HallucinationNote, value ); }
        }

        /// <summary>
        /// Question Number: P108
        /// </summary>
        /// <value>The horrible experiences indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> HorribleExperiencesIndicator
        {
            get { return _horribleExperiencesIndicator; }
            set { ApplyPropertyChange ( ref _horribleExperiencesIndicator, () => HorribleExperiencesIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P108
        /// </summary>
        /// <value>The horrible experiences indicator note.</value>
        public string HorribleExperiencesIndicatorNote
        {
            get { return _horribleExperiencesIndicatorNote; }
            set { ApplyPropertyChange ( ref _horribleExperiencesIndicatorNote, () => HorribleExperiencesIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P14
        /// </summary>
        /// <value>The importance of psychological problem counseling dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfPsychologicalProblemCounselingDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfPsychologicalProblemCounselingDensAsiPatientRating,
                    () => ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating,
                    value );
            }
        }

        /// <summary>
        /// Question Number: P14
        /// </summary>
        /// <value>The importance of psychological problem counseling dens asi patient rating note.</value>
        public string ImportanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote,
                    () => ImportanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: P109
        /// </summary>
        /// <value>The nightmares last thirty days dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> NightmaresLastThirtyDaysDensAsiPatientRating
        {
            get { return _nightmaresLastThirtyDaysDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _nightmaresLastThirtyDaysDensAsiPatientRating, () => NightmaresLastThirtyDaysDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: P109
        /// </summary>
        /// <value>The nightmares last thirty days dens asi patient rating note.</value>
        public string NightmaresLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _nightmaresLastThirtyDaysDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _nightmaresLastThirtyDaysDensAsiPatientRatingNote, () => NightmaresLastThirtyDaysDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: P111
        /// </summary>
        /// <value>The on guard last thirty days dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> OnGuardLastThirtyDaysDensAsiPatientRating
        {
            get { return _onGuardLastThirtyDaysDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _onGuardLastThirtyDaysDensAsiPatientRating, () => OnGuardLastThirtyDaysDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: P111
        /// </summary>
        /// <value>The on guard last thirty days dens asi patient rating note.</value>
        public string OnGuardLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _onGuardLastThirtyDaysDensAsiPatientRatingNote; }
            set { ApplyPropertyChange ( ref _onGuardLastThirtyDaysDensAsiPatientRatingNote, () => OnGuardLastThirtyDaysDensAsiPatientRatingNote, value ); }
        }

        /// <summary>
        /// Question Number: P17
        /// </summary>
        /// <value>The patient anxious indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PatientAnxiousIndicator
        {
            get { return _patientAnxiousIndicator; }
            set { ApplyPropertyChange ( ref _patientAnxiousIndicator, () => PatientAnxiousIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P17
        /// </summary>
        /// <value>The patient anxious indicator note.</value>
        public string PatientAnxiousIndicatorNote
        {
            get { return _patientAnxiousIndicatorNote; }
            set { ApplyPropertyChange ( ref _patientAnxiousIndicatorNote, () => PatientAnxiousIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P21
        /// </summary>
        /// <value>The patient counseling dens asi interviewer rating.</value>
        public LookupValueDto PatientCounselingDensAsiInterviewerRating
        {
            get { return _patientCounselingDensAsiInterviewerRating; }
            set { ApplyPropertyChange ( ref _patientCounselingDensAsiInterviewerRating, () => PatientCounselingDensAsiInterviewerRating, value ); }
        }

        /// <summary>
        /// Question Number: P21
        /// </summary>
        /// <value>The patient counseling dens asi interviewer rating note.</value>
        public string PatientCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientCounselingDensAsiInterviewerRatingNote; }
            set { ApplyPropertyChange ( ref _patientCounselingDensAsiInterviewerRatingNote, () => PatientCounselingDensAsiInterviewerRatingNote, value ); }
        }

        /// <summary>
        /// Question Number: P15
        /// </summary>
        /// <value>The patient depressed indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PatientDepressedIndicator
        {
            get { return _patientDepressedIndicator; }
            set { ApplyPropertyChange ( ref _patientDepressedIndicator, () => PatientDepressedIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P15
        /// </summary>
        /// <value>The patient depressed indicator note.</value>
        public string PatientDepressedIndicatorNote
        {
            get { return _patientDepressedIndicatorNote; }
            set { ApplyPropertyChange ( ref _patientDepressedIndicatorNote, () => PatientDepressedIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P16
        /// </summary>
        /// <value>The patient hostile indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PatientHostileIndicator
        {
            get { return _patientHostileIndicator; }
            set { ApplyPropertyChange ( ref _patientHostileIndicator, () => PatientHostileIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P16
        /// </summary>
        /// <value>The patient hostile indicator note.</value>
        public string PatientHostileIndicatorNote
        {
            get { return _patientHostileIndicatorNote; }
            set { ApplyPropertyChange ( ref _patientHostileIndicatorNote, () => PatientHostileIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P18
        /// </summary>
        /// <value>The patient paranoid indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PatientParanoidIndicator
        {
            get { return _patientParanoidIndicator; }
            set { ApplyPropertyChange ( ref _patientParanoidIndicator, () => PatientParanoidIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P18
        /// </summary>
        /// <value>The patient paranoid indicator note.</value>
        public string PatientParanoidIndicatorNote
        {
            get { return _patientParanoidIndicatorNote; }
            set { ApplyPropertyChange ( ref _patientParanoidIndicatorNote, () => PatientParanoidIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P20
        /// </summary>
        /// <value>The patient thoughts of suicide indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PatientThoughtsOfSuicideIndicator
        {
            get { return _patientThoughtsOfSuicideIndicator; }
            set { ApplyPropertyChange ( ref _patientThoughtsOfSuicideIndicator, () => PatientThoughtsOfSuicideIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P20
        /// </summary>
        /// <value>The patient thoughts of suicide indicator note.</value>
        public string PatientThoughtsOfSuicideIndicatorNote
        {
            get { return _patientThoughtsOfSuicideIndicatorNote; }
            set { ApplyPropertyChange ( ref _patientThoughtsOfSuicideIndicatorNote, () => PatientThoughtsOfSuicideIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P19
        /// </summary>
        /// <value>The patient trouble concentrating indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PatientTroubleConcentratingIndicator
        {
            get { return _patientTroubleConcentratingIndicator; }
            set { ApplyPropertyChange ( ref _patientTroubleConcentratingIndicator, () => PatientTroubleConcentratingIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P19
        /// </summary>
        /// <value>The patient trouble concentrating indicator note.</value>
        public string PatientTroubleConcentratingIndicatorNote
        {
            get { return _patientTroubleConcentratingIndicatorNote; }
            set { ApplyPropertyChange ( ref _patientTroubleConcentratingIndicatorNote, () => PatientTroubleConcentratingIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P11
        /// </summary>
        /// <value>The prescribed medications last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PrescribedMedicationsLastThirtyDaysIndicator
        {
            get { return _prescribedMedicationsLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _prescribedMedicationsLastThirtyDaysIndicator, () => PrescribedMedicationsLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P11
        /// </summary>
        /// <value>The prescribed medications lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PrescribedMedicationsLifetimeIndicator
        {
            get { return _prescribedMedicationsLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _prescribedMedicationsLifetimeIndicator, () => PrescribedMedicationsLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P11
        /// </summary>
        /// <value>The prescribed medications note.</value>
        public string PrescribedMedicationsNote
        {
            get { return _prescribedMedicationsNote; }
            set { ApplyPropertyChange ( ref _prescribedMedicationsNote, () => PrescribedMedicationsNote, value ); }
        }

        /// <summary>
        /// Question Number: P3
        /// </summary>
        /// <value>The psychiatric disability pension indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> PsychiatricDisabilityPensionIndicator
        {
            get { return _psychiatricDisabilityPensionIndicator; }
            set { ApplyPropertyChange ( ref _psychiatricDisabilityPensionIndicator, () => PsychiatricDisabilityPensionIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P3
        /// </summary>
        /// <value>The psychiatric disability pension indicator note.</value>
        public string PsychiatricDisabilityPensionIndicatorNote
        {
            get { return _psychiatricDisabilityPensionIndicatorNote; }
            set { ApplyPropertyChange ( ref _psychiatricDisabilityPensionIndicatorNote, () => PsychiatricDisabilityPensionIndicatorNote, value ); }
        }

        /// <summary>
        /// Question Number: P12
        /// </summary>
        /// <value>The psychological problems last thirty days day count.</value>
        public DensAsiNonResponseTypeDto<int?> PsychologicalProblemsLastThirtyDaysDayCount
        {
            get { return _psychologicalProblemsLastThirtyDaysDayCount; }
            set { ApplyPropertyChange ( ref _psychologicalProblemsLastThirtyDaysDayCount, () => PsychologicalProblemsLastThirtyDaysDayCount, value ); }
        }

        /// <summary>
        /// Question Number: P12
        /// </summary>
        /// <value>The psychological problems last thirty days day count note.</value>
        public string PsychologicalProblemsLastThirtyDaysDayCountNote
        {
            get { return _psychologicalProblemsLastThirtyDaysDayCountNote; }
            set
            {
                ApplyPropertyChange (
                    ref _psychologicalProblemsLastThirtyDaysDayCountNote, () => PsychologicalProblemsLastThirtyDaysDayCountNote, value );
            }
        }

        /// <summary>
        /// Question Number: P2
        /// </summary>
        /// <value>The psychological treatment as outpatient count.</value>
        public DensAsiNonResponseTypeDto<int?> PsychologicalTreatmentAsOutpatientCount
        {
            get { return _psychologicalTreatmentAsOutpatientCount; }
            set { ApplyPropertyChange ( ref _psychologicalTreatmentAsOutpatientCount, () => PsychologicalTreatmentAsOutpatientCount, value ); }
        }

        /// <summary>
        /// Question Number: P2
        /// </summary>
        /// <value>The psychological treatment as outpatient count note.</value>
        public string PsychologicalTreatmentAsOutpatientCountNote
        {
            get { return _psychologicalTreatmentAsOutpatientCountNote; }
            set { ApplyPropertyChange ( ref _psychologicalTreatmentAsOutpatientCountNote, () => PsychologicalTreatmentAsOutpatientCountNote, value ); }
        }

        /// <summary>
        /// Question Number: P1
        /// </summary>
        /// <value>The psychological treatment in hospital count.</value>
        public DensAsiNonResponseTypeDto<int?> PsychologicalTreatmentInHospitalCount
        {
            get { return _psychologicalTreatmentInHospitalCount; }
            set { ApplyPropertyChange ( ref _psychologicalTreatmentInHospitalCount, () => PsychologicalTreatmentInHospitalCount, value ); }
        }

        /// <summary>
        /// Question Number: P1
        /// </summary>
        /// <value>The psychological treatment in hospital count note.</value>
        public string PsychologicalTreatmentInHospitalCountNote
        {
            get { return _psychologicalTreatmentInHospitalCountNote; }
            set { ApplyPropertyChange ( ref _psychologicalTreatmentInHospitalCountNote, () => PsychologicalTreatmentInHospitalCountNote, value ); }
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
        /// Question Number: P9
        /// </summary>
        /// <value>The thoughts of suicide last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ThoughtsOfSuicideLastThirtyDaysIndicator
        {
            get { return _thoughtsOfSuicideLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _thoughtsOfSuicideLastThirtyDaysIndicator, () => ThoughtsOfSuicideLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P9
        /// </summary>
        /// <value>The thoughts of suicide lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ThoughtsOfSuicideLifetimeIndicator
        {
            get { return _thoughtsOfSuicideLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _thoughtsOfSuicideLifetimeIndicator, () => ThoughtsOfSuicideLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P9
        /// </summary>
        /// <value>The thoughts of suicide note.</value>
        public string ThoughtsOfSuicideNote
        {
            get { return _thoughtsOfSuicideNote; }
            set { ApplyPropertyChange ( ref _thoughtsOfSuicideNote, () => ThoughtsOfSuicideNote, value ); }
        }

        /// <summary>
        /// Question Number: P110
        /// </summary>
        /// <value>The traumatic event thoughts last thirty days dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating
        {
            get { return _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating,
                    () => TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating,
                    value );
            }
        }

        /// <summary>
        /// Question Number: P110
        /// </summary>
        /// <value>The traumatic event thoughts last thirty days dens asi patient rating note.</value>
        public string TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote,
                    () => TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: P7
        /// </summary>
        /// <value>The trouble concentrating last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> TroubleConcentratingLastThirtyDaysIndicator
        {
            get { return _troubleConcentratingLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _troubleConcentratingLastThirtyDaysIndicator, () => TroubleConcentratingLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P7
        /// </summary>
        /// <value>The trouble concentrating lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> TroubleConcentratingLifetimeIndicator
        {
            get { return _troubleConcentratingLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _troubleConcentratingLifetimeIndicator, () => TroubleConcentratingLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P7
        /// </summary>
        /// <value>The trouble concentrating note.</value>
        public string TroubleConcentratingNote
        {
            get { return _troubleConcentratingNote; }
            set { ApplyPropertyChange ( ref _troubleConcentratingNote, () => TroubleConcentratingNote, value ); }
        }

        /// <summary>
        /// Question Number: P8
        /// </summary>
        /// <value>The trouble controlling rage last thirty days indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> TroubleControllingRageLastThirtyDaysIndicator
        {
            get { return _troubleControllingRageLastThirtyDaysIndicator; }
            set { ApplyPropertyChange ( ref _troubleControllingRageLastThirtyDaysIndicator, () => TroubleControllingRageLastThirtyDaysIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P8
        /// </summary>
        /// <value>The trouble controlling rage lifetime indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> TroubleControllingRageLifetimeIndicator
        {
            get { return _troubleControllingRageLifetimeIndicator; }
            set { ApplyPropertyChange ( ref _troubleControllingRageLifetimeIndicator, () => TroubleControllingRageLifetimeIndicator, value ); }
        }

        /// <summary>
        /// Question Number: P8
        /// </summary>
        /// <value>The trouble controlling rage note.</value>
        public string TroubleControllingRageNote
        {
            get { return _troubleControllingRageNote; }
            set { ApplyPropertyChange ( ref _troubleControllingRageNote, () => TroubleControllingRageNote, value ); }
        }

        /// <summary>
        /// Question Number: P13
        /// </summary>
        /// <value>The troubled by psychological problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TroubledByPsychologicalProblemsDensAsiPatientRating
        {
            get { return _troubledByPsychologicalProblemsDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByPsychologicalProblemsDensAsiPatientRating, () => TroubledByPsychologicalProblemsDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: P13
        /// </summary>
        /// <value>The troubled by psychological problems dens asi patient rating note.</value>
        public string TroubledByPsychologicalProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByPsychologicalProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByPsychologicalProblemsDensAsiPatientRatingNote, () => TroubledByPsychologicalProblemsDensAsiPatientRatingNote, value );
            }
        }

        #endregion
    }
}
