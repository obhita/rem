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
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.GpraModule.NamingStrategy;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// <see cref="T:Rem.Domain.Clinical.GpraModule.GpraNonResponseType`1">GpraNonResponseType&lt;T&gt;</see> provides a list of possible 
    /// answer values when a direct answer is not provided for the question by the patient. 
    /// </summary>
    /// <typeparam name="T">The non response type.</typeparam>
    [Component]
    [ComponentNamingStrategy(typeof(GpraNonResponseTypeTypeNamingStrategy))]
    public struct GpraNonResponseType<T> : IEquatable<GpraNonResponseType<T>>
    {
        private readonly GpraNonResponse _gpraNonResponse;
        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraNonResponseType&lt;T&gt;"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public GpraNonResponseType ( T value )
        {
            Check.IsNotNull ( value, "Value Cannot be Null." );
            _value = value;
            _gpraNonResponse = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraNonResponseType&lt;T&gt;"/> struct.
        /// </summary>
        /// <param name="gpraNonResponse">The gpra non response.</param>
        public GpraNonResponseType ( GpraNonResponse gpraNonResponse )
        {
            Check.IsNotNull ( gpraNonResponse, "GpraNonResponse cannot equal null." );
            _gpraNonResponse = gpraNonResponse;
            _value = (T)typeof ( T ).GetDefault ();
        }

        /// <summary>
        /// Gets the T value.
        /// </summary>
        public T Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the gpra non response.
        /// </summary>
        public GpraNonResponse GpraNonResponse
        {
            get { return _gpraNonResponse; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        [IgnoreMapping]
        public bool HasValue
        {
            get { return !Equals ( _value, default( T ) ); }
        }

        #region Overrides
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var ret = string.Empty;

            if (!Equals(Value, typeof(T).GetDefault ()))
            {
                ret = Value.ToString();
            }
            else if (GpraNonResponse != null)
            {
                ret = GpraNonResponse.Name;
            }

            return ret;
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
            return (this as IEquatable<GpraNonResponseType<T>>).Equals((GpraNonResponseType<T>)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 0;
            if (!Equals(Value, typeof(T).GetDefault ()))
            {
                hash ^= Value.GetHashCode();
            }
            else if (GpraNonResponse != null)
            {
                hash ^= GpraNonResponse.GetHashCode();
            }
            return hash;
        }
        #endregion

        #region IEquatable<GpraNonResponseType<T>> interface
        bool IEquatable<GpraNonResponseType<T>>.Equals(GpraNonResponseType<T> other)
        {
            if (GetType() != other.GetType())
            {
                return false;
            }

            if (Equals(Value, other.Value) && Equals(GpraNonResponse, other.GpraNonResponse))
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
