using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiPsychiatricStatusSection contains patient psychiatric status information from the Psychological Information section of the DensAsi. 
    /// <remarks>
    /// Included in each of these sections is the interviewer's severity rating, suggesting the client's need for treatment or additional treatment. 
    /// This is based on the information provided by the client. 
    /// </remarks>
    /// </summary>
    [Component]
    public class DensAsiPsychiatricStatusSection : DensAsiInterviewSectionBase
    {
        private readonly DensAsiNonResponseType<bool?> _anxietyLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _anxietyLifetimeIndicator;
        private readonly string _anxietyNote;
        private readonly DensAsiNonResponseType<bool?> _attemptedSuicideLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _attemptedSuicideLifetimeIndicator;
        private readonly string _attemptedSuicideNote;
        private readonly bool? _confidenceDistortedByPatientMisrepresentationIndicator;
        private readonly string _confidenceDistortedByPatientMisrepresentationIndicatorNote;
        private readonly bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private readonly string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _depressionLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _depressionLifetimeIndicator;
        private readonly string _depressionNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _feltNumbLastThirtyDaysDensAsiPatientRating;
        private readonly string _feltNumbLastThirtyDaysDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<bool?> _hallucinationLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _hallucinationLifetimeIndicator;
        private readonly string _hallucinationNote;
        private readonly DensAsiNonResponseType<bool?> _horribleExperiencesIndicator;
        private readonly string _horribleExperiencesIndicatorNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfPsychologicalProblemCounselingDensAsiPatientRating;
        private readonly string _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _nightmaresLastThirtyDaysDensAsiPatientRating;
        private readonly string _nightmaresLastThirtyDaysDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _onGuardLastThirtyDaysDensAsiPatientRating;
        private readonly string _onGuardLastThirtyDaysDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<bool?> _patientAnxiousIndicator;
        private readonly string _patientAnxiousIndicatorNote;
        private readonly DensAsiInterviewerRating _patientCounselingDensAsiInterviewerRating;
        private readonly string _patientCounselingDensAsiInterviewerRatingNote;
        private readonly DensAsiNonResponseType<bool?> _patientDepressedIndicator;
        private readonly string _patientDepressedIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _patientHostileIndicator;
        private readonly string _patientHostileIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _patientParanoidIndicator;
        private readonly string _patientParanoidIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _patientThoughtsOfSuicideIndicator;
        private readonly string _patientThoughtsOfSuicideIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _patientTroubleConcentratingIndicator;
        private readonly string _patientTroubleConcentratingIndicatorNote;
        private readonly DensAsiNonResponseType<bool?> _prescribedMedicationsLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _prescribedMedicationsLifetimeIndicator;
        private readonly string _prescribedMedicationsNote;
        private readonly DensAsiNonResponseType<bool?> _psychiatricDisabilityPensionIndicator;
        private readonly string _psychiatricDisabilityPensionIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _psychologicalProblemsLastThirtyDaysDayCount;
        private readonly string _psychologicalProblemsLastThirtyDaysDayCountNote;
        private readonly DensAsiNonResponseType<int?> _psychologicalTreatmentAsOutpatientCount;
        private readonly string _psychologicalTreatmentAsOutpatientCountNote;
        private readonly DensAsiNonResponseType<int?> _psychologicalTreatmentInHospitalCount;
        private readonly string _psychologicalTreatmentInHospitalCountNote;
        private readonly string _sectionNote;
        private readonly DensAsiNonResponseType<bool?> _thoughtsOfSuicideLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _thoughtsOfSuicideLifetimeIndicator;
        private readonly string _thoughtsOfSuicideNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating;
        private readonly string _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<bool?> _troubleConcentratingLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _troubleConcentratingLifetimeIndicator;
        private readonly string _troubleConcentratingNote;
        private readonly DensAsiNonResponseType<bool?> _troubleControllingRageLastThirtyDaysIndicator;
        private readonly DensAsiNonResponseType<bool?> _troubleControllingRageLifetimeIndicator;
        private readonly string _troubleControllingRageNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _troubledByPsychologicalProblemsDensAsiPatientRating;
        private readonly string _troubledByPsychologicalProblemsDensAsiPatientRatingNote;

        private DensAsiPsychiatricStatusSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiPsychiatricStatusSection"/> class.
        /// </summary>
        /// <param name="psychologicalTreatmentInHospitalCount">The psychological treatment in hospital count.</param>
        /// <param name="psychologicalTreatmentInHospitalCountNote">The psychological treatment in hospital count note.</param>
        /// <param name="psychologicalTreatmentAsOutpatientCount">The psychological treatment as outpatient count.</param>
        /// <param name="psychologicalTreatmentAsOutpatientCountNote">The psychological treatment as outpatient count note.</param>
        /// <param name="psychiatricDisabilityPensionIndicator">The psychiatric disability pension indicator.</param>
        /// <param name="psychiatricDisabilityPensionIndicatorNote">The psychiatric disability pension indicator note.</param>
        /// <param name="depressionLastThirtyDaysIndicator">The depression last thirty days indicator.</param>
        /// <param name="depressionLifetimeIndicator">The depression lifetime indicator.</param>
        /// <param name="depressionNote">The depression note.</param>
        /// <param name="anxietyLastThirtyDaysIndicator">The anxiety last thirty days indicator.</param>
        /// <param name="anxietyLifetimeIndicator">The anxiety lifetime indicator.</param>
        /// <param name="anxietyNote">The anxiety note.</param>
        /// <param name="hallucinationLastThirtyDaysIndicator">The hallucination last thirty days indicator.</param>
        /// <param name="hallucinationLifetimeIndicator">The hallucination lifetime indicator.</param>
        /// <param name="hallucinationNote">The hallucination note.</param>
        /// <param name="troubleConcentratingLastThirtyDaysIndicator">The trouble concentrating last thirty days indicator.</param>
        /// <param name="troubleConcentratingLifetimeIndicator">The trouble concentrating lifetime indicator.</param>
        /// <param name="troubleConcentratingNote">The trouble concentrating note.</param>
        /// <param name="troubleControllingRageLastThirtyDaysIndicator">The trouble controlling rage last thirty days indicator.</param>
        /// <param name="troubleControllingRageLifetimeIndicator">The trouble controlling rage lifetime indicator.</param>
        /// <param name="troubleControllingRageNote">The trouble controlling rage note.</param>
        /// <param name="thoughtsOfSuicideLastThirtyDaysIndicator">The thoughts of suicide last thirty days indicator.</param>
        /// <param name="thoughtsOfSuicideLifetimeIndicator">The thoughts of suicide lifetime indicator.</param>
        /// <param name="thoughtsOfSuicideNote">The thoughts of suicide note.</param>
        /// <param name="attemptedSuicideLastThirtyDaysIndicator">The attempted suicide last thirty days indicator.</param>
        /// <param name="attemptedSuicideLifetimeIndicator">The attempted suicide lifetime indicator.</param>
        /// <param name="attemptedSuicideNote">The attempted suicide note.</param>
        /// <param name="prescribedMedicationsLastThirtyDaysIndicator">The prescribed medications last thirty days indicator.</param>
        /// <param name="prescribedMedicationsLifetimeIndicator">The prescribed medications lifetime indicator.</param>
        /// <param name="prescribedMedicationsNote">The prescribed medications note.</param>
        /// <param name="psychologicalProblemsLastThirtyDaysDayCount">The psychological problems last thirty days day count.</param>
        /// <param name="psychologicalProblemsLastThirtyDaysDayCountNote">The psychological problems last thirty days day count note.</param>
        /// <param name="troubledByPsychologicalProblemsDensAsiPatientRating">The troubled by psychological problems patient rating.</param>
        /// <param name="troubledByPsychologicalProblemsDensAsiPatientRatingNote">The troubled by psychological problems patient rating note.</param>
        /// <param name="importanceOfPsychologicalProblemCounselingDensAsiPatientRating">The importance of psychological problem counseling patient rating.</param>
        /// <param name="importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote">The importance of psychological problem counseling patient rating note.</param>
        /// <param name="patientDepressedIndicator">The patient depressed indicator.</param>
        /// <param name="patientDepressedIndicatorNote">The patient depressed indicator note.</param>
        /// <param name="patientHostileIndicator">The patient hostile indicator.</param>
        /// <param name="patientHostileIndicatorNote">The patient hostile indicator note.</param>
        /// <param name="patientAnxiousIndicator">The patient anxious indicator.</param>
        /// <param name="patientAnxiousIndicatorNote">The patient anxious indicator note.</param>
        /// <param name="patientParanoidIndicator">The patient paranoid indicator.</param>
        /// <param name="patientParanoidIndicatorNote">The patient paranoid indicator note.</param>
        /// <param name="patientTroubleConcentratingIndicator">The patient trouble concentrating indicator.</param>
        /// <param name="patientTroubleConcentratingIndicatorNote">The patient trouble concentrating indicator note.</param>
        /// <param name="patientThoughtsOfSuicideIndicator">The patient thoughts of suicide indicator.</param>
        /// <param name="patientThoughtsOfSuicideIndicatorNote">The patient thoughts of suicide indicator note.</param>
        /// <param name="patientCounselingDensAsiInterviewerRating">The patient counseling interviewer rating.</param>
        /// <param name="patientCounselingDensAsiInterviewerRatingNote">The patient counseling interviewer rating note.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicator">The confidence distorted by patient misrepresentation indicator.</param>
        /// <param name="confidenceDistortedByPatientMisrepresentationIndicatorNote">The confidence distorted by patient misrepresentation indicator note.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <param name="sectionNote">The section note.</param>
        /// <param name="horribleExperiencesIndicator">The horrible experiences indicator.</param>
        /// <param name="horribleExperiencesIndicatorNote">The horrible experiences indicator note.</param>
        /// <param name="nightmaresLastThirtyDaysDensAsiPatientRating">The nightmares last thirty days patient rating.</param>
        /// <param name="nightmaresLastThirtyDaysDensAsiPatientRatingNote">The nightmares last thirty days patient rating note.</param>
        /// <param name="traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating">The traumatic event thoughts last thirty days patient rating.</param>
        /// <param name="traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote">The traumatic event thoughts last thirty days patient rating note.</param>
        /// <param name="onGuardLastThirtyDaysDensAsiPatientRating">The on guard last thirty days patient rating.</param>
        /// <param name="onGuardLastThirtyDaysDensAsiPatientRatingNote">The on guard last thirty days patient rating note.</param>
        /// <param name="feltNumbLastThirtyDaysDensAsiPatientRating">The felt numb last thirty days patient rating.</param>
        /// <param name="feltNumbLastThirtyDaysDensAsiPatientRatingNote">The felt numb last thirty days patient rating note.</param>
        public DensAsiPsychiatricStatusSection(DensAsiNonResponseType<int?> psychologicalTreatmentInHospitalCount,
                                                   string psychologicalTreatmentInHospitalCountNote,
                                                   DensAsiNonResponseType<int?> psychologicalTreatmentAsOutpatientCount,
                                                   string psychologicalTreatmentAsOutpatientCountNote,
                                                   DensAsiNonResponseType<bool?> psychiatricDisabilityPensionIndicator,
                                                   string psychiatricDisabilityPensionIndicatorNote,
                                                   DensAsiNonResponseType<bool?> depressionLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> depressionLifetimeIndicator,
                                                   string depressionNote,
                                                   DensAsiNonResponseType<bool?> anxietyLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> anxietyLifetimeIndicator,
                                                   string anxietyNote,
                                                   DensAsiNonResponseType<bool?> hallucinationLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> hallucinationLifetimeIndicator,
                                                   string hallucinationNote,
                                                   DensAsiNonResponseType<bool?> troubleConcentratingLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> troubleConcentratingLifetimeIndicator,
                                                   string troubleConcentratingNote,
                                                   DensAsiNonResponseType<bool?> troubleControllingRageLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> troubleControllingRageLifetimeIndicator,
                                                   string troubleControllingRageNote,
                                                   DensAsiNonResponseType<bool?> thoughtsOfSuicideLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> thoughtsOfSuicideLifetimeIndicator,
                                                   string thoughtsOfSuicideNote,
                                                   DensAsiNonResponseType<bool?> attemptedSuicideLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> attemptedSuicideLifetimeIndicator,
                                                   string attemptedSuicideNote,
                                                   DensAsiNonResponseType<bool?> prescribedMedicationsLastThirtyDaysIndicator,
                                                   DensAsiNonResponseType<bool?> prescribedMedicationsLifetimeIndicator,
                                                   string prescribedMedicationsNote,
                                                   DensAsiNonResponseType<int?> psychologicalProblemsLastThirtyDaysDayCount,
                                                   string psychologicalProblemsLastThirtyDaysDayCountNote,
                                                   DensAsiNonResponseType<DensAsiPatientRating> troubledByPsychologicalProblemsDensAsiPatientRating,
                                                   string troubledByPsychologicalProblemsDensAsiPatientRatingNote,
                                                   DensAsiNonResponseType<DensAsiPatientRating> importanceOfPsychologicalProblemCounselingDensAsiPatientRating,
                                                   string importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote,
                                                   DensAsiNonResponseType<bool?> patientDepressedIndicator,
                                                   string patientDepressedIndicatorNote,
                                                   DensAsiNonResponseType<bool?> patientHostileIndicator,
                                                   string patientHostileIndicatorNote,
                                                   DensAsiNonResponseType<bool?> patientAnxiousIndicator,
                                                   string patientAnxiousIndicatorNote,
                                                   DensAsiNonResponseType<bool?> patientParanoidIndicator,
                                                   string patientParanoidIndicatorNote,
                                                   DensAsiNonResponseType<bool?> patientTroubleConcentratingIndicator,
                                                   string patientTroubleConcentratingIndicatorNote,
                                                   DensAsiNonResponseType<bool?> patientThoughtsOfSuicideIndicator,
                                                   string patientThoughtsOfSuicideIndicatorNote,
                                                   DensAsiInterviewerRating patientCounselingDensAsiInterviewerRating,
                                                   string patientCounselingDensAsiInterviewerRatingNote,
                                                   bool? confidenceDistortedByPatientMisrepresentationIndicator,
                                                   string confidenceDistortedByPatientMisrepresentationIndicatorNote,
                                                   bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                                                   string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                                                   string sectionNote,
                                                   DensAsiNonResponseType<bool?> horribleExperiencesIndicator,
                                                   string horribleExperiencesIndicatorNote,
                                                   DensAsiNonResponseType<DensAsiPatientRating> nightmaresLastThirtyDaysDensAsiPatientRating,
                                                   string nightmaresLastThirtyDaysDensAsiPatientRatingNote,
                                                   DensAsiNonResponseType<DensAsiPatientRating> traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating,
                                                   string traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote,
                                                   DensAsiNonResponseType<DensAsiPatientRating> onGuardLastThirtyDaysDensAsiPatientRating,
                                                   string onGuardLastThirtyDaysDensAsiPatientRatingNote,
                                                   DensAsiNonResponseType<DensAsiPatientRating> feltNumbLastThirtyDaysDensAsiPatientRating,
                                                   string feltNumbLastThirtyDaysDensAsiPatientRatingNote )
        {
            if ( psychologicalTreatmentInHospitalCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PsychologicalTreatmentInHospitalCount ).Contains ( psychologicalTreatmentInHospitalCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PsychologicalTreatmentInHospitalCount DensAsiNonResponse value '" + psychologicalTreatmentInHospitalCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( psychologicalTreatmentAsOutpatientCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PsychologicalTreatmentAsOutpatientCount ).Contains ( psychologicalTreatmentAsOutpatientCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PsychologicalTreatmentAsOutpatientCount DensAsiNonResponse value '" + psychologicalTreatmentAsOutpatientCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( psychiatricDisabilityPensionIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PsychiatricDisabilityPensionIndicator ).Contains ( psychiatricDisabilityPensionIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PsychiatricDisabilityPensionIndicator DensAsiNonResponse value '" + psychiatricDisabilityPensionIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( depressionLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DepressionLastThirtyDaysIndicator ).Contains ( depressionLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DepressionLastThirtyDaysIndicator DensAsiNonResponse value '" + depressionLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( depressionLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => DepressionLifetimeIndicator ).Contains ( depressionLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "DepressionLifetimeIndicator DensAsiNonResponse value '" + depressionLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( anxietyLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AnxietyLastThirtyDaysIndicator ).Contains ( anxietyLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AnxietyLastThirtyDaysIndicator DensAsiNonResponse value '" + anxietyLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( anxietyLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AnxietyLifetimeIndicator ).Contains ( anxietyLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AnxietyLifetimeIndicator DensAsiNonResponse value '" + anxietyLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hallucinationLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HallucinationLastThirtyDaysIndicator ).Contains ( hallucinationLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HallucinationLastThirtyDaysIndicator DensAsiNonResponse value '" + hallucinationLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( hallucinationLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HallucinationLifetimeIndicator ).Contains ( hallucinationLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HallucinationLifetimeIndicator DensAsiNonResponse value '" + hallucinationLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubleConcentratingLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubleConcentratingLastThirtyDaysIndicator ).Contains ( troubleConcentratingLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubleConcentratingLastThirtyDaysIndicator DensAsiNonResponse value '" + troubleConcentratingLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubleConcentratingLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubleConcentratingLifetimeIndicator ).Contains ( troubleConcentratingLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubleConcentratingLifetimeIndicator DensAsiNonResponse value '" + troubleConcentratingLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubleControllingRageLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubleControllingRageLastThirtyDaysIndicator ).Contains ( troubleControllingRageLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubleControllingRageLastThirtyDaysIndicator DensAsiNonResponse value '" + troubleControllingRageLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubleControllingRageLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubleControllingRageLifetimeIndicator ).Contains ( troubleControllingRageLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubleControllingRageLifetimeIndicator DensAsiNonResponse value '" + troubleControllingRageLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( thoughtsOfSuicideLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ThoughtsOfSuicideLastThirtyDaysIndicator ).Contains ( thoughtsOfSuicideLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ThoughtsOfSuicideLastThirtyDaysIndicator DensAsiNonResponse value '" + thoughtsOfSuicideLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( thoughtsOfSuicideLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ThoughtsOfSuicideLifetimeIndicator ).Contains ( thoughtsOfSuicideLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ThoughtsOfSuicideLifetimeIndicator DensAsiNonResponse value '" + thoughtsOfSuicideLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( attemptedSuicideLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AttemptedSuicideLastThirtyDaysIndicator ).Contains ( attemptedSuicideLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AttemptedSuicideLastThirtyDaysIndicator DensAsiNonResponse value '" + attemptedSuicideLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( attemptedSuicideLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => AttemptedSuicideLifetimeIndicator ).Contains ( attemptedSuicideLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "AttemptedSuicideLifetimeIndicator DensAsiNonResponse value '" + attemptedSuicideLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( prescribedMedicationsLastThirtyDaysIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PrescribedMedicationsLastThirtyDaysIndicator ).Contains ( prescribedMedicationsLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PrescribedMedicationsLastThirtyDaysIndicator DensAsiNonResponse value '" + prescribedMedicationsLastThirtyDaysIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( prescribedMedicationsLifetimeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PrescribedMedicationsLifetimeIndicator ).Contains ( prescribedMedicationsLifetimeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PrescribedMedicationsLifetimeIndicator DensAsiNonResponse value '" + prescribedMedicationsLifetimeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( psychologicalProblemsLastThirtyDaysDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PsychologicalProblemsLastThirtyDaysDayCount ).Contains ( psychologicalProblemsLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PsychologicalProblemsLastThirtyDaysDayCount DensAsiNonResponse value '" + psychologicalProblemsLastThirtyDaysDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubledByPsychologicalProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubledByPsychologicalProblemsDensAsiPatientRating ).Contains ( troubledByPsychologicalProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubledByPsychologicalProblemsDensAsiPatientRating DensAsiNonResponse value '" + troubledByPsychologicalProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfPsychologicalProblemCounselingDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating ).Contains ( importanceOfPsychologicalProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating DensAsiNonResponse value '" + importanceOfPsychologicalProblemCounselingDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( patientDepressedIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PatientDepressedIndicator ).Contains ( patientDepressedIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PatientDepressedIndicator DensAsiNonResponse value '" + patientDepressedIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( patientHostileIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PatientHostileIndicator ).Contains ( patientHostileIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PatientHostileIndicator DensAsiNonResponse value '" + patientHostileIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( patientAnxiousIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PatientAnxiousIndicator ).Contains ( patientAnxiousIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PatientAnxiousIndicator DensAsiNonResponse value '" + patientAnxiousIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( patientParanoidIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PatientParanoidIndicator ).Contains ( patientParanoidIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PatientParanoidIndicator DensAsiNonResponse value '" + patientParanoidIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( patientTroubleConcentratingIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PatientTroubleConcentratingIndicator ).Contains ( patientTroubleConcentratingIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PatientTroubleConcentratingIndicator DensAsiNonResponse value '" + patientTroubleConcentratingIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( patientThoughtsOfSuicideIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => PatientThoughtsOfSuicideIndicator ).Contains ( patientThoughtsOfSuicideIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "PatientThoughtsOfSuicideIndicator DensAsiNonResponse value '" + patientThoughtsOfSuicideIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( horribleExperiencesIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HorribleExperiencesIndicator ).Contains ( horribleExperiencesIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HorribleExperiencesIndicator DensAsiNonResponse value '" + horribleExperiencesIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( nightmaresLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => NightmaresLastThirtyDaysDensAsiPatientRating ).Contains ( nightmaresLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "NightmaresLastThirtyDaysDensAsiPatientRating DensAsiNonResponse value '" + nightmaresLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating ).Contains ( traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating DensAsiNonResponse value '" + traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( onGuardLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => OnGuardLastThirtyDaysDensAsiPatientRating ).Contains ( onGuardLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "OnGuardLastThirtyDaysDensAsiPatientRating DensAsiNonResponse value '" + onGuardLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( feltNumbLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => FeltNumbLastThirtyDaysDensAsiPatientRating ).Contains ( feltNumbLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "FeltNumbLastThirtyDaysDensAsiPatientRating DensAsiNonResponse value '" + feltNumbLastThirtyDaysDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _psychologicalTreatmentInHospitalCount = psychologicalTreatmentInHospitalCount;
            _psychologicalTreatmentInHospitalCountNote = psychologicalTreatmentInHospitalCountNote;
            _psychologicalTreatmentAsOutpatientCount = psychologicalTreatmentAsOutpatientCount;
            _psychologicalTreatmentAsOutpatientCountNote = psychologicalTreatmentAsOutpatientCountNote;
            _psychiatricDisabilityPensionIndicator = psychiatricDisabilityPensionIndicator;
            _psychiatricDisabilityPensionIndicatorNote = psychiatricDisabilityPensionIndicatorNote;
            _depressionLastThirtyDaysIndicator = depressionLastThirtyDaysIndicator;
            _depressionLifetimeIndicator = depressionLifetimeIndicator;
            _depressionNote = depressionNote;
            _anxietyLastThirtyDaysIndicator = anxietyLastThirtyDaysIndicator;
            _anxietyLifetimeIndicator = anxietyLifetimeIndicator;
            _anxietyNote = anxietyNote;
            _hallucinationLastThirtyDaysIndicator = hallucinationLastThirtyDaysIndicator;
            _hallucinationLifetimeIndicator = hallucinationLifetimeIndicator;
            _hallucinationNote = hallucinationNote;
            _troubleConcentratingLastThirtyDaysIndicator = troubleConcentratingLastThirtyDaysIndicator;
            _troubleConcentratingLifetimeIndicator = troubleConcentratingLifetimeIndicator;
            _troubleConcentratingNote = troubleConcentratingNote;
            _troubleControllingRageLastThirtyDaysIndicator = troubleControllingRageLastThirtyDaysIndicator;
            _troubleControllingRageLifetimeIndicator = troubleControllingRageLifetimeIndicator;
            _troubleControllingRageNote = troubleControllingRageNote;
            _thoughtsOfSuicideLastThirtyDaysIndicator = thoughtsOfSuicideLastThirtyDaysIndicator;
            _thoughtsOfSuicideLifetimeIndicator = thoughtsOfSuicideLifetimeIndicator;
            _thoughtsOfSuicideNote = thoughtsOfSuicideNote;
            _attemptedSuicideLastThirtyDaysIndicator = attemptedSuicideLastThirtyDaysIndicator;
            _attemptedSuicideLifetimeIndicator = attemptedSuicideLifetimeIndicator;
            _attemptedSuicideNote = attemptedSuicideNote;
            _prescribedMedicationsLastThirtyDaysIndicator = prescribedMedicationsLastThirtyDaysIndicator;
            _prescribedMedicationsLifetimeIndicator = prescribedMedicationsLifetimeIndicator;
            _prescribedMedicationsNote = prescribedMedicationsNote;
            _psychologicalProblemsLastThirtyDaysDayCount = psychologicalProblemsLastThirtyDaysDayCount;
            _psychologicalProblemsLastThirtyDaysDayCountNote = psychologicalProblemsLastThirtyDaysDayCountNote;
            _troubledByPsychologicalProblemsDensAsiPatientRating = troubledByPsychologicalProblemsDensAsiPatientRating;
            _troubledByPsychologicalProblemsDensAsiPatientRatingNote = troubledByPsychologicalProblemsDensAsiPatientRatingNote;
            _importanceOfPsychologicalProblemCounselingDensAsiPatientRating = importanceOfPsychologicalProblemCounselingDensAsiPatientRating;
            _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote = importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote;
            _patientDepressedIndicator = patientDepressedIndicator;
            _patientDepressedIndicatorNote = patientDepressedIndicatorNote;
            _patientHostileIndicator = patientHostileIndicator;
            _patientHostileIndicatorNote = patientHostileIndicatorNote;
            _patientAnxiousIndicator = patientAnxiousIndicator;
            _patientAnxiousIndicatorNote = patientAnxiousIndicatorNote;
            _patientParanoidIndicator = patientParanoidIndicator;
            _patientParanoidIndicatorNote = patientParanoidIndicatorNote;
            _patientTroubleConcentratingIndicator = patientTroubleConcentratingIndicator;
            _patientTroubleConcentratingIndicatorNote = patientTroubleConcentratingIndicatorNote;
            _patientThoughtsOfSuicideIndicator = patientThoughtsOfSuicideIndicator;
            _patientThoughtsOfSuicideIndicatorNote = patientThoughtsOfSuicideIndicatorNote;
            _patientCounselingDensAsiInterviewerRating = patientCounselingDensAsiInterviewerRating;
            _patientCounselingDensAsiInterviewerRatingNote = patientCounselingDensAsiInterviewerRatingNote;
            _confidenceDistortedByPatientMisrepresentationIndicator = confidenceDistortedByPatientMisrepresentationIndicator;
            _confidenceDistortedByPatientMisrepresentationIndicatorNote = confidenceDistortedByPatientMisrepresentationIndicatorNote;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            _sectionNote = sectionNote;
            _horribleExperiencesIndicator = horribleExperiencesIndicator;
            _horribleExperiencesIndicatorNote = horribleExperiencesIndicatorNote;
            _nightmaresLastThirtyDaysDensAsiPatientRating = nightmaresLastThirtyDaysDensAsiPatientRating;
            _nightmaresLastThirtyDaysDensAsiPatientRatingNote = nightmaresLastThirtyDaysDensAsiPatientRatingNote;
            _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating = traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating;
            _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote = traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote;
            _onGuardLastThirtyDaysDensAsiPatientRating = onGuardLastThirtyDaysDensAsiPatientRating;
            _onGuardLastThirtyDaysDensAsiPatientRatingNote = onGuardLastThirtyDaysDensAsiPatientRatingNote;
            _feltNumbLastThirtyDaysDensAsiPatientRating = feltNumbLastThirtyDaysDensAsiPatientRating;
            _feltNumbLastThirtyDaysDensAsiPatientRatingNote = feltNumbLastThirtyDaysDensAsiPatientRatingNote;
        }

        /// <summary>
        /// Gets the number of times psychological treatment in hospital.
        /// Question Number: P1
        /// </summary>
        public virtual DensAsiNonResponseType<int?> PsychologicalTreatmentInHospitalCount
        {
            get { return _psychologicalTreatmentInHospitalCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of times psychological treatment in hospital note.
        /// Question Number: P1
        /// </summary>
        public virtual string PsychologicalTreatmentInHospitalCountNote
        {
            get { return _psychologicalTreatmentInHospitalCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets number of times psychological treatment as outpatient.
        /// Question Number: P2
        /// </summary>
        public virtual DensAsiNonResponseType<int?> PsychologicalTreatmentAsOutpatientCount
        {
            get { return _psychologicalTreatmentAsOutpatientCount; }
            private set { }
        }

        /// <summary>
        /// Gets number of times psychological treatment as outpatient note.
        /// Question Number: P2
        /// </summary>
        public virtual string PsychologicalTreatmentAsOutpatientCountNote
        {
            get { return _psychologicalTreatmentAsOutpatientCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating receipt of psychiatric disability pension.
        /// Question Number: P3
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PsychiatricDisabilityPensionIndicator
        {
            get { return _psychiatricDisabilityPensionIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating receipt of psychiatric disability pension note.
        /// Question Number: P3
        /// </summary>
        public virtual string PsychiatricDisabilityPensionIndicatorNote
        {
            get { return _psychiatricDisabilityPensionIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating depression in last thirty days.
        /// Question Number: P4
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DepressionLastThirtyDaysIndicator
        {
            get { return _depressionLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating depression in last thirty days note.
        /// Question Number: P4
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> DepressionLifetimeIndicator
        {
            get { return _depressionLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the depression note.
        /// Question Number: P4
        /// </summary>
        public virtual string DepressionNote
        {
            get { return _depressionNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating anxiety in last thirty days.
        /// Question Number: P5
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AnxietyLastThirtyDaysIndicator
        {
            get { return _anxietyLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating anxiety in lifetime.
        /// Question Number: P5
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AnxietyLifetimeIndicator
        {
            get { return _anxietyLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the anxiety note.
        /// Question Number: P5
        /// </summary>
        public virtual string AnxietyNote
        {
            get { return _anxietyNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating hallucination in last thirty days.
        /// Question Number: P6
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> HallucinationLastThirtyDaysIndicator
        {
            get { return _hallucinationLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating hallucination in lifetime.
        /// Question Number: P6
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> HallucinationLifetimeIndicator
        {
            get { return _hallucinationLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the hallucination note.
        /// Question Number: P6
        /// </summary>
        public virtual string HallucinationNote
        {
            get { return _hallucinationNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating trouble concentrating in last thirty days.
        /// Question Number: P7
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> TroubleConcentratingLastThirtyDaysIndicator
        {
            get { return _troubleConcentratingLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating trouble concentrating in lifetime.
        /// Question Number: P7
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> TroubleConcentratingLifetimeIndicator
        {
            get { return _troubleConcentratingLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the trouble concentrating note.
        /// Question Number: P7
        /// </summary>
        public virtual string TroubleConcentratingNote
        {
            get { return _troubleConcentratingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating trouble controlling rage last thirty days.
        /// Question Number: P8
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> TroubleControllingRageLastThirtyDaysIndicator
        {
            get { return _troubleControllingRageLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating trouble controlling rage in lifetime.
        /// Question Number: P8
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> TroubleControllingRageLifetimeIndicator
        {
            get { return _troubleControllingRageLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the trouble controlling rage note.
        /// Question Number: P8
        /// </summary>
        public virtual string TroubleControllingRageNote
        {
            get { return _troubleControllingRageNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating thoughts of suicide last thirty days.
        /// Question Number: P9
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ThoughtsOfSuicideLastThirtyDaysIndicator
        {
            get { return _thoughtsOfSuicideLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating thoughts of suicide in lifetime.
        /// Question Number: P9
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ThoughtsOfSuicideLifetimeIndicator
        {
            get { return _thoughtsOfSuicideLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating thoughts of suicide in lifetime note.
        /// Question Number: P9
        /// </summary>
        public virtual string ThoughtsOfSuicideNote
        {
            get { return _thoughtsOfSuicideNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating attempted suicide last thirty days.
        /// Question Number: P10
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AttemptedSuicideLastThirtyDaysIndicator
        {
            get { return _attemptedSuicideLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating attempted suicide lifetime.
        /// Question Number: P10
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> AttemptedSuicideLifetimeIndicator
        {
            get { return _attemptedSuicideLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the attempted suicide note.
        /// Question Number: P10
        /// </summary>
        public virtual string AttemptedSuicideNote
        {
            get { return _attemptedSuicideNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating prescribed medications last thirty days.
        /// Question Number: P11
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PrescribedMedicationsLastThirtyDaysIndicator
        {
            get { return _prescribedMedicationsLastThirtyDaysIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating prescribed medications lifetime.
        /// Question Number: P11
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PrescribedMedicationsLifetimeIndicator
        {
            get { return _prescribedMedicationsLifetimeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the prescribed medications note.
        /// Question Number: P11
        /// </summary>
        public virtual string PrescribedMedicationsNote
        {
            get { return _prescribedMedicationsNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in last thirty days psychological problems.
        /// Question Number: P12
        /// </summary>
        public virtual DensAsiNonResponseType<int?> PsychologicalProblemsLastThirtyDaysDayCount
        {
            get { return _psychologicalProblemsLastThirtyDaysDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days in last thirty days psychological problems note.
        /// Question Number: P12
        /// </summary>
        public virtual string PsychologicalProblemsLastThirtyDaysDayCountNote
        {
            get { return _psychologicalProblemsLastThirtyDaysDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledByPsychologicalProblemsDensAsiPatientRating</see>
        /// denoting how troubled the patient has been by psychological problems in the last thirty days. Question Number: P13
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TroubledByPsychologicalProblemsDensAsiPatientRating
        {
            get { return _troubledByPsychologicalProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the how troubled the patient has been by psychological problems in the last thirty days note.
        /// Question Number: P13
        /// </summary>
        public virtual string TroubledByPsychologicalProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByPsychologicalProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating</see>
        /// denoting the importance of psychological problem counseling in the last thirty days. Question Number: P14
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfPsychologicalProblemCounselingDensAsiPatientRating
        {
            get { return _importanceOfPsychologicalProblemCounselingDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the importance of psychological problem counseling in the last thirty days note.
        /// Question Number: P14
        /// </summary>
        public virtual string ImportanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote
        {
            get { return _importanceOfPsychologicalProblemCounselingDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is depressed.
        /// Question Number: P15
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PatientDepressedIndicator
        {
            get { return _patientDepressedIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is depressed note.
        /// Question Number: P15
        /// </summary>
        public virtual string PatientDepressedIndicatorNote
        {
            get { return _patientDepressedIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is hostile.
        /// Question Number: P16
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PatientHostileIndicator
        {
            get { return _patientHostileIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is hostile note.
        /// Question Number: P16
        /// </summary>
        public virtual string PatientHostileIndicatorNote
        {
            get { return _patientHostileIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is anxious.
        /// Question Number: P17
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PatientAnxiousIndicator
        {
            get { return _patientAnxiousIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is anxious note.
        /// Question Number: P17
        /// </summary>
        public virtual string PatientAnxiousIndicatorNote
        {
            get { return _patientAnxiousIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is paranoid.
        /// Question Number: P18
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PatientParanoidIndicator
        {
            get { return _patientParanoidIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient is paranoid note.
        /// Question Number: P18
        /// </summary>
        public virtual string PatientParanoidIndicatorNote
        {
            get { return _patientParanoidIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient has trouble concentrating.
        /// Question Number: P19
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PatientTroubleConcentratingIndicator
        {
            get { return _patientTroubleConcentratingIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient has trouble concentrating note.
        /// Question Number: P19
        /// </summary>
        public virtual string PatientTroubleConcentratingIndicatorNote
        {
            get { return _patientTroubleConcentratingIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient has thoughts of suicide.
        /// Question Number: P20
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> PatientThoughtsOfSuicideIndicator
        {
            get { return _patientThoughtsOfSuicideIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating if the patient has thoughts of suicide note.
        /// Question Number: P20
        /// </summary>
        public virtual string PatientThoughtsOfSuicideIndicatorNote
        {
            get { return _patientThoughtsOfSuicideIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">PatientCounselingDensAsiInterviewerRating</see> interviewer 
        /// rating denoting that the patient requires employment counseling.
        /// Question Number: P21
        /// </summary>
        public virtual DensAsiInterviewerRating PatientCounselingDensAsiInterviewerRating
        {
            get { return _patientCounselingDensAsiInterviewerRating; }
            private set { }
        }

        /// <summary>
        /// Gets the interviewer rating denoting that the patient requires employment counseling note.
        /// Question Number: P21
        /// </summary>
        public virtual string PatientCounselingDensAsiInterviewerRatingNote
        {
            get { return _patientCounselingDensAsiInterviewerRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation.
        /// Question Number: P22
        /// </summary>
        public virtual bool? ConfidenceDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient misrepresentation note.
        /// Question Number: P22
        /// </summary>
        public virtual string ConfidenceDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceDistortedByPatientMisrepresentationIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand.
        /// Question Number: P23
        /// </summary>
        public virtual bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that information was distorted by patient inability to understand note.
        /// Question Number: P23
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
        /// Gets a boolean value indicating horrible experiences.
        /// Question Number: P108
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> HorribleExperiencesIndicator
        {
            get { return _horribleExperiencesIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating horrible experiences note.
        /// Question Number: P108
        /// </summary>
        public virtual string HorribleExperiencesIndicatorNote
        {
            get { return _horribleExperiencesIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">NightmaresLastThirtyDaysDensAsiPatientRating</see> 
        /// denoting that the patient has experienced nightmares in the last thirty days.
        /// Question Number: P109
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> NightmaresLastThirtyDaysDensAsiPatientRating
        {
            get { return _nightmaresLastThirtyDaysDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the patient has experienced nightmares in the last thirty days note.
        /// Question Number: P109
        /// </summary>
        public virtual string NightmaresLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _nightmaresLastThirtyDaysDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating</see> 
        /// denoting that the patient has tried not to think of a traumatic event in the last thirty days.
        /// Question Number: P110
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRating
        {
            get { return _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the patient has tried not to think of a traumatic event in the last thirty days note.
        /// Question Number: P110
        /// </summary>
        public virtual string TraumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _traumaticEventThoughtsLastThirtyDaysDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">OnGuardLastThirtyDaysDensAsiPatientRating</see> 
        /// denoting that the patient has felt on guard in the last thirty days.
        /// Question Number: P111
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> OnGuardLastThirtyDaysDensAsiPatientRating
        {
            get { return _onGuardLastThirtyDaysDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Get the patient has felt on guard note.
        /// Question Number: P111
        /// </summary>
        public virtual string OnGuardLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _onGuardLastThirtyDaysDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">OnGuardLastThirtyDaysDensAsiPatientRating</see> 
        /// denoting that the patient has felt numb in the last thirty days.
        /// Question Number: P112
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> FeltNumbLastThirtyDaysDensAsiPatientRating
        {
            get { return _feltNumbLastThirtyDaysDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the patient has felt numb in the last thirty days note.
        /// Question Number: P112
        /// </summary>
        public virtual string FeltNumbLastThirtyDaysDensAsiPatientRatingNote
        {
            get { return _feltNumbLastThirtyDaysDensAsiPatientRatingNote; }
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

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => HorribleExperiencesIndicator ) )
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
                           { PropertyUtil.ExtractPropertyName ( () => HorribleExperiencesIndicator ), GetPossibleDensAsiNonResponseWellKnownNames ( () => HorribleExperiencesIndicator ) }
                       };
        }
    }
}