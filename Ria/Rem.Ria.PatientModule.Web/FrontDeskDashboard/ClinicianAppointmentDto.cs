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
using System.Collections.Generic;
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.FrontDeskDashboard
{
    /// <summary>
    /// Data transfer object for ClinicianAppointment class.
    /// </summary>
    [DataContract]
    public partial class ClinicianAppointmentDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private LookupValueDto _visitStatus;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the appointment end date time.
        /// </summary>
        /// <value>The appointment end date time.</value>
        [DataMember]
        public DateTime AppointmentEndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the appointment start date time.
        /// </summary>
        /// <value>The appointment start date time.</value>
        [DataMember]
        public DateTime AppointmentStartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the clinician key.
        /// </summary>
        /// <value>The clinician key.</value>
        [DataMember]
        public long ClinicianKey { get; set; }

        /// <summary>
        /// Gets or sets the patient alerts.
        /// </summary>
        /// <value>The patient alerts.</value>
        [DataMember]
        public IEnumerable<PatientAlertDto> PatientAlerts { get; set; }

        /// <summary>
        /// Gets or sets the first name of the patient.
        /// </summary>
        /// <value>The first name of the patient.</value>
        [DataMember]
        public string PatientFirstName { get; set; }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey { get; set; }

        /// <summary>
        /// Gets or sets the last name of the patient.
        /// </summary>
        /// <value>The last name of the patient.</value>
        [DataMember]
        public string PatientLastName { get; set; }

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

        /// <summary>
        /// Gets or sets the name of the visit template.
        /// </summary>
        /// <value>The name of the visit template.</value>
        [DataMember]
        public string VisitTemplateName { get; set; }

        #endregion
    }
}
