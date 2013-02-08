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
    /// CodedConceptBuilder provides a fluent interface for creating a coded concept value object.
    /// </summary>
    public class CodedConceptBuilder
    {
        private string _codedConceptCode;
        private string _codeSystemIdentifier;
        private string _codeSystemName;
        private string _codeSystemVersionNumber;
        private string _displayName;
        private bool _nullFlavorIndicator;
        private string _originalDescription;

        /// <summary>
        /// Performs an implicit conversion from <see cref="CodedConceptBuilder"/> to <see cref="CodedConcept"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator CodedConcept(CodedConceptBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the coded concept code.
        /// </summary>
        /// <param name="codedConceptCode">The coded concept code.</param>
        /// <returns>A CodedConceptBuilder.</returns>
        public CodedConceptBuilder WithCodedConceptCode(string codedConceptCode)
        {
            _codedConceptCode = codedConceptCode;
            return this;
        }

        /// <summary>
        /// Assigns the code system identifier.
        /// </summary>
        /// <param name="codeSystemIdentifier">The code system identifier.</param>
        /// <returns>A CodedConceptBuilder.</returns>
        public CodedConceptBuilder WithCodeSystemIdentifier(string codeSystemIdentifier)
        {
            _codeSystemIdentifier = codeSystemIdentifier;
            return this;
        }

        /// <summary>
        /// Assigns the name of the code system.
        /// </summary>
        /// <param name="codeSystemName">Name of the code system.</param>
        /// <returns>A CodedConceptBuilder.</returns>
        public CodedConceptBuilder WithCodeSystemName(string codeSystemName)
        {
            _codeSystemName = codeSystemName;
            return this;
        }

        /// <summary>
        /// Assigns the code system version number.
        /// </summary>
        /// <param name="codeSystemVersionNumber">The code system version number.</param>
        /// <returns>A CodedConceptBuilder.</returns>
        public CodedConceptBuilder WithCodeSystemVersionNumber(string codeSystemVersionNumber)
        {
            _codeSystemVersionNumber = codeSystemVersionNumber;
            return this;
        }

        /// <summary>
        /// Assigns the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>A CodedConceptBuilder.</returns>
        public CodedConceptBuilder WithDisplayName(string displayName)
        {
            _displayName = displayName;
            return this;
        }

        /// <summary>
        /// Assigns the null flavor indicator.
        /// </summary>
        /// <param name="nullFlavorIndicator">If set to <c>true</c> [null flavor indicator].</param>
        /// <returns>A CodedConceptBuilder.</returns>
        public CodedConceptBuilder WithNullFlavorIndicator(bool nullFlavorIndicator)
        {
            _nullFlavorIndicator = nullFlavorIndicator;
            return this;
        }

        /// <summary>
        /// Assigns the original description.
        /// </summary>
        /// <param name="originalDescription">The original description.</param>
        /// <returns>A CodedConceptBuilder.</returns>
        public CodedConceptBuilder WithOriginalDescription(string originalDescription)
        {
            _originalDescription = originalDescription;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// A CodedConcept.
        /// </returns>
        public CodedConcept Build()
        {
            return new CodedConcept (
                _codedConceptCode,
                _codeSystemIdentifier,
                _codeSystemName,
                _codeSystemVersionNumber,
                _displayName,
                _nullFlavorIndicator,
                _originalDescription );
        }
    }
}
