using System.Web.Http;
using Owin;
using Beginor.Owin.StaticFile;
using Beginor.Owin.WebApi.Windsor;
using Beginor.Owin.Windsor;
using Microsoft.Owin.Logging;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.DataProtection;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Beginor.Owin.Application.Security;

[assembly: Microsoft.Owin.OwinStartup(typeof(Beginor.Owin.Application.Startup))]

namespace Beginor.Owin.Application {

    public class Startup {

        public void Configure(IAppBuilder app) {
            var windsorFile = "windsor.config";//System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "windsor.config");
            app.UseWindsorContainer(windsorFile);
            var container = app.GetWindsorContainer();

            app.UseWindsorMiddleWare();

            // owin logger factory
            var loggerFactory = container.Resolve<ILoggerFactory>();
            app.SetLoggerFactory(loggerFactory);

            var options = container.Resolve<StaticFileMiddlewareOptions>();
            app.UseStaticFile(options);

            // identity;
            container.Register(
                Component.For<IAuthenticationManager>().FromOwinContext().LifestyleTransient()
            );
            var dataProtectionProvider = container.Resolve<IDataProtectionProvider>();
            app.SetDataProtectionProvider(dataProtectionProvider);

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new ApplicationCookieAuthenticationcProvider()
            });

            var config = new HttpConfiguration();
            config.Formatters.JsonFormatter.Indent = true;
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            cors.SupportsCredentials = true;
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}"
            );

            config.UseWindsorContainer(container);
            app.UseWebApi(config);
        }

    }

}
