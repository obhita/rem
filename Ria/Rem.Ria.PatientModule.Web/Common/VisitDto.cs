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
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for Visit class.
    /// </summary>
    public class VisitDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private ObservableCollection<ActivityDto> _activities;
        private DateTime _appointmentEndDateTime;
        private DateTime _appointmentStartDateTime;
        private long _clinicalCaseKey;
        private string _cptCode;
        private LocationDisplayNameDto _location;
        private string _name;
        private string _note;
        private ObservableCollection<ProblemDto> _problems;
        private StaffSummaryDto _staff;
        private LookupValueDto _visitStatus;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitDto"/> class.
        /// </summary>
        public VisitDto ()
        {
            _activities = new ObservableCollection<ActivityDto> ();
            _problems = new ObservableCollection<ProblemDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the activities.
        /// </summary>
        /// <value>The activities.</value>
        [DataMember]
        public ObservableCollection<ActivityDto> Activities
        {
            get { return _activities; }
            set { ApplyCollectionChange ( ref _activities, () => Activities, value ); }
        }

        /// <summary>
        /// Gets or sets the appointment end date time.
        /// </summary>
        /// <value>The appointment end date time.</value>
        [DataMember]
        public DateTime AppointmentEndDateTime
        {
            get { return _appointmentEndDateTime; }
            set { ApplyPropertyChange ( ref _appointmentEndDateTime, () => AppointmentEndDateTime, value ); }
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
        /// Gets or sets the CPT code.
        /// </summary>
        /// <value>The CPT code.</value>
        [DataMember]
        public string CptCode
        {
            get { return _cptCode; }
            set { ApplyPropertyChange ( ref _cptCode, () => CptCode, value ); }
        }

        /// <summary>
        /// Gets or sets the end timestamp time.
        /// </summary>
        /// <value>The end timestamp time.</value>
        public string EndTimestampTime
        {
            get { return _appointmentEndDateTime.ToShortTimeString (); }
            set
            {
                if ( !string.IsNullOrEmpty ( value ) )
                {
                    var newValue = _appointmentStartDateTime.Date + DateTime.Parse ( value ).TimeOfDay;
                    ApplyPropertyChange ( ref _appointmentEndDateTime, () => EndTimestampTime, newValue );
                }
            }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [DataMember]
        public LocationDisplayNameDto Location
        {
            get { return _location; }
            set { ApplyPropertyChange ( ref _location, () => Location, value ); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the visit.</value>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note for the visit.</value>
        [DataMember]
        public string Note
        {
            get { return _note; }
            set { ApplyPropertyChange ( ref _note, () => Note, value ); }
        }

        /// <summary>
        /// Gets or sets the problems.
        /// </summary>
        /// <value>The problems.</value>
        [DataMember]
        public ObservableCollection<ProblemDto> Problems
        {
            get { return _problems; }
            set { ApplyCollectionChange ( ref _problems, () => Problems, value ); }
        }

        /// <summary>
        /// Gets or sets the staff.
        /// </summary>
        /// <value>The staff.</value>
        [DataMember]
        public StaffSummaryDto Staff
        {
            get { return _staff; }
            set { ApplyPropertyChange ( ref _staff, () => Staff, value ); }
        }

        /// <summary>
        /// Gets or sets the start timestamp time.
        /// </summary>
        /// <value>The start timestamp time.</value>
        public string StartTimestampTime
        {
            get { return _appointmentStartDateTime.ToShortTimeString (); }
            set
            {
                if ( !string.IsNullOrEmpty ( value ) )
                {
                    var newValue = _appointmentStartDateTime.Date + DateTime.Parse ( value ).TimeOfDay;
                    ApplyPropertyChange ( ref _appointmentStartDateTime, () => StartTimestampTime, newValue );
                }
            }
        }

        /// <summary>
        /// Gets or sets the visit status.
        /// </summary>
        /// <value>The visit status.</value>
        [DataMember]
        public LookupValueDto VisitStatus
        {
            get { return _visitStatus; }
            set { ApplyPropertyChange ( ref _visitStatus, () => VisitStatus, value ); }
        }

        #endregion
    }
}
