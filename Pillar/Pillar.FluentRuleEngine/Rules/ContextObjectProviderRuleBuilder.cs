using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Resources;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// RuleBuilder that can provide a context object.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <typeparam name="TContextObject">The type of the context object.</typeparam>
    public class ContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject>
        : RuleBuilder<TContext, TSubject>, IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject>
        where TContext : RuleEngineContext<TSubject>
    {
        private readonly List<IConstraint> _constraints = new List<IConstraint> ();
        private readonly string _contextObjectName;
        private readonly Expression<Func<TContextObject, object>> _contextPropertyExpression;
        private readonly bool _isBuildingPropertyRule = true;
        private bool _addedElseThen;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextObjectProviderRuleBuilder&lt;TContext, TSubject, TContextObject&gt;"/> class.
        /// </summary>
        /// <param name="rule">The rule to build.</param>
        /// <param name="contextObjectName">Name of the context object.</param>
        public ContextObjectProviderRuleBuilder ( Rule rule, string contextObjectName = null )
            : this ( rule, contextObjectName, o => o )
        {
            _isBuildingPropertyRule = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextObjectProviderRuleBuilder&lt;TContext, TSubject, TContextObject&gt;"/> class.
        /// </summary>
        /// <param name="rule">The rule to build.</param>
        /// <param name="contextManipulator">The context manipulator.</param>
        public ContextObjectProviderRuleBuilder ( Rule rule, Expression<Func<TContextObject, object>> contextManipulator )
            : this ( rule, null, contextManipulator )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextObjectProviderRuleBuilder&lt;TContext, TSubject, TContextObject&gt;"/> class.
        /// </summary>
        /// <param name="rule">The rule to build.</param>
        /// <param name="contextObjectName">Name of the context object.</param>
        /// <param name="contextPropertyExpression">The context manipulator.</param>
        public ContextObjectProviderRuleBuilder (
            Rule rule, string contextObjectName, Expression<Func<TContextObject, object>> contextPropertyExpression )
            : base ( rule )
        {
            Check.IsNotNull ( contextPropertyExpression, "Context Manipulator is required." );

            _contextObjectName = contextObjectName;
            _contextPropertyExpression = contextPropertyExpression;
        }

        #region IContextObjectProviderRuleBuilder<TContext,TSubject,TContextObject> Members

        /// <summary>
        /// Gets the type of the context object.
        /// </summary>
        public Type ContextObjectType
        {
            get { return typeof( TContextObject ); }
        }

        /// <summary>
        /// Gets the name of the context object.
        /// </summary>
        public string ContextObjectName
        {
            get { return _contextObjectName; }
        }

        /// <summary>
        /// Gets the constraints.
        /// </summary>
        public IEnumerable<IConstraint> Constraints
        {
            get { return _constraints; }
        }

        /// <summary>
        /// Gets the context object.
        /// </summary>
        /// <param name="workingMemory">The workingMemory to get the context object from.</param>
        /// <returns>
        /// A context object.
        /// </returns>
        public object GetContextObject ( WorkingMemory workingMemory )
        {
            return _contextPropertyExpression.Compile ().Invoke ( workingMemory.GetContextObject<TContextObject> ( _contextObjectName ) );
        }

        /// <summary>
        /// Withes the property.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> WithProperty (
            Expression<Func<TContextObject, object>> propertyExpression )
        {
            if ( _constraints.Count > 0 )
            {
                throw new InvalidRuleException ( "Cannot call WithProperty after a constraint has already been added to the Rule." );
            }
            return new ContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> (
                Rule as Rule,
                ContextObjectName,
                propertyExpression );
        }

        /// <summary>
        /// Constrains the specified constraint.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> Constrain ( IConstraint constraint )
        {
            Check.IsNotNull ( constraint, "constraint is required." );

            _constraints.Add ( constraint );

            CheckAddClauses ();
            return this;
        }

        #endregion

        private void CheckAddClauses ()
        {
            When (
                ( s, ctx ) =>
                    {
                        var whenClauseResult = true;

                        var value = GetContextObject ( ctx.WorkingMemory );
                        var failedConstraints = new List<IConstraint> ();
                        ctx.WorkingMemory.AddContextObject ( failedConstraints, Rule.Name );
                        foreach ( var constraint in Constraints )
                        {
                            var constraintIsCompliant = constraint.IsCompliant ( value, ctx );
                            if (!constraintIsCompliant )
                            {
                                whenClauseResult = false;
                                failedConstraints.Add ( constraint );
                            }
                        }

                        return whenClauseResult;
                    } );
            if (!_addedElseThen )
            {
                _addedElseThen = true;
                ElseThen (
                    ( s, ctx ) =>
                        {
                            var failedConstraints = ctx.WorkingMemory.GetContextObject<List<IConstraint>> ( Rule.Name );
                            foreach ( var constraint in failedConstraints )
                            {
                                if (!( constraint is IHandleAddingRuleViolations ) )
                                {
                                    var contextObject = ctx.WorkingMemory.GetContextObject<TContextObject> ( _contextObjectName );

                                    var propertyName = _isBuildingPropertyRule
                                                              ? ctx.NameProvider.GetName ( contextObject, _contextPropertyExpression )
                                                              : ctx.NameProvider.GetName ( contextObject );

                                    var formatedMessage = constraint.Message.FormatRuleEngineMessage ( propertyName );

                                    var ruleViolation = new RuleViolation (
                                        Rule, contextObject, formatedMessage, PropertyUtil.ExtractPropertyName ( _contextPropertyExpression ) );
                                    ctx.RuleViolationReporter.Report ( ruleViolation );
                                }
                            }
                        } );
            }
        }
    }
}
