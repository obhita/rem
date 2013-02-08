using System;
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Extension;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Event;
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Clinical.PatientModule.Event;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Patient represents an individual who receives medical attention, care, or treatment.
    /// </summary>
    public class Patient : AuditableAggregateRootBase, IPatientAccessAuditable
    {
        private readonly IList<PatientAddress> _addresses;
        private readonly IList<PatientAlert> _alerts;
        private readonly IList<PatientAlias> _aliases;
        private readonly IList<Allergy> _allergies;
        private readonly IList<ClinicalCase> _clinicalCases;
        private readonly IList<PatientContact> _contacts;
        private readonly IList<PatientDisability> _disabilities;
        private readonly IList<PatientIdentifier> _identifiers;
        private readonly IList<Medication> _medications;
        private readonly IList<PatientPhone> _phoneNumbers;
        private readonly IList<PatientPhoto> _photos;
        private readonly IList<PatientRace> _races;
        private readonly IList<PatientSpecialNeed> _specialNeeds;

        private PersonName _name;
        private PatientProfile _profile;
        private PatientBirthInfo _birthInfo;
        private PatientRace _primaryPatientRace;
        private PatientPhoto _primaryPatientPhoto;
        private MotherName _motherName;
        private PatientAssignedArea _assignedArea;
        private PatientLanguage _language;
        private PatientLegalInfo _legalInfo;
        private PatientConfidentialInfo _confidentialInfo;
        private SmokingStatus _smokingStatus;
        private bool? _paperFileIndicator;
        private PatientEthnicity _ethnicity;
        private string _note;
        private string _uniqueIdentifier;
        private DateTimeOffset? _revisedTimestamp;
        private long _revisedAccountKey;
        private PatientVeteranInformation _veteranInformation;

        private RecordStatus _recordStatus;
        private EducationStatus _educationStatus;
        private ReligiousAffiliation _religiousAffiliation;
        private MaritalStatus _maritalStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="patientName">Name of the patient.</param>
        /// <param name="profile">The profile.</param>
        protected internal Patient (
            Agency agency,
            PersonName patientName,
            PatientProfile profile )
            : this ()
        {
            Check.IsNotNull ( agency, "Agency is required." );
            Check.IsNotNull ( patientName, "Patient name is required" );
            Check.IsNotNull ( profile, "Patient profile is required" );

            Agency = agency;
            _name = patientName;

            _profile = profile;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Patient"/> class.
        /// </summary>
        protected internal Patient ()
        {
            _phoneNumbers = new List<PatientPhone> ();
            _addresses = new List<PatientAddress> ();
            _races = new List<PatientRace> ();
            _contacts = new List<PatientContact> ();
            _aliases = new List<PatientAlias> ();
            _specialNeeds = new List<PatientSpecialNeed> ();
            _identifiers = new List<PatientIdentifier> ();
            _photos = new List<PatientPhoto> ();
            _disabilities = new List<PatientDisability> ();
            _clinicalCases = new List<ClinicalCase> ();
            _allergies = new List<Allergy> ();

            _medications = new List<Medication> ();
            _alerts = new List<PatientAlert> ();
        }

        /// <summary>
        /// Gets the agency.
        /// </summary>
        [NotNull]
        public virtual Agency Agency { get; private set; }

        /// <summary>
        /// Gets the patients name.
        /// </summary>
        [NotNull]
        public virtual PersonName Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        [NotNull]
        public virtual PatientProfile Profile
        {
            get { return _profile; }
            private set { ApplyPropertyChange(ref _profile, () => Profile, value); }
        }

        /// <summary>
        /// Gets the birth info.
        /// </summary>
        public virtual PatientBirthInfo BirthInfo
        {
            get { return _birthInfo; }
            private set { ApplyPropertyChange(ref _birthInfo, () => BirthInfo, value); }
        }

        /// <summary>
        /// Gets the primary patient race.
        /// </summary>
        [NoneCascading]
        public virtual PatientRace PrimaryPatientRace
        {
            get { return _primaryPatientRace; }
            private set { ApplyPropertyChange ( ref _primaryPatientRace, () => PrimaryPatientRace, value ); }
        }

        /// <summary>
        /// Gets the primary patient photo.
        /// </summary>
        public virtual PatientPhoto PrimaryPatientPhoto
        {
            get { return _primaryPatientPhoto; }

            private set { ApplyPropertyChange(ref _primaryPatientPhoto, () => PrimaryPatientPhoto, value); }
        }

        /// <summary>
        /// Gets the name of the mother.
        /// </summary>
        /// <value>
        /// The name of the mother.
        /// </value>
        public virtual MotherName MotherName
        {
            get { return _motherName; }

            private set { ApplyPropertyChange(ref _motherName, () => MotherName, value); }
        }

        /// <summary>
        /// Gets the assigned area.
        /// </summary>
        public virtual PatientAssignedArea AssignedArea
        {
            get { return _assignedArea; }
            private set { ApplyPropertyChange(ref _assignedArea, () => AssignedArea, value); }
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        public virtual PatientLanguage Language
        {
            get { return _language; }
            private set { ApplyPropertyChange(ref _language, () => Language, value); }
        }

        /// <summary>
        /// Gets the legal info.
        /// </summary>
        public virtual PatientLegalInfo LegalInfo
        {
            get { return _legalInfo; }
            private set { ApplyPropertyChange(ref _legalInfo, () => LegalInfo, value); }
        }

        /// <summary>
        /// Gets the confidential info.
        /// </summary>
        public virtual PatientConfidentialInfo ConfidentialInfo
        {
            get { return _confidentialInfo; }
            private set { ApplyPropertyChange(ref _confidentialInfo, () => ConfidentialInfo, value); }
        }

        /// <summary>
        /// Gets the smoking status.
        /// </summary>
        public virtual SmokingStatus SmokingStatus
        {
            get { return _smokingStatus; }
            private set { ApplyPropertyChange(ref _smokingStatus, () => SmokingStatus, value); }
        }

        /// <summary>
        /// Gets the paper file indicator.
        /// </summary>
        public virtual bool? PaperFileIndicator
        {
            get { return _paperFileIndicator; }
            private set { ApplyPropertyChange(ref _paperFileIndicator, () => PaperFileIndicator, value); }
        }

        /// <summary>
        /// Gets the ethnicity.
        /// </summary>
        public virtual PatientEthnicity Ethnicity
        {
            get { return _ethnicity; }
            private set { ApplyPropertyChange(ref _ethnicity, () => Ethnicity, value); }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        public virtual string Note
        {
            get { return _note; }
            private set { ApplyPropertyChange(ref _note, () => Note, value); }
        }

        /// <summary>
        /// Gets the record status.
        /// </summary>
        public virtual RecordStatus RecordStatus
        {
            get { return _recordStatus; }
            private set { ApplyPropertyChange(ref _recordStatus, () => RecordStatus, value); }
        }

        /// <summary>
        /// Gets the religious affiliation.
        /// </summary>
        public virtual ReligiousAffiliation ReligiousAffiliation
        {
            get { return _religiousAffiliation; }
            private set { ApplyPropertyChange(ref _religiousAffiliation, () => ReligiousAffiliation, value); }
        }

        /// <summary>
        /// Gets the marital status.
        /// </summary>
        public virtual MaritalStatus MaritalStatus
        {
            get { return _maritalStatus; }

            private set { ApplyPropertyChange(ref _maritalStatus, () => MaritalStatus, value); }
        }

        /// <summary>
        /// Gets the education status.
        /// </summary>
        public virtual EducationStatus EducationStatus
        {
            get { return _educationStatus; }
            private set { ApplyPropertyChange(ref _educationStatus, () => EducationStatus, value); }
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        [NotNull]
        [Unique]
        public virtual string UniqueIdentifier
        {
            get { return _uniqueIdentifier; }

            private set { ApplyPropertyChange(ref _uniqueIdentifier, () => UniqueIdentifier, value); }
        }

        /// <summary>
        /// Gets the revised timestamp.
        /// </summary>
        public virtual DateTimeOffset? RevisedTimestamp
        {
            get { return _revisedTimestamp; }
            private set { ApplyPropertyChange(ref _revisedTimestamp, () => RevisedTimestamp, value); }
        }

        /// <summary>
        /// Gets the revised account key.
        /// </summary>
        public virtual long RevisedAccountKey
        {
            get { return _revisedAccountKey; }
            private set { ApplyPropertyChange(ref _revisedAccountKey, () => RevisedAccountKey, value); }
        }

        /// <summary>
        ///  Gets veteran information.
        /// </summary>
        public virtual PatientVeteranInformation VeteranInformation
        {
            get { return _veteranInformation; }
            private set { ApplyPropertyChange(ref _veteranInformation, () => VeteranInformation, value); }
        }

        /// <summary>
        /// Gets the disabilities.
        /// </summary>
        public virtual IEnumerable<PatientDisability> Disabilities
        {
            get { return _disabilities.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the special needs.
        /// </summary>
        public virtual IEnumerable<PatientSpecialNeed> SpecialNeeds
        {
            get { return _specialNeeds.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the races.
        /// </summary>
        public virtual IEnumerable<PatientRace> Races
        {
            get { return _races.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the contacts.
        /// </summary>
        public virtual IEnumerable<PatientContact> Contacts
        {
            get { return _contacts.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the clinical cases.
        /// </summary>
        public virtual IEnumerable<ClinicalCase> ClinicalCases
        {
            get { return _clinicalCases.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the allergies.
        /// </summary>
        public virtual IEnumerable<Allergy> Allergies
        {
            get { return _allergies.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the alerts.
        /// </summary>
        public virtual IEnumerable<PatientAlert> Alerts
        {
            get { return _alerts.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the medications.
        /// </summary>
        public virtual IEnumerable<Medication> Medications
        {
            get { return _medications; }
            private set { }
        }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        public virtual IEnumerable<PatientAlias> Aliases
        {
            get { return _aliases.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the phone numbers.
        /// </summary>
        public virtual IEnumerable<PatientPhone> PhoneNumbers
        {
            get { return _phoneNumbers.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the addresses.
        /// </summary>
        public virtual IEnumerable<PatientAddress> Addresses
        {
            get { return _addresses.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the identifiers.
        /// </summary>
        public virtual IEnumerable<PatientIdentifier> Identifiers
        {
            get { return _identifiers.ToList().AsReadOnly(); }

            private set { }
        }

        /// <summary>
        /// Gets the photos.
        /// </summary>
        public virtual IEnumerable<PatientPhoto> Photos
        {
            get { return _photos.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the home address.
        /// </summary>
        [IgnoreMapping]
        public virtual Address HomeAddress
        {
            get
            {
                var patientAddress =
                    _addresses.Where( p => p.PatientAddressType.WellKnownName == WellKnownNames.PatientModule.PatientAddressType.Home ).FirstOrDefault ();

                return patientAddress == null ? null : patientAddress.Address;
            }
        }

        #region IPatientAccessAuditable Members

        Patient IPatientAccessAuditable.AuditedPatient
        {
            get { return this; }
        }

        string IPatientAccessAuditable.AuditedContextDescription
        {
            get { return string.Format("{0}: {1}", GetType().Name.SeparatePascalCaseWords(), ToString()); }
        }

        #endregion

        /// <summary>
        /// Renames the specified patient name.
        /// </summary>
        /// <param name="patientName">Name of the patient.</param>
        public virtual void Rename ( PersonName patientName )
        {
            Check.IsNotNull ( patientName, "Patient name is required." );

            DomainRuleEngine.CreateRuleEngine<Patient, PersonName> ( this, () => Rename )
                .WithContext ( patientName )
                .Execute ( () =>
                             {
                                 Name = patientName;
                                 DomainEvent.Raise(new PatientRenamedEvent { Patient = this });
                             });
        }

        /// <summary>
        /// Revises the profile.
        /// </summary>
        /// <param name="patientProfile">The patient profile.</param>
        public virtual void ReviseProfile ( PatientProfile patientProfile )
        {
            Check.IsNotNull ( patientProfile, "Patient Profile is required." );

            DomainRuleEngine.CreateRuleEngine<Patient, PatientProfile> ( this, () => ReviseProfile )
                .WithContext ( patientProfile )
                .Execute (
                    () =>
                        {
                            Profile = patientProfile;
                            DomainEvent.Raise ( new PatientProfileRevisedEvent { Patient = this } );
                        } );
        }

        /// <summary>
        /// Revises the language.
        /// </summary>
        /// <param name="language">The language.</param>
        public virtual void ReviseLanguage(PatientLanguage language)
        {
            Language = language;
        }

        /// <summary>
        /// Revises the smoking status.
        /// </summary>
        /// <param name="smokingStatus">The smoking status.</param>
        public virtual void ReviseSmokingStatus(SmokingStatus smokingStatus)
        {
            SmokingStatus = smokingStatus;
        }

        /// <summary>
        /// Revises the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        public virtual void ReviseNotes(string notes)
        {
            Note = notes;
        }

        /// <summary>
        /// Revises the paper record.
        /// </summary>
        /// <param name="hasPaperRecord">The has paper record.</param>
        public virtual void RevisePaperRecord(bool? hasPaperRecord)
        {
            PaperFileIndicator = hasPaperRecord;
        }

        /// <summary>
        /// Adds the alias.
        /// </summary>
        /// <param name="patientAlias">The patient alias.</param>
        public virtual void AddAlias(PatientAlias patientAlias)
        {
            Check.IsNotNull(patientAlias, "patient alias is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientAlias> ( this, () => AddAlias )
                .WithContext ( patientAlias )
                .Execute (
                    () =>
                        {
                            patientAlias.Patient = this;
                            _aliases.Add ( patientAlias );
                            NotifyItemAdded ( () => Aliases, patientAlias );
                        } );
        }

        /// <summary>
        /// Removes the alias.
        /// </summary>
        /// <param name="patientAlias">The patient alias.</param>
        public virtual void RemoveAlias(PatientAlias patientAlias)
        {
            Check.IsNotNull(patientAlias, "alias is required.");

            _aliases.Delete(patientAlias);
            NotifyItemRemoved(() => Aliases, patientAlias);
        }

        /// <summary>
        /// Adds the phone number.
        /// </summary>
        /// <param name="patientPhone">The patient phone.</param>
        public virtual void AddPhoneNumber(PatientPhone patientPhone)
        {
            Check.IsNotNull(patientPhone, "patient phone is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientPhone> ( this, () => AddPhoneNumber )
                .WithContext ( patientPhone )
                .Execute (
                    () =>
                        {
                            patientPhone.Patient = this;
                            _phoneNumbers.Add ( patientPhone );
                            NotifyItemAdded ( () => PhoneNumbers, patientPhone );
                        } );
        }

        /// <summary>
        /// Removes the phone number.
        /// </summary>
        /// <param name="patientPhone">The patient phone.</param>
        public virtual void RemovePhoneNumber(PatientPhone patientPhone)
        {
            Check.IsNotNull(patientPhone, "patient phone is required.");

            _phoneNumbers.Delete(patientPhone);
            NotifyItemRemoved(() => PhoneNumbers, patientPhone);
        }

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="patientAddress">The patient address.</param>
        public virtual void AddAddress(PatientAddress patientAddress)
        {
            Check.IsNotNull(patientAddress, "patient address is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientAddress> ( this, () => AddAddress )
                .WithContext ( patientAddress )
                .Execute (
                    () =>
                        {
                            patientAddress.Patient = this;
                            _addresses.Add ( patientAddress );
                            NotifyItemAdded ( () => Addresses, patientAddress );
                        } );
        }

        /// <summary>
        /// Removes the address.
        /// </summary>
        /// <param name="patientAddress">The patient address.</param>
        public virtual void RemoveAddress(PatientAddress patientAddress)
        {
            Check.IsNotNull(patientAddress, "patient address is required.");

            _addresses.Delete(patientAddress);
            NotifyItemRemoved(() => Addresses, patientAddress);
        }

        /// <summary>
        /// Adds the patient identifier.
        /// </summary>
        /// <param name="patientIdentifier">The patient identifier.</param>
        public virtual void AddPatientIdentifier(PatientIdentifier patientIdentifier)
        {
            Check.IsNotNull(patientIdentifier, "patient identifier is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientIdentifier> ( this, () => AddPatientIdentifier )
                .WithContext ( patientIdentifier )
                .Execute (
                    () =>
                        {
                            patientIdentifier.Patient = this;
                            _identifiers.Add ( patientIdentifier );
                            NotifyItemAdded ( () => Identifiers, patientIdentifier );
                        } );
        }

        /// <summary>
        /// Removes the identifier.
        /// </summary>
        /// <param name="patientIdentifier">The patient identifier.</param>
        public virtual void RemoveIdentifier(PatientIdentifier patientIdentifier)
        {
            Check.IsNotNull(patientIdentifier, "patient identifier is required.");

            _identifiers.Delete(patientIdentifier);
            NotifyItemRemoved(() => Identifiers, patientIdentifier);
        }

        /// <summary>
        /// Adds the patient race.
        /// </summary>
        /// <param name="patientRace">The patient race.</param>
        public virtual void AddPatientRace(PatientRace patientRace)
        {
            Check.IsNotNull(patientRace, "patient race is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientRace> ( this, () => AddPatientRace )
                .WithContext ( patientRace )
                .Execute (
                    () =>
                        {
                            patientRace.Patient = this;
                            _races.Add ( patientRace );
                            NotifyItemAdded ( () => Races, patientRace );
                        } );
        }

        /// <summary>
        /// Sets the primary race.
        /// </summary>
        /// <param name="race">The race.</param>
        public virtual void SetPrimaryRace(Race race)
        {
            Check.IsNotNull ( race, "race is required." );

            PatientRace patientRace = null;
            if (race != null)
            {
                patientRace = _races.FirstOrDefault ( p => p.Race.Key == race.Key );
            }

            DomainRuleEngine.CreateRuleEngine<Patient, Race> ( this, () => SetPrimaryRace )
                .WithContext ( patientRace )
                .Execute ( () => PrimaryPatientRace = patientRace );
        }

        /// <summary>
        /// Removes the patient race.
        /// </summary>
        /// <param name="patientRace">The patient race.</param>
        public virtual void RemovePatientRace(PatientRace patientRace)
        {
            Check.IsNotNull(patientRace, "patient race is required.");

            if (PrimaryPatientRace == patientRace)
            {
                PrimaryPatientRace = null;
            }

            _races.Delete(patientRace);
            NotifyItemRemoved(() => Races, patientRace);
        }

        /// <summary>
        /// Revises the ethnicity.
        /// </summary>
        /// <param name="ethnicity">The ethnicity.</param>
        public virtual void ReviseEthnicity(PatientEthnicity ethnicity)
        {
            Ethnicity = ethnicity;
        }

        /// <summary>
        /// Adds the patient special need.
        /// </summary>
        /// <param name="patientSpecialNeed">The patient special need.</param>
        public virtual void AddPatientSpecialNeed(PatientSpecialNeed patientSpecialNeed)
        {
            Check.IsNotNull(patientSpecialNeed, "patient special need is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientSpecialNeed> ( this, () => AddPatientSpecialNeed )
                .WithContext ( patientSpecialNeed )
                .Execute (
                    () =>
                        {
                            patientSpecialNeed.Patient = this;
                            _specialNeeds.Add ( patientSpecialNeed );
                            NotifyItemAdded ( () => SpecialNeeds, patientSpecialNeed );
                        } );
        }

        /// <summary>
        /// Removes the patient special need.
        /// </summary>
        /// <param name="patientSpecialNeed">The patient special need.</param>
        public virtual void RemovePatientSpecialNeed(PatientSpecialNeed patientSpecialNeed)
        {
            Check.IsNotNull(patientSpecialNeed, "patient special need is required.");

            _specialNeeds.Delete(patientSpecialNeed);
            NotifyItemRemoved(() => SpecialNeeds, patientSpecialNeed);
        }

        /// <summary>
        /// Adds the patient disability.
        /// </summary>
        /// <param name="patientDisability">The patient disability.</param>
        public virtual void AddPatientDisability(PatientDisability patientDisability)
        {
            Check.IsNotNull(patientDisability, "patient disability is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientDisability> ( this, () => AddPatientDisability )
                .WithContext ( patientDisability )
                .Execute (
                    () =>
                        {
                            patientDisability.Patient = this;
                            _disabilities.Add ( patientDisability );
                            NotifyItemAdded ( () => Disabilities, patientDisability );
                        } );
        }

        /// <summary>
        /// Removes the patient disability.
        /// </summary>
        /// <param name="patientDisability">The patient disability.</param>
        public virtual void RemovePatientDisabilility(PatientDisability patientDisability)
        {
            Check.IsNotNull(patientDisability, "patient disability is required.");

            _disabilities.Delete(patientDisability);
            NotifyItemRemoved(() => Disabilities, patientDisability);
        }

        /// <summary>
        /// Adds the patient photo.
        /// </summary>
        /// <param name="patientPhoto">The patient photo.</param>
        public virtual void AddPatientPhoto(PatientPhoto patientPhoto)
        {
            Check.IsNotNull(patientPhoto, "patientPhoto is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientPhoto> ( this, () => AddPatientPhoto )
                .WithContext ( patientPhoto )
                .Execute (
                    () =>
                        {
                            patientPhoto.Patient = this;
                            _photos.Add ( patientPhoto );

                            if ( _photos.Count == 1 )
                            {
                                SetPrimaryPhoto ( patientPhoto );
                            }

                            NotifyItemAdded ( () => Photos, patientPhoto );
                        } );
        }

        /// <summary>
        /// Sets the primary photo.
        /// </summary>
        /// <param name="patientPhoto">The patient photo.</param>
        public virtual void SetPrimaryPhoto(PatientPhoto patientPhoto)
        {
            Check.IsNotNull(patientPhoto, "patientPhoto is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientPhoto> ( this, () => SetPrimaryPhoto )
                .WithContext ( patientPhoto )
                .Execute ( () => PrimaryPatientPhoto = patientPhoto );
        }

        /// <summary>
        /// Adds the contact.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>A PatientContact.</returns>
        public virtual PatientContact AddContact(string firstName, string lastName)
        {
            InitializeServices();
            var factory = IoC.CurrentContainer.Resolve<IPatientContactFactory>();
            var contact = factory.CreatePatientContact(this, firstName, lastName);
            if (contact != null)
            {
                _contacts.Add ( contact );

                NotifyItemAdded ( () => Contacts, contact );
            }

            return contact;
        }

        /// <summary>
        /// Removes the contact.
        /// </summary>
        /// <param name="patientContact">The patient contact.</param>
        public virtual void RemoveContact(PatientContact patientContact)
        {
            var factory = IoC.CurrentContainer.Resolve<IPatientContactFactory>();
            factory.DestroyPatientContact(patientContact);
            NotifyItemRemoved(() => Contacts, patientContact);
        }

        /// <summary>
        /// Adds the allergy.
        /// </summary>
        /// <param name="allergyStatus">The allergy status.</param>
        /// <param name="allergenCodedConcept">The allergen coded concept.</param>
        /// <returns>An Allergy.</returns>
        public virtual Allergy AddAllergy(AllergyStatus allergyStatus, CodedConcept allergenCodedConcept)
        {
            InitializeServices();

            var factory = IoC.CurrentContainer.Resolve<IAllergyFactory>();
            var allergy = factory.CreateAllergy(this, allergyStatus, allergenCodedConcept);
            _allergies.Add(allergy);

            NotifyItemAdded(() => Allergies, allergy);

            return allergy;
        }

        /// <summary>
        /// Adds the allergy.
        /// </summary>
        /// <param name="allergyStatus">The allergy status.</param>
        /// <param name="allergenCodedConcept">The allergen coded concept.</param>
        /// <param name="provenance">The provenance.</param>
        /// <returns>AN Allergy.</returns>
        public virtual Allergy AddAllergy(AllergyStatus allergyStatus, CodedConcept allergenCodedConcept, Provenance provenance)
        {
            InitializeServices();

            var factory = IoC.CurrentContainer.Resolve<IAllergyFactory>();
            var allergy = factory.CreateAllergy(this, allergyStatus, allergenCodedConcept, provenance);
            _allergies.Add(allergy);

            NotifyItemAdded(() => Allergies, allergy);

            return allergy;
        }


        /// <summary>
        /// Removes the allergy.
        /// </summary>
        /// <param name="allergy">The allergy.</param>
        public virtual void RemoveAllergy(Allergy allergy)
        {
            if (_allergies.Contains(allergy))
            {
                _allergies.Remove(allergy);
            }
            else
            {
                throw new ArgumentException("Allergy not found.");
            }

            var factory = IoC.CurrentContainer.Resolve<IAllergyFactory>();
            factory.DestroyAllergy(allergy);

            NotifyItemRemoved(() => Allergies, allergy);
        }

        /// <summary>
        /// Adds the medication.
        /// </summary>
        /// <param name="medicationCodeCodedConcept">The medication code coded concept.</param>
        /// <param name="rootMedicationCodedConcept">The root medication coded concept.</param>
        /// <returns>A Medication</returns>
        public virtual Medication AddMedication(CodedConcept medicationCodeCodedConcept, CodedConcept rootMedicationCodedConcept)
        {
            InitializeServices();

            var factory = IoC.CurrentContainer.Resolve<IMedicationFactory>();
            var medication = factory.CreateMedication(this, medicationCodeCodedConcept, rootMedicationCodedConcept);
            _medications.Add(medication);

            NotifyItemAdded(() => Medications, medication);

            return medication;
        }

        /// <summary>
        /// Adds the medication.
        /// </summary>
        /// <param name="medicationCodeCodedConcept">The medication code coded concept.</param>
        /// <param name="provenance">The provenance.</param>
        /// <returns>A Medication.</returns>
        public virtual Medication AddMedication(CodedConcept medicationCodeCodedConcept, Provenance provenance)
        {
            InitializeServices();

            var factory = IoC.CurrentContainer.Resolve<IMedicationFactory>();
            var medication = factory.CreateMedication(this, medicationCodeCodedConcept, provenance);
            _medications.Add(medication);

            NotifyItemAdded(() => Medications, medication);

            return medication;
        }

        /// <summary>
        /// Removes the medication.
        /// </summary>
        /// <param name="medication">The medication.</param>
        public virtual void RemoveMedication(Medication medication)
        {
            if (_medications.Contains(medication))
            {
                _medications.Remove(medication);
            }
            else
            {
                throw new ArgumentException("Medication not found.");
            }

            var factory = IoC.CurrentContainer.Resolve<IMedicationFactory>();
            factory.DestroyMedication(medication);

            NotifyItemRemoved(() => Medications, medication);
        }

        /// <summary>
        /// Adds the alert.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="note">The note.</param>
        /// <param name="cdsIdentifier">The CDS identifier.</param>
        /// <returns>A PatientAlert.</returns>
        public virtual PatientAlert AddAlert(string name, string note, string cdsIdentifier)
        {
            InitializeServices();

            var factory = IoC.CurrentContainer.Resolve<IPatientAlertFactory>();
            var alert = factory.CreateAlert(this, name, note, cdsIdentifier);
            _alerts.Add(alert);

            NotifyItemAdded(() => Alerts, alert);

            return alert;
        }

        /// <summary>
        /// Removes the alert.
        /// </summary>
        /// <param name="alert">The alert.</param>
        public virtual void RemoveAlert(PatientAlert alert)
        {
            if (_alerts.Contains(alert))
            {
                _alerts.Remove(alert);
            }
            else
            {
                throw new ArgumentException("Alert not found.");
            }

            var factory = IoC.CurrentContainer.Resolve<IPatientAlertFactory>();
            factory.DestroyAlert(alert);

            NotifyItemRemoved(() => Alerts, alert);
        }

        /// <summary>
        /// Updates the unique identifier.
        /// </summary>
        /// <param name="uniqueIdentifier">The unique identifier.</param>
        public virtual void UpdateUniqueIdentifier(string uniqueIdentifier)
        {
            // for now we'll just change the value but in the future we'll have to add rules in 
            // place that verify that the identifier is unique as well as informing the audit log.
            UniqueIdentifier = uniqueIdentifier;
        }

        /// <summary>
        /// Revises the confidential info.
        /// </summary>
        /// <param name="patientConfidentialInfo">The patient confidential info.</param>
        public virtual void ReviseConfidentialInfo(PatientConfidentialInfo patientConfidentialInfo)
        {
            _confidentialInfo = patientConfidentialInfo;
        }

        /// <summary>
        /// Revises the legal info.
        /// </summary>
        /// <param name="patientLegalInfo">The patient legal info.</param>
        public virtual void ReviseLegalInfo(PatientLegalInfo patientLegalInfo)
        {
            _legalInfo = patientLegalInfo;
        }

        /// <summary>
        /// Revises the birth info.
        /// </summary>
        /// <param name="patientBirthInfo">The patient birth info.</param>
        public virtual void ReviseBirthInfo(PatientBirthInfo patientBirthInfo)
        {
            _birthInfo = patientBirthInfo;
        }

        /// <summary>
        /// Revises the name of the mother.
        /// </summary>
        /// <param name="motherName">Name of the mother.</param>
        public virtual void ReviseMotherName(MotherName motherName)
        {
            _motherName = motherName;
        }

        /// <summary>
        /// Revises the assigned area.
        /// </summary>
        /// <param name="patientAssignedArea">The patient assigned area.</param>
        public virtual void ReviseAssignedArea(PatientAssignedArea patientAssignedArea)
        {
            _assignedArea = patientAssignedArea;
        }

        /// <summary>
        /// Revises the veteran information.
        /// </summary>
        /// <param name="veteranInformation">The veteran information.</param>
        public virtual void ReviseVeteranInformation(PatientVeteranInformation veteranInformation)
        {
            Check.IsNotNull(veteranInformation, "Patient Veteran Information is required.");

            DomainRuleEngine.CreateRuleEngine<Patient, PatientVeteranInformation>(this, () => ReviseVeteranInformation)
                .WithContext(veteranInformation)
                .Execute(() =>
                    {
                        VeteranInformation = veteranInformation;
                    });
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return Name.Complete;
        }
    }
}
