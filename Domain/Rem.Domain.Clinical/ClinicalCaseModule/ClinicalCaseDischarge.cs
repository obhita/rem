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
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.ClinicalCaseModule
{
    /// <summary>
    /// The ClinicalCaseDischarge defines information about patient discharge.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class ClinicalCaseDischarge : IEquatable<ClinicalCaseDischarge>
    {
        private ClinicalCaseDischarge()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseDischarge"/> class.
        /// </summary>
        /// <param name="dischargeDate">The discharge date.</param>
        /// <param name="dischargeNote">The discharge note.</param>
        /// <param name="dischargeReason">The discharge reason.</param>
        /// <param name="dischargedByStaff">The discharged by staff.</param>
        public ClinicalCaseDischarge(
            DateTime? dischargeDate,
            string dischargeNote,
            DischargeReason dischargeReason,
            Staff dischargedByStaff)
        {
            DischargeDate = dischargeDate;
            DischargeNote = dischargeNote;
            DischargeReason = dischargeReason;
            DischargedByStaff = dischargedByStaff;
        }

        /// <summary>
        /// Gets the discharge date.
        /// </summary>
        public virtual DateTime? DischargeDate { get; private set; }

        /// <summary>
        /// Gets the discharge note.
        /// </summary>
        public virtual string DischargeNote { get; private set; }

        /// <summary>
        /// Gets the discharge reason.
        /// </summary>
        public virtual DischargeReason DischargeReason { get; private set; }

        /// <summary>
        /// Gets the discharged by staff.
        /// </summary>
        public virtual Staff DischargedByStaff { get; private set; }

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
            if (obj.GetType() != typeof(ClinicalCaseDischarge))
            {
                return false;
            }
            return Equals((ClinicalCaseDischarge)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(ClinicalCaseDischarge other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return other.DischargeDate.Equals(this.DischargeDate) && Equals(other.DischargeNote, this.DischargeNote) && Equals(other.DischargeReason, this.DischargeReason) && Equals(other.DischargedByStaff, this.DischargedByStaff);
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
                int result = (this.DischargeDate.HasValue ? this.DischargeDate.Value.GetHashCode() : 0);
                result = (result * 397) ^ (this.DischargeNote != null ? this.DischargeNote.GetHashCode() : 0);
                result = (result * 397) ^ (this.DischargeReason != null ? this.DischargeReason.GetHashCode() : 0);
                result = (result * 397) ^ (this.DischargedByStaff != null ? this.DischargedByStaff.GetHashCode() : 0);
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
        public static bool operator ==(ClinicalCaseDischarge left, ClinicalCaseDischarge right)
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
        public static bool operator !=(ClinicalCaseDischarge left, ClinicalCaseDischarge right)
        {
            return !Equals(left, right);
        }
    }
}