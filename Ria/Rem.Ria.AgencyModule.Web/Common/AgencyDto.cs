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

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for Agency class.
    /// </summary>
    public class AgencyDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private AgencyAddressesAndPhonesDto _addressesAndPhones;
        private ObservableCollection<AgencyAliasDto> _agencyAliases;
        private ObservableCollection<AgencyCharacteristicDto> _agencyCharacteristics;
        private AgencyContactsDto _agencyContacts;
        private AgencyFaqsDto _agencyFrequentlyAskedQuestions;
        private AgencyIdentifiersDto _agencyIdentifiers;
        private AgencyProfileDto _agencyProfile;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the addresses and phones.
        /// </summary>
        /// <value>The addresses and phones.</value>
        [DataMember]
        public AgencyAddressesAndPhonesDto AddressesAndPhones
        {
            get { return _addressesAndPhones; }
            set
            {
                _addressesAndPhones = value;
                RaisePropertyChanged ( () => AddressesAndPhones );
            }
        }

        /// <summary>
        /// Gets or sets the agency aliases.
        /// </summary>
        /// <value>The agency aliases.</value>
        [DataMember]
        public ObservableCollection<AgencyAliasDto> AgencyAliases
        {
            get { return _agencyAliases; }
            set
            {
                _agencyAliases = value;
                RaisePropertyChanged ( () => AgencyAliases );
            }
        }

        /// <summary>
        /// Gets or sets the agency characteristics.
        /// </summary>
        /// <value>The agency characteristics.</value>
        [DataMember]
        public ObservableCollection<AgencyCharacteristicDto> AgencyCharacteristics
        {
            get { return _agencyCharacteristics; }
            set
            {
                _agencyCharacteristics = value;
                RaisePropertyChanged ( () => AgencyCharacteristics );
            }
        }

        /// <summary>
        /// Gets or sets the agency contacts.
        /// </summary>
        /// <value>The agency contacts.</value>
        [DataMember]
        public AgencyContactsDto AgencyContacts
        {
            get { return _agencyContacts; }
            set
            {
                _agencyContacts = value;
                RaisePropertyChanged ( () => AgencyContacts );
            }
        }

        /// <summary>
        /// Gets or sets the agency frequently asked questions.
        /// </summary>
        /// <value>The agency frequently asked questions.</value>
        [DataMember]
        public AgencyFaqsDto AgencyFrequentlyAskedQuestions
        {
            get { return _agencyFrequentlyAskedQuestions; }
            set
            {
                _agencyFrequentlyAskedQuestions = value;
                RaisePropertyChanged ( () => AgencyFrequentlyAskedQuestions );
            }
        }

        /// <summary>
        /// Gets or sets the agency identifiers.
        /// </summary>
        /// <value>The agency identifiers.</value>
        [DataMember]
        public AgencyIdentifiersDto AgencyIdentifiers
        {
            get { return _agencyIdentifiers; }
            set
            {
                _agencyIdentifiers = value;
                RaisePropertyChanged ( () => AgencyIdentifiers );
            }
        }

        /// <summary>
        /// Gets or sets the agency profile.
        /// </summary>
        /// <value>The agency profile.</value>
        [DataMember]
        public AgencyProfileDto AgencyProfile
        {
            get { return _agencyProfile; }
            set
            {
                _agencyProfile = value;
                RaisePropertyChanged ( () => AgencyProfile );
            }
        }

        #endregion
    }
}
