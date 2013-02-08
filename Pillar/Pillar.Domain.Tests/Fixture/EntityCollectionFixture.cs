using System.Collections.Generic;

namespace Pillar.Domain.Tests.Fixture
{
    public class CollectionItemItem : AbstractAggregateNode
    {
        public CollectionItemItem ( CollectionItem collectionItem )
        {
            CollectionItem = collectionItem;
        }

        public CollectionItem CollectionItem { get; set; }

        #region Overrides of AbstractAggregateNode

        public override IAggregateRoot AggregateRoot
        {
            get { return CollectionItem.AggregateRoot; }
        }

        #endregion
    }

    public class CollectionItem : AbstractAggregateNode
    {
        private readonly IList<CollectionItemItem> _collectionItemItems;
        private bool _propertyValue;

        public CollectionItem ( EntityCollectionFixture entityCollectionFixture, bool propertyValue )
        {
            _collectionItemItems = new List<CollectionItemItem> ();
            EntityCollectionFixture = entityCollectionFixture;
            _propertyValue = propertyValue;
        }

        public EntityCollectionFixture EntityCollectionFixture { get; set; }

        public bool PropertyValue
        {
            get { return _propertyValue; }
            set { ApplyPropertyChange ( ref _propertyValue, () => PropertyValue, value ); }
        }

        public virtual IEnumerable<CollectionItemItem> CollectionItemItems
        {
            get { return _collectionItemItems; }
        }

        public CollectionItemItem CreateCollectionItemItem ()
        {
            var itemItem = new CollectionItemItem ( this );
            _collectionItemItems.Add ( itemItem );
            NotifyItemAdded ( () => CollectionItemItems, itemItem );
            return itemItem;
        }

        public void RemoveCollectionItemItem ( CollectionItemItem itemItem )
        {
            _collectionItemItems.Remove ( itemItem );
            NotifyItemRemoved ( () => CollectionItemItems, itemItem );
        }

        #region Overrides of AbstractAggregateNode

        public override IAggregateRoot AggregateRoot
        {
            get { return EntityCollectionFixture; }
        }

        #endregion
    }

    public class EntityCollectionFixture : SimpleAggregateRoot
    {
        private readonly IList<CollectionItem> _collectionItems;

        public EntityCollectionFixture ()
        {
            _collectionItems = new List<CollectionItem> ();
        }

        public virtual IEnumerable<CollectionItem> CollectionItems
        {
            get { return _collectionItems; }
        }

        public CollectionItem CreateCollectionItem ( bool propertyValue )
        {
            var item = new CollectionItem ( this, propertyValue );
            _collectionItems.Add ( item );
            NotifyItemAdded ( () => CollectionItems, item );
            return item;
        }

        public void RemoveCollectionItem ( CollectionItem item )
        {
            _collectionItems.Remove ( item );
            NotifyItemRemoved ( () => CollectionItems, item );
        }
    }
}