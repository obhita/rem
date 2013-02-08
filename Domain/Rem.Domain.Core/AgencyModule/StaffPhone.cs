// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaffPhone.cs" company="">
//   
// </copyright>
// <summary>
//   The StaffPhone defines a phone for a staff.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    /// The StaffPhone defines a phone for a staff.
    /// </summary>
    public class StaffPhone : StaffAggregateNodeBase, IAggregateNodeValueObject
    {
        /// <summary>
        /// The _confidential indicator.
        /// </summary>
        private bool _confidentialIndicator;

        /// <summary>
        /// The _phone.
        /// </summary>
        private Phone _phone;

        /// <summary>
        /// The _staff phone type.
        /// </summary>
        private StaffPhoneType _staffPhoneType;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffPhone"/> class.
        /// </summary>
        protected internal StaffPhone ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffPhone"/> class.
        /// </summary>
        /// <param name="staffPhoneType">
        /// Type of the staff phone.
        /// </param>
        /// <param name="phone">
        /// The phone.
        /// </param>
        /// <param name="confidentialIndicator">
        /// If set to <c>true</c> [confidential indicator].
        /// </param>
        public StaffPhone ( StaffPhoneType staffPhoneType, Phone phone, bool confidentialIndicator )
        {
            Check.IsNotNull ( staffPhoneType, () => StaffPhoneType );
            Check.IsNotNull ( phone, () => Phone );

            _staffPhoneType = staffPhoneType;
            _phone = phone;
            _confidentialIndicator = confidentialIndicator;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the staff phone.
        /// </summary>
        /// <value>
        /// The type of the staff phone.
        /// </value>
        [NotNull]
        public virtual StaffPhoneType StaffPhoneType
        {
            get { return _staffPhoneType; }
            private set { ApplyPropertyChange ( ref _staffPhoneType, () => StaffPhoneType, value ); }
        }

        /// <summary>
        /// Gets the phone.
        /// </summary>
        [NotNull]
        public virtual Phone Phone
        {
            get { return _phone; }
            private set { ApplyPropertyChange ( ref _phone, () => Phone, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether [confidential indicator].
        /// </summary>
        /// <value>
        /// <c>true</c> if [confidential indicator]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ConfidentialIndicator
        {
            get { return _confidentialIndicator; }
            private set { ApplyPropertyChange ( ref _confidentialIndicator, () => ConfidentialIndicator, value ); }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="staffPhone">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>
        public virtual bool ValuesEqual ( StaffPhone staffPhone )
        {
            if ( staffPhone == null )
            {
                return false;
            }

            var valuesEqual = Equals ( _staffPhoneType.Key, staffPhone._staffPhoneType.Key ) && Equals ( _phone, staffPhone._phone )
                              && Equals ( _confidentialIndicator, staffPhone._confidentialIndicator );
            return valuesEqual;
        }
    }
}
