using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Pillar.Common.Extension;
using Pillar.Common.Specification;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Resources;
using Pillar.FluentRuleEngine.Rules;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Extension methods for Constraints.
    /// </summary>
    public static class ConstraintExtensions
    {
        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of GreaterThan todays date to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> CannotBeFutureDate<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder )
            where TContext : RuleEngineContext<TSubject>
            where TProperty : IComparable<DateTime>
        {
            var compareValue = DateTime.Today.AddDays ( 1 );
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage(compareValue, "<");
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(lhs => lhs.CompareTo ( compareValue ) < 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds a <see cref="NotNullConstraint"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> NotNull<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder ) where TContext : RuleEngineContext<TSubject>
        {
            propertyRuleBuilder.Constrain ( new NotNullConstraint () );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds a <see cref="NullConstraint"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> Null<TContext, TSubject, TProperty>(
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder) where TContext : RuleEngineContext<TSubject>
        {
            propertyRuleBuilder.Constrain(new NullConstraint());
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds a <see cref="NotEmptyConstraint"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> NotEmpty<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder ) where TContext : RuleEngineContext<TSubject>
        {
            propertyRuleBuilder.Constrain ( new NotEmptyConstraint () );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of GreaterThan to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> GreaterThan<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, TProperty compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, ">" );
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(lhs => Comparer<TProperty>.Default.Compare(compareValue, lhs) < 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of GreaterThanOrEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> GreaterThanOrEqualTo<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, TProperty compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, ">=" );
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(lhs => Comparer<TProperty>.Default.Compare(compareValue, lhs) <= 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of LessThen to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> LessThen<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, TProperty compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "<" );
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(lhs => Comparer<TProperty>.Default.Compare(compareValue, lhs) > 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of LessThenOrEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> LessThenOrEqualTo<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, TProperty compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "<=" );
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(lhs => Comparer<TProperty>.Default.Compare(compareValue, lhs) >= 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of EqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> EqualTo<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, TProperty compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "=" );
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(lhs => Comparer<TProperty>.Default.Compare(compareValue, lhs) == 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of InList to the Rule.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyRuleBuilder">The property rule builder.</param>
        /// <param name="list">The list to check.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> InList<TContext, TSubject, TProperty>(
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, params TProperty[] list)
            where TContext : RuleEngineContext<TSubject>
        {
            Check.IsNotNull ( list, "list is required." );
            var message = Messages.Constraints_InList_Message.FormatRuleEngineMessage ( new Dictionary<string, string> {{"ListString", string.Join ( ", ", list )}} );
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(list.Contains, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of NotEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> NotEqualTo<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, TProperty compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "!=" );
            propertyRuleBuilder.Constrain(new InlineConstraint<TProperty>(lhs => Comparer<TProperty>.Default.Compare(compareValue, lhs) != 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of InclusiveBetween to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="startValue">Start Value to use in comparison to property value.</param>
        /// <param name="endValue">End Value to use in comparison to property value.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> InclusiveBetween<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            IComparable startValue,
            IComparable endValue ) where TContext : RuleEngineContext<TSubject>
        {
            var message =
                Messages.Constraints_InclusiveBetween_Message.FormatRuleEngineMessage (
                    new Dictionary<string, string> { { "StartValue", startValue.ToString () }, { "EndValue", startValue.ToString () } } );
            propertyRuleBuilder.Constrain (
                new InlineConstraint<TProperty> ( lhs => startValue.CompareTo ( lhs ) <= 0 && endValue.CompareTo ( lhs ) >= 0, message ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of InclusiveBetween or null to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="startValue">Start Value to use in comparison to property value.</param>
        /// <param name="endValue">End Value to use in comparison to property value.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> InclusiveBetweenOrNull<TContext, TSubject, TProperty>(
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            IComparable startValue,
            IComparable endValue) 
            where TContext : RuleEngineContext<TSubject>
        {
            var message =
                Messages.Constraints_InclusiveBetween_Message.FormatRuleEngineMessage(
                    new Dictionary<string, string> { { "StartValue", startValue.ToString() }, { "EndValue", startValue.ToString() } });
            propertyRuleBuilder.Constrain(
                new InlineConstraint<TProperty>(lhs => (typeof(TProperty).IsNullable () && lhs == null) || ( startValue.CompareTo(lhs) <= 0 && endValue.CompareTo(lhs) >= 0), message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of ExclusiveBetween to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="startValue">Start Value to use in comparison to property value.</param>
        /// <param name="endValue">End Value to use in comparison to property value.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> ExclusiveBetween<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            TProperty startValue,
            TProperty endValue ) where TContext : RuleEngineContext<TSubject>
        {
            var message =
                Messages.Constraints_ExclusiveBetween_Message.FormatRuleEngineMessage (
                    new Dictionary<string, string> { { "StartValue", startValue.ToString () }, { "EndValue", startValue.ToString () } } );
            propertyRuleBuilder.Constrain (
                new InlineConstraint<TProperty>(lhs => Comparer<TProperty>.Default.Compare(startValue, lhs) < 0 && Comparer<TProperty>.Default.Compare(endValue, lhs) > 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of Regex Match to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="regexString">Regex string to check match on property value.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> MatchesRegex<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, string regexString )
            where TContext : RuleEngineContext<TSubject>
        {
            var message =
                Messages.Constraints_Regex_Message.FormatRuleEngineMessage ( new Dictionary<string, string> { { "RegexString", regexString } } );
            propertyRuleBuilder.Constrain ( new InlineConstraint<TProperty> ( lhs => Regex.IsMatch ( lhs.ToString (), regexString ), message ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of Specification to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="specification"><see cref="ISpecification{TEntity}"/> to use in Constraint.</param>
        /// <param name="violationMessage">Violation message to use in Constraint.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> MeetsSpecification<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder,
            ISpecification<TProperty> specification,
            string violationMessage = null ) where TContext : RuleEngineContext<TSubject>
        {
            var message = violationMessage
                             ??
                             Messages.Constraint_Specification_Message.FormatRuleEngineMessage (
                                 new Dictionary<string, string> { { "Specification", specification.ToString () } } );
            propertyRuleBuilder.Constrain ( new InlineConstraint<TProperty> ( specification.IsSatisfiedBy, message ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of MaxLength to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="maxLength">Max Length property value can be.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> MaxLength<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, int maxLength ) where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraint_MaxLength_Message.FormatCompareRuleEngineMessage ( maxLength, string.Empty );
            propertyRuleBuilder.Constrain ( new InlineConstraint<TProperty> ( lhs => lhs.ToString ().Length <= maxLength, message ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of MinLength to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="minLength">Min Length property value can be.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> MinLength<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, int minLength ) where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraint_MinLength_Message.FormatCompareRuleEngineMessage ( minLength, string.Empty );
            propertyRuleBuilder.Constrain ( new InlineConstraint<TProperty> ( lhs => lhs.ToString ().Length >= minLength, message ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="ContextObjectInlineConstraint{TProperty}"/> of GreaterThan to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="contextObjectName">Optional Name of ContextObject.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> GreaterThanContextObject<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, string contextObjectName = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( ">" );
            propertyRuleBuilder.Constrain (
                new ContextObjectInlineConstraint<TProperty>((lhs, ctx) => Comparer<TProperty>.Default.Compare(ctx, lhs) < 0, contextObjectName, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="ContextObjectInlineConstraint{TProperty}"/> of GreaterThanOrEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="contextObjectName">Optional Name of ContextObject.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> GreaterThanOrEqualToContextObject<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, string contextObjectName = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( ">=" );
            propertyRuleBuilder.Constrain(new ContextObjectInlineConstraint<TProperty>((lhs, ctx) => Comparer<TProperty>.Default.Compare(ctx, lhs) <= 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="ContextObjectInlineConstraint{TProperty}"/> of LessThen to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="contextObjectName">Optional Name of ContextObject.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> LessThenContextObject<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, string contextObjectName = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( "<" );
            propertyRuleBuilder.Constrain(new ContextObjectInlineConstraint<TProperty>((lhs, ctx) => Comparer<TProperty>.Default.Compare(ctx, lhs) > 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="ContextObjectInlineConstraint{TProperty}"/> of LessThenOrEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="contextObjectName">Optional Name of ContextObject.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> LessThenOrEqualToContextObject<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, string contextObjectName = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( "<=" );
            propertyRuleBuilder.Constrain(new ContextObjectInlineConstraint<TProperty>((lhs, ctx) => Comparer<TProperty>.Default.Compare(ctx, lhs) >= 0, message));
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="ContextObjectInlineConstraint{TProperty}"/> of EqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="contextObjectName">Optional Name of ContextObject.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> EqualToContextObject<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, string contextObjectName = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( "=" );
            propertyRuleBuilder.Constrain ( new ContextObjectInlineConstraint<TProperty> ( ( lhs, ctx ) => ctx.Equals ( lhs ), message ) );
            return propertyRuleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="ContextObjectInlineConstraint{TProperty}"/> of NotEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TProperty">Type of property of the subject of the rule.</typeparam>
        /// <param name="propertyRuleBuilder"><see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/> currently configuring the rule.</param>
        /// <param name="contextObjectName">Optional Name of ContextObject.</param>
        /// <returns>A <see cref="IPropertyRuleBuilder{TContext,TSubject,TProperty}"/></returns>
        public static IPropertyRuleBuilder<TContext, TSubject, TProperty> NotEqualToContextObject<TContext, TSubject, TProperty> (
            this IPropertyRuleBuilder<TContext, TSubject, TProperty> propertyRuleBuilder, string contextObjectName = null )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( "!=" );
            propertyRuleBuilder.Constrain ( new ContextObjectInlineConstraint<TProperty> ( ( lhs, ctx ) => !ctx.Equals ( lhs ), message ) );
            return propertyRuleBuilder;
        }
    }
}
