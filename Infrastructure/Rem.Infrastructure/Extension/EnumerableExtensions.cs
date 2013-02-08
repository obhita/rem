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
using System.Linq;

namespace Rem.Infrastructure.Extension
{
    /// <summary>
    /// Enumerable extension class to sort and order a collection.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Apply an OrderBy rule that is based on a sort property.
        /// </summary>
        /// <typeparam name="T">The type of the objects that are stored in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="sortProperty">The property to sort on.</param>
        /// <param name="isAscending">The direction to sort by.</param>
        /// <param name="comparer">The optional comparer used to perform the comparison.</param>
        /// <returns>A sorter collection.</returns>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> collection, string sortProperty, bool isAscending = true, IComparer<object> comparer = null)
        {
            //Get the collection type
            Type dataSourceType = collection.GetType();

            // Determine the data type of the items in the data source at runtime.
            Type dataItemType = typeof(object);
            if (dataSourceType.HasElementType)
            {
                dataItemType = dataSourceType.GetElementType ();
            }
            else if (dataSourceType.IsGenericType)
            {
                dataItemType = dataSourceType.GetGenericArguments ()[0];
            }

            // Create an instance of the GenericSorter class passing in the data item type.
            Type sorterType = typeof(GenericSorter<>).MakeGenericType(dataItemType);
            var sorterObject = Activator.CreateInstance(sorterType);

            // Now I can call the "Sort" method passing in my runtime types.
            var sortMethod = sorterType.GetMethod ( "Sort", new[] { dataSourceType, typeof ( string ), typeof( bool ), typeof(IComparer<object>) } );
            return sortMethod.Invoke(sorterObject, new object[] { collection, sortProperty, isAscending, comparer }) as IEnumerable<T>;
        }

        /// <summary>
        /// Sort a collection based on a sort property.
        /// </summary>
        /// <typeparam name="T">The type of the objects that are stored in the collection.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="sortProperty">The property to sort on.</param>
        public static void Sort<T>(this List<T> list, string sortProperty)
        {
            IEnumerable<T> sorted = list.OrderBy(sortProperty).ToList();
            list.Clear();
            list.AddRange(sorted);
        }
    }
}
