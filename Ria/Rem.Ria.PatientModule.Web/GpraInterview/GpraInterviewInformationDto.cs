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

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraInterviewInformation class.
    /// </summary>
    public class GpraInterviewInformationDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private DateTime _appointmentStartDateTime;
        private int? _assistAlcoholSubScore;
        private int? _auditCScore;
        private int? _cageScore;
        private bool? _conductedInterviewIndicator;
        private string _contractGrantIdentifier;
        private bool? _cooccuringMhSaScreenerIndicator;
        private int? _dast10Score;
        private int? _dastScore;
        private LookupValueDto _gpraInterviewType;
        private LookupValueDto _gpraPatientType;
        private int? _niaaaGuideScore;
        private int? _otherScore;
        private string _otherSpecificationDescription;
        private string _patientUniqueIdentifier;
        private bool? _positiveCooccuringMhSaScreenerIndicator;
        private bool? _sbirtSbiPositiveIndicator;
        private bool? _sbirtWillingIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the appointment start date time.
        /// </summary>
        /// <value>The appointment start date time.</value>
        public DateTime AppointmentStartDateTime
        {
            get { return _appointmentStartDateTime; }
            set { ApplyPropertyChange ( ref _appointmentStartDateTime, () => AppointmentStartDateTime, value ); }
        }

        /// <summary>
        /// Gets or sets the assist alcohol sub score.
        /// </summary>
        /// <value>The assist alcohol sub score.</value>
        public int? AssistAlcoholSubScore
        {
            get { return _assistAlcoholSubScore; }
            set { ApplyPropertyChange ( ref _assistAlcoholSubScore, () => AssistAlcoholSubScore, value ); }
        }

        /// <summary>
        /// Gets or sets the audit C score.
        /// </summary>
        /// <value>The audit C score.</value>
        public int? AuditCScore
        {
            get { return _auditCScore; }
            set { ApplyPropertyChange ( ref _auditCScore, () => AuditCScore, value ); }
        }

        /// <summary>
        /// Gets or sets the cage score.
        /// </summary>
        /// <value>The cage score.</value>
        public int? CageScore
        {
            get { return _cageScore; }
            set { ApplyPropertyChange ( ref _cageScore, () => CageScore, value ); }
        }

        /// <summary>
        /// Did you conduct an interview?
        /// </summary>
        /// <value>The conducted interview indicator.</value>
        public bool? ConductedInterviewIndicator
        {
            get { return _conductedInterviewIndicator; }
            set { ApplyPropertyChange ( ref _conductedInterviewIndicator, () => ConductedInterviewIndicator, value ); }
        }

        /// <summary>
        /// Contract/Grant ID
        /// </summary>
        /// <value>The contract grant identifier.</value>
        public string ContractGrantIdentifier
        {
            get { return _contractGrantIdentifier; }
            set { ApplyPropertyChange ( ref _contractGrantIdentifier, () => ContractGrantIdentifier, value ); }
        }

        /// <summary>
        /// Was the client screened by your program for co-occurring mental health and substance use disorders?
        /// </summary>
        /// <value>The cooccuring mh sa screener indicator.</value>
        public bool? CooccuringMhSaScreenerIndicator
        {
            get { return _cooccuringMhSaScreenerIndicator; }
            set { ApplyPropertyChange ( ref _cooccuringMhSaScreenerIndicator, () => CooccuringMhSaScreenerIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the dast10 score.
        /// </summary>
        /// <value>The dast10 score.</value>
        public int? Dast10Score
        {
            get { return _dast10Score; }
            set { ApplyPropertyChange ( ref _dast10Score, () => Dast10Score, value ); }
        }

        /// <summary>
        /// Gets or sets the dast score.
        /// </summary>
        /// <value>The dast score.</value>
        public int? DastScore
        {
            get { return _dastScore; }
            set { ApplyPropertyChange ( ref _dastScore, () => DastScore, value ); }
        }

        /// <summary>
        /// Interview Type
        /// </summary>
        /// <value>The type of the gpra interview.</value>
        public LookupValueDto GpraInterviewType
        {
            get { return _gpraInterviewType; }
            set { ApplyPropertyChange ( ref _gpraInterviewType, () => GpraInterviewType, value ); }
        }

        /// <summary>
        /// Patient Type
        /// </summary>
        /// <value>The type of the gpra patient.</value>
        public LookupValueDto GpraPatientType
        {
            get { return _gpraPatientType; }
            set { ApplyPropertyChange ( ref _gpraPatientType, () => GpraPatientType, value ); }
        }

        /// <summary>
        /// Gets or sets the niaaa guide score.
        /// </summary>
        /// <value>The niaaa guide score.</value>
        public int? NiaaaGuideScore
        {
            get { return _niaaaGuideScore; }
            set { ApplyPropertyChange ( ref _niaaaGuideScore, () => NiaaaGuideScore, value ); }
        }

        /// <summary>
        /// Gets or sets the other score.
        /// </summary>
        /// <value>The other score.</value>
        public int? OtherScore
        {
            get { return _otherScore; }
            set { ApplyPropertyChange ( ref _otherScore, () => OtherScore, value ); }
        }

        /// <summary>
        /// Gets or sets the other specification description.
        /// </summary>
        /// <value>The other specification description.</value>
        public string OtherSpecificationDescription
        {
            get { return _otherSpecificationDescription; }
            set { ApplyPropertyChange ( ref _otherSpecificationDescription, () => OtherSpecificationDescription, value ); }
        }

        /// <summary>
        /// Gets or sets the patient unique identifier.
        /// </summary>
        /// <value>The patient unique identifier.</value>
        public string PatientUniqueIdentifier
        {
            get { return _patientUniqueIdentifier; }
            set { ApplyPropertyChange ( ref _patientUniqueIdentifier, () => PatientUniqueIdentifier, value ); }
        }

        /// <summary>
        /// Did the client screen positive for co-occurring mental health and substance use disorders?
        /// </summary>
        /// <value>The positive cooccuring mh sa screener indicator.</value>
        public bool? PositiveCooccuringMhSaScreenerIndicator
        {
            get { return _positiveCooccuringMhSaScreenerIndicator; }
            set { ApplyPropertyChange ( ref _positiveCooccuringMhSaScreenerIndicator, () => PositiveCooccuringMhSaScreenerIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the sbirt sbi positive indicator.
        /// </summary>
        /// <value>The sbirt sbi positive indicator.</value>
        public bool? SbirtSbiPositiveIndicator
        {
            get { return _sbirtSbiPositiveIndicator; }
            set { ApplyPropertyChange ( ref _sbirtSbiPositiveIndicator, () => SbirtSbiPositiveIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the sbirt willing indicator.
        /// </summary>
        /// <value>The sbirt willing indicator.</value>
        public bool? SbirtWillingIndicator
        {
            get { return _sbirtWillingIndicator; }
            set { ApplyPropertyChange ( ref _sbirtWillingIndicator, () => SbirtWillingIndicator, value ); }
        }

        #endregion
    }
}
