using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Core.Logging;
using Castle.Windsor;

namespace WebApi.Windsor {

    public class WindsorDependencyScope : IDependencyScope {

        private readonly IWindsorContainer container;
        private ILogger logger = NullLogger.Instance;

        public ILogger Logger {
            get { return logger; }
            set { logger = value; }
        }

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
            Logger.DebugFormat("GetService of type {0}", serviceType);
            object service = null;
            try {
                service = container.Resolve(serviceType);
            }
            catch (Exception ex) {
                Logger.Error(string.Format("Can not resolve {0}", serviceType), ex);
            }
            return service;
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            Logger.DebugFormat("Get All Service of type {0}", serviceType);
            return container.ResolveAll(serviceType).Cast<object>();
        }

    }
}