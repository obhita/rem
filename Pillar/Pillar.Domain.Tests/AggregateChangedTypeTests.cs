using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain.Tests.Fixture;

namespace Pillar.Domain.Tests
{
    [TestClass]
    public class AggregateChangedTypeTests
    {
        [TestMethod]
        [ExpectedException ( typeof ( ArgumentNullException ) )]
        public void Constructor_AggregateRootIsNull_ThrowsArgumentNullException ()
        {
            new AggregateChangedEventArgs ( null, new AllPrimitiveTypes (), "Property", 1, 0 );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_EntityRootIsNull_ThrowsArgumentNullException()
        {
            new AggregateChangedEventArgs(new AllPrimitiveTypes(), null, "Property", 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_PropertyNameIsBlank_ThrowsArgumentNullException()
        {
            var entity = new AllPrimitiveTypes();
            new AggregateChangedEventArgs(entity, entity, "", 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_PropertyNameIsNull_ThrowsArgumentNullException()
        {
            var entity = new AllPrimitiveTypes();
            new AggregateChangedEventArgs(entity, entity, null, 1, 0);
        }

        [TestMethod]
        public void Constructor_AllParametersProvided_Succeeds()
        {
            var entity = new AllPrimitiveTypes();
            new AggregateChangedEventArgs(entity, entity, "SomeProp", 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_AddedItemIsNull_ThrowsArgumentNullException()
        {
            var entity = new AllPrimitiveTypes();
            new AggregateChangedEventArgs(
                entity,
                entity,
                "SomeProperty",
                AggregateChangedType.CollectionItemAdded,
                null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_RemovedItemIsNull_ThrowsArgumentNullException()
        {
            var entity = new AllPrimitiveTypes();
            new AggregateChangedEventArgs(
                entity,
                entity,
                "SomeProperty",
                AggregateChangedType.CollectionItemRemoved,
                null);
        }
    }
}