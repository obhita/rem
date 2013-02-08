#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
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

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// The Money defines a money object.
    /// </summary>
    [Component]
    public class Money : IEquatable<Money>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="Money"/> class from being created.
        /// </summary>
        private Money ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> class.
        /// </summary>
        /// <param name="currency">The currency.</param>
        /// <param name="amount">The amount.</param>
        public Money ( Currency currency, decimal amount )
        {
            Check.IsNotNull ( currency, () => Currency );

            Currency = currency;
            Amount = amount;
        }

        /// <summary>
        /// Gets the currency.
        /// </summary>
        [NotNull]
        public virtual Currency Currency { get; private set; }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        public virtual decimal Amount { get; private set; }


        #region Implementation of IEquatable<Money>

        bool IEquatable<Money>.Equals(Money other)
        {
            return Equals ( other );
        }

        #endregion

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( Money other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.Amount == Amount && Equals ( other.Currency, Currency );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.</returns>
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
            if ( obj.GetType () != typeof( Money ) )
            {
                return false;
            }
            return Equals ( ( Money )obj );
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
        public override int GetHashCode ()
        {
            unchecked
            {
                return ( Amount.GetHashCode () * 397 ) ^ ( Currency != null ? Currency.GetHashCode () : 0 );
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator == ( Money left, Money right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator != ( Money left, Money right )
        {
            return !Equals ( left, right );
        }


        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static Money operator +(Money left, Money right)
        {
            if (left.Currency.Key.Equals ( right.Currency.Key))
            {
                return new Money (left.Currency, left.Amount + right.Amount );
            }
            throw new NotImplementedException (
                string.Format (
                    "Currency conversion between {0} and {1} is not implemented.", left.Currency.WellKnownName, right.Currency.WellKnownName ) );
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static Money operator -(Money left, Money right)
        {
            if (left.Currency.Key.Equals(right.Currency.Key))
            {
                return new Money(left.Currency, left.Amount - right.Amount);
            }
            throw new NotImplementedException(
                string.Format(
                    "Currency conversion between {0} and {1} is not implemented.", left.Currency.WellKnownName, right.Currency.WellKnownName));
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rem.Domain.Core.CommonModule.Money"/> to <see cref="System.Decimal"/>.
        /// </summary>
        /// <param name="money">The money.</param>
        /// <returns>The decimal amount of the money.</returns>
        public static implicit operator decimal(Money money)
        {
            return money.Amount;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format ( "{0:c}", Amount );
        }
    }
}
