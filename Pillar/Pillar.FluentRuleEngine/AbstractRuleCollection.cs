using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.Rules;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Abstract class used when Createing an <see cref="IRuleCollection{TSubject}"/>
    /// </summary>
    /// <typeparam name="TSubject">Type of subject Rule Colleciton is for.</typeparam>
    public abstract class AbstractRuleCollection<TSubject> : IRuleCollection<TSubject>
    {
        #region Constants and Fields

        private readonly List<IRule> _rules = new List<IRule> ();

        private Predicate<IRuleEngineContext> _shouldRunClauseToAdd;

        private bool? _internalAutoValidatePropertyRules;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to auto validate property rules.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [auto validate]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoValidatePropertyRules { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets an Enumerator fo the Rule Collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{IRule}"/></returns>
        public IEnumerator<IRule> GetEnumerator ()
        {
            return _rules.GetEnumerator ();
        }

        /// <summary>
        /// Creates and adds a New Property Rule to the Collection.
        /// </summary>
        /// <typeparam name="TProperty">Type of property the rule will be set to. Must be <see cref="IPropertyRule"/></typeparam>
        /// <param name="propertyExpression">Expression to property rule will be set to.</param>
        /// <returns>A <see cref="IPropertyRuleBuilderInitializer{TContext,TSubject}"/></returns>
        public IPropertyRuleBuilderInitializer<RuleEngineContext<TSubject>, TSubject> NewPropertyRule<TProperty> (
            Expression<Func<TProperty>> propertyExpression ) where TProperty : IPropertyRule
        {
            Check.IsNotNull ( propertyExpression, "propertyExpression is required." );
            var name = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return new PropertyRuleBuilderInitializer<RuleEngineContext<TSubject>, TSubject> (
                name,
                rule =>
                    {
                        if ( _shouldRunClauseToAdd != null )
                        {
                            rule.AddShouldRunClause ( _shouldRunClauseToAdd );
                        }
                        AddRule ( rule );

                        rule.AutoValidate = _internalAutoValidatePropertyRules.HasValue ? _internalAutoValidatePropertyRules.Value : AutoValidatePropertyRules;
                    } );
        }

        /// <summary>
        /// Creates and adds a New Collection Property Rule to the Collection.
        /// </summary>
        /// <typeparam name="TProperty">Type of property the rule will be set to. Must be <see cref="ICollectionPropertyRule"/></typeparam>
        /// <param name="propertyExpression">Expression to property rule will be set to.</param>
        /// <returns>A <see cref="ICollectionPropertyRuleBuilderInitializer{TContext,TSubject}"/></returns>
        public ICollectionPropertyRuleBuilderInitializer<RuleEngineContext<TSubject>, TSubject> NewCollectionPropertyRule<TProperty> (
            Expression<Func<TProperty>> propertyExpression ) where TProperty : ICollectionPropertyRule
        {
            Check.IsNotNull ( propertyExpression, "propertyExpression is required." );
            var name = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return new CollectionPropertyRuleBuilderInitializer<RuleEngineContext<TSubject>, TSubject> (
                name,
                rule =>
                    {
                        if ( _shouldRunClauseToAdd != null )
                        {
                            rule.AddShouldRunClause ( _shouldRunClauseToAdd );
                        }
                        AddRule ( rule );
                    } );
        }

        /// <summary>
        /// Creates and adds a New Rule to the Collection.
        /// </summary>
        /// <typeparam name="TProperty">Type of property the rule will be set to. Must be <see cref="IRule"/></typeparam>
        /// <param name="propertyExpression">Expression to property the rule will be set to.</param>
        /// <returns>A <see cref="IRuleBuilderInitializer{TContext,TSubject}"/></returns>
        public IRuleBuilderInitializer<RuleEngineContext<TSubject>, TSubject> NewRule<TProperty> ( Expression<Func<TProperty>> propertyExpression )
            where TProperty : IRule
        {
            Check.IsNotNull ( propertyExpression, "propertyExpression is required." );
            var name = PropertyUtil.ExtractPropertyName ( propertyExpression );

            var rule = new Rule ( name );
            if ( _shouldRunClauseToAdd != null )
            {
                rule.AddShouldRunClause ( _shouldRunClauseToAdd );
            }
            AddRule ( rule );
            return new RuleBuilder<RuleEngineContext<TSubject>, TSubject> ( rule );
        }

        /// <summary>
        /// Creates and adds a New Specification Rule to the Collection.
        /// </summary>
        /// <typeparam name="TProperty">Type of property the rule will be set to. Must be <see cref="ISpecificationRule"/></typeparam>
        /// <param name="propertyExpression">Expression to property rule will be set to.</param>
        /// <returns>A <see cref="ISpecificationRuleBuilderInitializer{TContext,TSubject}"/></returns>
        public ISpecificationRuleBuilderInitializer<RuleEngineContext<TSubject>, TSubject> NewSpecificationRule<TProperty> (
            Expression<Func<TProperty>> propertyExpression ) where TProperty : IRule
        {
            Check.IsNotNull ( propertyExpression, "propertyExpression is required." );
            var name = PropertyUtil.ExtractPropertyName ( propertyExpression );

            return new SpecificationRuleBuilderInitializer<RuleEngineContext<TSubject>, TSubject> (
                name,
                rule =>
                    {
                        if ( _shouldRunClauseToAdd != null )
                        {
                            rule.AddShouldRunClause ( _shouldRunClauseToAdd );
                        }
                        AddRule ( rule );
                    } );
        }

        /// <summary>
        /// Creates a new <see cref="RuleSet"/> for the Collection.
        /// </summary>
        /// <typeparam name="TProperty">Type of property the rule set will be set to. Must be <see cref="IRuleSet"/></typeparam>
        /// <param name="propertyExpression">Expression to the property the rule set will be set to.</param>
        /// <param name="rules">Rules to add to the Rule Set.</param>
        public void NewRuleSet<TProperty> ( Expression<Func<TProperty>> propertyExpression, params IRule[] rules ) where TProperty : IRuleSet
        {
            Check.IsNotNull ( propertyExpression, "propertyExpression is required." );
            var ruleSet = new RuleSet ( PropertyUtil.ExtractPropertyName ( propertyExpression ), rules );
            var propertyInfo = GetType ().GetProperty ( ruleSet.Name );
            Check.IsNotNull (
                propertyInfo, string.Format ( "There must be a property with the given rule name in this collection: {0}", ruleSet.Name ) );
            propertyInfo.SetValue (
                this,
                ruleSet,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                null,
                null,
                CultureInfo.CurrentCulture );
        }

        /// <summary>
        /// Adds rules from another collection to this collection.
        /// </summary>
        /// <param name="ruleCollection">RuleCollection to get rules from.</param>
        public void AddRules ( IRuleCollection<TSubject> ruleCollection )
        {
            foreach ( var rule in ruleCollection )
            {
                AddRule ( rule, false );
            }
        }

        /// <summary>
        /// Adds a ShouldRunClause to the rules added in the <see cref="Action"/>.
        /// </summary>
        /// <param name="shouldRunClause"><see cref="Predicate{T}"/> when rule should run.</param>
        /// <param name="action"><see cref="Action"/> that creates the rules.</param>
        public void ShouldRunWhen ( Predicate<TSubject> shouldRunClause, Action action )
        {
            Predicate<IRuleEngineContext> shouldRunClauseWrapper = ctx => shouldRunClause ( ( TSubject )ctx.Subject );
            ShouldRunWhen ( shouldRunClauseWrapper, action );
        }

        /// <summary>
        /// Adds a ShouldRunClause to the rules added in the <see cref="Action"/>.
        /// </summary>
        /// <param name="shouldRunClause"><see cref="Func{TSubject,TContext,TResult}"/> when rule should run.</param>
        /// <param name="action"><see cref="Action"/> that creates the rules.</param>
        public void ShouldRunWhen ( Func<TSubject, RuleEngineContext<TSubject>, bool> shouldRunClause, Action action )
        {
            Predicate<IRuleEngineContext> shouldRunClauseWrapper =
                ctx => shouldRunClause ( ( TSubject )ctx.Subject, ( RuleEngineContext<TSubject> )ctx );
            ShouldRunWhen ( shouldRunClauseWrapper, action );
        }

        private void ShouldRunWhen ( Predicate<IRuleEngineContext> shouldRunClause, Action action )
        {
            _shouldRunClauseToAdd = shouldRunClause;
            action ();
            _shouldRunClauseToAdd = null;
        }

        /// <summary>
        /// Sets Auto Validation to true for all property rules created in Action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void AutoValidateAllPropertyRules( Action action)
        {
            _internalAutoValidatePropertyRules = true;
            action ();
            _internalAutoValidatePropertyRules = null;
        }

        /// <summary>
        /// Sets Auto Validation to false for all property rules created in Action.
        /// </summary>
        /// <param name="action">The action.</param>
        public void DoNotAutoValidateAllPropertyRules( Action action )
        {
            _internalAutoValidatePropertyRules = false;
            action();
            _internalAutoValidatePropertyRules = null;
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        #endregion

        #region Methods

        private void AddRule ( IRule rule )
        {
            AddRule ( rule, true );
        }

        private void AddRule ( IRule rule, bool setProperty )
        {
            if ( setProperty )
            {
                var ruleType = rule.GetType ();
                var propertyInfo = GetType ().GetProperty ( rule.Name );
                Check.IsNotNull (
                    propertyInfo, string.Format ( "There must be a property with the given rule name in this collection: {0}", rule.Name ) );
                propertyInfo.SetValue (
                    this,
                    rule,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                    null,
                    null,
                    CultureInfo.CurrentCulture );
            }
            var ruleNameExists = _rules.FirstOrDefault ( r => r.Name == rule.Name ) != null;
            if ( ruleNameExists )
            {
                throw new InvalidRuleException ( string.Format ( "Rule with name {0} already exists.", rule.Name ) );
            }
            _rules.Add ( rule );
        }

        #endregion
    }
}
