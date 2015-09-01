using System.Web.Http;
using Microsoft.Owin;
using Owin;
using WebApi;
using Beginor.Owin.Windsor;
using Beginor.Owin.WebApi.Windsor;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApi {

    public class Startup {

        public void Configuration(IAppBuilder app) {

            app.UseCookieAuthentication(new CookieAuthenticationOptions {

            });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.UseWindsorContainer(app.GetWindsorContainer());

            app.UseWebApi(config);
        }
    }
}
