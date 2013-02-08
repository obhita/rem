using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pillar.Common.InversionOfControl;

namespace Rem.Infrastructure.Mvc.Bootstrapper
{
    public class CustomDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public CustomDependencyResolver (IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.TryResolve (serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var list = _container.ResolveAll(serviceType);
            List<object> listObject = list.Cast<object> ().ToList ();
            return listObject;
        }
    }
}
