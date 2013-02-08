using System;
using System.Linq.Expressions;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Base class used to hold base property rule properties.
    /// </summary>
    public class PropertyRuleBase : Rule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyRuleBase"/> class.
        /// </summary>
        /// <param name="propertyExpression">Property Expression for property.</param>
        /// <param name="name">Name of Rule.</param>
        protected PropertyRuleBase ( LambdaExpression propertyExpression, string name )
            : base ( name )
        {
            Check.IsNotNull ( propertyExpression, "Property Expression is required" );

            PropertyExpression = propertyExpression;
            PropertyChain = PropertyChain.FromLambdaExpression ( propertyExpression );
            AddAttribute("PropertyChain", PropertyChain);
        }

        /// <summary>
        /// Delegate that gets the value of the property.
        /// </summary>
        protected Delegate PropertyValueDelegate { get; set; }

        /// <inheritdoc/>
        public PropertyChain PropertyChain { get; private set; }

        /// <inheritdoc/>
        public LambdaExpression PropertyExpression { get; private set; }
    }
}
