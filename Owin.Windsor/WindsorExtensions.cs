using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Owin.Windsor {

    public static class WindsorExtensions {

        private static readonly string AppWindsorContainer = "app.windsor_container";

        public static IAppBuilder UseWindsorContainer(this IAppBuilder app, string configPath) {
            if (app == null) {
                throw new ArgumentNullException("app");
            }
            if (string.IsNullOrEmpty(configPath)) {
                throw new ArgumentNullException("configPath");
            }

            var container = new WindsorContainer();
            var installer = Configuration.FromXmlFile(configPath);
            container.Install(installer);

            UseWindsorContainer(app, container);
            return app;
        }

        public static IAppBuilder UseWindsorContainer(this IAppBuilder app, IWindsorContainer container) {
            if (app == null) {
                throw new ArgumentNullException("app");
            }
            if (container == null) {
                throw new ArgumentNullException("container");
            }

            app.Properties[AppWindsorContainer] = container;

            return app;
        }

        public static IWindsorContainer GetWindsorContainer(this IAppBuilder app) {
            if (!app.Properties.ContainsKey(AppWindsorContainer)) {
                throw new InvalidOperationException("No windsor container configured.");
            }
            return (IWindsorContainer)app.Properties[AppWindsorContainer];
        }

    }
}