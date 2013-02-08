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

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientConfidentialInfo defines patient confidential information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class PatientConfidentialInfo : IEquatable<PatientConfidentialInfo>
    {
        private PatientConfidentialInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientConfidentialInfo"/> class.
        /// </summary>
        /// <param name="confidentialFamilyInformationDescription">The confidential family information description.</param>
        /// <param name="sexualAbuseVictimIndicator">The sexual abuse victim indicator.</param>
        /// <param name="physicalAbuseVictimIndicator">The physical abuse victim indicator.</param>
        /// <param name="domesticAbuseVictimIndicator">The domestic abuse victim indicator.</param>
        /// <param name="registeredSexOffenderDate">The registered sex offender date.</param>
        /// <param name="registeredSexOffenderIndicator">The registered sex offender indicator.</param>
        /// <param name="convictedOfArsonDate">The convicted of arson date.</param>
        /// <param name="convictedOfArsonIndicator">The convicted of arson indicator.</param>
        public PatientConfidentialInfo(string confidentialFamilyInformationDescription,
        bool? sexualAbuseVictimIndicator,
        bool? physicalAbuseVictimIndicator,
        bool? domesticAbuseVictimIndicator,
        DateTime? registeredSexOffenderDate,
        bool? registeredSexOffenderIndicator,
        DateTime? convictedOfArsonDate,
        bool? convictedOfArsonIndicator)
        {
            ConfidentialFamilyInformationDescription = confidentialFamilyInformationDescription;
            SexualAbuseVictimIndicator = sexualAbuseVictimIndicator;
            PhysicalAbuseVictimIndicator = physicalAbuseVictimIndicator;
            DomesticAbuseVictimIndicator = domesticAbuseVictimIndicator;

            RegisteredSexOffenderDate = registeredSexOffenderDate;
            RegisteredSexOffenderIndicator = registeredSexOffenderIndicator;

            ConvictedOfArsonDate = convictedOfArsonDate;
            ConvictedOfArsonIndicator = convictedOfArsonIndicator;
        }

        /// <summary>
        /// Gets the confidential family information description.
        /// </summary>
        public string ConfidentialFamilyInformationDescription { get; private set; }

        /// <summary>
        /// Gets the sexual abuse victim indicator.
        /// </summary>
        public bool? SexualAbuseVictimIndicator { get; private set; }

        /// <summary>
        /// Gets the physical abuse victim indicator.
        /// </summary>
        public bool? PhysicalAbuseVictimIndicator { get; private set; }

        /// <summary>
        /// Gets the domestic abuse victim indicator.
        /// </summary>
        public bool? DomesticAbuseVictimIndicator { get; private set; }
       
        /// <summary>
        /// Gets the registered sex offender date.
        /// </summary>
        public DateTime? RegisteredSexOffenderDate { get; private set; }
        
        /// <summary>
        /// Gets the registered sex offender indicator.
        /// </summary>
        public bool? RegisteredSexOffenderIndicator { get; private set; }
        
        /// <summary>
        /// Gets the convicted of arson date.
        /// </summary>
        public DateTime? ConvictedOfArsonDate { get; private set; }

        /// <summary>
        /// Gets the convicted of arson indicator.
        /// </summary>
        public bool? ConvictedOfArsonIndicator { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof (PatientConfidentialInfo))
            {
                return false;
            }
            return Equals((PatientConfidentialInfo) obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(PatientConfidentialInfo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.ConfidentialFamilyInformationDescription, ConfidentialFamilyInformationDescription) && other.SexualAbuseVictimIndicator.Equals(SexualAbuseVictimIndicator) && other.PhysicalAbuseVictimIndicator.Equals(PhysicalAbuseVictimIndicator) && other.DomesticAbuseVictimIndicator.Equals(DomesticAbuseVictimIndicator) && other.RegisteredSexOffenderDate.Equals(RegisteredSexOffenderDate) && other.RegisteredSexOffenderIndicator.Equals(RegisteredSexOffenderIndicator) && other.ConvictedOfArsonDate.Equals(ConvictedOfArsonDate) && other.ConvictedOfArsonIndicator.Equals(ConvictedOfArsonIndicator);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (ConfidentialFamilyInformationDescription != null ? ConfidentialFamilyInformationDescription.GetHashCode() : 0);
                result = (result*397) ^ (SexualAbuseVictimIndicator.HasValue ? SexualAbuseVictimIndicator.Value.GetHashCode() : 0);
                result = (result*397) ^ (PhysicalAbuseVictimIndicator.HasValue ? PhysicalAbuseVictimIndicator.Value.GetHashCode() : 0);
                result = (result*397) ^ (DomesticAbuseVictimIndicator.HasValue ? DomesticAbuseVictimIndicator.Value.GetHashCode() : 0);
                result = (result*397) ^ (RegisteredSexOffenderDate.HasValue ? RegisteredSexOffenderDate.Value.GetHashCode() : 0);
                result = (result*397) ^ (RegisteredSexOffenderIndicator.HasValue ? RegisteredSexOffenderIndicator.Value.GetHashCode() : 0);
                result = (result*397) ^ (ConvictedOfArsonDate.HasValue ? ConvictedOfArsonDate.Value.GetHashCode() : 0);
                result = (result*397) ^ (ConvictedOfArsonIndicator.HasValue ? ConvictedOfArsonIndicator.Value.GetHashCode() : 0);
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
        public static bool operator ==(PatientConfidentialInfo left, PatientConfidentialInfo right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(PatientConfidentialInfo left, PatientConfidentialInfo right)
        {
            return !Equals(left, right);
        }
    }
}
