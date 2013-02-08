using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Resources;
using Pillar.FluentRuleEngine.RuleSelectors;
using Pillar.FluentRuleEngine.Rules;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Extension methods for Pillar.FluentRuleEngine
    /// </summary>
    public static class RuleExtensions
    {
        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> to a rule.
        /// </summary>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="predicate"><see cref="Predicate{T}"/> to use in the inline constraint.</param>
        /// <param name="message">Rule Violation Message for Constraint.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/>.</returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> Constrain<TSubject, TContext, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, Predicate<TProperty> predicate, string message = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var inlineConstraint = new InlineConstraint<TProperty> ( predicate, message );
            propertyRuleBuilder.Constrain ( inlineConstraint );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> to a rule.
        /// </summary>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="ruleCollection">Rule Collection for property.</param>
        /// <param name="ruleSelector">Optional Rule Selector for <paramref name="ruleCollection"/></param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/>.</returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> ConstrainWithCollection<TSubject, TContext, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            IRuleCollection<TProperty> ruleCollection,
            IRuleSelector ruleSelector = null )
            where TContext : RuleEngineContext<TSubject>
        {
            propertyRuleBuilder.Constrain ( new RuleCollectionConstraint<TProperty> ( ruleCollection, ruleSelector ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="ContextObjectInlineConstraint{TProperty}"/> to a rule.
        /// </summary>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="func"><see cref="Func{TProperty,TProperty,TResult}"/> to use in the context object inline constraint.</param>
        /// <param name="contextObjectName">Optional Name of the ContextObject.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/>.</returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> ContextObjectConstrain<TSubject, TContext, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            Func<TProperty, TProperty, bool> func,
            string contextObjectName = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var inlineConstraint = new ContextObjectInlineConstraint<TProperty> ( func, contextObjectName );
            propertyRuleBuilder.Constrain ( inlineConstraint );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds a Then clause to a Rule the Reports a <see cref="RuleViolation"/>.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{TContext,TSubject}"/> currently configuring the rule.</param>
        /// <param name="message">Message to use in <see cref="RuleViolation"/></param>
        /// <param name="nameDictionary"><see cref="IDictionary{TKey,TValue}"/> to use when formating the <paramref name="message"/></param>
        /// <param name="propertyNames">Params of propertyNames that caused the rule violation.</param>
        /// <returns>A <see cref="IRuleBuilder{TContext,TSubject}"/>.</returns>
        public static IRuleBuilder<TContext, TSubject> ThenReportRuleViolation<TContext, TSubject> (
            this IRuleBuilder<TContext, TSubject> ruleBuilder,
            string message,
            IDictionary<string, string> nameDictionary = null,
            params string[] propertyNames ) where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Then (
                ( s, ctx ) =>
                    {
                        var formattedMessage = message.FormatRuleEngineMessage ( nameDictionary );
                        ctx.RuleViolationReporter.Report ( new RuleViolation ( ruleBuilder.Rule, s, formattedMessage, propertyNames ) );
                    } );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds a Then clause to a Rule the Reports a <see cref="RuleViolation"/>.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IRuleBuilder{TContext,TSubject}"/> currently configuring the rule.</param>
        /// <param name="getMessageFunc"><see cref="Func{TSubject,TContext,TResult}"/> to get the Message to use in <see cref="RuleViolation"/>.</param>
        /// <param name="nameDictionary"><see cref="IDictionary{TKey,TValue}"/> to use when formating the message.</param>
        /// <param name="propertyNames">Params of propertyNames that caused the rule violation.</param>
        /// <returns>A <see cref="IRuleBuilder{TContext,TSubject}"/>.</returns>
        public static IRuleBuilder<TContext, TSubject> ThenReportRuleViolation<TContext, TSubject> (
            this IRuleBuilder<TContext, TSubject> ruleBuilder,
            Func<TSubject, TContext, string> getMessageFunc,
            IDictionary<string, string> nameDictionary = null,
            params string[] propertyNames ) where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Then (
                ( s, ctx ) =>
                    {
                        var formattedMessage = getMessageFunc ( s, ctx ).FormatRuleEngineMessage ( nameDictionary );
                        ctx.RuleViolationReporter.Report ( new RuleViolation ( ruleBuilder.Rule, s, formattedMessage, propertyNames ) );
                    } );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds a Then clause to a Rule the Reports a <see cref="RuleViolation"/>.
        /// </summary>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="message">Message to use in <see cref="RuleViolation"/></param>
        /// <param name="nameDictionary"><see cref="IDictionary{TKey,TValue}"/> to use when formating the <paramref name="message"/></param>
        /// <param name="propertyNames">Params of propertyNames that caused the rule violation.</param>
        /// <returns>
        /// A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/>.
        /// </returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> ThenReportRuleViolation<TSubject, TContext, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            string message,
            IDictionary<string, string> nameDictionary = null,
            params string[] propertyNames ) where TContext : RuleEngineContext<TSubject>
        {
            propertyRuleBuilder.Then (
                ( s, ctx ) =>
                    {
                        var formattedMessage =
                            message.FormatRuleEngineMessage (
                                ctx.NameProvider.GetName (
                                    s, ( Expression<Func<TSubject, TProperty>> )propertyRuleBuilder.PropertyRule.PropertyExpression ),
                                nameDictionary );
                        ctx.RuleViolationReporter.Report(
                            new RuleViolation(propertyRuleBuilder.PropertyRule, s, formattedMessage, propertyNames));
                    } );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Disables a <see cref="IRule">Rule</see>.
        /// </summary>
        /// <param name="rule">Rule to disable.</param>
        public static void Disable ( this IRule rule )
        {
            rule.IsDisabled = true;
        }

        /// <summary>
        /// Adds rules that will only run if there are no rule violations.
        /// </summary>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <param name="abstractRuleCollection">The abstract rule collection.</param>
        /// <param name="action">The action that adds the rules.</param>
        public static void ShouldRunWhenNoViolations<TSubject> ( this AbstractRuleCollection<TSubject> abstractRuleCollection, Action action )
        {
            abstractRuleCollection.ShouldRunWhen ( ( p, ctx ) => !ctx.RuleViolationReporter.HasRuleViolation, action );
        }

        /// <summary>
        /// Adds a attribute to the rule with the key "PropertyChain".
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="IRuleBuilderInitializer{TContext,TSubject}"/>.</returns>
        public static IRuleBuilderInitializer<TContext, TSubject> RunForProperty<TContext, TSubject, TProperty>(
            this IRuleBuilderInitializer<TContext, TSubject> ruleBuilder,
            Expression<Func<TSubject, TProperty>> propertyExpression) 
            where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.AddAttribute ( "PropertyChain", PropertyChain.FromLambdaExpression ( propertyExpression ) );
            return ruleBuilder;
        }
    }
}
