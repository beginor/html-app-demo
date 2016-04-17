using System;
using System.Web.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Owin.Security;

namespace Beginor.Owin.Application.Results {

    public class ChallengeResult : IHttpActionResult {

        public string LoginProvider { get; set; }
        public HttpRequestMessage Request { get; set; }
        private IAuthenticationManager authenticationManager;

        public ChallengeResult(string loginProvider, ApiController controller, IAuthenticationManager authenticationManager) {
            LoginProvider = loginProvider;
            Request = controller.Request;
            this.authenticationManager = authenticationManager;
        }


        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken) {
            authenticationManager.Challenge(LoginProvider);

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized) {
                RequestMessage = Request
            };
            return Task.FromResult(response);
        }
    }
}

