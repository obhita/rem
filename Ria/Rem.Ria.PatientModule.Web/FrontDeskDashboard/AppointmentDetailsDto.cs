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
    /// Data transfer object for AppointmentDetails class.
    /// </summary>
    [DataContract]
    public class AppointmentDetailsDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private IList<string> _activityNames;
        private DateTime? _appointmentEndDateTime;
        private long _appointmentKey;
        private DateTime? _appointmentStartDateTime;
        private long? _clinicianKey;
        private string _cptCode;
        private LocationDisplayNameDto _location;
        private string _name;
        private string _patientAddressLine1;
        private string _patientCity;
        private DateTime? _patientDateOfBirth;
        private string _patientFirstName;
        private LookupValueDto _patientGender;
        private long _patientKey;
        private string _patientLastName;
        private PatientPhoneDto _patientPhoneNumber;
        private string _patientPostalCode;
        private string _patientPrefix;
        private string _patientState;
        private string _patientUniqueIdentifier;
        private LookupValueDto _visitStatus;
        private long? _visitTemplateKey;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the activity names.
        /// </summary>
        /// <value>The activity names.</value>
        [DataMember]
        public IList<string> ActivityNames
        {
            get { return _activityNames; }
            set { ApplyPropertyChange ( ref _activityNames, () => ActivityNames, value ); }
        }

        /// <summary>
        /// Gets or sets the appointment end date time.
        /// </summary>
        /// <value>The appointment end date time.</value>
        [DataMember]
        public DateTime? AppointmentEndDateTime
        {
            get { return _appointmentEndDateTime; }
            set { ApplyPropertyChange ( ref _appointmentEndDateTime, () => AppointmentEndDateTime, value ); }
        }

        /// <summary>
        /// Gets or sets the appointment key.
        /// </summary>
        /// <value>The appointment key.</value>
        [DataMember]
        public long AppointmentKey
        {
            get { return _appointmentKey; }
            set { ApplyPropertyChange ( ref _appointmentKey, () => AppointmentKey, value ); }
        }

        /// <summary>
        /// Gets or sets the appointment start date time.
        /// </summary>
        /// <value>The appointment start date time.</value>
        [DataMember]
        public DateTime? AppointmentStartDateTime
        {
            get { return _appointmentStartDateTime; }
            set { ApplyPropertyChange ( ref _appointmentStartDateTime, () => AppointmentStartDateTime, value ); }
        }

        /// <summary>
        /// Gets or sets the clinician key.
        /// </summary>
        /// <value>The clinician key.</value>
        [DataMember]
        public long? ClinicianKey
        {
            get { return _clinicianKey; }
            set { ApplyPropertyChange ( ref _clinicianKey, () => ClinicianKey, value ); }
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
        /// Gets the full patient address.
        /// </summary>
        public string FullPatientAddress
        {
            get
            {
                return string.Format (
                    "{0}, {1} {2} {3}",
                    PatientAddressLine1,
                    PatientCity,
                    PatientState,
                    PatientPostalCode );
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
        /// <value>The name of the appointment.</value>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the patient address line1.
        /// </summary>
        /// <value>The patient address line1.</value>
        [DataMember]
        public string PatientAddressLine1
        {
            get { return _patientAddressLine1; }
            set { ApplyPropertyChange ( ref _patientAddressLine1, () => PatientAddressLine1, value ); }
        }

        /// <summary>
        /// Gets or sets the patient city.
        /// </summary>
        /// <value>The patient city.</value>
        [DataMember]
        public string PatientCity
        {
            get { return _patientCity; }
            set { ApplyPropertyChange ( ref _patientCity, () => PatientCity, value ); }
        }

        /// <summary>
        /// Gets or sets the patient date of birth.
        /// </summary>
        /// <value>The patient date of birth.</value>
        [DataMember]
        public DateTime? PatientDateOfBirth
        {
            get { return _patientDateOfBirth; }
            set { ApplyPropertyChange ( ref _patientDateOfBirth, () => PatientDateOfBirth, value ); }
        }

        /// <summary>
        /// Gets or sets the first name of the patient.
        /// </summary>
        /// <value>The first name of the patient.</value>
        [DataMember]
        public string PatientFirstName
        {
            get { return _patientFirstName; }
            set { ApplyPropertyChange ( ref _patientFirstName, () => PatientFirstName, value ); }
        }

        /// <summary>
        /// Gets or sets the patient gender.
        /// </summary>
        /// <value>The patient gender.</value>
        [DataMember]
        public LookupValueDto PatientGender
        {
            get { return _patientGender; }
            set { ApplyPropertyChange ( ref _patientGender, () => PatientGender, value ); }
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
        /// Gets or sets the last name of the patient.
        /// </summary>
        /// <value>The last name of the patient.</value>
        [DataMember]
        public string PatientLastName
        {
            get { return _patientLastName; }
            set { ApplyPropertyChange ( ref _patientLastName, () => PatientLastName, value ); }
        }

        /// <summary>
        /// Gets or sets the patient phone number.
        /// </summary>
        /// <value>The patient phone number.</value>
        [DataMember]
        public PatientPhoneDto PatientPhoneNumber
        {
            get { return _patientPhoneNumber; }
            set { ApplyPropertyChange ( ref _patientPhoneNumber, () => PatientPhoneNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the patient postal code.
        /// </summary>
        /// <value>The patient postal code.</value>
        [DataMember]
        public string PatientPostalCode
        {
            get { return _patientPostalCode; }
            set { ApplyPropertyChange ( ref _patientPostalCode, () => PatientPostalCode, value ); }
        }

        /// <summary>
        /// Gets or sets the patient prefix.
        /// </summary>
        /// <value>The patient prefix.</value>
        [DataMember]
        public string PatientPrefix
        {
            get { return _patientPrefix; }
            set { ApplyPropertyChange ( ref _patientPrefix, () => PatientPrefix, value ); }
        }

        /// <summary>
        /// Gets or sets the state of the patient.
        /// </summary>
        /// <value>The state of the patient.</value>
        [DataMember]
        public string PatientState
        {
            get { return _patientState; }
            set { ApplyPropertyChange ( ref _patientState, () => PatientState, value ); }
        }

        /// <summary>
        /// Gets or sets the patient unique identifier.
        /// </summary>
        /// <value>The patient unique identifier.</value>
        [DataMember]
        public string PatientUniqueIdentifier
        {
            get { return _patientUniqueIdentifier; }
            set { ApplyPropertyChange ( ref _patientUniqueIdentifier, () => PatientUniqueIdentifier, value ); }
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

        /// <summary>
        /// Gets or sets the visit template key.
        /// </summary>
        /// <value>The visit template key.</value>
        [DataMember]
        public long? VisitTemplateKey
        {
            get { return _visitTemplateKey; }
            set { ApplyPropertyChange ( ref _visitTemplateKey, () => VisitTemplateKey, value ); }
        }

        #endregion
    }
}
