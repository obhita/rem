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

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// StaffHr defines human resources related information for a staff.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class StaffHr : IEquatable<StaffHr>
    {
        private StaffHr()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffHr"/> class.
        /// </summary>
        /// <param name="employmentType">Type of the employment.</param>
        /// <param name="titleName">Name of the title.</param>
        /// <param name="supervisorStaff">The supervisor staff.</param>
        /// <param name="confidentialNote">The confidential note.</param>
        protected internal StaffHr(EmploymentType employmentType, string titleName, Staff supervisorStaff, string confidentialNote)
        {
            EmploymentType = employmentType;
            TitleName = titleName;
            SupervisorStaff = supervisorStaff;
            ConfidentialNote = confidentialNote;
        }

        /// <summary>
        /// Gets the type of the employment.
        /// </summary>
        /// <value>
        /// The type of the employment.
        /// </value>
        public virtual EmploymentType EmploymentType { get; private set; }

        /// <summary>
        /// Gets the name of the title.
        /// </summary>
        /// <value>
        /// The name of the title.
        /// </value>
        public virtual string TitleName { get; private set; }

        /// <summary>
        /// Gets the supervisor staff.
        /// </summary>
        public virtual Staff SupervisorStaff { get; private set; }

        /// <summary>
        /// Gets the confidential note.
        /// </summary>
        public virtual string ConfidentialNote { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(StaffHr left, StaffHr right)
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
        public static bool operator !=(StaffHr left, StaffHr right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(StaffHr other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.EmploymentType, EmploymentType) && Equals(other.TitleName, TitleName) && Equals(other.SupervisorStaff, SupervisorStaff) && Equals(other.ConfidentialNote, ConfidentialNote);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
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
            if (obj.GetType() != typeof(StaffHr))
            {
                return false;
            }
            return Equals((StaffHr)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = EmploymentType != null ? EmploymentType.GetHashCode() : 0;
                result = (result * 397) ^ (TitleName != null ? TitleName.GetHashCode() : 0);
                result = (result * 397) ^ (SupervisorStaff != null ? SupervisorStaff.GetHashCode() : 0);
                result = (result * 397) ^ (ConfidentialNote != null ? ConfidentialNote.GetHashCode() : 0);
                return result;
            }
        }
    }
}