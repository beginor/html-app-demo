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
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApi.IdentitySupport;
using System;
using WebApi.Data;

[assembly: OwinStartup(typeof(Startup))]

namespace WebApi {

    public class Startup {

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

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
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new CookieAuthenticationProvider {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<ApplicationUser>, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
                    )
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
//            OAuthOptions = new OAuthAuthorizationServerOptions {
//                TokenEndpointPath = new PathString("/Token"),
//                Provider = new ApplicationOAuthProvider(PublicClientId),
//
//                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
//                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
//                // In production mode set AllowInsecureHttp = false
//                AllowInsecureHttp = true
//            };
//
//            // Enable the application to use bearer tokens to authenticate users
//            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: ""
            //);

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: ""
            //);

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: ""
            //);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions {
            //    ClientId = "",
            //    ClientSecret = ""
            //});
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
