using System.Web.Http;
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
            var resolver = new WindsorDependencyResolver(container);
            config.DependencyResolver = resolver;

            app.UseWebApi(config);
        }
    }
}
