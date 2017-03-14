using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http.Dependencies;
using SimpleInjector;

namespace Autenticacao.Api.Helpers
{
    public sealed class SimpleInjectorWebApiDependencyResolver
       : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly Container container;

        public SimpleInjectorWebApiDependencyResolver(
            Container container)
        {
            this.container = container;
        }

        [DebuggerStepThrough]
        public IDependencyScope BeginScope()
        {
            return this;
        }

        [DebuggerStepThrough]
        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)this.container)
                .GetService(serviceType);
        }

        [DebuggerStepThrough]
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.GetAllInstances(serviceType);
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
        }
    }
}
