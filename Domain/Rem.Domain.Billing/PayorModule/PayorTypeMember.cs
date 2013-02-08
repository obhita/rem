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
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// Defines the payor member.
    /// </summary>
    public class PayorTypeMember : AuditableAggregateNodeBase, IAggregateNodeValueObject, IComparable<PayorTypeMember>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayorTypeMember"/> class.
        /// </summary>
        protected internal PayorTypeMember ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorTypeMember"/> class.
        /// </summary>
        /// <param name="payorType">Type of the payor.</param>
        /// <param name="payor">The payor.</param>
        public PayorTypeMember (PayorType payorType, Payor payor)
        {
            Check.IsNotNull ( payorType, () => PayorType );
            Check.IsNotNull ( payor, () => Payor );

            PayorType = payorType;
            Payor = payor;
        }

        /// <summary>
        /// Gets the payor.
        /// </summary>
        [NotNull]
        public virtual Payor Payor { get; protected internal set; }

        /// <summary>
        /// Gets the type of the payor.
        /// </summary>
        /// <value>
        /// The type of the payor.
        /// </value>
        [NotNull]
        public virtual PayorType PayorType { get; private set; }

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return Payor; }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public virtual int CompareTo(PayorTypeMember other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
