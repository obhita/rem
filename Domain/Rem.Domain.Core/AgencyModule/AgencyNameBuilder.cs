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
namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// AgencyNameBuilder provides a fluent interface for creating a AgencyName.
    /// </summary>
    public class AgencyNameBuilder
    {
        private string _legalName;
        private string _displayName;
        private string _doingBusinessAsName;

        /// <summary>
        /// Performs an implicit conversion from <see cref="AgencyNameBuilder"/> to <see cref="AgencyName"/>.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator AgencyName(AgencyNameBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the name of the legal.
        /// </summary>
        /// <param name="legalName">
        /// Name of the legal.
        /// </param>
        /// <returns>
        /// An AgencyNameBuilder.
        /// </returns>
        public AgencyNameBuilder WithLegalName(string legalName)
        {
            _legalName = legalName;
            return this;
        }

        /// <summary>
        /// Assigns the display name.
        /// </summary>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <returns>
        /// An AgencyNameBuilder.
        /// </returns>
        public AgencyNameBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        /// <summary>
        /// Assigns the name of the doing business as.
        /// </summary>
        /// <param name="doingBusinessAsName">
        /// Name of the doing business as.
        /// </param>
        /// <returns>
        /// An AgencyNameBuilder.
        /// </returns>
        public AgencyNameBuilder WithDoingBusinessAsName(string doingBusinessAsName)
        {
            _doingBusinessAsName = doingBusinessAsName;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An AgencyName.
        /// </returns>
        public AgencyName Build()
        {
            return new AgencyName(_legalName, _displayName, _doingBusinessAsName);
        }
    }
}