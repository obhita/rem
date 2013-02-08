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

using Pillar.Common.Cryptography;
using Pillar.Common.Extension;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientDocument defines a system document that is associated to a patient.
    /// </summary>
    public class PatientDocument : AuditableAggregateRootBase, IPatientAccessAuditable
    {
        private DateRange _clinicalDateRange;
        private string _description;
        private byte[] _document;
        private string _documentHashValue;
        private string _documentProviderName;
        private string _fileName;
        private string _otherDocumentTypeName;
        private Patient _patient;
        private PatientDocumentType _patientDocumentType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDocument"/> class.
        /// </summary>
        protected internal PatientDocument ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientDocument"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="patientDocumentType">Type of the patient document.</param>
        /// <param name="document">The document.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="documentHashValue">The document hash value.</param>
        protected internal PatientDocument (
            Patient patient,
            PatientDocumentType patientDocumentType,
            byte[] document,
            string fileName,
            string documentHashValue )
        {
            Check.IsNotNull ( patient, "Patient is required." );
            Check.IsNotNull ( patientDocumentType, "Patient Document Type is required." );
            Check.IsNotNull ( document, "Document contents are required." );
            Check.IsNotNullOrWhitespace ( fileName, "Filename is required." );
            Check.IsNotNullOrWhitespace ( documentHashValue, "Document Hash is required." );

            _patient = patient;
            _patientDocumentType = patientDocumentType;
            _document = document;
            _fileName = fileName;
            _documentHashValue = documentHashValue;
        }

        /// <summary>
        /// Gets the patient.
        /// </summary>
        [NotNull]
        public virtual Patient Patient
        {
            get { return _patient; }
            private set { ApplyPropertyChange ( ref _patient, () => Patient, value ); }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [NotNull]
        public virtual string FileName
        {
            get { return _fileName; }
            private set { ApplyPropertyChange ( ref _fileName, () => FileName, value ); }
        }

        /// <summary>
        /// Gets the document.
        /// </summary>
        [NotNull]
        public virtual byte[] Document
        {
            get { return _document; }
            private set { ApplyPropertyChange ( ref _document, () => Document, value ); }
        }

        /// <summary>
        /// Gets the name of the document provider.
        /// </summary>
        /// <value>
        /// The name of the document provider.
        /// </value>
        public virtual string DocumentProviderName
        {
            get { return _documentProviderName; }
            private set { ApplyPropertyChange ( ref _documentProviderName, () => DocumentProviderName, value ); }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public virtual string Description
        {
            get { return _description; }
            private set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }

        /// <summary>
        /// Gets the type of the patient document.
        /// </summary>
        /// <value>
        /// The type of the patient document.
        /// </value>
        [NotNull]
        public virtual PatientDocumentType PatientDocumentType
        {
            get { return _patientDocumentType; }
            private set { ApplyPropertyChange ( ref _patientDocumentType, () => PatientDocumentType, value ); }
        }

        /// <summary>
        /// Gets the name of the other document type.
        /// </summary>
        /// <value>
        /// The name of the other document type.
        /// </value>
        public virtual string OtherDocumentTypeName
        {
            get { return _otherDocumentTypeName; }
            private set { ApplyPropertyChange ( ref _otherDocumentTypeName, () => OtherDocumentTypeName, value ); }
        }

        /// <summary>
        /// Gets the clinical date range.
        /// </summary>
        public virtual DateRange ClinicalDateRange
        {
            get { return _clinicalDateRange; }
            private set { ApplyPropertyChange ( ref _clinicalDateRange, () => ClinicalDateRange, value ); }
        }

        /// <summary>
        /// Gets the document hash value.
        /// </summary>
        [NotNull]
        public virtual string DocumentHashValue
        {
            get { return _documentHashValue; }
            private set { ApplyPropertyChange ( ref _documentHashValue, () => DocumentHashValue, value ); }
        }

        /// <summary>
        /// Gets the C32 imported indicator.
        /// </summary>
        public virtual bool? C32ImportedIndicator { get; private set; }

        #region IPatientAccessAuditable Members

        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }

        #endregion

        /// <summary>
        /// Revises the name of the document provider.
        /// </summary>
        /// <param name="documentProviderName">Name of the document provider.</param>
        public virtual void ReviseDocumentProviderName(string documentProviderName)
        {
            DomainRuleEngine.CreateRuleEngine<PatientDocument, string> ( this, () => ReviseDocumentProviderName )
                .WithContext ( documentProviderName )
                .Execute(() => DocumentProviderName = documentProviderName);
        }

        /// <summary>
        /// Revises the name of the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public virtual void ReviseFileName(string fileName)
        {
            Check.IsNotNull(fileName, "FileName is required.");
            FileName = fileName;
        }

        /// <summary>
        /// Revises the document.
        /// </summary>
        /// <param name="document">The document.</param>
        public virtual void ReviseDocument(byte[] document)
        {
            Check.IsNotNull(document, "document is required.");
            Document = document;
            DocumentHashValue = IoC.CurrentContainer.Resolve<IHashingUtility> ().ComputeHash ( document );
        }

        /// <summary>
        /// Revises the description.
        /// </summary>
        /// <param name="description">The description.</param>
        public virtual void ReviseDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Revises the type of the patient document.
        /// </summary>
        /// <param name="patientDocumentType">Type of the patient document.</param>
        public virtual void RevisePatientDocumentType(PatientDocumentType patientDocumentType)
        {
            DomainRuleEngine.CreateRuleEngine<PatientDocument, PatientDocumentType>(this, () => RevisePatientDocumentType)
                .WithContext(patientDocumentType)
                .Execute ( () => PatientDocumentType = patientDocumentType );
        }

        /// <summary>
        /// Revises the name of the other document type.
        /// </summary>
        /// <param name="otherDocumentTypeName">Name of the other document type.</param>
        public virtual void ReviseOtherDocumentTypeName(string otherDocumentTypeName)
        {
            _otherDocumentTypeName = otherDocumentTypeName;
        }

        /// <summary>
        /// Revises the clinical date range.
        /// </summary>
        /// <param name="clinicalDateRange">The clinical date range.</param>
        public virtual void ReviseClinicalDateRange(DateRange clinicalDateRange)
        {
            DomainRuleEngine.CreateRuleEngine<PatientDocument, DateRange>(this, () => ReviseClinicalDateRange)
                .WithContext(clinicalDateRange)
                .Execute ( () => ClinicalDateRange = clinicalDateRange);
        }

        /// <summary>
        /// Revises the C32 imported indicator.
        /// </summary>
        /// <param name="c32ImportedIndicatior">The C32 imported indicatior.</param>
        public virtual void ReviseC32ImportedIndicator(bool? c32ImportedIndicatior)
        {
            C32ImportedIndicator = c32ImportedIndicatior;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return string.Format ( "({0}) - {1}", PatientDocumentType, FileName );
        }
    }
}
