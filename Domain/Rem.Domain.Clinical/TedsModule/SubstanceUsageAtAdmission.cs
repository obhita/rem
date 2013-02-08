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
    /// The value object about how a substance usage.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class SubstanceUsageAtAdmission : IEquatable<SubstanceUsageAtAdmission>
    {
        private SubstanceUsageAtAdmission ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubstanceUsageAtAdmission"/> class.
        /// </summary>
        /// <param name="substanceProblemAndFrequency">The substance problem and frequency.</param>
        /// <param name="usualAdministrationRouteType">Type of the usual route of administration.</param>
        /// <param name="firstUseAge">The first use age.</param>
        /// <param name="detailedDrugCode">The detailed drug code.</param>
        public SubstanceUsageAtAdmission(SubstanceProblemAndFrequency substanceProblemAndFrequency,
            TedsAnswer<UsualAdministrationRouteType> usualAdministrationRouteType, 
            TedsAnswer<int?> firstUseAge,
            TedsAnswer<DetailedDrugCode> detailedDrugCode)
        {
            Check.IsNotNull(substanceProblemAndFrequency, () => SubstanceProblemAndFrequency);
            SubstanceProblemAndFrequency = substanceProblemAndFrequency;

            CheckIfTedsAnswerHasInvalidNonResponse(usualAdministrationRouteType, () => this.UsualAdministrationRouteType, "usual route of administration");
            UsualAdministrationRouteType = usualAdministrationRouteType;

            CheckIfTedsAnswerHasInvalidNonResponse(firstUseAge, () => this.FirstUseAge, "age of first use");
            FirstUseAge = firstUseAge;

            CheckIfTedsAnswerHasInvalidNonResponse(detailedDrugCode, () => this.DetailedDrugCode, "detailed drug code");
            DetailedDrugCode = detailedDrugCode;

            // Note: two-digit substance problem code forms the first two digits of its associated detailed drug code
            if (substanceProblemAndFrequency.SubstanceProblemType.HasResponse && detailedDrugCode != null && detailedDrugCode.HasResponse)
            {
                if (detailedDrugCode.Response.SubstanceProblemType.Key != substanceProblemAndFrequency.SubstanceProblemType.Response.Key)
                {
                    throw new ArgumentException("The detailed drug code is not associated with the substance problem code.");
                }
            }
        }

        /// <summary>
        /// Gets the substance problem and frequency.
        /// </summary>
        [NotNull]
        public virtual SubstanceProblemAndFrequency SubstanceProblemAndFrequency { get; private set; }

        /// <summary>
        /// Gets the type of the usual route of administration.
        /// MDS 15
        /// </summary>
        /// <value>
        /// The type of the usual route of administration.
        /// </value>
        public virtual TedsAnswer<UsualAdministrationRouteType> UsualAdministrationRouteType { get; private set; }

        /// <summary>
        /// Gets the first use age.
        /// MDS 17
        /// </summary>
        public virtual TedsAnswer<int?> FirstUseAge { get; private set; }

        /// <summary>
        /// Gets the detailed drug code.
        /// SuDS 1 or 2 or 3
        /// </summary>
        public virtual TedsAnswer<DetailedDrugCode> DetailedDrugCode { get; private set; }

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

            if (propertyName == PropertyUtil.ExtractPropertyName(() => this.UsualAdministrationRouteType))
            {
                wellKnownNames = wellKnownNamesForMostProperties;
            }
            else if (propertyName == PropertyUtil.ExtractPropertyName(() => this.FirstUseAge))
            {
                wellKnownNames = wellKnownNamesForMostProperties;
            }
            else if (propertyName == PropertyUtil.ExtractPropertyName(() => this.DetailedDrugCode))
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
        public bool Equals ( SubstanceUsageAtAdmission other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.DetailedDrugCode, DetailedDrugCode ) && Equals ( other.FirstUseAge, FirstUseAge ) && Equals ( other.UsualAdministrationRouteType, UsualAdministrationRouteType ) && Equals ( other.SubstanceProblemAndFrequency, SubstanceProblemAndFrequency );
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
            if ( obj.GetType () != typeof( SubstanceUsageAtAdmission ) )
            {
                return false;
            }
            return Equals ( ( SubstanceUsageAtAdmission )obj );
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
                int result = ( DetailedDrugCode != null ? DetailedDrugCode.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( FirstUseAge != null ? FirstUseAge.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( UsualAdministrationRouteType != null ? UsualAdministrationRouteType.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( SubstanceProblemAndFrequency != null ? SubstanceProblemAndFrequency.GetHashCode () : 0 );
                return result;
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
        public static bool operator == ( SubstanceUsageAtAdmission left, SubstanceUsageAtAdmission right )
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
        public static bool operator != ( SubstanceUsageAtAdmission left, SubstanceUsageAtAdmission right )
        {
            return !Equals ( left, right );
        }


        /// <summary>
        /// Gets the usual route of administration type code.
        /// </summary>
        /// <param name="substanceUsageAtAdmission">The substance usage at admission.</param>
        /// <returns>An usual route of administration type code.</returns>
        public static string GetUsualRouteOfAdministrationTypeCode(SubstanceUsageAtAdmission substanceUsageAtAdmission)
        {
            var code = "98";
            if (substanceUsageAtAdmission != null)
            {
                if (substanceUsageAtAdmission.UsualAdministrationRouteType.HasResponse)
                {
                    code = substanceUsageAtAdmission.UsualAdministrationRouteType.Response.ShortName;
                }
                else if (substanceUsageAtAdmission.UsualAdministrationRouteType.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    code = "97";
                }
                else if (substanceUsageAtAdmission.UsualAdministrationRouteType.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
                {
                    code = "96";
                }
            }
            return code;
        }

        /// <summary>
        /// Gets the substance use frequency type code.
        /// </summary>
        /// <param name="substanceUsageAtAdmission">The substance problem and frequency.</param>
        /// <returns>A string of use frequency code.</returns>
        public static string GetFirstUseAgeCode(SubstanceUsageAtAdmission substanceUsageAtAdmission)
        {
            var code = "98";
            if (substanceUsageAtAdmission != null)
            {
                if (substanceUsageAtAdmission.FirstUseAge.HasResponse)
                {
                    if (substanceUsageAtAdmission.FirstUseAge.Response.Value <= 95)
                    {
                        code = string.Format("{0,2:00}", substanceUsageAtAdmission.FirstUseAge.Response.Value);
                    }
                    else
                    {
                        code = "95";
                    }
                }
                else if (substanceUsageAtAdmission.FirstUseAge.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    code = "97";
                }
                else if (substanceUsageAtAdmission.FirstUseAge.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
                {
                    code = "96";
                }
            }
            return code;
        }

        /// <summary>
        /// Gets the detailed drug code.
        /// </summary>
        /// <param name="substanceUsageAtAdmission">The substance usage at admission.</param>
        /// <returns>A string.</returns>
        public static string GetDetailedDrugCode(SubstanceUsageAtAdmission substanceUsageAtAdmission)
        {
            var code = "9998";
            if (substanceUsageAtAdmission != null)
            {
                if (substanceUsageAtAdmission.DetailedDrugCode.HasResponse)
                {
                    code = substanceUsageAtAdmission.DetailedDrugCode.Response.ShortName;
                }
                else if (substanceUsageAtAdmission.DetailedDrugCode.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.Unknown)
                {
                    code = "9997";
                }
                else if (substanceUsageAtAdmission.DetailedDrugCode.TedsNonResponse.Name == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)
                {
                    code = "9996";
                }
            }
            return code;
        }

        /// <summary>
        /// Gets the substance problem type code.
        /// </summary>
        /// <param name="substanceUsageAtAdmission">The substance usage at admission.</param>
        /// <returns>
        /// A string of problem code.
        /// </returns>
        public static string GetSubstanceProblemTypeCode(SubstanceUsageAtAdmission substanceUsageAtAdmission)
        {
            var problemCode = "98";
            if (substanceUsageAtAdmission != null)
            {
                problemCode = SubstanceProblemAndFrequency.GetSubstanceProblemTypeCode (substanceUsageAtAdmission.SubstanceProblemAndFrequency);
            }
            return problemCode;
        }

        /// <summary>
        /// Gets the substance use frequency type code.
        /// </summary>
        /// <param name="substanceUsageAtAdmission">The substance usage at admission.</param>
        /// <returns>
        /// A string of use frequency code.
        /// </returns>
        public static string GetSubstanceUseFrequencyTypeCode(SubstanceUsageAtAdmission substanceUsageAtAdmission)
        {
            var useFrequencyCode = "98";
            if (substanceUsageAtAdmission != null)
            {
                useFrequencyCode = SubstanceProblemAndFrequency.GetSubstanceUseFrequencyTypeCode(substanceUsageAtAdmission.SubstanceProblemAndFrequency);
            }
            return useFrequencyCode;
        }
    }
}
