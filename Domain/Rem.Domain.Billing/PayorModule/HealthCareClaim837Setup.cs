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

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// Defines the professional 837.
    /// </summary>
    [Component]
    public class HealthCareClaim837Setup : IEquatable<HealthCareClaim837Setup>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="HealthCareClaim837Setup"/> class from being created.
        /// </summary>
        private HealthCareClaim837Setup ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCareClaim837Setup"/> class.
        /// </summary>
        /// <param name="interchangeReceiverNumber">The interchange receiver number.</param>
        /// <param name="interchangeSenderNumber">The interchange sender number.</param>
        /// <param name="x12Delimiters">The X12 delimiters.</param>
        public HealthCareClaim837Setup(string interchangeReceiverNumber, string interchangeSenderNumber, X12Delimiters x12Delimiters)
        {
            Check.IsNotNull ( interchangeReceiverNumber, "Interchange Receiver Number is required." );
            Check.IsNotNull ( interchangeSenderNumber, "Interchange Sender Number is required" );

            //Check.IsNotNull(x12Delimiters, "X12 delimiters are required.");

            InterchangeReceiverNumber = interchangeReceiverNumber;
            InterchangeSenderNumber = interchangeSenderNumber;
            X12Delimiters = x12Delimiters;
        }

        /// <summary>
        /// Gets the interchange receiver number.
        /// </summary>
        [NotNull]
        public virtual string InterchangeReceiverNumber { get; private set; }

        /// <summary>
        /// Gets the interchange sender number.
        /// </summary>
        [NotNull]
        public virtual string InterchangeSenderNumber { get; private set; }

        /// <summary>
        /// Gets the X12 delimiters.
        /// </summary>
        [NotNull]
        public virtual X12Delimiters X12Delimiters { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( HealthCareClaim837Setup other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.InterchangeReceiverNumber, InterchangeReceiverNumber ) && Equals ( other.InterchangeSenderNumber, InterchangeSenderNumber ) && Equals ( other.X12Delimiters, X12Delimiters );
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
            if ( obj.GetType () != typeof( HealthCareClaim837Setup ) )
            {
                return false;
            }
            return Equals ( ( HealthCareClaim837Setup )obj );
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
                int result = ( InterchangeReceiverNumber != null ? InterchangeReceiverNumber.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( InterchangeSenderNumber != null ? InterchangeSenderNumber.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( X12Delimiters != null ? X12Delimiters.GetHashCode () : 0 );
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
        public static bool operator == ( HealthCareClaim837Setup left, HealthCareClaim837Setup right )
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
        public static bool operator != ( HealthCareClaim837Setup left, HealthCareClaim837Setup right )
        {
            return !Equals ( left, right );
        }
    }
}
