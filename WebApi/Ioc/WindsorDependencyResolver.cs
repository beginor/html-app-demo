using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace WebApi.Ioc {

    public class WindsorDependencyScope : IDependencyScope {

        private IWindsorContainer container;

        protected IWindsorContainer Container {
            get {
                return container;
            }
        }

        public WindsorDependencyScope(IWindsorContainer container) {
            this.container = container;
        }

        public void Dispose() {
            if (container.Parent != null) {
                container.RemoveChildContainer(container);
            }
            container.Dispose();
        }

        public object GetService(Type serviceType) {
            try {
                return container.Resolve(serviceType);
            }
            catch (Exception ex) {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            var services = new List<Object>();
            try {
                var resolved = container.ResolveAll(serviceType);
                foreach (var service in resolved) {
                    services.Add(service);
                }
            }
            catch (Exception ex) {

            }
            return services;
        }

    }

    public class WindsorDependencyResolver : WindsorDependencyScope, IDependencyResolver {

        public WindsorDependencyResolver(IWindsorContainer container) : base(container) { }

        public IDependencyScope BeginScope() {
            var childContainer = new WindsorContainer();
            Container.AddChildContainer(childContainer);
            return new WindsorDependencyScope(childContainer);
        }

    }

}