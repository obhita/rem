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
using System.Text.RegularExpressions;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// Defines the FTP client object.
    /// </summary>
    [Component]
    [ComponentNamingStrategy(typeof(TypeNamePlusPropertyNameAsColumnNameNamingStrategy))]
    public class Ftp: IEquatable<Ftp>
    {
        /// <summary>
        /// Validation Expression
        /// </summary>
        public static readonly string ValidationExpression = @"^ftp\://[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|COM|ORG|NET|MIL|EDU)$";

        private Ftp()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ftp"/> class.
        /// </summary>
        /// <param name="hostValue">The host value.</param>
        public Ftp(string hostValue)
        {
            Check.IsNotNullOrWhitespace(hostValue, "FTP host is required");
            Validate(hostValue);
            HostValue = hostValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ftp"/> class.
        /// </summary>
        /// <param name="hostValue">The host value.</param>
        /// <param name="username">The username.</param>
        public Ftp (string hostValue, string username)
            : this (hostValue)
        {
            UserName = username;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ftp"/> class.
        /// </summary>
        /// <param name="hostValue">The host value.</param>
        /// <param name="userName">The username.</param>
        /// <param name="passCode">The password.</param>
        public Ftp(string hostValue, string userName, string passCode)
            : this(hostValue, userName)
        {
            PassCode = passCode;
        }


        /// <summary>
        /// Gets the host value.
        /// </summary>
        [NotNull]
        public string HostValue { get; private set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the password .
        /// </summary>
        public string PassCode { get; private set; }

        private static void Validate(string host)
        {
            var regex = new Regex(ValidationExpression);
            if (!regex.IsMatch(host))
            {
                throw new ArgumentException(string.Format("{0} is not a valid FTP host URI.", host));
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals ( Ftp other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other.HostValue, HostValue ) && Equals ( other.UserName, UserName ) && Equals ( other.PassCode, PassCode );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
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
            if ( obj.GetType () != typeof( Ftp ) )
            {
                return false;
            }
            return Equals ( ( Ftp )obj );
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
                int result = ( HostValue != null ? HostValue.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( UserName != null ? UserName.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( PassCode != null ? PassCode.GetHashCode () : 0 );
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
        public static bool operator == ( Ftp left, Ftp right )
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
        public static bool operator != ( Ftp left, Ftp right )
        {
            return !Equals ( left, right );
        }
    }
}