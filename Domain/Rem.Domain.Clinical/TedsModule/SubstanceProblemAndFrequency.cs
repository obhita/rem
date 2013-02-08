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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines the substance problem as well as the frequency of use.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class SubstanceProblemAndFrequency : IEquatable<SubstanceProblemAndFrequency>
    {
        private SubstanceProblemAndFrequency ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubstanceProblemAndFrequency"/> class.
        /// </summary>
        /// <param name="substanceProblemType">Type of the substance problem.</param>
        /// <param name="frequencyOfUseType">Type of the frequency of use.</param>
        public SubstanceProblemAndFrequency (TedsAnswer<SubstanceProblemType> substanceProblemType, TedsAnswer<UseFrequencyType> frequencyOfUseType)
        {
            Check.IsNotNull ( substanceProblemType, () => SubstanceProblemType );

            CheckIfTedsAnswerHasInvalidNonResponse(substanceProblemType, () => this.SubstanceProblemType, "substance problem code");
            SubstanceProblemType = substanceProblemType;

            CheckIfTedsAnswerHasInvalidNonResponse(frequencyOfUseType, () => this.UseFrequencyType, "frequency of use");
            UseFrequencyType = frequencyOfUseType;
        }

        /// <summary>
        /// Gets the type of the substance problem.
        /// MDS 14 (A)/(B)/(C) or DIS 21 (A)/(B)/(C)
        /// </summary>
        /// <value>
        /// The type of the substance problem.
        /// </value>
        [NotNull]
        public virtual TedsAnswer<SubstanceProblemType> SubstanceProblemType { get; private set; }

        /// <summary>
        /// Gets the type of the frequency of use.
        /// MDS 16 (A)/(B)/(C) or DIS 22 (A)/(B)/(C)
        /// </summary>
        /// <value>
        /// The type of the frequency of use.
        /// </value>
        public virtual TedsAnswer<UseFrequencyType> UseFrequencyType { get; private set; }

        private void CheckIfTedsAnswerHasInvalidNonResponse<T>(TedsAnswer<T> tedsAnswer, Expression<Func<TedsAnswer<T>>> propertyExpression, string tedsQuestion)
        {
            if (tedsAnswer != null
                && !tedsAnswer.HasResponse
                && !GetNonResponseLookupWellKnownNames(propertyExpression).ToList().Contains(tedsAnswer.TedsNonResponse.WellKnownName)
                )
            {
                throw new ArgumentException(
                    string.Format("{0} has invalid non-reponse.", tedsQuestion.Substring(0, 1).ToUpper() + tedsQuestion.Substring(1)));
            }
        }

        private IEnumerable<string> GetNonResponseLookupWellKnownNames<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            IEnumerable<string> wellKnownNames = TedsAdmissionInterview.DefaultNonResponseLookupWellKnownNames;

            var wellKnownNamesForMostProperties = new List<string> { WellKnownNames.TedsModule.TedsNonResponse.NotApplicable, WellKnownNames.TedsModule.TedsNonResponse.Unknown };

            string propertyName = PropertyUtil.ExtractPropertyName(propertyExpression);

            if (propertyName == PropertyUtil.ExtractPropertyName(() => this.UseFrequencyType))
            {
                wellKnownNames = wellKnownNamesForMostProperties;
            }

            return wellKnownNames;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( SubstanceProblemAndFrequency other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.SubstanceProblemType, SubstanceProblemType ) && Equals ( other.UseFrequencyType, UseFrequencyType );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType () != typeof( SubstanceProblemAndFrequency ) )
            {
                return false;
            }
            return Equals ( ( SubstanceProblemAndFrequency )obj );
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode ()
        {
            unchecked
            {
                return ( ( SubstanceProblemType != null ? SubstanceProblemType.GetHashCode () : 0 ) * 397 ) ^ ( UseFrequencyType != null ? UseFrequencyType.GetHashCode () : 0 );
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == ( SubstanceProblemAndFrequency left, SubstanceProblemAndFrequency right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator != ( SubstanceProblemAndFrequency left, SubstanceProblemAndFrequency right )
        {
            return !Equals ( left, right );
        }

        /// <summary>
        /// Gets the substance problem type code.
        /// </summary>
        /// <param name="substanceProblemAndFrequency">The substance problem and frequency.</param>
        /// <returns>A string of problem code.</returns>
        public static string GetSubstanceProblemTypeCode (SubstanceProblemAndFrequency substanceProblemAndFrequency)
        {
            var problemCode = "98";
            if (substanceProblemAndFrequency != null)
            {
                if ( substanceProblemAndFrequency.SubstanceProblemType.HasResponse )
                {
                    problemCode = substanceProblemAndFrequency.SubstanceProblemType.Response.ShortName;
                }
                else if ( substanceProblemAndFrequency.SubstanceProblemType.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown )
                {
                    problemCode = "97";
                }
            }
            return problemCode;
        }

        /// <summary>
        /// Gets the substance use frequency type code.
        /// </summary>
        /// <param name="substanceProblemAndFrequency">The substance problem and frequency.</param>
        /// <returns>A string of use frequency code.</returns>
        public static string GetSubstanceUseFrequencyTypeCode(SubstanceProblemAndFrequency substanceProblemAndFrequency)
        {
            var useFrequencyCode = "98";
            if (substanceProblemAndFrequency != null)
            {
                if (substanceProblemAndFrequency.UseFrequencyType.HasResponse)
                {
                    useFrequencyCode = substanceProblemAndFrequency.UseFrequencyType.Response.ShortName;
                }
                else if (substanceProblemAndFrequency.UseFrequencyType.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    useFrequencyCode = "97";
                }
                else if (substanceProblemAndFrequency.UseFrequencyType.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
                {
                    useFrequencyCode = "96";
                }
            }
            return useFrequencyCode;
        }
    }
}
