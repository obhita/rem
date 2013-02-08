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
using System;
using System.Text.RegularExpressions;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines the identifier object that used in TEDS client and provider identifier object.
    /// </summary>
    [Component]
    public class TedsIdentifier : IEquatable<TedsIdentifier>
    {
        private static readonly string ValidationExpression = @"^\w$";

        private readonly string _identifierValue;

        private TedsIdentifier ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsIdentifier"/> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        public TedsIdentifier(string identifier)
        {
            Check.IsNotNullOrWhitespace(identifier, "Identifier must not be null or white space.");

            identifier = identifier.Trim();

            var regex = new Regex(ValidationExpression);
            if (!regex.IsMatch(identifier))
            {
                throw new ArgumentException("Identifier must only contain alphanumeric characters.");
            }

            if (identifier.Length > 15)
            {
                throw new ArgumentException("Identifier must not contain more than 15 alphanumeric characters.");
            }

            _identifierValue = identifier;
        }

        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        [NotNull]
        public virtual string IdentifierValue
        {
            get { return _identifierValue; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (this.Equals(obj as TedsIdentifier));
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals ( TedsIdentifier other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other._identifierValue, _identifierValue );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode ()
        {
            return ( _identifierValue != null ? _identifierValue.GetHashCode () : 0 );
        }
    }
}
