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

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// PersonNameBuilder provides a fluent interface for creating person name.
    /// </summary>
    public class PersonNameBuilder
    {
        private string _first;
        private string _last;
        private string _middle;
        private string _prefix;
        private string _suffix;

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rem.Domain.Core.CommonModule.PersonNameBuilder"/> to <see cref="Rem.Domain.Core.CommonModule.PersonName"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator PersonName ( PersonNameBuilder builder )
        {
            return builder.Build ();
        }

        /// <summary>
        /// Assigns the prefix.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <returns>A PersonNameBuilder.</returns>
        public PersonNameBuilder WithPrefix ( string prefix )
        {
            _prefix = prefix;
            return this;
        }

        /// <summary>
        /// Assigns the first.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <returns>A PersonNameBuilder.</returns>
        public PersonNameBuilder WithFirst ( string first )
        {
            _first = first;
            return this;
        }

        /// <summary>
        /// Assigns the middle.
        /// </summary>
        /// <param name="middle">The middle.</param>
        /// <returns>A PersonNameBuilder.</returns>
        public PersonNameBuilder WithMiddle ( string middle )
        {
            _middle = middle;
            return this;
        }

        /// <summary>
        /// Assigns the last.
        /// </summary>
        /// <param name="last">The last.</param>
        /// <returns>A PersonNameBuilder.</returns>
        public PersonNameBuilder WithLast ( string last )
        {
            _last = last;
            return this;
        }

        /// <summary>
        /// Assigns the suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns>A PersonNameBuilder.</returns>
        public PersonNameBuilder WithSuffix ( string suffix )
        {
            _suffix = suffix;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A PersonName.</returns>
        public PersonName Build ()
        {
            return new PersonName ( _prefix, _first, _middle, _last, _suffix );
        }
    }
}
