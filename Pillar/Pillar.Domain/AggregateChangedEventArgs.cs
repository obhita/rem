using System;

namespace Pillar.Domain
{
    /// <summary>
    /// Type of aggregate change.
    /// </summary>
    public enum AggregateChangedType
    {
        /// <summary>
        /// Property Value Changed
        /// </summary>
        PropertyValueChanged,

        /// <summary>
        /// Collection Item Added
        /// </summary>
        CollectionItemAdded,

        /// <summary>
        /// Collection Item Removed
        /// </summary>
        CollectionItemRemoved
    }

    /// <summary>
    /// AggregateChangedEventArgs class.
    /// </summary>
    public class AggregateChangedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public AggregateChangedEventArgs (
            IAggregateRoot aggregateRoot,
            IEntity entity,
            string propertyName,
            object oldValue,
            object newValue )
        {
            if ( aggregateRoot == null )
            {
                throw new ArgumentNullException ( "aggregateRoot" );
            }

            if ( entity == null )
            {
                throw new ArgumentNullException ( "entity" );
            }

            if ( string.IsNullOrWhiteSpace ( propertyName ) )
            {
                throw new ArgumentNullException ( "propertyName" );
            }

            AggregateChangedType = AggregateChangedType.PropertyValueChanged;
            AggregateRoot = aggregateRoot;
            SourceEntity = entity;
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="aggregateRoot">The aggregate root.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="aggregateChangedType">Type of the aggregate changed.</param>
        /// <param name="collectionItem">The collection item.</param>
        public AggregateChangedEventArgs (
            IAggregateRoot aggregateRoot,
            IEntity entity,
            string propertyName,
            AggregateChangedType aggregateChangedType,
            object collectionItem )
        {
            if ( aggregateRoot == null )
            {
                throw new ArgumentNullException ( "aggregateRoot" );
            }

            if ( entity == null )
            {
                throw new ArgumentNullException ( "entity" );
            }

            if ( string.IsNullOrWhiteSpace ( propertyName ) )
            {
                throw new ArgumentNullException ( "propertyName" );
            }

            if ( aggregateChangedType == AggregateChangedType.PropertyValueChanged )
            {
                throw new ArgumentException ( "This constructor is for collection changes only." );
            }

            if ( collectionItem == null )
            {
                throw new ArgumentNullException ( "collectionItem" );
            }

            AggregateChangedType = aggregateChangedType;
            AggregateRoot = aggregateRoot;
            SourceEntity = entity;
            PropertyName = propertyName;

            if ( AggregateChangedType == AggregateChangedType.CollectionItemAdded )
            {
                NewValue = collectionItem;
            }
            else
            {
                OldValue = collectionItem;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the type of the aggregate changed.
        /// </summary>
        public AggregateChangedType AggregateChangedType { get; private set; }

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public IAggregateRoot AggregateRoot { get; private set; }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        public object NewValue { get; private set; }

        /// <summary>
        /// Gets the old value.
        /// </summary>
        public object OldValue { get; private set; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Gets the source entity.
        /// </summary>
        public IEntity SourceEntity { get; private set; }

        #endregion
    }
}
