#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// Defines the X12 delimiters.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof( PropertyNameAsColumnNameNamingStrategy ) )]
    public class X12Delimiters : IEquatable<X12Delimiters>
    {
        private X12Delimiters ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12Delimiters"/> class.
        /// </summary>
        /// <param name="compositeDelimiter">
        /// The composite delimiter.
        /// </param>
        /// <param name="elementDelimiter">
        /// The element delimiter.
        /// </param>
        /// <param name="segmentDelimiter">
        /// The segment delimiter.
        /// </param>
        /// <param name="repetitionDelimiter">
        /// The repetition delimiter.
        /// </param>
        public X12Delimiters ( char compositeDelimiter, char elementDelimiter, char segmentDelimiter, char repetitionDelimiter )
        {
            Check.IsNotNull ( compositeDelimiter, "Composite Delimiter is required." );
            Check.IsNotNull ( elementDelimiter, "Element Delimiter is required." );
            Check.IsNotNull ( segmentDelimiter, "Segment Delimiter is required." );
            Check.IsNotNull ( repetitionDelimiter, "Repetition Delimiter is required." );

            CompositeDelimiter = compositeDelimiter;
            ElementDelimiter = elementDelimiter;
            SegmentDelimiter = segmentDelimiter;
            RepetitionDelimiter = repetitionDelimiter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12Delimiters"/> class.
        /// </summary>
        /// <param name="compositeDelimiter">
        /// The composite delimiter.
        /// </param>
        /// <param name="elementDelimiter">
        /// The element delimiter.
        /// </param>
        /// <param name="segmentDelimiter">
        /// The segment delimiter.
        /// </param>
        /// <param name="repetitionDelimiter">
        /// The repetition delimiter.
        /// </param>
        public X12Delimiters ( string compositeDelimiter, string elementDelimiter, string segmentDelimiter, string repetitionDelimiter )
        {
            Check.IsNotNull ( compositeDelimiter, "Composite Delimiter is required." );
            Check.IsNotNull ( elementDelimiter, "Element Delimiter is required." );
            Check.IsNotNull ( segmentDelimiter, "Segment Delimiter is required." );
            Check.IsNotNull ( repetitionDelimiter, "Repetition Delimiter is required." );

            CompositeDelimiter = char.Parse ( compositeDelimiter );
            ElementDelimiter = char.Parse ( elementDelimiter );
            SegmentDelimiter = char.Parse ( segmentDelimiter );
            RepetitionDelimiter = char.Parse ( repetitionDelimiter );
        }

        /// <summary>
        /// Gets the composite delimiter.
        /// </summary>
        [NotNull]
        public virtual char CompositeDelimiter { get; private set; }

        /// <summary>
        /// Gets the element delimiter.
        /// </summary>
        [NotNull]
        public virtual char ElementDelimiter { get; private set; }

        /// <summary>
        /// Gets the segment delimiter.
        /// </summary>
        [NotNull]
        public virtual char SegmentDelimiter { get; private set; }

        /// <summary>
        /// Gets the repetition delimiter.
        /// </summary>
        [NotNull]
        public virtual char RepetitionDelimiter { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals ( X12Delimiters other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.CompositeDelimiter == CompositeDelimiter && other.ElementDelimiter == ElementDelimiter
                   && other.SegmentDelimiter == SegmentDelimiter && other.RepetitionDelimiter == RepetitionDelimiter;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param>
        /// <filterpriority>2</filterpriority>
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
            if ( obj.GetType () != typeof( X12Delimiters ) )
            {
                return false;
            }
            return Equals ( ( X12Delimiters )obj );
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
                int result = CompositeDelimiter.GetHashCode ();
                result = ( result * 397 ) ^ ElementDelimiter.GetHashCode ();
                result = ( result * 397 ) ^ SegmentDelimiter.GetHashCode ();
                result = ( result * 397 ) ^ RepetitionDelimiter.GetHashCode ();
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
        public static bool operator == ( X12Delimiters left, X12Delimiters right )
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
        public static bool operator != ( X12Delimiters left, X12Delimiters right )
        {
            return !Equals ( left, right );
        }
    }
}
