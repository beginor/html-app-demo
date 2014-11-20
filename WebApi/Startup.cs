using System.Web.Http;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin;
using Owin;
using WebApi.Ioc;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new WindsorContainer();
            var installer = Castle.Windsor.Installer.Configuration.FromXmlFile("windsor.config");
            container.Install(installer);
            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<IDependencyResolver>().ImplementedBy<WindsorDependencyResolver>()
            );
            //container.Register(Castle.MicroKernel.Registration.
            var resolver = container.Resolve<IDependencyResolver>();
            config.DependencyResolver = resolver;

            app.UseWebApi(config);
        }
    }
}
