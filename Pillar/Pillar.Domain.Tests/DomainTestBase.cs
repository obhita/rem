using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Tests;
using Pillar.Domain.Event;
using StructureMap;
using IContainer = Pillar.Common.InversionOfControl.IContainer;

namespace Pillar.Domain.Tests
{
    [TestClass]
    public abstract class DomainTestBase : TestFixtureBase
    {
        public StructureMap.IContainer StructureMapContainer { get; set; }
        public IContainer Container { get; set; }

        protected override void OnSetup ()
        {
            base.OnSetup ();

            StructureMapContainer = new Container ();
            Container = new IoC.StructureMap.Container ( StructureMapContainer );
            
            StructureMapContainer.Configure ( c => c.For<IDomainEventService>().HybridHttpOrThreadLocalScoped ().Use<DomainEventService> () );

            ServiceLocatorUnitTestHelper.SetThreadLocalServiceLocator ( Container );
        }

        protected override void OnTeardown ()
        {
            base.OnTeardown ();

            ServiceLocatorUnitTestHelper.SetThreadLocalServiceLocator ( null );
            Container = null;
            StructureMapContainer = null;
        }
    }
}
