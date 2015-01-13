using System.Web.Http;
using System.Web.Http.Dependencies;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin;
using Owin;
using Owin.Windsor;
using WebApi;
using WebApi.Windsor;

[assembly: OwinStartup(typeof(Startup))]

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
            config.UseWindsorContainer(app.GetWindsorContainer());

            app.UseWebApi(config);
        }
    }
}
