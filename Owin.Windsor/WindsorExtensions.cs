using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Owin.Windsor {

    public static class WindsorExtensions {

        private static readonly string AppWindsorContainer = "app.windsor_container";

        public static IAppBuilder UseWindsorContainer(this IAppBuilder appBuilder, string configPath) {
            if (appBuilder == null) {
                throw new ArgumentNullException("appBuilder");
            }
            if (string.IsNullOrEmpty(configPath)) {
                throw new ArgumentNullException("configPath");
            }

            var container = new WindsorContainer();
            var installer = Configuration.FromXmlFile(configPath);
            container.Install(installer);

            UseWindsorContainer(appBuilder, container);
            return appBuilder;
        }

        public static IAppBuilder UseWindsorContainer(this IAppBuilder appBuilder, IWindsorContainer container) {
            if (appBuilder == null) {
                throw new ArgumentNullException("appBuilder");
            }
            if (container == null) {
                throw new ArgumentNullException("container");
            }

            var rootContainer = GetWindsorContainer(appBuilder);
            rootContainer.AddChildContainer(container);

            return appBuilder;
        }

        public static IWindsorContainer GetWindsorContainer(this IAppBuilder app) {
            if (!app.Properties.ContainsKey(AppWindsorContainer)) {
                lock(typeof(WindsorExtensions)) {
                    if (!app.Properties.ContainsKey(AppWindsorContainer)) {
                        var container = new WindsorContainer();
                        app.Properties[AppWindsorContainer] = container;
                    }
                }
            }
            return (IWindsorContainer)app.Properties[AppWindsorContainer];
        }

    }
}