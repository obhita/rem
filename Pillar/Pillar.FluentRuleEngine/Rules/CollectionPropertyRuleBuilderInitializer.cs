using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pillar.FluentRuleEngine.Rules
{
    /// <summary>
    /// Builder Initializer for a <see cref="ICollectionPropertyRule">Collection Property Rule</see>Collection property
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    public class CollectionPropertyRuleBuilderInitializer<TContext, TSubject> : ICollectionPropertyRuleBuilderInitializer<TContext, TSubject>
        where TContext : RuleEngineContext<TSubject>
    {
        #region Constants and Fields

        private readonly Action<CollectionPropertyRule> _addRuleCallBack;

        private readonly string _name;

        #endregion

        #region Constructors and Destructors

        internal CollectionPropertyRuleBuilderInitializer ( string name, Action<CollectionPropertyRule> addRuleCallBack )
        {
            _name = name;
            _addRuleCallBack = addRuleCallBack;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public ICollectionPropertyRuleBuilder<TContext, TSubject, TProperty> WithProperty<TProperty> (
            Expression<Func<TSubject, IEnumerable<TProperty>>> propertyExpression )
        {
            var collectionPropertyRule = CollectionPropertyRule.CreateCollectionPropertyRule ( propertyExpression, _name );
            _addRuleCallBack ( collectionPropertyRule );
            return new CollectionPropertyRuleBuilder<TContext, TSubject, TProperty> ( collectionPropertyRule );
        }

        #endregion
    }
}
