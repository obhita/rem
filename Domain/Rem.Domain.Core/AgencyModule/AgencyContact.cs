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

using System;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The AgencyContact contains contacts associated to the agency.
    /// </summary>
    public class AgencyContact : AgencyAggregateNodeBase, IAggregateNodeValueObject
    {
        private AgencyContactType _agencyContactType;
        private bool _alternativeContactIndicator;
        private Staff _contactStaff;
        private DateTime? _effectiveStartDate;
        private bool _statusIndicator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyContact"/> class.
        /// </summary>
        protected internal AgencyContact()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyContact"/> class.
        /// </summary>
        /// <param name="agencyContactType">
        /// The agency contact type.
        /// </param>
        /// <param name="contactStaff">
        /// The contact staff.
        /// </param>
        /// <param name="effectiveStartDate">
        /// The effective start date.
        /// </param>
        /// <param name="statusIndicator">
        /// The status indicator.
        /// </param>
        /// <param name="alternativeContactIndicator">
        /// The alternative contact indicator.
        /// </param>
        protected internal AgencyContact(
            AgencyContactType agencyContactType, 
            Staff contactStaff, 
            DateTime? effectiveStartDate, 
            bool statusIndicator, 
            bool alternativeContactIndicator)
        {
            Check.IsNotNull(agencyContactType, () => AgencyContactType);
            Check.IsNotNull(contactStaff, () => ContactStaff);

            _agencyContactType = agencyContactType;
            _contactStaff = contactStaff;
            _effectiveStartDate = effectiveStartDate;
            _statusIndicator = statusIndicator;
            _alternativeContactIndicator = alternativeContactIndicator;
        }

        /// <summary>
        /// Gets AgencyContactType.
        /// </summary>
        [NotNull]
        public virtual AgencyContactType AgencyContactType
        {
            get { return _agencyContactType; }
            private set { ApplyPropertyChange(ref _agencyContactType, () => AgencyContactType, value); }
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
        /// Gets EffectiveStartDate.
        /// </summary>
        public virtual DateTime? EffectiveStartDate
        {
            get { return _effectiveStartDate; }
            private set { ApplyPropertyChange(ref _effectiveStartDate, () => EffectiveStartDate, value); }
        }

        /// <summary>
        /// Gets a value indicating whether StatusIndicator.
        /// </summary>
        [NotNull]
        public virtual bool StatusIndicator
        {
            get { return _statusIndicator; }
            private set { ApplyPropertyChange(ref _statusIndicator, () => StatusIndicator, value); }
        }

        /// <summary>
        /// Gets a value indicating whether AlternativeContactIndicator.
        /// </summary>
        public virtual bool AlternativeContactIndicator
        {
            get { return _alternativeContactIndicator; }
            private set { ApplyPropertyChange(ref _alternativeContactIndicator, () => AlternativeContactIndicator, value); }
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
        public virtual bool ValuesEqual(AgencyContact other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual = Equals(_agencyContactType, other.AgencyContactType) && Equals(_contactStaff, other.ContactStaff)
                              && Equals(_effectiveStartDate, other.EffectiveStartDate) && Equals(_statusIndicator, other.StatusIndicator)
                              && Equals(_alternativeContactIndicator, other.AlternativeContactIndicator);

            return valuesEqual;
        }
    }
}