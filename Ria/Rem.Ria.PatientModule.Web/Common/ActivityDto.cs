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

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for Activity class.
    /// </summary>
    [DataContract]
    public class ActivityDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private DateTime _activityEndDateTime;
        private DateTime _activityStartDateTime;
        private ActivityTypeDto _activityType;
        private DateTime _appointmentStartDateTime;
        private long _clinicianKey, _patientKey;
        private long _visitKey;
        private string _visitStatusWellKnownName;
        private string _visitTemplateName;
        private long _provenanceKey;
        private long _clinicalCaseKey;
        private string _provenanceAssigningAuthorityName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the activity end date time.
        /// </summary>
        /// <value>The activity end date time.</value>
        [DataMember]
        public DateTime ActivityEndDateTime
        {
            get { return _activityEndDateTime; }
            set { ApplyPropertyChange ( ref _activityEndDateTime, () => ActivityEndDateTime, value ); }
        }

        /// <summary>
        /// Gets or sets the activity start date time.
        /// </summary>
        /// <value>The activity start date time.</value>
        [DataMember]
        public DateTime ActivityStartDateTime
        {
            get { return _activityStartDateTime; }
            set { ApplyPropertyChange ( ref _activityStartDateTime, () => ActivityStartDateTime, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the activity.
        /// </summary>
        /// <value>The type of the activity.</value>
        [DataMember]
        public ActivityTypeDto ActivityType
        {
            get { return _activityType; }
            set { ApplyPropertyChange ( ref _activityType, () => ActivityType, value ); }
        }

        /// <summary>
        /// Gets or sets the appointment start date time.
        /// </summary>
        /// <value>The appointment start date time.</value>
        [DataMember]
        public DateTime AppointmentStartDateTime
        {
            get { return _appointmentStartDateTime; }
            set { ApplyPropertyChange ( ref _appointmentStartDateTime, () => AppointmentStartDateTime, value ); }
        }

        /// <summary>
        /// Gets or sets the clinician key.
        /// </summary>
        /// <value>The clinician key.</value>
        [DataMember]
        public long ClinicianKey
        {
            get { return _clinicianKey; }
            set { ApplyPropertyChange ( ref _clinicianKey, () => ClinicianKey, value ); }
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
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        [DataMember]
        public string Results { get; set; }

        /// <summary>
        /// Gets or sets the visit key.
        /// </summary>
        /// <value>The visit key.</value>
        [DataMember]
        public long VisitKey
        {
            get { return _visitKey; }
            set { ApplyPropertyChange ( ref _visitKey, () => VisitKey, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the visit status well known.
        /// </summary>
        /// <value>The name of the visit status well known.</value>
        [DataMember]
        public string VisitStatusWellKnownName
        {
            get { return _visitStatusWellKnownName; }
            set { ApplyPropertyChange ( ref _visitStatusWellKnownName, () => VisitStatusWellKnownName, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the visit template.
        /// </summary>
        /// <value>The name of the visit template.</value>
        [DataMember]
        public string VisitTemplateName
        {
            get { return _visitTemplateName; }
            set { ApplyPropertyChange ( ref _visitTemplateName, () => VisitTemplateName, value ); }
        }

        /// <summary>
        /// Gets or sets the provenance key.
        /// </summary>
        /// <value>The provenance key.</value>
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
        /// Gets or sets the clinical case key.
        /// </summary>
        /// <value>The clinical case key.</value>
        [DataMember]
        public long ClinicalCaseKey
        {
            get { return _clinicalCaseKey; }
            set { ApplyPropertyChange ( ref _clinicalCaseKey, () => ClinicalCaseKey, value ); }
        }

        #endregion
    }
}
