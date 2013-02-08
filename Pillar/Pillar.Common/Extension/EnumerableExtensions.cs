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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Pillar.Common.Extension
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtension
    {
        #region Public Methods

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the items in <paramref name="source"/> that are distict by the
        /// field returned by the <paramref name="keySelector"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of item stored in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <typeparam name="TKey">The key property.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> of items that are filtered by the method.</param>
        /// <param name="keySelector">A delegate that receives a <typeparamref name="TSource"/> and should return the key property.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the items from <paramref name="source"/> that are distict according
        /// to the property returned by the <paramref name="keySelector"/>.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TSource> DistinctBy<TSource, TKey> ( this IEnumerable<TSource> source, Func<TSource, TKey> keySelector )
        {
            var knownKeys = new HashSet<TKey> ();
            return source.Where ( element => knownKeys.Add ( keySelector ( element ) ) );
        }

        /// <summary>
        /// Iterates over the elements of the <see cref="IEnumerable{T}"/> and applies the provided <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="enumerable">The <see cref="IEnumerable{T}"/>.</param>
        /// <param name="action">The action that should be applied.</param>
        [DebuggerStepThrough]
        public static void ForEach<T> ( this IEnumerable<T> enumerable, Action<T> action )
        {
            foreach ( var item in enumerable )
            {
                action ( item );
            }
        }

        /// <summary>
        /// Iterates over the elements of an <see cref="IEnumerable{T}"/> and creates a single string by
        /// calling <see cref="object.ToString"/> on the elements and separating them with the provided <paramref name="delimiter"/>.
        /// </summary>
        /// <typeparam name="T">Type type of element in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="enumerable">The items to join together.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>A string containing the string representation of the elements in the <see cref="IEnumerable{T}"/> separated by the <paramref name="delimiter"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if given a null <see cref="IEnumerable{T}"/>.
        /// </exception>
        public static string ToDelimitedString<T> ( this IEnumerable<T> enumerable, string delimiter )
        {
            if ( delimiter == null )
            {
                throw new ArgumentNullException ( "delimiter" );
            }

            var ret = string.Empty;
            var builder = new StringBuilder ();
            if ( enumerable.Count () > 0 )
            {
                foreach ( var item in enumerable )
                {
                    builder.Append ( item + delimiter );
                }

                ret = builder.ToString ();

                // remove last comma
                ret = ret.Substring ( 0, ret.Length - delimiter.Length );
            }

            return ret;
        }

        #endregion
    }
}
