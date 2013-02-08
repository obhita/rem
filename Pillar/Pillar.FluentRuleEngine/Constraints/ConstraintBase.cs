namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// Abstract base class for <see cref="IConstraint">IConstraint</see>.
    /// </summary>
    public abstract class ConstraintBase : IConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintBase"/> class.
        /// </summary>
        /// <param name="message">Message used for constraint.</param>
        protected ConstraintBase ( string message )
        {
            Message = message;
        }

        #region IConstraint Members

        /// <inheritdoc/>
        public string Message { get; protected set; }

        /// <inheritdoc/>
        public abstract bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext );

        #endregion
    }
}
