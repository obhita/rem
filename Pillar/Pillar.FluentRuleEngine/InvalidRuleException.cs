using System;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Exception that is thrown for invalid rules.
    /// </summary>
    public class InvalidRuleException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRuleException"/> class.
        /// </summary>
        /// <param name="message">Message to intialize rule with.</param>
        public InvalidRuleException ( string message )
            : base ( message )
        {
        }

        #endregion
    }
}
