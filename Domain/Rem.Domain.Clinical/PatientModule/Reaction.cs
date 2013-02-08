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

using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Reaction lookup contains a list of coded concept reactions.
    /// </summary>
    public class Reaction : CodedConceptLookupBase
    {
        private const string CODE_SYSTEM_IDENTIFIER = "2.16.840.1.113883.6.96";
        private const string CODE_SYSTEM_NAME_CONST = "SNOMED CT";
        private const string CODE_SYSTEM_VERSION_NUMBER_CONST = "Unknown";

        /// <summary>
        /// Gets the code system identifier.
        /// </summary>
        [IgnoreMapping]
        public override string CodeSystemIdentifier
        {
            get { return CODE_SYSTEM_IDENTIFIER; }
        }

        /// <summary>
        /// Gets the name of the code system.
        /// </summary>
        /// <value>
        /// The name of the code system.
        /// </value>
        [IgnoreMapping]
        public override string CodeSystemName
        {
            get { return CODE_SYSTEM_NAME_CONST; }
        }

        /// <summary>
        /// Gets the code system version number.
        /// </summary>
        [IgnoreMapping]
        public override string CodeSystemVersionNumber
        {
            get { return CODE_SYSTEM_VERSION_NUMBER_CONST; }
        }
    }
}
