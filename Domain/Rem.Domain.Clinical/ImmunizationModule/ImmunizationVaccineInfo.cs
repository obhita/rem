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
using Pillar.Domain.NamingStrategy;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ImmunizationModule
{
    /// <summary>
    /// ImmunizationVaccineInfo contains general vaccine related information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class ImmunizationVaccineInfo : IEquatable<ImmunizationVaccineInfo>
    {
        private ImmunizationVaccineInfo ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImmunizationVaccineInfo"/> class.
        /// </summary>
        /// <param name="vaccineCodedConcept">The vaccine coded concept.</param>
        /// <param name="vaccineLotNumber">The vaccine lot number.</param>
        /// <param name="immunizationVaccineManufacturer">The immunization vaccine manufacturer.</param>
        public ImmunizationVaccineInfo (
            CodedConcept vaccineCodedConcept,
            string vaccineLotNumber,
            ImmunizationVaccineManufacturer immunizationVaccineManufacturer)
        {
            Check.IsNotNull(vaccineCodedConcept, "vaccineCodedConcept is required.");
            Check.IsNotNull(immunizationVaccineManufacturer, "immunizationVaccineManufacturer is required.");

            VaccineCodedConcept = vaccineCodedConcept;
            VaccineLotNumber = vaccineLotNumber;
            ImmunizationVaccineManufacturer = immunizationVaccineManufacturer;
        }

        /// <summary>
        /// Gets the vaccine coded concept.
        /// </summary>
        public virtual CodedConcept VaccineCodedConcept { get; private set; }

        /// <summary>
        /// Gets the vaccine lot number.
        /// </summary>
        public virtual string VaccineLotNumber { get; private set; }

        /// <summary>
        /// Gets the immunization vaccine manufacturer.
        /// </summary>
        public virtual ImmunizationVaccineManufacturer ImmunizationVaccineManufacturer { get; private set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.
        /// </param>
        public bool Equals ( ImmunizationVaccineInfo other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.VaccineCodedConcept, VaccineCodedConcept ) && Equals ( other.VaccineLotNumber, VaccineLotNumber ) && Equals ( other.ImmunizationVaccineManufacturer, ImmunizationVaccineManufacturer );
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
            if ( obj.GetType () != typeof ( ImmunizationVaccineInfo ) )
            {
                return false;
            }
            return Equals ( ( ImmunizationVaccineInfo ) obj );
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
                int result = ( VaccineCodedConcept != null ? VaccineCodedConcept.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( VaccineLotNumber != null ? VaccineLotNumber.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( ImmunizationVaccineManufacturer != null ? ImmunizationVaccineManufacturer.GetHashCode () : 0 );
                return result;
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
            return VaccineCodedConcept == null ? base.ToString() : VaccineCodedConcept.DisplayName;
        }
    }
}