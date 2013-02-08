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

using Rem.Domain.Core.CommonModule;
using Rem.Ria.Infrastructure.Web.DataTransferObject;

namespace Rem.Ria.Infrastructure.Web.Extension
{
    /// <summary>
    /// CodedConceptBuilderExtensions class.
    /// </summary>
    public static class CodedConceptBuilderExtensions
    {
        #region Public Methods

        /// <summary>
        /// Withes the coded concept dto.
        /// </summary>
        /// <param name="codedConceptBuilder">The coded concept builder.</param>
        /// <param name="codedConceptDto">The coded concept dto.</param>
        /// <returns>A <see cref="Rem.Domain.Core.CommonModule.CodedConceptBuilder"/></returns>
        public static CodedConceptBuilder WithCodedConceptDto ( this CodedConceptBuilder codedConceptBuilder, CodedConceptDto codedConceptDto )
        {
            codedConceptBuilder.WithCodedConceptCode ( codedConceptDto.CodedConceptCode )
                .WithCodeSystemIdentifier ( codedConceptDto.CodeSystemIdentifier )
                .WithCodeSystemName ( codedConceptDto.CodeSystemName )
                .WithCodeSystemVersionNumber ( codedConceptDto.CodeSystemVersionNumber )
                .WithDisplayName ( codedConceptDto.DisplayName )
                .WithNullFlavorIndicator ( codedConceptDto.NullFlavorIndicator )
                .WithOriginalDescription ( codedConceptDto.OriginalDescription );

            return codedConceptBuilder;
        }

        #endregion
    }
}
