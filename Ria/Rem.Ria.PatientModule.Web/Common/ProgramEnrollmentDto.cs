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

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Program Enrollment Dto.
    /// </summary>
    [DataContract]
    public partial class ProgramEnrollmentDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private long _clinicalCaseKey;
        private string _commentsNote;
        private int? _daysOnWaitingListCount;
        private string _disenrollOtherReasonNote;
        private LookupValueDto _disenrollReason;
        private DateTime? _disenrollmentDate;
        private StaffSummaryDto _enrollingStaff;
        private DateTime _enrollmentDate;
        private LocationSummaryDto _location;
        private string _programName;
        private long _programOfferingKey;

        #endregion

        #region Public Properties

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
        /// Gets or sets the comments note.
        /// </summary>
        /// <value>The comments note.</value>
        [DataMember]
        public string CommentsNote
        {
            get { return _commentsNote; }
            set { ApplyPropertyChange ( ref _commentsNote, () => CommentsNote, value ); }
        }

        /// <summary>
        /// Gets or sets the days on waiting list count.
        /// </summary>
        /// <value>The days on waiting list count.</value>
        [DataMember]
        public int? DaysOnWaitingListCount
        {
            get { return _daysOnWaitingListCount; }
            set { ApplyPropertyChange ( ref _daysOnWaitingListCount, () => DaysOnWaitingListCount, value ); }
        }

        /// <summary>
        /// Gets or sets the disenroll other reason note.
        /// </summary>
        /// <value>The disenroll other reason note.</value>
        [DataMember]
        public string DisenrollOtherReasonNote
        {
            get { return _disenrollOtherReasonNote; }
            set { ApplyPropertyChange ( ref _disenrollOtherReasonNote, () => DisenrollOtherReasonNote, value ); }
        }

        /// <summary>
        /// Gets or sets the disenroll reason.
        /// </summary>
        /// <value>The disenroll reason.</value>
        [DataMember]
        public LookupValueDto DisenrollReason
        {
            get { return _disenrollReason; }
            set { ApplyPropertyChange ( ref _disenrollReason, () => DisenrollReason, value ); }
        }

        /// <summary>
        /// Gets or sets the disenrollment date.
        /// </summary>
        /// <value>The disenrollment date.</value>
        [DataMember]
        public DateTime? DisenrollmentDate
        {
            get { return _disenrollmentDate; }
            set { ApplyPropertyChange ( ref _disenrollmentDate, () => DisenrollmentDate, value ); }
        }

        /// <summary>
        /// Gets or sets the enrolling staff.
        /// </summary>
        /// <value>The enrolling staff.</value>
        [DataMember]
        public StaffSummaryDto EnrollingStaff
        {
            get { return _enrollingStaff; }
            set { ApplyPropertyChange ( ref _enrollingStaff, () => EnrollingStaff, value ); }
        }

        /// <summary>
        /// Gets or sets the enrollment date.
        /// </summary>
        /// <value>The enrollment date.</value>
        [DataMember]
        public DateTime EnrollmentDate
        {
            get { return _enrollmentDate; }
            set { ApplyPropertyChange ( ref _enrollmentDate, () => EnrollmentDate, value ); }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        [DataMember]
        public LocationSummaryDto Location
        {
            get { return _location; }
            set { ApplyPropertyChange ( ref _location, () => Location, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>The name of the program.</value>
        [DataMember]
        public string ProgramName
        {
            get { return _programName; }
            set { ApplyPropertyChange ( ref _programName, () => ProgramName, value ); }
        }

        /// <summary>
        /// Gets or sets the program offering key.
        /// </summary>
        /// <value>The program offering key.</value>
        [DataMember]
        public long ProgramOfferingKey
        {
            get { return _programOfferingKey; }
            set { ApplyPropertyChange ( ref _programOfferingKey, () => ProgramOfferingKey, value ); }
        }

        #endregion
    }
}
