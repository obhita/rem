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
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// The ClinicalCaseProfile defines general clinical case information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class ClinicalCaseProfile : IEquatable<ClinicalCaseProfile>
    {
        private ClinicalCaseProfile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseProfile"/> class.
        /// </summary>
        /// <param name="initialLocation">The initial location.</param>
        /// <param name="clinicalCaseStartDate">The clinical case start date.</param>
        /// <param name="performedByStaff">The performed by staff.</param>
        /// <param name="patientPresentingProblemNote">The patient presenting problem note.</param>
        /// <param name="referralType">Type of the referral.</param>
        /// <param name="initialContactMethod">The initial contact method.</param>
        public ClinicalCaseProfile(
            Location initialLocation,
            DateTime? clinicalCaseStartDate,
            Staff performedByStaff,
            string patientPresentingProblemNote,
            ReferralType referralType,
            InitialContactMethod initialContactMethod)
        {
            Check.IsNotNull(initialLocation, "Initial location is required.");

            InitialLocation = initialLocation;
            ClinicalCaseStartDate = clinicalCaseStartDate;
            PerformedByStaff = performedByStaff;
            PatientPresentingProblemNote = patientPresentingProblemNote;
            ReferralType = referralType;
            InitialContactMethod = initialContactMethod;
        }

        /// <summary>
        /// Gets the initial location.
        /// </summary>
        [NotNull]
        public virtual Location InitialLocation { get; private set; }

        /// <summary>
        /// Gets the clinical case start date.
        /// </summary>
        public virtual DateTime? ClinicalCaseStartDate { get; private set; }

        /// <summary>
        /// Gets the performed by staff.
        /// </summary>
        public virtual Staff PerformedByStaff { get; private set; }

        /// <summary>
        /// Gets the patient presenting problem note.
        /// </summary>
        public virtual string PatientPresentingProblemNote { get; private set; }

        /// <summary>
        /// Gets the type of the referral.
        /// </summary>
        /// <value>
        /// The type of the referral.
        /// </value>
        public virtual ReferralType ReferralType { get; private set; }

        /// <summary>
        /// Gets the initial contact method.
        /// </summary>
        public virtual InitialContactMethod InitialContactMethod { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(ClinicalCaseProfile left, ClinicalCaseProfile right)
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
        public static bool operator !=(ClinicalCaseProfile left, ClinicalCaseProfile right)
        {
            return !Equals(left, right);
        }

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
            if (obj.GetType() != typeof(ClinicalCaseProfile))
            {
                return false;
            }
            return Equals((ClinicalCaseProfile)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(ClinicalCaseProfile other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.InitialLocation, this.InitialLocation) && other.ClinicalCaseStartDate.Equals(this.ClinicalCaseStartDate)
                   && Equals(other.PerformedByStaff, this.PerformedByStaff)
                   && Equals(other.PatientPresentingProblemNote, this.PatientPresentingProblemNote) && Equals(other.ReferralType, this.ReferralType)
                   && Equals(other.InitialContactMethod, this.InitialContactMethod);
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
                int result = (this.InitialLocation != null ? this.InitialLocation.GetHashCode() : 0);
                result = (result * 397) ^ (this.ClinicalCaseStartDate.HasValue ? this.ClinicalCaseStartDate.Value.GetHashCode() : 0);
                result = (result * 397) ^ (this.PerformedByStaff != null ? this.PerformedByStaff.GetHashCode() : 0);
                result = (result * 397) ^ (this.PatientPresentingProblemNote != null ? this.PatientPresentingProblemNote.GetHashCode() : 0);
                result = (result * 397) ^ (this.ReferralType != null ? this.ReferralType.GetHashCode() : 0);
                result = (result * 397) ^ (this.InitialContactMethod != null ? this.InitialContactMethod.GetHashCode() : 0);
                return result;
            }
        }
    }
}