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
    /// Data transfer object for PatientContactProfile class.
    /// </summary>
    [DataContract]
    public class PatientContactProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private DateTime? _birthDate;
        private bool? _canContactIndicator;
        private DateTime? _consentExpirationDate;
        private bool? _consentOnFileIndicator;
        private bool? _desigantedFollowUpIndicator;
        private bool? _emergencyIndicator;
        private string _firstName;
        private LookupValueDto _gender;
        private string _lastName;
        private LookupValueDto _legalAuthorizationType;
        private string _middleName;
        private string _note;
        private bool? _primaryIndicator;
        private LookupValueDto _patientContactRelationshipType;
        private string _socialSecurityNumber;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        [DataMember]
        public virtual DateTime? BirthDate
        {
            get { return _birthDate; }
            set { ApplyPropertyChange ( ref _birthDate, () => BirthDate, value ); }
        }

        /// <summary>
        /// Gets or sets the can contact indicator.
        /// </summary>
        /// <value>The can contact indicator.</value>
        [DataMember]
        public bool? CanContactIndicator
        {
            get { return _canContactIndicator; }

            set { ApplyPropertyChange ( ref _canContactIndicator, () => CanContactIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the consent expiration date.
        /// </summary>
        /// <value>The consent expiration date.</value>
        [DataMember]
        public DateTime? ConsentExpirationDate
        {
            get { return _consentExpirationDate; }

            set { ApplyPropertyChange ( ref _consentExpirationDate, () => ConsentExpirationDate, value ); }
        }

        /// <summary>
        /// Gets or sets the consent on file indicator.
        /// </summary>
        /// <value>The consent on file indicator.</value>
        [DataMember]
        public bool? ConsentOnFileIndicator
        {
            get { return _consentOnFileIndicator; }

            set { ApplyPropertyChange ( ref _consentOnFileIndicator, () => ConsentOnFileIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the designated follow up indicator.
        /// </summary>
        /// <value>The designated follow up indicator.</value>
        [DataMember]
        public virtual bool? DesignatedFollowUpIndicator
        {
            get { return _desigantedFollowUpIndicator; }
            set { ApplyPropertyChange ( ref _desigantedFollowUpIndicator, () => DesignatedFollowUpIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the emergency indicator.
        /// </summary>
        /// <value>The emergency indicator.</value>
        [DataMember]
        public virtual bool? EmergencyIndicator
        {
            get { return _emergencyIndicator; }
            set { ApplyPropertyChange ( ref _emergencyIndicator, () => EmergencyIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [DataMember]
        public string FirstName
        {
            get { return _firstName; }

            set { ApplyPropertyChange ( ref _firstName, () => FirstName, value ); }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        [DataMember]
        public virtual LookupValueDto Gender
        {
            get { return _gender; }
            set { ApplyPropertyChange ( ref _gender, () => Gender, value ); }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [DataMember]
        public string LastName
        {
            get { return _lastName; }

            set { ApplyPropertyChange ( ref _lastName, () => LastName, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the legal authorization.
        /// </summary>
        /// <value>The type of the legal authorization.</value>
        [DataMember]
        public LookupValueDto LegalAuthorizationType
        {
            get { return _legalAuthorizationType; }

            set { ApplyPropertyChange ( ref _legalAuthorizationType, () => LegalAuthorizationType, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        [DataMember]
        public string MiddleName
        {
            get { return _middleName; }

            set { ApplyPropertyChange ( ref _middleName, () => MiddleName, value ); }
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note for the profile.</value>
        [DataMember]
        public string Note
        {
            get { return _note; }

            set { ApplyPropertyChange ( ref _note, () => Note, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey { get; set; }

        /// <summary>
        /// Gets or sets the primary indicator.
        /// </summary>
        /// <value>The primary indicator.</value>
        [DataMember]
        public bool? PrimaryIndicator
        {
            get { return _primaryIndicator; }

            set { ApplyPropertyChange ( ref _primaryIndicator, () => PrimaryIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the relationship.
        /// </summary>
        /// <value>The type of the relationship.</value>
        [DataMember]
        public LookupValueDto PatientContactRelationshipType
        {
            get { return _patientContactRelationshipType; }

            set { ApplyPropertyChange ( ref _patientContactRelationshipType, () => PatientContactRelationshipType, value ); }
        }

        /// <summary>
        /// Gets or sets the social security number.
        /// </summary>
        /// <value>The social security number.</value>
        [DataMember]
        public string SocialSecurityNumber
        {
            get { return _socialSecurityNumber; }

            set { ApplyPropertyChange ( ref _socialSecurityNumber, () => SocialSecurityNumber, value ); }
        }

        #endregion
    }
}
