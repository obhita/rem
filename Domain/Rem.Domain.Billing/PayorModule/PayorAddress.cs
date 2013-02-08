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
using Pillar.Common;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// Defines the payor address.
    /// </summary>
    public class PayorAddress : AuditableAggregateNodeBase, IAggregateNodeValueObject, IValuesEquatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayorAddress"/> class.
        /// </summary>
        protected PayorAddress ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorAddress"/> class.
        /// </summary>
        /// <param name="payorAddressType">Type of the payor address.</param>
        /// <param name="address">The address.</param>
        public PayorAddress (PayorAddressType payorAddressType, Address address)
        {
            Check.IsNotNull ( payorAddressType, ()=>PayorAddressType );
            Check.IsNotNull ( address, () => Address );

            PayorAddressType = payorAddressType;
            Address = address;
        }

        /// <summary>
        /// Gets or sets the payor.
        /// </summary>
        /// <value>
        /// The payor.
        /// </value>
        [NotNull]
        public virtual Payor Payor { get; protected internal set; }

        /// <summary>
        /// Gets the type of the payor address.
        /// </summary>
        /// <value>
        /// The type of the payor address.
        /// </value>
        public virtual PayorAddressType PayorAddressType { get; private set; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public virtual Address Address { get; private set; }

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return Payor; }
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

            return ValuesEqual(obj as PayorAddress);
        }
    }
}
