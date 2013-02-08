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
    /// AgencyPhoneBuilder provides a fluent interface for creating a AgencyPhone.
    /// </summary>
    public class AgencyPhoneBuilder
    {
        private AgencyPhoneType _agencyPhoneType;
        private Phone _phone;

        /// <summary>
        /// Performs an implicit conversion from <see cref="AgencyPhoneBuilder"/> to <see cref="AgencyPhone"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator AgencyPhone(AgencyPhoneBuilder builder)
        {
            return builder.Build();
        }


        /// <summary>
        /// Assigns the type of the agency phone.
        /// </summary>
        /// <param name="agencyPhoneType">Type of the agency phone.</param>
        /// <returns>An AgencyPhoneBuilder.</returns>
        public AgencyPhoneBuilder WithAgencyPhoneType(AgencyPhoneType agencyPhoneType)
        {
            _agencyPhoneType = agencyPhoneType;
            return this;
        }

        /// <summary>
        /// Assigns the phone.
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <returns>An AgencyPhoneBuilder.</returns>
        public AgencyPhoneBuilder WithPhone(Phone phone)
        {
            _phone = phone;
            return this;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <returns>
        /// An AgencyPhone.
        /// </returns>
        public AgencyPhone Build()
        {
            return new AgencyPhone(_agencyPhoneType, _phone);
        }
    }
}