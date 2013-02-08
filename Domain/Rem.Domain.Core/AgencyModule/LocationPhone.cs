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
    /// The location phone.
    /// </summary>
    public class LocationPhone : AuditableAggregateNodeBase, IAggregateNodeValueObject
    {
        private LocationPhoneType _locationPhoneType;
        private Phone _phone;
        private LocationAddressAndPhone _locationAddressAndPhone;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationPhone"/> class.
        /// </summary>
        protected internal LocationPhone()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationPhone"/> class.
        /// </summary>
        /// <param name="locationPhoneType">
        /// The location phone type.
        /// </param>
        /// <param name="phone">
        /// The phone.
        /// </param>
        public LocationPhone(LocationPhoneType locationPhoneType, Phone phone)
        {
            Check.IsNotNull(locationPhoneType, () => LocationPhoneType);
            Check.IsNotNull(phone, () => Phone);

            _locationPhoneType = locationPhoneType;
            _phone = phone;
        }

        /// <summary>
        /// Gets or sets LocationAddressAndPhone.
        /// </summary>
        [NotNull]
        public virtual LocationAddressAndPhone LocationAddressAndPhone
        {
            get { return _locationAddressAndPhone; }
            protected internal set { ApplyPropertyChange(ref _locationAddressAndPhone, () => LocationAddressAndPhone, value); }
        }

        /// <summary>
        /// Gets the location Phone.
        /// </summary>
        [NotNull]
        public virtual Phone Phone
        {
            get { return _phone; }
            private set { ApplyPropertyChange(ref _phone, () => Phone, value); }
        }

        /// <summary>
        /// Gets LocationPhoneType.
        /// </summary>
        [NotNull]
        public virtual LocationPhoneType LocationPhoneType
        {
            get { return _locationPhoneType; }
            private set { ApplyPropertyChange(ref _locationPhoneType, () => LocationPhoneType, value); }
        }

        #region Overrides of AbstractAggregateNode

        /// <summary>
        /// Gets AggregateRoot.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return LocationAddressAndPhone.Location; }
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
        public virtual bool ValuesEqual(LocationPhone other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual = Equals(_phone, other.Phone)
                              && Equals(_locationPhoneType, other.LocationPhoneType);

            return valuesEqual;
        }
    }
}