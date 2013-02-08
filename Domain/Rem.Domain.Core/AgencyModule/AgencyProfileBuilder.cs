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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// AgencyProfileBuilder provides a fluent interface for creating a AgencyProfile.
    /// </summary>
    public class AgencyProfileBuilder
    {
        private AgencyType _agencyType;
        private AgencyName _agencyName;
        private DateRange _effectiveDateRange;
        private string _websiteUrlName;
        private GeographicalRegion _geographicalRegion;
        private string _note;
        
        /// <summary>
        /// Performs an implicit conversion from <see cref="AgencyProfileBuilder"/> to <see cref="AgencyProfile"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator AgencyProfile(AgencyProfileBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the type of the agency.
        /// </summary>
        /// <param name="agencyType">Type of the agency.</param>
        /// <returns>An AgencyProfileBuilder.</returns>
        public AgencyProfileBuilder WithAgencyType(AgencyType agencyType)
        {
            _agencyType = agencyType;
            return this;
        }

        /// <summary>
        /// Assigns the name of the agency.
        /// </summary>
        /// <param name="agencyName">Name of the agency.</param>
        /// <returns>An AgencyProfileBuilder.</returns>
        public AgencyProfileBuilder WithAgencyName(AgencyName agencyName)
        {
            _agencyName = agencyName;
            return this;
        }

        /// <summary>
        /// Assigns the effective date range.
        /// </summary>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <returns>An AgencyProfileBuilder.</returns>
        public AgencyProfileBuilder WithEffectiveDateRange(DateRange effectiveDateRange)
        {
            _effectiveDateRange = effectiveDateRange;
            return this;
        }

        /// <summary>
        /// Assigns the name of the website URL.
        /// </summary>
        /// <param name="websiteUrlName">Name of the website URL.</param>
        /// <returns>An AgencyProfileBuilder.</returns>
        public AgencyProfileBuilder WithWebsiteUrlName(string websiteUrlName)
        {
            _websiteUrlName = websiteUrlName;
            return this;
        }

        /// <summary>
        /// Assigns the geographical region.
        /// </summary>
        /// <param name="geographicalRegion">The geographical region.</param>
        /// <returns>An AgencyProfileBuilder.</returns>
        public AgencyProfileBuilder WithGeographicalRegion(GeographicalRegion geographicalRegion)
        {
            _geographicalRegion = geographicalRegion;
            return this;
        }

        /// <summary>
        /// Assigns the note.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>An AgencyProfileBuilder.</returns>
        public AgencyProfileBuilder WithNote(string note)
        {
            _note = note;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>An AgencyProfile.</returns>
        public AgencyProfile Build()
        {
            return new AgencyProfile(_agencyType, _agencyName, _effectiveDateRange, _websiteUrlName, _geographicalRegion, _note);
        }
    }
}