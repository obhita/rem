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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The StaffAddress defines an address associated to a staff.
    /// </summary>
    public class StaffAddress : StaffAggregateNodeBase, IAggregateNodeValueObject
    {
        private Address _address;
        private bool? _confidentialIndicator;
        private StaffAddressType _staffAddressType;
        private int? _yearsOfStayNumber;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffAddress"/> class.
        /// </summary>
        protected internal StaffAddress ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffAddress"/> class.
        /// </summary>
        /// <param name="staffAddressType">Type of the staff address.</param>
        /// <param name="address">The address.</param>
        /// <param name="confidentialIndicator">The confidential indicator.</param>
        /// <param name="yearsOfStayNumber">The years of stay number.</param>
        public StaffAddress ( 
            StaffAddressType staffAddressType, 
            Address address,
            bool? confidentialIndicator,
            int? yearsOfStayNumber)
        {
            Check.IsNotNull ( staffAddressType, "Staff address type is required." );
            Check.IsNotNull (address, "Address is required.");

            _staffAddressType = staffAddressType;
            _address = address;
            _confidentialIndicator = confidentialIndicator;
            _yearsOfStayNumber = yearsOfStayNumber;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the staff address.
        /// </summary>
        /// <value>
        /// The type of the staff address.
        /// </value>
        [NotNull]
        public virtual StaffAddressType StaffAddressType
        {
            get { return _staffAddressType; }
            private set { ApplyPropertyChange ( ref _staffAddressType, () => StaffAddressType, value ); }
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        [NotNull]
        public virtual Address Address
        {
            get { return _address; }
            private set { ApplyPropertyChange(ref _address, () => Address, value); }
        }

        /// <summary>
        /// Gets the confidential indicator.
        /// </summary>
        public virtual bool? ConfidentialIndicator
        {
            get { return _confidentialIndicator; }
            private set { ApplyPropertyChange ( ref _confidentialIndicator, () => ConfidentialIndicator, value ); }
        }

        /// <summary>
        /// Gets the years of stay number.
        /// </summary>
        public virtual int? YearsOfStayNumber
        {
            get { return _yearsOfStayNumber; }
            private set { ApplyPropertyChange ( ref _yearsOfStayNumber, () => YearsOfStayNumber, value ); }
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
        public virtual bool ValuesEqual(StaffAddress other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual =
                Equals(_staffAddressType.Key, other.StaffAddressType.Key) &&
                Equals(_address, other.Address) &&
                Equals(_confidentialIndicator, other.ConfidentialIndicator) &&
                Equals(_yearsOfStayNumber, other.YearsOfStayNumber);

            return valuesEqual;
        }
    }
}