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
using Pillar.Domain.Event;
using Rem.Domain.Core.AgencyModule.Event;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The Agency defines an overarching legal entity for multiple facilities.
    /// </summary>
    public class Agency : AuditableAggregateRootBase
    { 
        /// <summary>
        /// This is check in policy test.
        /// </summary>
        private readonly IList<AgencyAddressAndPhone> _addressesAndPhones;
        private readonly IList<AgencyAlias> _agencyAliases;
        private readonly IList<AgencyCharacteristic> _agencyCharacteristics;
        private readonly IList<AgencyContact> _agencyContacts;
        private readonly IList<AgencyFrequentlyAskedQuestion> _agencyFrequentlyAskedQuestions;
        private readonly IList<AgencyIdentifier> _agencyIdentifiers;
        private readonly IList<AgencyEmailAddress> _emailAddresses;
        private readonly IList<Location> _locations;
        private readonly IList<Staff> _staffMembers;
        private AgencyProfile _agencyProfile;
        private Agency _parentAgency;

        /// <summary>
        /// Initializes a new instance of the <see cref="Agency"/> class.
        /// </summary>
        protected internal Agency ()
        {
            _agencyContacts = new List<AgencyContact> ();
            _agencyCharacteristics = new List<AgencyCharacteristic> ();
            _locations = new List<Location> ();
            _agencyIdentifiers = new List<AgencyIdentifier> ();
            _agencyFrequentlyAskedQuestions = new List<AgencyFrequentlyAskedQuestion> ();
            _addressesAndPhones = new List<AgencyAddressAndPhone> ();
            _staffMembers = new List<Staff> ();
            _agencyAliases = new List<AgencyAlias> ();
            _emailAddresses = new List<AgencyEmailAddress> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Agency"/> class.
        /// </summary>
        /// <param name="agencyProfile">
        /// The agency profile.
        /// </param>
        /// <param name="parentAgency">
        /// The parent agency.
        /// </param>
        protected internal Agency ( AgencyProfile agencyProfile, Agency parentAgency )
            : this ()
        {
            Check.IsNotNull ( agencyProfile, () => AgencyProfile );

            _agencyProfile = agencyProfile;
            _parentAgency = parentAgency;
        }

        /// <summary>
        /// Gets the agency profile.
        /// </summary>
        [NotNull]
        public virtual AgencyProfile AgencyProfile
        {
            get { return _agencyProfile; }
            private set { ApplyPropertyChange ( ref _agencyProfile, () => AgencyProfile, value ); }
        }

        /// <summary>
        /// Gets the parent agency.
        /// </summary>
        public virtual Agency ParentAgency
        {
            get { return _parentAgency; }
            private set { ApplyPropertyChange ( ref _parentAgency, () => ParentAgency, value ); }
        }

        /// <summary>
        /// Gets the agency contacts.
        /// </summary>
        public virtual IEnumerable<AgencyContact> AgencyContacts
        {
            get { return _agencyContacts.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets AgencyCharacteristics.
        /// </summary>
        public virtual IEnumerable<AgencyCharacteristic> AgencyCharacteristics
        {
            get { return _agencyCharacteristics.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the agency locations.
        /// </summary>
        public virtual IEnumerable<Location> Locations
        {
            get { return _locations.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the agency identifiers.
        /// </summary>
        public virtual IEnumerable<AgencyIdentifier> AgencyIdentifiers
        {
            get { return _agencyIdentifiers.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the agency frequently asked questions.
        /// </summary>
        public virtual IEnumerable<AgencyFrequentlyAskedQuestion> AgencyFrequentlyAskedQuestions
        {
            get { return _agencyFrequentlyAskedQuestions.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the addresses and phones.
        /// </summary>
        public virtual IEnumerable<AgencyAddressAndPhone> AddressesAndPhones
        {
            get { return _addressesAndPhones.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the staff members.
        /// </summary>
        public virtual IEnumerable<Staff> StaffMembers
        {
            get { return _staffMembers.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the email addresses.
        /// </summary>
        public virtual IEnumerable<AgencyEmailAddress> EmailAddresses
        {
            get { return _emailAddresses.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the agency aliases.
        /// </summary>
        public virtual IEnumerable<AgencyAlias> AgencyAliases
        {
            get { return _agencyAliases.ToList ().AsReadOnly (); }
            private set { }
        }

        #region Collection Methods

        /// <summary>
        /// Adds the agency alias.
        /// </summary>
        /// <param name="agencyAlias">
        /// The agency alias.
        /// </param>
        public virtual void AddAgencyAlias ( AgencyAlias agencyAlias )
        {
            Check.IsNotNull ( agencyAlias, "agencyAlias is required." );

            agencyAlias.Agency = this;
            _agencyAliases.Add ( agencyAlias );

            NotifyItemAdded ( () => AgencyAliases, agencyAlias );
        }

        /// <summary>
        /// Removes the agency alias.
        /// </summary>
        /// <param name="agencyAlias">
        /// The agency alias.
        /// </param>
        public virtual void RemoveAgencyAlias ( AgencyAlias agencyAlias )
        {
            _agencyAliases.Delete(agencyAlias);
            NotifyItemRemoved ( () => AgencyAliases, agencyAlias );
        }

        /// <summary>
        /// Adds the email address.
        /// </summary>
        /// <param name="agencyEmailAddress">
        /// The agency email address.
        /// </param>
        public virtual void AddEmailAddress ( AgencyEmailAddress agencyEmailAddress )
        {
            Check.IsNotNull ( agencyEmailAddress, "agencyEmailAddress is required." );

            DomainRuleEngine.CreateRuleEngine<Agency, AgencyEmailAddress> ( this, () => AddEmailAddress )
                .WithContext ( agencyEmailAddress )
                .Execute(() =>
                {
                    agencyEmailAddress.Agency = this;
                    _emailAddresses.Add(agencyEmailAddress);
                    NotifyItemAdded(() => EmailAddresses, agencyEmailAddress);
                });
        }

        /// <summary>
        /// Removes the email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        public virtual void RemoveEmailAddress ( AgencyEmailAddress emailAddress )
        {
            _emailAddresses.Delete(emailAddress);
            NotifyItemRemoved ( () => EmailAddresses, emailAddress );
        }

        /// <summary>
        /// Adds the address and phone.
        /// </summary>
        /// <param name="agencyAddress">
        /// The agency address.
        /// </param>
        /// <returns>
        /// An AgencyAddressAndPhone.
        /// </returns>
        public virtual AgencyAddressAndPhone AddAddressAndPhone ( AgencyAddress agencyAddress )
        {
            Check.IsNotNull ( agencyAddress, "agencyAddress is required." );

            AgencyAddressAndPhone addressAndPhone = null;
            var createdAgencyAddressAndPhone = new AgencyAddressAndPhone ( agencyAddress );

            DomainRuleEngine.CreateRuleEngine ( this, "AddAddressAndPhone" )
                .WithContext ( createdAgencyAddressAndPhone )
                .Execute(() =>
                {
                    addressAndPhone = createdAgencyAddressAndPhone;
                    addressAndPhone.Agency = this;
                    _addressesAndPhones.Add(addressAndPhone);
                    NotifyItemAdded(() => AddressesAndPhones, addressAndPhone);
                });

            return addressAndPhone;
        }

        /// <summary>
        /// Removes the address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public virtual void RemoveAddress ( AgencyAddressAndPhone address )
        {
            _addressesAndPhones.Delete(address);
            NotifyItemRemoved ( () => AddressesAndPhones, address );
        }

        /// <summary>
        /// Adds the identifier.
        /// </summary>
        /// <param name="agencyIdentifier">
        /// The agency identifier.
        /// </param>
        public virtual void AddIdentifier(AgencyIdentifier agencyIdentifier)
        {
            Check.IsNotNull ( agencyIdentifier, "agencyIdentifier is required." );

            DomainRuleEngine.CreateRuleEngine<Agency, AgencyIdentifier> ( this, () => AddIdentifier )
                .WithContext ( agencyIdentifier )
                .Execute(() =>
                {
                    agencyIdentifier.Agency = this;
                    _agencyIdentifiers.Add(agencyIdentifier);
                    NotifyItemAdded(() => AgencyIdentifiers, agencyIdentifier);
                });
        }

        /// <summary>
        /// Removes the identifier.
        /// </summary>
        /// <param name="identifier">
        /// The identifier.
        /// </param>
        public virtual void RemoveIdentifier ( AgencyIdentifier identifier )
        {
            _agencyIdentifiers.Delete(identifier);
            NotifyItemRemoved ( () => AgencyIdentifiers, identifier );
        }

        /// <summary>
        /// Adds the frequently asked question.
        /// </summary>
        /// <param name="agencyFrequentlyAskedQuestion">
        /// The agency frequently asked question.
        /// </param>
        public virtual void AddFrequentlyAskedQuestion ( AgencyFrequentlyAskedQuestion agencyFrequentlyAskedQuestion )
        {
            Check.IsNotNull ( agencyFrequentlyAskedQuestion, "agencyFrequentlyAskedQuestion is required." );

            DomainRuleEngine.CreateRuleEngine<Agency, AgencyFrequentlyAskedQuestion> ( this, () => AddFrequentlyAskedQuestion )
                .WithContext ( agencyFrequentlyAskedQuestion )
                .Execute(() =>
                {
                    agencyFrequentlyAskedQuestion.Agency = this;
                    _agencyFrequentlyAskedQuestions.Add(agencyFrequentlyAskedQuestion);
                    NotifyItemAdded(() => AgencyFrequentlyAskedQuestions, agencyFrequentlyAskedQuestion);
                });
        }

        /// <summary>
        /// Removes the frequently asked question.
        /// </summary>
        /// <param name="frequentlyAskedQuestion">
        /// The frequently asked question.
        /// </param>
        public virtual void RemoveFrequentlyAskedQuestion ( AgencyFrequentlyAskedQuestion frequentlyAskedQuestion )
        {
            _agencyFrequentlyAskedQuestions.Delete(frequentlyAskedQuestion);
            NotifyItemRemoved ( () => AgencyFrequentlyAskedQuestions, frequentlyAskedQuestion );
        }

        /// <summary>
        /// Adds the contact.
        /// </summary>
        /// <param name="agencyContact">
        /// The agency contact.
        /// </param>
        public virtual void AddContact ( AgencyContact agencyContact )
        {
            Check.IsNotNull ( agencyContact, "agencyContact is required." );

            DomainRuleEngine.CreateRuleEngine<Agency, AgencyContact> ( this, () => AddContact )
                .WithContext ( agencyContact )
                .Execute(() =>
                {
                    agencyContact.Agency = this;
                    _agencyContacts.Add(agencyContact);
                    NotifyItemAdded(() => AgencyContacts, agencyContact);
                });
        }

        /// <summary>
        /// Removes the contacts.
        /// </summary>
        /// <param name="agencyContact">
        /// The agency contact.
        /// </param>
        public virtual void RemoveContacts ( AgencyContact agencyContact )
        {
            _agencyContacts.Delete(agencyContact);
            NotifyItemRemoved ( () => AgencyContacts, agencyContact );
        }

        #endregion

        /// <summary>
        /// Revises the agency profile.
        /// </summary>
        /// <param name="agencyProfile">
        /// The agency profile.
        /// </param>
        public virtual void ReviseAgencyProfile ( AgencyProfile agencyProfile )
        {
            Check.IsNotNull ( agencyProfile, () => AgencyProfile );

            if ( AgencyProfile == agencyProfile )
            {
                return;
            }

            AgencyProfile oldAgencyProfile = AgencyProfile;

            AgencyProfile = agencyProfile;

            DomainEvent.Raise ( new AgencyProfileChangedEvent { Agency = this, OldAgencyProfile = oldAgencyProfile } );
        }

        /// <summary>
        /// Revises the parent agency.
        /// </summary>
        /// <param name="parentAgency">
        /// The parent agency.
        /// </param>
        public virtual void ReviseParentAgency ( Agency parentAgency )
        {
            ParentAgency = parentAgency;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _agencyProfile.AgencyName.LegalName;
        }
    }
}
