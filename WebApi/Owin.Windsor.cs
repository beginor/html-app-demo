using System.Web.Http;
using Microsoft.Owin.Logging;

namespace Owin {

    public static class WindsorExtensions {

        public static IAppBuilder UseWindsor(this IAppBuilder appBuilder, string configPath) {
            //appBuilder.Properties[configPath]
            return appBuilder;
        }

        public static IAppBuilder UseWebApiWithWindsor(this IAppBuilder appBuilder, HttpConfiguration config) {
            return appBuilder;
        }
    }
}