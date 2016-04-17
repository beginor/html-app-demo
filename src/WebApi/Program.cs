using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin.Builder;
using Nowin;

namespace Beginor.Owin.Application {
    
    public class Program {

        public static void Main(string[] args) {
            // create a new AppBuilder
            var app = new AppBuilder();
            // init nowin's owin server factory.
            OwinServerFactory.Initialize(app.Properties);
            var startup = new Startup();
            startup.Configure(app);

            var serverBuilder = new ServerBuilder();
            const string ip = "0.0.0.0";
            const int port = 8080;
            serverBuilder.SetAddress(IPAddress.Parse(ip)).SetPort(port)
                         .SetOwinApp(app.Build())
                         .SetOwinCapabilities((IDictionary<string, object>)app.Properties[OwinKeys.ServerCapabilitiesKey]);

            using (var server = serverBuilder.Build()) {

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

