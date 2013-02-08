using System;
using Pillar.Common.InversionOfControl;

namespace Pillar.Common.Tests
{
    public static class ServiceLocatorUnitTestHelper
    {
        [ThreadStatic] private static IContainer _container;
        
        private static bool _isInitialized = false;

        public static void Initialize ()
        {
            InversionOfControl.IoC.SetContainerProvider( () => _container);
            _isInitialized = true;
        }

        public static void SetThreadLocalServiceLocator(IContainer container)
        {
            if (! _isInitialized)
                throw new ApplicationException("Call Initialize() method first.");

            _container = container;
        }
    }
}