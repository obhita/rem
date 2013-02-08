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

using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientBirthInfoBuilder provides a fluent interface for creating a patient birth information.
    /// </summary>
    public class PatientBirthInfoBuilder
    {
        private string _birthCityName;
        private CountyArea _birthCountyArea;
        private string _birthFirstName;
        private string _birthLastName;
        private StateProvince _birthStateProvince;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PatientBirthInfoBuilder"/> to <see cref="PatientBirthInfo"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator PatientBirthInfo(PatientBirthInfoBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the first name of the birth.
        /// </summary>
        /// <param name="birthFirstName">First name of the birth.</param>
        /// <returns>A PatientBirthInfoBuilder.</returns>
        public PatientBirthInfoBuilder WithBirthFirstName(string birthFirstName)
        {
            _birthFirstName = birthFirstName;
            return this;
        }

        /// <summary>
        /// Assigns the last name of the birth.
        /// </summary>
        /// <param name="birthLastName">Last name of the birth.</param>
        /// <returns>A PatientBirthInfoBuilder.</returns>
        public PatientBirthInfoBuilder WithBirthLastName(string birthLastName)
        {
            _birthLastName = birthLastName;
            return this;
        }

        /// <summary>
        /// Assigns the name of the birth city.
        /// </summary>
        /// <param name="birthCityName">Name of the birth city.</param>
        /// <returns>A PatientBirthInfoBuilder.</returns>
        public PatientBirthInfoBuilder WithBirthCityName(string birthCityName)
        {
            _birthCityName = birthCityName;
            return this;
        }

        /// <summary>
        /// Assigns the birth county area.
        /// </summary>
        /// <param name="birthCountyArea">The birth county area.</param>
        /// <returns>A PatientBirthInfoBuilder.</returns>
        public PatientBirthInfoBuilder WithBirthCountyArea(CountyArea birthCountyArea)
        {
            _birthCountyArea = birthCountyArea;
            return this;
        }

        /// <summary>
        /// Assigns the birth state province.
        /// </summary>
        /// <param name="birthStateProvince">The birth state province.</param>
        /// <returns>A PatientBirthInfoBuilder.</returns>
        public PatientBirthInfoBuilder WithBirthStateProvince(StateProvince birthStateProvince)
        {
            _birthStateProvince = birthStateProvince;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A PatientBirthInfo.</returns>
        public PatientBirthInfo Build()
        {
            return new PatientBirthInfo(_birthFirstName, _birthLastName, _birthCityName, _birthCountyArea, _birthStateProvince);
        }
    }
}