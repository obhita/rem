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

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// LocationProfileBuilder provides a fluent interface for creating a LocationProfile.
    /// </summary>
    public class LocationProfileBuilder
    {
        private CountyArea _countyArea;
        private DateRange _effectiveDateRange;
        private GeographicalRegion _geographicalRegion;
        private HipaaServiceLocation _hipaaServiceLocation;
        private LocationName _locationName;
        private string _websiteUrlName;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LocationProfileBuilder"/> to <see cref="LocationProfile"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LocationProfile(LocationProfileBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the name of the location.
        /// </summary>
        /// <param name="locationName">Name of the location.</param>
        /// <returns>A LocationProfileBuilder.</returns>
        public LocationProfileBuilder WithLocationName(LocationName locationName)
        {
            _locationName = locationName;
            return this;
        }

        /// <summary>
        /// Assigns the effective date range.
        /// </summary>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <returns>A LocationProfileBuilder.</returns>
        public LocationProfileBuilder WithEffectiveDateRange(DateRange effectiveDateRange)
        {
            _effectiveDateRange = effectiveDateRange;
            return this;
        }

        /// <summary>
        /// Assigns the name of the website URL.
        /// </summary>
        /// <param name="websiteUrlName">Name of the website URL.</param>
        /// <returns>A LocationProfileBuilder.</returns>
        public LocationProfileBuilder WithWebsiteUrlName(string websiteUrlName)
        {
            _websiteUrlName = websiteUrlName;
            return this;
        }

        /// <summary>
        /// Assigns the county area.
        /// </summary>
        /// <param name="countyArea">The county area.</param>
        /// <returns>A LocationProfileBuilder.</returns>
        public LocationProfileBuilder WithCountyArea(CountyArea countyArea)
        {
            _countyArea = countyArea;
            return this;
        }

        /// <summary>
        /// Assigns the geographical region.
        /// </summary>
        /// <param name="geographicalRegion">The geographical region.</param>
        /// <returns>A LocationProfileBuilder.</returns>
        public LocationProfileBuilder WithGeographicalRegion(GeographicalRegion geographicalRegion)
        {
            _geographicalRegion = geographicalRegion;
            return this;
        }

        /// <summary>
        /// Assigns the hipaa service location.
        /// </summary>
        /// <param name="hipaaServiceLocation">The hipaa service location.</param>
        /// <returns>A LocationProfileBuilder.</returns>
        public LocationProfileBuilder WithHipaaServiceLocation(HipaaServiceLocation hipaaServiceLocation)
        {
            _hipaaServiceLocation = hipaaServiceLocation;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A LocationProfile.</returns>
        public LocationProfile Build()
        {
            return new LocationProfile(
                _locationName, _effectiveDateRange, _websiteUrlName, _countyArea, _geographicalRegion, _hipaaServiceLocation);
        }
    }
}