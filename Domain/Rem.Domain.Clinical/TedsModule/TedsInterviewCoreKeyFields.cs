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

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// This class defines the common (core) key fields for TEDS admission and discharge interview.
    /// </summary>
    public abstract class TedsInterviewCoreKeyFields : IEquatable<TedsInterviewCoreKeyFields>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TedsInterviewCoreKeyFields"/> class.
        /// </summary>
        protected TedsInterviewCoreKeyFields ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsInterviewCoreKeyFields"/> class.
        /// </summary>
        /// <param name="systemDataSet">The system data set.</param>
        /// <param name="providerIdentifier">The provider identifier.</param>
        /// <param name="clientIdentifier">The client identifier.</param>
        /// <param name="coDependentIndicator">If set to <c>true</c> [co dependent indicator].</param>
        /// <param name="tedsServiceType">Type of the teds service.</param>
        protected internal TedsInterviewCoreKeyFields (
            SystemDataSet systemDataSet,
            TedsIdentifier providerIdentifier,
            TedsIdentifier clientIdentifier,
            bool coDependentIndicator,
            TedsAnswer<TedsServiceType> tedsServiceType )
        {
            Check.IsNotNull ( systemDataSet, () => SystemDataSet );
            Check.IsNotNull ( providerIdentifier, () => ProviderIdentifier );
            Check.IsNotNull ( clientIdentifier, () => ClientIdentifier );
            Check.IsNotNull ( tedsServiceType, () => TedsServiceType );

            CheckIfTedsAnswerHasInvalidNonResponse ( tedsServiceType, () => this.TedsServiceType, "type of services" );

            if ( !coDependentIndicator && !tedsServiceType.HasResponse
                 && tedsServiceType.TedsNonResponse.WellKnownName == WellKnownNames.TedsModule.TedsNonResponse.NotApplicable )
            {
                throw new ArgumentException ( "Use not applicable for type of services only for co-dependents/collateral clients." );
            }

            SystemDataSet = systemDataSet;
            ProviderIdentifier = providerIdentifier;
            ClientIdentifier = clientIdentifier;
            CoDependentIndicator = coDependentIndicator;
            TedsServiceType = tedsServiceType;
        }

        /// <summary>
        /// Gets the system data set.
        /// SDS 1 - 3
        /// </summary>
        public virtual SystemDataSet SystemDataSet { get; protected set; }

        /// <summary>
        /// Gets the provider identifier.
        /// MDS 1 Or DIS 4
        /// </summary>
        public virtual TedsIdentifier ProviderIdentifier { get; protected set; }

        /// <summary>
        /// Gets the client identifier.
        /// MDS 2 or DIS 5
        /// </summary>
        public virtual TedsIdentifier ClientIdentifier { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether [co dependent indicator].
        /// MDS 3 Or DIS 6
        /// </summary>
        /// <value>
        /// <c>true</c> if [co dependent indicator]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool CoDependentIndicator { get; protected set; }

        /// <summary>
        /// Gets the type of the teds service.
        /// MDS 18 or DIS 7
        /// </summary>
        /// <value>
        /// The type of the teds service.
        /// </value>
        public virtual TedsAnswer<TedsServiceType> TedsServiceType { get; protected set; }

        private void CheckIfTedsAnswerHasInvalidNonResponse<T> (
            TedsAnswer<T> tedsAnswer, Expression<Func<TedsAnswer<T>>> propertyExpression, string tedsQuestion )
        {
            if ( tedsAnswer != null
                 && !tedsAnswer.HasResponse
                 && !GetNonResponseLookupWellKnownNames ( propertyExpression ).ToList ().Contains ( tedsAnswer.TedsNonResponse.WellKnownName )
                )
            {
                throw new ArgumentException (
                    string.Format ( "{0} has invalid non-reponse.", tedsQuestion.Substring ( 0, 1 ).ToUpper () + tedsQuestion.Substring ( 1 ) ) );
            }
        }

        private IEnumerable<string> GetNonResponseLookupWellKnownNames<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            var wellKnownNames = TedsAdmissionInterview.DefaultNonResponseLookupWellKnownNames;

            string propertyName = PropertyUtil.ExtractPropertyName(propertyExpression);

            if (propertyName == PropertyUtil.ExtractPropertyName(() => this.TedsServiceType))
            {
                wellKnownNames = new List<string> { WellKnownNames.TedsModule.TedsNonResponse.NotApplicable };
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
        public bool Equals ( TedsInterviewCoreKeyFields other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.SystemDataSet, SystemDataSet ) && Equals ( other.ProviderIdentifier, ProviderIdentifier )
                   && Equals ( other.ClientIdentifier, ClientIdentifier ) && other.CoDependentIndicator.Equals ( CoDependentIndicator )
                   && Equals ( other.TedsServiceType, TedsServiceType );
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
            if ( obj.GetType () != typeof( TedsInterviewCoreKeyFields ) )
            {
                return false;
            }
            return Equals ( ( TedsInterviewCoreKeyFields )obj );
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
                var result = ( SystemDataSet != null ? SystemDataSet.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( ProviderIdentifier != null ? ProviderIdentifier.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( ClientIdentifier != null ? ClientIdentifier.GetHashCode () : 0 );
                result = ( result * 397 ) ^ CoDependentIndicator.GetHashCode ();
                result = ( result * 397 ) ^ ( TedsServiceType != null ? TedsServiceType.GetHashCode () : 0 );
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
        public static bool operator == ( TedsInterviewCoreKeyFields left, TedsInterviewCoreKeyFields right )
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
        public static bool operator != ( TedsInterviewCoreKeyFields left, TedsInterviewCoreKeyFields right )
        {
            return !Equals ( left, right );
        }
    }
}
