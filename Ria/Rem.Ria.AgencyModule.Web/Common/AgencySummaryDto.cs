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
    /// Data transfer object for AgencySummary class.
    /// </summary>
    [DataContract]
    public class AgencySummaryDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private ObservableCollection<AgencyAddressAndPhoneDto> _addressesAndPhones;
        private ObservableCollection<AgencyCharacteristicDto> _agencyCharacteristics;
        private ObservableCollection<AgencyContactDto> _agencyContacts;
        private ObservableCollection<AgencyIdentifierDto> _agencyIdentifiers;
        private string _displayName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the addresses and phones.
        /// </summary>
        /// <value>The addresses and phones.</value>
        [DataMember]
        public ObservableCollection<AgencyAddressAndPhoneDto> AddressesAndPhones
        {
            get { return _addressesAndPhones; }
            set
            {
                _addressesAndPhones = value;
                RaisePropertyChanged ( () => AddressesAndPhones );
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
        public ObservableCollection<AgencyContactDto> AgencyContacts
        {
            get { return _agencyContacts; }
            set
            {
                _agencyContacts = value;
                RaisePropertyChanged ( () => AgencyContacts );
            }
        }

        /// <summary>
        /// Gets or sets the agency identifiers.
        /// </summary>
        /// <value>The agency identifiers.</value>
        [DataMember]
        public ObservableCollection<AgencyIdentifierDto> AgencyIdentifiers
        {
            get { return _agencyIdentifiers; }
            set
            {
                _agencyIdentifiers = value;
                RaisePropertyChanged ( () => AgencyIdentifiers );
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [DataMember]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged ( () => DisplayName );
            }
        }

        #endregion
    }
}
