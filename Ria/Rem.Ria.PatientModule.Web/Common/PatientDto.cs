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
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.Ria.PatientModule.Web.PatientEditor;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for Patient class.
    /// </summary>
    [DataContract]
    public class PatientDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private ObservableCollection<PatientAlertDto> _alerts;
        private LookupValueDto _educationStatus;
        private LookupValueDto _maritalStatus;
        private ObservableCollection<MedicationDto> _medications;
        private PatientAliasesDto _patientAliases;
        private PatientAllergiesDto _patientAllergies;
        private PatientImportedAllergiesDto _patientImportedAllergies;
        private PatientConfidentialInformationDto _patientConfidentialInformationDto;
        private PatientContactsDto _patientContacts;
        private PatientDemographicDetailsDto _patientDemographicDetails;
        private PatientIdentifiersDto _patientIdentifiers;
        private PatientLegalStatusDto _patientLegalStatus;
        private PatientOtherConsiderationsDto _patientOtherConsiderations;
        private PatientPhoneNumbersDto _patientPhoneNumbers;
        private PatientProfileDto _patientProfile;
        private PatientRaceAndEthnicityDto _patientRaceAndEthnicity;
        private PatientAddressesDto _patientaddresses;
        private LookupValueDto _recordStatus;
        private LookupValueDto _religiousAffiliation;
        private long _revisedAccountKey;
        private DateTimeOffset? _revisedTimestamp;
        private PatientVeteranInformationDto _veteranInformation;
        private string _zipCode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        /// <value>The alerts.</value>
        [DataMember]
        public ObservableCollection<PatientAlertDto> Alerts
        {
            get { return _alerts; }

            set
            {
                _alerts = value;
                RaisePropertyChanged ( () => Alerts );
            }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get
            {
                var name = new StringBuilder ();
                name.Append ( string.IsNullOrWhiteSpace ( PatientProfile.FirstName ) ? string.Empty : PatientProfile.FirstName + " " );
                name.Append ( string.IsNullOrWhiteSpace ( PatientProfile.MiddleName ) ? string.Empty : PatientProfile.MiddleName + " " );
                name.Append ( string.IsNullOrWhiteSpace ( PatientProfile.LastName ) ? string.Empty : PatientProfile.LastName );
                name.Append ( string.IsNullOrWhiteSpace ( PatientProfile.SuffixName ) ? string.Empty : " " + PatientProfile.SuffixName );
                return name.ToString ();
            }
        }

        /// <summary>
        /// Gets or sets the education status.
        /// </summary>
        /// <value>The education status.</value>
        [DataMember]
        public LookupValueDto EducationStatus
        {
            get { return _educationStatus; }

            set
            {
                _educationStatus = value;
                RaisePropertyChanged ( () => EducationStatus );
            }
        }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>The marital status.</value>
        [DataMember]
        public LookupValueDto MaritalStatus
        {
            get { return _maritalStatus; }

            set
            {
                _maritalStatus = value;
                RaisePropertyChanged ( () => MaritalStatus );
            }
        }

        /// <summary>
        /// Gets or sets the medications.
        /// </summary>
        /// <value>The medications.</value>
        [DataMember]
        public ObservableCollection<MedicationDto> Medications
        {
            get { return _medications; }

            set
            {
                _medications = value;
                RaisePropertyChanged ( () => Medications );
            }
        }

        /// <summary>
        /// Gets or sets the patient addresses.
        /// </summary>
        /// <value>The patient addresses.</value>
        [DataMember]
        public PatientAddressesDto PatientAddresses
        {
            get { return _patientaddresses; }
            set
            {
                _patientaddresses = value;
                RaisePropertyChanged ( () => PatientAddresses );
            }
        }

        /// <summary>
        /// Gets or sets the patient aliases.
        /// </summary>
        /// <value>The patient aliases.</value>
        [DataMember]
        public PatientAliasesDto PatientAliases
        {
            get { return _patientAliases; }
            set
            {
                _patientAliases = value;
                RaisePropertyChanged ( () => PatientAliases );
            }
        }

        /// <summary>
        /// Gets or sets the patient allergies.
        /// </summary>
        /// <value>The patient allergies.</value>
        [DataMember]
        public PatientAllergiesDto PatientAllergies
        {
            get { return _patientAllergies; }
            set
            {
                _patientAllergies = value;
                RaisePropertyChanged ( () => PatientAllergies );
            }
        }

        /// <summary>
        /// Gets or sets the patient imported allergies.
        /// </summary>
        /// <value>
        /// The patient imported allergies.
        /// </value>
        [DataMember]
        public PatientImportedAllergiesDto PatientImportedAllergies
        {
            get { return _patientImportedAllergies; }
            set
            {
                _patientImportedAllergies = value;
                RaisePropertyChanged(() => PatientImportedAllergies);
            }
        }

        /// <summary>
        /// Gets or sets the patient confidential information.
        /// </summary>
        /// <value>The patient confidential information.</value>
        [DataMember]
        public PatientConfidentialInformationDto PatientConfidentialInformation
        {
            get { return _patientConfidentialInformationDto; }
            set
            {
                _patientConfidentialInformationDto = value;
                RaisePropertyChanged ( () => PatientConfidentialInformation );
            }
        }

        /// <summary>
        /// Gets or sets the patient contacts.
        /// </summary>
        /// <value>The patient contacts.</value>
        [DataMember]
        public PatientContactsDto PatientContacts
        {
            get { return _patientContacts; }
            set
            {
                _patientContacts = value;
                RaisePropertyChanged ( () => PatientContacts );
            }
        }

        /// <summary>
        /// Gets or sets the patient demographic details.
        /// </summary>
        /// <value>The patient demographic details.</value>
        [DataMember]
        public PatientDemographicDetailsDto PatientDemographicDetails
        {
            get { return _patientDemographicDetails; }
            set
            {
                _patientDemographicDetails = value;
                RaisePropertyChanged ( () => PatientDemographicDetails );
            }
        }

        /// <summary>
        /// Gets or sets the patient identifiers.
        /// </summary>
        /// <value>The patient identifiers.</value>
        [DataMember]
        public PatientIdentifiersDto PatientIdentifiers
        {
            get { return _patientIdentifiers; }
            set
            {
                _patientIdentifiers = value;
                RaisePropertyChanged ( () => PatientIdentifiers );
            }
        }

        /// <summary>
        /// Gets or sets the patient legal status.
        /// </summary>
        /// <value>The patient legal status.</value>
        [DataMember]
        public PatientLegalStatusDto PatientLegalStatus
        {
            get { return _patientLegalStatus; }
            set
            {
                _patientLegalStatus = value;
                RaisePropertyChanged ( () => PatientLegalStatus );
            }
        }

        /// <summary>
        /// Gets or sets the patient other considerations.
        /// </summary>
        /// <value>The patient other considerations.</value>
        [DataMember]
        public PatientOtherConsiderationsDto PatientOtherConsiderations
        {
            get { return _patientOtherConsiderations; }
            set
            {
                _patientOtherConsiderations = value;
                RaisePropertyChanged ( () => PatientOtherConsiderations );
            }
        }

        /// <summary>
        /// Gets or sets the patient phone numbers.
        /// </summary>
        /// <value>The patient phone numbers.</value>
        [DataMember]
        public PatientPhoneNumbersDto PatientPhoneNumbers
        {
            get { return _patientPhoneNumbers; }
            set
            {
                _patientPhoneNumbers = value;
                RaisePropertyChanged ( () => PatientPhoneNumbers );
            }
        }

        /// <summary>
        /// Gets or sets the patient profile.
        /// </summary>
        /// <value>The patient profile.</value>
        [DataMember]
        public PatientProfileDto PatientProfile
        {
            get { return _patientProfile; }
            set
            {
                _patientProfile = value;
                RaisePropertyChanged ( () => PatientProfile );
            }
        }

        /// <summary>
        /// Gets or sets the patient race and ethnicity.
        /// </summary>
        /// <value>The patient race and ethnicity.</value>
        [DataMember]
        public PatientRaceAndEthnicityDto PatientRaceAndEthnicity
        {
            get { return _patientRaceAndEthnicity; }
            set
            {
                _patientRaceAndEthnicity = value;
                RaisePropertyChanged ( () => PatientRaceAndEthnicity );
            }
        }

        /// <summary>
        /// Gets or sets the record status.
        /// </summary>
        /// <value>The record status.</value>
        [DataMember]
        public LookupValueDto RecordStatus
        {
            get { return _recordStatus; }

            set
            {
                _recordStatus = value;
                RaisePropertyChanged ( () => RecordStatus );
            }
        }

        /// <summary>
        /// Gets or sets the religious affiliation.
        /// </summary>
        /// <value>The religious affiliation.</value>
        [DataMember]
        public LookupValueDto ReligiousAffiliation
        {
            get { return _religiousAffiliation; }

            set
            {
                _religiousAffiliation = value;
                RaisePropertyChanged ( () => ReligiousAffiliation );
            }
        }

        /// <summary>
        /// Gets or sets the revised account key.
        /// </summary>
        /// <value>The revised account key.</value>
        [DataMember]
        public long RevisedAccountKey
        {
            get { return _revisedAccountKey; }

            set
            {
                _revisedAccountKey = value;
                RaisePropertyChanged ( () => RevisedAccountKey );
            }
        }

        /// <summary>
        /// Gets or sets the revised timestamp.
        /// </summary>
        /// <value>The revised timestamp.</value>
        [DataMember]
        public DateTimeOffset? RevisedTimestamp
        {
            get { return _revisedTimestamp; }

            set
            {
                _revisedTimestamp = value;
                RaisePropertyChanged ( () => RevisedTimestamp );
            }
        }

        /// <summary>
        /// Gets or sets the veteran information.
        /// </summary>
        /// <value>The veteran information.</value>
        [DataMember]
        public PatientVeteranInformationDto VeteranInformation
        {
            get { return _veteranInformation; }
            set
            {
                _veteranInformation = value;
                RaisePropertyChanged ( () => VeteranInformation );
            }
        }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        [DataMember]
        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
                RaisePropertyChanged ( () => ZipCode );
            }
        }

        #endregion
    }
}
