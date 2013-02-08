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

using Pillar.Common;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Billing.PayorModule;

namespace Rem.Domain.Billing.PatientAccountModule
{
    /// <summary>
    /// The payor coverage defines the insurance payors that cover a patients health care.
    /// </summary>
    public class PayorCoverage : PatientAccountAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverage"/> class.
        /// </summary>
        protected internal PayorCoverage ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverage"/> class.
        /// </summary>
        /// <param name="payor">The payor.</param>
        /// <param name="payorSubscriber">The payor subscriber.</param>
        /// <param name="memberNumber">The member number.</param>
        /// <param name="effectiveCoverageDateRange">The effective coverage date range.</param>
        /// <param name="payorCoverageType">Type of the payor coverage.</param>
        public PayorCoverage (Payor payor, PayorSubscriber payorSubscriber, string memberNumber, DateRange effectiveCoverageDateRange,  PayorCoverageType payorCoverageType )
        {
            Check.IsNotNull ( payor, ()=> Payor );
            Check.IsNotNull ( payorSubscriber, () => PayorSubscriber );
            Check.IsNotNull ( memberNumber, () => MemberNumber );
            Check.IsNotNull ( effectiveCoverageDateRange, () =>  EffectiveCoverageDateRange );
            Check.IsNotNull ( payorCoverageType, () => PayorCoverageType );

            Payor = payor;
            MemberNumber = memberNumber;
            EffectiveCoverageDateRange = effectiveCoverageDateRange;
            PayorSubscriber = payorSubscriber;
            PayorCoverageType = payorCoverageType;
        }

        #region Public Properties

        /// <summary>
        /// Gets the effective coverage date range.
        /// </summary>
        [NotNull]
        public virtual DateRange EffectiveCoverageDateRange { get; private set; }

        /// <summary>
        /// Gets the member number.
        /// </summary>
        [NotNull]
        public virtual string MemberNumber { get; private set; }

        /// <summary>
        /// Gets the payor.
        /// </summary>
        [NotNull]
        public virtual Payor Payor { get; private set; }

        /// <summary>
        /// Gets the type of the payor coverage.
        /// </summary>
        /// <value>
        /// The type of the payor coverage.
        /// </value>
        [NotNull]
        public virtual PayorCoverageType PayorCoverageType { get; private set; }

        /// <summary>
        /// Gets the payor subscriber.
        /// </summary>
        public virtual PayorSubscriber PayorSubscriber { get; private set; }
        
        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return Payor; }
        }
        #endregion

        /// <summary>
        /// Equals the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public virtual bool Equals(PayorCoverage other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return base.Equals(other) && Equals(other.EffectiveCoverageDateRange, EffectiveCoverageDateRange) && Equals(other.MemberNumber, MemberNumber) && Equals(other.Payor, Payor) && Equals(other.PayorSubscriber, PayorSubscriber);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if ( ReferenceEquals ( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, obj ) )
            {
                return true;
            }
            return Equals ( obj as PayorCoverage );
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var result = base.GetHashCode();
                result = (result * 397) ^ (EffectiveCoverageDateRange != null ? EffectiveCoverageDateRange.GetHashCode() : 0);
                result = (result * 397) ^ (MemberNumber != null ? MemberNumber.GetHashCode() : 0);
                result = (result * 397) ^ (Payor != null ? Payor.GetHashCode() : 0);
                result = (result * 397) ^ (PayorSubscriber != null ? PayorSubscriber.GetHashCode() : 0);
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
        public static bool operator == ( PayorCoverage left, PayorCoverage right )
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
        public static bool operator != ( PayorCoverage left, PayorCoverage right )
        {
            return !Equals ( left, right );
        }

        /// <summary>
        /// Checks if all the values of the object are equal.
        /// </summary>
        /// <param name="obj">The object to check equality with.</param>
        /// <returns>A bool indicating whether objects are equal.</returns>
        public virtual bool ValuesEqual ( object obj )
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return ValuesEqual(obj as PayorCoverage);
        }

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="payorCoverage">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>            
        public virtual bool ValuesEqual(PayorCoverage payorCoverage)
        {
            if (payorCoverage == null)
            {
                return false;
            }

            var valuesEqual =
                Equals(Payor.Key, payorCoverage.Payor.Key) &&
                Equals(PayorSubscriber, payorCoverage.PayorSubscriber) &&
                Equals(MemberNumber, payorCoverage.MemberNumber) &&
                Equals(EffectiveCoverageDateRange, payorCoverage.EffectiveCoverageDateRange);

            return valuesEqual;
        }
    }
}
