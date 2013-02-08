namespace Pillar.FluentRuleEngine.RuleSelectors
{
    /// <summary>
    /// Interface for class that takes a parameter
    /// </summary>
    public interface ITakeParameter
    {
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        object Parameter { get; set; }
    }
}
