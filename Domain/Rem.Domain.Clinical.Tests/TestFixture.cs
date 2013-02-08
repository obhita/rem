using Pillar.Common.Tests;
using Pillar.Domain.Event;
using Pillar.FluentRuleEngine;

namespace Rem.Domain.Tests
{
    class TestFixture
    {
        public static void SetupServiceLocatorFixture(ServiceLocatorFixture serviceLocatorFixture)
        {
            serviceLocatorFixture.StructureMapContainer.Configure(
                c =>
                    {
                        c.For<IDomainEventService>().Use(new DomainEventService());
                    });

            serviceLocatorFixture.StructureMapContainer.Configure(
                c => c.Scan(x =>
                {
                    // in the scan operation, include all needed dlls (Rem.*)
                    // be cautious in the future - this could still pick up unwanted assemblies,
                    // such as the stray test project that mistakenly ends up in the bin folder.
                    // so consider those possibilities if errors pop up, and you're led here.
                    x.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("Rem."));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));

                    x.ConnectImplementationsToTypesClosing(typeof(IRuleCollectionCustomizer<,>));
                }));
        }
    }
}
