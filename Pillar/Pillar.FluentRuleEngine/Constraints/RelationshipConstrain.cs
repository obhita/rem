using System;
using Pillar.Common.Utility;

namespace Pillar.FluentRuleEngine.Constraints
{
    /// <summary>
    /// A <see cref="IConstraint">Constraint</see> that requires object to be satisfy relationship with other objects in the subject.
    /// </summary>
    /// <typeparam name="TProperty">Type of property.</typeparam>
    /// <typeparam name="TSubject">Type of subject.</typeparam>
    public class RelationshipConstrain<TProperty, TSubject> : ConstraintBase
    {
        private readonly Func<TProperty, TSubject, bool> _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelationshipConstrain&lt;TProperty, TSubject&gt;"/> class.
        /// </summary>
        /// <param name="predicate"><see cref="Predicate{T}">Predicate</see> to satisfy.</param>
        public RelationshipConstrain(Func<TProperty, TSubject, bool> predicate)
            : this ( predicate, string.Format ( "{0} failed.", predicate ) )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RelationshipConstrain&lt;TProperty, TSubject&gt;"/> class.
        /// </summary>
        /// <param name="predicate"><see cref="Predicate{T}">Predicate</see> to satisfy.</param>
        /// /// <param name="message">Message to show if constraint is not met.</param>
        public RelationshipConstrain ( Func<TProperty, TSubject, bool> predicate, string message )
            : base ( message )
        {
            Check.IsNotNull(predicate, "Predicate is required.");

            _predicate = predicate;
        }

        /// <inheritdoc/>
        public override bool IsCompliant(object propertyValue, IRuleEngineContext ruleEngineContext)
        {
            if (propertyValue != null && !(propertyValue is TProperty))
            {
                throw new ArgumentException(string.Format("Property value is not of the correct type ({0}).", typeof(TProperty)));
            }

            if (ruleEngineContext.Subject != null && !(ruleEngineContext.Subject is TSubject))
            {
                throw new ArgumentException(string.Format("Subject in the rule engine context is not of the correct type ({0}).", typeof(TSubject)));
            }

            var isCompliant = _predicate((TProperty)propertyValue, (TSubject)(ruleEngineContext.Subject));

            return isCompliant;
        }
    }
}