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

using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Data transfer object for Location class.
    /// </summary>
    public class LocationDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private LocationAddressesAndPhonesDto _addressesAndPhones;
        private LocationContactsDto _locationContacts;
        private LocationIdentifiersDto _locationIdentifiers;
        private LocationOperationSchedulesDto _locationOperationSchedulesDto;
        private LocationProfileDto _locationProfile;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the location addresses and phones.
        /// </summary>
        /// <value>The location addresses and phones.</value>
        [DataMember]
        public LocationAddressesAndPhonesDto LocationAddressesAndPhones
        {
            get { return _addressesAndPhones; }
            set
            {
                _addressesAndPhones = value;
                RaisePropertyChanged ( () => LocationAddressesAndPhones );
            }
        }

        /// <summary>
        /// Gets or sets the location contacts.
        /// </summary>
        /// <value>The location contacts.</value>
        [DataMember]
        public LocationContactsDto LocationContacts
        {
            get { return _locationContacts; }
            set
            {
                _locationContacts = value;
                RaisePropertyChanged ( () => LocationContacts );
            }
        }

        /// <summary>
        /// Gets or sets the location identifiers.
        /// </summary>
        /// <value>The location identifiers.</value>
        [DataMember]
        public LocationIdentifiersDto LocationIdentifiers
        {
            get { return _locationIdentifiers; }
            set
            {
                _locationIdentifiers = value;
                RaisePropertyChanged ( () => LocationIdentifiers );
            }
        }

        /// <summary>
        /// Gets or sets the location operation schedules.
        /// </summary>
        /// <value>The location operation schedules.</value>
        [DataMember]
        public LocationOperationSchedulesDto LocationOperationSchedules
        {
            get { return _locationOperationSchedulesDto; }
            set
            {
                _locationOperationSchedulesDto = value;
                RaisePropertyChanged ( () => LocationOperationSchedules );
            }
        }

        /// <summary>
        /// Gets or sets the location profile.
        /// </summary>
        /// <value>The location profile.</value>
        [DataMember]
        public LocationProfileDto LocationProfile
        {
            get { return _locationProfile; }
            set
            {
                _locationProfile = value;
                RaisePropertyChanged ( () => LocationProfile );
            }
        }

        #endregion
    }
}
