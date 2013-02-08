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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientAddressBuilder provides a fluent interface for creating a patient address.
    /// </summary>
    public class PatientAddressBuilder
    {
        private bool? _confidentialIndicator;
        private PatientAddressType _patientAddressType;
        private int? _yearsOfStayNumber;

        private Address _address;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PatientAddressBuilder"/> to <see cref="PatientAddress"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator PatientAddress(PatientAddressBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the type of the patient address.
        /// </summary>
        /// <param name="patientAddressType">Type of the patient address.</param>
        /// <returns>A PatientAddressBuilder.</returns>
        public PatientAddressBuilder WithPatientAddressType ( PatientAddressType patientAddressType )
        {
            _patientAddressType = patientAddressType;
            return this;
        }

        /// <summary>
        /// Assigns the first street address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>
        /// A PatientAddressBuilder.
        /// </returns>
        public PatientAddressBuilder WithAddress ( Address address )
        {
            _address = address;
            return this;
        }

        /// <summary>
        /// Assigns the confidential indicator.
        /// </summary>
        /// <param name="confidentialIndicator">The confidential indicator.</param>
        /// <returns>A PatientAddressBuilder.</returns>
        public PatientAddressBuilder WithConfidentialIndicator ( bool? confidentialIndicator )
        {
            _confidentialIndicator = confidentialIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the years of stay number.
        /// </summary>
        /// <param name="yearsOfStayNumber">The years of stay number.</param>
        /// <returns>A PatientAddressBuilder.</returns>
        public PatientAddressBuilder WithYearsOfStayNumber ( int? yearsOfStayNumber )
        {
            _yearsOfStayNumber = yearsOfStayNumber;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A PatientAddress.</returns>
        public PatientAddress Build ()
        {
            return new PatientAddress (
                _patientAddressType,
                _confidentialIndicator,
                _yearsOfStayNumber,
                _address);
        }
    }
}