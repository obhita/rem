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
namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// LocationNameBuilder provides a fluent interface for creating a LocationName.
    /// </summary>
    public class LocationNameBuilder
    {
        private string _displayName;
        private string _name;

        /// <summary>
        /// Performs an implicit conversion from <see cref="LocationNameBuilder"/> to <see cref="LocationName"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator LocationName(LocationNameBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A LocationNameBuilder.</returns>
        public LocationNameBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Assigns the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>A LocationNameBuilder.</returns>
        public LocationNameBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A LocationName.</returns>
        public LocationName Build()
        {
            return new LocationName(_name, _displayName);
        }
    }
}