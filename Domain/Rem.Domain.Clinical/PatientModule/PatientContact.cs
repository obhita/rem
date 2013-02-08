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
using System.Linq;
using System.Text;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientContact defines a patient contact association.
    /// </summary>
    public class PatientContact : AuditableAggregateRootBase, IPatientAccessAuditable
    {
        private readonly IList<PatientContactContactType> _contactTypes;
        private readonly IList<PatientContactPhone> _phoneNumbers;
        private DateTime? _birthDate;
        private bool? _canContactIndicator;
        private string _cityName;
        private DateTime? _consentExpirationDate;
        private bool? _consentOnFileIndicator;
        private Country _country;
        private CountyArea _countyArea;
        private bool? _designatedFollowUpIndicator;
        private string _emailAddress;
        private bool? _emergencyIndicator;
        private string _firstName;
        private string _firstStreetAddress;
        private Gender _gender;
        private string _lastName;

        private LegalAuthorizationType _legalAuthorizationType;
        private string _middleName;
        private string _note;
        private Patient _patient;
        private string _postalCode;
        private bool? _primaryIndicator;
        private PatientContactRelationshipType _patientContactRelationshipType;
        private string _secondStreetAddress;
        private string _socialSecurityNumber;
        private StateProvince _stateProvince;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContact"/> class.
        /// </summary>
        protected PatientContact ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContact"/> class.
        /// </summary>
        /// <param name="patient">The patient.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        protected internal PatientContact ( Patient patient, string firstName, string lastName )
        {
            Check.IsNotNull ( patient, "Patient is required." );
            Check.IsNotNullOrWhitespace ( firstName, "First name is required." );
            Check.IsNotNullOrWhitespace ( lastName, "Last name is required." );

            _patient = patient;
            _firstName = firstName;
            _lastName = lastName;

            _phoneNumbers = new List<PatientContactPhone> ();
            _contactTypes = new List<PatientContactContactType> ();
        }

        #endregion

        #region Public Properties

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
        /// Gets the first name.
        /// </summary>
        [NotNull]
        public virtual string FirstName
        {
            get { return _firstName; }
            private set { ApplyPropertyChange ( ref _firstName, () => FirstName, value ); }
        }

        /// <summary>
        /// Gets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public virtual string MiddleName
        {
            get { return _middleName; }
            private set { ApplyPropertyChange ( ref _middleName, () => MiddleName, value ); }
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        [NotNull]
        public virtual string LastName
        {
            get { return _lastName; }
            private set { ApplyPropertyChange ( ref _lastName, () => LastName, value ); }
        }

        /// <summary>
        /// Gets the first street address.
        /// </summary>
        public virtual string FirstStreetAddress
        {
            get { return _firstStreetAddress; }
            private set { ApplyPropertyChange ( ref _firstStreetAddress, () => FirstStreetAddress, value ); }
        }

        /// <summary>
        /// Gets the second street address.
        /// </summary>
        public virtual string SecondStreetAddress
        {
            get { return _secondStreetAddress; }
            private set { ApplyPropertyChange ( ref _secondStreetAddress, () => SecondStreetAddress, value ); }
        }

        /// <summary>
        /// Gets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public virtual string CityName
        {
            get { return _cityName; }
            private set { ApplyPropertyChange ( ref _cityName, () => CityName, value ); }
        }

        /// <summary>
        /// Gets the postal code.
        /// </summary>
        public virtual string PostalCode
        {
            get { return _postalCode; }
            private set { ApplyPropertyChange ( ref _postalCode, () => PostalCode, value ); }
        }

        /// <summary>
        /// Gets the county area.
        /// </summary>
        public virtual CountyArea CountyArea
        {
            get { return _countyArea; }
            private set { ApplyPropertyChange ( ref _countyArea, () => CountyArea, value ); }
        }

        /// <summary>
        /// Gets the state province.
        /// </summary>
        public virtual StateProvince StateProvince
        {
            get { return _stateProvince; }
            private set { ApplyPropertyChange ( ref _stateProvince, () => StateProvince, value ); }
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public virtual Country Country
        {
            get { return _country; }
            private set { ApplyPropertyChange ( ref _country, () => Country, value ); }
        }

        /// <summary>
        /// Gets the name of the complete.
        /// </summary>
        /// <value>
        /// The name of the complete.
        /// </value>
        [IgnoreMapping]
        public virtual string CompleteName
        {
            get
            {
                var nameBuilder = new StringBuilder ();
                nameBuilder.Append ( string.IsNullOrWhiteSpace ( FirstName ) ? string.Empty : FirstName.Trim () + " " );
                nameBuilder.Append ( string.IsNullOrWhiteSpace ( MiddleName ) ? string.Empty : MiddleName.Trim () + " " );
                nameBuilder.Append ( string.IsNullOrWhiteSpace ( LastName ) ? string.Empty : LastName.Trim () );
                return nameBuilder.ToString ();
            }
        }

        /// <summary>
        /// Gets the primary indicator.
        /// </summary>
        public virtual bool? PrimaryIndicator
        {
            get { return _primaryIndicator; }
            private set { ApplyPropertyChange ( ref _primaryIndicator, () => PrimaryIndicator, value ); }
        }

        /// <summary>
        /// Gets the type of the relationship.
        /// </summary>
        /// <value>
        /// The type of the relationship.
        /// </value>
        public virtual PatientContactRelationshipType PatientContactRelationshipType
        {
            get { return _patientContactRelationshipType; }
            private set { ApplyPropertyChange ( ref _patientContactRelationshipType, () => PatientContactRelationshipType, value ); }
        }

        /// <summary>
        /// Gets the type of the legal authorization.
        /// </summary>
        /// <value>
        /// The type of the legal authorization.
        /// </value>
        public virtual LegalAuthorizationType LegalAuthorizationType
        {
            get { return _legalAuthorizationType; }
            private set { ApplyPropertyChange ( ref _legalAuthorizationType, () => LegalAuthorizationType, value ); }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        public virtual string Note
        {
            get { return _note; }
            private set { ApplyPropertyChange ( ref _note, () => Note, value ); }
        }

        /// <summary>
        /// Gets the consent expiration date.
        /// </summary>
        public virtual DateTime? ConsentExpirationDate
        {
            get { return _consentExpirationDate; }
            private set { ApplyPropertyChange ( ref _consentExpirationDate, () => ConsentExpirationDate, value ); }
        }

        /// <summary>
        /// Gets the can contact indicator.
        /// </summary>
        public virtual bool? CanContactIndicator
        {
            get { return _canContactIndicator; }
            private set { ApplyPropertyChange ( ref _canContactIndicator, () => CanContactIndicator, value ); }
        }

        /// <summary>
        /// Gets the consent on file indicator.
        /// </summary>
        public virtual bool? ConsentOnFileIndicator
        {
            get { return _consentOnFileIndicator; }
            private set { ApplyPropertyChange ( ref _consentOnFileIndicator, () => ConsentOnFileIndicator, value ); }
        }

        /// <summary>
        /// Gets the social security number.
        /// </summary>
        public virtual string SocialSecurityNumber
        {
            get { return _socialSecurityNumber; }
            private set { ApplyPropertyChange ( ref _socialSecurityNumber, () => SocialSecurityNumber, value ); }
        }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public virtual string EmailAddress
        {
            get { return _emailAddress; }
            private set { ApplyPropertyChange ( ref _emailAddress, () => EmailAddress, value ); }
        }

        /// <summary>
        /// Gets the phone numbers.
        /// </summary>
        public virtual IEnumerable<PatientContactPhone> PhoneNumbers
        {
            get { return _phoneNumbers; }
            private set { }
        }

        /// <summary>
        /// Gets the contact types.
        /// </summary>
        public virtual IEnumerable<PatientContactContactType> ContactTypes
        {
            get { return _contactTypes; }
            private set { }
        }

        /// <summary>
        /// Gets the emergency indicator.
        /// </summary>
        public virtual bool? EmergencyIndicator
        {
            get { return _emergencyIndicator; }
            private set { ApplyPropertyChange ( ref _emergencyIndicator, () => EmergencyIndicator, value ); }
        }

        /// <summary>
        /// Gets the designated follow up indicator.
        /// </summary>
        public virtual bool? DesignatedFollowUpIndicator
        {
            get { return _designatedFollowUpIndicator; }
            private set { ApplyPropertyChange ( ref _designatedFollowUpIndicator, () => DesignatedFollowUpIndicator, value ); }
        }

        /// <summary>
        /// Gets the gender.
        /// </summary>
        public virtual Gender Gender
        {
            get { return _gender; }
            private set { ApplyPropertyChange ( ref _gender, () => Gender, value ); }
        }

        #endregion

        /// <summary>
        /// Gets the birth date.
        /// </summary>
        public virtual DateTime? BirthDate
        {
            get { return _birthDate; }
            private set { ApplyPropertyChange ( ref _birthDate, () => BirthDate, value ); }
        }

        #region Implementation of IPatientAccessAuditable

        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return Patient; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Revises the first name.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        public virtual void ReviseFirstName ( string firstName )
        {
            Check.IsNotNullOrWhitespace( firstName, "first name is required.");
            FirstName = firstName;
        }

        /// <summary>
        /// Revises the name of the middle.
        /// </summary>
        /// <param name="middleName">Name of the middle.</param>
        public virtual void ReviseMiddleName ( string middleName )
        {
            MiddleName = middleName;
        }

        /// <summary>
        /// Revises the last name.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        public virtual void ReviseLastName ( string lastName )
        {
            Check.IsNotNullOrWhitespace( lastName, "last name is required.");
            LastName = lastName;
        }

        /// <summary>
        /// Revises the first street address.
        /// </summary>
        /// <param name="firstStreetAddress">The first street address.</param>
        public virtual void ReviseFirstStreetAddress ( string firstStreetAddress )
        {
            FirstStreetAddress = firstStreetAddress;
        }

        /// <summary>
        /// Revises the second street address.
        /// </summary>
        /// <param name="secondStreetAddress">The second street address.</param>
        public virtual void ReviseSecondStreetAddress ( string secondStreetAddress )
        {
            SecondStreetAddress = secondStreetAddress;
        }

        /// <summary>
        /// Revises the name of the city.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        public virtual void ReviseCityName ( string cityName )
        {
            CityName = cityName;
        }

        /// <summary>
        /// Revises the postal code.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        public virtual void RevisePostalCode ( string postalCode )
        {
            PostalCode = postalCode;
        }

        /// <summary>
        /// Revises the county area.
        /// </summary>
        /// <param name="countyArea">The county area.</param>
        public virtual void ReviseCountyArea ( CountyArea countyArea )
        {
            CountyArea = countyArea;
        }

        /// <summary>
        /// Revises the state province.
        /// </summary>
        /// <param name="stateProvince">The state province.</param>
        public virtual void ReviseStateProvince ( StateProvince stateProvince )
        {
            StateProvince = stateProvince;
        }

        /// <summary>
        /// Revises the country.
        /// </summary>
        /// <param name="country">The country.</param>
        public virtual void ReviseCountry ( Country country )
        {
            Country = country;
        }

        /// <summary>
        /// Revises the primary indicator.
        /// </summary>
        /// <param name="primaryIndicator">The primary indicator.</param>
        public virtual void RevisePrimaryIndicator ( bool? primaryIndicator )
        {
            PrimaryIndicator = primaryIndicator;
        }

        /// <summary>
        /// Revises the type of the relationship.
        /// </summary>
        /// <param name="patientContactRelationshipType">Type of the relationship.</param>
        public virtual void RevisePatientContactRelationshipType ( PatientContactRelationshipType patientContactRelationshipType )
        {
            DomainRuleEngine.CreateRuleEngine(this, "RevisePatienContactRelationshipTypeRuleSet")
                .WithContext(patientContactRelationshipType)
                .Execute(() => PatientContactRelationshipType = patientContactRelationshipType);
        }

        /// <summary>
        /// Revises the type of the legal authorization.
        /// </summary>
        /// <param name="legalAuthorizationType">Type of the legal authorization.</param>
        public virtual void ReviseLegalAuthorizationType ( LegalAuthorizationType legalAuthorizationType )
        {
            LegalAuthorizationType = legalAuthorizationType;
        }

        /// <summary>
        /// Revises the note.
        /// </summary>
        /// <param name="note">The note.</param>
        public virtual void ReviseNote ( string note )
        {
            Note = note;
        }

        /// <summary>
        /// Revises the consent expiration date.
        /// </summary>
        /// <param name="consentExpirationDate">The consent expiration date.</param>
        public virtual void ReviseConsentExpirationDate ( DateTime? consentExpirationDate )
        {
            ConsentExpirationDate = consentExpirationDate;
        }

        /// <summary>
        /// Revises the can contact indicator.
        /// </summary>
        /// <param name="canContactIndicator">The can contact indicator.</param>
        public virtual void ReviseCanContactIndicator ( bool? canContactIndicator )
        {
            CanContactIndicator = canContactIndicator;
        }

        /// <summary>
        /// Revises the consent on file indicator.
        /// </summary>
        /// <param name="consentOnFileIndicator">The consent on file indicator.</param>
        public virtual void ReviseConsentOnFileIndicator ( bool? consentOnFileIndicator )
        {
            ConsentOnFileIndicator = consentOnFileIndicator;
        }

        /// <summary>
        /// Revises the social security number.
        /// </summary>
        /// <param name="socialSecurityNumber">The social security number.</param>
        public virtual void ReviseSocialSecurityNumber ( string socialSecurityNumber )
        {
            SocialSecurityNumber = socialSecurityNumber;
        }

        /// <summary>
        /// Revises the email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        public virtual void ReviseEmailAddress ( string emailAddress )
        {
            EmailAddress = emailAddress;
        }

        /// <summary>
        /// Revises the emergency indicator.
        /// </summary>
        /// <param name="emergencyIndicator">The emergency indicator.</param>
        public virtual void ReviseEmergencyIndicator ( bool? emergencyIndicator )
        {
            EmergencyIndicator = emergencyIndicator;
        }

        /// <summary>
        /// Revises the designated follow up indicator.
        /// </summary>
        /// <param name="designatedFollowUpIndicator">The designated follow up indicator.</param>
        public virtual void ReviseDesignatedFollowUpIndicator ( bool? designatedFollowUpIndicator )
        {
            DesignatedFollowUpIndicator = designatedFollowUpIndicator;
        }

        /// <summary>
        /// Revises the gender.
        /// </summary>
        /// <param name="gender">The gender.</param>
        public virtual void ReviseGender ( Gender gender )
        {
            Gender = gender;
        }

        /// <summary>
        /// Revises the birth date.
        /// </summary>
        /// <param name="birthDate">The birth date.</param>
        public virtual void ReviseBirthDate ( DateTime? birthDate )
        {
            BirthDate = birthDate;
        }

        /// <summary>
        /// Renames the patient contact.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        public virtual void RenamePatientContact ( string firstName = null, string middleName = null, string lastName = null )
        {
            string oldCompleteName = CompleteName;

            FirstName = ( firstName ?? _firstName );
            MiddleName = ( middleName ?? _middleName );
            LastName = ( lastName ?? _lastName );

            if ( oldCompleteName != CompleteName )
            {
                NotifyPropertyChanged ( () => CompleteName, oldCompleteName, CompleteName );
            }
        }

        /// <summary>
        /// Adds the type of the contact.
        /// </summary>
        /// <param name="contactType">Type of the contact.</param>
        public virtual void AddContactType ( PatientContactType contactType )
        {
            if ( _contactTypes.FirstOrDefault ( p => p.PatientContactType == contactType ) != null )
            {
                throw new Exception ( "Contact Type already exists for PatientContact." );
            }

            _contactTypes.Add ( new PatientContactContactType ( this, contactType ) );

            NotifyItemAdded ( () => ContactTypes, contactType );
        }

        /// <summary>
        /// Removes the type of the contact.
        /// </summary>
        /// <param name="contactType">Type of the contact.</param>
        public virtual void RemoveContactType ( PatientContactType contactType )
        {
            PatientContactContactType patientContactType = _contactTypes.FirstOrDefault ( p => p.PatientContactType == contactType );

            if ( patientContactType == null )
            {
                throw new Exception ( "Contact Type does not contain PatientContactType." );
            }

            _contactTypes.Remove ( patientContactType );
            NotifyItemRemoved ( () => ContactTypes, contactType );
        }

        /// <summary>
        /// Adds the contact phone.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="patientContactPhoneType">Type of the patient contact phone.</param>
        /// <param name="phoneExtensionNumber">The phone extension number.</param>
        /// <param name="confidentialIndicator">The confidential indicator.</param>
        /// <returns>A PatientContactPhone.</returns>
        public virtual PatientContactPhone AddContactPhone ( string phoneNumber, PatientContactPhoneType patientContactPhoneType, string phoneExtensionNumber, bool? confidentialIndicator = null )
        {
            var patientContactPhone = new PatientContactPhone ( patientContactPhoneType, phoneNumber, phoneExtensionNumber, confidentialIndicator ) { PatientContact = this };

            _phoneNumbers.Add ( patientContactPhone );
            NotifyItemAdded ( () => PhoneNumbers, patientContactPhone );
            return patientContactPhone;
        }

        /// <summary>
        /// Removes the contact phone.
        /// </summary>
        /// <param name="patientContactPhone">The patient contact phone.</param>
        public virtual void RemoveContactPhone ( PatientContactPhone patientContactPhone )
        {
            _phoneNumbers.Remove ( patientContactPhone );
            NotifyItemRemoved ( () => PhoneNumbers, patientContactPhone );
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return CompleteName;
        }

        #endregion
    }
}