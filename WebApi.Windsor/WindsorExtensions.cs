using System.Web.Http;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace WebApi.Windsor {

    public static class WindsorExtensions {

        public static void UseWindsorContainer(this HttpConfiguration config, IWindsorContainer container) {
            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<IDependencyResolver>().ImplementedBy<WindsorDependencyResolver>()
            );
            //container.Register(Castle.MicroKernel.Registration.
            var resolver = container.Resolve<IDependencyResolver>();
            config.DependencyResolver = resolver;
        }
    }
}