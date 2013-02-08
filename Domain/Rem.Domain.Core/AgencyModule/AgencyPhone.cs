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

using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The AgencyPhone defines a phone number for an agency.
    /// </summary>
    public class AgencyPhone : AuditableAggregateNodeBase, IAggregateNodeValueObject
    {
        private AgencyAddressAndPhone _agencyAddressAndPhone;
        private AgencyPhoneType _agencyPhoneType;
        private Phone _phone;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyPhone"/> class.
        /// </summary>
        protected internal AgencyPhone()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyPhone"/> class.
        /// </summary>
        /// <param name="agencyPhoneType">
        /// The agency phone type.
        /// </param>
        /// <param name="phone">
        /// The phone.
        /// </param>
        protected internal AgencyPhone(AgencyPhoneType agencyPhoneType, Phone phone)
        {
            Check.IsNotNull(agencyPhoneType, () => AgencyPhoneType);
            Check.IsNotNull(phone, () => Phone);

            _agencyPhoneType = agencyPhoneType;
            _phone = phone;
        }

        /// <summary>
        /// Gets or sets AgencyAddressAndPhone.
        /// </summary>
        [NotNull]
        public virtual AgencyAddressAndPhone AgencyAddressAndPhone
        {
            get { return _agencyAddressAndPhone; }
            protected internal set { ApplyPropertyChange(ref _agencyAddressAndPhone, () => AgencyAddressAndPhone, value); }
        }

        /// <summary>
        /// Gets Phone.
        /// </summary>
        [NotNull]
        public virtual Phone Phone
        {
            get { return _phone; }
            private set { ApplyPropertyChange(ref _phone, () => Phone, value); }
        }

        /// <summary>
        /// Gets AgencyPhoneType.
        /// </summary>
        [NotNull]
        public virtual AgencyPhoneType AgencyPhoneType
        {
            get { return _agencyPhoneType; }
            private set { ApplyPropertyChange(ref _agencyPhoneType, () => AgencyPhoneType, value); }
        }

        #region Overrides of AbstractAggregateNode

        /// <summary>
        /// Gets the agregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return AgencyAddressAndPhone.Agency; }
        }

        #endregion


        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="other">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>             
        public virtual bool ValuesEqual(AgencyPhone other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual = Equals(_phone, other.Phone)
                              && Equals(_agencyPhoneType, other.AgencyPhoneType);

            return valuesEqual;
        }
    }
}