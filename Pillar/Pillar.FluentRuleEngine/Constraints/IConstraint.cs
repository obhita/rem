namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// Interface for defining a constraint.
    /// </summary>
    public interface IConstraint
    {
        /// <summary>
        /// Gets the Error Message for the constraint.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets whether the <paramref name="propertyValue">propertyValue</paramref> is compliant with the constraint.
        /// </summary>
        /// <param name="propertyValue">Object to test for compliancy.</param>
        /// <param name="ruleEngineContext">Current Rule Engine Context.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext );
    }
}
