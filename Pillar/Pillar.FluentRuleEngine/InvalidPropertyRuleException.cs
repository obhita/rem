namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Exception thrown by <see cref="IPropertyRule">Property Rules</see>.
    /// </summary>
    public class InvalidPropertyRuleException : InvalidRuleException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPropertyRuleException"/> class.
        /// </summary>
        /// <param name="message">Message to initialize exception with.</param>
        public InvalidPropertyRuleException ( string message )
            : base ( message )
        {
        }

        #endregion
    }
}
