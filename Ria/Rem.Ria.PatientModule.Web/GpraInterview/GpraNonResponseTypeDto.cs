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
using System.Collections.Generic;
using Pillar.Common.Extension;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <content>
    /// Data transfer object for GpraNonResponseType class.
    /// </content>
    public partial class GpraNonResponseTypeDto<T> : EditableDataTransferObject,
                                             IEquatable<GpraNonResponseTypeDto<T>>,
                                             IEquatable<T>,
                                             IComparable<GpraNonResponseTypeDto<T>>,
                                             IComparable<T>
    {
        #region Constants and Fields

        private LookupValueDto _gpraNonResponse;
        private T _value;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the gpra non response.
        /// </summary>
        /// <value>The gpra non response.</value>
        public LookupValueDto GpraNonResponse
        {
            get { return _gpraNonResponse; }
            set { ApplyPropertyChange ( ref _gpraNonResponse, () => GpraNonResponse, value ); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Value
        {
            get { return _value; }
            set { ApplyPropertyChange ( ref _value, () => Value, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.</returns>
        public int CompareTo ( GpraNonResponseTypeDto<T> other )
        {
            if ( other == null )
            {
                return 1;
            }
            if ( HasValue () && !other.HasValue () )
            {
                return 1;
            }
            if ( !HasValue () && other.HasValue () )
            {
                return -1;
            }
            if ( !HasValue () && !other.HasValue () )
            {
                return Comparer<LookupValueDto>.Default.Compare ( GpraNonResponse, other.GpraNonResponse );
            }
            return CompareTo ( other.Value );
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.</returns>
        public int CompareTo ( T other )
        {
            if ( !HasValue () && ReferenceEquals ( null, other ) )
            {
                return 0;
            }
            if ( HasValue () && ReferenceEquals ( null, other ) )
            {
                return 1;
            }
            return Comparer<T>.Default.Compare ( Value, other );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( T other )
        {
            if ( ReferenceEquals ( null, other ) && !HasValue () )
            {
                return true;
            }
            if ( ReferenceEquals ( null, other ) && HasValue () )
            {
                return false;
            }
            return Value.Equals ( other );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( GpraNonResponseTypeDto<T> other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return base.Equals ( other ) && Equals ( other._value, _value ) && Equals ( other._gpraNonResponse, _gpraNonResponse );
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.</returns>
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
            return Equals ( obj as GpraNonResponseTypeDto<T> );
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current <see cref="T:System.Object"/>.</returns>
        public override int GetHashCode ()
        {
            unchecked
            {
                var result = base.GetHashCode ();
                result = ( result * 397 ) ^ ( HasValue () ? _value.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _gpraNonResponse != null ? _gpraNonResponse.GetHashCode () : 0 );
                return result;
            }
        }

        /// <summary>
        /// Determines whether this instance has value.
        /// </summary>
        /// <returns><c>true</c> if this instance has value; otherwise, <c>false</c>.</returns>
        public bool HasValue ()
        {
            return !Equals ( Value, typeof( T ).GetDefault () );
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString ()
        {
            if ( !Equals ( Value, typeof( T ).GetDefault () ) )
            {
                return Value.ToString ();
            }
            if ( GpraNonResponse != null )
            {
                return GpraNonResponse.ToString ();
            }
            return "-";
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator == ( GpraNonResponseTypeDto<T> left, GpraNonResponseTypeDto<T> right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Rem.Ria.PatientModule.Web.GpraInterview.GpraNonResponseTypeDto&lt;T&gt;"/> to generic type.
        /// </summary>
        /// <param name="gpraNonResponseTypeDto">The gpra non response type dto.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator T ( GpraNonResponseTypeDto<T> gpraNonResponseTypeDto )
        {
            if ( gpraNonResponseTypeDto.HasValue () )
            {
                return gpraNonResponseTypeDto.Value;
            }
            return ( T )typeof( T ).GetDefault ();
        }

        /// <summary>
        /// Performs an implicit conversion from generic type to <see cref="Rem.Ria.PatientModule.Web.GpraInterview.GpraNonResponseTypeDto&lt;T&gt;"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator GpraNonResponseTypeDto<T> ( T value )
        {
            return new GpraNonResponseTypeDto<T> { Value = value };
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator != ( GpraNonResponseTypeDto<T> left, GpraNonResponseTypeDto<T> right )
        {
            return !Equals ( left, right );
        }

        #endregion
    }
}
