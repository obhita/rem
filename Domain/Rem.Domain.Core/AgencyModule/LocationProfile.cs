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

using System;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The LocationProfile contains the location profile information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class LocationProfile : IEquatable<LocationProfile>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="LocationProfile"/> class from being created.
        /// </summary>
        private LocationProfile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationProfile"/> class.
        /// </summary>
        /// <param name="locationName">Name of the location.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <param name="websiteUrlName">Name of the website URL.</param>
        /// <param name="countyArea">The county area.</param>
        /// <param name="geographicalRegion">The geographical region.</param>
        /// <param name="hipaaServiceLocation">The hipaa service location.</param>
        protected internal LocationProfile(
            LocationName locationName, 
            DateRange effectiveDateRange, 
            string websiteUrlName, 
            CountyArea countyArea, 
            GeographicalRegion geographicalRegion, 
            HipaaServiceLocation hipaaServiceLocation)
        {
            Check.IsNotNull(locationName, () => LocationName);

            LocationName = locationName;
            EffectiveDateRange = effectiveDateRange;
            WebsiteUrlName = websiteUrlName;
            CountyArea = countyArea;
            GeographicalRegion = geographicalRegion;
            HipaaServiceLocation = hipaaServiceLocation;
        }

        /// <summary>
        /// Gets LocationName.
        /// </summary>
        [NotNull]
        public virtual LocationName LocationName { get; private set; }

        /// <summary>
        /// Gets EffectiveDateRange.
        /// </summary>
        public virtual DateRange EffectiveDateRange { get; private set; }

        /// <summary>
        /// Gets WebsiteUrlName.
        /// </summary>
        public virtual string WebsiteUrlName { get; private set; }

        /// <summary>
        /// Gets CountyArea.
        /// </summary>
        public virtual CountyArea CountyArea { get; private set; }

        /// <summary>
        /// Gets GeographicalRegion.
        /// </summary>
        public virtual GeographicalRegion GeographicalRegion { get; private set; }

        /// <summary>
        /// Gets HipaaServiceLocation.
        /// </summary>
        public virtual HipaaServiceLocation HipaaServiceLocation { get; private set; }


        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(LocationProfile left, LocationProfile right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(LocationProfile left, LocationProfile right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// True if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals(LocationProfile other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.LocationName, LocationName) && Equals(other.EffectiveDateRange, EffectiveDateRange) && Equals(other.WebsiteUrlName, WebsiteUrlName) && Equals(other.CountyArea, CountyArea) && Equals(other.GeographicalRegion, GeographicalRegion) && Equals(other.HipaaServiceLocation, HipaaServiceLocation);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(LocationProfile))
            {
                return false;
            }

            return Equals((LocationProfile)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = LocationName != null ? LocationName.GetHashCode() : 0;
                result = (result * 397) ^ (EffectiveDateRange != null ? EffectiveDateRange.GetHashCode() : 0);
                result = (result * 397) ^ (WebsiteUrlName != null ? WebsiteUrlName.GetHashCode() : 0);
                result = (result * 397) ^ (CountyArea != null ? CountyArea.GetHashCode() : 0);
                result = (result * 397) ^ (GeographicalRegion != null ? GeographicalRegion.GetHashCode() : 0);
                result = (result * 397) ^ (HipaaServiceLocation != null ? HipaaServiceLocation.GetHashCode() : 0);
                return result;
            }
        }
    }
}