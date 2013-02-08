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

namespace Rem.Domain.Clinical.ImmunizationModule
{
    /// <summary>
    /// ImmunizationAdministration defines the delivery of immunization to a patient.
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof ( PropertyNameAsColumnNameNamingStrategy ) )]
    public class ImmunizationAdministration : IEquatable<ImmunizationAdministration>
    {
        private ImmunizationAdministration ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmunizationAdministration"/> class.
        /// </summary>
        /// <param name="administeredAmount">The administered amount.</param>
        /// <param name="immunizationUnitOfMeasure">The immunization unit of measure.</param>
        public ImmunizationAdministration (
            double? administeredAmount,
            ImmunizationUnitOfMeasure immunizationUnitOfMeasure )
        {
            AdministeredAmount = administeredAmount;
            ImmunizationUnitOfMeasure = immunizationUnitOfMeasure;
        }

        /// <summary>
        /// Gets the administered amount.
        /// </summary>
        public virtual double? AdministeredAmount { get; private set; }

        /// <summary>
        /// Gets the immunization unit of measure.
        /// </summary>
        public virtual ImmunizationUnitOfMeasure ImmunizationUnitOfMeasure { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.
        /// </param>
        public bool Equals ( ImmunizationAdministration other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return other.AdministeredAmount.Equals ( AdministeredAmount ) && Equals ( other.ImmunizationUnitOfMeasure, ImmunizationUnitOfMeasure );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
        /// </exception><filterpriority>2</filterpriority>
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
            if ( obj.GetType () != typeof ( ImmunizationAdministration ) )
            {
                return false;
            }
            return Equals ( ( ImmunizationAdministration ) obj );
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
                return ( ( AdministeredAmount.HasValue ? AdministeredAmount.Value.GetHashCode () : 0 ) * 397 ) ^ ( ImmunizationUnitOfMeasure != null ? ImmunizationUnitOfMeasure.GetHashCode () : 0 );
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return AdministeredAmount == null && ImmunizationUnitOfMeasure == null ? base.ToString () : string.Format ( "{0} {1}", AdministeredAmount, ImmunizationUnitOfMeasure == null ? string.Empty : ImmunizationUnitOfMeasure.Name );
        }
    }
}