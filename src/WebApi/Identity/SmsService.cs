using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Beginor.Owin.Application.Identity {

    public class SmsService : IIdentityMessageService {

        public Task SendAsync(IdentityMessage message) {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

}