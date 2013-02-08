using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rem.Infrastructure.Tests.Service.Fixtures
{
    public class AggregateCollectionMapperTestRequestHandler
    {
        public int NoOfTimesAggregateNodeAddMethodCalled { get; private set; }
        public int NoOfTimesAggregateNodeChangeMethodCalled { get; private set; }
        public int NoOfTimesAggregateNodeRemoveMethodCalled { get; private set; }

        public int NoOfTimesAggregateNodeAddWithThrowExceptionMethodCalled { get; private set; }
        public int NoOfTimesAggregateNodeChangeWithThrowExceptionMethodCalled { get; private set; }
        public int NoOfTimesAggregateNodeRemoveWithThrowExceptionMethodCalled { get; private set; }

        public int NoOfTimesAggregateRootAddMethodCalled { get; private set; }
        public int NoOfTimesAggregateRootChangeMethodCalled { get; private set; }
        public int NoOfTimesAggregateRootRemoveMethodCalled { get; private set; }
        public int NoOfTimesAggregateRootFindMethodCalled { get; private set; }


        public int NoOfTimesAggregateRootAddWithThrowExceptionMethodCalled { get; private set; }
        public int NoOfTimesAggregateRootChangeWithThrowExceptionMethodCalled { get; private set; }
        public int NoOfTimesAggregateRootRemoveWithThrowExceptionMethodCalled { get; private set; }

        #region AggregateNode
        public void AggregateNodeAdd(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity)
        {
            NoOfTimesAggregateNodeAddMethodCalled++;
        }

        public void AggregateNodeChange(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity, AggregateCollectionMapperTestAggregateNodeEntity aggregateNodeCollectionMapperTestAggregateNodeEntity)
        {
            NoOfTimesAggregateNodeChangeMethodCalled++;
        }

        public void AggregateNodeRemove(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity, AggregateCollectionMapperTestAggregateNodeEntity aggregateNodeCollectionMapperTestAggregateNodeEntity)
        {
            NoOfTimesAggregateNodeRemoveMethodCalled++;
        }

        public void AggregateNodeAddWithThrowException(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity)
        {
            NoOfTimesAggregateNodeAddWithThrowExceptionMethodCalled++;
            throw new Exception();
        }

        public void AggregateNodeChangeWithThrowException(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity, AggregateCollectionMapperTestAggregateNodeEntity aggregateNodeCollectionMapperTestAggregateNodeEntity)
        {
            NoOfTimesAggregateNodeChangeWithThrowExceptionMethodCalled++;
            throw new Exception();
        }

        public void AggregateNodeRemoveWithThrowException(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity, AggregateCollectionMapperTestAggregateNodeEntity aggregateNodeCollectionMapperTestAggregateNodeEntity)
        {
            NoOfTimesAggregateNodeRemoveWithThrowExceptionMethodCalled++;
            throw new Exception();
        }
        #endregion

        #region AggregateRoot
        public void AggregateRootAdd(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto)
        {
            NoOfTimesAggregateRootAddMethodCalled++;
        }

        public void AggregateRootChange(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity)
        {
            NoOfTimesAggregateRootChangeMethodCalled++;
        }

        public void AggregateRootRemove(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity)
        {
            NoOfTimesAggregateRootRemoveMethodCalled++;
        }

        public AggregateCollectionMapperTestAggregateRootEntity AggregateRootFind(long l)
        {
            NoOfTimesAggregateRootFindMethodCalled++;
            return new AggregateCollectionMapperTestAggregateRootEntity ();
        }

        public void AggregateRootAddWithThrowException(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto)
        {
            NoOfTimesAggregateRootAddWithThrowExceptionMethodCalled++;
            throw new Exception();
        }

        public void AggregateRootChangeWithThrowException(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity)
        {
            NoOfTimesAggregateRootChangeWithThrowExceptionMethodCalled++;
            throw new Exception();
        }

        public void AggregateRootRemoveWithThrowException(AggregateCollectionMapperTestDto aggregateNodeCollectionMapperTestDto, AggregateCollectionMapperTestAggregateRootEntity aggregateNodeCollectionMapperTestAggregateRootEntity)
        {
            NoOfTimesAggregateRootRemoveWithThrowExceptionMethodCalled++;
            throw new Exception();
        }
        #endregion

    }
}
