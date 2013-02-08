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
    /// The LocationContact contains a contact associated to a location.
    /// </summary>
    public class LocationContact : LocationAggregateNodeBase, IAggregateNodeValueObject
    {
        private bool _alternativeContactIndicator;
        private Staff _contactStaff;
        private DateRange _effectiveDateRange;
        private LocationContactType _locationContactType;
        private bool _statusIndicator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationContact"/> class.
        /// </summary>
        protected internal LocationContact()
        {
            _statusIndicator = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationContact"/> class.
        /// </summary>
        /// <param name="locationContactType">Type of the location contact.</param>
        /// <param name="contactStaff">The contact staff.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <param name="statusIndicator">If set to <c>true</c> [status indicator].</param>
        /// <param name="alternativeContactIndicator">If set to <c>true</c> [alternative contact indicator].</param>
        protected internal LocationContact(
            LocationContactType locationContactType, 
            Staff contactStaff, 
            DateRange effectiveDateRange, 
            bool statusIndicator, 
            bool alternativeContactIndicator)
        {
            Check.IsNotNull(locationContactType, () => LocationContactType);
            Check.IsNotNull(contactStaff, () => ContactStaff);

            _locationContactType = locationContactType;
            _contactStaff = contactStaff;
            _effectiveDateRange = effectiveDateRange;
            _statusIndicator = statusIndicator;
            _alternativeContactIndicator = alternativeContactIndicator;
        }

        /// <summary>
        /// Gets LocationContactType.
        /// </summary>
        [NotNull]
        public virtual LocationContactType LocationContactType
        {
            get { return _locationContactType; }
            private set { ApplyPropertyChange(ref _locationContactType, () => LocationContactType, value); }
        }

        /// <summary>
        /// Gets ContactStaff.
        /// </summary>
        [NotNull]
        public virtual Staff ContactStaff
        {
            get { return _contactStaff; }
            private set { ApplyPropertyChange(ref _contactStaff, () => ContactStaff, value); }
        }

        /// <summary>
        /// Gets EffectiveDateRange.
        /// </summary>
        public virtual DateRange EffectiveDateRange
        {
            get { return _effectiveDateRange; }
            private set { ApplyPropertyChange(ref _effectiveDateRange, () => EffectiveDateRange, value); }
        }

        /// <summary>
        /// Gets a boolean value indicating an AlternativeContact.
        /// </summary>
        public virtual bool AlternativeContactIndicator
        {
            get { return _alternativeContactIndicator; }
            private set { ApplyPropertyChange(ref _alternativeContactIndicator, () => AlternativeContactIndicator, value); }
        }

        /// <summary>
        /// Gets a boolean value indicating a Status.
        /// </summary>
        [NotNull]
        public virtual bool StatusIndicator
        {
            get { return _statusIndicator; }
            private set { ApplyPropertyChange(ref _statusIndicator, () => StatusIndicator, value); }
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
        public virtual bool ValuesEqual(LocationContact other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual = Equals(_locationContactType, other.LocationContactType) && Equals(_contactStaff, other.ContactStaff)
                              && Equals(_effectiveDateRange, other.EffectiveDateRange) && Equals(_statusIndicator, other.StatusIndicator)
                              && Equals(_alternativeContactIndicator, other.AlternativeContactIndicator);

            return valuesEqual;
        }
    }
}