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
    /// StaffIdentifier defines unique identity of a staff.
    /// </summary>
    public class StaffIdentifier : StaffAggregateNodeBase, IAggregateNodeValueObject
    {
        private DateRange _effectiveDateRange;
        private string _identifierNumber;
        private StaffIdentifierType _staffIdentifierType;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffIdentifier"/> class.
        /// </summary>
        protected internal StaffIdentifier ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffIdentifier"/> class.
        /// </summary>
        /// <param name="staffIdentifierType">Type of the staff identifier.</param>
        /// <param name="identifierNumber">The identifier number.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        public StaffIdentifier(StaffIdentifierType staffIdentifierType, string identifierNumber, DateRange effectiveDateRange) 
        {
            Check.IsNotNull(staffIdentifierType, "Staff identifier type is required.");
            Check.IsNotNullOrWhitespace(identifierNumber, "Identifier number is required.");

            _staffIdentifierType = staffIdentifierType;
            _identifierNumber = identifierNumber;
            _effectiveDateRange = effectiveDateRange;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the identifier number.
        /// </summary>
        [NotNull]
        public virtual string IdentifierNumber
        {
            get { return _identifierNumber; }
            private set {  ApplyPropertyChange ( ref _identifierNumber, () => IdentifierNumber, value ); }
        }

        /// <summary>
        /// Gets the effective date range.
        /// </summary>
        public virtual DateRange EffectiveDateRange
        {
            get { return _effectiveDateRange; }
            private set { ApplyPropertyChange ( ref _effectiveDateRange, () => EffectiveDateRange, value ); }
        }

        /// <summary>
        /// Gets the type of the staff identifier.
        /// </summary>
        /// <value>
        /// The type of the staff identifier.
        /// </value>
        [NotNull]
        public virtual StaffIdentifierType StaffIdentifierType
        {
            get { return _staffIdentifierType; }
            private set { ApplyPropertyChange(ref _staffIdentifierType, () => StaffIdentifierType, value); }
        }

        #endregion
            
        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="staffIdentifier">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns> 
        public virtual bool ValuesEqual(StaffIdentifier staffIdentifier)
        {
            if (staffIdentifier == null)
            {
                return false;
            }

            var valuesEqual =
                Equals ( _staffIdentifierType.Key, staffIdentifier._staffIdentifierType.Key ) &&
                Equals ( _identifierNumber, staffIdentifier._identifierNumber ) &&
                Equals ( _effectiveDateRange, staffIdentifier._effectiveDateRange );
            return valuesEqual;
        }
    }
}