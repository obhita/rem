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
    /// Data transfer object for PatientDocument class.
    /// </summary>
    public class PatientDocumentDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private DateTime? _clinicalEndDate;
        private DateTime? _clinicalStartDate;
        private DateTime _createdDate;
        private string _description;
        private byte[] _document;
        private string _documentProviderName;
        private string _fileName;
        private bool _isEncrypted;
        private string _otherDocumentTypeName;
        private LookupValueDto _patientDocumentType;
        private long _patientKey;
        private bool? _c32ImportedIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the clinical end date.
        /// </summary>
        /// <value>The clinical end date.</value>
        [DataMember]
        public DateTime? ClinicalEndDate
        {
            get { return _clinicalEndDate; }
            set { ApplyPropertyChange ( ref _clinicalEndDate, () => ClinicalEndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the clinical start date.
        /// </summary>
        /// <value>The clinical start date.</value>
        [DataMember]
        public DateTime? ClinicalStartDate
        {
            get { return _clinicalStartDate; }
            set { ApplyPropertyChange ( ref _clinicalStartDate, () => ClinicalStartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [DataMember]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { ApplyPropertyChange ( ref _createdDate, () => CreatedDate, value ); }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description
        {
            get { return _description; }
            set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>The document.</value>
        [DataMember]
        public byte[] Document
        {
            get { return _document; }
            set { ApplyPropertyChange ( ref _document, () => Document, value ); }
        }

        /// <summary>
        /// Gets or sets the document hash value.
        /// </summary>
        /// <value>The document hash value.</value>
        [DataMember]
        public string DocumentHashValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the document provider.
        /// </summary>
        /// <value>The name of the document provider.</value>
        [DataMember]
        public string DocumentProviderName
        {
            get { return _documentProviderName; }
            set { ApplyPropertyChange ( ref _documentProviderName, () => DocumentProviderName, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        [DataMember]
        public string FileName
        {
            get { return _fileName; }
            set { ApplyPropertyChange ( ref _fileName, () => FileName, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is encrypted.
        /// </summary>
        /// <value><c>true</c> if this instance is encrypted; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsEncrypted
        {
            get { return _isEncrypted; }
            set { ApplyPropertyChange ( ref _isEncrypted, () => ClinicalStartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the other document type.
        /// </summary>
        /// <value>The name of the other document type.</value>
        [DataMember]
        public string OtherDocumentTypeName
        {
            get { return _otherDocumentTypeName; }
            set { ApplyPropertyChange ( ref _otherDocumentTypeName, () => OtherDocumentTypeName, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the patient document.
        /// </summary>
        /// <value>The type of the patient document.</value>
        [DataMember]
        public LookupValueDto PatientDocumentType
        {
            get { return _patientDocumentType; }
            set { ApplyPropertyChange ( ref _patientDocumentType, () => PatientDocumentType, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey
        {
            get { return _patientKey; }
            set { ApplyPropertyChange(ref _patientKey, () => PatientKey, value); }
        }

        /// <summary>
        /// Gets or sets the C32 imported indicator.
        /// </summary>
        /// <value>
        /// The C32 imported indicator.
        /// </value>
        [DataMember]
        public bool? C32ImportedIndicator
        {
            get { return _c32ImportedIndicator; }
            set { ApplyPropertyChange(ref _c32ImportedIndicator, () => C32ImportedIndicator, value); }
        }
        #endregion
    }
}
