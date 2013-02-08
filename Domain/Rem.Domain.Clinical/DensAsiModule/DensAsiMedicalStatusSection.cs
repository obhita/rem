using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// The DensAsiMedicalStatusSection contains patient medical status information from the Medical Information section of the DensAsi. 
    /// <remarks>
    /// Included in each of these sections is the interviewer's severity rating, suggesting the client's need for treatment or additional treatment. 
    /// This is based on the information provided by the client. 
    /// </remarks>
    /// </summary>
    [Component]
    public class DensAsiMedicalStatusSection : DensAsiInterviewSectionBase
    {
        private readonly string _chronicMedicalProblemThatInterferesWithLifeDescription;
        private readonly DensAsiNonResponseType<bool?> _chronicMedicalProblemThatInterferesWithLifeIndicator;
        private readonly string _chronicMedicalProblemThatInterferesWithLifeNote;
        private readonly bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private readonly string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private readonly bool? _confidenceRateDistortedByPatientMisrepresentationIndicator;
        private readonly string _confidenceRateDistortedByPatientMisrepresentationIndicatorNote;
        private readonly DensAsiNonResponseType<int?> _hopitalizedForMedicalProblemsCount;
        private readonly string _hopitalizedForMedicalProblemsCountNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _importanceOfMedicalProblemTreatmentDensAsiPatientRating;
        private readonly string _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<int?> _medicalProblemsDayCount;
        private readonly string _medicalProblemsDayCountNote;
        private readonly DensAsiInterviewerRating _patientTreatmentDensAsiInterviewerRating;
        private readonly string _patientTreatmentDensAsiInterviewerRatingNote;
        private readonly string _receivePensionForPhysicalDisabilityDescription;
        private readonly DensAsiNonResponseType<bool?> _receivePensionForPhysicalDisabilityIndicator;
        private readonly string _receivePensionForPhysicalDisabilityNote;
        private readonly string _sectionNote;
        private readonly string _takingPrescribedMedicationsForPhysicalProblemDescription;
        private readonly DensAsiNonResponseType<bool?> _takingPrescribedMedicationsForPhysicalProblemIndicator;
        private readonly string _takingPrescribedMedicationsForPhysicalProblemNote;
        private readonly DensAsiNonResponseType<DensAsiPatientRating> _troubledByMedicalProblemsDensAsiPatientRating;
        private readonly string _troubledByMedicalProblemsDensAsiPatientRatingNote;
        private readonly DensAsiNonResponseType<TimeSpan?> _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan;
        private readonly string _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote;

        private DensAsiMedicalStatusSection ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DensAsiMedicalStatusSection"/> class.
        /// </summary>
        /// <param name="hopitalizedForMedicalProblemsCount">The hopitalized for medical problems count.</param>
        /// <param name="hopitalizedForMedicalProblemsCountNote">The hopitalized for medical problems count note.</param>
        /// <param name="yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan">The years and months after last hospitalization for physical problem time span.</param>
        /// <param name="yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote">The years and months after last hospitalization for physical problem time span note.</param>
        /// <param name="chronicMedicalProblemThatInterferesWithLifeIndicator">The chronic medical problem that interferes with life indicator.</param>
        /// <param name="chronicMedicalProblemThatInterferesWithLifeDescription">The chronic medical problem that interferes with life description.</param>
        /// <param name="chronicMedicalProblemThatInterferesWithLifeNote">The chronic medical problem that interferes with life note.</param>
        /// <param name="takingPrescribedMedicationsForPhysicalProblemIndicator">The taking prescribed medications for physical problem indicator.</param>
        /// <param name="takingPrescribedMedicationsForPhysicalProblemDescription">The taking prescribed medications for physical problem description.</param>
        /// <param name="takingPrescribedMedicationsForPhysicalProblemNote">The taking prescribed medications for physical problem note.</param>
        /// <param name="receivePensionForPhysicalDisabilityIndicator">The receive pension for physical disability indicator.</param>
        /// <param name="receivePensionForPhysicalDisabilityDescription">The receive pension for physical disability description.</param>
        /// <param name="receivePensionForPhysicalDisabilityNote">The receive pension for physical disability note.</param>
        /// <param name="medicalProblemsDayCount">The medical problems day count.</param>
        /// <param name="medicalProblemsDayCountNote">The medical problems day count note.</param>
        /// <param name="troubledByMedicalProblemsDensAsiPatientRating">The troubled by medical problems dens asi patient rating.</param>
        /// <param name="troubledByMedicalProblemsDensAsiPatientRatingNote">The troubled by medical problems dens asi patient rating note.</param>
        /// <param name="importanceOfMedicalProblemTreatmentDensAsiPatientRating">The importance of medical problem treatment dens asi patient rating.</param>
        /// <param name="importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote">The importance of medical problem treatment dens asi patient rating note.</param>
        /// <param name="patientTreatmentDensAsiInterviewerRating">The patient treatment dens asi interviewer rating.</param>
        /// <param name="patientTreatmentDensAsiInterviewerRatingNote">The patient treatment dens asi interviewer rating note.</param>
        /// <param name="confidenceRateDistortedByPatientMisrepresentationIndicator">The confidence rate distorted by patient misrepresentation indicator.</param>
        /// <param name="confidenceRateDistortedByPatientMisrepresentationIndicatorNote">The confidence rate distorted by patient misrepresentation indicator note.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicator">The confidence rate distorted by patient inability to understand indicator.</param>
        /// <param name="confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote">The confidence rate distorted by patient inability to understand indicator note.</param>
        /// <param name="sectionNote">The section note.</param>
        public DensAsiMedicalStatusSection(DensAsiNonResponseType<int?> hopitalizedForMedicalProblemsCount,
                                               string hopitalizedForMedicalProblemsCountNote,
                                               DensAsiNonResponseType<TimeSpan?> yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan,
                                               string yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote,
                                               DensAsiNonResponseType<bool?> chronicMedicalProblemThatInterferesWithLifeIndicator,
                                               string chronicMedicalProblemThatInterferesWithLifeDescription,
                                               string chronicMedicalProblemThatInterferesWithLifeNote,
                                               DensAsiNonResponseType<bool?> takingPrescribedMedicationsForPhysicalProblemIndicator,
                                               string takingPrescribedMedicationsForPhysicalProblemDescription,
                                               string takingPrescribedMedicationsForPhysicalProblemNote,
                                               DensAsiNonResponseType<bool?> receivePensionForPhysicalDisabilityIndicator,
                                               string receivePensionForPhysicalDisabilityDescription,
                                               string receivePensionForPhysicalDisabilityNote,
                                               DensAsiNonResponseType<int?> medicalProblemsDayCount,
                                               string medicalProblemsDayCountNote,
                                               DensAsiNonResponseType<DensAsiPatientRating> troubledByMedicalProblemsDensAsiPatientRating,
                                               string troubledByMedicalProblemsDensAsiPatientRatingNote,
                                               DensAsiNonResponseType<DensAsiPatientRating> importanceOfMedicalProblemTreatmentDensAsiPatientRating,
                                               string importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote,
                                               DensAsiInterviewerRating patientTreatmentDensAsiInterviewerRating,
                                               string patientTreatmentDensAsiInterviewerRatingNote,
                                               bool? confidenceRateDistortedByPatientMisrepresentationIndicator,
                                               string confidenceRateDistortedByPatientMisrepresentationIndicatorNote,
                                               bool? confidenceRateDistortedByPatientInabilityToUnderstandIndicator,
                                               string confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote,
                                               string sectionNote )
        {
            if ( hopitalizedForMedicalProblemsCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => HopitalizedForMedicalProblemsCount ).Contains ( hopitalizedForMedicalProblemsCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "HopitalizedForMedicalProblemsCount DensAsiNonResponse value '" + hopitalizedForMedicalProblemsCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan ).Contains ( yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan DensAsiNonResponse value '" + yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( chronicMedicalProblemThatInterferesWithLifeIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ChronicMedicalProblemThatInterferesWithLifeIndicator ).Contains ( chronicMedicalProblemThatInterferesWithLifeIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ChronicMedicalProblemThatInterferesWithLifeIndicator DensAsiNonResponse value '" + chronicMedicalProblemThatInterferesWithLifeIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( takingPrescribedMedicationsForPhysicalProblemIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TakingPrescribedMedicationsForPhysicalProblemIndicator ).Contains ( takingPrescribedMedicationsForPhysicalProblemIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TakingPrescribedMedicationsForPhysicalProblemIndicator DensAsiNonResponse value '" + takingPrescribedMedicationsForPhysicalProblemIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( receivePensionForPhysicalDisabilityIndicator.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ReceivePensionForPhysicalDisabilityIndicator ).Contains ( receivePensionForPhysicalDisabilityIndicator.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ReceivePensionForPhysicalDisabilityIndicator DensAsiNonResponse value '" + receivePensionForPhysicalDisabilityIndicator.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( medicalProblemsDayCount.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => MedicalProblemsDayCount ).Contains ( medicalProblemsDayCount.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "MedicalProblemsDayCount DensAsiNonResponse value '" + medicalProblemsDayCount.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( troubledByMedicalProblemsDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => TroubledByMedicalProblemsDensAsiPatientRating ).Contains ( troubledByMedicalProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "TroubledByMedicalProblemsDensAsiPatientRating DensAsiNonResponse value '" + troubledByMedicalProblemsDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            if ( importanceOfMedicalProblemTreatmentDensAsiPatientRating.DensAsiNonResponse != null && !GetPossibleDensAsiNonResponseWellKnownNames ( () => ImportanceOfMedicalProblemTreatmentDensAsiPatientRating ).Contains ( importanceOfMedicalProblemTreatmentDensAsiPatientRating.DensAsiNonResponse.WellKnownName ) )
            {
                throw new ArgumentException ( "ImportanceOfMedicalProblemTreatmentDensAsiPatientRating DensAsiNonResponse value '" + importanceOfMedicalProblemTreatmentDensAsiPatientRating.DensAsiNonResponse.WellKnownName + "' is not valid." );
            }
            _hopitalizedForMedicalProblemsCount = hopitalizedForMedicalProblemsCount;
            _hopitalizedForMedicalProblemsCountNote = hopitalizedForMedicalProblemsCountNote;
            _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan = yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan;
            _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote = yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote;
            _chronicMedicalProblemThatInterferesWithLifeIndicator = chronicMedicalProblemThatInterferesWithLifeIndicator;
            _chronicMedicalProblemThatInterferesWithLifeDescription = chronicMedicalProblemThatInterferesWithLifeDescription;
            _chronicMedicalProblemThatInterferesWithLifeNote = chronicMedicalProblemThatInterferesWithLifeNote;
            _takingPrescribedMedicationsForPhysicalProblemIndicator = takingPrescribedMedicationsForPhysicalProblemIndicator;
            _takingPrescribedMedicationsForPhysicalProblemDescription = takingPrescribedMedicationsForPhysicalProblemDescription;
            _takingPrescribedMedicationsForPhysicalProblemNote = takingPrescribedMedicationsForPhysicalProblemNote;
            _receivePensionForPhysicalDisabilityIndicator = receivePensionForPhysicalDisabilityIndicator;
            _receivePensionForPhysicalDisabilityDescription = receivePensionForPhysicalDisabilityDescription;
            _receivePensionForPhysicalDisabilityNote = receivePensionForPhysicalDisabilityNote;
            _medicalProblemsDayCount = medicalProblemsDayCount;
            _medicalProblemsDayCountNote = medicalProblemsDayCountNote;
            _troubledByMedicalProblemsDensAsiPatientRating = troubledByMedicalProblemsDensAsiPatientRating;
            _troubledByMedicalProblemsDensAsiPatientRatingNote = troubledByMedicalProblemsDensAsiPatientRatingNote;
            _importanceOfMedicalProblemTreatmentDensAsiPatientRating = importanceOfMedicalProblemTreatmentDensAsiPatientRating;
            _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote = importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote;
            _patientTreatmentDensAsiInterviewerRating = patientTreatmentDensAsiInterviewerRating;
            _patientTreatmentDensAsiInterviewerRatingNote = patientTreatmentDensAsiInterviewerRatingNote;
            _confidenceRateDistortedByPatientMisrepresentationIndicator = confidenceRateDistortedByPatientMisrepresentationIndicator;
            _confidenceRateDistortedByPatientMisrepresentationIndicatorNote = confidenceRateDistortedByPatientMisrepresentationIndicatorNote;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicator = confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
            _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote = confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
            _sectionNote = sectionNote;
        }

        /// <summary>
        /// Gets the the number of times that the patient has been hopitalized for medical problems.
        /// Question Number: M1
        /// </summary>
        public virtual DensAsiNonResponseType<int?> HopitalizedForMedicalProblemsCount
        {
            get { return _hopitalizedForMedicalProblemsCount; }
            private set { }
        }

        /// <summary>
        /// Gets number of times that the patient has been hopitalized for medical problems count note.
        /// Question Number: M1
        /// </summary>
        public virtual string HopitalizedForMedicalProblemsCountNote
        {
            get { return _hopitalizedForMedicalProblemsCountNote; }
            private set { }
        }


        /// <summary>
        /// Gets the years and months after the patient's last hospitalization for physical problem.
        /// Question Number: M2
        /// </summary>
        public virtual DensAsiNonResponseType<TimeSpan?> YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan
        {
            get { return _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan; }
            private set { }
        }


        /// <summary>
        /// Gets the years and months after last hospitalization for physical problem time span note.
        /// Question Number: M2
        /// </summary>
        public virtual string YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote
        {
            get { return _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote; }
            private set { }
        }


        /// <summary>
        /// Gets a boolean value indicating that chronic medical problems interfere with the patient's life.
        /// Question Number: M3
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ChronicMedicalProblemThatInterferesWithLifeIndicator
        {
            get { return _chronicMedicalProblemThatInterferesWithLifeIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the a description of the chronic medical problem that interferes with the patient's life.
        /// Question Number: M3
        /// </summary>
        public virtual string ChronicMedicalProblemThatInterferesWithLifeDescription
        {
            get { return _chronicMedicalProblemThatInterferesWithLifeDescription; }
            private set { }
        }

        /// <summary>
        /// Gets the a description of the chronic medical problem that interferes with the patient's life note.
        /// Question Number: M3
        /// </summary>
        public virtual string ChronicMedicalProblemThatInterferesWithLifeNote
        {
            get { return _chronicMedicalProblemThatInterferesWithLifeNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating that the patient is taking prescribed medications for physical problem.
        /// Question Number: M4
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> TakingPrescribedMedicationsForPhysicalProblemIndicator
        {
            get { return _takingPrescribedMedicationsForPhysicalProblemIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a description of the physical problem description that medications have been prescribed for.
        /// Question Number: M4
        /// </summary>
        public virtual string TakingPrescribedMedicationsForPhysicalProblemDescription
        {
            get { return _takingPrescribedMedicationsForPhysicalProblemDescription; }
            private set { }
        }

        /// <summary>
        /// Gets a description of the physical problem description that medications have been prescribed for note.
        /// Question Number: M4
        /// </summary>
        public virtual string TakingPrescribedMedicationsForPhysicalProblemNote
        {
            get { return _takingPrescribedMedicationsForPhysicalProblemNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating whether a patient receives a pension for physical disability.
        /// Question Number: M5
        /// </summary>
        public virtual DensAsiNonResponseType<bool?> ReceivePensionForPhysicalDisabilityIndicator
        {
            get { return _receivePensionForPhysicalDisabilityIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a description of the pension the patient receives for a physical disability.
        /// Question Number: M5
        /// </summary>
        public virtual string ReceivePensionForPhysicalDisabilityDescription
        {
            get { return _receivePensionForPhysicalDisabilityDescription; }
            private set { }
        }

        /// <summary>
        /// Gets a description of the pension the patient receives for a physical disability note.
        /// Question Number: M5
        /// </summary>
        public virtual string ReceivePensionForPhysicalDisabilityNote
        {
            get { return _receivePensionForPhysicalDisabilityNote; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days the patient has experienced medical problems.
        /// Question Number: M6
        /// </summary>
        public virtual DensAsiNonResponseType<int?> MedicalProblemsDayCount
        {
            get { return _medicalProblemsDayCount; }
            private set { }
        }

        /// <summary>
        /// Gets the number of days the patient has experienced medical problems note.
        /// Question Number: M6
        /// </summary>
        public virtual string MedicalProblemsDayCountNote
        {
            get { return _medicalProblemsDayCountNote; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">TroubledByMedicalProblemsDensAsiPatientRating</see>
        /// rating denoting whether the patient is troubled by medical problems. Question Number: M7
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> TroubledByMedicalProblemsDensAsiPatientRating
        {
            get { return _troubledByMedicalProblemsDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the rating denoting whether the patient is troubled by medical problems note.
        /// Question Number: M7
        /// </summary>
        public virtual string TroubledByMedicalProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByMedicalProblemsDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the rating denoting the importance of medical problem.
        /// <see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">ImportanceOfMedicalProblemTreatmentDensAsiPatientRating</see>
        /// Question Number: M8
        /// </summary>
        public virtual DensAsiNonResponseType<DensAsiPatientRating> ImportanceOfMedicalProblemTreatmentDensAsiPatientRating
        {
            get { return _importanceOfMedicalProblemTreatmentDensAsiPatientRating; }
            private set { }
        }

        /// <summary>
        /// Gets the rating denoting the importance of medical problem note.
        /// Question Number: M8
        /// </summary>
        public virtual string ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingNote
        {
            get { return _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets the rating denoting the importance of patient treatment.
        /// <see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiPatientRating">PatientTreatmentDensAsiInterviewerRating</see>
        /// Question Number: M9
        /// </summary>
        public virtual DensAsiInterviewerRating PatientTreatmentDensAsiInterviewerRating
        {
            get { return _patientTreatmentDensAsiInterviewerRating; }
            private set { }
        }

        /// <summary>
        /// Gets the rating denoting the importance of patient treatment note.
        /// Question Number: M9
        /// </summary>
        public virtual string PatientTreatmentDensAsiInterviewerRatingNote
        {
            get { return _patientTreatmentDensAsiInterviewerRatingNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient misrepresentation.
        /// Question Number: M10
        /// </summary>
        public virtual bool? ConfidenceRateDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceRateDistortedByPatientMisrepresentationIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient misrepresentation note.
        /// Question Number: M10
        /// </summary>
        public virtual string ConfidenceRateDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientMisrepresentationIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient inability to understand.
        /// Question Number: M11
        /// </summary>
        public virtual bool? ConfidenceRateDistortedByPatientInabilityToUnderstandIndicator
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating the interviewer's confidence that medical information was distorted by patient inability to understand note.
        /// Question Number: M11
        /// </summary>
        public virtual string ConfidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote; }
            private set { }
        }

        /// <summary>
        /// Gets the medical status section note.
        /// </summary>
        public virtual string SectionNote
        {
            get { return _sectionNote; }
            private set { }
        }


        /// <summary>
        /// Gets the possible DensAsi non response well known names for this interview
        /// section.
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

            if ( propertyName == PropertyUtil.ExtractPropertyName ( () => YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan ) )
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
                           { PropertyUtil.ExtractPropertyName ( () => YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan ), GetPossibleDensAsiNonResponseWellKnownNames ( () => YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan ) }
                       };
        }
    }
}