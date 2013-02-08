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

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <content>
    /// Data transfer object for TedsAnswer class.
    /// </content>
    public partial class TedsAnswerDto<T> : EditableDataTransferObject,
                                             IEquatable<TedsAnswerDto<T>>,
                                             IEquatable<T>,
                                             IComparable<TedsAnswerDto<T>>,
                                             IComparable<T>
    {
        #region Constants and Fields

        private LookupValueDto _tedsNonResponse;
        private T _response;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the TEDS non response.
        /// </summary>
        /// <value>The TEDS non response.</value>
        public LookupValueDto TedsNonResponse
        {
            get { return _tedsNonResponse; }
            set { ApplyPropertyChange ( ref _tedsNonResponse, () => TedsNonResponse, value ); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public T Response
        {
            get { return _response; }
            set { ApplyPropertyChange ( ref _response, () => Response, value ); }
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
        public int CompareTo ( TedsAnswerDto<T> other )
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
                return Comparer<LookupValueDto>.Default.Compare ( TedsNonResponse, other.TedsNonResponse );
            }
            return CompareTo ( other.Response );
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
            return Comparer<T>.Default.Compare ( Response, other );
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
            return Response.Equals ( other );
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
        public bool Equals ( TedsAnswerDto<T> other )
        {
            if ( ReferenceEquals ( null, other ) )
            {
                return false;
            }
            if ( ReferenceEquals ( this, other ) )
            {
                return true;
            }
            return Equals ( other._response, _response ) && Equals ( other._tedsNonResponse, _tedsNonResponse );
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
            return Equals ( obj as TedsAnswerDto<T> );
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
                result = ( result * 397 ) ^ ( HasValue () ? _response.GetHashCode () : 0 );
                result = ( result * 397 ) ^ ( _tedsNonResponse != null ? _tedsNonResponse.GetHashCode () : 0 );
                return result;
            }
        }

        /// <summary>
        /// Determines whether this instance has value.
        /// </summary>
        /// <returns><c>true</c> if this instance has value; otherwise, <c>false</c>.</returns>
        public bool HasValue ()
        {
            return !Equals ( Response, typeof( T ).GetDefault () );
        }

        /// <summary>
        /// Determines whether this instance is answered.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is answered; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAnswered()
        {
            return HasValue() || TedsNonResponse != null;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString ()
        {
            if ( !Equals ( Response, typeof( T ).GetDefault () ) )
            {
                return Response.ToString ();
            }
            if ( TedsNonResponse != null )
            {
                return TedsNonResponse.ToString ();
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
        public static bool operator == ( TedsAnswerDto<T> left, TedsAnswerDto<T> right )
        {
            return Equals ( left, right );
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="TedsAnswerDto{T}"/> to generic type.
        /// </summary>
        /// <param name="tedsNonResponseTypeDto">The teds non response type dto.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator T ( TedsAnswerDto<T> tedsNonResponseTypeDto )
        {
            if ( tedsNonResponseTypeDto.HasValue () )
            {
                return tedsNonResponseTypeDto.Response;
            }
            return ( T )typeof( T ).GetDefault ();
        }

        /// <summary>
        /// Performs an implicit conversion from generic type to <see cref="TedsAnswerDto{T}"/>.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator TedsAnswerDto<T>(T response)
        {
            return new TedsAnswerDto<T> { Response = response };
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator != ( TedsAnswerDto<T> left, TedsAnswerDto<T> right )
        {
            return !Equals ( left, right );
        }

        #endregion
    }
}
