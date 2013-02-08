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

using Pillar.Common;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientAddress defines elements of a patient address.
    /// </summary>
    public class PatientAddress : PatientAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        private bool? _confidentialIndicator;
        private PatientAddressType _patientAddressType;
        private int? _yearsOfStayNumber;

        private Address _address;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAddress"/> class.
        /// </summary>
        protected internal PatientAddress ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAddress"/> class.
        /// </summary>
        /// <param name="patientAddressType">Type of the patient address.</param>
        /// <param name="confidentialIndicator">The confidential indicator.</param>
        /// <param name="yearsOfStayNumber">The years of stay number.</param>
        /// <param name="address">The address.</param>
        public PatientAddress ( 
            PatientAddressType patientAddressType, 
            bool? confidentialIndicator,
            int? yearsOfStayNumber,
            Address address)
        {
            Check.IsNotNull ( patientAddressType, "Patient address type is required." );
            Check.IsNotNull ( address, "Address is required." );

            _patientAddressType = patientAddressType;
            _confidentialIndicator = confidentialIndicator;
            _yearsOfStayNumber = yearsOfStayNumber;
            _address = address;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the patient address.
        /// </summary>
        /// <value>
        /// The type of the patient address.
        /// </value>
        [NotNull]
        public virtual PatientAddressType PatientAddressType
        {
            get { return _patientAddressType; }
            private set { ApplyPropertyChange ( ref _patientAddressType, () => PatientAddressType, value ); }
        }

        /// <summary>
        /// Gets the first street address.
        /// </summary>
        [NotNull]
        public virtual Address Address
        {
            get { return _address; }
            private set { ApplyPropertyChange ( ref _address, () => Address, value ); }
        }

        /// <summary>
        /// Gets a boolean value indicating confidential.
        /// </summary>
        public virtual bool? ConfidentialIndicator
        {
            get { return _confidentialIndicator; }
            private set { ApplyPropertyChange(ref _confidentialIndicator, () => ConfidentialIndicator, value); }
        }

        /// <summary>
        /// Gets the years of stay number.
        /// </summary>
        public virtual int? YearsOfStayNumber
        {
            get { return _yearsOfStayNumber; }
            private set { ApplyPropertyChange(ref _yearsOfStayNumber, () => YearsOfStayNumber, value); }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="patientAddress">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>            
        public virtual bool ValuesEqual ( PatientAddress patientAddress )
        {
            if (patientAddress == null)
            {
                return false;
            }

            var valuesEqual =
                Equals ( PatientAddressType.Key, patientAddress.PatientAddressType.Key ) &&
                Equals ( Address, patientAddress.Address ) &&
                Equals ( ConfidentialIndicator, patientAddress.ConfidentialIndicator ) &&
                Equals ( YearsOfStayNumber, patientAddress.YearsOfStayNumber );

            return valuesEqual;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return string.Format ( "{0}, ({1})", _address.FirstStreetAddress, PatientAddressType );
        }


        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="obj">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>             
        public virtual bool ValuesEqual(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return ValuesEqual(obj as PatientAddress);
        }
    }
}
