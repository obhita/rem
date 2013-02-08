using System;
using System.Collections.Generic;
using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires property and contextobject of same type to satisfy a Function.
    /// </summary>
    /// <typeparam name="TProperty">Type of property.</typeparam>
    public class ContextObjectInlineConstraint<TProperty> : ConstraintBase
    {
        private readonly string _contextObjectName;
        private readonly Func<TProperty, TProperty, bool> _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextObjectInlineConstraint&lt;TProperty&gt;"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="message">The message.</param>
        public ContextObjectInlineConstraint ( Func<TProperty, TProperty, bool> predicate, string message = null )
            : this ( predicate, null, message )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextObjectInlineConstraint&lt;TProperty&gt;"/> class.
        /// </summary>
        /// <param name="predicate"><see cref="Func{TProperty,TProperty,TResult}">Predicate</see> to meet.</param>
        /// <param name="contextObjectName">Name of contextObject.</param>
        /// <param name="message">Error Message for Constraint.</param>
        public ContextObjectInlineConstraint ( Func<TProperty, TProperty, bool> predicate, string contextObjectName, string message = null )
            : base ( message ?? string.Format ( "{0} failed.", predicate ) )
        {
            _predicate = predicate;
            _contextObjectName = contextObjectName;
        }

        /// <inheritdoc/>
        public override bool IsCompliant ( object propertyValue, IRuleEngineContext ruleEngineContext )
        {
            if (!( propertyValue is TProperty ) )
            {
                throw new ArgumentException ( string.Format ( "Property value is not of the correct type ({0}).", typeof( TProperty ) ) );
            }

            var contextObject = _contextObjectName == null
                                          ? ruleEngineContext.WorkingMemory.GetContextObject<TProperty> ()
                                          : ruleEngineContext.WorkingMemory.GetContextObject<TProperty> ( _contextObjectName );

            Message =
                Message.FormatRuleEngineMessage (
                    new Dictionary<string, string> { { "PropertyValue", propertyValue.ToString () }, { "CompareValue", contextObject.ToString () } } );

            return _predicate ( ( TProperty )propertyValue, contextObject );
        }
    }
}
