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

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// This class defines employment status and detailed not in labor force information.
    /// </summary>
    [Component]
    public class TedsEmploymentStatusInformation : IEquatable<TedsEmploymentStatusInformation>
    {
        private TedsEmploymentStatusInformation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsEmploymentStatusInformation"/> class.
        /// </summary>
        /// <param name="tedsEmploymentStatus">The teds employment status.</param>
        /// <param name="detailedNotInLaborForce">The detailed not in labor force.</param>
        public TedsEmploymentStatusInformation(TedsAnswer<TedsEmploymentStatus> tedsEmploymentStatus, TedsAnswer<DetailedNotInLaborForce> detailedNotInLaborForce)
        {
            Check.IsNotNull(tedsEmploymentStatus, () => TedsEmploymentStatus);

            CheckIfTedsAnswerHasInvalidNonResponse(tedsEmploymentStatus, () => this.TedsEmploymentStatus, "employment status");
            TedsEmploymentStatus = tedsEmploymentStatus;

            CheckIfTedsAnswerHasInvalidNonResponse(detailedNotInLaborForce, () => this.DetailedNotInLaborForce, "detailed not in labor force");
            DetailedNotInLaborForce = detailedNotInLaborForce;

            // MDS 13 - EMPLOYMENT STATUS
            // SuDs12 - DETAILED NOT IN LABOR FORCE
            // SuDs12 - Not Applicable (96) Use this code if Employment Status (MDS 13) is coded 01, 02, 03, or 97.
            if (!(tedsEmploymentStatus.HasResponse && tedsEmploymentStatus.Response.WellKnownName == WellKnownNames.TedsModule.TedsEmploymentStatus.NotInLaborForce))
            {
                if (detailedNotInLaborForce != null && (detailedNotInLaborForce.HasResponse || (!detailedNotInLaborForce.HasResponse && detailedNotInLaborForce.TedsNonResponse.WellKnownName != WellKnownNames.TedsModule.TedsNonResponse.NotApplicable)))
                {
                    throw new ArgumentException("Detailed not in labor force information should be coded as not applicable if employment status information is coded as full time, part time, unemployed, or unknown.");
                }
            }
        }

        /// <summary>
        /// Gets the teds employment status answer.
        /// MDS 13
        /// </summary>
        [NotNull]
        public virtual TedsAnswer<TedsEmploymentStatus> TedsEmploymentStatus { get; private set; }

        /// <summary>
        /// Gets the detailed not in labor force answer.
        /// SUDS 12
        /// </summary>
        public virtual TedsAnswer<DetailedNotInLaborForce> DetailedNotInLaborForce { get; private set; }

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

            string propertyName =  PropertyUtil.ExtractPropertyName ( propertyExpression );

            if (propertyName == PropertyUtil.ExtractPropertyName(() => this.DetailedNotInLaborForce))
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
        public bool Equals ( TedsEmploymentStatusInformation other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.TedsEmploymentStatus, TedsEmploymentStatus ) && Equals ( other.DetailedNotInLaborForce, DetailedNotInLaborForce );
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
            if ( obj.GetType () != typeof( TedsEmploymentStatusInformation ) )
            {
                return false;
            }
            return Equals ( ( TedsEmploymentStatusInformation )obj );
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
                return ( ( TedsEmploymentStatus != null ? TedsEmploymentStatus.GetHashCode () : 0 ) * 397 ) ^ ( DetailedNotInLaborForce != null ? DetailedNotInLaborForce.GetHashCode () : 0 );
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
        public static bool operator == ( TedsEmploymentStatusInformation left, TedsEmploymentStatusInformation right )
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
        public static bool operator != ( TedsEmploymentStatusInformation left, TedsEmploymentStatusInformation right )
        {
            return !Equals ( left, right );
        }
    }
}
