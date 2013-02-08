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
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Web.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for Problem class.
    /// </summary>
    [DataContract]
    public sealed partial class ProblemDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private bool? _associatedIndicator;
        private bool? _causeOfDeathIndicator;
        private long _clinicalCaseKey;
        private StaffSummaryDto _observedByStaff;
        private DateTime? _observedDate;
        private DateTime? _onsetEndDate;
        private DateTime? _onsetStartDate;
        private CodedConceptDto _problemCodeCodedConcept;
        private LookupValueDto _problemStatus;
        private LookupValueDto _problemType;
        private DateTime? _statusChangedDate;
        private long _provenanceKey;
        private string _provenanceAssigningAuthorityName;
        private DateTime? _provenanceDate;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDto"/> class.
        /// </summary>
        public ProblemDto()
        {
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets the associated indicator.
        /// </summary>
        /// <value>The associated indicator.</value>
        [DataMember]
        public bool? AssociatedIndicator
        {
            get { return _associatedIndicator; }
            set { ApplyPropertyChange ( ref _associatedIndicator, () => AssociatedIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the cause of death indicator.
        /// </summary>
        /// <value>The cause of death indicator.</value>
        [DataMember]
        public bool? CauseOfDeathIndicator
        {
            get { return _causeOfDeathIndicator; }
            set { ApplyPropertyChange ( ref _causeOfDeathIndicator, () => CauseOfDeathIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the clinical case key.
        /// </summary>
        /// <value>The clinical case key.</value>
        [DataMember]
        public long ClinicalCaseKey
        {
            get { return _clinicalCaseKey; }
            set { ApplyPropertyChange ( ref _clinicalCaseKey, () => ClinicalCaseKey, value ); }
        }

        /// <summary>
        /// Gets or sets the observed by staff.
        /// </summary>
        /// <value>The observed by staff.</value>
        [DataMember]
        public StaffSummaryDto ObservedByStaff
        {
            get { return _observedByStaff; }
            set { ApplyPropertyChange ( ref _observedByStaff, () => ObservedByStaff, value ); }
        }

        /// <summary>
        /// Gets or sets the observed date.
        /// </summary>
        /// <value>The observed date.</value>
        [DataMember]
        public DateTime? ObservedDate
        {
            get { return _observedDate; }
            set { ApplyPropertyChange ( ref _observedDate, () => ObservedDate, value ); }
        }

        /// <summary>
        /// Gets or sets the onset end date.
        /// </summary>
        /// <value>The onset end date.</value>
        [DataMember]
        public DateTime? OnsetEndDate
        {
            get { return _onsetEndDate; }
            set { ApplyPropertyChange ( ref _onsetEndDate, () => OnsetEndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the onset start date.
        /// </summary>
        /// <value>The onset start date.</value>
        [DataMember]
        public DateTime? OnsetStartDate
        {
            get { return _onsetStartDate; }
            set { ApplyPropertyChange ( ref _onsetStartDate, () => OnsetStartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the problem code coded concept.
        /// </summary>
        /// <value>The problem code coded concept.</value>
        [DataMember]
        public CodedConceptDto ProblemCodeCodedConcept
        {
            get { return _problemCodeCodedConcept; }
            set { ApplyPropertyChange ( ref _problemCodeCodedConcept, () => ProblemCodeCodedConcept, value ); }
        }

        /// <summary>
        /// Gets or sets the problem status.
        /// </summary>
        /// <value>The problem status.</value>
        [DataMember]
        public LookupValueDto ProblemStatus
        {
            get { return _problemStatus; }
            set { ApplyPropertyChange ( ref _problemStatus, () => ProblemStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the problem.
        /// </summary>
        /// <value>The type of the problem.</value>
        [DataMember]
        public LookupValueDto ProblemType
        {
            get { return _problemType; }
            set { ApplyPropertyChange ( ref _problemType, () => ProblemType, value ); }
        }

        /// <summary>
        /// Gets or sets the status changed date.
        /// </summary>
        /// <value>The status changed date.</value>
        [DataMember]
        public DateTime? StatusChangedDate
        {
            get { return _statusChangedDate; }
            set { ApplyPropertyChange ( ref _statusChangedDate, () => StatusChangedDate, value ); }
        }

        /// <summary>
        /// Gets or sets the provenance key.
        /// </summary>
        /// <value>
        /// The provenance key.
        /// </value>
        [DataMember]
        public long ProvenanceKey
        {
            get { return _provenanceKey; }
            set { ApplyPropertyChange(ref _provenanceKey, () => ProvenanceKey, value); }
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
