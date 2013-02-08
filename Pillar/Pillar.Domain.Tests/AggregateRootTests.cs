using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Tests;
using Pillar.Domain.Tests.Fixture;

namespace Pillar.Domain.Tests
{
    [TestClass]
    public class AggregateRootTests : DomainTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void InitializeServices_WhenServiceLocatorIsNotSet_ThrowsException()
        {
            ServiceLocatorUnitTestHelper.SetThreadLocalServiceLocator ( null );
            var root = new SimpleAggregateRoot();
            root.InitializeServices();
        }

        [TestMethod]
        public void SByteProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.SByteProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.SByteProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void ByteProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.ByteProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.ByteProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void ShortProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.ShortProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.ShortProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void UShortProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.UShortProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.UShortProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void IntProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.IntProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.IntProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void UIntProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.UIntProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.UIntProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void LongProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.LongProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.LongProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void ULongProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.ULongProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.ULongProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void FloatProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.FloatProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.FloatProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void DoubleProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.DoubleProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.DoubleProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void DecimalProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.DecimalProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.DecimalProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void BoolProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();

            allPrimitiveTypes.BoolProperty = false;
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.BoolProperty = true;
            aggregateIsDirty = false;
            allPrimitiveTypes.BoolProperty = true;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void CharProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.CharProperty = 'T';
            aggregateIsDirty = false;
            allPrimitiveTypes.CharProperty = 'T';

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableSByteProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableSByteProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableSByteProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableByteProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableByteProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableByteProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableShortProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableShortProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableShortProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableUShortProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableUShortProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableUShortProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableIntProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableIntProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableIntProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableUIntProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableUIntProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableUIntProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableLongProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableLongProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableLongProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableULongProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableULongProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableULongProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableFloatProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableFloatProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableFloatProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableDoubleProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableDoubleProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableDoubleProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableDecimalProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableDecimalProperty = 100;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableDecimalProperty = 100;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableBoolProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableBoolProperty = true;
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableBoolProperty = true;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NullableCharProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.NullableCharProperty = 'T';
            aggregateIsDirty = false;
            allPrimitiveTypes.NullableCharProperty = 'T';

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void StringProperty_ChangingPropertyTwice_FiresOnAggregateChangedEventOneTimeOnly()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.StringProperty = "test";
            aggregateIsDirty = false;
            allPrimitiveTypes.StringProperty = "test";

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void StringProperty_ChangingPropertyToNullWhenItIsAlreadyNull_DoesNotFireAggregateChanged()
        {
            bool aggregateIsDirty = false;
            var allPrimitiveTypes = new AllPrimitiveTypes();
            allPrimitiveTypes.AggregateChanged += (obj, eventArgs) => { aggregateIsDirty = true; };

            allPrimitiveTypes.StringProperty = null;

            Assert.IsFalse(aggregateIsDirty);
        }

        [TestMethod]
        public void NotifyItemAdded_ItemAdded_EntityFiresAggregateChangedEvent()
        {
            var collectionEntity = new EntityCollectionFixture();

            AggregateChangedEventArgs args = null;

            collectionEntity.AggregateChanged += (obj, eventArgs) => { args = eventArgs; };
            var item = collectionEntity.CreateCollectionItem(true);

            AssertItemAdded(args, item);
        }

        private static void AssertItemAdded(AggregateChangedEventArgs args, CollectionItem item)
        {
            Assert.IsNotNull(args);
            Assert.AreEqual(args.AggregateChangedType, AggregateChangedType.CollectionItemAdded);
            Assert.AreEqual(args.PropertyName, "CollectionItems");
            Assert.AreSame(args.NewValue, item);
            Assert.IsNull(args.OldValue);
        }

        [TestMethod]
        public void NotifyItemRemoved_ItemRemoved_EntityFiresAggregateChangedEvent()
        {
            var collectionEntity = new EntityCollectionFixture();
            var item = collectionEntity.CreateCollectionItem(true);

            AggregateChangedEventArgs args = null;

            collectionEntity.AggregateChanged += (obj, eventArgs) => { args = eventArgs; };
            collectionEntity.RemoveCollectionItem(item);

            AssertItemRemoved(args, item);
        }

        private static void AssertItemRemoved(AggregateChangedEventArgs args, CollectionItem item)
        {
            Assert.IsNotNull(args);
            Assert.AreEqual(args.AggregateChangedType, AggregateChangedType.CollectionItemRemoved);
            Assert.AreEqual(args.PropertyName, "CollectionItems");
            Assert.AreSame(args.OldValue, item);
            Assert.IsNull(args.NewValue);
        }
    }
}
