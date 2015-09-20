using System.Web.Http;
using Microsoft.Owin;
using Owin;
using WebApi;
using Beginor.Owin.Security.Aes;
using Beginor.Owin.Windsor;
using Beginor.Owin.WebApi.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json.Serialization;
using NHibernate;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApi {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            var container = app.GetWindsorContainer();
            container.Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(() => HibernateFactory.CreateSessionFactory(""))
                         .LifestyleSingleton()
                );
            ConfigIdentity(app);
            ConfigAuth(app);
            ConfigWebApi(app);
        }

        private void ConfigAuth(IAppBuilder app) {
            app.UseAesDataProtectionProvider();
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
            });
        }

        private static void ConfigIdentity(IAppBuilder app) {
            
            app.CreatePerOwinContext<IWindsorContainer>((opt, context) => app.GetWindsorContainer(), (opt, c) => {
                // Do not dispose windsor container.
            });
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
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
