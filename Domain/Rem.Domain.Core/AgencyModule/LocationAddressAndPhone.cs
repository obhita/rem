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
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// LocationAddressAndPhone contains location address and phone.
    /// </summary>
    public class LocationAddressAndPhone : LocationAggregateNodeBase
    {
        private readonly IList<LocationPhone> _phoneNumbers;
        private LocationAddress _locationAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationAddressAndPhone"/> class.
        /// </summary>
        protected internal LocationAddressAndPhone()
        {
            _phoneNumbers = new List<LocationPhone>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationAddressAndPhone"/> class.
        /// </summary>
        /// <param name="locationAddress">
        /// The location address.
        /// </param>
        protected internal LocationAddressAndPhone(LocationAddress locationAddress)
            : this()
        {
            Check.IsNotNull(locationAddress, () => LocationAddress);
        }

        /// <summary>
        /// Gets LocationAddress.
        /// </summary>
        [NotNull]
        public virtual LocationAddress LocationAddress
        {
            get { return _locationAddress; }
            private set { ApplyPropertyChange(ref _locationAddress, () => LocationAddress, value); }
        }

        /// <summary>
        /// Gets PhoneNumbers.
        /// </summary>
        public virtual IEnumerable<LocationPhone> PhoneNumbers
        {
            get { return _phoneNumbers; }
            private set { }
        }

        #region Collection Methods

        /// <summary>
        /// The add phone.
        /// </summary>
        /// <param name="locationPhone">
        /// The location phone.
        /// </param>
        public virtual void AddPhone(LocationPhone locationPhone)
        {
            Check.IsNotNull(locationPhone, "locationPhone is required.");

            DomainRuleEngine.CreateRuleEngine<Location, LocationPhone> ( Location, () => AddPhone )
                .WithContext ( locationPhone )
                .WithContext ( this )
                .Execute(() =>
                {
                    locationPhone.LocationAddressAndPhone = this;
                    _phoneNumbers.Add(locationPhone);
                    NotifyItemAdded(() => PhoneNumbers, locationPhone);
                });
        }

        /// <summary>
        /// The remove phone.
        /// </summary>
        /// <param name="locationPhone">
        /// The location phone.
        /// </param>
        public virtual void RemovePhone(LocationPhone locationPhone)
        {
            _phoneNumbers.Delete(locationPhone);
            NotifyItemRemoved(() => PhoneNumbers, locationPhone);
        }

        #endregion

        /// <summary>
        /// The revise location address.
        /// </summary>
        /// <param name="locationAddress">
        /// The location address.
        /// </param>
        public virtual void ReviseLocationAddress(LocationAddress locationAddress)
        {
            Check.IsNotNull(locationAddress, () => LocationAddress);

            DomainRuleEngine.CreateRuleEngine<Location, LocationAddress> ( Location, () => ReviseLocationAddress )
                .WithContext ( locationAddress )
                .WithContext ( this )
                .Execute(() => { LocationAddress = locationAddress; });
        }
    }
}
