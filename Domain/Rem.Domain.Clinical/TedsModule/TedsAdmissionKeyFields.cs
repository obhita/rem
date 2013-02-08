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
using System.Diagnostics.CodeAnalysis;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// This class defines the key fields for TEDS admission.
    /// Included in both the admission and the Admission data sets are several key fields. The key fields
    /// combine to form a unique identifier (retrieval key) for the record in the TEDS Admission
    /// database. Any Admission record submitted to TEDS that matches a record already in the TEDS
    /// database on all the Admission key fields is rejected as a duplicate.
    /// Admission Data Key fields are: 
    /// State Code
    /// Provider Identifier
    /// Client Identifier
    /// Co-dependent/Collateral code
    /// Client transaction type
    /// Type of Service at Admission
    /// Date of Admission 
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof( PropertyNameAsColumnNameNamingStrategy ) )]
    public class TedsAdmissionKeyFields : TedsInterviewCoreKeyFields, IEquatable<TedsAdmissionKeyFields>
    {
        private TedsAdmissionKeyFields ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAdmissionKeyFields"/> class.
        /// </summary>
        /// <param name="systemDataSet">The system data set.</param>
        /// <param name="providerIdentifier">The provider identifier.</param>
        /// <param name="clientIdentifier">The client identifier.</param>
        /// <param name="coDependentIndicator">If set to <c>true</c> [co dependent indicator].</param>
        /// <param name="tedsServiceType">Type of the teds service.</param>
        /// <param name="clientTransactionType">Type of the client transaction.</param>
        /// <param name="admissionDate">The admission date.</param>
        public TedsAdmissionKeyFields (
            SystemDataSet systemDataSet,
            TedsIdentifier providerIdentifier,
            TedsIdentifier clientIdentifier,
            bool coDependentIndicator,
            TedsAnswer<TedsServiceType> tedsServiceType,
            ClientTransactionType clientTransactionType,
            DateTime admissionDate )
            :
                base ( systemDataSet,
                       providerIdentifier,
                       clientIdentifier,
                       coDependentIndicator,
                       tedsServiceType )
        {
            Check.IsNotNull ( clientTransactionType, () => ClientTransactionType );

            ClientTransactionType = clientTransactionType;
            AdmissionDate = admissionDate;
        }

        /// <summary>
        /// Gets or sets the type of the client transaction.
        /// MDS 4
        /// </summary>
        /// <value>
        /// The type of the client transaction.
        /// </value>
        [SuppressMessage ( "StyleCop.CSharp.DocumentationRules", "SA1624:PropertySummaryDocumentationMustOmitSetAccessorWithRestrictedAccess",
            Justification = "Reviewed. Suppression is OK here." )]
        public virtual ClientTransactionType ClientTransactionType { get; private set; }

        /// <summary>
        /// Gets the admission date.
        /// MDS 5
        /// </summary>
        public virtual DateTime AdmissionDate { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( TedsAdmissionKeyFields other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return base.Equals ( other ) && other.AdmissionDate.Equals ( AdmissionDate )
                   && Equals ( other.ClientTransactionType, ClientTransactionType );
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
            return Equals ( obj as TedsAdmissionKeyFields );
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
                int result = base.GetHashCode ();
                result = ( result * 397 ) ^ AdmissionDate.GetHashCode ();
                result = ( result * 397 ) ^ ( ClientTransactionType != null ? ClientTransactionType.GetHashCode () : 0 );
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
        public static bool operator == ( TedsAdmissionKeyFields left, TedsAdmissionKeyFields right )
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
        public static bool operator != ( TedsAdmissionKeyFields left, TedsAdmissionKeyFields right )
        {
            return !Equals ( left, right );
        }
    }
}
