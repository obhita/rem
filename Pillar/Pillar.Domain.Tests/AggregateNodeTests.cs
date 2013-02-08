using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain.Tests.Fixture;

namespace Pillar.Domain.Tests
{
    [TestClass]
    public class AggregateNodeTests : DomainTestBase
    {
        [TestMethod]
        public void SByteProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var collectionEntity = new EntityCollectionFixture ();
            var item = collectionEntity.CreateCollectionItem ( true );
            collectionEntity.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            aggregateIsDirty = false;
            item.PropertyValue = false;

            Assert.IsTrue(aggregateIsDirty);
        }

        [TestMethod]
        public void NotifyItemAdded_ItemAdded_EntityFiresAggregateChangedEvent()
        {
            var collectionEntity = new EntityCollectionFixture();
            var item = collectionEntity.CreateCollectionItem ( true );

            AggregateChangedEventArgs args = null;

            collectionEntity.AggregateChanged += (obj, eventArgs) => { args = eventArgs; };
            var itemItem = item.CreateCollectionItemItem ();

            AssertItemAdded(args, itemItem);
        }

        private static void AssertItemAdded(AggregateChangedEventArgs args, CollectionItemItem itemItem)
        {
            Assert.IsNotNull(args);
            Assert.AreEqual(args.AggregateChangedType, AggregateChangedType.CollectionItemAdded);
            Assert.AreEqual(args.PropertyName, "CollectionItemItems");
            Assert.AreSame(args.NewValue, itemItem);
            Assert.IsNull(args.OldValue);
        }
        
        [TestMethod]
        public void NotifyItemRemoved_ItemRemoved_EntityFiresAggregateChangedEvent()
        {
            var collectionEntity = new EntityCollectionFixture();
            var item = collectionEntity.CreateCollectionItem(true);
            var itemItem = item.CreateCollectionItemItem ();

            AggregateChangedEventArgs args = null;

            collectionEntity.AggregateChanged += (obj, eventArgs) => { args = eventArgs; };
            item.RemoveCollectionItemItem(itemItem);

            AssertItemRemoved(args, itemItem);
        }

        private static void AssertItemRemoved(AggregateChangedEventArgs args, CollectionItemItem itemItem)
        {
            Assert.IsNotNull(args);
            Assert.AreEqual(args.AggregateChangedType, AggregateChangedType.CollectionItemRemoved);
            Assert.AreEqual(args.PropertyName, "CollectionItemItems");
            Assert.AreSame(args.OldValue, itemItem);
            Assert.IsNull(args.NewValue);
        }
    }
}
