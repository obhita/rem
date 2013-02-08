using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Rule written for the Property of a Subject.
    /// </summary>
    public class PropertyRule : PropertyRuleBase, IPropertyRule
    {
        #region Constants and Fields

        private readonly IList<IConstraint> _constraints;
        private string _actualPropertyName;
        private Func<IRuleEngineContext, string> _getPropertyNameFunc;

        #endregion

        #region Constructors and Destructors

        internal PropertyRule ( LambdaExpression propertyExpression, string name )
            : base ( propertyExpression, name )
        {
            _constraints = new List<IConstraint> ();
            WhenClause = ExecuteWhenClause;
            AddElseThenClause ( ExecuteElseThenClause );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}">constraints</see> of the rule.
        /// </summary>
        /// <inheritdoc/>
        public IEnumerable<IConstraint> Constraints
        {
            get { return _constraints; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to auto validate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if is auto validating; otherwise, <c>false</c>.
        /// </value>
        public bool AutoValidate { get; set; }

        #endregion

        #region Methods

        internal static PropertyRule CreatePropertyRule<T, TProperty> ( Expression<Func<T, TProperty>> propertyExpression, string name )
        {
            var propertyRule = new PropertyRule ( propertyExpression, name )
                {
                    PropertyValueDelegate = propertyExpression.Compile (),
                    _getPropertyNameFunc = ctx => ctx.NameProvider.GetName ( ( T )ctx.Subject, propertyExpression ),
                    _actualPropertyName = PropertyUtil.ExtractPropertyName ( propertyExpression )
                };

            return propertyRule;
        }

        /// <summary>
        /// Adds Constraint to rule.
        /// </summary>
        /// <param name="constraint"><see cref="IConstraint"/> to add to rule.</param>
        protected internal virtual void AddConstraint ( IConstraint constraint )
        {
            Check.IsNotNull ( constraint, "constraint is required." );

            _constraints.Add ( constraint );
        }

        private void ExecuteElseThenClause ( IRuleEngineContext context )
        {
            if (AutoValidate)
            {
                var failedConstraints = context.WorkingMemory.GetContextObject<List<IConstraint>> ( Name );
                foreach ( var constraint in failedConstraints )
                {
                    if (!( constraint is IHandleAddingRuleViolations ) )
                    {
                        var propertyName = _getPropertyNameFunc ( context );
                        var formatedMessage = constraint.Message.FormatRuleEngineMessage ( propertyName );
                        var ruleViolation = new RuleViolation ( this, context.Subject, formatedMessage, _actualPropertyName );
                        context.RuleViolationReporter.Report ( ruleViolation );
                    }
                }
            }
        }

        private bool ExecuteWhenClause ( IRuleEngineContext context )
        {
            var whenClauseResult = true;

            var propertyValue = PropertyValueDelegate.DynamicInvoke ( context.Subject );
            var failedConstraints = new List<IConstraint> ();
            context.WorkingMemory.AddContextObject ( failedConstraints, Name );
            foreach ( var constraint in Constraints )
            {
                var constraintIsCompliant = constraint.IsCompliant ( propertyValue, context );
                if (!constraintIsCompliant )
                {
                    whenClauseResult = false;
                    failedConstraints.Add ( constraint );
                }
            }

            return whenClauseResult;
        }

        #endregion
    }
}
