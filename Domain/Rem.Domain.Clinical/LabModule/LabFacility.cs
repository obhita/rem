#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using System.Text;
using Pillar.Domain;
using Pillar.Domain.NamingStrategy;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// The LabFacility defines laboratory physical location. 
    /// </summary>
    [Component]
    [ComponentNamingStrategy ( typeof( TypeNamePlusPropertyNameAsColumnNameNamingStrategy ) )]
    public class LabFacility : IEquatable<LabFacility>
    {
        private readonly string _cityName;
        private readonly string _name;
        private readonly string _postalCode;
        private readonly string _stateName;
        private readonly string _streetAddress;

        private LabFacility ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabFacility"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="streetAddress">
        /// The street address.
        /// </param>
        /// <param name="cityName">
        /// Name of the city.
        /// </param>
        /// <param name="stateName">
        /// Name of the state.
        /// </param>
        /// <param name="postalCode">
        /// The postal code.
        /// </param>
        public LabFacility ( string name, string streetAddress, string cityName, string stateName, string postalCode )
        {
            _name = name;
            _streetAddress = streetAddress;
            _cityName = cityName;
            _stateName = stateName;
            _postalCode = postalCode;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the street address.
        /// </summary>
        public virtual string StreetAddress
        {
            get { return _streetAddress; }
        }

        /// <summary>
        /// Gets the name of the city.
        /// </summary>
        /// <value>
        /// The name of the city.
        /// </value>
        public virtual string CityName
        {
            get { return _cityName; }
        }

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public virtual string StateName
        {
            get { return _stateName; }
        }

        /// <summary>
        /// Gets the postal code.
        /// </summary>
        public virtual string PostalCode
        {
            get { return _postalCode; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            var sb = new StringBuilder ();
            sb.Append ( "Lab Facility Name: " );
            sb.Append ( _name );

            return sb.ToString ();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// True if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">
        /// An object to compare with this object.
        /// </param>
        public bool Equals ( LabFacility other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other._cityName, _cityName ) && Equals ( other._name, _name ) && Equals ( other._postalCode, _postalCode )
                   && Equals ( other._stateName, _stateName ) && Equals ( other._streetAddress, _streetAddress );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// True if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        /// </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals ( object obj )
        {
            return ( this as IEquatable<LabFacility> ).Equals ( obj as LabFacility );
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
                int result = _cityName != null ? _cityName.GetHashCode () : 0;
                result = ( result * 397 ) ^ ( _name != null ? _name.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _postalCode != null ? _postalCode.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _stateName != null ? _stateName.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _streetAddress != null ? _streetAddress.GetHashCode () : 0 );
                return result;
            }
        }
    }
}
