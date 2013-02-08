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

using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The LocationFactory implements lifetime management of the <see cref="T:Rem.Domain.Core.AgencyModule.Location">Location</see>.
    /// </summary>
    public class LocationFactory : ILocationFactory
    {
        private readonly ILocationRepository _locationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationFactory"/> class.
        /// </summary>
        /// <param name="locationRepository">The location repository.</param>
        public LocationFactory(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        #region ILocationFactory Members

        /// <summary>
        /// Creates the location.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="locationProfile">The location profile.</param>
        /// <returns>
        /// A Location.
        /// </returns>
        public Location CreateLocation(Agency agency, LocationProfile locationProfile)
        {
            Check.IsNotNull(agency, "agency is required.");
            Check.IsNotNull(locationProfile, "locationProfile is required.");

            var newLocation = new Location ( agency, locationProfile );
            Location createdLocation = null;

            DomainRuleEngine.CreateRuleEngine ( newLocation, "CreateLocationRuleSet" )
                .Execute(() =>
                {
                    _locationRepository.MakePersistent(newLocation);
                    createdLocation = newLocation;
                });

            return createdLocation;
        }

        /// <summary>
        /// Destroys the location.
        /// </summary>
        /// <param name="location">The location.</param>
        public void DestroyLocation(Location location)
        {
            Check.IsNotNull(location, "Location is required");
            _locationRepository.MakeTransient(location);
        }

        #endregion
    }
}