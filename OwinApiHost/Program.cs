﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;
using Owin;
using Owin.Windsor;
using OwinApiHost.Middlewares;

namespace OwinApiHost {

    class Program {

        static void Main(string[] args) {
            var app = new AppBuilder();
            Nowin.OwinServerFactory.Initialize(app.Properties);

            app.UseWindsorContainer("windsor.config");

            var logMiddleware = app.GetWindsorContainer().Resolve<ConsoleLogMiddleware>();
            app.Use(logMiddleware);

            app.Use<SimpleStaticFileMiddleWare>(System.IO.Path.Combine(Environment.CurrentDirectory, @"../www"));

            var startup = new WebApi.Startup();
            startup.Configuration(app);

            var builder = new Nowin.ServerBuilder();
            const string ip = "127.0.0.1";
            const int port = 8888;
            builder.SetAddress(System.Net.IPAddress.Parse(ip)).SetPort(port)
                .SetOwinApp(app.Build())
                .SetOwinCapabilities((IDictionary<string, object>)app.Properties[Nowin.OwinKeys.ServerCapabilitiesKey]);

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
