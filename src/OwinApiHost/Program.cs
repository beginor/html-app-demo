﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;
using Nowin;
using WebApi;

namespace OwinApiHost {

    class Program {

        static void Main(string[] args) {
            var app = new AppBuilder();
            OwinServerFactory.Initialize(app.Properties);

            var startup = new Startup();
            startup.Configuration(app);

            var builder = new ServerBuilder();
            const string ip = "127.0.0.1";
            const int port = 8888;
            builder.SetAddress(IPAddress.Parse(ip)).SetPort(port)
                .SetOwinApp(app.Build())
                .SetOwinCapabilities((IDictionary<string, object>)app.Properties[OwinKeys.ServerCapabilitiesKey]);

            using (var server = builder.Build()) {

                var serverRef = new WeakReference<INowinServer>(server);

                Task.Run(() => {
                    INowinServer nowinServer;
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