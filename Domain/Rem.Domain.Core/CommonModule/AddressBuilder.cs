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

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// AddressBuilder provides a fluent interface for creating an address value object.
    /// </summary>
    public class AddressBuilder
    {
        private string _cityName;
        private Country _country;
        private CountyArea _countyArea;
        private string _firstStreetAddress;
        private PostalCode _postalCode;
        private string _secondStreetAddress;
        private StateProvince _stateProvince;

        /// <summary>
        /// Performs an implicit conversion from <see cref="AddressBuilder"/> to <see cref="Address"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Address(AddressBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the first street address.
        /// </summary>
        /// <param name="firstStreetAddress">The first street address.</param>
        /// <returns>An AddressBuilder.</returns>
        public AddressBuilder WithFirstStreetAddress(string firstStreetAddress)
        {
            _firstStreetAddress = firstStreetAddress;
            return this;
        }

        /// <summary>
        /// Assigns the second street address.
        /// </summary>
        /// <param name="secondStreetAddress">The second street address.</param>
        /// <returns>An AddressBuilder.</returns>
        public AddressBuilder WithSecondStreetAddress(string secondStreetAddress)
        {
            _secondStreetAddress = secondStreetAddress;
            return this;
        }

        /// <summary>
        /// Assigns the name of the city.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <returns>An AddressBuilder.</returns>
        public AddressBuilder WithCityName(string cityName)
        {
            _cityName = cityName;
            return this;
        }

        /// <summary>
        /// Assigns the county area.
        /// </summary>
        /// <param name="countyArea">The county area.</param>
        /// <returns>An AddressBuilder.</returns>
        public AddressBuilder WithCountyArea(CountyArea countyArea)
        {
            _countyArea = countyArea;
            return this;
        }

        /// <summary>
        /// Assigns the state province.
        /// </summary>
        /// <param name="stateProvince">The state province.</param>
        /// <returns>An AddressBuilder.</returns>
        public AddressBuilder WithStateProvince(StateProvince stateProvince)
        {
            _stateProvince = stateProvince;
            return this;
        }

        /// <summary>
        /// Assigns the country.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns>An AddressBuilder.</returns>
        public AddressBuilder WithCountry(Country country)
        {
            _country = country;
            return this;
        }

        /// <summary>
        /// Assigns the postal code.
        /// </summary>
        /// <param name="postalCode">The postal code.</param>
        /// <returns>An AddressBuilder.</returns>
        public AddressBuilder WithPostalCode(PostalCode postalCode)
        {
            _postalCode = postalCode;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>An Address.</returns>
        public Address Build()
        {
            return new Address(_firstStreetAddress, _secondStreetAddress, _cityName, _countyArea, _stateProvince, _country, _postalCode);
        }
    }
}