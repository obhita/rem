#region License
// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The Location defines an agency location.
    /// </summary>
    public class Location : AuditableAggregateRootBase
    {
        private readonly IList<LocationAddressAndPhone> _locationAddressesAndPhones;
        private readonly IList<LocationCharacteristic> _locationCharacteristics;
        private readonly IList<LocationContact> _locationContacts;
        private readonly IList<LocationFrequentlyAskedQuestion> _locationFrequentlyAskedQuestions;
        private readonly IList<LocationIdentifier> _locationIdentifiers;
        private readonly IList<LocationLanguage> _locationLanguages;
        private readonly IList<LocationOperationSchedule> _locationOperationSchedules;
        private readonly IList<LocationEmailAddress> _emailAddresses;
        private Agency _agency;
        private LocationProfile _locationProfile;

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        protected internal Location()
        {
            _locationAddressesAndPhones = new List<LocationAddressAndPhone>();
            _locationContacts = new List<LocationContact>();
            _locationCharacteristics = new List<LocationCharacteristic>();
            _locationFrequentlyAskedQuestions = new List<LocationFrequentlyAskedQuestion>();
            _locationLanguages = new List<LocationLanguage>();
            _locationOperationSchedules = new List<LocationOperationSchedule>();
            _locationIdentifiers = new List<LocationIdentifier>();
            _emailAddresses = new List<LocationEmailAddress>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="locationProfile">The location profile.</param>
        protected internal Location(Agency agency, LocationProfile locationProfile)
            : this()
        {
            Check.IsNotNull(agency, () => Agency);
            Check.IsNotNull(locationProfile, () => LocationProfile);

            _agency = agency;
            _locationProfile = locationProfile;
        }

        /// <summary>
        /// Gets Agency.
        /// </summary>
        [NotNull]
        public virtual Agency Agency
        {
            get { return _agency; }
            private set { ApplyPropertyChange(ref _agency, () => Agency, value); }
        }

        /// <summary>
        /// Gets LocationProfile.
        /// </summary>
        [NotNull]
        public virtual LocationProfile LocationProfile
        {
            get { return _locationProfile; }
            private set { ApplyPropertyChange(ref _locationProfile, () => LocationProfile, value); }
        }

        /// <summary>
        /// Gets LocationAddressesAndPhones.
        /// </summary>
        public virtual IEnumerable<LocationAddressAndPhone> LocationAddressesAndPhones
        {
            get { return _locationAddressesAndPhones.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets LocationContacts.
        /// </summary>
        public virtual IEnumerable<LocationContact> LocationContacts
        {
            get { return _locationContacts.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets LocationCharacteristics.
        /// </summary>
        public virtual IEnumerable<LocationCharacteristic> LocationCharacteristics
        {
            get { return _locationCharacteristics.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets LocationFrequentlyAskedQuestions.
        /// </summary>
        public virtual IEnumerable<LocationFrequentlyAskedQuestion> LocationFrequentlyAskedQuestions
        {
            get { return _locationFrequentlyAskedQuestions.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets LocationLanguages.
        /// </summary>
        public virtual IEnumerable<LocationLanguage> LocationLanguages
        {
            get { return _locationLanguages.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets LocationOperationSchedules.
        /// </summary>
        public virtual IEnumerable<LocationOperationSchedule> LocationOperationSchedules
        {
            get { return _locationOperationSchedules.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets LocationIdentifiers.
        /// </summary>
        public virtual IEnumerable<LocationIdentifier> LocationIdentifiers
        {
            get { return _locationIdentifiers.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets EmailAddresses.
        /// </summary>
        public virtual IList<LocationEmailAddress> EmailAddresses
        {
            get { return _emailAddresses; }
            private set { }
        }

        #region Collection Methods

        /// <summary>
        /// The add address and phone.
        /// </summary>
        /// <param name="locationAddress">
        /// The location address.
        /// </param>
        /// <returns>
        /// A LocationAddressAndPhone.
        /// </returns>
        public virtual LocationAddressAndPhone AddAddressAndPhone(LocationAddress locationAddress)
        {
            Check.IsNotNull(locationAddress, "locationAddress is required.");

            LocationAddressAndPhone locationAddressAndPhone = null;
            var createdLocationAddressAndPhone = new LocationAddressAndPhone ( locationAddress );

            DomainRuleEngine.CreateRuleEngine ( this, "AddAddressAndPhoneRuleSet" )
                .WithContext ( createdLocationAddressAndPhone )
                .Execute(() =>
                {
                    locationAddressAndPhone = createdLocationAddressAndPhone;
                    locationAddressAndPhone.Location = this;
                    _locationAddressesAndPhones.Add(locationAddressAndPhone);
                    NotifyItemAdded(() => LocationAddressesAndPhones, locationAddressAndPhone);
                });

            return locationAddressAndPhone;
        }

        /// <summary>
        /// The remove address and phone.
        /// </summary>
        /// <param name="locationAddressAndPhone">
        /// The location address and phone.
        /// </param>
        public virtual void RemoveAddressAndPhone(LocationAddressAndPhone locationAddressAndPhone)
        {
            _locationAddressesAndPhones.Delete(locationAddressAndPhone);
            NotifyItemRemoved(() => LocationAddressesAndPhones, locationAddressAndPhone);
        }

        /// <summary>
        /// The add identifier.
        /// </summary>
        /// <param name="locationIdentifier">
        /// The location identifier.
        /// </param>
        public virtual void AddIdentifier(LocationIdentifier locationIdentifier)
        {
            Check.IsNotNull(locationIdentifier, "locationIdentifier is required.");

            DomainRuleEngine.CreateRuleEngine<Location, LocationIdentifier> ( this, () => AddIdentifier )
                .WithContext ( locationIdentifier )
                .Execute(() =>
                {
                    locationIdentifier.Location = this;
                    _locationIdentifiers.Add(locationIdentifier);
                    NotifyItemAdded(() => LocationIdentifiers, locationIdentifier);
                });
        }

        /// <summary>
        /// The remove identifier.
        /// </summary>
        /// <param name="identifier">
        /// The identifier.
        /// </param>
        public virtual void RemoveIdentifier(LocationIdentifier identifier)
        {
            _locationIdentifiers.Delete(identifier);
            NotifyItemRemoved(() => LocationIdentifiers, identifier);
        }

        /// <summary>
        /// The add contact.
        /// </summary>
        /// <param name="locationContact">
        /// The location contact.
        /// </param>
        public virtual void AddContact(LocationContact locationContact)
        {
            Check.IsNotNull(locationContact, "locationContact is required.");

            DomainRuleEngine.CreateRuleEngine<Location, LocationContact> ( this, () => AddContact )
                .WithContext ( locationContact )
                .Execute(() =>
                {
                    locationContact.Location = this;
                    _locationContacts.Add(locationContact);
                    NotifyItemAdded(() => LocationContacts, locationContact);
                });
        }

        /// <summary>
        /// The remove contacts.
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        public virtual void RemoveContacts(LocationContact contact)
        {
            _locationContacts.Delete(contact);
            NotifyItemRemoved(() => LocationContacts, contact);
        }

        /// <summary>
        /// The add operation schedule.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// A LocationOperationSchedule.
        /// </returns>
        public virtual LocationOperationSchedule AddOperationSchedule(string name)
        {
            Check.IsNotNullOrWhitespace(name, "LocationOperationSchedule name is required.");

            LocationOperationSchedule locationOperationSchedule = null;
            var newLocationOperatingSchedule = new LocationOperationSchedule ( name );

            DomainRuleEngine.CreateRuleEngine( this, "AddOperationSchedule" )
                .WithContext(newLocationOperatingSchedule)
                .Execute(() =>
                    {
                        locationOperationSchedule = newLocationOperatingSchedule;
                        locationOperationSchedule.Location = this;
                    _locationOperationSchedules.Add(locationOperationSchedule);
                    NotifyItemAdded(() => LocationOperationSchedules, locationOperationSchedule);
                });

            return locationOperationSchedule;
        }

        /// <summary>
        /// The remove operation schedule.
        /// </summary>
        /// <param name="locationOperationSchedule">
        /// The location operation schedule.
        /// </param>
        public virtual void RemoveOperationSchedule(LocationOperationSchedule locationOperationSchedule)
        {
            _locationOperationSchedules.Delete(locationOperationSchedule);
            NotifyItemRemoved(() => LocationOperationSchedules, locationOperationSchedule);
        }

        /// <summary>
        /// The add email address.
        /// </summary>
        /// <param name="locationEmailAddress">
        /// The location email address.
        /// </param>
        public virtual void AddEmailAddress(LocationEmailAddress locationEmailAddress)
        {
            Check.IsNotNull(locationEmailAddress, "locationEmailAddress is required.");

            DomainRuleEngine.CreateRuleEngine<Location, LocationEmailAddress> ( this, () => AddEmailAddress )
                .WithContext ( locationEmailAddress )
                .Execute(() =>
                {
                    locationEmailAddress.Location = this;
                    _emailAddresses.Add(locationEmailAddress);
                    NotifyItemAdded(() => EmailAddresses, locationEmailAddress);
                });
        }

        /// <summary>
        /// The remove email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        public virtual void RemoveEmailAddress(LocationEmailAddress emailAddress)
        {
            _emailAddresses.Delete(emailAddress);
            NotifyItemRemoved(() => EmailAddresses, emailAddress);
        }

        #endregion

        /// <summary>
        /// The revise location profile.
        /// </summary>
        /// <param name="locationProfile">
        /// The location profile.
        /// </param>
        public virtual void ReviseLocationProfile(LocationProfile locationProfile)
        {
            Check.IsNotNull(locationProfile, () => LocationProfile);

            LocationProfile = locationProfile;
        }

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="otherLocationProfile">The other location profile.</param>
        /// <param name="otherLocationProfileAgency">The other location profile agency.</param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>
        public virtual bool ValuesEqual(LocationProfile otherLocationProfile, Agency otherLocationProfileAgency)
        {
            return LocationProfile.LocationName.Name.Equals(otherLocationProfile.LocationName.Name)
                   && Agency.Key == otherLocationProfileAgency.Key;
        }


        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="other">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>             
        public virtual bool ValuesEqual(Location other)
        {
            return ValuesEqual(other.LocationProfile, other.Agency);           
        }

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return LocationProfile.LocationName.Name;
        }

        #endregion
    }
}
