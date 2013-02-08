using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Collections;
using Pillar.Domain.Primitives;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Tests.Service.Fixtures;

namespace Rem.Infrastructure.Tests.Service
{
    [TestClass]
    public class AggregateNodeCollectionMapperTests
    {
        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void AggregateNodeCollectionMapper_GivenNullDtoCollection_ThrowsArgumentException ()
        {
            new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                null, null, null );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void AggregateNodeCollectionMapper_GivenNullAggregateRoot_ThrowsArgumentException ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();

            new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection.Object, null, null );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void AggregateNodeCollectionMapper_GivenNullAggregateNodeCollection_ThrowsArgumentException ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();

            new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection.Object, aggregateRoot, null );
        }

        [TestMethod]
        public void MapRemovedItem_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new Mock<IEnumerable<AggregateCollectionMapperTestAggregateNodeEntity>> ();
            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection.Object, aggregateRoot, aggregateNodeCollection.Object );

            var resultValue = aggregateNodeCollectionMapper.MapRemovedItem ( null );

            Assert.AreSame ( aggregateNodeCollectionMapper, resultValue );
        }

        [TestMethod]
        public void MapAddedItem_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new Mock<IEnumerable<AggregateCollectionMapperTestAggregateNodeEntity>> ();
            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection.Object, aggregateRoot, aggregateNodeCollection.Object );

            var resultValue = aggregateNodeCollectionMapper.MapAddedItem ( null );

            Assert.AreSame ( aggregateNodeCollectionMapper, resultValue );
        }

        [TestMethod]
        public void MapChangedItem_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new Mock<IEnumerable<AggregateCollectionMapperTestAggregateNodeEntity>> ();
            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection.Object, aggregateRoot, aggregateNodeCollection.Object );

            var resultValue = aggregateNodeCollectionMapper.MapChangedItem ( null );

            Assert.AreSame ( aggregateNodeCollectionMapper, resultValue );
        }

        [TestMethod]
        public void FindCollectionEntity_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new Mock<IEnumerable<AggregateCollectionMapperTestAggregateNodeEntity>> ();
            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection.Object, aggregateRoot, aggregateNodeCollection.Object );

            var resultValue = aggregateNodeCollectionMapper.FindCollectionEntity ( null );

            Assert.AreSame ( aggregateNodeCollectionMapper, resultValue );
        }

        [TestMethod]
        public void Map_GivenNoCurrentAndRemovedDtos_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new Mock<IEnumerable<AggregateCollectionMapperTestAggregateNodeEntity>> ();
            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection.Object );

            bool returnValue = aggregateNodeCollectionMapper.Map ();

            Assert.IsTrue ( returnValue );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void Map_GivenNullActionForRemovedDto_ThrowsArgumentException ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            dtoCollection.RemovedItems.Add ( new AggregateCollectionMapperTestDto () );

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new Mock<IEnumerable<AggregateCollectionMapperTestAggregateNodeEntity>> ();
            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection.Object );

            aggregateNodeCollectionMapper
                .MapRemovedItem ( null )
                .Map ();
        }

        [TestMethod]
        public void Map_GivenRemovedDtosWithNoMatchingKey_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            dtoCollection.RemovedItems.Add ( new AggregateCollectionMapperTestDto () );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity> ();
            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemove )
                .Map ();

            Assert.IsFalse ( returnValue );
            Assert.IsTrue ( dtoCollection.RemovedItems[ 0 ].DataErrorInfoCollection.Count () > 0 );
        }

        [TestMethod]
        public void Map_GivenRemovedDtosWithMatchingKey_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            dtoCollection.RemovedItems.Add ( new AggregateCollectionMapperTestDto () );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemove )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.RemovedItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveMethodCalled > 0 );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsNoop_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            dtoCollection.CurrentItems.Add ( new AggregateCollectionMapperTestDto () );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemove )
                .MapChangedItem ( requestHandler.AggregateNodeChange )
                .MapAddedItem ( requestHandler.AggregateNodeAdd )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeChangeMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeAddMethodCalled == 0 );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsCreate_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Create };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemove )
                .MapChangedItem ( requestHandler.AggregateNodeChange )
                .MapAddedItem ( requestHandler.AggregateNodeAdd )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeChangeMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeAddMethodCalled > 0 );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsUpdate_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Update };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemove )
                .MapChangedItem ( requestHandler.AggregateNodeChange )
                .MapAddedItem ( requestHandler.AggregateNodeAdd )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeChangeMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeAddMethodCalled == 0 );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void Map_GivenWithCurrentDtosAndEditStatusAsDelete_ThrowsArgumentException ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Delete };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemove )
                .MapChangedItem ( requestHandler.AggregateNodeChange )
                .MapAddedItem ( requestHandler.AggregateNodeAdd )
                .Map ();
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsUpdateWithExceptionThrownByChangeMethod_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Update };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemoveWithThrowException )
                .MapChangedItem ( requestHandler.AggregateNodeChangeWithThrowException )
                .MapAddedItem ( requestHandler.AggregateNodeAddWithThrowException )
                .Map ();

            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeAddWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeChangeWithThrowExceptionMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveWithThrowExceptionMethodCalled == 0 );
            Assert.IsFalse ( returnValue );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsCreateWithExceptionThrownByAddMethod_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Create };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemoveWithThrowException )
                .MapChangedItem ( requestHandler.AggregateNodeChangeWithThrowException )
                .MapAddedItem ( requestHandler.AggregateNodeAddWithThrowException )
                .Map ();

            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeAddWithThrowExceptionMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeChangeWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveWithThrowExceptionMethodCalled == 0 );
            Assert.IsFalse ( returnValue );
        }

        [TestMethod]
        public void Map_GivenWithRemovedDtosWithExceptionThrownByRemoveMethod_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto ();
            dtoCollection.RemovedItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity>
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };

            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemoveWithThrowException )
                .MapChangedItem ( requestHandler.AggregateNodeChangeWithThrowException )
                .MapAddedItem ( requestHandler.AggregateNodeAddWithThrowException )
                .Map ();

            Assert.IsTrue ( dtoCollection.RemovedItems[ 0 ].DataErrorInfoCollection.Count () > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeAddWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeChangeWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveWithThrowExceptionMethodCalled > 0 );
            Assert.IsFalse ( returnValue );
        }

        [TestMethod]
        public void Map_GivenWithMultipleRemovedAndCurrentDtos_Succeeds ()
        {
            int NoOfRemovedItems = 5;
            int NoOfAddedItems = 6;
            int NoOfUpdatedItems = 7;

            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            for ( int removedItemIndex = 0; removedItemIndex < NoOfRemovedItems; removedItemIndex++ )
            {
                dtoCollection.RemovedItems.Add ( new AggregateCollectionMapperTestDto () );
            }

            for ( int addedItemIndex = 0; addedItemIndex < NoOfAddedItems; addedItemIndex++ )
            {
                dtoCollection.CurrentItems.Add ( new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Create } );
            }

            for ( int updatedItemIndex = 0; updatedItemIndex < NoOfUpdatedItems; updatedItemIndex++ )
            {
                dtoCollection.CurrentItems.Add ( new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Update } );
            }

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRoot = new AggregateCollectionMapperTestAggregateRootEntity ();
            var aggregateNodeCollection = new List<AggregateCollectionMapperTestAggregateNodeEntity> ()
                                              { new AggregateCollectionMapperTestAggregateNodeEntity () };


            var aggregateNodeCollectionMapper = new AggregateNodeCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity,
                    AggregateCollectionMapperTestAggregateNodeEntity> (
                dtoCollection, aggregateRoot, aggregateNodeCollection );

            bool returnValue = aggregateNodeCollectionMapper
                .MapRemovedItem ( requestHandler.AggregateNodeRemove )
                .MapChangedItem ( requestHandler.AggregateNodeChange )
                .MapAddedItem ( requestHandler.AggregateNodeAdd )
                .Map ();

            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeRemoveMethodCalled == NoOfRemovedItems );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeChangeMethodCalled == NoOfUpdatedItems );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateNodeAddMethodCalled == NoOfAddedItems );
            Assert.IsTrue ( returnValue );
        }
    }
}