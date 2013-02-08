using System;
using System.Linq.Expressions;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;

namespace Pillar.Domain
{
    /// <summary>
    /// AbstractAggregateRoot provides a base aggregrate root implementation.
    /// </summary>
    public abstract class AbstractAggregateRoot : Entity, IAggregateRoot
    {
        #region Constants and Fields

        private bool _servicesInitialized;

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [aggregate changed].
        /// </summary>
        public virtual event EventHandler<AggregateChangedEventArgs> AggregateChanged = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Pillar IoC container.
        /// </summary>
        [IgnoreMapping]
        protected virtual IContainer Container
        {
            get
            {
                IContainer container = null;

                try
                {
                    container = IoC.CurrentContainer;
                }
                catch ( NullReferenceException )
                {
                    throw new AggregateException ( "Pillar IoC container is not initialized." );
                }

                return container;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Notifies that the item was added.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="newItem">The new item.</param>
        public virtual void NotifyItemAdded<TProperty> (
            IEntity sourceEntity,
            Expression<Func<TProperty>> propertyExpression,
            object newItem )
        {
            InitializeServices ();

            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            var aggregateChangedEventArgs = new AggregateChangedEventArgs (
                this, sourceEntity, propertyName, AggregateChangedType.CollectionItemAdded, newItem );
            AggregateChanged ( this, aggregateChangedEventArgs );
        }

        /// <summary>
        /// Notifies that the item was removed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="oldItem">The old item.</param>
        public virtual void NotifyItemRemoved<TProperty> (
            IEntity sourceEntity,
            Expression<Func<TProperty>> propertyExpression,
            object oldItem )
        {
            InitializeServices ();

            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            var aggregateChangedEventArgs = new AggregateChangedEventArgs (
                this, sourceEntity, propertyName, AggregateChangedType.CollectionItemRemoved, oldItem );
            AggregateChanged ( this, aggregateChangedEventArgs );
        }

        /// <summary>
        /// Notifies that the property changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public virtual void NotifyPropertyChanged<TProperty> (
            IEntity sourceEntity,
            Expression<Func<TProperty>> propertyExpression,
            object oldValue,
            object newValue )
        {
            InitializeServices ();

            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            var aggregateChangedEventArgs = new AggregateChangedEventArgs (
                this, sourceEntity, propertyName, oldValue, newValue );
            AggregateChanged ( this, aggregateChangedEventArgs );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the services.
        /// </summary>
        protected internal virtual void InitializeServices ()
        {
            // For performance reasons we want to short circuit this method if it has already been called.
            if ( _servicesInitialized )
            {
                return;
            }

            if ( Container == null )
            {
                throw new AggregateException ( "Pillar IoC container is not set up." );
            }

            _servicesInitialized = true;
        }

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
            NotifyItemAdded ( this, propertyExpression, newItem );
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
            NotifyItemRemoved ( this, propertyExpression, oldItem );
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
            NotifyPropertyChanged ( this, propertyExpression, oldValue, newValue );
        }

        #endregion
    }
}
