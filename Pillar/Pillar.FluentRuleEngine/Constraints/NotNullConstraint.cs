using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires object to not be null.
    /// </summary>
    public class NotNullConstraint : ConstraintBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullConstraint"/> class.
        /// </summary>
        public NotNullConstraint ()
            : base ( Messages.Constraints_NotNull_Message )
        {
        }

        /// <inheritdoc/>
        public override bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext )
        {
            var isCompliant = true;

            if ( propertyValue == null )
            {
                isCompliant = false;
            }

            return isCompliant;
        }
    }
}
