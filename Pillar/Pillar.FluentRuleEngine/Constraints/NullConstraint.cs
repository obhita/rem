using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires object to be null.
    /// </summary>
    public class NullConstraint : ConstraintBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullConstraint"/> class.
        /// </summary>
        public NullConstraint ()
            : base ( Messages.Constraints_Null_Message )
        {
        }

        /// <inheritdoc/>
        public override bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext )
        {
            var isCompliant = true;

            if ( propertyValue != null )
            {
                isCompliant = false;
            }

            return isCompliant;
        }
    }
}
