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
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for PatientProfile class.
    /// </summary>
    public class PatientProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private long _agencyKey;
        private DateTime? _birthDate;
        private LookupValueDto _contactPreference;
        private DateTime? _deathDate;
        private string _emailAddress;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private LookupValueDto _patientGender;
        private string _prefixName;
        private string _suffixName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the agency key.
        /// </summary>
        /// <value>The agency key.</value>
        [DataMember]
        public long AgencyKey
        {
            get { return _agencyKey; }
            set { ApplyPropertyChange ( ref _agencyKey, () => AgencyKey, value ); }
        }

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
        /// Gets or sets the contact preference.
        /// </summary>
        /// <value>The contact preference.</value>
        [DataMember]
        public LookupValueDto ContactPreference
        {
            get { return _contactPreference; }
            set { ApplyPropertyChange ( ref _contactPreference, () => ContactPreference, value ); }
        }

        /// <summary>
        /// Gets or sets the death date.
        /// </summary>
        /// <value>The death date.</value>
        [DataMember]
        public DateTime? DeathDate
        {
            get { return _deathDate; }
            set { ApplyPropertyChange ( ref _deathDate, () => DeathDate, value ); }
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
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get
            {
                var nameBuilder = new StringBuilder ();
                nameBuilder.Append ( string.IsNullOrWhiteSpace ( _firstName ) ? string.Empty : _firstName + " " );
                nameBuilder.Append ( string.IsNullOrWhiteSpace ( _middleName ) ? string.Empty : _middleName + " " );
                nameBuilder.Append ( string.IsNullOrWhiteSpace ( _lastName ) ? string.Empty : _lastName );
                nameBuilder.Append ( string.IsNullOrWhiteSpace ( _suffixName ) ? string.Empty : " " + _suffixName );

                return nameBuilder.ToString ();
            }
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

        #region Public Methods

        /// <summary>
        /// Called when [deserialize].
        /// </summary>
        /// <param name="streamingContext">The streaming context.</param>
        [OnDeserializing]
        public void OnDeserialize ( StreamingContext streamingContext )
        {
            InitializeEventHandlers (); // TODO: Remove?
        }

        #endregion

        #region Methods

        private void InitializeEventHandlers ()
        {
            PropertyChanged += OnPatientNameChanged;
        }

        private void OnPatientNameChanged ( object sender, PropertyChangedEventArgs e ) // TODO: Remove?
        {
            IList<string> propNames = new List<string>
                {
                    "FirstName",
                    "MiddleName",
                    "LastName",
                    "SuffixName",
                };
            if ( propNames.Contains ( e.PropertyName ) )
            {
                RaisePropertyChanged ( () => FullName );
            }
        }

        #endregion
    }
}
