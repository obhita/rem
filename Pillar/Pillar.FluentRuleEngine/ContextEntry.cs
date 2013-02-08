namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Class used for items added to <see cref="WorkingMemory"/>
    /// </summary>
    public class ContextEntry
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of the entry.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the context object.
        /// </summary>
        /// <value>
        /// The context object.
        /// </value>
        public object ContextObject { get; set; }
    }
}
