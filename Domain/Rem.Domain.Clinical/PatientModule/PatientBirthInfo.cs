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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// PatientBirthInfo defines patient birth information.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class PatientBirthInfo : IEquatable<PatientBirthInfo>
    {
        private PatientBirthInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientBirthInfo"/> class.
        /// </summary>
        /// <param name="birthFirstName">First name of the birth.</param>
        /// <param name="birthLastName">Last name of the birth.</param>
        /// <param name="birthCityName">Name of the birth city.</param>
        /// <param name="birthCountyArea">The birth county area.</param>
        /// <param name="birthStateProvince">The birth state province.</param>
        public PatientBirthInfo(
            string birthFirstName,
            string birthLastName,
            string birthCityName,
            CountyArea birthCountyArea,
            StateProvince birthStateProvince)
        {
            BirthFirstName = birthFirstName;
            BirthLastName = birthLastName;
            BirthCityName = birthCityName;
            BirthCountyArea = birthCountyArea;
            BirthStateProvince = birthStateProvince;
        }

        /// <summary>
        /// Gets the first name of the birth.
        /// </summary>
        /// <value>
        /// The first name of the birth.
        /// </value>
        public virtual string BirthFirstName { get; private set; }

        /// <summary>
        /// Gets the last name of the birth.
        /// </summary>
        /// <value>
        /// The last name of the birth.
        /// </value>
        public virtual string BirthLastName { get; private set; }

        /// <summary>
        /// Gets the name of the birth city.
        /// </summary>
        /// <value>
        /// The name of the birth city.
        /// </value>
        public virtual string BirthCityName { get; private set; }

        /// <summary>
        /// Gets the birth county area.
        /// </summary>
        public virtual CountyArea BirthCountyArea { get; private set; }

        /// <summary>
        /// Gets the birth state province.
        /// </summary>
        public virtual StateProvince BirthStateProvince { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(PatientBirthInfo left, PatientBirthInfo right)
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
        public static bool operator !=(PatientBirthInfo left, PatientBirthInfo right)
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
            if (obj.GetType() != typeof (PatientBirthInfo))
            {
                return false;
            }
            return Equals((PatientBirthInfo) obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(PatientBirthInfo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.BirthFirstName, BirthFirstName) && Equals(other.BirthLastName, BirthLastName) && Equals(other.BirthCityName, BirthCityName) && Equals(other.BirthCountyArea, BirthCountyArea) && Equals(other.BirthStateProvince, BirthStateProvince);
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
                int result = (BirthFirstName != null ? BirthFirstName.GetHashCode() : 0);
                result = (result*397) ^ (BirthLastName != null ? BirthLastName.GetHashCode() : 0);
                result = (result*397) ^ (BirthCityName != null ? BirthCityName.GetHashCode() : 0);
                result = (result*397) ^ (BirthCountyArea != null ? BirthCountyArea.GetHashCode() : 0);
                result = (result*397) ^ (BirthStateProvince != null ? BirthStateProvince.GetHashCode() : 0);
                return result;
            }
        }
    }
}