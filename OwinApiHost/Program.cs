using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;
using Owin;
using OwinApiHost.Middlewares;

namespace OwinApiHost {

    class Program {

        static void Main(string[] args) {
            var appBuilder = new AppBuilder();
            Nowin.OwinServerFactory.Initialize(appBuilder.Properties);

            appBuilder.Use<ConsoleLogMiddleware>();

            appBuilder.Use<SimpleStaticFileMiddleWare>(System.IO.Path.Combine(Environment.CurrentDirectory, @"../../www"));

            var startup = new WebApi.Startup();
            startup.Configuration(appBuilder);

            var builder = new Nowin.ServerBuilder();
            const string ip = "127.0.0.1";
            const int port = 8888;
            builder.SetAddress(System.Net.IPAddress.Parse(ip)).SetPort(port)
                .SetOwinApp(appBuilder.Build())
                .SetOwinCapabilities((IDictionary<string, object>)appBuilder.Properties[Nowin.OwinKeys.ServerCapabilitiesKey]);

            using (var server = builder.Build()) {

                var serverRef = new WeakReference<Nowin.INowinServer>(server);

                Task.Run(() => {
                    Nowin.INowinServer nowinServer;
                    if (serverRef.TryGetTarget(out nowinServer)) {
                        nowinServer.Start();
                    }
                });

                var baseAddress = "http://" + ip + ":" + port + "/";
                Console.WriteLine("Nowin server listening {0}, press ENTER to exit.", baseAddress);

                Console.ReadLine();
            }
        }
    }
}
