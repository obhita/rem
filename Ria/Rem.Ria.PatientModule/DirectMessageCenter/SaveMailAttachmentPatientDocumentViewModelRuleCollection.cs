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

using Pillar.Common.Metadata;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.DirectMessageCenter;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// Contains client side business rules for TEDS admission Interview.
    /// </summary>
    public class SaveMailAttachmentPatientDocumentViewModelRuleCollection : AbstractRuleCollection<SaveMailAttachmentPatientDocumentViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveMailAttachmentPatientDocumentViewModelRuleCollection"/> class.
        /// </summary>
        public SaveMailAttachmentPatientDocumentViewModelRuleCollection ()
        {
            // Validation rules
            var patientDocumentTypeIsRequiredRuleError = new DataErrorInfo(
                "Patient documentType type is required.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object>(dto => dto.PatientDocumentType));

            NewRule(() => PatientDocumentTypeIsRequiredRule)
                .RunForProperty(s => s.EditingDto.PatientDocumentType)
                .When(
                    s => s.EditingDto.PatientDocumentType == null
                )
                .ThenReportRuleViolation(patientDocumentTypeIsRequiredRuleError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(patientDocumentTypeIsRequiredRuleError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(patientDocumentTypeIsRequiredRuleError));

            //var patientIsRequiredRuleError = new DataErrorInfo(
            //    "Patient is required.",
            //    ErrorLevel.Error,
            //    PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object>(dto => dto.Patient));

            //NewRule(() => PatientIsRequiredRule)
            //    .RunForProperty(s => s.EditingDto.PatientKey)
            //    .When(s => s.EditingDto.PatientKey == 0)
            //    .ThenReportRuleViolation(patientIsRequiredRuleError.Message)
            //    .Then(s => s.EditingDto.TryAddDataErrorInfo(patientIsRequiredRuleError))
            //    .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(patientIsRequiredRuleError));

            var clinicalEndDateMustNotBeBeforeClinicalStartDateError = new DataErrorInfo(
                "Clinical end date cannot be before start date.",
                ErrorLevel.Error,
                PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object>(dto => dto.ClinicalEndDate),
                PropertyUtil.ExtractPropertyName<MailAttachmentPatientDocumentDto, object>(dto => dto.ClinicalStartDate));

            NewRule(() => ClinicalEndDateMustNotBeBeforeClinicalStartDateRule)
                .RunForProperty(s => s.EditingDto.ClinicalStartDate)
                .RunForProperty(s => s.EditingDto.ClinicalEndDate)
                .When(
                    s => s.EditingDto.ClinicalStartDate != null &&
                         s.EditingDto.ClinicalEndDate != null &&
                         s.EditingDto.ClinicalStartDate > s.EditingDto.ClinicalEndDate
                )
                .ThenReportRuleViolation(clinicalEndDateMustNotBeBeforeClinicalStartDateError.Message)
                .Then(s => s.EditingDto.TryAddDataErrorInfo(clinicalEndDateMustNotBeBeforeClinicalStartDateError))
                .ElseThen(s => s.EditingDto.RemoveDataErrorInfo(clinicalEndDateMustNotBeBeforeClinicalStartDateError));

            BuildPresentaionRules();
        }

        /// <summary>
        /// Gets or sets the patient document type is required rule.
        /// </summary>
        /// <value>
        /// The patient document type is required rule.
        /// </value>
        public IRule PatientDocumentTypeIsRequiredRule { get; set; }

        /// <summary>
        /// Gets or sets the patient is required rule.
        /// </summary>
        /// <value>
        /// The patient is required rule.
        /// </value>
        public IRule PatientIsRequiredRule { get; set; }

        /// <summary>
        /// Gets or sets the clinical end date must not be before clinical start date rule.
        /// </summary>
        /// <value>
        /// The clinical end date must not be before clinical start date rule.
        /// </value>
        public IRule ClinicalEndDateMustNotBeBeforeClinicalStartDateRule { get; set; }

        #region --- Begin Presentation Rules ---

        /// <summary>
        /// Gets or sets the gender information presentation rule.
        /// </summary>
        /// <value>
        /// The gender information presentation rule.
        /// </value>
        public IRule OtherPatientDocumentTyPepresentationRule { get; set; }

        #endregion --- End Presentation Rules ---

        /// <summary>
        /// Builds the presentation rules.
        /// </summary>
        public void BuildPresentaionRules()
        {
            NewRule(() => OtherPatientDocumentTyPepresentationRule)
                .RunForProperty(s => s.EditingDto.PatientDocumentType)
                .When(
                    s => s.EditingDto.PatientDocumentType != null && s.EditingDto.PatientDocumentType.WellKnownName == WellKnownNames.PatientModule.PatientDocumentType.Other
                        )
                .Then(
                    s =>
                    {
                        s.EditingDto.Enable(dto => dto.OtherDocumentTypeName);
                    })
                .ElseThen(
                    s =>
                    {
                        s.EditingDto.Disable(dto => dto.OtherDocumentTypeName);
                    });
        }
    }
}
