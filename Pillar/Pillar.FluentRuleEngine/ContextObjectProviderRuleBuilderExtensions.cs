using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Pillar.Common.Specification;
using Pillar.FluentRuleEngine.Constraints;
using Pillar.FluentRuleEngine.Resources;
using Pillar.FluentRuleEngine.RuleSelectors;
using Pillar.FluentRuleEngine.Rules;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Static class for <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> Extensions.
    /// </summary>
    public static class ContextObjectProviderRuleBuilderExtensions
    {
        /// <summary>
        /// Adds a WhenClause to the rule that checks the context object against a rule collection that returns true if a rule violation occurs from rule collection.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TContextObject">The type of the context object.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="ruleCollection">The rule collection.</param>
        /// <param name="ruleSelector">The rule selector.</param>
        /// <returns>
        /// A <see cref="IRuleBuilder{TContext,TSubject}"/>
        /// </returns>
        public static IRuleBuilder<TContext, TSubject> WithCollection<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder,
            IRuleCollection<TContextObject> ruleCollection,
            IRuleSelector ruleSelector = null )
            where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Constrain ( new RuleCollectionConstraint<TContextObject> ( ruleCollection, ruleSelector ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TProperty}"/> of GreaterThan todays date to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> CannotBeFutureDate<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder )
            where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.GreaterThan ( DateTime.Today );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds a <see cref="NotNullConstraint"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> NotNull<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder ) where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Constrain ( new NotNullConstraint () );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds a <see cref="NullConstraint"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> Null<TContext, TSubject, TContextObject>(
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder) where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Constrain(new NullConstraint());
            return ruleBuilder;
        }

        /// <summary>
        /// Adds a <see cref="NotEmptyConstraint"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> NotEmpty<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder ) where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Constrain ( new NotEmptyConstraint () );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds a <see cref="NotNullOrWhiteSpaceConstraint"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> NotNullOrWhitespace<TContext, TSubject, TContextObject>(
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder) where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Constrain(new NotNullOrWhiteSpaceConstraint());
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of GreaterThan to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> GreaterThan<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, IComparable compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, ">" );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => compareValue.CompareTo ( lhs ) < 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> to the Rule.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TContextObject">The type of the context object.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="message">The message.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> Constrain<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, Predicate<object> predicate, string message )
            where TContext : RuleEngineContext<TSubject>
        {
            ruleBuilder.Constrain ( new InlineConstraint<object> ( predicate, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of GreaterThanOrEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> GreaterThanOrEqualTo<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, IComparable compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, ">=" );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => compareValue.CompareTo ( lhs ) <= 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of LessThen to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> LessThen<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, IComparable compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "<" );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => compareValue.CompareTo ( lhs ) > 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of LessThenOrEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> LessThenOrEqualTo<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, IComparable compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "<=" );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => compareValue.CompareTo ( lhs ) >= 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of EqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> EqualTo<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, IComparable compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "=" );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => compareValue.CompareTo ( lhs ) == 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of NotEqualTo to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="compareValue">Value to compare to value of property.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> NotEqualTo<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, IComparable compareValue )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraints_Comparison_Message.FormatCompareRuleEngineMessage ( compareValue, "!=" );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => compareValue.CompareTo ( lhs ) != 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of InclusiveBetween to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="startValue">Start Value to use in comparison to property value.</param>
        /// <param name="endValue">End Value to use in comparison to property value.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> InclusiveBetween<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder,
            IComparable startValue,
            IComparable endValue ) where TContext : RuleEngineContext<TSubject>
        {
            var message =
                Messages.Constraints_InclusiveBetween_Message.FormatRuleEngineMessage (
                    new Dictionary<string, string> { { "StartValue", startValue.ToString () }, { "EndValue", startValue.ToString () } } );
            ruleBuilder.Constrain (
                new InlineConstraint<object> ( lhs => startValue.CompareTo ( lhs ) <= 0 && endValue.CompareTo ( lhs ) >= 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of ExclusiveBetween to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="startValue">Start Value to use in comparison to property value.</param>
        /// <param name="endValue">End Value to use in comparison to property value.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ExclusiveBetween<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder,
            IComparable startValue,
            IComparable endValue ) where TContext : RuleEngineContext<TSubject>
        {
            var message =
                Messages.Constraints_ExclusiveBetween_Message.FormatRuleEngineMessage (
                    new Dictionary<string, string> { { "StartValue", startValue.ToString () }, { "EndValue", startValue.ToString () } } );
            ruleBuilder.Constrain (
                new InlineConstraint<object> ( lhs => startValue.CompareTo ( lhs ) < 0 && endValue.CompareTo ( lhs ) > 0, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of Regex Match to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="regexString">Regex string to check match on property value.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> MatchesRegex<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, string regexString )
            where TContext : RuleEngineContext<TSubject>
        {
            var message =
                Messages.Constraints_Regex_Message.FormatRuleEngineMessage ( new Dictionary<string, string> { { "RegexString", regexString } } );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => Regex.IsMatch ( lhs.ToString (), regexString ), message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of Specification to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="specification"><see cref="ISpecification{TEntity}"/> to use in Constraint.</param>
        /// <param name="violationMessage">Violation message to use in Constraint.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> MeetsSpecification<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder,
            ISpecification<object> specification,
            string violationMessage = null ) where TContext : RuleEngineContext<TSubject>
        {
            var message = violationMessage
                             ??
                             Messages.Constraint_Specification_Message.FormatRuleEngineMessage (
                                 new Dictionary<string, string> { { "Specification", specification.ToString () } } );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( specification.IsSatisfiedBy, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of MaxLength to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="maxLength">Max Length property value can be.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> MaxLength<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, int maxLength )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraint_MaxLength_Message.FormatCompareRuleEngineMessage ( maxLength, string.Empty );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => lhs.ToString ().Length <= maxLength, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds an <see cref="InlineConstraint{TContextObject}"/> of MinLength to the Rule.
        /// </summary>
        /// <typeparam name="TContext">Type of <see cref="IRuleEngineContext"/> of the rule.</typeparam>
        /// <typeparam name="TSubject">Type of subject of the rule.</typeparam>
        /// <typeparam name="TContextObject">Type of property of the subject of the rule.</typeparam>
        /// <param name="ruleBuilder"><see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/> currently configuring the rule.</param>
        /// <param name="minLength">Min Length property value can be.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> MinLength<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder, int minLength )
            where TContext : RuleEngineContext<TSubject>
        {
            var message = Messages.Constraint_MinLength_Message.FormatCompareRuleEngineMessage ( minLength, string.Empty );
            ruleBuilder.Constrain ( new InlineConstraint<object> ( lhs => lhs.ToString ().Length >= minLength, message ) );
            return ruleBuilder;
        }

        /// <summary>
        /// Adds a propertyExpression to the Context Object in the Rule Builder.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TContextObject">The type of the context object.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="IContextObjectProviderRuleBuilder{TContext,TSubject,TContextObject}"/></returns>
        public static IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> WithProperty<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder,
            Expression<Func<TContextObject, object>> propertyExpression )
            where TContext : RuleEngineContext<TSubject>
        {
            return new ContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> (
                ruleBuilder.Rule as Rule,
                ruleBuilder.ContextObjectName,
                propertyExpression );
        }
    }
}
