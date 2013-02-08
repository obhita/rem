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
    /// Data transfer object for PatientIdentifier class.
    /// </summary>
    [DataContract]
    public class PatientIdentifierDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private bool? _activeIndicator;
        private string _description;
        private DateTime? _effectiveDate;
        private DateTime? _expirationDate;
        private string _identifier;
        private PatientContactDto _patientContact;
        private LookupValueDto _patientIdentifierType;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the active indicator.
        /// </summary>
        /// <value>The active indicator.</value>
        [DataMember]
        public bool? ActiveIndicator
        {
            get { return _activeIndicator; }
            set { ApplyPropertyChange ( ref _activeIndicator, () => ActiveIndicator, value ); }
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
        /// Gets or sets the effective date.
        /// </summary>
        /// <value>The effective date.</value>
        [DataMember]
        public DateTime? EffectiveDate
        {
            get { return _effectiveDate; }
            set { ApplyPropertyChange ( ref _effectiveDate, () => EffectiveDate, value ); }
        }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>The expiration date.</value>
        [DataMember]
        public DateTime? ExpirationDate
        {
            get { return _expirationDate; }
            set { ApplyPropertyChange ( ref _expirationDate, () => ExpirationDate, value ); }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        public string Identifier
        {
            get { return _identifier; }
            set { ApplyPropertyChange ( ref _identifier, () => Identifier, value ); }
        }

        /// <summary>
        /// Gets or sets the patient contact.
        /// </summary>
        /// <value>The patient contact.</value>
        [DataMember]
        public PatientContactDto PatientContact
        {
            get { return _patientContact; }
            set { ApplyPropertyChange ( ref _patientContact, () => PatientContact, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the patient identifier.
        /// </summary>
        /// <value>The type of the patient identifier.</value>
        [DataMember]
        public LookupValueDto PatientIdentifierType
        {
            get { return _patientIdentifierType; }
            set { ApplyPropertyChange ( ref _patientIdentifierType, () => PatientIdentifierType, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey { get; set; }

        #endregion
    }
}
