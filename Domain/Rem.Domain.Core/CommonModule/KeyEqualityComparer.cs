using System;
using System.Collections.Generic;
using Pillar.Domain;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// KeyEqualityComparer&lt;T&gt; provides an equality comparison implementation for a keyExtractor.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    public class KeyEqualityComparer<T> : IEqualityComparer<T>
        where T : Entity
    {
        private readonly Func<T, object> _keyExtractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEqualityComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="keyExtractor">The key extractor.</param>
        public KeyEqualityComparer(Func<T, object> keyExtractor)
        {
            _keyExtractor = keyExtractor;
        }

        #region IEqualityComparer<T> Members

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y)
        {
            return _keyExtractor(x).Equals(_keyExtractor(y));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
        public int GetHashCode(T obj)
        {
            return _keyExtractor(obj).GetHashCode();
        }

        #endregion
    }
}