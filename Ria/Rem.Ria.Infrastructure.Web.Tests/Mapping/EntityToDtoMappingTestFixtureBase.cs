using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Tests;

namespace Rem.Ria.Infrastructure.Web.Tests.Mapping
{
    [TestClass]
    public abstract class EntityToDtoMappingTestFixtureBase : TestFixtureBase
    {
        protected void AssertAutoMapperConfigurationIsValid ()
        {
            Mapper.AssertConfigurationIsValid ();
        }

        protected static bool HasEntityToDtoMapping<T, TD> ()
        {
            if ( ( Mapper.GetAllTypeMaps ().Count ( m =>
                                                    m.SourceType == typeof ( T ) &&
                                                    m.DestinationType == typeof ( TD ) ) ) == 0 )
            {
                return false;
            }

            return true;
        }
    }
}