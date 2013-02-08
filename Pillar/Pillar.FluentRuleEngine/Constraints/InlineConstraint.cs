using System;
using System.Collections.Generic;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires object to be satisfy Predicate.
    /// </summary>
    /// <typeparam name="TProperty">Type of property.</typeparam>
    public class InlineConstraint<TProperty> : ConstraintBase
    {
        private readonly Predicate<TProperty> _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineConstraint&lt;TProperty&gt;"/> class.
        /// </summary>
        /// <param name="predicate"><see cref="Predicate{T}">Predicate</see> to satisfy.</param>
        public InlineConstraint ( Predicate<TProperty> predicate )
            : this ( predicate, string.Format ( "{0} failed.", predicate ) )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineConstraint&lt;TProperty&gt;"/> class.
        /// </summary>
        /// <param name="predicate"><see cref="Predicate{T}">Predicate</see> to satisfy.</param>
        /// <param name="message">Message to show if constraint is not met.</param>
        public InlineConstraint ( Predicate<TProperty> predicate, string message )
            : base ( message )
        {
            Check.IsNotNull ( predicate, "predicate is required" );

            _predicate = predicate;
        }

        /// <inheritdoc/>
        public override bool IsCompliant(object propertyValue, IRuleEngineContext ruleEngineContext)
        {
            if ( propertyValue != null && !( propertyValue is TProperty ) )
            {
                throw new ArgumentException ( string.Format ( "Property value is not of the correct type ({0}).", typeof( TProperty ) ) );
            }

            Message = Message.FormatRuleEngineMessage ( new Dictionary<string, string> { { "PropertyValue", propertyValue == null ? string.Empty : propertyValue.ToString () } } );

            var isCompliant = _predicate ( ( TProperty )propertyValue );

            return isCompliant;
        }
    }
}
