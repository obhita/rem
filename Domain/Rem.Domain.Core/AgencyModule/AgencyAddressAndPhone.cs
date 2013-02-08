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

using System.Collections.Generic;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The AgencyAddressAndPhone contains agency address and phone.
    /// </summary>
    public class AgencyAddressAndPhone : AgencyAggregateNodeBase
    {
        private readonly IList<AgencyPhone> _phoneNumbers;
        private AgencyAddress _agencyAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyAddressAndPhone"/> class.
        /// </summary>
        protected internal AgencyAddressAndPhone()
        {
            _phoneNumbers = new List<AgencyPhone>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyAddressAndPhone"/> class.
        /// </summary>
        /// <param name="agencyAddress">
        /// The agency address.
        /// </param>
        protected internal AgencyAddressAndPhone(AgencyAddress agencyAddress)
            : this()
        {
            Check.IsNotNull(agencyAddress, () => AgencyAddress);

            _agencyAddress = agencyAddress;
        }

        /// <summary>
        /// Gets the agency address.
        /// </summary>
        [NotNull]
        public virtual AgencyAddress AgencyAddress
        {
            get { return _agencyAddress; }
            private set { ApplyPropertyChange(ref _agencyAddress, () => AgencyAddress, value); }
        }

        /// <summary>
        /// Gets the phone numbers.
        /// </summary>
        public virtual IEnumerable<AgencyPhone> PhoneNumbers
        {
            get { return _phoneNumbers; }
            private set { }
        }

        #region Collection Methods

        /// <summary>
        /// Adds the phone.
        /// </summary>
        /// <param name="agencyPhone">
        /// The agency phone.
        /// </param>
        public virtual void AddPhone(AgencyPhone agencyPhone)
        {
            Check.IsNotNull(agencyPhone, "agencyPhone is required.");

            DomainRuleEngine.CreateRuleEngine<Agency, AgencyPhone> ( Agency, () => AddPhone )
                .WithContext ( agencyPhone )
                .WithContext ( this )
                .Execute(() =>
                {
                    agencyPhone.AgencyAddressAndPhone = this;
                    _phoneNumbers.Add(agencyPhone);
                    NotifyItemAdded(() => PhoneNumbers, agencyPhone);
                });
        }

        /// <summary>
        /// Removes the phone.
        /// </summary>
        /// <param name="agencyPhone">
        /// The agency phone.
        /// </param>
        public virtual void RemovePhone(AgencyPhone agencyPhone)
        {
            _phoneNumbers.Delete(agencyPhone);
            NotifyItemRemoved(() => PhoneNumbers, agencyPhone);
        }

        #endregion

        /// <summary>
        /// Revises the agency address.
        /// </summary>
        /// <param name="agencyAddress">
        /// The agency address.
        /// </param>
        public virtual void ReviseAgencyAddress(AgencyAddress agencyAddress)
        {
            Check.IsNotNull(agencyAddress, () => AgencyAddress);

            DomainRuleEngine.CreateRuleEngine<Agency, AgencyAddress> ( Agency, () => ReviseAgencyAddress )
                .WithContext ( agencyAddress )
                .WithContext ( this )
                .Execute(() => AgencyAddress = agencyAddress);
        }
    }
}
