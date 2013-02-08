using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pillar.Domain;

namespace Rem.Infrastructure.Tests.Service.Fixtures
{
    public class AggregateCollectionMapperTestAggregateNodeEntity : AbstractAggregateNode
    {
        public override IAggregateRoot AggregateRoot
        {
            get { throw new NotImplementedException(); }
        }
    }
}
