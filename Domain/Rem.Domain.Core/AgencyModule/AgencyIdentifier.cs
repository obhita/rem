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

using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The AgencyIdentifier contains an indetifier associated to an Agency.
    /// </summary>
    public class AgencyIdentifier : AgencyAggregateNodeBase, IAggregateNodeValueObject
    {
        private AgencyIdentifierType _agencyIdentifierType;
        private DateRange _effectiveDateRange;
        private string _identifierNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyIdentifier"/> class.
        /// </summary>
        protected internal AgencyIdentifier ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyIdentifier"/> class.
        /// </summary>
        /// <param name="agencyIdentifierType">
        /// The agency identifier type.
        /// </param>
        /// <param name="identifierNumber">
        /// The identifier number.
        /// </param>
        /// <param name="effectiveDateRange">
        /// The effective date range.
        /// </param>
        protected internal AgencyIdentifier(AgencyIdentifierType agencyIdentifierType, string identifierNumber, DateRange effectiveDateRange)
        {
            Check.IsNotNull ( agencyIdentifierType, () => AgencyIdentifierType );
            Check.IsNotNullOrWhitespace ( identifierNumber, () => IdentifierNumber );

            _agencyIdentifierType = agencyIdentifierType;
            _identifierNumber = identifierNumber;
            _effectiveDateRange = effectiveDateRange;
        }

        /// <summary>
        /// Gets AgencyIdentifierType.
        /// </summary>
        [NotNull]
        public virtual AgencyIdentifierType AgencyIdentifierType
        {
            get { return _agencyIdentifierType; }
            private set { ApplyPropertyChange ( ref _agencyIdentifierType, () => AgencyIdentifierType, value ); }
        }

        /// <summary>
        /// Gets IdentifierNumber.
        /// </summary>
        [NotNull]
        public virtual string IdentifierNumber
        {
            get { return _identifierNumber; }
            private set { ApplyPropertyChange ( ref _identifierNumber, () => IdentifierNumber, value ); }
        }

        /// <summary>
        /// Gets EffectiveDateRange.
        /// </summary>
        public virtual DateRange EffectiveDateRange
        {
            get { return _effectiveDateRange; }
            private set { ApplyPropertyChange ( ref _effectiveDateRange, () => EffectiveDateRange, value ); }
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
        public virtual bool ValuesEqual(AgencyIdentifier other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual = Equals(_agencyIdentifierType, other.AgencyIdentifierType) && Equals(_identifierNumber, other.IdentifierNumber)
                              && Equals(_effectiveDateRange, other.EffectiveDateRange);

            return valuesEqual;
        }
    }
}