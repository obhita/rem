using System;
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
    public class AggregateRootCollectionMapperTests
    {
        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void AggregateRootCollectionMapper_GivenNullDtoCollection_ThrowsArgumentException ()
        {
            new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( null );
        }

        [TestMethod]
        public void AggregateRootCollectionMapper_GivenNullDtoCollection_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection.Object );
        }

        [TestMethod]
        public void MapRemovedItem_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection.Object );

            var resultValue = aggregateRootCollectionMapper.MapRemovedItem ( null );

            Assert.AreSame ( aggregateRootCollectionMapper, resultValue );
        }

        [TestMethod]
        public void MapAddedItem_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection.Object );

            var resultValue = aggregateRootCollectionMapper.MapAddedItem ( null );

            Assert.AreSame ( aggregateRootCollectionMapper, resultValue );
        }

        [TestMethod]
        public void MapChangedItem_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection.Object );

            var resultValue = aggregateRootCollectionMapper.MapChangedItem ( null );

            Assert.AreSame ( aggregateRootCollectionMapper, resultValue );
        }

        [TestMethod]
        public void FindCollectionEntity_GivenNullAction_Succeeds ()
        {
            var dtoCollection = new Mock<ISoftDelete<AggregateCollectionMapperTestDto>> ();
            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection.Object );

            var resultValue = aggregateRootCollectionMapper.FindCollectionEntity ( null );

            Assert.AreSame ( aggregateRootCollectionMapper, resultValue );
        }

        [TestMethod]
        public void Map_GivenNoCurrentAndRemovedDtos_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper.Map ();

            Assert.IsTrue ( returnValue );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void Map_GivenNullActionForFindCollectionEntity_ThrowsArgumentException ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            dtoCollection.RemovedItems.Add ( new AggregateCollectionMapperTestDto () );

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            aggregateRootCollectionMapper
                .FindCollectionEntity ( null )
                .Map ();
        }

        [TestMethod]
        public void Map_GivenRemovedDtosWithMatchingKey_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            dtoCollection.RemovedItems.Add ( new AggregateCollectionMapperTestDto () );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemove )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.RemovedItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveMethodCalled > 0 );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsNoop_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();

            dtoCollection.CurrentItems.Add ( new AggregateCollectionMapperTestDto () );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemove )
                .MapChangedItem ( requestHandler.AggregateRootChange )
                .MapAddedItem ( requestHandler.AggregateRootAdd )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootChangeMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootAddMethodCalled == 0 );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsCreate_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Create };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemove )
                .MapChangedItem ( requestHandler.AggregateRootChange )
                .MapAddedItem ( requestHandler.AggregateRootAdd )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootChangeMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootAddMethodCalled > 0 );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsUpdate_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Update };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemove )
                .MapChangedItem ( requestHandler.AggregateRootChange )
                .MapAddedItem ( requestHandler.AggregateRootAdd )
                .Map ();

            Assert.IsTrue ( returnValue );
            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootChangeMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootAddMethodCalled == 0 );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void Map_GivenWithCurrentDtosAndEditStatusAsDelete_ThrowsArgumentException ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Delete };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemove )
                .MapChangedItem ( requestHandler.AggregateRootChange )
                .MapAddedItem ( requestHandler.AggregateRootAdd )
                .Map ();
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsUpdateWithExceptionThrownByChangeMethod_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Update };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemoveWithThrowException )
                .MapChangedItem ( requestHandler.AggregateRootChangeWithThrowException )
                .MapAddedItem ( requestHandler.AggregateRootAddWithThrowException )
                .Map ();

            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootAddWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootChangeWithThrowExceptionMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveWithThrowExceptionMethodCalled == 0 );
            Assert.IsFalse ( returnValue );
        }

        [TestMethod]
        public void Map_GivenWithCurrentDtosAndEditStatusAsCreateWithExceptionThrownByAddMethod_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto { EditStatus = EditStatus.Create };
            dtoCollection.CurrentItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemoveWithThrowException )
                .MapChangedItem ( requestHandler.AggregateRootChangeWithThrowException )
                .MapAddedItem ( requestHandler.AggregateRootAddWithThrowException )
                .Map ();

            Assert.IsTrue ( dtoCollection.CurrentItems[ 0 ].DataErrorInfoCollection.Count () > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootAddWithThrowExceptionMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootChangeWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveWithThrowExceptionMethodCalled == 0 );
            Assert.IsFalse ( returnValue );
        }

        [TestMethod]
        public void Map_GivenWithRemovedDtosWithExceptionThrownByRemoveMethod_Succeeds ()
        {
            var dtoCollection = new SoftDeleteObservableCollection<AggregateCollectionMapperTestDto> ();
            var dto = new AggregateCollectionMapperTestDto ();
            dtoCollection.RemovedItems.Add ( dto );

            var requestHandler = new AggregateCollectionMapperTestRequestHandler ();

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemoveWithThrowException )
                .MapChangedItem ( requestHandler.AggregateRootChangeWithThrowException )
                .MapAddedItem ( requestHandler.AggregateRootAddWithThrowException )
                .Map ();

            Assert.IsTrue ( dtoCollection.RemovedItems[ 0 ].DataErrorInfoCollection.Count () > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled > 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootAddWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootChangeWithThrowExceptionMethodCalled == 0 );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveWithThrowExceptionMethodCalled > 0 );
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

            var aggregateRootCollectionMapper = new AggregateRootCollectionMapper
                <AggregateCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity> ( dtoCollection );

            bool returnValue = aggregateRootCollectionMapper
                .FindCollectionEntity ( requestHandler.AggregateRootFind )
                .MapRemovedItem ( requestHandler.AggregateRootRemove )
                .MapChangedItem ( requestHandler.AggregateRootChange )
                .MapAddedItem ( requestHandler.AggregateRootAdd )
                .Map ();

            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootFindMethodCalled == NoOfRemovedItems + NoOfUpdatedItems );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootRemoveMethodCalled == NoOfRemovedItems );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootChangeMethodCalled == NoOfUpdatedItems );
            Assert.IsTrue ( requestHandler.NoOfTimesAggregateRootAddMethodCalled == NoOfAddedItems );
            Assert.IsTrue ( returnValue );
        }
    }
}