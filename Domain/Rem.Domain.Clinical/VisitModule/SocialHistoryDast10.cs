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
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;
using Rem.Domain.Clinical.SbirtModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// SocialHistoryDast10 defines elements for managing the scheduling of the <see cref="Dast10">Dast10</see> 
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class SocialHistoryDast10 : IEquatable<SocialHistoryDast10>
    {
        private SocialHistoryDast10 ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialHistoryDast10"/> class.
        /// </summary>
        /// <param name="dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber">The dast10 times past year used illegal drug or prescription medication for non medical reasons number.</param>
        public SocialHistoryDast10(int? dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber )
        {
            Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber = dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber;
        }

        /// <summary>
        /// Gets the dast10 times past year used illegal drug or prescription medication for non medical reasons number.
        /// </summary>
        public virtual int? Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is dast10 threshold satisfied.
        /// </summary>
        /// <value><c>true</c> if this instance is dast10 threshold satisfied; otherwise, <c>false</c>.</value>
        [IgnoreMapping]
        public virtual bool IsDast10ThresholdSatisfied
        {
            get
            {
                return
                    Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber.HasValue &&
                    Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber.Value > 0;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals ( SocialHistoryDast10 other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber.Equals ( Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode ()
        {
            return ( Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber.HasValue ? Dast10TimesPastYearUsedIllegalDrugOrPrescriptionMedicationForNonMedicalReasonsNumber.Value : 0 );
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator == ( SocialHistoryDast10 left, SocialHistoryDast10 right )
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
        public static bool operator != ( SocialHistoryDast10 left, SocialHistoryDast10 right )
        {
            return !Equals ( left, right );
        }
    }
}
