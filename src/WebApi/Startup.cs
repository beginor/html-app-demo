using System.Web.Http;
using Microsoft.Owin;
using Owin;
using WebApi;
using Beginor.Owin.StaticFile;
using Beginor.Owin.Windsor;
using Beginor.Owin.Security.Aes;
using Beginor.Owin.WebApi.Windsor;
using Microsoft.Owin.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Serialization;
using NHibernate;
using NHibernate.Cfg;
using WebApi.Middlewares;
using Castle.Windsor.Installer;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApi {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            app.UseWindsorContainer("windsor.config");
            var container = app.GetWindsorContainer();

            app.UseWindsorMiddleWare();

            var loggerFactory = container.Resolve<Microsoft.Owin.Logging.ILoggerFactory>();
            app.SetLoggerFactory(loggerFactory);

            var logMiddleware = container.Resolve<ConsoleLogMiddleware>();
            app.Use(logMiddleware);

            var options = container.Resolve<StaticFileMiddlewareOptions>();
            app.UseStaticFile(options);
            // identity;
            container.Register(
                Component.For<IAuthenticationManager>().FromOwinContext().LifestyleTransient()
            );

            // authentication
            var dataProtectionProvider = container.Resolve<IDataProtectionProvider>();
            app.SetDataProtectionProvider(dataProtectionProvider);
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
            });

            ConfigWebApi(app);
        }

        private static void ConfigWebApi(IAppBuilder app) {
            var container = app.GetWindsorContainer();

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.UseWindsorContainer(container);

            app.UseWebApi(config);
        }

    }
}
