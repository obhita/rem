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

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// Height defines a patient height in feet and inches.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(TypeNamePlusPropertyNameAsColumnNameNamingStrategy))]
    public class Height : IEquatable<Height>
    {
        private Height ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Height"/> class.
        /// </summary>
        /// <param name="feetMeasure">The feet measure.</param>
        /// <param name="inchesMeasure">The inches measure.</param>
        public Height ( int? feetMeasure, double? inchesMeasure )
        {
            FeetMeasure = feetMeasure;
            InchesMeasure = inchesMeasure;
        }

        /// <summary>
        /// Gets the feet measure.
        /// </summary>
        public virtual int? FeetMeasure { get; private set; }

        /// <summary>
        /// Gets the inches measure.
        /// </summary>
        public virtual double? InchesMeasure { get; private set; }

        /// <summary>
        /// Gets the total height in inches.
        /// </summary>
        /// <returns>A double.</returns>
        public virtual double GetTotalHeightInInches ()
        {
            var heightInFeet = FeetMeasure == null ? 0.0 : FeetMeasure.Value;
            var heightInInches = InchesMeasure == null ? 0.0 : InchesMeasure.Value;

            return (heightInFeet * 12.0) + heightInInches;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( Height other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.FeetMeasure.Equals ( FeetMeasure ) && other.InchesMeasure.Equals ( InchesMeasure );
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
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
            if ( obj.GetType () != typeof ( Height ) )
            {
                return false;
            }
            return Equals ( ( Height ) obj );
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
                return ( ( FeetMeasure.HasValue ? FeetMeasure.Value : 0 ) * 397 ) ^ ( InchesMeasure.HasValue ? InchesMeasure.Value.GetHashCode () : 0 );
            }
        }
    }
}