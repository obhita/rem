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
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// This class defines a Diagnostic and Statistical Manual of Mental Disorders (DSM) diagnosis response.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(TypeNamePlusPropertyNameAsColumnNameNamingStrategy))]
    public class DsmDiagnosisResponse
    {
        /// <summary>
        /// Validation Expression
        /// </summary>
        public static readonly string ValidationExpression = @"^\w{3}(\.\d{0,2}|\.?)$";

        private DsmDiagnosisResponse ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DsmDiagnosisResponse"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public DsmDiagnosisResponse (string code)
        {
            Check.IsNotNullOrWhitespace(code, "DSM diagnosis code is required");
            Validate(code);
            Code = code;
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        public string Code { get; private set; }

        private static void Validate(string code)
        {
            var regex = new Regex(ValidationExpression);
            if (!regex.IsMatch(code))
            {
                throw new ArgumentException(string.Format("{0} is not a valid DSM diagnosis code.", code));
            }
        }
    }
}
