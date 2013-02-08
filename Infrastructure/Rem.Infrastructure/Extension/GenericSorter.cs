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
using System.Linq;
using System.Linq.Expressions;

namespace Rem.Infrastructure.Extension
{
    /// <summary>
    /// This help class is used to encapsule sorting logic.
    /// </summary>
    /// <typeparam name="T">
    /// The type to sort. 
    /// </typeparam>
    internal class GenericSorter<T>
    {
        #region Public Methods

        public IEnumerable<T> Sort ( IEnumerable<T> source, string sortBy, bool isAscending, IComparer<object> comparer )
        {
            var param = Expression.Parameter ( typeof( T ), "item" );
            var sortExpression = Expression.Lambda<Func<T, object>> (
                Expression.Convert ( Expression.Property ( param, sortBy ), typeof( object ) ), param );
            IEnumerable<T> ret = null;
            if ( comparer != null )
            {
                ret = isAscending
                          ? source.AsQueryable ().OrderBy ( sortExpression, comparer )
                          : source.AsQueryable ().OrderByDescending ( sortExpression, comparer );
            }
            else
            {
                ret = isAscending ? source.AsQueryable ().OrderBy ( sortExpression ) : source.AsQueryable ().OrderByDescending ( sortExpression );
            }

            return ret;
        }

        #endregion
    }
}
