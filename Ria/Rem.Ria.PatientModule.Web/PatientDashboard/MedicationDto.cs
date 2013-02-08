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
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for Medication class.
    /// </summary>
    public partial class MedicationDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private string _discontinuedByPhysicianName;
        private LookupValueDto _discontinuedReason;
        private string _discontinuedReasonOtherDescription;
        private DateTime? _endDate;
        private string _frequencyDescription;
        private string _instructionsNote;
        private CodedConceptDto _medicationCodeCodedConcept;
        private LookupValueDto _medicationStatus;
        private bool? _overTheCounterIndicator;
        private long _patientKey;
        private string _prescribingPhysicianName;
        private DateTime? _startDate;
        private long _provenanceKey;
        private string _provenanceAssigningAuthorityName;
        private DateTime? _provenanceDate;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the discontinued by physician.
        /// </summary>
        /// <value>The name of the discontinued by physician.</value>
        [DataMember]
        public string DiscontinuedByPhysicianName
        {
            get { return _discontinuedByPhysicianName; }
            set { ApplyPropertyChange ( ref _discontinuedByPhysicianName, () => DiscontinuedByPhysicianName, value ); }
        }

        /// <summary>
        /// Gets or sets the discontinued reason.
        /// </summary>
        /// <value>The discontinued reason.</value>
        [DataMember]
        public LookupValueDto DiscontinuedReason
        {
            get { return _discontinuedReason; }
            set { ApplyPropertyChange ( ref _discontinuedReason, () => DiscontinuedReason, value ); }
        }

        /// <summary>
        /// Gets or sets the discontinued reason other description.
        /// </summary>
        /// <value>The discontinued reason other description.</value>
        [DataMember]
        public string DiscontinuedReasonOtherDescription
        {
            get { return _discontinuedReasonOtherDescription; }
            set { ApplyPropertyChange ( ref _discontinuedReasonOtherDescription, () => DiscontinuedReasonOtherDescription, value ); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [DataMember]
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the frequency description.
        /// </summary>
        /// <value>The frequency description.</value>
        [DataMember]
        public string FrequencyDescription
        {
            get { return _frequencyDescription; }
            set { ApplyPropertyChange ( ref _frequencyDescription, () => FrequencyDescription, value ); }
        }

        /// <summary>
        /// Gets or sets the instructions note.
        /// </summary>
        /// <value>The instructions note.</value>
        [DataMember]
        public string InstructionsNote
        {
            get { return _instructionsNote; }
            set { ApplyPropertyChange ( ref _instructionsNote, () => InstructionsNote, value ); }
        }

        /// <summary>
        /// Gets or sets the medication code coded concept.
        /// </summary>
        /// <value>The medication code coded concept.</value>
        [DataMember]
        public CodedConceptDto MedicationCodeCodedConcept
        {
            get { return _medicationCodeCodedConcept; }
            set { ApplyPropertyChange ( ref _medicationCodeCodedConcept, () => MedicationCodeCodedConcept, value ); }
        }

        /// <summary>
        /// Gets or sets the medication status.
        /// </summary>
        /// <value>The medication status.</value>
        [DataMember]
        public LookupValueDto MedicationStatus
        {
            get { return _medicationStatus; }
            set { ApplyPropertyChange ( ref _medicationStatus, () => MedicationStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the over the counter indicator.
        /// </summary>
        /// <value>The over the counter indicator.</value>
        [DataMember]
        public bool? OverTheCounterIndicator
        {
            get { return _overTheCounterIndicator; }
            set { ApplyPropertyChange ( ref _overTheCounterIndicator, () => OverTheCounterIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey
        {
            get { return _patientKey; }
            set { ApplyPropertyChange ( ref _patientKey, () => PatientKey, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the prescribing physician.
        /// </summary>
        /// <value>The name of the prescribing physician.</value>
        [DataMember]
        public string PrescribingPhysicianName
        {
            get { return _prescribingPhysicianName; }
            set { ApplyPropertyChange ( ref _prescribingPhysicianName, () => PrescribingPhysicianName, value ); }
        }

        /// <summary>
        /// Gets or sets the root medication coded concept.
        /// </summary>
        /// <value>The root medication coded concept.</value>
        [DataMember]
        public CodedConceptDto RootMedicationCodedConcept { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [DataMember]
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { ApplyPropertyChange ( ref _startDate, () => StartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the provenance key.
        /// </summary>
        /// <value>The provenance key.</value>
        [DataMember]
        public long ProvenanceKey
        {
            get { return _provenanceKey; }
            set { ApplyPropertyChange ( ref _provenanceKey, () => ProvenanceKey, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the provenance assigning authority.
        /// </summary>
        /// <value>The name of the provenance assigning authority.</value>
        [DataMember]
        public string ProvenanceAssigningAuthorityName
        {
            get { return _provenanceAssigningAuthorityName; }
            set { ApplyPropertyChange(ref _provenanceAssigningAuthorityName, () => ProvenanceAssigningAuthorityName, value); }
        }

        /// <summary>
        /// Gets or sets the provenance date.
        /// </summary>
        /// <value>The provenance date.</value>
        [DataMember]
        public DateTime? ProvenanceDate
        {
            get { return _provenanceDate; }
            set { ApplyPropertyChange(ref _provenanceDate, () => ProvenanceDate, value); }
        }

        #endregion
    }
}
