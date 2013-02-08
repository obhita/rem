// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StaffAddressBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   StaffAddressBuilder provides a fluent interface for creating a StaffAddress.
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

using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// StaffAddressBuilder provides a fluent interface for creating a StaffAddress.
    /// </summary>
    public class StaffAddressBuilder
    {
        private Address _address;
        private bool? _confidentialIndicator;
        private StaffAddressType _staffAddressType;
        private int? _yearsOfStayNumber;

        /// <summary>
        /// Performs an implicit conversion from <see cref="StaffAddressBuilder"/> to <see cref="StaffAddress"/>.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator StaffAddress(StaffAddressBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the type of the staff address.
        /// </summary>
        /// <param name="staffAddressType">
        /// Type of the staff address.
        /// </param>
        /// <returns>
        /// A StaffAddressBuilder.
        /// </returns>
        public StaffAddressBuilder WithStaffAddressType ( StaffAddressType staffAddressType )
        {
            _staffAddressType = staffAddressType;
            return this;
        }

        /// <summary>
        /// Assigns the address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        /// <returns>
        /// A StaffAddressBuilder.
        /// </returns>
        public StaffAddressBuilder WithAddress ( Address address )
        {
            _address = address;
            return this;
        }

        /// <summary>
        /// Assigns the confidential indicator.
        /// </summary>
        /// <param name="confidentialIndicator">
        /// The confidential indicator.
        /// </param>
        /// <returns>
        /// A StaffAddressBuilder.
        /// </returns>
        public StaffAddressBuilder WithConfidentialIndicator ( bool? confidentialIndicator )
        {
            _confidentialIndicator = confidentialIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the years of stay number.
        /// </summary>
        /// <param name="yearsOfStayNumber">
        /// The years of stay number.
        /// </param>
        /// <returns>
        /// A StaffAddressBuilder.
        /// </returns>
        public StaffAddressBuilder WithYearsOfStayNumber ( int? yearsOfStayNumber )
        {
            _yearsOfStayNumber = yearsOfStayNumber;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// A StaffAddress.
        /// </returns>
        public StaffAddress Build ()
        {
            return new StaffAddress ( _staffAddressType, _address, _confidentialIndicator, _yearsOfStayNumber );
        }
    }
}
