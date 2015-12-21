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

[assembly: OwinStartup(typeof(Startup))]

namespace WebApi {

    public class Startup {

        public void Configuration(IAppBuilder app) {

            var container = ConfigWindsor();
            app.UseWindsorContainer(container);

            var loggerFactory = container.Resolve<Microsoft.Owin.Logging.ILoggerFactory>();
            app.SetLoggerFactory(loggerFactory);

            app.UseAesDataProtectionProvider();

            var logMiddleware = container.Resolve<ConsoleLogMiddleware>();
            app.Use(logMiddleware);

            var options = container.Resolve<StaticFileMiddlewareOptions>();
            app.UseStaticFile(options);

            container.Register(
                Component.For<ISessionFactory>()
                .UsingFactoryMethod(
                    factoryMethod: (kernel, componentModel, creationContext) => {
                        var config = new NHibernate.Cfg.Configuration();
                        var sessionFactory = config.BuildSessionFactory();
                        return sessionFactory;
                    },
                    managedExternally: true
                )
                .LifestyleSingleton()
            );
            ConfigIdentity(app);
            ConfigAuth(app);
            ConfigWebApi(app);
        }

        private static IWindsorContainer ConfigWindsor() {
            IWindsorContainer container = new WindsorContainer();
            //var xml = new Castle.Windsor.Configuration.Interpreters.XmlInterpreter("windsor.config");

            //container.Install()
            // log4net facility
            container.AddFacility(new Castle.Facilities.Logging.LoggingFacility(Castle.Facilities.Logging.LoggerImplementation.Log4net, "log.config"));
            // console log middleware
            container.Register(
                // static file middleware options
                Component.For<StaticFileMiddlewareOptions>()
                .DependsOn(
                    Dependency.OnValue("RootDirectory", "../www"),
                    Dependency.OnValue("DefaultFile", "index.html")
                )
                .LifestyleSingleton(),
                // owin logger factory.
                Component.For<Microsoft.Owin.Logging.ILoggerFactory>()
                .ImplementedBy<Beginor.Owin.Logging.CastleLoggerFactory>()
                .LifestyleSingleton()
            );

            Castle.Core.Configuration.Xml.XmlConfigurationDeserializer d = new Castle.Core.Configuration.Xml.XmlConfigurationDeserializer();
            var cfg = d.Deserialize(null);
            cfg.Children.Find(c => c.Name == "database");

            return container;
        }

        private void ConfigAuth(IAppBuilder app) {
            app.UseAesDataProtectionProvider();

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
            });
        }

        private static void ConfigIdentity(IAppBuilder app) {
            //
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
