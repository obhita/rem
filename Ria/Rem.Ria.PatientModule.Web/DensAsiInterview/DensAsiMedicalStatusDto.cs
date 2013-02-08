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

using System;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiMedicalStatus class.
    /// </summary>
    public class DensAsiMedicalStatusDto : DensAsiDtoBase
    {
        #region Constants and Fields

        private string _chronicMedicalProblemThatInterferesWithLifeDescription;
        private DensAsiNonResponseTypeDto<bool?> _chronicMedicalProblemThatInterferesWithLifeIndicator;
        private string _chronicMedicalProblemThatInterferesWithLifeNote;
        private bool? _confidenceRateDistortedByPatientInabilityToUnderstandIndicator;
        private string _confidenceRateDistortedByPatientInabilityToUnderstandIndicatorNote;
        private bool? _confidenceRateDistortedByPatientMisrepresentationIndicator;
        private string _confidenceRateDistortedByPatientMisrepresentationIndicatorNote;
        private DensAsiNonResponseTypeDto<int?> _hopitalizedForMedicalProblemsCount;
        private string _hopitalizedForMedicalProblemsCountNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _importanceOfMedicalProblemTreatmentDensAsiPatientRating;
        private string _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<int?> _medicalProblemsDayCount;
        private string _medicalProblemsDayCountNote;
        private LookupValueDto _patientTreatmentDensAsiInterviewerRating;
        private string _patientTreatmentDensAsiInterviewerRatingNote;
        private string _receivePensionForPhysicalDisabilityDescription;
        private DensAsiNonResponseTypeDto<bool?> _receivePensionForPhysicalDisabilityIndicator;
        private string _receivePensionForPhysicalDisabilityNote;
        private string _sectionNote;
        private string _takingPrescribedMedicationsForPhysicalProblemDescription;
        private DensAsiNonResponseTypeDto<bool?> _takingPrescribedMedicationsForPhysicalProblemIndicator;
        private string _takingPrescribedMedicationsForPhysicalProblemNote;
        private DensAsiNonResponseTypeDto<LookupValueDto> _troubledByMedicalProblemsDensAsiPatientRating;
        private string _troubledByMedicalProblemsDensAsiPatientRatingNote;
        private DensAsiNonResponseTypeDto<TimeSpan?> _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan;
        private string _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote;

        #endregion

        #region Public Properties

        /// <summary>
        /// Question Number: M3
        /// </summary>
        /// <value>The chronic medical problem that interferes with life description.</value>
        public string ChronicMedicalProblemThatInterferesWithLifeDescription
        {
            get { return _chronicMedicalProblemThatInterferesWithLifeDescription; }
            set
            {
                ApplyPropertyChange (
                    ref _chronicMedicalProblemThatInterferesWithLifeDescription, () => ChronicMedicalProblemThatInterferesWithLifeDescription, value );
            }
        }

        /// <summary>
        /// Question Number: M3
        /// </summary>
        /// <value>The chronic medical problem that interferes with life indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ChronicMedicalProblemThatInterferesWithLifeIndicator
        {
            get { return _chronicMedicalProblemThatInterferesWithLifeIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _chronicMedicalProblemThatInterferesWithLifeIndicator, () => ChronicMedicalProblemThatInterferesWithLifeIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: M3
        /// </summary>
        /// <value>The chronic medical problem that interferes with life note.</value>
        public string ChronicMedicalProblemThatInterferesWithLifeNote
        {
            get { return _chronicMedicalProblemThatInterferesWithLifeNote; }
            set
            {
                ApplyPropertyChange (
                    ref _chronicMedicalProblemThatInterferesWithLifeNote, () => ChronicMedicalProblemThatInterferesWithLifeNote, value );
            }
        }

        /// <summary>
        /// Question Number: M11
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
        /// Question Number: M11
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
        /// Question Number: M10
        /// </summary>
        /// <value>The confidence rate distorted by patient misrepresentation indicator.</value>
        public bool? ConfidenceRateDistortedByPatientMisrepresentationIndicator
        {
            get { return _confidenceRateDistortedByPatientMisrepresentationIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceRateDistortedByPatientMisrepresentationIndicator,
                    () => ConfidenceRateDistortedByPatientMisrepresentationIndicator,
                    value );
            }
        }

        /// <summary>
        /// Question Number: M10
        /// </summary>
        /// <value>The confidence rate distorted by patient misrepresentation indicator note.</value>
        public string ConfidenceRateDistortedByPatientMisrepresentationIndicatorNote
        {
            get { return _confidenceRateDistortedByPatientMisrepresentationIndicatorNote; }
            set
            {
                ApplyPropertyChange (
                    ref _confidenceRateDistortedByPatientMisrepresentationIndicatorNote,
                    () => ConfidenceRateDistortedByPatientMisrepresentationIndicatorNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: M1
        /// </summary>
        /// <value>The hopitalized for medical problems count.</value>
        public DensAsiNonResponseTypeDto<int?> HopitalizedForMedicalProblemsCount
        {
            get { return _hopitalizedForMedicalProblemsCount; }
            set { ApplyPropertyChange ( ref _hopitalizedForMedicalProblemsCount, () => HopitalizedForMedicalProblemsCount, value ); }
        }

        /// <summary>
        /// Question Number: M1
        /// </summary>
        /// <value>The hopitalized for medical problems count note.</value>
        public string HopitalizedForMedicalProblemsCountNote
        {
            get { return _hopitalizedForMedicalProblemsCountNote; }
            set { ApplyPropertyChange ( ref _hopitalizedForMedicalProblemsCountNote, () => HopitalizedForMedicalProblemsCountNote, value ); }
        }

        /// <summary>
        /// Question Number: M8
        /// </summary>
        /// <value>The importance of medical problem treatment dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> ImportanceOfMedicalProblemTreatmentDensAsiPatientRating
        {
            get { return _importanceOfMedicalProblemTreatmentDensAsiPatientRating; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfMedicalProblemTreatmentDensAsiPatientRating, () => ImportanceOfMedicalProblemTreatmentDensAsiPatientRating, value );
            }
        }

        /// <summary>
        /// Question Number: M8
        /// </summary>
        /// <value>The importance of medical problem treatment dens asi patient rating note.</value>
        public string ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingNote
        {
            get { return _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _importanceOfMedicalProblemTreatmentDensAsiPatientRatingNote,
                    () => ImportanceOfMedicalProblemTreatmentDensAsiPatientRatingNote,
                    value );
            }
        }

        /// <summary>
        /// Question Number: M6
        /// </summary>
        /// <value>The medical problems day count.</value>
        public DensAsiNonResponseTypeDto<int?> MedicalProblemsDayCount
        {
            get { return _medicalProblemsDayCount; }
            set { ApplyPropertyChange ( ref _medicalProblemsDayCount, () => MedicalProblemsDayCount, value ); }
        }

        /// <summary>
        /// Question Number: M6
        /// </summary>
        /// <value>The medical problems day count note.</value>
        public string MedicalProblemsDayCountNote
        {
            get { return _medicalProblemsDayCountNote; }
            set { ApplyPropertyChange ( ref _medicalProblemsDayCountNote, () => MedicalProblemsDayCountNote, value ); }
        }

        /// <summary>
        /// Question Number: M9
        /// </summary>
        /// <value>The patient treatment dens asi interviewer rating.</value>
        public LookupValueDto PatientTreatmentDensAsiInterviewerRating
        {
            get { return _patientTreatmentDensAsiInterviewerRating; }
            set { ApplyPropertyChange ( ref _patientTreatmentDensAsiInterviewerRating, () => PatientTreatmentDensAsiInterviewerRating, value ); }
        }

        /// <summary>
        /// Question Number: M9
        /// </summary>
        /// <value>The patient treatment dens asi interviewer rating note.</value>
        public string PatientTreatmentDensAsiInterviewerRatingNote
        {
            get { return _patientTreatmentDensAsiInterviewerRatingNote; }
            set { ApplyPropertyChange ( ref _patientTreatmentDensAsiInterviewerRatingNote, () => PatientTreatmentDensAsiInterviewerRatingNote, value ); }
        }

        /// <summary>
        /// Question Number: M5
        /// </summary>
        /// <value>The receive pension for physical disability description.</value>
        public string ReceivePensionForPhysicalDisabilityDescription
        {
            get { return _receivePensionForPhysicalDisabilityDescription; }
            set
            {
                ApplyPropertyChange (
                    ref _receivePensionForPhysicalDisabilityDescription, () => ReceivePensionForPhysicalDisabilityDescription, value );
            }
        }

        /// <summary>
        /// Question Number: M5
        /// </summary>
        /// <value>The receive pension for physical disability indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> ReceivePensionForPhysicalDisabilityIndicator
        {
            get { return _receivePensionForPhysicalDisabilityIndicator; }
            set { ApplyPropertyChange ( ref _receivePensionForPhysicalDisabilityIndicator, () => ReceivePensionForPhysicalDisabilityIndicator, value ); }
        }

        /// <summary>
        /// Question Number: M5
        /// </summary>
        /// <value>The receive pension for physical disability note.</value>
        public string ReceivePensionForPhysicalDisabilityNote
        {
            get { return _receivePensionForPhysicalDisabilityNote; }
            set { ApplyPropertyChange ( ref _receivePensionForPhysicalDisabilityNote, () => ReceivePensionForPhysicalDisabilityNote, value ); }
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
        /// Question Number: M4
        /// </summary>
        /// <value>The taking prescribed medications for physical problem description.</value>
        public string TakingPrescribedMedicationsForPhysicalProblemDescription
        {
            get { return _takingPrescribedMedicationsForPhysicalProblemDescription; }
            set
            {
                ApplyPropertyChange (
                    ref _takingPrescribedMedicationsForPhysicalProblemDescription,
                    () => TakingPrescribedMedicationsForPhysicalProblemDescription,
                    value );
            }
        }

        /// <summary>
        /// Question Number: M4
        /// </summary>
        /// <value>The taking prescribed medications for physical problem indicator.</value>
        public DensAsiNonResponseTypeDto<bool?> TakingPrescribedMedicationsForPhysicalProblemIndicator
        {
            get { return _takingPrescribedMedicationsForPhysicalProblemIndicator; }
            set
            {
                ApplyPropertyChange (
                    ref _takingPrescribedMedicationsForPhysicalProblemIndicator, () => TakingPrescribedMedicationsForPhysicalProblemIndicator, value );
            }
        }

        /// <summary>
        /// Question Number: M4
        /// </summary>
        /// <value>The taking prescribed medications for physical problem note.</value>
        public string TakingPrescribedMedicationsForPhysicalProblemNote
        {
            get { return _takingPrescribedMedicationsForPhysicalProblemNote; }
            set
            {
                ApplyPropertyChange (
                    ref _takingPrescribedMedicationsForPhysicalProblemNote, () => TakingPrescribedMedicationsForPhysicalProblemNote, value );
            }
        }

        /// <summary>
        /// Question Number: M7
        /// </summary>
        /// <value>The troubled by medical problems dens asi patient rating.</value>
        public DensAsiNonResponseTypeDto<LookupValueDto> TroubledByMedicalProblemsDensAsiPatientRating
        {
            get { return _troubledByMedicalProblemsDensAsiPatientRating; }
            set { ApplyPropertyChange ( ref _troubledByMedicalProblemsDensAsiPatientRating, () => TroubledByMedicalProblemsDensAsiPatientRating, value ); }
        }

        /// <summary>
        /// Question Number: M7
        /// </summary>
        /// <value>The troubled by medical problems dens asi patient rating note.</value>
        public string TroubledByMedicalProblemsDensAsiPatientRatingNote
        {
            get { return _troubledByMedicalProblemsDensAsiPatientRatingNote; }
            set
            {
                ApplyPropertyChange (
                    ref _troubledByMedicalProblemsDensAsiPatientRatingNote, () => TroubledByMedicalProblemsDensAsiPatientRatingNote, value );
            }
        }

        /// <summary>
        /// Question Number: M2
        /// </summary>
        /// <value>The years and months after last hospitalization for physical problem time span.</value>
        public DensAsiNonResponseTypeDto<TimeSpan?> YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan
        {
            get { return _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan; }
            set
            {
                ApplyPropertyChange (
                    ref _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan,
                    () => YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpan,
                    value );
            }
        }

        /// <summary>
        /// Question Number: M2
        /// </summary>
        /// <value>The years and months after last hospitalization for physical problem time span note.</value>
        public string YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote
        {
            get { return _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote; }
            set
            {
                ApplyPropertyChange (
                    ref _yearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote,
                    () => YearsAndMonthsAfterLastHospitalizationForPhysicalProblemTimeSpanNote,
                    value );
            }
        }

        #endregion
    }
}
