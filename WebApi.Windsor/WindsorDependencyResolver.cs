using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace WebApi.Windsor {

    public class WindsorDependencyResolver : WindsorDependencyScope, IDependencyResolver {

        public WindsorDependencyResolver(IWindsorContainer container) : base(container) { }

        public IDependencyScope BeginScope() {
            var childContainer = new WindsorContainer();
            Container.AddChildContainer(childContainer);
            return new WindsorDependencyScope(childContainer);
        }

    }

}