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
using Pillar.Domain.Primitives;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The LocationEmailAddress contains an email address associated to a location .
    /// </summary>
    public class LocationEmailAddress : LocationAggregateNodeBase, IAggregateNodeValueObject
    {
        private EmailAddress _emailAddress;
        private LocationEmailAddressType _locationEmailAddressType;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationEmailAddress"/> class.
        /// </summary>
        protected internal LocationEmailAddress()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationEmailAddress"/> class.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <param name="emailAddressType">
        /// The email address type.
        /// </param>
        public LocationEmailAddress(EmailAddress emailAddress, LocationEmailAddressType emailAddressType)
        {
            Check.IsNotNull(emailAddress, () => EmailAddress);
            Check.IsNotNull(emailAddressType, () => LocationEmailAddressType);

            _emailAddress = emailAddress;
            _locationEmailAddressType = emailAddressType;
        }

        /// <summary>
        /// Gets EmailAddress.
        /// </summary>
        public virtual EmailAddress EmailAddress
        {
            get { return _emailAddress; }
            private set { ApplyPropertyChange(ref _emailAddress, () => EmailAddress, value); }
        }

        /// <summary>
        /// Gets LocationEmailAddressType.
        /// </summary>
        [NotNull]
        public virtual LocationEmailAddressType LocationEmailAddressType
        {
            get { return _locationEmailAddressType; }
            private set { ApplyPropertyChange(ref _locationEmailAddressType, () => LocationEmailAddressType, value); }
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
        public virtual bool ValuesEqual(LocationEmailAddress other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual = Equals(_emailAddress, other.EmailAddress) && Equals(_locationEmailAddressType, other.LocationEmailAddressType);
            return valuesEqual;
        }
    }
}