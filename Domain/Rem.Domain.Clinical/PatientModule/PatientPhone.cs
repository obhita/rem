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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientPhone defines a patient telephone number.
    /// </summary>
    public class PatientPhone : PatientAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        private bool? _confidentialIndicator;
        private PatientPhoneType _patientPhoneType;
        private string _phoneExtensionNumber;
        private string _phoneNumber;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientPhone"/> class.
        /// </summary>
        protected internal PatientPhone ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientPhone"/> class.
        /// </summary>
        /// <param name="patientPhoneType">Type of the patient phone.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="phoneExtensionNumber">The phone extension number.</param>
        /// <param name="confidentialIndicator">The confidential indicator.</param>
        public PatientPhone (
            PatientPhoneType patientPhoneType, 
            string phoneNumber,
            string phoneExtensionNumber = null,
            bool? confidentialIndicator= null )
        {
            Check.IsNotNull ( patientPhoneType, "Patient Phone Type is required." );
            Check.IsNotNullOrWhitespace ( phoneNumber, "Phone number is required." );

            _patientPhoneType = patientPhoneType;
            _phoneNumber = phoneNumber;
            _phoneExtensionNumber = phoneExtensionNumber;
            _confidentialIndicator = confidentialIndicator;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the patient phone.
        /// </summary>
        /// <value>
        /// The type of the patient phone.
        /// </value>
        [NotNull]
        public virtual PatientPhoneType PatientPhoneType
        {
            get { return _patientPhoneType; }
            private set { ApplyPropertyChange ( ref _patientPhoneType, () => PatientPhoneType, value ); }
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
        /// <param name="patientPhone">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>           
        public virtual bool ValuesEqual ( PatientPhone patientPhone )
        {
            if (patientPhone == null)
            {
                return false;
            }

            var valuesEqual =
                Equals ( PatientPhoneType.Key, patientPhone.PatientPhoneType.Key ) &&
                Equals ( PhoneNumber, patientPhone.PhoneNumber ) &&
                Equals ( PhoneExtensionNumber, patientPhone.PhoneExtensionNumber ) &&
                Equals ( _confidentialIndicator, patientPhone._confidentialIndicator );
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
            return string.Format ( "{0} ({1})", PhoneNumber, PatientPhoneType );
        }

        /// <summary>
        /// Checks if all the values of the object are equal.
        /// </summary>
        /// <param name="obj">The object to check equality with.</param>
        /// <returns>
        /// A bool indicating whether objects are equal.
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

            return ValuesEqual(obj as PatientPhone);
        }
    }
}
