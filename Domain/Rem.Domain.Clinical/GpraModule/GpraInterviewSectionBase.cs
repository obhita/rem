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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// GpraInterviewSectionBase provides a base class for Gpra Interview sections.
    /// </summary>
    public abstract class GpraInterviewSectionBase
    {
        /// <summary>
        /// Gets the possible Gpra non response well known names for this interview section.
        /// <remarks>NotAnswered is included in this base class because it is used in most Nonresponse lists.</remarks>
        /// </summary>
        /// <typeparam name="TProperty">The property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1">
        /// IEnumerable&lt;string&gt;</see> list of WellKNownNames.
        /// </returns>
        public virtual IEnumerable<string> GetPossibleGpraNonResponseWellKnownNames<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            return new List<string>
                {
                    WellKnownNames.GpraModule.GpraNonResponse.Refused,
                    WellKnownNames.GpraModule.GpraNonResponse.DontKnow
                };
        }

        /// <summary>
        /// Gets the well known names filters dictionary.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IDictionary`2">Dictionary&lt;string,
        /// IEnumerable&lt;string&gt;</see>
        /// </returns>
        public virtual Dictionary<string, IEnumerable<string>> GetFiltersDictionary()
        {
            return new Dictionary<string, IEnumerable<string>>();
        }
    }
}