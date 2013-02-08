using System.Collections.Generic;
using System.Linq;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Collection of attributes
    /// </summary>
    public class AttributeCollection : List<KeyValuePair<string, object>>, IAttributeCollection
    {
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="key">Key to find.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>   
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="key"/> is less than 0.
        /// -or-
        /// <paramref name="key"/> is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count"/>.
        /// </exception>
        public IEnumerable<object> this [ string key ]
        {
            get { return this.Where ( kvp => kvp.Key == key ).Select ( kvp => kvp.Value ); }
        }
    }
}
