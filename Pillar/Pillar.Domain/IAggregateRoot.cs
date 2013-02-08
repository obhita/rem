using System;
using System.Linq.Expressions;

namespace Pillar.Domain
{
    /// <summary>
    /// IAggregateRoot interface.
    /// </summary>
    [IgnoreMapping]
    public interface IAggregateRoot : IEntity
    {
        #region Public Events

        /// <summary>
        /// Occurs when [aggregate changed].
        /// </summary>
        event EventHandler<AggregateChangedEventArgs> AggregateChanged;

        #endregion

        #region Public Methods

        /// <summary>
        /// Notifies the item added.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="newItem">The new item.</param>
        void NotifyItemAdded<TProperty> (
            IEntity sourceEntity,
            Expression<Func<TProperty>> propertyExpression,
            object newItem );

        /// <summary>
        /// Notifies the item removed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="oldItem">The old item.</param>
        void NotifyItemRemoved<TProperty> (
            IEntity sourceEntity,
            Expression<Func<TProperty>> propertyExpression,
            object oldItem );

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        void NotifyPropertyChanged<TProperty> (
            IEntity sourceEntity,
            Expression<Func<TProperty>> propertyExpression,
            object oldValue,
            object newValue );

        #endregion
    }
}
