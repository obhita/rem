using System;
using System.Linq.Expressions;

namespace Pillar.Domain
{
    /// <summary>
    /// AbstractAggregateNode defines a base implementation for a relationship with an aggregate root.
    /// </summary>
    public abstract class AbstractAggregateNode : Entity, IAggregateNode
    {
        #region Public Properties

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public abstract IAggregateRoot AggregateRoot { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Applies the property change.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="value">The value.</param>
        protected virtual void ApplyPropertyChange<TProperty, TField> (
            ref TField field,
            Expression<Func<TProperty>> propertyExpression,
            TField value )
        {
            object oldValue = field;

            if ( !Equals ( field, value ) )
            {
                field = value;

                NotifyPropertyChanged ( propertyExpression, oldValue, value );
            }
        }

        /// <summary>
        /// Notifies the item added.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="newItem">The new item.</param>
        protected virtual void NotifyItemAdded<TProperty> (
            Expression<Func<TProperty>> propertyExpression,
            object newItem )
        {
            AggregateRoot.NotifyItemAdded ( this, propertyExpression, newItem );
        }

        /// <summary>
        /// Notifies the item removed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="oldItem">The old item.</param>
        protected virtual void NotifyItemRemoved<TProperty> (
            Expression<Func<TProperty>> propertyExpression,
            object oldItem )
        {
            AggregateRoot.NotifyItemRemoved ( this, propertyExpression, oldItem );
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected virtual void NotifyPropertyChanged<TProperty> (
            Expression<Func<TProperty>> propertyExpression,
            object oldValue,
            object newValue )
        {
            AggregateRoot.NotifyPropertyChanged ( this, propertyExpression, oldValue, newValue );
        }

        #endregion
    }
}
