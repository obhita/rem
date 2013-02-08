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
    /// LocationContactBuilder provides a fluent interface for creating a LocationContact.
    /// </summary>
    public class LocationContactBuilder
    {
        private bool _alternativeContactIndicator;
        private Staff _contactStaff;
        private DateRange _effectiveDateRange;
        private LocationContactType _locationContactType;
        private bool _statusIndicator;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LocationContactBuilder"/> to <see cref="LocationContact"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LocationContact(LocationContactBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the type of the location contact.
        /// </summary>
        /// <param name="locationContactType">Type of the location contact.</param>
        /// <returns>A LocationContactBuilder.</returns>
        public LocationContactBuilder WithLocationContactType(LocationContactType locationContactType)
        {
            _locationContactType = locationContactType;
            return this;
        }

        /// <summary>
        /// Assigns the contact staff.
        /// </summary>
        /// <param name="contactStaff">The contact staff.</param>
        /// <returns>A LocationContactBuilder.</returns>
        public LocationContactBuilder WithContactStaff(Staff contactStaff)
        {
            _contactStaff = contactStaff;
            return this;
        }

        /// <summary>
        /// Assigns the effective date range.
        /// </summary>
        /// <param name="effectiveDateRange">The effective date range.</param>
        /// <returns>A LocationContactBuilder.</returns>
        public LocationContactBuilder WithEffectiveDateRange(DateRange effectiveDateRange)
        {
            _effectiveDateRange = effectiveDateRange;
            return this;
        }

        /// <summary>
        /// Assigns the status indicator.
        /// </summary>
        /// <param name="statusIndicator">If set to <c>true</c> [status indicator].</param>
        /// <returns>A LocationContactBuilder.</returns>
        public LocationContactBuilder WithStatusIndicator(bool statusIndicator)
        {
            _statusIndicator = statusIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the alternative contact indicator.
        /// </summary>
        /// <param name="alternativeContactIndicator">If set to <c>true</c> [alternative contact indicator].</param>
        /// <returns>A LocationContactBuilder.</returns>
        public LocationContactBuilder WithAlternativeContactIndicator(bool alternativeContactIndicator)
        {
            _alternativeContactIndicator = alternativeContactIndicator;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A LocationContact.</returns>
        public LocationContact Build()
        {
            return new LocationContact(_locationContactType, _contactStaff, _effectiveDateRange, _statusIndicator, _alternativeContactIndicator);
        }
    }
}