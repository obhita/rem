using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires object to not be empty.
    /// </summary>
    public class NotEmptyConstraint : ConstraintBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEmptyConstraint"/> class.
        /// </summary>
        public NotEmptyConstraint ()
            : base ( Messages.Constraints_NotEmpty_Message )
        {
        }

        /// <inheritdoc/>
        public override bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext )
        {
            var isCompliant = true;

            if ( propertyValue is string )
            {
                if ( string.IsNullOrEmpty ( propertyValue.ToString () ) )
                {
                    isCompliant = false;
                }
            }
            else if ( propertyValue == null )
            {
                isCompliant = false;
            }

            return isCompliant;
        }
    }
}
