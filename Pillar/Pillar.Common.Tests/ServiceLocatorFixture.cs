using System;
using StructureMap;
using IContainer = Pillar.Common.InversionOfControl.IContainer;

namespace Pillar.Common.Tests
{
    public sealed class ServiceLocatorFixture : IDisposable
    {
        [ThreadStatic]
        private static IContainer _container;

        private bool _disposed;

        static ServiceLocatorFixture()
        {
            InversionOfControl.IoC.SetContainerProvider( () => _container );
        }

        public ServiceLocatorFixture()
        {
            StructureMapContainer = new Container();
            _container = new Pillar.IoC.StructureMap.Container(StructureMapContainer);
        }

        public StructureMap.IContainer StructureMapContainer { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _container = null;
            }
            _disposed = true;
        }
    }
}
