﻿#region License
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
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Clinical.TedsModule.NamingStrategy;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// <see cref="T:Rem.Domain.Clinical.TedsModule.TedsAnswer`1">TedsAnswer&lt;T&gt;</see> defines an answer which is direct response or non-reponse. 
    /// </summary>
    /// <typeparam name="T">The non response type.</typeparam>
    [Component]
    [ComponentNamingStrategy(typeof(TedsAnswerTypeNamingStrategy))]
    public class TedsAnswer<T> : IEquatable<TedsAnswer<T>>
    {
        private readonly TedsNonResponse _tedsNonResponse;
        private readonly T _response;

        private TedsAnswer()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAnswer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public TedsAnswer(T response)
        {
            Check.IsNotNull(response, "Response cannot be null.");
            _response = response;
            _tedsNonResponse = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsAnswer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="tedsNonResponse">The teds non response.</param>
        public TedsAnswer(TedsNonResponse tedsNonResponse)
        {
            Check.IsNotNull(tedsNonResponse, "TedsNonResponse cannot equal null.");
            _tedsNonResponse = tedsNonResponse;
            _response = (T)typeof(T).GetDefault();
        }

        /// <summary>
        /// Gets the response of type T.
        /// </summary>
        public T Response
        {
            get
            {
                return _response;
            }
        }

        /// <summary>
        /// Gets the Teds non response.
        /// </summary>
        public TedsNonResponse TedsNonResponse
        {
            get
            {
                return _tedsNonResponse;
            }
        }

        /// <summary>
        /// Gets a response indicating whether this instance has response.
        /// </summary>
        /// <response>
        ///   <c>true</c> if this instance has response; otherwise, <c>false</c>.
        /// </response>
        [IgnoreMapping]
        public bool HasResponse
        {
            get
            {
                return !Equals(_response, default(T));
            }
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

            if (!Equals(Response, typeof(T).GetDefault()))
            {
                ret = Response.ToString();
            }
            else if (TedsNonResponse != null)
            {
                ret = TedsNonResponse.Name;
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
            return (this as IEquatable<TedsAnswer<T>>).Equals((TedsAnswer<T>)obj);
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
            if (!Equals(Response, typeof(T).GetDefault()))
            {
                hash ^= Response.GetHashCode();
            }
            else if (TedsNonResponse != null)
            {
                hash ^= TedsNonResponse.GetHashCode();
            }
            return hash;
        }

        #endregion

        #region IEquatable<TedsAnswer<T>> interface

        bool IEquatable<TedsAnswer<T>>.Equals(TedsAnswer<T> other)
        {
            if (GetType() != other.GetType())
            {
                return false;
            }

            if (Equals(Response, other.Response) && Equals(TedsNonResponse, other.TedsNonResponse))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
