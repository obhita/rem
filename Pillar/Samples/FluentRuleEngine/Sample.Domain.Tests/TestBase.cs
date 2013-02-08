namespace Sample.Domain.Tests
{
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Pillar.Domain.Event;
    using Pillar.FluentRuleEngine;

    using StructureMap;
    using StructureMap.ServiceLocatorAdapter;

    [TestClass]
    public class TestBase
    {
        protected IContainer Container;

        [TestInitialize]
        public void Setup()
        {
            this.Container = new Container();
            this.Container.Configure(c => c.ForSingletonOf<IDomainEventService>().Use<DomainEventService>());
            Container.Configure(x => x.Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory();
                scanner.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));
                scanner.WithDefaultConventions();
            }));

            var structureMapServiceLocator = new StructureMapServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(() => structureMapServiceLocator);
        }

        [TestCleanup]
        public void Teardown()
        {
            Container = null;
            ServiceLocator.SetLocatorProvider(null);
        }
    }
}
