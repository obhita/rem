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

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// AgencyName provides a name for an <see cref="Agency">Agency</see>
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(PropertyNameAsColumnNameNamingStrategy))]
    public class AgencyName : IEquatable<AgencyName>
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="AgencyName"/> class from being created.
        /// </summary>
        private AgencyName()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyName"/> class.
        /// </summary>
        /// <param name="legalName">Name of the legal.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="doingBusinessAsName">Name of the doing business as.</param>
        public AgencyName(string legalName, string displayName, string doingBusinessAsName)
        {
            Check.IsNotNullOrWhitespace(legalName, () => LegalName);

            LegalName = legalName;
            DisplayName = displayName;
            DoingBusinessAsName = doingBusinessAsName;
        }

        /// <summary>
        /// Gets the agency legal name.
        /// </summary>
        /// <value>
        /// The agency legal name.
        /// </value>
        [NotNull]
        public virtual string LegalName { get; private set; }

        /// <summary>
        /// Gets the doing business as name.
        /// </summary>
        /// <value>
        /// The  doing business as name.
        /// </value>
        public virtual string DoingBusinessAsName { get; private set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public virtual string DisplayName { get; private set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(AgencyName left, AgencyName right)
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
        public static bool operator !=(AgencyName left, AgencyName right)
        {
            return !Equals(left, right);
        }

        #region IEquatable<AgencyName> Members

        /// <summary>
        ///   Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///   true if the current object is equal to the <paramref name = "other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name = "other">An object to compare with this object.
        /// </param>
        public bool Equals(AgencyName other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.LegalName, LegalName) && Equals(other.DoingBusinessAsName, DoingBusinessAsName)
                   && Equals(other.DisplayName, DisplayName);
        }

        #endregion

        /// <summary>
        ///   Determines whether the specified <see cref = "T:System.Object" /> is equal to the current <see cref = "T:System.Object" />.
        /// </summary>
        /// <returns>
        ///   true if the specified <see cref = "T:System.Object" /> is equal to the current <see cref = "T:System.Object" />; otherwise, false.
        /// </returns>
        /// <param name = "obj">The <see cref = "T:System.Object" /> to compare with the current <see cref = "T:System.Object" />. 
        /// </param>
        /// <exception cref = "T:System.NullReferenceException">The <paramref name = "obj" /> parameter is null.
        /// </exception>
        /// <filterpriority>2</filterpriority>
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
            if (obj.GetType() != typeof(AgencyName))
            {
                return false;
            }
            return Equals((AgencyName)obj);
        }

        /// <summary>
        ///   Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///   A hash code for the current <see cref = "T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = LegalName != null ? LegalName.GetHashCode() : 0;
                result = (result * 397) ^ (DoingBusinessAsName != null ? DoingBusinessAsName.GetHashCode() : 0);
                result = (result * 397) ^ (DisplayName != null ? DisplayName.GetHashCode() : 0);
                return result;
            }
        }
    }
}