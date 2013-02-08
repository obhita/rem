using System;
using System.Collections.Generic;
using System.Linq;
using Pillar.Common;
using Pillar.FluentRuleEngine;
using Pillar.FluentRuleEngine.Rules;

namespace Pillar.Domain
{
    /// <summary>
    /// Extension methods for rule engine.
    /// </summary>
    public static class RuleEngineExtensions
    {
        #region Public Methods

        /// <summary>
        /// Checks for duplicates.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="collectionFunc">The collection func.</param>
        /// <returns>A <see cref="IRuleBuilder{TContext,TSubject}"/></returns>
        public static IRuleBuilder<TContext, TSubject> NoDuplicates<TContext, TSubject> (
            this IRuleBuilderInitializer<TContext, TSubject> ruleBuilder, Func<TContext, IEnumerable<TSubject>> collectionFunc )
            where TContext : RuleEngineContext<TSubject>
        {
            return ruleBuilder.When (
                ( s, ctx ) =>
                    {
                        var collection = collectionFunc ( ctx );
                        bool isDuplicate;
                        if ( s is IValuesEquatable )
                        {
                            var aggregateNodeValueObject = ( s as IValuesEquatable );
                            isDuplicate = collection.Any ( item => aggregateNodeValueObject.ValuesEqual ( item ) );
                        }
                        else
                        {
                            isDuplicate = collection.Any ( item => item.Equals ( s ) );
                        }
                        return isDuplicate;
                    } )
                .ThenReportRuleViolation ( ( s, ctx ) => string.Format ( "Cannot not have duplicate {0}.", ctx.NameProvider.GetName ( s ) ) );
        }

        /// <summary>
        /// Adds a WhenClause to the rule checking for duplicates, and Adds A ThenCluase that will report the violation if it occurs.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TSubject">The type of the subject.</typeparam>
        /// <typeparam name="TContextObject">The type of the context object.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="collectionFunc">The collection func.</param>
        /// <returns>A <see cref="IRuleBuilder{TContext,TSubject}"/></returns>
        public static IRuleBuilder<TContext, TSubject> NoDuplicates<TContext, TSubject, TContextObject> (
            this IContextObjectProviderRuleBuilder<TContext, TSubject, TContextObject> ruleBuilder,
            Func<TContext, IEnumerable<TContextObject>> collectionFunc )
            where TContext : RuleEngineContext<TSubject>
        {
            if ( ruleBuilder.Constraints.Count () > 0 )
            {
                throw new InvalidRuleException ( "Cannot use NoDuplicates after a constraint has already been added to the Rule." );
            }

            return ruleBuilder.When (
                ( s, ctx ) =>
                    {
                        var collection = collectionFunc ( ctx );
                        var contextObject = ruleBuilder.GetContextObject ( ctx.WorkingMemory );
                        bool isDuplicate;
                        if ( contextObject is IValuesEquatable )
                        {
                            var aggregateNodeValueObject = ( contextObject as IValuesEquatable );
                            isDuplicate = collection.Any ( item => aggregateNodeValueObject.ValuesEqual ( item ) );
                        }
                        else
                        {
                            isDuplicate = collection.Any ( item => item.Equals ( contextObject ) );
                        }
                        return isDuplicate;
                    } )
                .ThenReportRuleViolation (
                    ( s, ctx ) =>
                    string.Format (
                        "Cannot have duplicate {0}.", ctx.NameProvider.GetName ( ruleBuilder.GetContextObject ( ctx.WorkingMemory ) ) ) );
        }

        #endregion
    }
}
