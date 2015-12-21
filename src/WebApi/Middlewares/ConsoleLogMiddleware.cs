using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Middlewares {

    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class ConsoleLogMiddleware {

        private AppFunc next;

        public void Initialize(AppFunc next) {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env) {
            Console.WriteLine("{0} {1} {2}", DateTime.Now, env["owin.RequestMethod"], env["owin.RequestPath"]);
            await next.Invoke(env);
            Console.WriteLine("Return HTTP status: {0}", env["owin.ResponseStatusCode"]);
        }

    }
}

