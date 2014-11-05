using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace OwinApiHost.Middlewares {

    public class ConsoleLogMiddleware : OwinMiddleware {

        public ConsoleLogMiddleware(OwinMiddleware next)
            : base(next) {
        }

        public async override Task Invoke(IOwinContext context) {
            Console.WriteLine("{0} {1} {2}", DateTime.Now, context.Request.Method, context.Request.Uri);
            await Next.Invoke(context);
            Console.WriteLine("Return HTTP status: {0}", context.Response.StatusCode);
        }

    }
}