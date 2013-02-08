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
    /// AgencyProfile provides basic agency information. 
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class AgencyProfile : IEquatable<AgencyProfile>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="AgencyProfile"/> class from being created.
        /// </summary>
        private AgencyProfile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyProfile"/> class.
        /// </summary>
        /// <param name="agencyType">Type of the agency.</param>
        /// <param name="agencyName">Name of the agency.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <param name="websiteUrlName">Name of the website URL.</param>
        /// <param name="geographicalRegion">The geographical region.</param>
        /// <param name="note">The note.</param>
        public AgencyProfile(
            AgencyType agencyType, 
            AgencyName agencyName, 
            DateRange effectiveDateRange, 
            string websiteUrlName, 
            GeographicalRegion geographicalRegion, 
            string note)
        {
            Check.IsNotNull(agencyType, () => AgencyType);
            Check.IsNotNull(agencyName, () => AgencyName);

            AgencyType = agencyType;
            AgencyName = agencyName;
            EffectiveDateRange = effectiveDateRange;
            WebsiteUrlName = websiteUrlName;
            GeographicalRegion = geographicalRegion;
            Note = note;
        }

        /// <summary>
        /// Gets AgencyType.
        /// </summary>
        [NotNull]
        public virtual AgencyType AgencyType { get; private set; }

        /// <summary>
        /// Gets AgencyName.
        /// </summary>
        [NotNull]
        public virtual AgencyName AgencyName { get; private set; }

        /// <summary>
        /// Gets EffectiveDateRange.
        /// </summary>
        public virtual DateRange EffectiveDateRange { get; private set; }

        /// <summary>
        /// Gets WebsiteUrlName.
        /// </summary>
        public virtual string WebsiteUrlName { get; private set; }

        /// <summary>
        /// Gets GeographicalRegion.
        /// </summary>
        public virtual GeographicalRegion GeographicalRegion { get; private set; }

        /// <summary>
        /// Gets agency note.
        /// </summary>
        public virtual string Note { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(AgencyProfile left, AgencyProfile right)
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
        public static bool operator !=(AgencyProfile left, AgencyProfile right)
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
        public bool Equals(AgencyProfile other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.AgencyType, AgencyType) && Equals(other.AgencyName, AgencyName) && Equals(other.EffectiveDateRange, EffectiveDateRange) && Equals(other.WebsiteUrlName, WebsiteUrlName) && Equals(other.GeographicalRegion, GeographicalRegion) && Equals(other.Note, Note);
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

            if (obj.GetType() != typeof(AgencyProfile))
            {
                return false;
            }

            return Equals((AgencyProfile)obj);
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
                int result = AgencyType != null ? AgencyType.GetHashCode() : 0;
                result = (result * 397) ^ (AgencyName != null ? AgencyName.GetHashCode() : 0);
                result = (result * 397) ^ (EffectiveDateRange != null ? EffectiveDateRange.GetHashCode() : 0);
                result = (result * 397) ^ (WebsiteUrlName != null ? WebsiteUrlName.GetHashCode() : 0);
                result = (result * 397) ^ (GeographicalRegion != null ? GeographicalRegion.GetHashCode() : 0);
                result = (result * 397) ^ (Note != null ? Note.GetHashCode() : 0);
                return result;
            }
        }
    }
}