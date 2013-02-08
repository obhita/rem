using Pillar.Common;
using Pillar.Domain;

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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// The PatientContactPhone defines phone numbers for patient contacts.
    /// </summary>
    public class PatientContactPhone : PatientContactAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        private bool? _confidentialIndicator;
        private PatientContactPhoneType _patientContactPhoneType;
        private string _phoneExtensionNumber;
        private string _phoneNumber;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContactPhone"/> class.
        /// </summary>
        protected internal PatientContactPhone ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContactPhone"/> class.
        /// </summary>
        /// <param name="patientContactPhoneType">Type of the patient contact phone.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="phoneExtensionNumber">The phone extension number.</param>
        /// <param name="confidentialIndicator">The confidential indicator.</param>
        protected internal PatientContactPhone ( 
            PatientContactPhoneType patientContactPhoneType,
            string phoneNumber,
            string phoneExtensionNumber,
            bool? confidentialIndicator)
        {
            _patientContactPhoneType = patientContactPhoneType;
            _phoneNumber = phoneNumber;
            _phoneExtensionNumber = phoneExtensionNumber;
            _confidentialIndicator = confidentialIndicator;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the patient contact phone.
        /// </summary>
        /// <value>
        /// The type of the patient contact phone.
        /// </value>
        [NotNull]
        public virtual PatientContactPhoneType PatientContactPhoneType
        {
            get { return _patientContactPhoneType; }
            private set { ApplyPropertyChange ( ref _patientContactPhoneType, () => PatientContactPhoneType, value ); }
        }

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        [NotNull]
        public virtual string PhoneNumber
        {
            get { return _phoneNumber; }
            private set { ApplyPropertyChange ( ref _phoneNumber, () => PhoneNumber, value ); }
        }

        /// <summary>
        /// Gets the phone extension number.
        /// </summary>
        public virtual string PhoneExtensionNumber
        {
            get { return _phoneExtensionNumber; }
            private set { ApplyPropertyChange ( ref _phoneExtensionNumber, () => PhoneExtensionNumber, value ); }
        }

        /// <summary>
        /// Gets the confidential indicator.
        /// </summary>
        public virtual bool? ConfidentialIndicator
        {
            get { return _confidentialIndicator; }
            private set { ApplyPropertyChange ( ref _confidentialIndicator, () => ConfidentialIndicator, value ); }
        }

        #endregion


        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="patientContactPhone">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>          
        public virtual bool ValuesEqual(PatientContactPhone patientContactPhone)
        {
            if (patientContactPhone == null)
            {
                return false;
            }

            var valuesEqual =
                Equals(PatientContactPhoneType, patientContactPhone.PatientContactPhoneType) &&
                Equals(PhoneNumber, patientContactPhone.PhoneNumber) &&
                Equals(ConfidentialIndicator, patientContactPhone.ConfidentialIndicator) &&
                Equals(PatientContactPhoneType, patientContactPhone.PatientContactPhoneType) &&
                Equals(PhoneNumber, patientContactPhone.PhoneNumber) &&
                Equals(PhoneExtensionNumber, patientContactPhone.PhoneExtensionNumber) &&
                Equals(ConfidentialIndicator, patientContactPhone.ConfidentialIndicator);

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
            return string.IsNullOrWhiteSpace ( PhoneNumber ) ? Key.ToString () : PhoneNumber;
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

            return ValuesEqual(obj as PatientContactPhone);
        }
    }
}
