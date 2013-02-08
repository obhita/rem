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
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for StaffProfile class.
    /// </summary>
    [DataContract]
    public class StaffProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private DateTime? _birthDate;
        private string _emailAddress;
        private DateTime? _endDate;
        private string _firstName;
        private LookupValueDto _gender;
        private SoftDeleteObservableCollection<StaffLanguageDto> _languages;
        private string _lastName;
        private string _middleName;
        private string _note;
        private string _prefixName;
        private string _professionalCredentialNote;
        private string _socialSecurityNumber;
        private LookupValueDto _staffType;
        private DateTime? _startDate;
        private string _suffixName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the agency key.
        /// </summary>
        /// <value>The agency key.</value>
        [DataMember]
        public long AgencyKey { get; set; }

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        [DataMember]
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set { ApplyPropertyChange ( ref _birthDate, () => BirthDate, value ); }
        }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        [DataMember]
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { ApplyPropertyChange ( ref _emailAddress, () => EmailAddress, value ); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [DataMember]
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
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
        public LookupValueDto Gender
        {
            get { return _gender; }
            set { ApplyPropertyChange ( ref _gender, () => Gender, value ); }
        }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        /// <value>The languages.</value>
        [DataMember]
        public SoftDeleteObservableCollection<StaffLanguageDto> Languages
        {
            get { return _languages; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _languages, () => Languages, value ); }
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
        /// Gets or sets the name of the prefix.
        /// </summary>
        /// <value>The name of the prefix.</value>
        [DataMember]
        public string PrefixName
        {
            get { return _prefixName; }
            set { ApplyPropertyChange ( ref _prefixName, () => PrefixName, value ); }
        }

        /// <summary>
        /// Gets or sets the professional credential note.
        /// </summary>
        /// <value>The professional credential note.</value>
        [DataMember]
        public string ProfessionalCredentialNote
        {
            get { return _professionalCredentialNote; }
            set { ApplyPropertyChange ( ref _professionalCredentialNote, () => ProfessionalCredentialNote, value ); }
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

        /// <summary>
        /// Gets or sets the type of the staff.
        /// </summary>
        /// <value>The type of the staff.</value>
        [DataMember]
        public LookupValueDto StaffType
        {
            get { return _staffType; }
            set { ApplyPropertyChange ( ref _staffType, () => StaffType, value ); }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [DataMember]
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { ApplyPropertyChange ( ref _startDate, () => StartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the suffix.
        /// </summary>
        /// <value>The name of the suffix.</value>
        [DataMember]
        public string SuffixName
        {
            get { return _suffixName; }
            set { ApplyPropertyChange ( ref _suffixName, () => SuffixName, value ); }
        }

        #endregion
    }
}
