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

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// LocationIdentifierBuilder provides a fluent interface for creating a LocationIdentifier.
    /// </summary>
    public class LocationIdentifierBuilder
    {
        private DateRange _effectiveDateRange;
        private string _identifierNumber;
        private LocationIdentifierType _locationIdentifierType;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LocationIdentifierBuilder"/> to <see cref="LocationIdentifier"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LocationIdentifier(LocationIdentifierBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the type of the location identifier.
        /// </summary>
        /// <param name="locationIdentifierType">Type of the location identifier.</param>
        /// <returns>A LocationIdentifierBuilder.</returns>
        public LocationIdentifierBuilder WithLocationIdentifierType(LocationIdentifierType locationIdentifierType)
        {
            _locationIdentifierType = locationIdentifierType;
            return this;
        }

        /// <summary>
        /// Assigns the identifier number.
        /// </summary>
        /// <param name="identifierNumber">The identifier number.</param>
        /// <returns>A LocationIdentifierBuilder.</returns>
        public LocationIdentifierBuilder WithIdentifierNumber(string identifierNumber)
        {
            _identifierNumber = identifierNumber;
            return this;
        }

        /// <summary>
        /// Assigns the effective date range.
        /// </summary>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <returns>A LocationIdentifierBuilder.</returns>
        public LocationIdentifierBuilder WithEffectiveDateRange(DateRange effectiveDateRange)
        {
            _effectiveDateRange = effectiveDateRange;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A LocationIdentifier.</returns>
        public LocationIdentifier Build()
        {
            return new LocationIdentifier(_locationIdentifierType, _identifierNumber, _effectiveDateRange);
        }
    }
}