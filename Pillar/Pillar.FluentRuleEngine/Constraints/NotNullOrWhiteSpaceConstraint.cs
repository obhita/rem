using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires object to not be null or white space.
    /// </summary>
    public class NotNullOrWhiteSpaceConstraint : ConstraintBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotNullOrWhiteSpaceConstraint"/> class.
        /// </summary>
        public NotNullOrWhiteSpaceConstraint ()
            : base ( Messages.Constraints_NotEmptyOrWhiteSpace_Message )
        {
        }

        /// <inheritdoc/>
        public override bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext )
        {
            var isCompliant = true;

            if ( propertyValue is string )
            {
                if ( string.IsNullOrWhiteSpace ( propertyValue.ToString () ) )
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
