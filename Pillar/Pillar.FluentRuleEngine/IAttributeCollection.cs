using System.Collections.Generic;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Collection of Attributes
    /// </summary>
    public interface IAttributeCollection : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// Gets the list of objects with the specified key.
        /// </summary>
        /// <param name="key">Key to find.</param>
        /// <returns><see cref="IEnumerable{T}"/> of objects.</returns>
        IEnumerable<object> this[string key] { get; }
    }
}
